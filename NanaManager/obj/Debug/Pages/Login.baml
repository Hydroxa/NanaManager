   M S B A M L   `   `   `  ����L  HNanaManager, Version=0.18.704.8374, Culture=neutral, PublicKeyToken=null)clr-namespace:NanaManagerNanaManager  5          NanaManager.Login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:NanaManager     5      -   5          N� Title$  Login��5	      .��5
      � .+�5      �� -   5         ��Name$ tbxUsername��5      
 � Tag$ Username��6!   $��Center=�60   $��317,172,317,221q�6M    �� TextWrapping$
 Wrap=�6f   $��Center=�6z   $��Bottom=�6�   $��16(�6�   
����������	#FF6C0243$��0,0,0,2q�6  $��32��6   $	��166��6,  $��0q�68     
CaretColor �� 
CaretBrush# �   6D    TextHighlightColor �� SelectionBrush# �  6�   �� SelectionTextBrush �	#FF424242��6�   Z� 6�  G� 5      -   5         G�Name$ tbxPassword��5      $��Center=�6%   $��0,50,0,0q�6B   $��Center=�6T   $��Bottom=�6o   $��16(�6�   
����������	#FF6C0243$��0,0,0,2q�6�   $��32��6�   $	��166��6  $��0q�6   G� 
CaretBrush
 �����	 G� SelectionBrush	 �	#FFFF2121
 G� SelectionTextBrush
 �	#FF424242��6�   Z� 6k  �� 5      -   5        $��Login��5        Primary Button#���  6   $��19(�6D   $��Center=�6R   $��39��6o   $��	0,150,0,0q�6{   $��Center=�6�   $	��166��6�   6�   �� 5      $��Username��6
   $��Center=�6   $��
0,0,260,50q�6:   $��Center=�6N   $��Center=�6i   $��Center=�6�   
�������$��18(�6�   �� 5      $��Password��6
   $��Center=�6   $��
0,50,255,0q�6:   $��Center=�6N   $��Center=�6i   $��Center=�6�   
�������$��18(�6�   �� 5      -   5        $��Change Login��5      #���  6"   $
��Left=�6K   $��39��6f   $��10,376,596,10q�6r   $��Bottom=�6�   $	��194��6�   $��18(�6�   6�   �� 5      $��Center=�6
   $	��164��6'   $��	0,3,0,258q�64   $��Center=�6G   $	��350��6b   $(��"../Resources/Nana Manager Logo.png��6n   $��UniformToFill=�6�   �� 5      ���	#FF6C0243$��0,0,0,2q�6#   $��Center=�6=   $��32��6Z   $��483,172,285,221q�6f   $��Center=�6   $��32��6�   ��5      �� 5       �� ImageSource$4 .pack://siteoforigin:,,,/Resources/Username.png��6   5      �� 5      ���	#FF6C0243$��0,0,0,2q�6#   $��Center=�6=   $��32��6Z   $��483,222,285,171q�6f   $��Center=�6   $��32��6�    � BindingGroup 5      &   System.Windows.Data.BindingGroup  5      ��5      �� 5      $8 2pack://siteoforigin:,,,/Resources/PasswordShow.png��6   5      �� 5      -   5         ��Name$ lblBadInput��5   
   $-��'Your username or password was incorrect��6   $��Center=�6Q   $��32��6n   $��
0,312,0,81q�6z   $��Center=�6�   $	��286��6�   $��Center=�6�   $��Center=�6�   ���	#FFFF3C3C$��14(�6  �� 5       $��AP[AED]b0.13.8.0��6
   $��Right=�6%   $��Bottom=�6A     	LightText#���  6\   5!      5"      