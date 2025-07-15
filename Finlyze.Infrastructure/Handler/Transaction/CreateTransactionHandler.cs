using System.Diagnostics;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class CreateTransactionHandler : ICreateTransactionHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly ITransactionRepository _transRepository;
    private readonly IAppLogRepository _appRepository;

    public CreateTransactionHandler(IUserAccountQuery userQuery, ITransactionRepository transReposotory, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _transRepository = transReposotory;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<Transaction>> Handle(CreateTransactionCommand command)
    {
        try
        {
            var existingUser = await _userQuery.GetByIdAsync(command.UserAccountId);

            if (existingUser is null)
                return ResultHandler<Transaction>.Fail("Usuário com esse Id não encontrado.");

            var transaction = new Transaction(command.TransactionTitle, command.TransactionDescription, command.Amount, command.TypeTransaction, command.TransactionCreateAt, command.UserAccountId);
            var id = await _transRepository.CreateAsync(transaction);

            transaction.ChangeId(id);

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Transaction", $"Transaction: {transaction.Id} criado com sucesso"));
            return ResultHandler<Transaction>.Ok("Transação criado com sucesso.", transaction);
        }

        catch (Exception ex) when (ex is DomainException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Validation, "Create", $"Erro na validação ao criar a transaction do usuário: -> {errorMsg}"));
            return ResultHandler<Transaction>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Create", $"Ocorreu um erro na criar a transaction do usuário: {errorMsg}"));
            return ResultHandler<Transaction>.Fail($"Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}