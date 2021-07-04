using DuelUI.Common;
using DuelUI.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DuelUI.Modals
{
    public class BaseModalView: UserControl
    {
        public BaseModalView()
        {
            this.Loaded += BaseModalView_Loaded;
        }

        private void BaseModalView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Loaded -= BaseModalView_Loaded;
            this.LoadView();
            this.AttachReturnEvent();
        }

        #region Close Modal View

        private const string Part_IconReturn = "MenuReturn";
        private const string Part_BdrRoot = "BdrRoot";

        private void AttachReturnEvent()
        {
            object obj = this.FindName(Part_IconReturn);
            if (obj != null)
            {
                if (obj is IconMenu)
                {
                    IconMenu returnMenu = obj as IconMenu;
                    returnMenu.MouseLeftButtonUp += ReturnMenu_MouseLeftButtonUp;
                }
            }
        }

        private void ReturnMenu_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IconMenu returnMenu = sender as IconMenu;
            returnMenu.MouseLeftButtonUp -= ReturnMenu_MouseLeftButtonUp;
            this.DoExecuteReturn();
            e.Handled = true;
        }

        public event DelegateMethod<BaseModalView> OnReturn;

        public virtual void DoFree()
        {
        }

        public virtual void DoExecuteReturn()
        {
            this.IsEnabled = false;

            object obj = this.FindName(Part_BdrRoot);
            if (obj != null)
            {
                if (obj is Border)
                {
                    Border bdrRoot = obj as Border;
                    bdrRoot.Background = Brushes.Transparent;
                }
            }

            obj = this.FindName(Part_BdrContainer);
            if (obj != null)
            {
                if (obj is Border)
                {
                    Border bdrContainer = obj as Border;
                    bdrContainer.Opacity = 0;
                    bdrContainer.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

                    TransformGroup transform = new TransformGroup();
                    ScaleTransform scaleTrans = new ScaleTransform();
                    scaleTrans.ScaleX = 0.4;
                    scaleTrans.ScaleY = 0.4;
                    transform.Children.Add(scaleTrans);
                    bdrContainer.RenderTransform = transform;
                    this.UpdateLayout();

                    DoubleAnimation anim = new DoubleAnimation(1.5, TimeSpan.FromSeconds(0.2));
                    anim.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut };
                    scaleTrans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
                    scaleTrans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
                   
                    anim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.2));
                    anim.EasingFunction = new ExponentialEase() { EasingMode = EasingMode.EaseOut };
                    anim.Completed += Anim_Completed;
                    bdrContainer.BeginAnimation(OpacityProperty, anim);
                }
            }
        }

        private void Anim_Completed(object sender, EventArgs e)
        {
            if (this.OnReturn != null)
                this.OnReturn(this);

            this.DoFree();
        }

        #endregion


        #region Load View Animation

        private const string Part_BdrContainer = "BdrContainer";

        private void LoadView()
        {
            object obj = this.FindName(Part_BdrContainer);
            if (obj != null)
            {
                if (obj is Border)
                {
                    Border bdrContainer = obj as Border;
                    bdrContainer.Opacity = 0;
                    bdrContainer.RenderTransformOrigin = new System.Windows.Point(0.5,0.5);

                    TransformGroup transform = new TransformGroup();
                    ScaleTransform scaleTrans = new ScaleTransform();
                    scaleTrans.ScaleX = 0.4;
                    scaleTrans.ScaleY = 0.4;
                    transform.Children.Add(scaleTrans);
                    bdrContainer.RenderTransform = transform;
                    this.UpdateLayout();

                    DoubleAnimation anim = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));
                    anim.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut };
                    scaleTrans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
                    scaleTrans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
                    bdrContainer.BeginAnimation(OpacityProperty, anim);
                }
            }
        }

        #endregion
    }
}
