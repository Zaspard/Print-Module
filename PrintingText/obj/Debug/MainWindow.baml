   M S B A M L   `   `   `  ����G  CPrintingText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PrintingTextPrintingText  5   	   ?$clr-namespace:PrintingText.ViewModelPrintingText.ViewModel  5   	       PrintingText.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PrintingText     5   	   :	viewmodel$clr-namespace:PrintingText.ViewModel     5   	     � Title$  
MainWindow��5	   	   $��674.5��6   $��1139.5��6+    � 	Resources 5
       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��$ 25��6'   �� 5       � ��$ 80��6&   �� 5       � ��$ 	25 5 25 5q�6'   5      5      .��5      � ���	#FFD6D4D4 � ColumnDefinitions 5   
   �� 5      $
��335* �6   �� 5      $
��899* �6   .+�5   
   � $��Stretch=�6   $��Stretch=�6-   
������� � RowDefinitions 5      �� 5      $	��21* �6    �� 5      $
��622* �6    .+�5      � $��0��6   .+�5      �� .@�5      �� $��Вкладка 1��6   .��61   Шаблоны6:   �� 5      $��Вкладка 2��6   .��61   1/Настройка бумаги и печати6L   5       5!      � 5"      $��1��6    5#      �� 5$      $
��167* �6$   �� 5%      $
��180* �6$   �� 5&      $
��208* �6$   �� 5'      $	��67* �6$   .+�5)      � $��	0,0,0,144q�6   $��2��6+   .+�5*      � $
��Left=�68   $	��135��6S   $��	53,28,0,0q�6`   $	��Top=�6s   $	��231��6�    � DataContext 6   �� *Page+.+�5+      �� $
��Left=�68   $��22��6S   $��84,6,0,0q�6_    �� TextWrapping$
 Wrap=�6q   $	��Top=�6�   $	��146��6�   
���������6"   �� *Left+6�   �� 5,      $
��Left��6    $
��Left=�6/   $��11,5,0,0q�6J   $	��Top=�6\   �� 5-      $
��Left=�67   $��22��6R   $��	85,39,0,0q�6^   $
 Wrap=�6q   $	��Top=�6�   $	��146��6�   
���������6"   �� *Top+6�   �� 5.      $	��Top��6    $
��Left=�6.   $��	11,37,0,0q�6I   $	��Top=�6\   �� 5/      $
��Left=�69   $��22��6T   $��	84,68,0,0q�6`   $
 Wrap=�6s   $	��Top=�6�   $	��146��6�   
���������6"   �� *Right+6�   �� 50      $��Right��6    $
��Left=�60   $��	11,67,0,0q�6K   $	��Top=�6^   �� 51      $
��Left=�6:   $��22��6U   $��
84,104,0,0q�6a   $
 Wrap=�6u   $	��Top=�6�   $	��146��6�   
���������6"   �� *Bottom+6�   �� 52      $��Bottom��6    $
��Left=�61   $��
11,101,0,0q�6L   $	��Top=�6`   53      54      �� 55      $
��Left=�6k   $��	43,15,0,0q�6�   $	��Top=�6�   $	��220��6�   $��2��6�   $��22��6�   ��6   �� *NamePrinters+	 �� SelectedItem	 6@   �� *NameSelectPrinter+6�   � 56      $
��Left=�6=   $	��174��6X   $��	21,14,0,0q�6e   $	��Top=�6x   $	��268��6�   $��2��6�    6   �� *SelectPrinter+.+�57      �� $
��Left=�6m   $��	21,29,0,0q�6�   $	��Top=�6�   $	��220��6�   ��6   �� *PageMediaSizes+	 6F   �� *PageMediaSize+6�   �� 58      $
��Left=�6q   $��	21,56,0,0q�6�   $	��Top=�6�   $	��220��6�   ��6   �� *PageOrientations+	 6H   �� *PageOrientation+6�   �� 59      $
��Left=�6i   $��	21,84,0,0q�6�   $	��Top=�6�   $	��220��6�   ��6   �� *OutputColors+	 6D   �� *OutputColor+6�   �� 5:      $
��Left=�6c   $��
21,111,0,0q�6~   $	��Top=�6�   $	��220��6�   ��6   �� *	InputBins+	 6A   �� *
InputBin+6�   �� 5;      $
��Left=�6^   $��22��6y   $��
93,145,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��146��6�   
���������6   �� *	CopyCount+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5<      $��
Count copy��6   $
��Left=�61   $��
19,139,0,0q�6L   $	��Top=�6`   5=      � 5>      $��3��6    5?      �� 5@      $
��167* �6+   �� 5A      $
��168* �6+   .+�5C      �� $��Print��67   $��Stretch=�6G   $��0��6e   ��6   �� *Print+6e   �� 5D      $��Button��6=   $��Stretch=�6N   $��1��6l   ��6   �� *PrintDialog+6l   5E      5F      5G      5H      5I      