﻿using BattleCity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace BattleCity.View.UnitViews
{
	public class BaseObjectView : Image
	{
		public int ID { get; set; }

		#region DependencyProperty
		public TypeObject TypeUnit
		{
			get { return (TypeObject)GetValue(TypeUnitProperty); }
			set { SetValue(TypeUnitProperty, value); }
		}

		public static readonly DependencyProperty TypeUnitProperty =
			DependencyProperty.Register("TypeUnit", typeof(TypeObject), typeof(BaseObjectView));

		public int ZIndex
		{
			get { return (int)GetValue(ZIndexProperty); }
			set { SetValue(ZIndexProperty, value); }
		}

		public static readonly DependencyProperty ZIndexProperty =
			DependencyProperty.Register("ZIndex", typeof(int), typeof(BaseObjectView), new PropertyMetadata(0, ZIndexChangedCallback));

		private static void ZIndexChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BaseObjectView)d).SetValue(Canvas.ZIndexProperty, e.NewValue);
		}

		public double X
		{
			get { return (double)GetValue(XProperty); }
			set { SetValue(XProperty, value); }
		}

		public static readonly DependencyProperty XProperty =
			DependencyProperty.Register("X", typeof(double), typeof(BaseObjectView), new PropertyMetadata(0.0, xChangedCallback));

		private static void xChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BaseObjectView)d).SetValue(Canvas.LeftProperty, e.NewValue);
		}

		public double Y
		{
			get { return (double)GetValue(YProperty); }
			set { SetValue(YProperty, value); }
		}

		public static readonly DependencyProperty YProperty =
			DependencyProperty.Register("Y", typeof(double), typeof(BaseObjectView), new PropertyMetadata(0.0, yChangedCallback));

		private static void yChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((BaseObjectView)d).SetValue(Canvas.TopProperty, e.NewValue);
		}
        #endregion

        public virtual void Update(PropertiesType prop, object value)
		{
			switch (prop)
			{
				case PropertiesType.X:
					X = (int)value;
					break;
				case PropertiesType.Y:
					Y = (int)value;
					break;
			}
		}
	}
}
