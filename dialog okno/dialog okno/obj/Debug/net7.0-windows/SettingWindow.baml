   M S B A M L   `   `   `  ����  dialog okno'    !ParabolaTrajectory.SettingsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$/  )Настройки отображения��5   	   $	��300��6'   $	��400��64    � WindowStartupLocation$ CenterOwner=�5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   .+�5      �� $$��Цвет траектории:��6   �� 5      -   5         ��Name$ 	cmbColors��5       �� SelectedIndex$ 0��6*   .@�5      �� .��6   
Синий6&   �� 5      .��6   Красный6(   �� 5      .��6   Зеленый6(   �� 5      .��6   Черный6'   5      5      �� 5   
   $��1��6   .+�5      �� $ ��Толщина линии:��6   �� 5      -   5         ��Name$ sliderThickness��5       � Minimum$ 1Q�6.    � Maximum$ 5Q�6:    � Value$ 2Q�6F   5      �� 5   
   $��2��6   .+�5      �� -   5        	 ��Name$	 chkShowMarkers��5      $)��#Показывать маркеры��6/   
 d� 	IsChecked$

 TrueY�6L   �� 5       -   5        $	 chkShowAxes��5       $!��Показывать оси��6,   $

 TrueY�6E   5!      �� 5#   
   $��4��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5$      �� -   5        $��OK��5$      $��80��6"   $��0,0,10,0q�6-   6?   �� 5%      -   5        $��Отмена��5%      $��80��6&   61   5&      5'      5(      