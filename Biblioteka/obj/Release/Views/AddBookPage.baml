   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  "    Biblioteka.Views.AddBookPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels     -     �Name$  AddBookWindow��   
WindowIcon � Icon# [   � Title$&  Добавяне на книга��$	��320��$	��310�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext ,   &Biblioteka.ViewModels.AddBookViewModel  .��� $��1,1,1,1q� � RowDefinitions �� $
��Auto � � ColumnDefinitions �� $	��300 �.+��� $��0��$��0��$$��Данни за книгата��$
��Auto��.���  �� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 � �� $��50 ��� $	��230 �.+��� $��Номер:�� �� TextAlignment$ Right=�$��Center=����#000000$��0��$��0����  �� TextAlignment$
 Left=�$	��220��$��0��$��1������ *SerialNumber+	 �� UpdateSourceTrigger$	 PropertyChanged=��� $��Автор:��$ Right=�$��Center=����#000000$��1��$��0���� $
 Left=�$	��220��$��1��$��1������ *Author+$	 PropertyChanged=��� $��Заглавие:��$ Right=�$��Center=����#000000$��2��$��0���� $
 Left=�$	��220��$��2��$��1������ *Title+$	 PropertyChanged=��� $��	Цена:��$ Right=�$��Center=����#000000$��3��$��0���� $
 Left=�$	��220��$��3��$��1������ *Price+$	 PropertyChanged=��� $��	Жанр:��$ Right=�$��Center=����#000000$��4��$��0���� $��4��$��1��$	��220��$
��Left=�$��Center=�
 �� SelectedIndex
 �� *SelectedGenre+ �� Mode$ TwoWay=����� *Genres+�� $��Година:��$ Right=�$��Center=����#000000$��5��$��0���� $
 Left=�$	��220��$��5��$��1������ *PublishedYear+$	 PropertyChanged=���  �� Orientation$ 
Horizontal=�$��Right=�$��6��$��0��$��2��.+��� ���� *AddBookCommand+����  �� ElementName$ AddBookWindow������ $ 
Horizontal=�.+���   AddImage#���  �� $��Добави книга���� ���� *CloseWindowCommand+���� $ AddBookWindow������ $ 
Horizontal=�.+���   RemoveImage#���  �� $��Откажи��