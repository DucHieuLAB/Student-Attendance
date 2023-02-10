using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Assigment.Models
{
    public partial class SchoolDatabaseContext : DbContext
    {
        public SchoolDatabaseContext()
        {
        }

        public SchoolDatabaseContext(DbContextOptions<SchoolDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseDetail> CourseDetails { get; set; }
        public virtual DbSet<SlotAttendance> SlotAttendances { get; set; }
        public virtual DbSet<SlotInformation> SlotInformations { get; set; }
        public virtual DbSet<SlotTime> SlotTimes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudentGroupDetail> StudentGroupDetails { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-1RHVQDS\\HIEUSERVICE;Initial Catalog=SchoolDatabase;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Roleid, e.Id })
                    .HasName("PK_ACCOUNT_STUDENT");

                entity.ToTable("ACCOUNT");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Roleid).HasColumnName("ROLEID");

                entity.Property(e => e.Id)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("PASSWORD");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("COURSE");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COURSE_ID");

                entity.Property(e => e.CourseDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COURSE_DESCRIPTION");

                entity.Property(e => e.CourseEndDate)
                    .HasColumnType("date")
                    .HasColumnName("COURSE_END_DATE");

                entity.Property(e => e.CourseStartDate)
                    .HasColumnType("date")
                    .HasColumnName("COURSE_START_DATE");

                entity.Property(e => e.CourseTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COURSE_TITLE");

                entity.Property(e => e.NumOfSlot).HasColumnName("NUM_OF_SLOT");
            });

            modelBuilder.Entity<CourseDetail>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.SlotId });

                entity.ToTable("COURSE_DETAIL");

                entity.HasIndex(e => e.SlotId, "UQ__COURSE_D__50DD09B7A438C8ED")
                    .IsUnique();

                entity.Property(e => e.CourseId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COURSE_ID");

                entity.Property(e => e.SlotId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SLOT_ID");

                entity.Property(e => e.StudentGroupId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_GROUP_ID");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TEACHER_ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseDetails)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSE_DETAIL_COURSE");

                entity.HasOne(d => d.StudentGroup)
                    .WithMany(p => p.CourseDetails)
                    .HasForeignKey(d => d.StudentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COURSE_DETAIL_STUDENT_GROUP_ID");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.CourseDetails)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEACHER_ID");
            });

            modelBuilder.Entity<SlotAttendance>(entity =>
            {
                entity.HasKey(e => new { e.SlotId, e.StudentId, e.SlotDate, e.SlotTimeName });

                entity.ToTable("SLOT_ATTENDANCE");

                entity.Property(e => e.SlotId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SLOT_ID");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_ID");

                entity.Property(e => e.SlotDate)
                    .HasColumnType("date")
                    .HasColumnName("SLOT_DATE");

                entity.Property(e => e.SlotTimeName).HasColumnName("SLOT_TIME_NAME");

                entity.Property(e => e.Attendance).HasColumnName("ATTENDANCE");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("NOTE");

                entity.Property(e => e.StudentGroupId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_GROUP_ID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SlotAttendances)
                    .HasForeignKey(d => new { d.StudentGroupId, d.StudentId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SLOT_ATTENDANCE");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.SlotAttendances)
                    .HasForeignKey(d => new { d.SlotId, d.SlotDate, d.SlotTimeName })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SLOT_ATTENDANCE_SLOT_DETAI");
            });

            modelBuilder.Entity<SlotInformation>(entity =>
            {
                entity.HasKey(e => new { e.SlotId, e.SlotDate, e.SlotTimeName });

                entity.ToTable("SLOT_INFORMATION");

                entity.Property(e => e.SlotId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SLOT_ID");

                entity.Property(e => e.SlotDate)
                    .HasColumnType("date")
                    .HasColumnName("SLOT_DATE");

                entity.Property(e => e.SlotTimeName).HasColumnName("SLOT_TIME_NAME");

                entity.Property(e => e.SlotNote)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("SLOT_NOTE");

                entity.Property(e => e.SlotStatus).HasColumnName("SLOT_STATUS");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.SlotInformations)
                    .HasPrincipalKey(p => p.SlotId)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SLOT_INFORMATION_SLOT_ID");

                entity.HasOne(d => d.SlotTimeNameNavigation)
                    .WithMany(p => p.SlotInformations)
                    .HasForeignKey(d => d.SlotTimeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SLOT_TIME_NAME");
            });

            modelBuilder.Entity<SlotTime>(entity =>
            {
                entity.HasKey(e => e.SlotTimeName)
                    .HasName("PK_SLOT_TIME_DETAIL");

                entity.ToTable("SLOT_TIME");

                entity.Property(e => e.SlotTimeName).HasColumnName("SLOT_TIME_NAME");

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("END_TIME");

                entity.Property(e => e.SlotDate)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SLOT_DATE");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Rollnumber);

                entity.ToTable("STUDENT");

                entity.Property(e => e.Rollnumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ROLLNUMBER");

                entity.Property(e => e.Address)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("DATE_OF_BIRTH");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.Image)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");
            });

            modelBuilder.Entity<StudentGroup>(entity =>
            {
                entity.ToTable("STUDENT_GROUP");

                entity.Property(e => e.StudentGroupId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_GROUP_ID");

                entity.Property(e => e.StudentGroupName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_GROUP_NAME");
            });

            modelBuilder.Entity<StudentGroupDetail>(entity =>
            {
                entity.HasKey(e => new { e.StudentGroupId, e.StudentId });

                entity.ToTable("STUDENT_GROUP_DETAIL");

                entity.Property(e => e.StudentGroupId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_GROUP_ID");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("STUDENT_ID");

                entity.HasOne(d => d.StudentGroup)
                    .WithMany(p => p.StudentGroupDetails)
                    .HasForeignKey(d => d.StudentGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STUDENT_GROUP_ID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentGroupDetails)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STUDENT_ID");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("TEACHER");

                entity.HasIndex(e => e.Gmail, "UQ__TEACHER__730B0993E9A02D82")
                    .IsUnique();

                entity.Property(e => e.TeacherId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TEACHER_ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("DATE_OF_BIRTH");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.Gmail)
                    .HasMaxLength(255)
                    .HasColumnName("GMAIL");

                entity.Property(e => e.Image)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.LastNamne)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAMNE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
