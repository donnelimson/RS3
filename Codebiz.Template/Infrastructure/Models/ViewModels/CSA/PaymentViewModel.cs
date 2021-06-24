using Codebiz.Domain.Common.Model.DTOs.Banking;
using System;
using System.Collections.Generic;

namespace Infrastructure.Models.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public int AccountId { get; set; }
        public int PayeeId { get; set; }
        public decimal Vat { get; set; }
        public string Transaction { get; set; }
        public decimal AmountDue { get; set; }
    }

    public class PaymentEntryViewModel
    {
        public int? AccountId { get; set; }
        public string AccountNo { get; set; }
        public string PayeeName { get; set; }
        public int PayerId { get; set; }
        public DateTime OrDate { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCash { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CheckAmount { get; set; }
        public string TIN { get; set; }
        public string Remarks { get; set; }
        public List<CheckPaymentViewModel> CheckPayment { get; set; } = new List<CheckPaymentViewModel>();
        public decimal PaymentOnAccount { get; set; }
        public decimal Change { get; set; }
        public decimal AmountDue { get; set; }
        public string CounterRef { get; set; }
        public string NumAtCard { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string TransactionType { get; set; }
        public List<decimal> CashDiscountPercentages { get; set; }
        public List<decimal> CashDiscountAmounts { get; set; }
        public decimal? TotalCashDiscount { get; set; }
        public long? IncomingPaymentId { get; set; }
        public BankTransferPaymentViewModel BankTransfer { get; set; } = new BankTransferPaymentViewModel();
    }
    public class BankTransferPaymentViewModel
    {
        public string GLAccount { get; set; }
        public DateTime? TransferDate { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
    }
    public class CheckPaymentViewModel
    {
        public int CheckNo { get; set; }
        public string AccountNo { get; set; }
        public DateTime DueDate { get; set; }
        public BankForChequeDTO Bank { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; } = 0;
    }

   public class CollectionDenominationViewModel
    {
        public int Id { get; set; }
        public decimal Cash { get; set; }
        public int NoOfPieces { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
