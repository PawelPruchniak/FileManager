   M S B A M L   `   `   `  ����B  >WpfApp7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TotalCommanderProjectV2TotalCommanderProjectV2  5   	   M+clr-namespace:TotalCommanderProjectV2.ViewsTotalCommanderProjectV2.Views  5   	   (    "TotalCommanderProjectV2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:TotalCommanderProjectV2     5   	   =Views+clr-namespace:TotalCommanderProjectV2.Views     5   	     � Title$  
MainWindow��5	   	   $	��680��6   $	��800��6)   .��5
      � -   5        $��0,0,0,0q�5
      ���	#FF343434 � ColumnDefinitions 5   
   �� 5      $	��15* �6   �� 5      $	��85* �6    � RowDefinitions 5   
   �� 5      $	��95* �6   �� 5      $��5* �6   .+�5   
   � $��0,0,0,0q�6   $��2��6    ���	#FF999999 5      �� 5      $��1* �6#   �� 5      $��1* �6#   .+�5      � -   5        $��leftGrid��5   #   $��0��6   $��0,0,0,0q�63   .+�5      .   (TotalCommanderProjectV2.Views.FilesPanel  -   5          Name$ 	leftPanel��5   #   .   (TotalCommanderProjectV2.Views.FilesPanel   GotFocus$ LeftPanel_GotFocus��6F   $��0��66   $��0,0,0,0q�6d   5      � 5      -   5        $��	rightGrid��5   $   $��1��6   $��0,0,0,0q�65   .+�5        -   5        $ 
rightPanel��5   #   $ RightPanel_GotFocus��6G   $��1��67   $��0,0,0,0q�6f   5      5      �� 5!   
   $��	F1 : Copy��6   $	��0,5q�6&   $��11(�63    �� TextAlignment$ Center=�6A   
�������$��1��6k   $��CharacterEllipsis=�6x   �� 5"   
   $��:��6   $��0,4,0,8q�6    �� 
LineHeight$ 6��6/   
�������$��1��6Q   $��1��6a   $
��Bold"�6n   $��CharacterEllipsis=�6�   5$      5&      