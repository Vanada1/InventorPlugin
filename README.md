# InventorPlugin
[![CI](https://github.com/Vanada1/InventorPlugin/actions/workflows/CI.yml/badge.svg)](https://github.com/Vanada1/InventorPlugin/actions/workflows/CI.yml)
## Documents
В данном каталоге находится вся документация к **InventorPlugin**:
- ClassDiagram.eapx — находится диаграмма классов приложения;
- Доп задания.docx — дополнительные задания, предложенные студентом;
- ПЗ.docx — пояснительная записка к приложению;
- Проект системы.docx — проект системы приложения;
- ТЗ.docx — техническое задание.

## src
В данном каталоге находится вся кодовая база приложения **InventorPlugin**:
- Builder — проект, в котором находится вся логика построения забора;
- Common — все используемые библиотеке САПР (для Kompas 3D и Inventor);
- CommonTestClass — проект, в котором хранятся вспомогательные классы для модульного тестирования;
- Core — проект бизнес логики приложения;
- FenceBuildingVm — проект модели представления (ViewModel) для главного окна приложения;
- FenceBuldingUI — проект пользовательского интерфейса приложения;
- InventorApi — проект работы с Inventor API;
- KompasApi — проект работы с Kompas 3D API;
- Services — проект, в котором хранятся все сервисы;
- StressTesting — проект для нагрузочного тестирования;
- TestBuilder — проект модульного тестирования проекта **Builder**;
- TestCore — проект модульного тестирования проекта **Core**;
- TestFenceBuildingVm — проект модульного тестирования проекта **FenceBuildingVm**.

## Как проводить нагрузочное тестирование
1. Откройте Solution (**InventorPlugin.sln**);
2. В проекте StressTesting, в классе Program выберете нужный тест САПР (закомментируйте тот метод, который не нужно тестировать, и раскомментировать тот метод, который хотите протестировать)
3. Выберете Solution Configuration Release
4. Нажмите ПКМ по проекту StressTesting и выберете Rebuild
5. Откройте StressTesting.exe по пути `{место репозитория}/InventorPlugin/src/StressTesting/bin/Release/StressTesting.exe`
6. Дождитесь пока САПР не упадет.
7. Результаты тестирования будут находиться в `logKompas 3D.txt` или `logInventor.txt` (зависит от выбранного САПР).
