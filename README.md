## Projeto de Testes End-to-End com Selenium

Projeto de automação de testes end-to-end utilizando Selenium com C#.

### Padrão de Projeto

O padrão adota foi o Page Object para manter a estrutura de testes organizada e reutilizável. Cada página da aplicação é representada por uma classe específica que contém os elementos da página e métodos para interagir com eles.

### Estruturação dos Arquivos

A estrutura de arquivos do projeto é organizada da seguinte maneira:

- **Drivers:** Contém os drivers do Selenium WebDriver.
- **Fixtures:** Dados estáticos utilizados nos testes.
- **Interfaces:** Define os contratos para as Page Objects e outras classes.
- **Pages:** Contém as Page Objects, responsáveis pela interação com elementos das páginas.
- **Reports:** Armazena os relatórios gerados pelos testes.
- **Tests:** Scripts de teste, organizados por funcionalidade ou tipo de teste.
- **Utils:** Classes utilitárias que auxiliam nos testes, como geradores de dados ou métodos auxiliares.
