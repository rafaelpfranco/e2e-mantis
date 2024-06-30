## Projeto de Testes End-to-End com Selenium

Projeto de automa��o de testes end-to-end utilizando Selenium com C#.

### Padr�o de Projeto

O padr�o adota foi o Page Object para manter a estrutura de testes organizada e reutiliz�vel. Cada p�gina da aplica��o � representada por uma classe espec�fica que cont�m os elementos da p�gina e m�todos para interagir com eles.

### Estrutura��o dos Arquivos

A estrutura de arquivos do projeto � organizada da seguinte maneira:

- **Drivers:** Cont�m os drivers do Selenium WebDriver.
- **Fixtures:** Dados est�ticos utilizados nos testes.
- **Interfaces:** Define os contratos para as Page Objects e outras classes.
- **Pages:** Cont�m as Page Objects, respons�veis pela intera��o com elementos das p�ginas.
- **Reports:** Armazena os relat�rios gerados pelos testes.
- **Tests:** Scripts de teste, organizados por funcionalidade ou tipo de teste.
- **Utils:** Classes utilit�rias que auxiliam nos testes, como geradores de dados ou m�todos auxiliares.
