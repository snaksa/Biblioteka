   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  (    "Biblioteka.Views.SearchStudentPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels     -     �Name$  SearchStudentWindow��   
WindowIcon � Icon# [   � Title$( "Търсене на читател��$	��510��$	��477�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext 2   ,Biblioteka.ViewModels.SearchStudentViewModel  .��� $��0,0,0,0q� � RowDefinitions �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��470 �.+��� $��0��$��0��$��Търсене��.���  �� $��35 ��� $��35 � �� $��70 ��� $	��280 ��� $	��110 �.+��� $��0��$��0�� �� TextAlignment$ Right=�$��Center=����#000000Търсене по:�� $��0��$��1�� �� Orientation$ 
Horizontal=�$��2��.+��� $	��100��$
��Left=�$��Center=�	 �� SelectedIndex	 �� *SearchOption+
 �� Mode$
 TwoWay=�.@��� .��ЕГН�� .��Име�� .��
Клас�� $	��100��$
��Left=�$��Center=�	 �� *SearchClassNumber+$
 TwoWay=�y��� *SearchClassOptionsVisibility+.@��� .��1�� .��2�� .��3�� .��4�� .��5�� .��6�� .��7�� .��8�� .��9�� .��10�� .��11�� .��12�� $	��100��$
��Left=�$��Center=�	 �� *SearchClassParalelka+$
 TwoWay=�y��� *SearchClassOptionsVisibility+ �� UpdateSourceTrigger$ PropertyChanged=����� *	Paralelki+�� $��1��$��0��$ Right=�$��Center=����#000000	Ключ:�� $��1��$��1��$
��Left=�$	��270������ *SearchValue+$ PropertyChanged=�{��� *SearchValueEnabled+$ PropertyChanged=��� $��1��$��2��$��
Търси��$	��100������ *SearchReaderCommand+�� $��1��$��0��$��Читатели��$	��360��$	��460��$	��Top=�$��Center=�.���� .+��� $	��300������ *SelectedReaders+	 �� *SelectedReader+$ PropertyChanged=� �� View �� .P��� $��ЕГН�� �� Width$ 80�� �� DisplayMemberBinding �� *EGN+�� $��
Номер��$ 75�� �� *SerialNumber+�� $��Име��$	 180�� �� *Name+�� $��Клас��$ 80�� �� *ClassAndParalelka+�� $ 
Horizontal=�$��Right=�.+��� ���� *ChooseCommand+����  �� ElementName$ SearchStudentWindow��y��� *ChooseVisibility+���� $ 
Horizontal=�.+���   ChooseImage#���  �� $!��Избери читател���� ���� *ShowDetailsCommand+���� $ 
Horizontal=�.+���   InfoUserImage#���  �� $$��Данни за читател���� ���� *RemoveReaderCommand+���� $ SearchStudentWindow������ $ 
Horizontal=�.+���   RemoveUserImage#���  �� $%��Премахни читател���� ���� *RefreshDataCommand+���� $ 
Horizontal=�.+���   RefreshIcon#���  �� $��Обнови��