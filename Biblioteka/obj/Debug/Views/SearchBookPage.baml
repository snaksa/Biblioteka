   M S B A M L   `   `   `  ����E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  5   	   %    Biblioteka.Views.SearchBookPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9	viewModel#clr-namespace:Biblioteka.ViewModels     5   	   -   5          �Name$  SearchBookWindow��5   	    � Title$$ Търсене на книга��5   	   $	��565��6"   $	��485��6/   ���#D6FFFF � 
ResizeMode$ NoResize=�6P    � DataContext 5      /   )Biblioteka.ViewModels.SearchBookViewModel  5   
   .��5
      � $��0,0,0,0q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��470 �6   .+�5   
   �� $��0��6   $��0��6    $��Търсене��60   .��5      �  5      �� 5      $��35 �6$   �� 5      $��35 �6$    5      �� 5      $��70 �6'   �� 5      $	��280 �6'   �� 5      $	��110 �6'   .+�5      �� $��0��6   $��0��5        �� TextAlignment$ Right=�5!      $��Center=�5"      ���#000000Търсене по:5#   >   �� 5$      $��0��6   $��1��5%      $��2��5&       �� Orientation$ 
Horizontal=�5'      .+�5(      �� $	��100��6    $
��Left=�5)      $��Center=�5*       �� SelectedIndex 5+      �� *SearchBookOption+	 �� Mode$	 TwoWay=�.@�5,      �� .��6'   
Номер6.   �� 5-      .��6'   
Автор6.   �� 5.      .��6'   Заглавие61   �� 5/      .��6'   
Жанр6-   50      �� 51      $	��267��6    $
��Left=�52      $��Center=�53       54      �� *SelectedGenre+$	 TwoWay=���55      �� *Genres+y�56      �� *GenresVisibility+57      58      �� 5:      $��1��6   $��0��5;      $ Right=�5<      $��Center=�5=      ���#000000	Ключ:5>   8   �� 5?      $��1��6   $��1��5@      $
��Left=�5A      $	��270��5B      ��5C      �� *SearchValue+$	 TwoWay=�{�5D      �� *SearchValueEnabled+
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5E      $��1��6   $��2��5F      $��
Търси��5G      $	��100��5H      ��5I      �� *SearchCommand+5J      5K      �� 5M   
   $��1��6   $��0��5N      $��5,5,5,5q�5O       5P      �� *SelectedTabItem+.@�5Q      �� $!��Актуални книги��6   .��5R      �� $1��+Списък с актуални книги��6   $	��360��5S      $	��440��5T      $	��Top=�5U      $��Center=�5V      .��5W      �� .+�5X      �� $	��300��5Z   #   ��5X   #   �� *
FoundBooks+ 5Y   #   �� *SelectedBook+$
 PropertyChanged=� �� View 5[      �� 5\   "   .P�5]   &   �� $��
Номер��65    �� Width$ 60��6D    �� DisplayMemberBinding 6O   �� *SerialNumber+�� 5^   &   $��Заглавие��65   $	 150��6G    6S   �� *Title+�� 5_   &   $��
Автор��65   $	 140��6D    6P   �� *Author+�� 5`   &   $��Статус��65   $ 75��6E    6P   �� *Status+5a   #   5c      �� 5d      $ 
Horizontal=�6%   $��Right=�6>   .+�5e      �� ��6%   �� *ChooseBookCommand+��5f      ��  �� ElementName$ SearchBookWindow��y�5g      �� *ShowChooseButton+��5h   "   �� 5i   &   $ 
Horizontal=�61   .+�5j   *   ��    ChooseImage#���   60   �� 5k   *   $��Избери книга��64   5l   '   5n      �� 5o      ��6%   �� *BookDetailsCommand+��5p   "   �� 5q   &   $ 
Horizontal=�61   .+�5r   *   ��   InfoUserImage#���  60   �� 5s   *   $ ��Данни за книга��64   5t   '   5v      5w      5x      5y      5z      �� 5{      $%��Архивирани книги��6   .��5|      �� $5��/Списък с архивирани книги��6   $	��360��5}      $	��440��5~      $	��Top=�5      $��Center=�5�      .��5�      �� .+�5�      �� $	��300��6�   ��6#   �� *ArchivedBooks+ 6I   �� *SelectedArchivedBook+$
 PropertyChanged=� 5�      �� 5�   "   .P�5�   &   �� $��
Номер��65   $ 60��6D    6O   �� *SerialNumber+�� 5�   &   $��Заглавие��65   $	 150��6G    6S   �� *Title+�� 5�   &   $��
Автор��65   $	 140��6D    6P   �� *Author+�� 5�   &   $��Статус��65   $ 75��6E    6P   �� *Status+5�   #   5�      �� 5�      $ 
Horizontal=�6%   $��Right=�6>   .+�5�      �� ��6%   �� *BookDetailsCommand+��5�   "   �� 5�   &   $ 
Horizontal=�61   .+�5�   *   �� #���  60   �� 5�   *   $ ��Данни за книга��64   5�   '   5�      5�      5�      5�      5�      5�      5�      5�      