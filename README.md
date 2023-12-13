# Project-SecureSphere
Academic project

In this project we create a software who can do backup for a company, here we will create the Version 1.0
The first version of EasySave is outlined in the following specifications:
### Software Characteristics:

The software is a Console application using .Net Core.
It allows the creation of up to 5 backup jobs.

### Backup Job Definition:
A backup job is defined by:
Backup name
Source directory
Target directory
Type (full, differential)


### Multilingual Support:
The software can be usable by English and French-speaking users.

### Execution Options:
Users can trigger the execution of a specific backup job or execute all jobs sequentially.
The program can be launched via a command line, supporting various execution scenarios.

### Directory Compatibility:
Source and target directories can be located on:
Local disks
External disks
Network drives

### Comprehensive Backup:
All elements within the source directory (files and subdirectories) must be backed up.
Command Line Execution Examples:

Examples include executing specific jobs or ranges of jobs.
###    Daily Log File:

The software must log real-time actions in a daily log file, including:
Timestamp
Backup name
Full source file address (UNC format)
Full destination file address (UNC format)
File size
File transfer time in milliseconds (negative if an error occurs)

### Real-time Status File:

The software must record real-time progress in a single file, including:
Backup job name
Timestamp
Status of the backup job (e.g., Active, Non-Active)
If active:
Total number of eligible files
Size of files to be transferred
Progress
Number of remaining files
Size of remaining files
Current source file being backed up
Destination file address

### File Locations:

File locations for the log and status files must be suitable for client servers, avoiding locations like "c:\temp".
Both log files and configuration files are in JSON format, with line breaks for readability.

### CryptoSoft Utilisation
The process that use the cryptosoft software need to know where is the cryptosoft.exe.
In the CryptoCopy.cs file on the value of CryptoSoftPath note the path of the Cryptosoft.exe like that @"C:\\...\CryptoSoft.exe";.
The ... represent the path on which is the software.

Important Note:

If the software proves satisfactory, a future directive may request the development of version 2.0 with a WPF graphical interface based on the MVVM architecture.



## UML Diagram

We will have four diagram to represent our application:

### Use Case Diagram

### Activities Diagram

### Sequence Diagram

### Class Diagram

## 



