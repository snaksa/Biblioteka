   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  #    Biblioteka.Views.TakeBookPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels     -     �Name$  TakeBookWindow��   
WindowIcon � Icon# [   � Title$$ Вземане на книга��$	��245��$	��495�����#D6FFFF � 
ResizeMode$ NoResize=�.���  � RowDefinitions ��  � ColumnDefinitions �� .+��� $��0��$��0��$$��Вземане на книга��$��Center=�$	��Top=�$	��475��.���  �� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 � �� $��1* ��� $��6* �.+��� $��0��$��0�� �� TextAlignment$ Right=�$��Center=����#000000	ЕГН:��  �� Orientation$ 
Horizontal=�$��0��$��1��.+��� $
��Left=�$	��100������ *	ReaderEGN+�� ���� *SearchReaderCommand+���� $ 
Horizontal=�.+���   SearchImage#���  �� $��Избери���� $
 Left=�$��Center=�$	��200�����#000000���� *
ReaderName+�� $��1��$��0��$ Right=�$��Center=����#000000Книга:�� $ 
Horizontal=�$��1��$��1��.+��� $
��Left=�$	��100������ *BookSerialNumber+�� ���� *SearchBookCommand+���� $ 
Horizontal=�.+��� #���  �� $��Избери���� $
 Left=�$��Center=�$	��200�����#000000���� *	BookTitle+�� $��2��$��0��$ Right=�$��Center=����#000000Взета на:(  "System.Windows.Controls.DatePicker  $��2��$��1��$	��390��$��27��$��Center=�$
��Left=�$��5,5,5,5q�
������ ���#CCEBFF   SelectedDate �� *DateOfTaking+�� $��3��$��0��$ Right=�$��Center=����#000000Краен срок:  $��3��$��1��$	��390��$��27��$��Center=�$
��Left=�$��5,5,5,5q�
������ ���#CCEBFF �� *
Deadline+�� $ 
Horizontal=�$��Right=�$��5��$��0��$��2��.+��� ���� *SaveCommand+���� 	 �� ElementName$	 TakeBookWindow������ $ 
Horizontal=�.+���   SaveChangesImage#���  �� $��Запази���� ���� *CloseWindowCommand+���� $	 TakeBookWindow������ $ 
Horizontal=�.+���   RemoveUserImage#���  �� $��Откажи��