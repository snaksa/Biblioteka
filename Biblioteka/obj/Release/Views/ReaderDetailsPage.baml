   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  (    "Biblioteka.Views.ReaderDetailsPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels     -     �Name$  ReaderDetailsWindow��   
WindowIcon � Icon# [   � Title$&  Данни за читателя��$	��642��$	��610�����#D6FFFF � 
ResizeMode$ NoResize=�.��� $��0,0,0,0q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��600 �.+��� $��0��$��0��$&�� Данни за читателя��$��Center=�$	��Top=�$	��590��.���  �� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 � �� $��1* ��� $��6* �.+��� $��0��$��0�� �� TextAlignment$ Right=�$��Center=����#000000	ЕГН:�� $��0��$��1��$
��Left=�$	��480������ *EGN+ �� UpdateSourceTrigger$ PropertyChanged=��� $��1��$��0��$ Right=�$��Center=����#000000	Име:�� $��1��$��1��$
��Left=�$	��480������ *Name+$ PropertyChanged=��� $��2��$��0��$ Right=�$��Center=����#000000Адрес:�� $��2��$��1��$
��Left=�$	��480������ *	Address+$ PropertyChanged=��� $��3��$��0��$ Right=�$��Center=����#000000Номер:�� $��3��$��1��$
��Left=�$	��480������ *SerialNumber+$ PropertyChanged=��� $��Длъжност:��$ Right=�$��Center=����#000000$��4��$��0���� $��4��$��1�� �� Orientation$ 
Horizontal=�$��1,0,0,0q�.+��� $	��100��$
��Left=�$��Center=�	 �� SelectedIndex	 �� *SelectedPosition+
 �� Mode$
 TwoWay=�.@��� .��Ученик�� .��Учител�� $	��100��$
��Left=�$��Center=�	 �� *SelectedClass+$
 TwoWay=�y��� *ShowClassChoice+.@��� .��1�� .��2�� .��3�� .��4�� .��5�� .��6�� .��7�� .��8�� .��9�� .��10�� .��11�� .��12�� $��92��$
��Left=�$��Center=�	 �� *SelectedParalelka+$
 TwoWay=����� *ParalelkiOptions+y��� *ShowClassChoice+�� $��5��$��0��$ Right=�$��Center=����#000000Добавен на:�� $��5��$��1��$
��Left=�$	��480��{�.  ���� *DateOfCreation+ �� StringFormat$ 
dd/MM/yyyy���� $ 
Horizontal=�$��Right=�$��6��$��0��$��2��.+��� {��� *EnableSaveChangesButton+���� *SaveChangesCommand+���� $ 
Horizontal=�.+���   SaveChangesImage#���  �� $%��Запази промените���� ���� *RemoveReaderCommand+����  �� ElementName$ ReaderDetailsWindow������ $ 
Horizontal=�.+���   RemoveUserImage#���  �� $%��Премахни читател���� $��1��$��0��$��5,5,5,5q�.@��� $��Взети книги��.���� $8��2Списък с всички взети книги��$	��270��$	��570��$	��Top=�$��Center=�.���  �� $	��200 ��� $��40 � �� $��1* �.+��� $��0��$��0������ *
TakenBooks+	 �� *SelectedTakenBook+$ PropertyChanged=�����  �� 
TargetType" ��.���  �� Property � �� �� Value$
 Left=� �� View �� .P��� $��Заглавие�� �� Width$	 150�� �� DisplayMemberBinding �� *Title+�� $��
Автор��$	 140�� �� *Author+�� $��Взета на��$ 80�� �� *DateOfTaking+$ 
dd/MM/yyyy���� $��Краен срок��$ 80�� �� *
Deadline+$ 
dd/MM/yyyy���� $��Срок��$ 75�� �� *DaysToDeadlineString+�� $ 
Horizontal=�$��Right=�$��1��$��0��$��2��.+��� ���� *TakeBookCommand+���� $ 
Horizontal=�.+���   AddImage#���  �� $-��'Вземане на нова книга���� ���� *ReturnBookCommand+���� $ 
Horizontal=�.+���   GiveBackImage#���  �� $$��Връщане на книга���� $��Taken������ *BookDetailsCommand+���� $ 
Horizontal=�.+���   InfoBookImage#���  �� $ ��Данни за книга���� $��Taken������ *RefreshDataCommand+���� $ 
Horizontal=�.+���   RefreshIcon#���  �� $��Обнови���� $��Върнати книги��.���� $<��6Списък с всички върнати книги��$	��570��$	��270��$	��Top=�$��Center=�.���  �� $	��200 ��� $��40 � �� $��1* �.+��� $��0��$��0������ *ReturnedBooks+	 �� *SelectedReturnedBook+$ PropertyChanged=����� " ��.���  � ��$
 Left=� �� .P��� $��Заглавие��$	 150�� �� *Title+�� $��
Автор��$	 140�� �� *Author+�� $��Взета на��$ 80�� �� *DateOfTaking+$ 
dd/MM/yyyy���� $��Върната на��$ 80�� �� *DateOfReturn+$ 
dd/MM/yyyy���� $��Навреме��$ 75�� �� *ReturnedOnTime+�� $ 
Horizontal=�$��Right=�$��1��$��0��$��2��.+��� $��Returned������ *BookDetailsCommand+���� $ 
Horizontal=�.+��� #���  �� $ ��Данни за книга���� $��Taken������ *RefreshDataCommand+���� $ 
Horizontal=�.+��� #���  �� $��Обнови��