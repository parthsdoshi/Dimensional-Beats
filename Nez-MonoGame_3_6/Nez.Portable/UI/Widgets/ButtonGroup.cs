﻿using System;
using System.Collections.Generic;


namespace Nez.UI
{
	/// <summary>
	/// Manages a group of buttons to enforce a minimum and maximum number of checked buttons. This enables "radio button"
	/// functionality and more. A button may only be in one group at a time.
	/// 
	/// The {@link #canCheck(Button, boolean)} method can be overridden to control if a button check or uncheck is allowed.
	/// </summary>
	public class ButtonGroup
	{
		private List<Button> buttons = new List<Button>();
		private List<Button> checkedButtons = new List<Button>( 1 );
		private int minCheckCount, maxCheckCount = 1;
		private bool uncheckLast = true;
		private Button lastChecked;


		public ButtonGroup()
		{
			minCheckCount = 1;
		}


		public ButtonGroup( params Button[] buttons )
		{
			minCheckCount = 0;
			add( buttons );
			minCheckCount = 1;
		}


		public void add( Button button )
		{
			button._buttonGroup = null;
			var shouldCheck = button.isChecked || buttons.Count < minCheckCount;
			button.isChecked = false;
			button._buttonGroup = this;
			buttons.Add( button );
			button.isChecked = shouldCheck;
		}


		public void add( params Button[] buttons )
		{
			for( int i = 0, n = buttons.Length; i < n; i++ )
				add( buttons[i] );
		}


		public void remove( Button button )
		{
			button._buttonGroup = null;
			buttons.Remove( button );
			checkedButtons.Remove( button );
		}


		public void remove( params Button[] buttons )
		{
			for( int i = 0, n = buttons.Length; i < n; i++ )
				remove( buttons[i] );
		}


		public void clear()
		{
			buttons.Clear();
			checkedButtons.Clear();
		}


		/// <summary>
		/// Sets the first {@link TextButton} with the specified text to checked.
		/// </summary>
		/// <param name="text">Text.</param>
		public void setChecked( string text )
		{
			for( var i = 0; i < buttons.Count; i++ )
			{
				var button = buttons[i];
				if( button is TextButton && text == ((TextButton)button).getText() )
				{
					button.isChecked = true;
					return;
				}
			}
		}


		/// <summary>
		/// Called when a button is checked or unchecked. If overridden, generally changing button checked states should not be done
		/// from within this method.
		/// </summary>
		/// <returns>True if the new state should be allowed</returns>
		/// <param name="button">Button.</param>
		/// <param name="newState">New state.</param>
		public bool canCheck( Button button, bool newState )
		{
			if( button.isChecked == newState )
				return false;

			if( !newState )
			{
				// Keep button checked to enforce minCheckCount.
				if( checkedButtons.Count <= minCheckCount )
					return false;
				checkedButtons.Remove( button );
			}
			else
			{
				// Keep button unchecked to enforce maxCheckCount.
				if( maxCheckCount != -1 && checkedButtons.Count >= maxCheckCount )
				{
					if( uncheckLast )
					{
						int old = minCheckCount;
						minCheckCount = 0;
						lastChecked.isChecked = false;
						minCheckCount = old;
					}
					else
						return false;
				}
				checkedButtons.Add( button );
				lastChecked = button;
			}

			return true;
		}


		/// <summary>
		/// Sets all buttons' {@link Button#isChecked()} to false, regardless of {@link #setMinCheckCount(int)}.
		/// </summary>
		public void uncheckAll()
		{
			int old = minCheckCount;
			minCheckCount = 0;
			for( int i = 0, n = buttons.Count; i < n; i++ )
			{
				var button = buttons[i];
				button.isChecked = false;
			}
			minCheckCount = old;
		}


		/// <summary>
		/// The first checked button, or null.
		/// </summary>
		/// <returns>The checked.</returns>
		public Button getChecked()
		{
			if( checkedButtons.Count > 0 )
				return checkedButtons[0];
			return null;
		}


		/// <summary>
		/// The first checked button index, or -1
		/// </summary>
		/// <returns>The checked index.</returns>
		public int getCheckedIndex()
		{
			if( checkedButtons.Count > 0 )
				return buttons.IndexOf( checkedButtons[0] );
			return -1;
		}


		public List<Button> getAllChecked()
		{
			return checkedButtons;
		}


		public List<Button> getButtons()
		{
			return buttons;
		}


		/// <summary>
		/// Sets the minimum number of buttons that must be checked. Default is 1.
		/// </summary>
		/// <param name="minCheckCount">Minimum check count.</param>
		public void setMinCheckCount( int minCheckCount )
		{
			this.minCheckCount = minCheckCount;
		}


		/// <summary>
		/// Sets the maximum number of buttons that can be checked. Set to -1 for no maximum. Default is 1.
		/// </summary>
		/// <param name="maxCheckCount">Max check count.</param>
		public void setMaxCheckCount( int maxCheckCount )
		{
			if( maxCheckCount == 0 )
				maxCheckCount = -1;
			this.maxCheckCount = maxCheckCount;
		}


		/// <summary>
		/// If true, when the maximum number of buttons are checked and an additional button is checked, the last button to be checked
		/// is unchecked so that the maximum is not exceeded. If false, additional buttons beyond the maximum are not allowed to be
		/// checked. Default is true.
		/// </summary>
		/// <param name="uncheckLast">Uncheck last.</param>
		public void setUncheckLast( bool uncheckLast )
		{
			this.uncheckLast = uncheckLast;
		}

	}
}

