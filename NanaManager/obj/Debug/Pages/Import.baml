   M S B A M L   `   `   `  ����I  ENanaManagerAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_@clr-namespace:NanaManagerAPI.UI.Controls;assembly=NanaManagerAPINanaManagerAPI.UI.Controls  5      L HNanaManager, Version=0.18.704.8374, Culture=neutral, PublicKeyToken=null)clr-namespace:NanaManagerNanaManager 5         NanaManager.Import   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Rnmapi@clr-namespace:NanaManagerAPI.UI.Controls;assembly=NanaManagerAPI     5      +localclr-namespace:NanaManager     5      -   5          N� Title$  Import��5
      .��5      �  � RowDefinitions 5      �� 5      $
��252* �6   �� 5      $
��143* �6   �� 5      $��30 �6    � ColumnDefinitions 5      �� 5      �� 5      .+�5       � -   5          �Name$ 
frmPreview��5   
   $��1��6   $��Stretch=�6.   $��10q�6L   $��Stretch=�6X   $��3��6t     � SandboxExternalContent . ��.   � NavigationUIVisibility$ Hidden=�6�   �� 5      $��Stretch=�6   $��
2,10,0,100q�6)   $��Stretch=�6=   ���	#FF959595$��2q�6q    �� CornerRadius$ 4��6�   $��2��6�   ._�5      ,   &NanaManagerAPI.UI.Controls.TagSelector  -   5          Name$ 	tslEditor��5         	LightText#���   6*     
LightGlass   GroupBoxBrush# �  6S     
BlackGlass	   LoadingBrush#	 �  6�   5      �� 5      -   5        
 ��Name$
 lblIndex��5   
   $	��0/0��6   $��Stretch=�6*   $��161,0,161,4q�6H   $��2��6]   $��Bottom=�6j   
�������$��Center=�6�   $��Center=�6�   $��23��6�   �� 5      -   5         ��Name$ 	btnCancel��5   1     Menu Button#���  6   $��Cancel��6D   $
��Left=�6U   $��25��6p   $��2,0,0,2q�6|   $��Bottom=�6�   $��83��6�   $��2��6�   6�   �� 5      -   5        $ btnBack��5   1   #���  6   $
��Back��6B   $
��Left=�6Q   $��25��6l   $��90,0,0,2q�6x   $��Bottom=�6�   $��66��6�   $��2��6�   6�   �� 5      -   5        $ btnUnimport��5   1   #���  6   $��Don't Import��6F   $��Right=�6]   $��25��6y   $��0,0,2,2q�6�   $��Bottom=�6�   $��83��6�   $��2��6�   6�   �� 5      -   5        $ btnSkip��5   1   #���  6   $
��Next��6B   $��Right=�6Q   $��25��6m   $��0,0,90,2q�6y   $��Bottom=�6�   $��66��6�   $��2��6�   6�   �� 5      -	   5        $ btnAdd��5   1   #���  6   $	��Add��6A   $��Right=�6O   $��25��6k   $��0,0,2,10q�6w   $��Bottom=�6�   $��83��6�   $��1��6�   6�   �� 5      -
   5        $ btnManageTags��5   0   #��[ 6   $��Manage Tags��6G   $
��Left=�6]   $��25��6x   $��2,0,0,10q�6�   $��Bottom=�6�   $��83��6�   $��1��6�   6�   �� 5       -   5         ��Name$ cbxCopy��5       $��Copy instead of Move��6   $
��Left=�6=   $��0,0,0,40q�6X   $��1��6j   $��Bottom=�6w   $	��146��6�   $��13(�6�   
�������
������� d� 	IsChecked$
 TrueY�6�   �� 5!      -   5         ��Name$ 	bdrCancel��5!      $��Stretch=�6   $��Stretch=�6<   $��2��6X   $��3��6l   #���  6}   $y�	Collapsed=�6�   ._�5"      �� $��200,100q�6     	DarkGlass#���  6   
���   �$��2q�6Z   $��Center=�6n   $��Center=�6�   ._�5#      �� .+�5$      �� -   5        $
 
lblClarify��5$      $&�� Are you sure you want to cancel?��6!   #���   6L   $��16(�6u   $��39��6�   $��Center=�6�   $��Center=�6�   $��0,10,0,0q�6�   �� 5%      -   5        $
 lblProgress��5%      $%��Your progress will NOT be saved��6"   #���   6L   $��14(�6u   $��39��6�   $��Center=�6�   $��Center=�6�   $��	0,-20,0,0q�6�   �� 5&      $��57��6   $��10,50q�6    �� Orientation$ 
Horizontal=�6-   .+�5'      �� -   5        $ btnYes��5'   5   #���  6   $	��Yes��6E   $��Stretch=�6S   $��5,5,0,5q�6q   $��Stretch=�6�   $	��170��6�   $��1��6�   $
��Auto��6�   6�   �� 5(      -   5        $ btnNo��5(   5   #���  6   $��No��6D   $��Stretch=�6Q   $��25,5,0,5q�6o   $��Stretch=�6�   $	��170��6�   $
��Auto��6�   6�   5)      5*      5+      5,      �� 5-      -   5        $ bdrImporting��5-      $��Stretch=�6!   $��Stretch=�6?   $��2��6[   $��3��6o   #���  6�   $y�	Collapsed=�6�   ._�5.      � $��200,100q�6
    5/      �� 50      �� 51      .+�53      (� .��54      �� $��Importing..��6   #���   6#   $��16(�6L   $��39��6Z   $��Center=�6f   $��Center=�6�   $��0,10,0,0q�6�   55      (� 56      $��1��6   .��57      �� $!��This may take a few seconds��6   #���   63   $��14(�6\   $��39��6j   $��Center=�6v   $��Center=�6�   $��0,10,0,0q�6�   58      59      5:      5;      5<      