   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  $    Biblioteka.Views.AddReaderPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels     -     �Name$  AddReaderWindow��   
WindowIcon � Icon# [   � Title$* $Добавяне на читател��$	��357��$	��520�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext .   (Biblioteka.ViewModels.AddReaderViewModel  .��� $��
5, 5, 5, 5q� � RowDefinitions �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��300 ��� $	��200 �.+��� $��0��$��0��$&�� Данни за читателя��.���  �� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 � �� $��65 ��� .+��� $��ЕГН:�� �� TextAlignment$ Right=�$��Center=����#000000$��0��$��0���� $��0��$��1�� �� Orientation$ 
Horizontal=�$��5,0,0,0q�.+��� 	 �� TextAlignment$
	 Left=�$	��133������ *EGN+
 �� UpdateSourceTrigger$
 PropertyChanged=��� ���� *SearchStudentCommand+���� $ 
Horizontal=�.+���   SearchImage#���  �� $��
Търси���� $��Име:��$ Right=�$��Center=����#000000$��1��$��0���� $
	 Left=�$	��200��$��1��$��1������ *Name+$
 PropertyChanged=��� $��Адрес:��$ Right=�$��Center=����#000000$��2��$��0���� $
	 Left=�$	��200��$��2��$��1������ *	Address+$
 PropertyChanged=��� $��Номер:��$ Right=�$��Center=����#000000$��3��$��0���� $
	 Left=�$	��200��$��3��$��1������ *SerialNumber+$
 PropertyChanged=��� $��Длъжност:��$ Right=�$��Center=����#000000$��4��$��0���� $��4��$��1��$ 
Horizontal=�$��5,0,0,0q�.+��� $��80��$
��Left=�$��Center=� �� SelectedIndex �� *SelectedPosition+ �� Mode$ TwoWay=�.@��� .��Ученик�� .��Учител�� $��50��$
��Left=�$��Center=� �� *SelectedClass+$ TwoWay=�y��� *ShowClassChoice+.@��� .��1�� .��2�� .��3�� .��4�� .��5�� .��6�� .��7�� .��8�� .��9�� .��10�� .��11�� .��12�� $��40��$
��Left=�$��Center=� �� *SelectedParalelka+$ TwoWay=����� *	Paralelki+y��� *ShowClassChoice+�� $��	Дата:��$ Right=�$��Center=����#000000$��5��$��0���� $
	 Left=�$	��200��$��5��$��1��{�.  ���� *DateOfCreation+ �� StringFormat$ 
dd/MM/yyyy���� $��0��$��1��$��Center=�$$��Дата на добавяне��.��&   System.Windows.Controls.Calendar  $��Center=�   SelectedDate �� *DateOfCreation+$ TwoWay=��� $��1��$��0��$��2��$��Действия��$
��Auto��$��0,0,0,0q�.���� $ 
Horizontal=�$��Right=�.+��� ���� *	AddReader+����  �� ElementName$ AddReaderWindow������ $ 
Horizontal=�.+���   AddImage#���  �� $!��Добави читател���� ���� *CloseWindow+���� $ AddReaderWindow������ $ 
Horizontal=�.+���   RemoveImage#���  �� $��Откажи��