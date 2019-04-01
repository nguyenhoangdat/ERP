﻿using Restmium.Messaging;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Integration.Ordering
{
    /// <summary>
    /// Event produced by Ordering.API
    /// </summary>
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderCreatedIntegrationEvent() : base()
        {

        }
        public OrderCreatedIntegrationEvent(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, IEnumerable<OrderItem> items) : this()
        {
            this.OrderId = orderId;
            this.UtcDispatchDate = utcDispatchDate;
            this.UtcDeliveryDate = utcDeliveryDate;
            this.OrderItems = items;
        }

        public long OrderId { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

        public DateTime UtcDispatchDate { get; set; } //Odeslat dne
        public DateTime UtcDeliveryDate { get; set; } //Dodat dne

        public class OrderItem
        {
            public OrderItem(int productId, int units)
            {
                this.ProductId = productId;
                this.Units = units;
            }

            public int ProductId { get; }
            public int Units { get; }
        }
    }
}