using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

public partial class TrainingContext : DbContext
{
    public TrainingContext()
    {
    }

    public TrainingContext(DbContextOptions<TrainingContext> options)
        : base(options)
    {
    }

    readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Training;Username=postgres;Password=11Murzik");
        optionsBuilder.LogTo(logStream.WriteLine);
//    Trace: используется для вывода наиболее детализированных сообщений. Подобные сообщения могут нести важную информацию о приложении и его строении, поэтому данный уровень лучше использовать при разработке, но никак не при публикации
//    Debug: для вывода информации, которая может быть полезной в процессе разработки и отладки приложения
//Information: уровень сообщений, позволяющий просто отследить поток выполнения приложения
//Warning: используется для вывода сообщений о неожиданных событиях, например, ошибках, которые не влияют не останавливают выполнение приложения, но в то же время должны быть иследованы
//Error: для вывода информации об ошибках и исключениях, которые возникли при текущей операции и которые не могут быть обработаны
//Critical: уровень критических ошибок, которые требуют немедленной реакции -ошибками операционной системы, потерей данных в бд, переполнение памяти диска и т.д.
//None: вывод информации в лог не применяется. все это пишется в параметр функции LogTo()
    }

    public override void Dispose()
    {
        base.Dispose();
        logStream.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await logStream.DisposeAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Age).HasDefaultValue(18);


        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
