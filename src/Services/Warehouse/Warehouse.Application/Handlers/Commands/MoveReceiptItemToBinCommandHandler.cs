﻿using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
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
            Receipt.Item item = this.DatabaseContext.ReceiptItems.FirstOrDefault(x =>
                x.ReceiptId == request.ReceiptId &&
                x.PositionId == request.PositionId &&
                x.WareId == request.WareId);

            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.ReceiptItem_EntityNotFoundException, request.ReceiptId, request.PositionId, request.WareId));
            }

            item.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
