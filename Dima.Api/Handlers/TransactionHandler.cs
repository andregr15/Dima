using Dima.Api.Data;
using Dima.Core.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                Title = request.Title,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Type = request.Type
            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(
                null,
                500,
                "[E002x0001] Não foi possível criar a Transação"
            );
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await GetTransactionAsync(request.UserId, request.Id);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            //transaction.UserId = request.UserId,
            transaction.Title = request.Title;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Amount = request.Amount;
            transaction.CategoryId = request.CategoryId;
            transaction.Type = request.Type;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(
                transaction,
                message: "Transação atualizada com sucesso"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(
                null,
                500,
                "[E002x0002] Não foi possível atualizar a Transação"
            );
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await GetTransactionAsync(request.UserId, request.Id);

            if (transaction is null)
                return new Response<Transaction?>(null, 404, "Transação não encontrada");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(
                transaction,
                message: "Transação removida com sucesso"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(
                null,
                500,
                "[E002x0003] Não foi possível remover a Transação"
            );
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context
                .Transactions.AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return transaction is null
                ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                : new Response<Transaction?>(transaction);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(
                null,
                500,
                "[E002x0004] Não foi possível encontrar a Transação"
            );
        }
    }

    public async Task<PagedResponse<IEnumerable<Transaction>?>> GetByPeriodAsync(
        GetTransactionsByPeriodRequest request
    )
    {
        try
        {
            request.StartDate ??= DateTime.Now.FirstDayInMonth();
            request.EndDate ??= DateTime.Now.LastDayInMonth();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new PagedResponse<IEnumerable<Transaction>?>(
                null,
                500,
                "[E002x0006] Não foi possível determinar a data de início ou término"
            );
        }

        try
        {
            var query = context
                .Transactions.AsNoTracking()
                .Where(x =>
                    x.UserId == request.UserId
                    && x.CreatedAt >= request.StartDate
                    && x.CreatedAt <= request.EndDate
                )
                .OrderBy(x => x.CreatedAt);

            var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var totalCount = await query.CountAsync();

            return new PagedResponse<IEnumerable<Transaction>?>(
                transactions,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new PagedResponse<IEnumerable<Transaction>?>(
                null,
                500,
                "[E002x0005] Não foi possível consultar as Transações"
            );
        }
    }

    private Task<Transaction?> GetTransactionAsync(string userId, long id) =>
        context.Transactions.Where(x => x.UserId == userId).FirstOrDefaultAsync(x => x.Id == id);
}
