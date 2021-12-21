# Как проводить нагрузочное тестирование
1. Откройте Solution (**[InventorPlugin.sln](https://github.com/Vanada1/InventorPlugin/blob/develop/src/InventorPlugin.sln)**);
2. В проекте [StressTesting](https://github.com/Vanada1/InventorPlugin/tree/develop/src/StressTesting), в классе Program выберете нужный тест САПР (закомментируйте тот метод, который не нужно тестировать, и раскомментировать тот метод, который хотите протестировать)
3. Выберете Solution Configuration Release
4. Нажмите ПКМ по проекту StressTesting и выберете Rebuild
5. Откройте StressTesting.exe по пути `{место_репозитория}/InventorPlugin/src/StressTesting/bin/Release/StressTesting.exe`
6. Дождитесь пока САПР не упадет.
7. Результаты тестирования будут находиться в `logKompas 3D.txt` или `logInventor.txt` (зависит от выбранного САПР).
