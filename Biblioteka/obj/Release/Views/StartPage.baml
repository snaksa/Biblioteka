   M S B A M L   `   `   `  ���� E  ABiblioteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Biblioteka.ViewModelsBiblioteka.ViewModels       Biblioteka.Views.StartPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9	viewModel#clr-namespace:Biblioteka.ViewModels        
WindowIcon  � Icon#  [  $	��228��$	��330�� � 
ResizeMode$ NoResize=� � Title �� *	PageTitle+ � DataContext .   (Biblioteka.ViewModels.StartPageViewModel  .���� {��� *	EnableApp+.+�o� .@�m� -    m�Name$ _user��$
��Left=�$��Center=����� $��Читатели��$��Center=�$
��Left=�$��-8,0,0,0q� m� Icon ��   	UserImage#���  $��0,0,0,0q�$��Center=�$��16��$��16������ .R�$�  $� Orientation$ Vertical=�.@�m� -   $ 	addReader��$*��$Добавяне на читател�� m� Command �� *AddReaderToDB+ ��   AddUserImage#���  $��0,0,0,0q�$��Center=�$��16��$��16��m� -   $ 
infoReader��$(��"Търсене на читател�� �� *SearchUserCommand+ ��   SearchImage#���  $��0,0,0,0q�$��Center=�$��16��$��16��m� -   $ _book��$
��Left=�$��Center=����� $��
Книги��$��Center=�$
��Left=�$��-8,0,0,0q� ��   	BookImage#���  $��0,0,0,0q�$��Center=� �� BitmapScalingMode$ HighQuality=�$��16��$��16������ .R�$� $ Vertical=�.@�m� -   $ addBook��$&�� Добавяне на книга�� �� *AddBookToDBCommand+ ��   AddImage#���  $��0,0,0,0q�$��Center=�$ HighQuality=�$��16��$��16��m� -   $ infoBook��$$��Търсене на книга�� �� *SearchBookCommand+ �� #���  $��0,0,0,0q�$��Center=�$ HighQuality=�$��16��$��16��m� -   $ 
takenBooks��$��Взети книги�� �� *TakenBooksCommand+ ��   TakenBooksImage#���  $��0,0,0,0q�$��Center=�$ HighQuality=�$��16��$��16��m� -   $ 	_settings��$
��Left=�$��Center=� �� *SettingsCommand+���� $��Настройки��$��Center=�$
��Left=�$��-8,0,0,0q� ��   SettingsImage#���  $��0,0,0,0q�$��Center=�$ HighQuality=�$��16��$��16��m� -	   $ _stats��$
��Left=�$��Center=� �� *StatsCommand+���� $��
Отчет��$��Center=�$
��Left=�$��-8,0,0,0q� ��   	StatsIcon#��[ $��0,0,0,0q�$��Center=�$ HighQuality=�$��16��$��16����  	 LibraryImage#��� 	 $
��Fill=�