   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  &     Biblioteka.Views.BookDetailsPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels        
WindowIcon  � Icon#  [   � Title$  Данни за книга��$	��631��$	��610�����#D6FFFF � 
ResizeMode$ NoResize=�.��� $��0,0,0,0q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��600 �.+��� $��0��$��0��$ ��Данни за книга��$��Center=�$	��Top=�$	��590��.���  �� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 � �� $��1* ��� $��6* �.+��� $��0��$��0�� �� TextAlignment$ Right=�$��Center=����#000000Номер:�� $��0��$��1��$
��Left=�$	��480������ *SerialNumber+ �� UpdateSourceTrigger$ PropertyChanged=��� $��1��$��0��$ Right=�$��Center=����#000000Автор:�� $��1��$��1��$
��Left=�$	��480������ *Author+$ PropertyChanged=��� $��2��$��0��$ Right=�$��Center=����#000000Заглавие:�� $��2��$��1��$
��Left=�$	��480������ *Title+$ PropertyChanged=��� $��3��$��0��$ Right=�$��Center=����#000000	Жанр:�� $��3��$��1��$	��220��$
��Left=�$��Center=� �� SelectedIndex �� *SelectedGenre+ �� Mode$ TwoWay=����� *Genres+�� $��Статус:��$ Right=�$��Center=����#000000$��4��$��0���� 	 �� Orientation$	 
Horizontal=�$��4��$��1��.+��� $
��Left=�$	��345��{�.  ���� *
BookStatus+�� $��StatusButton��y��� *ShowStatusButton+���� *ShowReaderDetailsCommand+���� $	 
Horizontal=�.+���   InfoUserImage#���  �� $&�� Данни за читателя���� $��5��$��0��$ Right=�$��Center=����#000000Година:�� $��5��$��1��$
��Left=�$	��480������ *PublishedYear+$ PropertyChanged=��� $��6��$��0��$ Right=�$��Center=����#000000	Цена:�� $��6��$��1��$
��Left=�$	��480������ *Price+$ PropertyChanged=��� $	 
Horizontal=�$��Right=�$��7��$��0��$��2��.+��� {��� *EnableSaveChangesButton+���� *SaveChangesCommand+���� $	 
Horizontal=�.+���   SaveChangesImage#���  �� $%��Запази промените���� ���� *ArchiveBookCommand+y��� *ShowArchiveButton+���� $	 
Horizontal=�.+���   ArchiveImage#���  �� $'��!Архивирай книгата���� ���� *UnarchiveBookCommand+y��� *ShowUnarchiveButton+���� $	 
Horizontal=�.+���   AddImage#���  �� $6��0Възстановяване на книгата���� $	��270��$	��590��$^��XСписък с всички читатели, които са взели книгата��$	��Top=�$��Center=�$��1��$��0��$��5,5,5,5q�.���  �� $	��200 ��� $��40 � �� $��1* �.+��� $��0��$��0������ *BookReaders+ �� *SelectedBookReader+���� 
 �� 
TargetType"
 ��.���  �� Property � �� �� Value$
 Left=� �� View �� .P��� $��ЕГН�� �� Width$	 120�� �� DisplayMemberBinding �� *	ReaderEGN+�� $��Име��$	 200�� �� *
ReaderName+�� $��Клас��$ 50�� �� *ClassAndParalelka+�� $��Взета на��$	 100�� �� *DateOfTaking+ �� StringFormat$ 
dd/MM/yyyy���� $��Върната на��$	 100�� �� *DateOfReturn+$ 
dd/MM/yyyy���� $	 
Horizontal=�$��Right=�$��1��$��0��$��2��.+��� $��
ListButton������ *ShowReaderDetailsCommand+���� $	 
Horizontal=�.+��� #���  �� $&�� Данни за читателя���� ���� *RefreshDataCommand+���� $	 
Horizontal=�.+���   RefreshIcon#���  �� $��Обнови��