using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartTrade.Entities;

namespace SmartTrade.Persistencia.Services
{
    internal class STService
    {
        private readonly IDAL dal;

        public STService(IDAL dal)
        {
            this.dal = dal;
        }

        public void Commit()
        {
            dal.Commit();
        }

    }
}
