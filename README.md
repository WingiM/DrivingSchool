﻿# Программа для автоматизации Автошколы

## Содержание

* [Краткое описание предметной области](#краткое-описание-предметной-области)
* [Возможности пользователей системы](#возможности-пользователей-системы)
* [Описание Solution'а](#описание-solutionа)
* [Развертывание системы с использованием Docker](#развертывание-системы-с-использованием-docker)

## Краткое описание предметной области

В текущей реализации автошколы существуют 3 роли:

* [Администратор](#описание-возможностей-администратора) - человек, ответственный за управление
  филиалом автошколы и корректность данных студентов;
* [Учитель](#описание-возможностей-учителя) - человек, проводящий практические занятия со студентами и отслеживающий
  результаты прохождения тестовой части экзамена;
* [Студент](#описание-возможностей-студента) - непосредственный ученик и пользователь автошколы.

Программа предоставялет возможности по автоматизации следующих процессов:

* Запись на практические занятия учителя со студентом:
    * Открытие свободных записей учителем, на которые могут записаться студенты;
    * Создание учителем занятий с конкретными студентами;
    * Просмотр истории проведения занятий, отслеживание откатанных часов;
* Управление пользователями автошколы администратором;
* Прохождение экзаменационных билетов студентами:
    * Прохождение студентами билетов из реального экзамена ГАИ;
    * Отслеживание результатов выполнения билетов как самим студентом, так и учителями.

## Возможности пользователей системы

### Описание возможностей Администратора

* Регистрация новых пользователей в систему;
* Управление существующими пользователями системы:
    * Изменение данных пользователя;
    * Отправка электронных писем с данными для входа;
    * Сброс пароля и изменение электронной почты для входа пользователя;
    * Удаление пользователей из системы.

### Описание возможностей Учителя

* Просмотр результатов студентов в прохождении теоретического экзамена;
* Просмотр занятых и(или) проведенных занятий на конкретных промежутках времени (день/неделя);
* Создание практических занятий:
    * С конкретным студентом;
    * Открытие свободного "окна", в которое студенты могут записаться сами.

### Описание возможностей Студента

* Просмотр проведенных и(или) предстовящий занятий на конкретных промежутках времени (день/неделя);
* Запись на занятие, открытое учителем (не чаще одного раза в день);
* Прохождение теоретических тестов;
* Просмотр результатов прохождения тестов.

## Описание Solution'а

В решении используются 4 проекта, связанных между собой:

### [DrivingSchool.BlazorWebClient](DrivingSchool.BlazorWebClient)

Содержит клиентское приложение Blazor Server (ServerPrerendered). Является зависимым от всех остальных проектов решения,
подключает все необходимые реализации интерфейсов посредством использования
интерфейса [IServiceInstaller](DrivingSchool.BlazorWebClient/ServiceInstallation/IServiceInstaller.cs). Реализации этого интерфейса
подключают необходимые зависимости в IoC-контейнер, как из других проектов решения, так и для внутренних нужд
клиентского приложения. <br/>
Внутренние реализации проектов определяются в самих проектах через класс
ServiceCollectionExtensions
(
см. [DependencyProjectsServiceInstaller](DrivingSchool.BlazorWebClient/ServiceInstallation/ServiceInstallers/DependencyProjectsServiceInstaller.cs)).

В зависимости от состояния системы (таблица system_info.system_state в PostgreSQL) при запуске клиента будут
запущены [программы инициализации](DrivingSchool.BlazorWebClient/HostedServices).
Для корректной работы в папке клиента должна лежать директория `Startups`, с поддиректориями:

* `sqls` - SQL-скрипты, каждый содержащий данные для одного теоритического билета ГАИ (вставки в таблицы билетов,
  вопросов и ответов);
* `images` - изображения в форматах `.png`, `.jpg`, `.jpeg`, имеющие названия соответсвующие названиям картинок для
  вопросов из скриптов в предыдущей папке. Также должен содержаться файл `default.jpg` - изображение, которое
  отображается для вопросов без картинки;

 После корректной инициализации приложения эти папки можно удалить.

### [DrivingSchool.Domain](DrivingSchool.Domain)

Ядро приложения, содержит все основные бизнес-сущности, сервисы для их
взаимодействия, константы, перечисления, основные настройки приложения.

* В папке [Repositories](DrivingSchool.Domain/Repositories) лежат интерфейсы для взаимодействия
  с [проектом извлечения данных](#drivingschooldata);
* В папке [FileSystem](DrivingSchool.Domain/FileSystem) лежат интерфейсы для взаимодействия
  с [файловым хранилищем](#drivingschoolgridfs).

Для конфигурации проекта используются блоки:

* `MailSettings` - конфигурация SMTP-сервера для отправки электронных писем;
* `UserSecrets` - конфигурация сервиса генерации паролей.

### [DrivingSchool.Data](DrivingSchool.Data)

Реализация работы с данными в проекте с использованием [Npgsql](https://www.npgsql.org/) (СУБД PostgreSQL). Для
взаимодействия с базой данных используются библиотеки:

* [EntityFrameworkCore](https://github.com/dotnet/efcore) - хранилище сессионных данных (схема blazor_identity), запись
  и редактирование сущностей, бо́льшая часть запросов на получение данных;
* [Dapper](https://github.com/DapperLib/Dapper) - точечные запросы на получение или изменение небольших данных.

Конфигурация проекта содержится в блоке `ConnectionStrings` файла [appsettings.json](DrivingSchool.BlazorWebClient/appsettings.json).

### [DrivingSchool.GridFS](DrivingSchool.GridFS)

Файловое хранилище, использующее технологию MongoDB GridFS. Используется для хранения изображений для билетов ГАИ и
аватаров пользователей. Все хранящиеся в хранилище картинки выгружаются из него в качестве потоков данных и отображаются
на клиенте без фактического сохранения в оперативной или постоянной памяти.

Конфигурация проекта содержится в блоке `FileSystemSettings` файла [appsettings.json](DrivingSchool.BlazorWebClient/appsettings.json).

## Развертывание системы с использованием Docker

Для каждой релизной версии проекта (ветка master) формируется новый образ
на [Docker Hub](https://hub.docker.com/repository/docker/wingim/driving_school/general). Новый выпущенный образ имеет 2
тега:

* Тег текущей версии (x.x)
* Тег latest

Развернуть систему можно используя Docker Compose, для этого необходимо:

* Скопировать [compose-файл](docker-compose.yml) в директорию
* В одну директорию с файлом поместить дамп базы данных с названием `drivingschool.sql`.
* В эту же директорию разместить файл с названием `.env`, в который поместить переменные:
    * CERTIFICATE_PATH - директория, содержащая SSL-сертификат для сайта;
    * CERTIFICATE_NAME - название файла сертификата;
    * ASPNETCORE_Password - пароль от установленного сертификата;
    * POSTGRES_DATABASE_NAME - название базы данных PostgreSQL;
    * POSTGRES_USER - стандартный пользователь базы данных;
    * POSTGRES_PASSWORD - пароль пользователя базы данных;
    * ASPNETCORE_ENVIRONMENT - определяет, какой конфигурационный файл (appsettings.xxx.json) использовать для
      приложения;
    * MONGO_PORT, POSTGRES_INNER_PORT, POSTGRES_OUTER_PORT - предпочитаемые порты для БД и файлового хранилища;
    * APP_HTTPS_PORT, APP_HTTP_PORT - внешние порты приложения, по которым оно будет доступно вне контейнера (внутри
      контейнера открываются порты 80 для HTTP и 443 для HTTPS);
* Выполнить в этой директории команду `docker compose up -d` для создания и запуска контейнеров.

### Примечания

* Для корректного взаимодействия контейнеров между собой необходимо правильно указывать строки подключения к ним.
  Например, в compose-файле контейнер, содержащий базу данных, называется `storage`, поэтому для соединения приложения с
  ним необходимо в конфигурационном файле использовать примерно такую строку подключения:
    * `"Host=storage;Port=5432;Database=driving_school;Username=postgres;Password=postgres;"`
* Чтобы не запутаться в конфигурационных файлах при локальном запуске и запуске через Docker, можно создать 2 отдельных
  файла для разных конфигураций окружения (для Docker поменять переменную ASPNETCORE_ENVIRONMENT=xxx), например:
    * `appsettings.Docker.json`;
    * `appsettings.Development.json`;
* В качестве SSL-сертификата можно
  использовать [self-signed сертификат](https://learn.microsoft.com/en-us/aspnet/core/security/docker-https#windows-using-linux-containers).
* Все пути в файле `.env` следует оказывать в UNIX-формате (используя знак `/` для разделения директорий) 
