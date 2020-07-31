using PUL.GS.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.App.Infrastructure
{
    public class BookData
    {
        private readonly AppSettings settings;

        public BookData(AppSettings configuration)
        {
            settings = configuration;
        }
    }
}
