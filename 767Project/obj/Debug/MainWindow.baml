   M S B A M L   `   `   `  ����E  A767Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:_767Project_767Project  5   	       _767Project.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:_767Project     5   	   -   5          �Name$  MainProg��5   	    � 
ResizeMode$ CanMinimize=�5   	    � Title$ SecureStream��5	   	   $��900.833��6   $��855.5��6/   .��5      � .+�5   
   �� -   5         ��Name$ btnStart��5      $��Start��6#   $
��Left=�63   $��718,744,0,0q�6N   $	��Top=�6c   $	��106��6{    J� RenderTransformOrigin$ -1.027,-2.171&�6�   $��28��6�   6�   �� 5   
   -   5        $ btnPlus��5      $��+��6"   $��0,-1,0,0q�6.   $
��Left=�6A   $��Center=�6\   $��Center=�6�   $��-23,724,0,0q�6�   $	 1,1&�6�   $	��Top=�6�   $��46��6�   $��32��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$	 180Q�6&   T� 5       T� X$	 -16Q�6)    T� Y$	 -17Q�61   5      5      �� 5   
   -   5         ��Name$ 
lblContent��5      $!��Select the Programs to Hide��6$   $
��Left=�6J   $��	10,44,0,0q�6e   $	��Top=�6x   &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 lstProgs��5      $
��Left=�6%   $	��647��6@   $��8,74,0,0q�6M   $	��Top=�6_   $	��816��6w   
   SelectionMode$
 Single=�6�      CanUserReorderColumns .     
IsReadOnly .    Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 25 6)      
IsReadOnly .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *
SelectProg+ �� Mode$ OneWay=�0  *System.Windows.Controls.DataGridTextColumn  5         Header$ Program Name��6%   $	 780 6;      SortDirection$ 	AscendingX�6d    6G   �� *
ProgName+6d     5      $ 
Process ID��6%      
Visibility$ Hidden=�69    6M   �� *procID+5      �� 5   
   -   5        $ 
btnRefresh��5      $	��↻��6%   $��0,-1,0,0q�61   $
��Left=�6D   $��Center=�6_   $��Center=�6�   $��
44,724,0,0q�6�   $	 1,1&�6�   $	��Top=�6�   $��47��6�   $��32��6�   z�5       V� 5!      .~�5"      �� �� 5#      �� 5$      $	 180Q�6&   T� 5%      $	 -16Q�6)   $	 -17Q�61   5&      5(      o� 5)   
   -   5         o�Name$ mnuFile��5)      $
��Left=�6    $��18��6;   $	��Top=�6G   $	��850��6_   $��-1,0,0,0q�6k   .@�5*      m� -   5         m�Name$ mnuFile1��5*      $
��File��6)   $��12(�67   .@�5+      m� -	   5        $ mnuSettings��5+      $��Settings��60   6B   5,      m� 5-      -
   5        $ MnuExit��5-      $
��Exit��6(   $��12(�66   6D   5.      5/      50      