   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  %    Biblioteka.Views.TakenBooksList   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels        
WindowIcon  � Icon#  [   � Title$ Взети книги��$	��375��$	��710�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext 3   -Biblioteka.ViewModels.TakenBooksListViewModel  .��� $��0,0,0,0q� � RowDefinitions �� $
��Auto � � ColumnDefinitions �� $	��700 �.+��� $	��330��$	��690��$��Взети книги��$	��Top=�$��Center=�$��0��$��0��$��5,5,5,5q�.���  �� $��60 ��� $	��200 ��� $��40 � �� $��1* �.+��� $��0��$��0��$��Center=�.+���  �� TextAlignment$
 Left=�$��Center=����#000000Покажи:��  �� Orientation$ 
Horizontal=�.+�	�  	� 	GroupName$ TakenBookChoices��$��Всички��$��AllTakenBooks��	 d� 	IsChecked$
	 TrueY����� *ChangeListDataCommand+�� $
 Left=�$��Center=����#000000(�� $
 Left=�$��Center=����#000000���� *NumberOfAllTakenBooks+�� $
 Left=�$��Center=����#000000)�� $ 
Horizontal=�.+�	� $ TakenBookChoices��$��Просрочени��$��ExpiredTakenBooks������ *ChangeListDataCommand+�� $
 Left=�$��Center=����#000000(�� $
 Left=�$��Center=����#000000���� *NumberOfAllExpiredBooks+�� $
 Left=�$��Center=����#000000)�� $��1��$��0������ *
TakenBooks+
 �� SelectedIndex
 �� *SelectedRecord+����  �� 
TargetType" ��.���  �� Property � �� �� Value$
 Left=� �� View �� .P��� $��Заглавие�� �� Width$	 140�� �� DisplayMemberBinding �� *Title+�� $��
Автор��$	 110�� �� *Author+�� $��Взета от��$	 150�� �� *
ReaderName+�� $��Клас��$ 50�� �� *ClassAndParalelka+�� $��Взета на��$	 100�� �� *DateOfTaking+ �� StringFormat$ 
dd/MM/yyyy���� $��Срок��$	 100�� �� *DaysToDeadlineString+�� $ 
Horizontal=�$��Right=�$��2��$��0��$��2��.+��� ���� *ShowReaderDetailsCommand+{��� *EnableButtons+���� $ 
Horizontal=�.+���   InfoUserImage#���  �� $&�� Данни за читателя���� ���� *ShowBookDetailsCommand+{��� *EnableButtons+���� $ 
Horizontal=�.+���   InfoBookImage#���  �� $$��Данни за книгата��