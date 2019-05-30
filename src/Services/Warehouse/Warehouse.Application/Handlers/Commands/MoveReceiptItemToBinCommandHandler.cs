﻿using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveReceiptItemToBinCommandHandler : IRequestHandler<MoveReceiptItemToBinCommand, Receipt.Item>
    {
        public MoveReceiptItemToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Receipt.Item> Handle(MoveReceiptItemToBinCommand request, CancellationToken cancellationToken)
        {
            Receipt.Item item = await this.DatabaseContext.ReceiptItems.FindAsync(new object[] { request.ReceiptId, request.WareId }, cancellationToken);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_EntityNotFoundException"], request.ReceiptId, request.WareId));
            }

            item.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
