﻿using Dapper;
using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;

namespace MyStorageApplication.Database.Repositories
{
    public class MovementsReadOnlyRepository(DatabaseSession session) : IMovementsReadOnlyRepository
    {
        private readonly DatabaseSession _session = session;

        public async Task<IEnumerable<HistoryMovementDto>> GetAllAsync()
            => await _session
                .Connection.QueryAsync<HistoryMovementDto>(@"
                    SELECT 
	                    M.MovimentId,
	                    M.ProductName AS ""ProductName"", 
	                    S.Identification AS ""StorageName"",
	                    M.Amount,
	                    M.""Type""
                    FROM Moviments M
                    INNER JOIN Products P ON P.ProductId = M.MovimentId
                    INNER JOIN Storage S ON S.StorageId = M.StorageId");

        public async Task<IEnumerable<HistoryMovementDto>> QueryAsync(string q)
            => await _session
                .Connection.QueryAsync<HistoryMovementDto>(@"
                    SELECT 
	                    M.MovimentId,
	                    M.ProductName AS ""ProductName"", 
	                    S.Identification AS ""StorageName"",
	                    M.Amount,
	                    M.""Type""
                    FROM Moviments M
                    INNER JOIN Products P ON P.ProductId = M.MovimentId
                    INNER JOIN Storage S ON S.StorageId = M.StorageId
                    WHERE (M.ProductName LIKE @WhereLike OR S.Identification LIKE @WhereLike)", new { WhereLike = q });
    }
}