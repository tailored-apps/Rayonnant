using System;
using System.Runtime.InteropServices;
using System.Transactions;
using Microsoft.Win32.SafeHandles;

namespace TailoredApps.Rayonnant.Commons.TxF
{
    [Guid("79427A2B-F895-40e0-BE79-B57DC82ED231"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKernelTransaction
    {
        int GetHandle(out IntPtr pHandle);
    }

    [System.Security.SuppressUnmanagedCodeSecurity]
    public class KtmTransactionHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal KtmTransactionHandle(IntPtr handle)
            : base(true)
        {
            this.handle = handle;
        }

        public static KtmTransactionHandle CreateKtmTransactionHandle()
        {
            if (Transaction.Current == null)
            {
                throw new InvalidOperationException("Cannot create a KTM handle without Transaction.Current");
            }

            return CreateKtmTransactionHandle(Transaction.Current);
        }

        public static KtmTransactionHandle CreateKtmTransactionHandle(Transaction managedTransaction)
        {
            var dtcTransaction = TransactionInterop.GetDtcTransaction(managedTransaction);
            var ktmInterface = dtcTransaction as IKernelTransaction;

            if (ktmInterface != null)
            {

                IntPtr ktmTxHandle;
                int hr = ktmInterface.GetHandle(out ktmTxHandle);
                HandleError(hr);
                return new KtmTransactionHandle(ktmTxHandle);

            }
            return null;
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.CloseHandle(handle);
        }

        private static void HandleError(int error)
        {
            if (error != NativeMethods.ErrorSuccess)
            {
                throw new System.ComponentModel.Win32Exception(error);
            }
        }
    }
}
