﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProjetoIntegrador.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201028191610_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjetoIntegrador.Shared.Carrinho", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("descricao_categoria")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("tipo_transacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Estoques");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("bairro")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("cep")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("cidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("cnpj")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("razao_social")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("rua")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.NotaFiscal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("cpf_comprador")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("NotaFiscais");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("NotaFiscalId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("total")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("NotaFiscalId")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImagemProduto")
                        .HasColumnType("longblob");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.ProdutoPedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<decimal>("preco_total_produto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("preco_unitario_produto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("quantidade_produto")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("PedidoId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutoPedidos");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Confirmarsenha")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Carrinho", b =>
                {
                    b.HasOne("ProjetoIntegrador.Shared.Produto", "Produtos")
                        .WithMany("Carrinhos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoIntegrador.Shared.Usuario", "Usuarios")
                        .WithMany("Carrinhos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Estoque", b =>
                {
                    b.HasOne("ProjetoIntegrador.Shared.Produto", "Produto")
                        .WithMany("Estoques")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Pedido", b =>
                {
                    b.HasOne("ProjetoIntegrador.Shared.NotaFiscal", "NotaFiscal")
                        .WithOne("Pedido")
                        .HasForeignKey("ProjetoIntegrador.Shared.Pedido", "NotaFiscalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoIntegrador.Shared.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.Produto", b =>
                {
                    b.HasOne("ProjetoIntegrador.Shared.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoIntegrador.Shared.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoIntegrador.Shared.ProdutoPedido", b =>
                {
                    b.HasOne("ProjetoIntegrador.Shared.Pedido", "Pedido")
                        .WithMany("ProdutoPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoIntegrador.Shared.Produto", "Produto")
                        .WithMany("ProdutoPedido")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
