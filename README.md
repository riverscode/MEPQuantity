# MEP Quantity
![Revit API](https://img.shields.io/badge/Revit%20API%202022-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgray.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[![YouTube Channel Subscribers](https://img.shields.io/youtube/channel/subscribers/UCB1cArVscPlRRBS7Sa-3Gqw?label=River%20Code&style=social)](https://youtube.com/riverscode?sub_confirmation=1)
![GitHub followers](https://img.shields.io/github/stars/riverscode?style=social)

## Introduction

Aplicativo que permite cuantificar elementos MEP incluido equipos, accesorios, tuberías, ductos, bandejas, etc. a partir de un Generic Model.

Entrada/Input
El usuario tiene un modelo MEP y desea cuantificar elementos a partir de una zona especifica, previamente el usuario debera crear un Generic Model que indentifique la zona donde se cuantificara los elementos.

Proceso
Al utilizar el aplicativo deberá debera cuantificar elementos MEP (equipos, accesorios, tuberías, ductos, bandejas, etc.) 

Output/ Salida
Generación de un reporte en Excel con los metrado de los elementos.

![MEP Quantity](https://user-images.githubusercontent.com/92652351/207517371-6baf6e67-82fe-48dc-88b8-b51c711f1662.png)

## Blog

You can check the blog on the Lambda engineering and innovation page. [Link](https://lambda.com.pe/blog/cuantificar-por-modelo-generico)

## Installation

Standard Revit add-in installation, cf.
[Revit Developer's Guide](https://help.autodesk.com/view/RVT/2022/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html)
[Add-in Registration](https://help.autodesk.com/view/RVT/2022/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Add_in_Registration_html):

- Copy the add-in manifest `*.addin` and .NET assembly `DLL` into the Revit `AddIns` folder and restart Revit
- Click the menu entry under `External Tools` &rarr; `MEPQuantity`

## TODO
- [ ] Loading para exportar la información
- [ ] Mejorar la plantilla de Excel

## Author

Rivers code, [BIM developer](https://riverscode.vercel.app/), blogger on [Lambda Ingeniría e Innovacion](https://lambda.com.pe/blog) and [Youtuber](https://www.youtube.com/c/RiversCode)

## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT).
Please see the [LICENSE](LICENSE) file for full details.
