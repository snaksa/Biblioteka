   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  %    Biblioteka.Views.StatisticsPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels        
WindowIcon  � Icon#  [   � Title$ Статистика��$	��380��$	��450�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext /   )Biblioteka.ViewModels.StatisticsViewModel  .��� $��0,0,0,0q� � RowDefinitions ��  � ColumnDefinitions �� .+��� $��0��$��0��$��5,5,5,5q�.@��� $��Читатели��.���  �� $��20 ��� $	��280 ��� $��30 � �� $��3* ��� $	��10* �.+���  �� Orientation$ 
Horizontal=�$��0��$��0��$��2��$��Center=�.+��� $ 
Horizontal=�$��Right=�.+���   	UserImage#��[ $��16��$��16����  �� TextAlignment$ Right=�$��Center=����#000000Читатели:�� 
���   �$��7 0 0 0q�$��Center=����� *NumberOfReaders+�� $/��)Топ читатели за период��$��1��$��0��$��2��$	��270��$	��410��$	��Top=�$��Center=�.���� $ Vertical=�.+��� $ Vertical=�.+��� $ 
Horizontal=�.+��� $ 
Horizontal=�$	��130��.+���   TakenBookIcon#��[ $��16��$��16���� $ Right=�$��Center=����#000000Активни:�� 
���   �$��7 0 0 0q�$��40��$
��Left=�$��Center=����� *NumberOfActiveReaders+�� 
���   �$��2 0 0 0q�$��20��$
��Left=�$��Center=�$
��От��(  "System.Windows.Controls.DatePicker  $��27��$	��105��$��Center=�$��2,2,2,2q�
������ ���#CCEBFF   SelectedDate �� *	StartDate+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 
���   �$��2 0 0 0q�$��20��$
��Left=�$��Center=�$
��до��  $��27��$	��105��$��Center=�$��2,2,2,2q�
������ ���#CCEBFF �� *	FinalDate+$	 PropertyChanged=��� $ 
Horizontal=�.+���   StarIcon#��[ $��16��$��16���� 
���   �$��2 0 0 0q�$��20��$
��Left=�$��Center=�$��Топ���� $��50��$
��Left=�$��Center=�
 �� SelectedIndex
 �� *SelectedTopReadersCategory+ �� Mode$ TwoWay=�.@��� .��5�� .��10�� .��20�� .��50�� 
���   �$��2 0 0 0q�$��60��$
��Left=�$��Center=�$��читатели���� $	��140������ *
TopReaders+
 �� *SelectedReader+$	 PropertyChanged=�����  �� 
TargetType" ��.���  �� Property � �� �� Value$
 Left=� �� View �� .P��� $��
Място�� �� Width$ 40�� �� DisplayMemberBinding �� *
Position+�� $��ЕГН��$ 90�� �� *EGN+�� $��Име��$	 150�� �� *Name+�� $��Клас��$ 40�� �� *ClassAndParalelka+�� $��
Книги��$ 40�� �� *NumberOfBooks+�� $ 
Horizontal=�$��Right=�.+��� $��
ListButton������ *ShowReaderDetailsCommand+���� $ 
Horizontal=�.+���   InfoUserImage#���  �� $&�� Данни за читателя���� ���� *RefreshUserDataCommand+���� $ 
Horizontal=�.+���   RefreshIcon#���  �� $��Обнови���� $��
Книги��.���  �� $��20 ��� $	��280 ��� $��30 � �� $��3* ��� $	��10* �.+��� $ 
Horizontal=�$��0��$��0��$��2��$��Center=�.+��� $ 
Horizontal=�$��Right=�.+���   	BookImage#��[ $��16��$��16���� $ Right=�$��Center=����#000000Книги:�� 
���   �$��7 0 0 0q�$��Center=����� *NumberOfBooks+�� $)��#Топ книги за период��$��1��$��0��$��3��$	��270��$	��410��$	��Top=�$��Center=�$��6,4,6,0q�.���� $ Vertical=�.+��� $ Vertical=�.+��� $ 
Horizontal=�.+��� $ 
Horizontal=�$	��130��.+��� #��[ $��16��$��16���� $��Center=����#000000Взети книги:�� 
���   �$��7 0 0 0q�$��40��$
��Left=�$��Center=����� *NumberOfTakenBooks+�� 
���   �$��2 0 0 0q�$��20��$
��Left=�$��Center=�$
��От��  $��27��$	��105��$��Center=�$��2,2,2,2q�
������ ���#CCEBFF �� *	StartDate+$	 PropertyChanged=��� 
���   �$��2 0 0 0q�$��20��$
��Left=�$��Center=�$
��до��  $��27��$	��105��$��Center=�$��2,2,2,2q�
������ ���#CCEBFF �� *	FinalDate+$	 PropertyChanged=��� $ 
Horizontal=�.+��� #��[ $��16��$��16���� 
���   �$��2 0 0 0q�$��20��$
��Left=�$��Center=�$��Топ���� $��50��$
��Left=�$��Center=�
 �� *SelectedTopBooksCategory+$ TwoWay=�.@��� .��5�� .��10�� .��20�� .��50�� 
���   �$��2 0 0 0q�$��60��$
��Left=�$��Center=�$��
книги���� $	��140������ *
TopBooks+
 �� *SelectedBook+$	 PropertyChanged=����� " ��.���  � ��$
 Left=� �� .P��� $��
Място��$ 40�� �� *
Position+�� $��
Номер��$ 60�� �� *SerialNumber+�� $��Заглавие��$	 130�� �� *Title+�� $��
Автор��$ 80�� �� *Author+�� $��Читатели��$ 60�� �� *NumberOfReaders+�� $ 
Horizontal=�$��Right=�.+��� $��
ListButton������ *ShowBookDetailsCommand+���� $ 
Horizontal=�.+���   InfoBookImage#���  �� $ ��Данни за книга���� ���� *RefreshBookDataCommand+���� $ 
Horizontal=�.+��� #���  �� $��Обнови��