   M S B A M L   `   `   `  ����  
okno vvod2!    FlightCalculator.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$>  8Калькулятор параметров полета��5   	   $	��350��6/   $	��400��6<   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $2��,Начальная скорость (м/с):��6!   $��0,0,0,5q�6B   �� 5   
   -   5         ��Name$ InitialVelocityTextBox��5      $��1��6   $��0,0,0,10q�6?   �� 5   
   $��2��6   $/��)Угол запуска (градусы):��6!   $��0,0,0,5q�6@   �� 5   
   -   5        $ LaunchAngleTextBox��5      $��3��6   $��0,0,0,10q�6;   �� 5   
   -   5        $��4��5      $��Рассчитать��6   $��0,0,0,10q�6Q   $
��10,5q�6c   �� 5   
   $��5��6   $��Результаты:��6!   $
��Bold"�64   $��0,10,0,5q�6F   �� 5   
   $��6��6    �� Orientation$ Vertical=�6"   .+�5      �� -   5         ��Name$ TimeOfFlightText��5      $��0,0,0,5q�62   �� 5      -   5        $ 	RangeText��5      $��0,0,0,5q�6+   �� 5      -   5        $ MaxHeightText��5      $��0,0,0,5q�6/   5      5       5!      