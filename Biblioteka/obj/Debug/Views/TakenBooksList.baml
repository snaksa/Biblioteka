   M S B A M L   `   `   `  ����E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels  5   	   %    Biblioteka.Views.TakenBooksList   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9	viewModel#clr-namespace:Biblioteka.ViewModels     5   	     � Title$  TakenBooksList��5   	   $	��500��6    $	��715��6-   ���#D6FFFF � DataContext 5      3   -Biblioteka.ViewModels.TakenBooksListViewModel  5   
   .��5	      � $��0,0,0,0q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��700 �6   .+�5   
   �� $	��330��6   $	��690��5      $��Взети книги��5      $	��Top=�5      $��Center=�5      $��0��5      $��0��5      $��5,5,5,5q�5      .��5      �  5      �� 5      $��60 �6$   �� 5      $	��200 �6$   �� 5      $��40 �6$    5       �� 5!      $��1* �6'   .+�5$      �� $��0��6   $��0��5%      $��Center=�5&      .+�5'      ��  �� TextAlignment$
 Left=�5(      $��Center=�5)      ���#000000Покажи:5*   :   	� 5+       	� 	GroupName$ TakenBookChoices��6"   $��Всички��5,   "   $��AllTakenBooks��5.   "    d� 	IsChecked$
 TrueY�5/   "   ��5-   "   �� *ChangeListDataCommand+5/   "   	� 50      $ TakenBookChoices��6"   $��Просрочени��51   "   $��ExpiredTakenBooks��53   "   ��52   "   �� *ChangeListDataCommand+53   "   54      �� 56      $��1��6   $��0��57   #   ��58   #   �� *
TakenBooks+ �� SelectedIndex 59   #   �� *SelectedRecord+��5;      �� 5<       �� 
TargetType" ��6    .�5=      �� 	 �� Property	 � ��
 �� Value$

 Left=�6K   5>       �� View 5A      �� 5B      .P�5C      �� $��Заглавие��6-    �� Width$	 150��6?    �� DisplayMemberBinding 6K   �� *Title+�� 5D      $��
Автор��6-   $	 120��6<    6H   �� *Author+�� 5E      $��Взета от��6-   $	 150��6?    6K   �� *
ReaderName+�� 5F      $��Клас��6-   $ 50��6;    6F   �� *ClassAndParalelka+�� 5G      $��Взета на��6-   $	 100��6?    6K   �� *DateOfTaking+ �� StringFormat$ 
dd/MM/yyyy���� 5H      $��Срок��6-   $	 100��6;    6G   �� *DaysToDeadlineString+5I      5K      �� 5M       �� Orientation$ 
Horizontal=�6   $��Right=�5N      $��2��5O      $��0��5P      $��2��5Q      .+�5R      �� ��6   �� *ShowReaderDetailsCommand+{�5S      �� *EnableButtons+��5T      �� 5U      $ 
Horizontal=�6)   .+�5V   "   ��    InfoUserImage#���   6(   �� 5W   "   $&�� Данни за читателя��6,   5X      5Z      �� 5[      ��6   �� *ShowBookDetailsCommand+{�5\      �� *EnableButtons+��5]      �� 5^      $ 
Horizontal=�6)   .+�5_   "   ��   InfoBookImage#���  6(   �� 5`   "   $$��Данни за книгата��6,   5a      5c      5d      5e      5f      5h      5i      