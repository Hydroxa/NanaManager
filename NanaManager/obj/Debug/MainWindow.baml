   M S B A M L   `   `   `  ����L  HNanaManager, Version=0.18.704.8374, Culture=neutral, PublicKeyToken=null)clr-namespace:NanaManagerNanaManager  5   	       NanaManager.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:NanaManager     5   	   -   5          �Name$  wndMain��5       � Title$ Nana Manager��5	   	   $	��450��6   $	��800��6+   $��Stretch=�67   $��Stretch=�6Z    � AllowsTransparency .  � WindowStyle$
 None=�6�   $	��800��6�   $	��450��6�   
����������	#FF200C27 � WindowStartupLocation$ CenterScreen=�61   � 	Resources 5
         blinkScreen g� AutoReverse g� Duration	 W� To	 W� From
 �� 
TargetName �� TargetProperty��%�   &	        �� 5      .��5      W�  . $ 0:0:0.2D�6(   $ 0X�6;   $	 1X�6B   $
 frmMain��6K   $ Opacity�6k   5      5      '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5      -   5        	wchChrome��   CaptionHeight$ 25Q�5   $   %  System.Windows.SystemParameters"   WindowResizeBorderThickness   ResizeBorderThickness# Z 67   .��5      ��  �� CornerRadius$ 10��6
   $��1q�6     
Background#���  60   ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      �� 5       �� Binding 6   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" � �� Path$ WindowState� �� Value 	Maximized6�   .a�5      ��  �� Property � �� �� Value$ 6q�6!   5      5      ._�5      � 
������ $��-1q�6"    � RowDefinitions 5      �� 5      $��25 �6   �� 5      $	��71* �6   .+�5!       � -   5          �Name$ frmMain��5!      $��5��6   $��0q�60   $��1��6;   $��2��6H     � SandboxExternalContent . ��.   � NavigationUIVisibility$ Hidden=�6�   $��Stretch=�6�   $��Stretch=�6�   � 5$      -   5         �Name$ grdTitle��5$   
    � ColumnDefinitions 5%      �� 5&      $
��Auto �6   �� 5'      $��* �6   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   .+�5,      �� -   5          ��Name$  rctTitleBar��5,      $��5��6%     TitleBar#���  69   ��5-      �� 5.      " ��6    5/   	   �� 50   
    6   �� *
IsActive+ �� $ WindowL�	 False6h   .a�51      ��  � ��  TitleBarInactive# �  6"   52      54   	   56      �� 57      -   5        ! ��Name$! imgIcon��57      $��0��6   $��Center=�6-   $C��=pack://siteoforigin:,,,/Resources/Nana Manager Icon Small.png��6J   $��UniformToFill=�6�   �� 58      $��1��6   $��4 0 0 0q�6    $��Center=�61   $��14(�6L     	LightText#���  6�   ��6Z   �� " �� ElementName$" wndMain��$ Title���59      �� 5:      " ��6    5;   	   �� 5<   
    6   �� *
IsActive+ �� $ WindowL�	 False6h   .a�5=      ��  � ��  TitleBarInactiveText# �  6(   5>      5@   	   5B      �� 5C      -   5        $��2��5C      # �� EdgeMode$# Aliased=�6;     TitleBarButtonStyle#��[ 6\   .��5D      F� $��46��6   $��25��6   ���Ѐ�@]� � ?�@]� 	$��1��6�   ��67   �� $ 
Foreground� �� " ��6�   5E      �� 5F      -   5        $��btnMaximise��5F      $��3��6    #��[ 6U   .��5G      F� $��46��6   $��25��6   +����I �� ���� �� ����`ѠI`	$��1��6�   ��6L   �� $ 
Foreground� �� " ��6�   5H      �� 5I      -	   5        $��
btnRestore��5I      $��3��6   #��[ 6T   $y�	Collapsed=�6�   .��5J      F� $��46��6   $��25��6   X����I�7� �`��7� �`�`ѠI`� �8�7� � �8 �� ���� �� ������� �`���� 	$��1��6�   ��6t   �� $ 
Foreground� �� " ��6�   5K      �� 5L      -
   5        $��4��5L        TitleBarCloseButtonStyle#��[ 68   .��5M      F� $��46��6   $��25��6   +���Ѐ� z ����@fЀ�@f���� z 	$��1��6�   ��6D   �� $ 
Foreground� �� " ��6�   5N      5O      �� 5R      -   5        $ ��Name$$ bdrNotif��5R   #   $y�	Collapsed=�6   $��1��65   $��40��6B     
BlackGlass#���  6N   $ 
0,10,10,10��6x   $	��300��6�   $
��Left=�6�   $	��Top=�6�   ._�5S      �� -   5        % ��Name$% lblNotif��5S      $��18(�6   $��Sorting by Tag��6,   #���  6E   $��Center=�6n   $��Center=�6�   5T      �� 5U      -   5        $$ bdrNotifList��5U   #   $y�	Collapsed=�6   $��1��69   $��40��6F   #���  6R   $ 
0,10,10,10��6|   $	��300��6�   $
��Left=�6�   $	��Top=�6�   ._�5V      �� .��5W      �� -   5        & ��Name$& stkNotifList��5W      ' �� Orientation$' Vertical=�6(   $	��300��6?   6K   5X      5Y      5Z      5[      5\      