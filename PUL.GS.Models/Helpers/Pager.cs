using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models.Helpers
{
    public class Pager<T> where T : class
    {
        public int ActualPage { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Records { get; set; }

        public Pager(IEnumerable<T> records, int totalRecords, int actualPage,
            int recordsPerPage)
        {
            Records = records;
            TotalRecords = totalRecords;
            ActualPage = actualPage;
            RecordsPerPage = recordsPerPage;
        }

        public int TotalPages
        {
            get
            {

                return (int)Math.Ceiling(TotalRecords / (double)RecordsPerPage);
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (ActualPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (ActualPage < TotalPages);
            }
        }

    }
}
