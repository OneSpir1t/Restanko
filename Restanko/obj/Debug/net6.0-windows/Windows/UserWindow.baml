   M S B A M L   `   `   `  ����C  ?Restanko, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Restanko.WindowsRestanko.Windows  5   	   !    Restanko.Windows.UserWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Restanko.Windows     5   	   -   5          � Title$  Restanko��5   	   $	��650��6   $
��1100��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �  5      �� 5      $
��auto �6#   �� 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� -   5         ��Name$ Search_TextBox��5      $	��200��6.   6:   �� 5      -   5        $��	FIO_Label��5   $   $��1��6   $��Right=�65   �� 5      -   5        $��RepairType_Button��5   <   $��2��6   $��Виды ремонта��6%   6U   �� 5       -   5        $��ReportPDF_Button��5    5   $��3��6   $��
Отчёт��6%   6M   �� 5!      -   5         ��Name$ Machine_Button��5!      $��3��6-   $��Станки��6=   6N   �� 5"      -   5        $��Exit_Button��5"   5   $��
Выйти��6   $��4��6%   6H   5#      � 5$   
   $��1��6    5%      �� 5&      �� 5'      �� 5(      �� 5)      .+�5+      �� -   5         ��Name$ Sort_Combobox��5+   9   $��0��6    �� SelectedIndex$ 0��6'   6P   �� 5,      -	   5        $��Filter_Combobox��5,      $ 0��6.   $��1��6@   6P   �� 5-      -
   5        $��CountDisplay_Label��5-   $   $��3��6   6$   5.      �� 5/   
   -   5         ��Name$ Repair_ListView��5/   ]   $��2��6    �� SelectionMode$ Single=�6    $��False��67   6�   �� 50   
   -   5        $��NotFound_Label��50      $��2��6&   $)��#Ничего не найдено...��63   $��Center=�6R   $��Center=�6t   $��48(�6�   � 51   
   $��3��6    52      �� 53      �� 54      �� 55      �� 56      .+�58      �� -   5        $��AddRepair_Button��58   (   $��Добавить��6   6@   59      � 5:   
   -   5        	 �Name$	 EditTypeRepair_Grid��5:      $��1��6,   $��0��6<   $��2��6F   .+�5;      �� $��Center=�6   $��Center=�64   .+�5<      �� $��Виды ремонта��6   �� 5=      -   5        $��TypeRepairComboboxEnable��5=   ,   
 d� 	IsChecked$

 TrueY�6   $��Включен��6L   $��5,0,0,0q�6^   �� 5>      -   5        $ TypeRapair_Combobox��5>   �    �� DisplayMemberPath$
 Name��6f   $ 0��6   $	��190��6�   {�6   ��  �� ElementName$ TypeRepairComboboxEnable�� �� Path$ 	IsChecked�6�   �� 5?      $(��"Наименование вида:��6   �� 5@      -   5        $ RepairTypeName_Textbox��5@      �� 5A      $��Стоимость��6   �� 5B      -   5        $ CostRepairType_Textbox��5B       �� 	MaxLength$ 12��6u   �� 5C      $7��1Продолжительность дн(/я/ей)��6   �� 5D      -   5        $  DurationRepairType_Textbox��5D      $��25��6>   $��Center=�6�   $��Center=�6�   $ 2��6�   �� 5E       �� Orientation$ 
Horizontal=�6   .+�5F      �� -   5        $ AddTypeRepair_Button��5F      $��Добавить��6;   6N   �� 5G      -   5        $ EditTypeRepair_Button��5G      $��Изменить��6<   6O   �� 5H      -   5        $ RemoveTypeRepair_Button��5H      $��Удалить��6>   $��Center=�6P   6m   5I      5J      �� 5K      -   5        $ GoBack_Button��5K      $��Bottom=�6,   $��Cкрыть панель��6G   $��Right=�6_   $��5,5,20,5q�6�   5L      5M      5N      