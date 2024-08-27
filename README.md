# Patient Management System

## Overview

This project is a patient management system built with .NET 8.0, designed to help administrators and assistants manage users, doctors, lab tests, patients, appointments, and lab results. It includes different functionalities, user roles, and screens to streamline the management process efficiently.

## Features

### Login

The system's initial screen is the login page, where users must enter their username and password to access the system. If the credentials are correct, the user is redirected to the Home screen of the patient management system. If the credentials are incorrect, an error message is displayed.

### Home Menu

Upon logging in, the user will see a menu with the following options:

- **User Management** (accessible only to Administrators)
- **Doctor Management** (accessible only to Administrators)
- **Lab Test Management** (accessible only to Administrators)
- **Patient Management** (accessible only to Assistants)
- **Appointment Management** (accessible only to Assistants)
- **Lab Results Management** (accessible only to Assistants)

### User Management (Administrator Only)

Administrators can view a list of all users, showing details such as name, surname, email, username, and user type. They can add a new user by clicking a button that opens a form with fields for name, surname, email, username, password, confirm password, and user type (Administrator or Assistant).

**Validations:**

- Username must be unique.
- Password and confirm password must match.
- All fields are required.

Users can also edit or delete existing users. When editing, the form pre-fills with the user's existing details and requires the same validations. When deleting, a confirmation message is shown before the user is removed.

### Doctor Management (Administrator Only)

Administrators can view a list of all doctors, including details like name, surname, email, phone number, ID, and photo. They can add new doctors using a form with fields for name, surname, email, phone number, ID, and photo (file upload).

**Validations:**

- All fields are required.

Doctors can be edited or deleted with similar functionality to user management, including pre-filled forms for editing and confirmation messages for deletion.

### Lab Test Management (Administrator Only)

Administrators can manage lab tests by viewing a list, adding new tests, editing existing ones, or deleting them.

**Validations:**

- The test name field is required.

### Patient Management (Assistant Only)

Assistants can manage patients by viewing a list of all patients and adding new ones using a form with fields for name, surname, phone, address, ID, birth date, smoking status, allergies, and photo (file upload).

**Validations:**

- All fields are required.

Patients can be edited or deleted with similar functionality to other management sections, including pre-filled forms for editing and confirmation messages for deletion.

### Lab Results Management (Assistant Only)

Assistants can view a list of pending lab results, searchable by patient ID. They can report lab results through a form and mark them as completed. Completed results are removed from the pending list.

### Appointment Management (Assistant Only)

Assistants can manage appointments by viewing a list of all appointments, adding new ones, and updating their status.

**Actions based on appointment status:**

- **Pending:** Ability to select and perform lab tests.
- **Pending Results:** Ability to view and complete lab results.
- **Completed:** View completed lab results.

Appointments can be deleted with a confirmation prompt to ensure the action.

## Getting Started

Follow these instructions to set up the project locally.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- * [Visual Studio](https://visualstudio.microsoft.com/) or any preferred IDE for [ASP.NET](http://asp.net/) Core development

### Installation

1. **Clone the repository**:

```bash
git clone https://github.com/rachelyperezdev/MedSyncApp.git
cd MedSyncApp
```

1. **Set up the database**:
- Update the `appsettings.json` file with your SQL Server connection string.
- Run the following commands to apply migrations and update the database:

```bash
dotnet ef database update
```

1. **Run the application**:

```bash
dotnet run
```
