   M S B A M L   `   `   `  ����E  AMinimapGen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MinimapGen
MinimapGen  5   	   !    MinimapGen.colorInputDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   $localclr-namespace:MinimapGen  5   	     � Title$  选择颜色��5	   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z   .��5
      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6    $
��10 5q�6-   .��6;   一层陆地:6B   #  Xceed.Wpf.Toolkit.ColorPicker  5   
   -   5        $��color1��5   8   $��1��6   $��0��6+   $
��5 10q�6F   $	��200��6T      SelectedColor$
 AquaX�5      62   �� 5   
   $��0��6   $��1��6    $
��10 5q�6-   .��6;   二层陆地:6B     5   
   -   5        $��color2��5   8   $��1��6   $��1��6(   $
��5 10q�6F   $	��200��6T   $ 
ChartreuseX�5      68   �� 5   
   $��0��6   $��2��6    $
��10 5q�6-   .��6;   三层陆地:6B     5    
   -   5        $��color3��5    8   $��1��6   $��2��6+   $
��5 10q�6F   $	��200��6T   $ CoralX�5!      63   �� 5"   
   $��0��6   $��3��6    $
��10 5q�6-   .��6;   四层陆地:6B     5#   
   -   5        $��color4��5#   8   $��1��6   $��3��6+   $
��5 10q�6F   $	��200��6T   $ 	GoldenrodX�5$      67   � 5'   
   $��4��6   $��2��6!   $��Right=�65   $��0,15,0,0q�6Q   .+�5(      �� -   5        $��btnDialogOk��5(   &    �� 	IsDefault . $��60��6S   $��0,0,10,0q�6a   .��6s   _OK6x   �� 5)       �� IsCancel . $��60��6%   .��63   	_Cancel6<   5*      5+      5,      