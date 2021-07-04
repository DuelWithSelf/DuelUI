using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuelUI.Common
{
    public delegate void DelegateMethod<T>(T sender);

    public delegate void DelegateMethod<T, T1>(T sender, T1 args);

    public delegate void DelegateMethod<T, T1, T2>(T sender, T1 args, T2 obj);

    public class AppEvents
    {
        public static event Action OnBeforeShutDown;

        public static void ExecuteBeforeShutDown()
        {
            if (OnBeforeShutDown != null)
                OnBeforeShutDown();
        }
    }
}
