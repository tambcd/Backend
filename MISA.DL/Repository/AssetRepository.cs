﻿using Microsoft.Extensions.Configuration;
using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Repository
{
    public class AssetRepository : BaseRepository<fixed_asset>
    {
        public AssetRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
