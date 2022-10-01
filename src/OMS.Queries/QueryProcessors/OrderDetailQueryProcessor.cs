using Microsoft.EntityFrameworkCore;
using OMS.API.Models.OrderDetailDto;
using OMS.Data.Access.DAL;
using OMS.Data.Model.Entities;
using OMS.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Queries.QueryProcessors
{
    public class OrderDetailQueryProcessor : IOrderDetailQueryProcessor
    {
        private IUnitOfWork _unitOfWork;

        public OrderDetailQueryProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<OrderDetail> Get(CancellationToken token)
        {
            //throw new NotImplementedException();
            return (IQueryable<OrderDetail>)this._unitOfWork.Query<OrderDetail>()
                //.Include(x => x.Order)
                .ToListAsync(token);
        }

        public async Task<OrderDetail> GetById(int id, CancellationToken token)
        {
            throw new NotImplementedException();
            //return await this._unitOfWork.Query<Order>()
            //    .FirstOrDefaultAsync(x => x.OrderId == id, token);
        }
      
        public Task<OrderDetail> Create(OrderDetailDtoCreate orderDto, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetail> Update(int id, OrderDetailDtoUpdate dto, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    
        public Task Delete(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}