   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.4.0.6, Culture=neutral, PublicKeyToken=nullfCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs  5   	    	TestExams   TestExams.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;Controls,http://metro.mahapps.com/winfx/xaml/controls   5   	   PDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro   5   	   -   5        $��0q�5   
   (   "MahApps.Metro.Controls.MetroWindow    	GlowBrush
  �   � � 
ResizeMode$ CanResizeWithGrip=�5   
    � WindowStartupLocation$ CenterScreen=�5	   
      WindowTransitionsEnabled .   � Title$ 
MainWindow��5   
   $��670.045��6   $��1146.171��6.      RightWindowCommands 5      +   %MahApps.Metro.Controls.WindowCommands  5   
   .@�5      �� -   5        $��LoginButton��5   %    � ToolTip$ Login��6   .��5      �� $��15��6   $��15��6'   ��63   ��  �� RelativeSource ��  �� AncestorType$ ButtonL�	 �� Path$	 
Foreground�
 J� OpacityMask
 5      !� 5       j� Stretch$ Uniform=�6&      appbar_user !� Visual# [  5   &   5      5      �� 5      -   5        $��LanguajeButton��5   ]    6   �� *Tile_Config+  TranslationRes �� Source# [ .��5      �� $��15��6   $��15��6'   ��63   ��  �� $ ButtonL�$	 
Foreground�
 5      !� 5      $ Uniform=�6&     appbar_chat# [ 5   &   5      5      5         Flyouts 5!      +   %MahApps.Metro.Controls.FlyoutsControl  5"   
   .@�5#      #   MahApps.Metro.Controls.Flyout  -   5        $��
LogFlayout��5#         Header$  ��60      Position$ Right=�6:   $	��250��6K      Theme$ Accent=�6W   .��5%      ��  �� Orientation$ Vertical=�6   $��10q�64   $��Center=�6@   .+�5&      �� .��5'      �� $ 
Horizontal=�6%   .+�5(      �� 
�������.��5)   "   �� $��30��6,   $��30��67   ��6C   ��  �� $ LabelL�$	 
Foreground�
 5*   &   !� 5+   *   $ Uniform=�66   # [  5,   &   5.   #   5/      �� 50      
�������$��20(�6�   $��Center=�6�   ��6$   �� *Title_Session+# [ 6�   51      52      �� 53      
�������$��0,5,0,5q�6/   $��13(�6@   ��6N   �� *
lab_sesion+# [ �� 54      -   5        $��tbxUser��54      $��5q�6-   6:   G� 55      -   5        $��tbxPassword��55   "   $��5q�65   6B   �� 56      -   5        $��btnLogin��56      $��5q�6-   
�������  MetroFlatButton#��[ 6�   ��68   �� *Btn_InicioSesion+# [ 6�   �� 57      $��Center=�6    �� 58      -   5         �Name$ Btn_PassLost��58   $   .�6S   .�59      �� 
���������6$   �� *Btn_PassLost+# [ 6�   5:      5;      �� 5<      $��Center=�6    �� 5=      -	   5        $ 
Btn_signIn��5=   $   .�6O   .�5>      �� 
���������6$   �� *
Btn_signIn+# [ 6�   5?      5@      5A      5C        5D      -
   5        $��LanguajeFlayout��5D      $ Right=�6|   $	��250��6�   $ Accent=�6�    65   �� *Tile_Config+# [ .��5E      �� $ Vertical=�6   $��15q�64   .+�5F      �� $��15(�6m   
���������6   �� *Lab_LenguajeSettings+# [ 6�   �� 5G      
���������6   �� *Lab_SelectLenguaje+# [ 6�   �� 5H      -   5        $��CbxLanguage��5H      .@�5I      �� -   5        $��EsItem��5I   '    �� 
IsSelected . ��65   �� *CbI_Castellano+# [ 6�   �� 5J      -   5        $��AsItem��5J   '   ��65   �� *Cbl_Asturianu+# [ 6�   5K      5L      5M      5N      .��5P      � .+�5Q   
   �� $ 
Horizontal=�6   $��Right=�6/   .+�5R      �� $��15(�6   $��Right=�6"   $��0 0 5 0q�6>   ��5S      �� *Lab_CurrentUser+# [ 6b   �� 5T      -   5        $��LabUser��5T      $��15(�6#   63   5U      � 5V   
   -   5        $��MainGrid��5V      $��5 35 5 5q�6   5W      5X      5Y      