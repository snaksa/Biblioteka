   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  %    Biblioteka.Views.SearchBookPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels     -     �Name$  SearchBookWindow��   
WindowIcon � Icon# [   � Title$$ Търсене на книга��$	��556��$	��477�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext /   )Biblioteka.ViewModels.SearchBookViewModel  .��� $��0,0,0,0q� � RowDefinitions �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��470 �.+��� $��0��$��0��$��Търсене��.���  �� $��35 ��� $��35 � �� $��70 ��� $	��280 ��� $	��110 �.+��� $��0��$��0�� �� TextAlignment$ Right=�$��Center=����#000000Търсене по:�� $��0��$��1��$��2�� �� Orientation$ 
Horizontal=�.+��� $	��100��$
��Left=�$��Center=�	 �� SelectedIndex	 �� *SearchBookOption+
 �� Mode$
 TwoWay=�.@��� .��
Номер�� .��
Автор�� .��Заглавие�� .��
Жанр�� $	��267��$
��Left=�$��Center=�	 �� *SelectedGenre+$
 TwoWay=����� *Genres+y��� *GenresVisibility+�� $��1��$��0��$ Right=�$��Center=����#000000	Ключ:�� $��1��$��1��$
��Left=�$	��270������ *SearchValue+$
 TwoWay=�{��� *SearchValueEnabled+ �� UpdateSourceTrigger$ PropertyChanged=��� $��1��$��2��$��
Търси��$	��100������ *SearchCommand+�� $��1��$��0��$��5,5,5,5q�	 �� *SelectedTabItem+.@��� $!��Актуални книги��.���� $1��+Списък с актуални книги��$	��360��$	��440��$	��Top=�$��Center=�.���� .+��� $	��300������ *
FoundBooks+	 �� *SelectedBook+$ PropertyChanged=� �� View �� .P��� $��
Номер�� �� Width$ 60�� �� DisplayMemberBinding �� *SerialNumber+�� $��Заглавие��$	 130�� �� *Title+�� $��
Автор��$	 120�� �� *Author+�� $��Статус��$ 85�� �� *Status+�� $ 
Horizontal=�$��Right=�.+��� ���� *ChooseBookCommand+����  �� ElementName$ SearchBookWindow��y��� *ShowChooseButton+���� $ 
Horizontal=�.+���   ChooseImage#���  �� $��Избери книга���� ���� *BookDetailsCommand+���� $ 
Horizontal=�.+���   InfoUserImage#���  �� $ ��Данни за книга���� ���� *RefreshDataCommand+���� $ 
Horizontal=�.+���   RefreshIcon#���  �� $��Обнови���� $%��Архивирани книги��.���� $5��/Списък с архивирани книги��$	��360��$	��440��$	��Top=�$��Center=�.���� .+��� $	��300������ *ArchivedBooks+	 �� *SelectedArchivedBook+$ PropertyChanged=� �� .P��� $��
Номер��$ 60�� �� *SerialNumber+�� $��Заглавие��$	 130�� �� *Title+�� $��
Автор��$	 120�� �� *Author+�� $��Статус��$ 85�� �� *Status+�� $ 
Horizontal=�$��Right=�.+��� ���� *BookDetailsCommand+���� $ 
Horizontal=�.+��� #���  �� $ ��Данни за книга���� ���� *RefreshDataCommand+���� $ 
Horizontal=�.+��� #���  �� $��Обнови��