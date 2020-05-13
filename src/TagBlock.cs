using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Common.Mvc
{
	// This class is modeled after System.Web.Mvc.Html.MvcForm

	/// <summary>
	/// Defines functionality for a tag block used by the applicable HtmlHelper extension methods.
	/// </summary>
	public class TagBlock : IDisposable
	{
		// Fields
		private bool _disposed;
		private readonly TextWriter _writer;
		private string _tagName;
		private TagBlock _innerBlock = null;
		private bool _isOpen = false;
		private TagBuilder _builder = null;
		private string[] _cantSelfClose = new string[] { "div" };

		/// <summary>
		/// Get/set the tag block nested in this one.
		/// </summary>
		public TagBlock InnerBlock
		{
			get { return _innerBlock; }
			set { _innerBlock = value; }
		}

		/// <summary>
		/// Creates a new instance of an arbitrary tag.
		/// </summary>
		/// <param name="viewContext">The context of the view.</param>
		/// <param name="tagName">The name of the tag to enclose.</param>
		public TagBlock(ViewContext viewContext, string tagName)
		{
			this._tagName = tagName;
			this._writer = viewContext.Writer;
			_builder = new TagBuilder(_tagName);
		}

		/// <summary>
		/// Adds an attribute to the tag.
		/// </summary>
		/// <param name="name">Attribute name.</param>
		/// <param name="value">Attribute value.</param>
		public void AddAttribute(string name, string value)
		{
			_builder.MergeAttribute(name, value);
		}

		/// <summary>
		/// Writes the tag block's start tag including present attributes.
		/// </summary>
		public void OpenBlock()
		{
			if(!_isOpen)
			{
				_writer.Write(_builder.ToString(TagRenderMode.StartTag));
				_isOpen = true;
			}
		}

		/// <summary>
		/// Writes the start tag for this and any inner blocks.
		/// </summary>
		public void OpenBlockTree()
		{
			OpenBlock();
			if(_innerBlock != null)
			{
				_innerBlock.OpenBlock();
			}
		}

		/// <summary>
		/// Render's the tag block normally.
		/// </summary>
		public void RenderBlock()
		{
			if(_innerBlock == null)
			{
				if(string.IsNullOrEmpty(this.InnerHtml) && !_cantSelfClose.Contains(this._tagName.ToLower()))
				{
					_writer.Write(_builder.ToString(TagRenderMode.SelfClosing));
				}
				else
				{
					_writer.Write(_builder.ToString(TagRenderMode.Normal));
				}
			}
			else
			{
				this.OpenBlock();
				_innerBlock.RenderBlock();
				this.CloseBlock();
			}
		}

		/// <summary>
		/// Writes the end tag of the tag block.
		/// </summary>
		public void CloseBlock()
		{
			if(_isOpen)
			{
				_writer.Write(_builder.ToString(TagRenderMode.EndTag));
				_isOpen = false;
			}
		}

		/// <summary>
		/// Writes the end tag of the tag block and any inner blocks.
		/// </summary>
		public void CloseBlockTree()
		{
			if(_innerBlock != null)
			{
				_innerBlock.CloseBlock();
			}
			CloseBlock();
		}

		/// <summary>
		/// Disposes the tag block.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if(_innerBlock != null)
			{
				_innerBlock.Dispose();
			}
			if(!this._disposed)
			{
				this._disposed = true;
				CloseBlockTree();
			}
		}

		/// <summary>
		/// Gets or sets the inner HTML for the tag.
		/// </summary>
		public string InnerHtml
		{
			get { return _builder.InnerHtml; }
			set { _builder.InnerHtml = value; }
		}

	}
}
