using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Shared;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IInvoiceService {

        void Generate();

        void Generate(int year, int month);

        void Regenerate(long invoiceId, int year, int month);

        InvoiceListViewModel GetInvoiceList(GridPageViewModel model);
        InvoiceListViewModel GetInvoiceList(long? companyID, int year, int month);

        IEnumerable<InvoiceDetailViewModel> GetInvoiceDetails(long invoiceId);

        void Save(List<InvoiceViewModel> invoices);

        void Pay(long invoiceId);

        IEnumerable<InvoiceDetailExportModel> GetInvoiceViewDetail(long invoiceId);

    }// interface
}// namespace
