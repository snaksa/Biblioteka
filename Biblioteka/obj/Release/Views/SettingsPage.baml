   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  #    Biblioteka.Views.SettingsPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels        
WindowIcon  � Icon#  [   � Title$ Настройки��$	��260��$	��400�����#D6FFFF � 
ResizeMode$ NoResize=� � DataContext 1   +Biblioteka.ViewModels.SettingsPageViewModel  .��� $��0,0,0,0q� � RowDefinitions �� $
��Auto � � ColumnDefinitions �� $��1* �.+��� $��0��$��0��$��5,5,5,5q�$
��auto��.@��� $��Категории��.���� $	��170��$	��350��$$��Списък с жанрове��$	��Top=�$��Center=�$��5,5,5,5q�.���  �� $	��140 � �� $��5* ��� $��3* �.+��� $��0��$��0������ *	AllGenres+ �� SelectedIndex �� *SelectedGenre+����  �� 
TargetType" ��.���  �� Property � ��	 �� Value$
	 Left=�
 �� View
 �� .P��� $��Жанр�� �� Width$	 180�� �� DisplayMemberBinding �� *	GenreText+��  �� Orientation$ Vertical=�$��Stretch=�$��0��$��1��$��2��.+��� ���� *RenameGenreCommand+���� $ 
Horizontal=�.+���   RenameImage#���  �� $��Преименувай���� ���� *AddGenreCommand+���� $ 
Horizontal=�$��Right=�.+���   AddImage#���  �� $��Добави���� ���� *RemoveGenreCommand+���� $ 
Horizontal=�.+���   RemoveImage#���  �� $��Премахни���� $��Паралелки��.���� $	��170��$	��350��$(��"Списък с паралелки��$	��Top=�$��Center=�$��5,5,5,5q�.���  �� $	��140 � �� $��5* ��� $��3* �.+��� $��0��$��0������ *AllParalelki+ �� *SelectedParalelka+���� " ��.���  � ��$
	 Left=�
 �� .P��� $��Паралелки��$	 180�� �� *	Character+�� $ Vertical=�$��Stretch=�$��0��$��1��$��2��.+��� ���� *RenameParalelkaCommand+���� $ 
Horizontal=�.+��� #���  �� $��Преименувай���� ���� *AddParalelkaCommand+���� $ 
Horizontal=�.+��� #���  �� $��Добави���� ���� *RemoveParalelkaCommand+���� $ 
Horizontal=�.+��� #���  �� $��Премахни��