   M S B A M L   `   `   `  ����  CustomHTTP.Client+    %CustomHTTP.Client.Views.CreateCarView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�6    � AllowsTransparency$
 True��62   
������  � WindowStartupLocation$ CenterOwner=�5   	   $	��300��6-   $	��500��6:   .��5
      �� ���#28282e �� CornerRadius$ 7��6"   ._�5   
   �  � RowDefinitions 5      �� 5      $��50��6    �� 5      .+�5      �� -   5        ���#232123$ 
7, 7, 0, 0��5   *   ._�5      ]�  � 	Resources 5       �� 
TargetType .  *MaterialDesignFloatingActionMiniDarkButton �� BasedOn	 �� Property
 �� Value��%�   '��       7   �� 5      " ��6    8   .�5      �� 	 � ��

 �����6;   �� 5      	 � ��

 ���� 6;   �� 5      	 � ��

 ���� 6<   �� 5      	 � ��$
 40��67   �� 5      	 � ��$
 40��66   5      5       .+�5"      �� -   5        $��0, 0, 10, 0q�5"   L   $��Right=�6�   ��6   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ Close=�6�   �� 5$      $��Create��6    
�������$��Center=�6H   $��Center=�6c   $��50, 0, 0, 0q�6�   $��24(�6�   5&      5'      � 5)      $��1��6    5+      �� 5,      �� 5-      �� 5.      $��70 �6$    � ColumnDefinitions 51      �� 52      �� 53      .+�56      �� 
�������)  #MaterialDesignThemes.Wpf.HintAssist   
Background �#28282e   Hint$ Id��6}   $��10q�6�   ${�False��6�   ��6   �� *	Car.Vin+6�   �� 57      
������� �#28282e$ Vendor��6�   $��10q�6�   $��0��6�   $��1��6�   ��6   �� *
Car.Vendor+6�   �� 58      
������� �#28282e$ Model��6   $��10q�6�   $��1��6�   $��0��6�   ��6   �� *	Car.Model+6�   �� 59      
������� �#28282e$
 Year��6~   $��10q�6�   $��1��6�   $��1��6�   ��6   �� *
Car.Year+6�   �� 5;      -   5        $
��Save��5;   7   $��50, 0q�6F   $��2��6U   $��2��6i   5=      5?      5@      5B      