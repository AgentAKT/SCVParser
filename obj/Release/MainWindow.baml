   M S B A M L   `   `   `  ���� D  @CSVParser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CSVParser	CSVParser      CSVParser.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:CSVParser       � Title$  	CSVParser��$	��500��$	��750�� � Icon$ /coffeecup.png�� � 
ResizeMode$ NoResize=�.��� .+��� -    ��Name$ pathBtn��$��
Папка��$
��Left=�$��	18,23,0,0q�$	��Top=�$��72��$��14(�$��35���� -   $ collectUIDs��$ ��Обработать UIDы��$
��Left=�$��571,335,0,0q�$	��Top=�$	��131��$��14(�$��35���� -   $ 
saveToFile��$$��Сохранить в файл��$
��Left=�$��571,411,0,0q�$	��Top=�$	��131��$��14(�$��35���� -   $ deleteDuplicates��$'��!Удалить дубликаты��$
��Left=�$��571,373,0,0q�$	��Top=�$	��131��$��14(�$��35���� -    ��Name$ pathTextBox��$
��Left=�$��
158,25,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��544��$��14(�$��32���� -   $ numberOfColumnTextBox��$
��Left=�$��270,128,0,0q�$
 Wrap=�$��1��$	��Top=�$��61��$��20(�$��32���� -   $ numberOfValuesTextBox��$
��Left=�$��270,170,0,0q�$
 Wrap=�$��4��$	��Top=�$��61��$��20(�$��32���� -   $ valuesRandomFromTextBox��$
��Left=�$
 Wrap=�$	��100��$	��Top=�$��64��$��267,215,0,0q�$��20(�$��32���� -	   $ valuesRandomToTextBox��$
��Left=�$��387,214,0,0q�$
 Wrap=�$	��200��$	��Top=�$��60��$��20(�$��32���� -
   $ timeRandomFromTextBox��$
��Left=�$��270,261,0,0q�$
 Wrap=�$��60��$	��Top=�$��61��$��20(�$��33���� -   $ timeRandomToTextBox��$
��Left=�$��385,260,0,0q�$
 Wrap=�$	��120��$	��Top=�$��64��$��20(�$��33���� -    ��Name$ 	pathLabel��$��	Путь:��$
��Left=�$��
106,19,0,0q�$	��Top=�$��14(�$��43��$��57���� -   $ processedFilesLabel��$'��!Найдено .csv файлов:��$
��Left=�$��
523,81,0,0q�$	��Top=�$��35��$	��142���� -   $ processedRowsLabel��$&�� Обработано строк:��$
��Left=�$��523,105,0,0q�$	��Top=�$��36��$	��127���� -   $ collectedUIDsLabel��$��Выбрано UIDов:��$
��Left=�$��523,132,0,0q�$	��Top=�$��36��$	��112���� -   $ processedFilesLabelValue��$��0��$
��Left=�$��
647,83,0,0q�$	��Top=�$��65��$��36���� -   $ processedRowsLabelValue��$��0��$
��Left=�$��647,108,0,0q�$	��Top=�$��65��$��36���� -   $ collectedUIDsLabelValue��$��0��$
��Left=�$��647,132,0,0q�$	��Top=�$��65��$��37���� -   $ numberOfColumnLabel��$ ��Номер столбца:��$
��Left=�$��
18,123,0,0q�$	��Top=�$��20(�$��44��$	��154���� -   $ numberOfValuesLabel��$,��&Количество значений:��$
��Left=�$��
17,166,0,0q�$	��Top=�$��20(�$��43��$	��211���� -   $ valuesRandomLabel��$/��)Значения (рандом):     от��$
��Left=�$��
17,209,0,0q�$	��Top=�$��20(�$��43��$	��245���� -   $ timeRandomLabel��$.��(Время (рандом):          от��$
��Left=�$��
18,255,0,0q�$	��Top=�$��20(�$��43��$	��247���� -   $ valueToLabel��$
��до��$
��Left=�$��338,209,0,0q�$	��Top=�$��20(�$��43��$��34���� -   $ timeToLabel��$
��до��$
��Left=�$��335,254,0,0q�$	��Top=�$��20(�$��43��$��35���� -   $ collectedUIDsLabel_Copy��$*��$Удалено дубликатов:��$
��Left=�$��523,158,0,0q�$	��Top=�$��36��$	��140���� -   $ deletedUIDsLabelValue��$��0��$
��Left=�$��647,159,0,0q�$	��Top=�$��65��$��36���� -   $ collectedUIDsLabel_Copy1��$#��Записано в файл:��$
��Left=�$��523,184,0,0q�$	��Top=�$��36��$	��119���� -   $ writeToFileUIDsLabelValue��$��0��$
��Left=�$��647,185,0,0q�$	��Top=�$��65��$��36��