/*!jQuery Migrate v1.4.1 | (c) jQuery Foundation and other contributors | jquery.org/license*/
"undefined" == typeof jQuery.migrateMute && (jQuery.migrateMute = !0),
	function (a, b, c) {
		function d(c) {
			var d = b.console;
			f[c] || (f[c] = !0, a.migrateWarnings.push(c), d && d.warn && !a.migrateMute && (d.warn("JQMIGRATE: " + c), a.migrateTrace && d.trace && d.trace()))
		}

		function e(b, c, e, f) {
			if (Object.defineProperty) try {
				return void Object.defineProperty(b, c, {
					configurable: !0,
					enumerable: !0,
					get: function () {
						return d(f), e
					},
					set: function (a) {
						d(f), e = a
					}
				})
			} catch (g) {}
			a._definePropertyBroken = !0, b[c] = e
		}
		a.migrateVersion = "1.4.1";
		var f = {};
		a.migrateWarnings = [], b.console && b.console.log && b.console.log("JQMIGRATE: Migrate is installed" + (a.migrateMute ? "" : " with logging active") + ", version " + a.migrateVersion), a.migrateTrace === c && (a.migrateTrace = !0), a.migrateReset = function () {
			f = {}, a.migrateWarnings.length = 0
		}, "BackCompat" === document.compatMode && d("jQuery is not compatible with Quirks Mode");
		var g = a("<input/>", {
				size: 1
			}).attr("size") && a.attrFn,
			h = a.attr,
			i = a.attrHooks.value && a.attrHooks.value.get || function () {
				return null
			},
			j = a.attrHooks.value && a.attrHooks.value.set || function () {
				return c
			},
			k = /^(?:input|button)$/i,
			l = /^[238]$/,
			m = /^(?:autofocus|autoplay|async|checked|controls|defer|disabled|hidden|loop|multiple|open|readonly|required|scoped|selected)$/i,
			n = /^(?:checked|selected)$/i;
		e(a, "attrFn", g || {}, "jQuery.attrFn is deprecated"), a.attr = function (b, e, f, i) {
			var j = e.toLowerCase(),
				o = b && b.nodeType;
			return i && (h.length < 4 && d("jQuery.fn.attr( props, pass ) is deprecated"), b && !l.test(o) && (g ? e in g : a.isFunction(a.fn[e]))) ? a(b)[e](f) : ("type" === e && f !== c && k.test(b.nodeName) && b.parentNode && d("Can't change the 'type' of an input or button in IE 6/7/8"), !a.attrHooks[j] && m.test(j) && (a.attrHooks[j] = {
				get: function (b, d) {
					var e, f = a.prop(b, d);
					return f === !0 || "boolean" != typeof f && (e = b.getAttributeNode(d)) && e.nodeValue !== !1 ? d.toLowerCase() : c
				},
				set: function (b, c, d) {
					var e;
					return c === !1 ? a.removeAttr(b, d) : (e = a.propFix[d] || d, e in b && (b[e] = !0), b.setAttribute(d, d.toLowerCase())), d
				}
			}, n.test(j) && d("jQuery.fn.attr('" + j + "') might use property instead of attribute")), h.call(a, b, e, f))
		}, a.attrHooks.value = {
			get: function (a, b) {
				var c = (a.nodeName || "").toLowerCase();
				return "button" === c ? i.apply(this, arguments) : ("input" !== c && "option" !== c && d("jQuery.fn.attr('value') no longer gets properties"), b in a ? a.value : null)
			},
			set: function (a, b) {
				var c = (a.nodeName || "").toLowerCase();
				return "button" === c ? j.apply(this, arguments) : ("input" !== c && "option" !== c && d("jQuery.fn.attr('value', val) no longer sets properties"), void(a.value = b))
			}
		};
		var o, p, q = a.fn.init,
			r = a.find,
			s = a.parseJSON,
			t = /^\s*</,
			u = /\[(\s*[-\w]+\s*)([~|^$*]?=)\s*([-\w#]*?#[-\w#]*)\s*\]/,
			v = /\[(\s*[-\w]+\s*)([~|^$*]?=)\s*([-\w#]*?#[-\w#]*)\s*\]/g,
			w = /^([^<]*)(<[\w\W]+>)([^>]*)$/;
		a.fn.init = function (b, e, f) {
			var g, h;
			return b && "string" == typeof b && !a.isPlainObject(e) && (g = w.exec(a.trim(b))) && g[0] && (t.test(b) || d("$(html) HTML strings must start with '<' character"), g[3] && d("$(html) HTML text after last tag is ignored"), "#" === g[0].charAt(0) && (d("HTML string cannot start with a '#' character"), a.error("JQMIGRATE: Invalid selector string (XSS)")), e && e.context && e.context.nodeType && (e = e.context), a.parseHTML) ? q.call(this, a.parseHTML(g[2], e && e.ownerDocument || e || document, !0), e, f) : (h = q.apply(this, arguments), b && b.selector !== c ? (h.selector = b.selector, h.context = b.context) : (h.selector = "string" == typeof b ? b : "", b && (h.context = b.nodeType ? b : e || document)), h)
		}, a.fn.init.prototype = a.fn, a.find = function (a) {
			var b = Array.prototype.slice.call(arguments);
			if ("string" == typeof a && u.test(a)) try {
				document.querySelector(a)
			} catch (c) {
				a = a.replace(v, function (a, b, c, d) {
					return "[" + b + c + '"' + d + '"]'
				});
				try {
					document.querySelector(a), d("Attribute selector with '#' must be quoted: " + b[0]), b[0] = a
				} catch (e) {
					d("Attribute selector with '#' was not fixed: " + b[0])
				}
			}
			return r.apply(this, b)
		};
		var x;
		for (x in r) Object.prototype.hasOwnProperty.call(r, x) && (a.find[x] = r[x]);
		a.parseJSON = function (a) {
			return a ? s.apply(this, arguments) : (d("jQuery.parseJSON requires a valid JSON string"), null)
		}, a.uaMatch = function (a) {
			a = a.toLowerCase();
			var b = /(chrome)[ \/]([\w.]+)/.exec(a) || /(webkit)[ \/]([\w.]+)/.exec(a) || /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(a) || /(msie) ([\w.]+)/.exec(a) || a.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(a) || [];
			return {
				browser: b[1] || "",
				version: b[2] || "0"
			}
		}, a.browser || (o = a.uaMatch(navigator.userAgent), p = {}, o.browser && (p[o.browser] = !0, p.version = o.version), p.chrome ? p.webkit = !0 : p.webkit && (p.safari = !0), a.browser = p), e(a, "browser", a.browser, "jQuery.browser is deprecated"), a.boxModel = a.support.boxModel = "CSS1Compat" === document.compatMode, e(a, "boxModel", a.boxModel, "jQuery.boxModel is deprecated"), e(a.support, "boxModel", a.support.boxModel, "jQuery.support.boxModel is deprecated"), a.sub = function () {
			function b(a, c) {
				return new b.fn.init(a, c)
			}
			a.extend(!0, b, this), b.superclass = this, b.fn = b.prototype = this(), b.fn.constructor = b, b.sub = this.sub, b.fn.init = function (d, e) {
				var f = a.fn.init.call(this, d, e, c);
				return f instanceof b ? f : b(f)
			}, b.fn.init.prototype = b.fn;
			var c = b(document);
			return d("jQuery.sub() is deprecated"), b
		}, a.fn.size = function () {
			return d("jQuery.fn.size() is deprecated; use the .length property"), this.length
		};
		var y = !1;
		a.swap && a.each(["height", "width", "reliableMarginRight"], function (b, c) {
			var d = a.cssHooks[c] && a.cssHooks[c].get;
			d && (a.cssHooks[c].get = function () {
				var a;
				return y = !0, a = d.apply(this, arguments), y = !1, a
			})
		}), a.swap = function (a, b, c, e) {
			var f, g, h = {};
			y || d("jQuery.swap() is undocumented and deprecated");
			for (g in b) h[g] = a.style[g], a.style[g] = b[g];
			f = c.apply(a, e || []);
			for (g in b) a.style[g] = h[g];
			return f
		}, a.ajaxSetup({
			converters: {
				"text json": a.parseJSON
			}
		});
		var z = a.fn.data;
		a.fn.data = function (b) {
			var e, f, g = this[0];
			return !g || "events" !== b || 1 !== arguments.length || (e = a.data(g, b), f = a._data(g, b), e !== c && e !== f || f === c) ? z.apply(this, arguments) : (d("Use of jQuery.fn.data('events') is deprecated"), f)
		};
		var A = /\/(java|ecma)script/i;
		a.clean || (a.clean = function (b, c, e, f) {
			c = c || document, c = !c.nodeType && c[0] || c, c = c.ownerDocument || c, d("jQuery.clean() is deprecated");
			var g, h, i, j, k = [];
			if (a.merge(k, a.buildFragment(b, c).childNodes), e)
				for (i = function (a) {
						return !a.type || A.test(a.type) ? f ? f.push(a.parentNode ? a.parentNode.removeChild(a) : a) : e.appendChild(a) : void 0
					}, g = 0; null != (h = k[g]); g++) a.nodeName(h, "script") && i(h) || (e.appendChild(h), "undefined" != typeof h.getElementsByTagName && (j = a.grep(a.merge([], h.getElementsByTagName("script")), i), k.splice.apply(k, [g + 1, 0].concat(j)), g += j.length));
			return k
		});
		var B = a.event.add,
			C = a.event.remove,
			D = a.event.trigger,
			E = a.fn.toggle,
			F = a.fn.live,
			G = a.fn.die,
			H = a.fn.load,
			I = "ajaxStart|ajaxStop|ajaxSend|ajaxComplete|ajaxError|ajaxSuccess",
			J = new RegExp("\\b(?:" + I + ")\\b"),
			K = /(?:^|\s)hover(\.\S+|)\b/,
			L = function (b) {
				return "string" != typeof b || a.event.special.hover ? b : (K.test(b) && d("'hover' pseudo-event is deprecated, use 'mouseenter mouseleave'"), b && b.replace(K, "mouseenter$1 mouseleave$1"))
			};
		a.event.props && "attrChange" !== a.event.props[0] && a.event.props.unshift("attrChange", "attrName", "relatedNode", "srcElement"), a.event.dispatch && e(a.event, "handle", a.event.dispatch, "jQuery.event.handle is undocumented and deprecated"), a.event.add = function (a, b, c, e, f) {
			a !== document && J.test(b) && d("AJAX events should be attached to document: " + b), B.call(this, a, L(b || ""), c, e, f)
		}, a.event.remove = function (a, b, c, d, e) {
			C.call(this, a, L(b) || "", c, d, e)
		}, a.each(["load", "unload", "error"], function (b, c) {
			a.fn[c] = function () {
				var a = Array.prototype.slice.call(arguments, 0);
				return "load" === c && "string" == typeof a[0] ? H.apply(this, a) : (d("jQuery.fn." + c + "() is deprecated"), a.splice(0, 0, c), arguments.length ? this.bind.apply(this, a) : (this.triggerHandler.apply(this, a), this))
			}
		}), a.fn.toggle = function (b, c) {
			if (!a.isFunction(b) || !a.isFunction(c)) return E.apply(this, arguments);
			d("jQuery.fn.toggle(handler, handler...) is deprecated");
			var e = arguments,
				f = b.guid || a.guid++,
				g = 0,
				h = function (c) {
					var d = (a._data(this, "lastToggle" + b.guid) || 0) % g;
					return a._data(this, "lastToggle" + b.guid, d + 1), c.preventDefault(), e[d].apply(this, arguments) || !1
				};
			for (h.guid = f; g < e.length;) e[g++].guid = f;
			return this.click(h)
		}, a.fn.live = function (b, c, e) {
			return d("jQuery.fn.live() is deprecated"), F ? F.apply(this, arguments) : (a(this.context).on(b, this.selector, c, e), this)
		}, a.fn.die = function (b, c) {
			return d("jQuery.fn.die() is deprecated"), G ? G.apply(this, arguments) : (a(this.context).off(b, this.selector || "**", c), this)
		}, a.event.trigger = function (a, b, c, e) {
			return c || J.test(a) || d("Global events are undocumented and deprecated"), D.call(this, a, b, c || document, e)
		}, a.each(I.split("|"), function (b, c) {
			a.event.special[c] = {
				setup: function () {
					var b = this;
					return b !== document && (a.event.add(document, c + "." + a.guid, function () {
						a.event.trigger(c, Array.prototype.slice.call(arguments, 1), b, !0)
					}), a._data(this, c, a.guid++)), !1
				},
				teardown: function () {
					return this !== document && a.event.remove(document, c + "." + a._data(this, c)), !1
				}
			}
		}), a.event.special.ready = {
			setup: function () {
				this === document && d("'ready' event is deprecated")
			}
		};
		var M = a.fn.andSelf || a.fn.addBack,
			N = a.fn.find;
		if (a.fn.andSelf = function () {
				return d("jQuery.fn.andSelf() replaced by jQuery.fn.addBack()"), M.apply(this, arguments)
			}, a.fn.find = function (a) {
				var b = N.apply(this, arguments);
				return b.context = this.context, b.selector = this.selector ? this.selector + " " + a : a, b
			}, a.Callbacks) {
			var O = a.Deferred,
				P = [
					["resolve", "done", a.Callbacks("once memory"), a.Callbacks("once memory"), "resolved"],
					["reject", "fail", a.Callbacks("once memory"), a.Callbacks("once memory"), "rejected"],
					["notify", "progress", a.Callbacks("memory"), a.Callbacks("memory")]
				];
			a.Deferred = function (b) {
				var c = O(),
					e = c.promise();
				return c.pipe = e.pipe = function () {
					var b = arguments;
					return d("deferred.pipe() is deprecated"), a.Deferred(function (d) {
						a.each(P, function (f, g) {
							var h = a.isFunction(b[f]) && b[f];
							c[g[1]](function () {
								var b = h && h.apply(this, arguments);
								b && a.isFunction(b.promise) ? b.promise().done(d.resolve).fail(d.reject).progress(d.notify) : d[g[0] + "With"](this === e ? d.promise() : this, h ? [b] : arguments)
							})
						}), b = null
					}).promise()
				}, c.isResolved = function () {
					return d("deferred.isResolved is deprecated"), "resolved" === c.state()
				}, c.isRejected = function () {
					return d("deferred.isRejected is deprecated"), "rejected" === c.state()
				}, b && b.call(c, c), c
			}
		}
	}(jQuery, window);;
(function ($) {
	"use strict";
	var timer = null,
		submit = function () {
			var $button = $(this),
				course_id = $button.attr('data-id'),
				nonce = $button.attr('data-nonce'),
				text = $button.data('text');
			if ($button.hasClass('ajaxload_wishlist')) {
				return;
			}
			$button.addClass('ajaxload_wishlist').prop('disabled', true);
			if (text) {
				$button.html(text);
			}
			$.ajax({
				url: window.location.href,
				type: 'post',
				dataType: 'html',
				data: {
					'lp-ajax': 'toggle_course_wishlist',
					course_id: course_id,
					nonce: nonce
				},
				success: function (response) {
					response = LearnPress.parseJSON(response);
					var $b = $('.learn-press-course-wishlist-button-' + response.course_id),
						$p = $b.closest('[data-context="tab-wishlist"]');
					if ($p.length) {
						$p.fadeOut(function () {
							var $siblings = $p.siblings(),
								$parent = $p.closest('#learn-press-profile-tab-course-wishlist');
							$p.remove();
							if ($siblings.length == 0) {
								$parent.removeClass('has-courses');
							}
						});
					} else {
						$b.removeClass('ajaxload_wishlist').toggleClass('on', response.state == 'on').prop('title', response.title).html(response.button_text);
					}
					$b.prop('disabled', false)
				}
			});
		};
	$(document).on('click', '.course-wishlist', function () {
		timer && clearTimeout(timer);
		timer = setTimeout($.proxy(submit, this), 50);
	});
})(jQuery);
(function () {
	function n(n) {
		function t(t, r, e, u, i, o) {
			for (; i >= 0 && o > i; i += n) {
				var a = u ? u[i] : i;
				e = r(e, t[a], a, t)
			}
			return e
		}
		return function (r, e, u, i) {
			e = b(e, i, 4);
			var o = !k(r) && m.keys(r),
				a = (o || r).length,
				c = n > 0 ? 0 : a - 1;
			return arguments.length < 3 && (u = r[o ? o[c] : c], c += n), t(r, e, u, o, c, a)
		}
	}

	function t(n) {
		return function (t, r, e) {
			r = x(r, e);
			for (var u = O(t), i = n > 0 ? 0 : u - 1; i >= 0 && u > i; i += n)
				if (r(t[i], i, t)) return i;
			return -1
		}
	}

	function r(n, t, r) {
		return function (e, u, i) {
			var o = 0,
				a = O(e);
			if ("number" == typeof i) n > 0 ? o = i >= 0 ? i : Math.max(i + a, o) : a = i >= 0 ? Math.min(i + 1, a) : i + a + 1;
			else if (r && i && a) return i = r(e, u), e[i] === u ? i : -1;
			if (u !== u) return i = t(l.call(e, o, a), m.isNaN), i >= 0 ? i + o : -1;
			for (i = n > 0 ? o : a - 1; i >= 0 && a > i; i += n)
				if (e[i] === u) return i;
			return -1
		}
	}

	function e(n, t) {
		var r = I.length,
			e = n.constructor,
			u = m.isFunction(e) && e.prototype || a,
			i = "constructor";
		for (m.has(n, i) && !m.contains(t, i) && t.push(i); r--;) i = I[r], i in n && n[i] !== u[i] && !m.contains(t, i) && t.push(i)
	}
	var u = this,
		i = u._,
		o = Array.prototype,
		a = Object.prototype,
		c = Function.prototype,
		f = o.push,
		l = o.slice,
		s = a.toString,
		p = a.hasOwnProperty,
		h = Array.isArray,
		v = Object.keys,
		g = c.bind,
		y = Object.create,
		d = function () {},
		m = function (n) {
			return n instanceof m ? n : this instanceof m ? void(this._wrapped = n) : new m(n)
		};
	"undefined" != typeof exports ? ("undefined" != typeof module && module.exports && (exports = module.exports = m), exports._ = m) : u._ = m, m.VERSION = "1.8.3";
	var b = function (n, t, r) {
			if (t === void 0) return n;
			switch (null == r ? 3 : r) {
				case 1:
					return function (r) {
						return n.call(t, r)
					};
				case 2:
					return function (r, e) {
						return n.call(t, r, e)
					};
				case 3:
					return function (r, e, u) {
						return n.call(t, r, e, u)
					};
				case 4:
					return function (r, e, u, i) {
						return n.call(t, r, e, u, i)
					}
			}
			return function () {
				return n.apply(t, arguments)
			}
		},
		x = function (n, t, r) {
			return null == n ? m.identity : m.isFunction(n) ? b(n, t, r) : m.isObject(n) ? m.matcher(n) : m.property(n)
		};
	m.iteratee = function (n, t) {
		return x(n, t, 1 / 0)
	};
	var _ = function (n, t) {
			return function (r) {
				var e = arguments.length;
				if (2 > e || null == r) return r;
				for (var u = 1; e > u; u++)
					for (var i = arguments[u], o = n(i), a = o.length, c = 0; a > c; c++) {
						var f = o[c];
						t && r[f] !== void 0 || (r[f] = i[f])
					}
				return r
			}
		},
		j = function (n) {
			if (!m.isObject(n)) return {};
			if (y) return y(n);
			d.prototype = n;
			var t = new d;
			return d.prototype = null, t
		},
		w = function (n) {
			return function (t) {
				return null == t ? void 0 : t[n]
			}
		},
		A = Math.pow(2, 53) - 1,
		O = w("length"),
		k = function (n) {
			var t = O(n);
			return "number" == typeof t && t >= 0 && A >= t
		};
	m.each = m.forEach = function (n, t, r) {
		t = b(t, r);
		var e, u;
		if (k(n))
			for (e = 0, u = n.length; u > e; e++) t(n[e], e, n);
		else {
			var i = m.keys(n);
			for (e = 0, u = i.length; u > e; e++) t(n[i[e]], i[e], n)
		}
		return n
	}, m.map = m.collect = function (n, t, r) {
		t = x(t, r);
		for (var e = !k(n) && m.keys(n), u = (e || n).length, i = Array(u), o = 0; u > o; o++) {
			var a = e ? e[o] : o;
			i[o] = t(n[a], a, n)
		}
		return i
	}, m.reduce = m.foldl = m.inject = n(1), m.reduceRight = m.foldr = n(-1), m.find = m.detect = function (n, t, r) {
		var e;
		return e = k(n) ? m.findIndex(n, t, r) : m.findKey(n, t, r), e !== void 0 && e !== -1 ? n[e] : void 0
	}, m.filter = m.select = function (n, t, r) {
		var e = [];
		return t = x(t, r), m.each(n, function (n, r, u) {
			t(n, r, u) && e.push(n)
		}), e
	}, m.reject = function (n, t, r) {
		return m.filter(n, m.negate(x(t)), r)
	}, m.every = m.all = function (n, t, r) {
		t = x(t, r);
		for (var e = !k(n) && m.keys(n), u = (e || n).length, i = 0; u > i; i++) {
			var o = e ? e[i] : i;
			if (!t(n[o], o, n)) return !1
		}
		return !0
	}, m.some = m.any = function (n, t, r) {
		t = x(t, r);
		for (var e = !k(n) && m.keys(n), u = (e || n).length, i = 0; u > i; i++) {
			var o = e ? e[i] : i;
			if (t(n[o], o, n)) return !0
		}
		return !1
	}, m.contains = m.includes = m.include = function (n, t, r, e) {
		return k(n) || (n = m.values(n)), ("number" != typeof r || e) && (r = 0), m.indexOf(n, t, r) >= 0
	}, m.invoke = function (n, t) {
		var r = l.call(arguments, 2),
			e = m.isFunction(t);
		return m.map(n, function (n) {
			var u = e ? t : n[t];
			return null == u ? u : u.apply(n, r)
		})
	}, m.pluck = function (n, t) {
		return m.map(n, m.property(t))
	}, m.where = function (n, t) {
		return m.filter(n, m.matcher(t))
	}, m.findWhere = function (n, t) {
		return m.find(n, m.matcher(t))
	}, m.max = function (n, t, r) {
		var e, u, i = -1 / 0,
			o = -1 / 0;
		if (null == t && null != n) {
			n = k(n) ? n : m.values(n);
			for (var a = 0, c = n.length; c > a; a++) e = n[a], e > i && (i = e)
		} else t = x(t, r), m.each(n, function (n, r, e) {
			u = t(n, r, e), (u > o || u === -1 / 0 && i === -1 / 0) && (i = n, o = u)
		});
		return i
	}, m.min = function (n, t, r) {
		var e, u, i = 1 / 0,
			o = 1 / 0;
		if (null == t && null != n) {
			n = k(n) ? n : m.values(n);
			for (var a = 0, c = n.length; c > a; a++) e = n[a], i > e && (i = e)
		} else t = x(t, r), m.each(n, function (n, r, e) {
			u = t(n, r, e), (o > u || 1 / 0 === u && 1 / 0 === i) && (i = n, o = u)
		});
		return i
	}, m.shuffle = function (n) {
		for (var t, r = k(n) ? n : m.values(n), e = r.length, u = Array(e), i = 0; e > i; i++) t = m.random(0, i), t !== i && (u[i] = u[t]), u[t] = r[i];
		return u
	}, m.sample = function (n, t, r) {
		return null == t || r ? (k(n) || (n = m.values(n)), n[m.random(n.length - 1)]) : m.shuffle(n).slice(0, Math.max(0, t))
	}, m.sortBy = function (n, t, r) {
		return t = x(t, r), m.pluck(m.map(n, function (n, r, e) {
			return {
				value: n,
				index: r,
				criteria: t(n, r, e)
			}
		}).sort(function (n, t) {
			var r = n.criteria,
				e = t.criteria;
			if (r !== e) {
				if (r > e || r === void 0) return 1;
				if (e > r || e === void 0) return -1
			}
			return n.index - t.index
		}), "value")
	};
	var F = function (n) {
		return function (t, r, e) {
			var u = {};
			return r = x(r, e), m.each(t, function (e, i) {
				var o = r(e, i, t);
				n(u, e, o)
			}), u
		}
	};
	m.groupBy = F(function (n, t, r) {
		m.has(n, r) ? n[r].push(t) : n[r] = [t]
	}), m.indexBy = F(function (n, t, r) {
		n[r] = t
	}), m.countBy = F(function (n, t, r) {
		m.has(n, r) ? n[r]++ : n[r] = 1
	}), m.toArray = function (n) {
		return n ? m.isArray(n) ? l.call(n) : k(n) ? m.map(n, m.identity) : m.values(n) : []
	}, m.size = function (n) {
		return null == n ? 0 : k(n) ? n.length : m.keys(n).length
	}, m.partition = function (n, t, r) {
		t = x(t, r);
		var e = [],
			u = [];
		return m.each(n, function (n, r, i) {
			(t(n, r, i) ? e : u).push(n)
		}), [e, u]
	}, m.first = m.head = m.take = function (n, t, r) {
		return null == n ? void 0 : null == t || r ? n[0] : m.initial(n, n.length - t)
	}, m.initial = function (n, t, r) {
		return l.call(n, 0, Math.max(0, n.length - (null == t || r ? 1 : t)))
	}, m.last = function (n, t, r) {
		return null == n ? void 0 : null == t || r ? n[n.length - 1] : m.rest(n, Math.max(0, n.length - t))
	}, m.rest = m.tail = m.drop = function (n, t, r) {
		return l.call(n, null == t || r ? 1 : t)
	}, m.compact = function (n) {
		return m.filter(n, m.identity)
	};
	var S = function (n, t, r, e) {
		for (var u = [], i = 0, o = e || 0, a = O(n); a > o; o++) {
			var c = n[o];
			if (k(c) && (m.isArray(c) || m.isArguments(c))) {
				t || (c = S(c, t, r));
				var f = 0,
					l = c.length;
				for (u.length += l; l > f;) u[i++] = c[f++]
			} else r || (u[i++] = c)
		}
		return u
	};
	m.flatten = function (n, t) {
		return S(n, t, !1)
	}, m.without = function (n) {
		return m.difference(n, l.call(arguments, 1))
	}, m.uniq = m.unique = function (n, t, r, e) {
		m.isBoolean(t) || (e = r, r = t, t = !1), null != r && (r = x(r, e));
		for (var u = [], i = [], o = 0, a = O(n); a > o; o++) {
			var c = n[o],
				f = r ? r(c, o, n) : c;
			t ? (o && i === f || u.push(c), i = f) : r ? m.contains(i, f) || (i.push(f), u.push(c)) : m.contains(u, c) || u.push(c)
		}
		return u
	}, m.union = function () {
		return m.uniq(S(arguments, !0, !0))
	}, m.intersection = function (n) {
		for (var t = [], r = arguments.length, e = 0, u = O(n); u > e; e++) {
			var i = n[e];
			if (!m.contains(t, i)) {
				for (var o = 1; r > o && m.contains(arguments[o], i); o++);
				o === r && t.push(i)
			}
		}
		return t
	}, m.difference = function (n) {
		var t = S(arguments, !0, !0, 1);
		return m.filter(n, function (n) {
			return !m.contains(t, n)
		})
	}, m.zip = function () {
		return m.unzip(arguments)
	}, m.unzip = function (n) {
		for (var t = n && m.max(n, O).length || 0, r = Array(t), e = 0; t > e; e++) r[e] = m.pluck(n, e);
		return r
	}, m.object = function (n, t) {
		for (var r = {}, e = 0, u = O(n); u > e; e++) t ? r[n[e]] = t[e] : r[n[e][0]] = n[e][1];
		return r
	}, m.findIndex = t(1), m.findLastIndex = t(-1), m.sortedIndex = function (n, t, r, e) {
		r = x(r, e, 1);
		for (var u = r(t), i = 0, o = O(n); o > i;) {
			var a = Math.floor((i + o) / 2);
			r(n[a]) < u ? i = a + 1 : o = a
		}
		return i
	}, m.indexOf = r(1, m.findIndex, m.sortedIndex), m.lastIndexOf = r(-1, m.findLastIndex), m.range = function (n, t, r) {
		null == t && (t = n || 0, n = 0), r = r || 1;
		for (var e = Math.max(Math.ceil((t - n) / r), 0), u = Array(e), i = 0; e > i; i++, n += r) u[i] = n;
		return u
	};
	var E = function (n, t, r, e, u) {
		if (!(e instanceof t)) return n.apply(r, u);
		var i = j(n.prototype),
			o = n.apply(i, u);
		return m.isObject(o) ? o : i
	};
	m.bind = function (n, t) {
		if (g && n.bind === g) return g.apply(n, l.call(arguments, 1));
		if (!m.isFunction(n)) throw new TypeError("Bind must be called on a function");
		var r = l.call(arguments, 2),
			e = function () {
				return E(n, e, t, this, r.concat(l.call(arguments)))
			};
		return e
	}, m.partial = function (n) {
		var t = l.call(arguments, 1),
			r = function () {
				for (var e = 0, u = t.length, i = Array(u), o = 0; u > o; o++) i[o] = t[o] === m ? arguments[e++] : t[o];
				for (; e < arguments.length;) i.push(arguments[e++]);
				return E(n, r, this, this, i)
			};
		return r
	}, m.bindAll = function (n) {
		var t, r, e = arguments.length;
		if (1 >= e) throw new Error("bindAll must be passed function names");
		for (t = 1; e > t; t++) r = arguments[t], n[r] = m.bind(n[r], n);
		return n
	}, m.memoize = function (n, t) {
		var r = function (e) {
			var u = r.cache,
				i = "" + (t ? t.apply(this, arguments) : e);
			return m.has(u, i) || (u[i] = n.apply(this, arguments)), u[i]
		};
		return r.cache = {}, r
	}, m.delay = function (n, t) {
		var r = l.call(arguments, 2);
		return setTimeout(function () {
			return n.apply(null, r)
		}, t)
	}, m.defer = m.partial(m.delay, m, 1), m.throttle = function (n, t, r) {
		var e, u, i, o = null,
			a = 0;
		r || (r = {});
		var c = function () {
			a = r.leading === !1 ? 0 : m.now(), o = null, i = n.apply(e, u), o || (e = u = null)
		};
		return function () {
			var f = m.now();
			a || r.leading !== !1 || (a = f);
			var l = t - (f - a);
			return e = this, u = arguments, 0 >= l || l > t ? (o && (clearTimeout(o), o = null), a = f, i = n.apply(e, u), o || (e = u = null)) : o || r.trailing === !1 || (o = setTimeout(c, l)), i
		}
	}, m.debounce = function (n, t, r) {
		var e, u, i, o, a, c = function () {
			var f = m.now() - o;
			t > f && f >= 0 ? e = setTimeout(c, t - f) : (e = null, r || (a = n.apply(i, u), e || (i = u = null)))
		};
		return function () {
			i = this, u = arguments, o = m.now();
			var f = r && !e;
			return e || (e = setTimeout(c, t)), f && (a = n.apply(i, u), i = u = null), a
		}
	}, m.wrap = function (n, t) {
		return m.partial(t, n)
	}, m.negate = function (n) {
		return function () {
			return !n.apply(this, arguments)
		}
	}, m.compose = function () {
		var n = arguments,
			t = n.length - 1;
		return function () {
			for (var r = t, e = n[t].apply(this, arguments); r--;) e = n[r].call(this, e);
			return e
		}
	}, m.after = function (n, t) {
		return function () {
			return --n < 1 ? t.apply(this, arguments) : void 0
		}
	}, m.before = function (n, t) {
		var r;
		return function () {
			return --n > 0 && (r = t.apply(this, arguments)), 1 >= n && (t = null), r
		}
	}, m.once = m.partial(m.before, 2);
	var M = !{
			toString: null
		}.propertyIsEnumerable("toString"),
		I = ["valueOf", "isPrototypeOf", "toString", "propertyIsEnumerable", "hasOwnProperty", "toLocaleString"];
	m.keys = function (n) {
		if (!m.isObject(n)) return [];
		if (v) return v(n);
		var t = [];
		for (var r in n) m.has(n, r) && t.push(r);
		return M && e(n, t), t
	}, m.allKeys = function (n) {
		if (!m.isObject(n)) return [];
		var t = [];
		for (var r in n) t.push(r);
		return M && e(n, t), t
	}, m.values = function (n) {
		for (var t = m.keys(n), r = t.length, e = Array(r), u = 0; r > u; u++) e[u] = n[t[u]];
		return e
	}, m.mapObject = function (n, t, r) {
		t = x(t, r);
		for (var e, u = m.keys(n), i = u.length, o = {}, a = 0; i > a; a++) e = u[a], o[e] = t(n[e], e, n);
		return o
	}, m.pairs = function (n) {
		for (var t = m.keys(n), r = t.length, e = Array(r), u = 0; r > u; u++) e[u] = [t[u], n[t[u]]];
		return e
	}, m.invert = function (n) {
		for (var t = {}, r = m.keys(n), e = 0, u = r.length; u > e; e++) t[n[r[e]]] = r[e];
		return t
	}, m.functions = m.methods = function (n) {
		var t = [];
		for (var r in n) m.isFunction(n[r]) && t.push(r);
		return t.sort()
	}, m.extend = _(m.allKeys), m.extendOwn = m.assign = _(m.keys), m.findKey = function (n, t, r) {
		t = x(t, r);
		for (var e, u = m.keys(n), i = 0, o = u.length; o > i; i++)
			if (e = u[i], t(n[e], e, n)) return e
	}, m.pick = function (n, t, r) {
		var e, u, i = {},
			o = n;
		if (null == o) return i;
		m.isFunction(t) ? (u = m.allKeys(o), e = b(t, r)) : (u = S(arguments, !1, !1, 1), e = function (n, t, r) {
			return t in r
		}, o = Object(o));
		for (var a = 0, c = u.length; c > a; a++) {
			var f = u[a],
				l = o[f];
			e(l, f, o) && (i[f] = l)
		}
		return i
	}, m.omit = function (n, t, r) {
		if (m.isFunction(t)) t = m.negate(t);
		else {
			var e = m.map(S(arguments, !1, !1, 1), String);
			t = function (n, t) {
				return !m.contains(e, t)
			}
		}
		return m.pick(n, t, r)
	}, m.defaults = _(m.allKeys, !0), m.create = function (n, t) {
		var r = j(n);
		return t && m.extendOwn(r, t), r
	}, m.clone = function (n) {
		return m.isObject(n) ? m.isArray(n) ? n.slice() : m.extend({}, n) : n
	}, m.tap = function (n, t) {
		return t(n), n
	}, m.isMatch = function (n, t) {
		var r = m.keys(t),
			e = r.length;
		if (null == n) return !e;
		for (var u = Object(n), i = 0; e > i; i++) {
			var o = r[i];
			if (t[o] !== u[o] || !(o in u)) return !1
		}
		return !0
	};
	var N = function (n, t, r, e) {
		if (n === t) return 0 !== n || 1 / n === 1 / t;
		if (null == n || null == t) return n === t;
		n instanceof m && (n = n._wrapped), t instanceof m && (t = t._wrapped);
		var u = s.call(n);
		if (u !== s.call(t)) return !1;
		switch (u) {
			case "[object RegExp]":
			case "[object String]":
				return "" + n == "" + t;
			case "[object Number]":
				return +n !== +n ? +t !== +t : 0 === +n ? 1 / +n === 1 / t : +n === +t;
			case "[object Date]":
			case "[object Boolean]":
				return +n === +t
		}
		var i = "[object Array]" === u;
		if (!i) {
			if ("object" != typeof n || "object" != typeof t) return !1;
			var o = n.constructor,
				a = t.constructor;
			if (o !== a && !(m.isFunction(o) && o instanceof o && m.isFunction(a) && a instanceof a) && "constructor" in n && "constructor" in t) return !1
		}
		r = r || [], e = e || [];
		for (var c = r.length; c--;)
			if (r[c] === n) return e[c] === t;
		if (r.push(n), e.push(t), i) {
			if (c = n.length, c !== t.length) return !1;
			for (; c--;)
				if (!N(n[c], t[c], r, e)) return !1
		} else {
			var f, l = m.keys(n);
			if (c = l.length, m.keys(t).length !== c) return !1;
			for (; c--;)
				if (f = l[c], !m.has(t, f) || !N(n[f], t[f], r, e)) return !1
		}
		return r.pop(), e.pop(), !0
	};
	m.isEqual = function (n, t) {
		return N(n, t)
	}, m.isEmpty = function (n) {
		return null == n ? !0 : k(n) && (m.isArray(n) || m.isString(n) || m.isArguments(n)) ? 0 === n.length : 0 === m.keys(n).length
	}, m.isElement = function (n) {
		return !(!n || 1 !== n.nodeType)
	}, m.isArray = h || function (n) {
		return "[object Array]" === s.call(n)
	}, m.isObject = function (n) {
		var t = typeof n;
		return "function" === t || "object" === t && !!n
	}, m.each(["Arguments", "Function", "String", "Number", "Date", "RegExp", "Error"], function (n) {
		m["is" + n] = function (t) {
			return s.call(t) === "[object " + n + "]"
		}
	}), m.isArguments(arguments) || (m.isArguments = function (n) {
		return m.has(n, "callee")
	}), "function" != typeof /./ && "object" != typeof Int8Array && (m.isFunction = function (n) {
		return "function" == typeof n || !1
	}), m.isFinite = function (n) {
		return isFinite(n) && !isNaN(parseFloat(n))
	}, m.isNaN = function (n) {
		return m.isNumber(n) && n !== +n
	}, m.isBoolean = function (n) {
		return n === !0 || n === !1 || "[object Boolean]" === s.call(n)
	}, m.isNull = function (n) {
		return null === n
	}, m.isUndefined = function (n) {
		return n === void 0
	}, m.has = function (n, t) {
		return null != n && p.call(n, t)
	}, m.noConflict = function () {
		return u._ = i, this
	}, m.identity = function (n) {
		return n
	}, m.constant = function (n) {
		return function () {
			return n
		}
	}, m.noop = function () {}, m.property = w, m.propertyOf = function (n) {
		return null == n ? function () {} : function (t) {
			return n[t]
		}
	}, m.matcher = m.matches = function (n) {
		return n = m.extendOwn({}, n),
			function (t) {
				return m.isMatch(t, n)
			}
	}, m.times = function (n, t, r) {
		var e = Array(Math.max(0, n));
		t = b(t, r, 1);
		for (var u = 0; n > u; u++) e[u] = t(u);
		return e
	}, m.random = function (n, t) {
		return null == t && (t = n, n = 0), n + Math.floor(Math.random() * (t - n + 1))
	}, m.now = Date.now || function () {
		return (new Date).getTime()
	};
	var B = {
			"&": "&amp;",
			"<": "&lt;",
			">": "&gt;",
			'"': "&quot;",
			"'": "&#x27;",
			"`": "&#x60;"
		},
		T = m.invert(B),
		R = function (n) {
			var t = function (t) {
					return n[t]
				},
				r = "(?:" + m.keys(n).join("|") + ")",
				e = RegExp(r),
				u = RegExp(r, "g");
			return function (n) {
				return n = null == n ? "" : "" + n, e.test(n) ? n.replace(u, t) : n
			}
		};
	m.escape = R(B), m.unescape = R(T), m.result = function (n, t, r) {
		var e = null == n ? void 0 : n[t];
		return e === void 0 && (e = r), m.isFunction(e) ? e.call(n) : e
	};
	var q = 0;
	m.uniqueId = function (n) {
		var t = ++q + "";
		return n ? n + t : t
	}, m.templateSettings = {
		evaluate: /<%([\s\S]+?)%>/g,
		interpolate: /<%=([\s\S]+?)%>/g,
		escape: /<%-([\s\S]+?)%>/g
	};
	var K = /(.)^/,
		z = {
			"'": "'",
			"\\": "\\",
			"\r": "r",
			"\n": "n",
			"\u2028": "u2028",
			"\u2029": "u2029"
		},
		D = /\\|'|\r|\n|\u2028|\u2029/g,
		L = function (n) {
			return "\\" + z[n]
		};
	m.template = function (n, t, r) {
		!t && r && (t = r), t = m.defaults({}, t, m.templateSettings);
		var e = RegExp([(t.escape || K).source, (t.interpolate || K).source, (t.evaluate || K).source].join("|") + "|$", "g"),
			u = 0,
			i = "__p+='";
		n.replace(e, function (t, r, e, o, a) {
			return i += n.slice(u, a).replace(D, L), u = a + t.length, r ? i += "'+\n((__t=(" + r + "))==null?'':_.escape(__t))+\n'" : e ? i += "'+\n((__t=(" + e + "))==null?'':__t)+\n'" : o && (i += "';\n" + o + "\n__p+='"), t
		}), i += "';\n", t.variable || (i = "with(obj||{}){\n" + i + "}\n"), i = "var __t,__p='',__j=Array.prototype.join," + "print=function(){__p+=__j.call(arguments,'');};\n" + i + "return __p;\n";
		try {
			var o = new Function(t.variable || "obj", "_", i)
		} catch (a) {
			throw a.source = i, a
		}
		var c = function (n) {
				return o.call(this, n, m)
			},
			f = t.variable || "obj";
		return c.source = "function(" + f + "){\n" + i + "}", c
	}, m.chain = function (n) {
		var t = m(n);
		return t._chain = !0, t
	};
	var P = function (n, t) {
		return n._chain ? m(t).chain() : t
	};
	m.mixin = function (n) {
		m.each(m.functions(n), function (t) {
			var r = m[t] = n[t];
			m.prototype[t] = function () {
				var n = [this._wrapped];
				return f.apply(n, arguments), P(this, r.apply(m, n))
			}
		})
	}, m.mixin(m), m.each(["pop", "push", "reverse", "shift", "sort", "splice", "unshift"], function (n) {
		var t = o[n];
		m.prototype[n] = function () {
			var r = this._wrapped;
			return t.apply(r, arguments), "shift" !== n && "splice" !== n || 0 !== r.length || delete r[0], P(this, r)
		}
	}), m.each(["concat", "join", "slice"], function (n) {
		var t = o[n];
		m.prototype[n] = function () {
			return P(this, t.apply(this._wrapped, arguments))
		}
	}), m.prototype.value = function () {
		return this._wrapped
	}, m.prototype.valueOf = m.prototype.toJSON = m.prototype.value, m.prototype.toString = function () {
		return "" + this._wrapped
	}, "function" == typeof define && define.amd && define("underscore", [], function () {
		return m
	})
}).call(this);
jQuery(document).ready(function () {
	jQuery("#buddypress").on("click", "a.confirm", function () {
		return !!confirm(BP_Confirm.are_you_sure)
	})
});

function member_widget_click_handler() {
	jQuery(".widget div#members-list-options a").on("click", function () {
		var e = this;
		return jQuery(e).addClass("loading"), jQuery(".widget div#members-list-options a").removeClass("selected"), jQuery(this).addClass("selected"), jQuery.post(ajaxurl, {
			action: "widget_members",
			cookie: encodeURIComponent(document.cookie),
			_wpnonce: jQuery("input#_wpnonce-members").val(),
			"max-members": jQuery("input#members_widget_max").val(),
			filter: jQuery(this).attr("id")
		}, function (t) {
			jQuery(e).removeClass("loading"), member_widget_response(t)
		}), !1
	})
}

function member_widget_response(e) {
	e = e.substr(0, e.length - 1), "-1" !== (e = e.split("[[SPLIT]]"))[0] ? jQuery(".widget ul#members-list").fadeOut(200, function () {
		jQuery(".widget ul#members-list").html(e[1]), jQuery(".widget ul#members-list").fadeIn(200)
	}) : jQuery(".widget ul#members-list").fadeOut(200, function () {
		var t = "<p>" + e[1] + "</p>";
		jQuery(".widget ul#members-list").html(t), jQuery(".widget ul#members-list").fadeIn(200)
	})
}
jQuery(document).ready(function () {
	member_widget_click_handler(), "undefined" != typeof wp && wp.customize && wp.customize.selectiveRefresh && wp.customize.selectiveRefresh.bind("partial-content-rendered", function () {
		member_widget_click_handler()
	})
});

function bp_get_querystring(n) {
	var t = location.search.split(n + "=")[1];
	return t ? decodeURIComponent(t.split("&")[0]) : null
};
! function (e) {
	"function" == typeof define && define.amd ? define(["jquery"], e) : e("object" == typeof exports ? require("jquery") : jQuery)
}(function (e) {
	function n(e) {
		return u.raw ? e : encodeURIComponent(e)
	}

	function o(e) {
		return u.raw ? e : decodeURIComponent(e)
	}

	function i(e) {
		return n(u.json ? JSON.stringify(e) : String(e))
	}

	function r(e) {
		0 === e.indexOf('"') && (e = e.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, "\\"));
		try {
			return e = decodeURIComponent(e.replace(c, " ")), u.json ? JSON.parse(e) : e
		} catch (e) {}
	}

	function t(n, o) {
		var i = u.raw ? n : r(n);
		return e.isFunction(o) ? o(i) : i
	}
	var c = /\+/g,
		u = e.cookie = function (r, c, f) {
			if (void 0 !== c && !e.isFunction(c)) {
				if ("number" == typeof (f = e.extend({}, u.defaults, f)).expires) {
					var a = f.expires,
						d = f.expires = new Date;
					d.setTime(+d + 864e5 * a)
				}
				return document.cookie = [n(r), "=", i(c), f.expires ? "; expires=" + f.expires.toUTCString() : "", f.path ? "; path=" + f.path : "", f.domain ? "; domain=" + f.domain : "", f.secure ? "; secure" : ""].join("")
			}
			for (var p = r ? void 0 : {}, s = document.cookie ? document.cookie.split("; ") : [], m = 0, x = s.length; m < x; m++) {
				var v = s[m].split("="),
					k = o(v.shift()),
					l = v.join("=");
				if (r && r === k) {
					p = t(l, c);
					break
				}
				r || void 0 === (l = t(l)) || (p[k] = l)
			}
			return p
		};
	u.defaults = {}, e.removeCookie = function (n, o) {
		return void 0 !== e.cookie(n) && (e.cookie(n, "", e.extend({}, o, {
			expires: -1
		})), !e.cookie(n))
	}
});
! function (e) {
	"function" == typeof define && define.amd ? define(["jquery"], e) : e("object" == typeof exports ? require("jquery") : jQuery)
}(function (e) {
	function t(t) {
		return e.isFunction(t) || "object" == typeof t ? t : {
			top: t,
			left: t
		}
	}
	var n = e.scrollTo = function (t, n, o) {
		return e(window).scrollTo(t, n, o)
	};
	return n.defaults = {
		axis: "xy",
		duration: parseFloat(e.fn.jquery) >= 1.3 ? 0 : 1,
		limit: !0
	}, n.window = function () {
		return e(window)._scrollable()
	}, e.fn._scrollable = function () {
		return this.map(function () {
			var t = this;
			if (!(!t.nodeName || -1 !== e.inArray(t.nodeName.toLowerCase(), ["iframe", "#document", "html", "body"]))) return t;
			var n = (t.contentWindow || t).document || t.ownerDocument || t;
			return /webkit/i.test(navigator.userAgent) || "BackCompat" === n.compatMode ? n.body : n.documentElement
		})
	}, e.fn.scrollTo = function (o, r, i) {
		return "object" == typeof r && (i = r, r = 0), "function" == typeof i && (i = {
			onAfter: i
		}), "max" === o && (o = 9e9), i = e.extend({}, n.defaults, i), r = r || i.duration, i.queue = i.queue && i.axis.length > 1, i.queue && (r /= 2), i.offset = t(i.offset), i.over = t(i.over), this._scrollable().each(function () {
			function s(e) {
				u.animate(l, r, i.easing, e && function () {
					e.call(this, c, i)
				})
			}
			if (null !== o) {
				var a, f = this,
					u = e(f),
					c = o,
					l = {},
					d = u.is("html,body");
				switch (typeof c) {
					case "number":
					case "string":
						if (/^([+-]=?)?\d+(\.\d+)?(px|%)?$/.test(c)) {
							c = t(c);
							break
						}
						if (!(c = d ? e(c) : e(c, this)).length) return;
					case "object":
						(c.is || c.style) && (a = (c = e(c)).offset())
				}
				var m = e.isFunction(i.offset) && i.offset(f, c) || i.offset;
				e.each(i.axis.split(""), function (e, t) {
					var o = "x" === t ? "Left" : "Top",
						r = o.toLowerCase(),
						h = "scroll" + o,
						p = f[h],
						y = n.max(f, t);
					if (a) l[h] = a[r] + (d ? 0 : p - u.offset()[r]), i.margin && (l[h] -= parseInt(c.css("margin" + o)) || 0, l[h] -= parseInt(c.css("border" + o + "Width")) || 0), l[h] += m[r] || 0, i.over[r] && (l[h] += c["x" === t ? "width" : "height"]() * i.over[r]);
					else {
						var b = c[r];
						l[h] = b.slice && "%" === b.slice(-1) ? parseFloat(b) / 100 * y : b
					}
					i.limit && /^\d+$/.test(l[h]) && (l[h] = l[h] <= 0 ? 0 : Math.min(l[h], y)), !e && i.queue && (p !== l[h] && s(i.onAfterFirst), delete l[h])
				}), s(i.onAfter)
			}
		}).end()
	}, n.max = function (t, n) {
		var o = "x" === n ? "Width" : "Height",
			r = "scroll" + o;
		if (!e(t).is("html,body")) return t[r] - e(t)[o.toLowerCase()]();
		var i = "client" + o,
			s = t.ownerDocument.documentElement,
			a = t.ownerDocument.body;
		return Math.max(s[r], a[r]) - Math.min(s[i], a[i])
	}, n
});

function bp_init_activity() {
	void 0 !== jq.cookie("bp-activity-filter") && jq("#activity-filter-select").length && jq('#activity-filter-select select option[value="' + jq.cookie("bp-activity-filter") + '"]').prop("selected", !0), void 0 !== jq.cookie("bp-activity-scope") && jq(".activity-type-tabs").length && (jq(".activity-type-tabs li").each(function () {
		jq(this).removeClass("selected")
	}), jq("#activity-" + jq.cookie("bp-activity-scope") + ", .item-list-tabs li.current").addClass("selected"))
}

function bp_init_objects(e) {
	jq(e).each(function (t) {
		void 0 !== jq.cookie("bp-" + e[t] + "-filter") && jq("#" + e[t] + "-order-select select").length && jq("#" + e[t] + '-order-select select option[value="' + jq.cookie("bp-" + e[t] + "-filter") + '"]').prop("selected", !0), void 0 !== jq.cookie("bp-" + e[t] + "-scope") && jq("div." + e[t]).length && (jq(".item-list-tabs li").each(function () {
			jq(this).removeClass("selected")
		}), jq("#" + e[t] + "-" + jq.cookie("bp-" + e[t] + "-scope") + ", #object-nav li.current").addClass("selected"))
	})
}

function bp_filter_request(e, t, i, a, s, n, r, o, l) {
	if ("activity" === e) return !1;
	null === i && (i = "all"), jq.cookie("bp-" + e + "-scope", i, {
		path: "/",
		secure: "https:" === window.location.protocol
	}), jq.cookie("bp-" + e + "-filter", t, {
		path: "/",
		secure: "https:" === window.location.protocol
	}), jq.cookie("bp-" + e + "-extras", r, {
		path: "/",
		secure: "https:" === window.location.protocol
	}), jq(".item-list-tabs li").each(function () {
		jq(this).removeClass("selected")
	}), jq("#" + e + "-" + i + ", #object-nav li.current").addClass("selected"), jq(".item-list-tabs li.selected").addClass("loading"), jq('.item-list-tabs select option[value="' + t + '"]').prop("selected", !0), "friends" !== e && "group_members" !== e || (e = "members"), bp_ajax_request && bp_ajax_request.abort(), bp_ajax_request = jq.post(ajaxurl, {
		action: e + "_filter",
		cookie: bp_get_cookies(),
		object: e,
		filter: t,
		search_terms: s,
		scope: i,
		page: n,
		extras: r,
		template: l
	}, function (e) {
		if ("pag-bottom" === o && jq("#subnav").length) {
			var t = jq("#subnav").parent();
			jq("html,body").animate({
				scrollTop: t.offset().top
			}, "slow", function () {
				jq(a).fadeOut(100, function () {
					jq(this).html(e), jq(this).fadeIn(100)
				})
			})
		} else jq(a).fadeOut(100, function () {
			jq(this).html(e), jq(this).fadeIn(100)
		});
		jq(".item-list-tabs li.selected").removeClass("loading")
	})
}

function bp_activity_request(e, t) {
	null !== e && jq.cookie("bp-activity-scope", e, {
		path: "/",
		secure: "https:" === window.location.protocol
	}), null !== t && jq.cookie("bp-activity-filter", t, {
		path: "/",
		secure: "https:" === window.location.protocol
	}), jq(".item-list-tabs li").each(function () {
		jq(this).removeClass("selected loading")
	}), jq("#activity-" + e + ", .item-list-tabs li.current").addClass("selected"), jq("#object-nav.item-list-tabs li.selected, div.activity-type-tabs li.selected").addClass("loading"), jq('#activity-filter-select select option[value="' + t + '"]').prop("selected", !0), jq(".widget_bp_activity_widget h2 span.ajax-loader").show(), bp_ajax_request && bp_ajax_request.abort(), bp_ajax_request = jq.post(ajaxurl, {
		action: "activity_widget_filter",
		cookie: bp_get_cookies(),
		_wpnonce_activity_filter: jq("#_wpnonce_activity_filter").val(),
		scope: e,
		filter: t
	}, function (e) {
		jq(".widget_bp_activity_widget h2 span.ajax-loader").hide(), jq("div.activity").fadeOut(100, function () {
			jq(this).html(e.contents), jq(this).fadeIn(100), bp_legacy_theme_hide_comments()
		}), void 0 !== e.feed_url && jq(".directory #subnav li.feed a, .home-page #subnav li.feed a").attr("href", e.feed_url), jq(".item-list-tabs li.selected").removeClass("loading")
	}, "json")
}

function bp_legacy_theme_hide_comments() {
	var e, t, i, a = jq("div.activity-comments");
	if (!a.length) return !1;
	a.each(function () {
		jq(this).children("ul").children("li").length < 5 || (comments_div = jq(this), e = comments_div.parents("#activity-stream > li"), t = jq(this).children("ul").children("li"), i = " ", jq("#" + e.attr("id") + " li").length && (i = jq("#" + e.attr("id") + " li").length), t.each(function (a) {
			a < t.length - 5 && (jq(this).hide(), a || jq(this).before('<li class="show-all"><a href="#' + e.attr("id") + '/show-all/">' + BP_DTheme.show_x_comments.replace("%d", i) + "</a></li>"))
		}))
	})
}

function checkAll() {
	var e, t = document.getElementsByTagName("input");
	for (e = 0; e < t.length; e++) "checkbox" === t[e].type && ("" === $("check_all").checked ? t[e].checked = "" : t[e].checked = "checked")
}

function clear(e) {
	if (e = document.getElementById(e)) {
		var t = e.getElementsByTagName("INPUT"),
			i = e.getElementsByTagName("OPTION"),
			a = 0;
		if (t)
			for (a = 0; a < t.length; a++) t[a].checked = "";
		if (i)
			for (a = 0; a < i.length; a++) i[a].selected = !1
	}
}

function bp_get_cookies() {
	var e, t, i, a, s, n = document.cookie.split(";"),
		r = {};
	for (e = 0; e < n.length; e++) i = (t = n[e]).indexOf("="), a = jq.trim(unescape(t.slice(0, i))), s = unescape(t.slice(i + 1)), 0 === a.indexOf("bp-") && (r[a] = s);
	return encodeURIComponent(jq.param(r))
}

function bp_get_query_var(e, t) {
	var i = {};
	return (t = void 0 === t ? location.search.substr(1).split("&") : t.split("?")[1].split("&")).forEach(function (e) {
		i[e.split("=")[0]] = e.split("=")[1] && decodeURIComponent(e.split("=")[1])
	}), !(!i.hasOwnProperty(e) || null == i[e]) && i[e]
}
var jq = jQuery,
	bp_ajax_request = null,
	newest_activities = "",
	activity_last_recorded = 0;
jq(document).ready(function () {
	var e = 1;
	bp_init_activity();
	var t = ["members", "groups", "blogs", "group_members"],
		i = jq("#whats-new");
	if (bp_init_objects(t), i.length && bp_get_querystring("r")) {
		var a = i.val();
		jq("#whats-new-options").slideDown(), i.animate({
			height: "3.8em"
		}), jq.scrollTo(i, 500, {
			offset: -125,
			easing: "swing"
		}), i.val("").focus().val(a)
	} else jq("#whats-new-options").hide();
	if (i.focus(function () {
			jq("#whats-new-options").slideDown(), jq(this).animate({
				height: "3.8em"
			}), jq("#aw-whats-new-submit").prop("disabled", !1), jq(this).parent().addClass("active"), jq("#whats-new-content").addClass("active");
			var e = jq("form#whats-new-form"),
				t = jq("#activity-all");
			e.hasClass("submitted") && e.removeClass("submitted"), t.length && (t.hasClass("selected") ? "-1" !== jq("#activity-filter-select select").val() && (jq("#activity-filter-select select").val("-1"), jq("#activity-filter-select select").trigger("change")) : (jq("#activity-filter-select select").val("-1"), t.children("a").trigger("click")))
		}), jq("#whats-new-form").on("focusout", function (e) {
			var t = jq(this);
			setTimeout(function () {
				if (!t.find(":hover").length) {
					if ("" !== i.val()) return;
					i.animate({
						height: "2.2em"
					}), jq("#whats-new-options").slideUp(), jq("#aw-whats-new-submit").prop("disabled", !0), jq("#whats-new-content").removeClass("active"), i.parent().removeClass("active")
				}
			}, 0)
		}), jq("#aw-whats-new-submit").on("click", function () {
			var e, t = 0,
				i = jq(this),
				a = i.closest("form#whats-new-form"),
				s = {};
			return jq.each(a.serializeArray(), function (e, t) {
				"_" !== t.name.substr(0, 1) && "whats-new" !== t.name.substr(0, 9) && (s[t.name] ? jq.isArray(s[t.name]) ? s[t.name].push(t.value) : s[t.name] = new Array(s[t.name], t.value) : s[t.name] = t.value)
			}), a.find("*").each(function () {
				(jq.nodeName(this, "textarea") || jq.nodeName(this, "input")) && jq(this).prop("disabled", !0)
			}), jq("div.error").remove(), i.addClass("loading"), i.prop("disabled", !0), a.addClass("submitted"), object = "", item_id = jq("#whats-new-post-in").val(), content = jq("#whats-new").val(), firstrow = jq("#buddypress ul.activity-list li").first(), activity_row = firstrow, timestamp = null, firstrow.length && (activity_row.hasClass("load-newest") && (activity_row = firstrow.next()), timestamp = activity_row.prop("class").match(/date-recorded-([0-9]+)/)), timestamp && (t = timestamp[1]), item_id > 0 && (object = jq("#whats-new-post-object").val()), e = jq.extend({
				action: "post_update",
				cookie: bp_get_cookies(),
				_wpnonce_post_update: jq("#_wpnonce_post_update").val(),
				content: content,
				object: object,
				item_id: item_id,
				since: t,
				_bp_as_nonce: jq("#_bp_as_nonce").val() || ""
			}, s), jq.post(ajaxurl, e, function (e) {
				if (a.find("*").each(function () {
						(jq.nodeName(this, "textarea") || jq.nodeName(this, "input")) && jq(this).prop("disabled", !1)
					}), e[0] + e[1] === "-1") a.prepend(e.substr(2, e.length)), jq("#" + a.attr("id") + " div.error").hide().fadeIn(200);
				else {
					if (0 === jq("ul.activity-list").length && (jq("div.error").slideUp(100).remove(), jq("#message").slideUp(100).remove(), jq("div.activity").append('<ul id="activity-stream" class="activity-list item-list">')), firstrow.hasClass("load-newest") && firstrow.remove(), jq("#activity-stream").prepend(e), t || jq("#activity-stream li:first").addClass("new-update just-posted"), 0 !== jq("#latest-update").length) {
						var i = jq("#activity-stream li.new-update .activity-content .activity-inner p").html(),
							s = jq("#activity-stream li.new-update .activity-content .activity-header p a.view").attr("href"),
							n = "";
						"" !== jq("#activity-stream li.new-update .activity-content .activity-inner p").text() && (n = i + " "), n += '<a href="' + s + '" rel="nofollow">' + BP_DTheme.view + "</a>", jq("#latest-update").slideUp(300, function () {
							jq("#latest-update").html(n), jq("#latest-update").slideDown(300)
						})
					}
					jq("li.new-update").hide().slideDown(300), jq("li.new-update").removeClass("new-update"), jq("#whats-new").val(""), a.get(0).reset(), newest_activities = "", activity_last_recorded = 0
				}
				jq("#whats-new-options").slideUp(), jq("#whats-new-form textarea").animate({
					height: "2.2em"
				}), jq("#aw-whats-new-submit").prop("disabled", !0).removeClass("loading"), jq("#whats-new-content").removeClass("active")
			}), !1
		}), jq("div.activity-type-tabs").on("click", function (e) {
			var t, i, a = jq(e.target).parent();
			if ("STRONG" === e.target.nodeName || "SPAN" === e.target.nodeName) a = a.parent();
			else if ("A" !== e.target.nodeName) return !1;
			return t = a.attr("id").substr(9, a.attr("id").length), i = jq("#activity-filter-select select").val(), "mentions" === t && jq("#" + a.attr("id") + " a strong").remove(), bp_activity_request(t, i), !1
		}), jq("#activity-filter-select select").change(function () {
			var e, t = jq("div.activity-type-tabs li.selected"),
				i = jq(this).val();
			return e = t.length ? t.attr("id").substr(9, t.attr("id").length) : null, bp_activity_request(e, i), !1
		}), jq("div.activity").on("click", function (t) {
			var i, a, s, n, r, o, l, c, d, p, u = jq(t.target);
			return u.hasClass("fav") || u.hasClass("unfav") ? !u.hasClass("loading") && (i = u.hasClass("fav") ? "fav" : "unfav", a = u.closest(".activity-item"), s = a.attr("id").substr(9, a.attr("id").length), l = bp_get_query_var("_wpnonce", u.attr("href")), u.addClass("loading"), jq.post(ajaxurl, {
				action: "activity_mark_" + i,
				cookie: bp_get_cookies(),
				id: s,
				nonce: l
			}, function (e) {
				u.removeClass("loading"), u.fadeOut(200, function () {
					jq(this).html(e), jq(this).attr("title", "fav" === i ? BP_DTheme.remove_fav : BP_DTheme.mark_as_fav), jq(this).fadeIn(200)
				}), "fav" === i ? (jq(".item-list-tabs #activity-favs-personal-li").length || (jq(".item-list-tabs #activity-favorites").length || jq(".item-list-tabs ul #activity-mentions").before('<li id="activity-favorites"><a href="#">' + BP_DTheme.my_favs + " <span>0</span></a></li>"), jq(".item-list-tabs ul #activity-favorites span").html(Number(jq(".item-list-tabs ul #activity-favorites span").html()) + 1)), u.removeClass("fav"), u.addClass("unfav")) : (u.removeClass("unfav"), u.addClass("fav"), jq(".item-list-tabs ul #activity-favorites span").html(Number(jq(".item-list-tabs ul #activity-favorites span").html()) - 1), Number(jq(".item-list-tabs ul #activity-favorites span").html()) || (jq(".item-list-tabs ul #activity-favorites").hasClass("selected") && bp_activity_request(null, null), jq(".item-list-tabs ul #activity-favorites").remove())), "activity-favorites" === jq(".item-list-tabs li.selected").attr("id") && u.closest(".activity-item").slideUp(100)
			}), !1) : u.hasClass("delete-activity") ? (n = u.parents("div.activity ul li"), r = n.attr("id").substr(9, n.attr("id").length), o = u.attr("href"), l = o.split("_wpnonce="), c = n.prop("class").match(/date-recorded-([0-9]+)/), l = l[1], u.addClass("loading"), jq.post(ajaxurl, {
				action: "delete_activity",
				cookie: bp_get_cookies(),
				id: r,
				_wpnonce: l
			}, function (e) {
				e[0] + e[1] === "-1" ? (n.prepend(e.substr(2, e.length)), n.children("#message").hide().fadeIn(300)) : (n.slideUp(300), c && activity_last_recorded === c[1] && (newest_activities = "", activity_last_recorded = 0))
			}), !1) : u.hasClass("spam-activity") ? (n = u.parents("div.activity ul li"), c = n.prop("class").match(/date-recorded-([0-9]+)/), u.addClass("loading"), jq.post(ajaxurl, {
				action: "bp_spam_activity",
				cookie: encodeURIComponent(document.cookie),
				id: n.attr("id").substr(9, n.attr("id").length),
				_wpnonce: u.attr("href").split("_wpnonce=")[1]
			}, function (e) {
				e[0] + e[1] === "-1" ? (n.prepend(e.substr(2, e.length)), n.children("#message").hide().fadeIn(300)) : (n.slideUp(300), c && activity_last_recorded === c[1] && (newest_activities = "", activity_last_recorded = 0))
			}), !1) : u.parent().hasClass("load-more") ? (bp_ajax_request && bp_ajax_request.abort(), jq("#buddypress li.load-more").addClass("loading"), d = e + 1, p = [], jq(".activity-list li.just-posted").each(function () {
				p.push(jq(this).attr("id").replace("activity-", ""))
			}), load_more_args = {
				action: "activity_get_older_updates",
				cookie: bp_get_cookies(),
				page: d,
				exclude_just_posted: p.join(",")
			}, load_more_search = bp_get_querystring("s"), load_more_search && (load_more_args.search_terms = load_more_search), bp_ajax_request = jq.post(ajaxurl, load_more_args, function (t) {
				jq("#buddypress li.load-more").removeClass("loading"), e = d, jq("#buddypress ul.activity-list").append(t.contents), u.parent().hide()
			}, "json"), !1) : void(u.parent().hasClass("load-newest") && (t.preventDefault(), u.parent().hide(), activity_html = jq.parseHTML(newest_activities), jq.each(activity_html, function (e, t) {
				"LI" === t.nodeName && jq(t).hasClass("just-posted") && jq("#" + jq(t).attr("id")).length && jq("#" + jq(t).attr("id")).remove()
			}), jq("#buddypress ul.activity-list").prepend(newest_activities), newest_activities = ""))
		}), jq("div.activity").on("click", ".activity-read-more a", function (e) {
			var t, i, a = jq(e.target),
				s = a.parent().attr("id").split("-"),
				n = s[3],
				r = s[0];
			return t = "acomment" === r ? "acomment-content" : "activity-inner", i = jq("#" + r + "-" + n + " ." + t + ":first"), jq(a).addClass("loading"), jq.post(ajaxurl, {
				action: "get_single_activity_content",
				activity_id: n
			}, function (e) {
				jq(i).slideUp(300).html(e).slideDown(300)
			}), !1
		}), jq("form.ac-form").hide(), jq(".activity-comments").length && bp_legacy_theme_hide_comments(), jq("div.activity").on("click", function (e) {
			var t, i, a, s, n, r, o, l, c, d, p, u, m, h, j, v = jq(e.target);
			return v.hasClass("acomment-reply") || v.parent().hasClass("acomment-reply") ? (v.parent().hasClass("acomment-reply") && (v = v.parent()), t = v.attr("id"), i = t.split("-"), a = i[2], s = v.attr("href").substr(10, v.attr("href").length), (n = jq("#ac-form-" + a)).css("display", "none"), n.removeClass("root"), jq(".ac-form").hide(), n.children("div").each(function () {
				jq(this).hasClass("error") && jq(this).hide()
			}), "comment" !== i[1] ? jq("#acomment-" + s).append(n) : jq("#activity-" + a + " .activity-comments").append(n), n.parent().hasClass("activity-comments") && n.addClass("root"), n.slideDown(200), jq.scrollTo(n, 500, {
				offset: -100,
				easing: "swing"
			}), jq("#ac-form-" + i[2] + " textarea").focus(), !1) : "ac_form_submit" === v.attr("name") ? (n = v.parents("form"), r = n.parent(), o = n.attr("id").split("-"), l = r.hasClass("activity-comments") ? o[2] : r.attr("id").split("-")[1], content = jq("#" + n.attr("id") + " textarea"), jq("#" + n.attr("id") + " div.error").hide(), v.addClass("loading").prop("disabled", !0), content.addClass("loading").prop("disabled", !0), c = {
				action: "new_activity_comment",
				cookie: bp_get_cookies(),
				_wpnonce_new_activity_comment: jq("#_wpnonce_new_activity_comment").val(),
				comment_id: l,
				form_id: o[2],
				content: content.val()
			}, (d = jq("#_bp_as_nonce_" + l).val()) && (c["_bp_as_nonce_" + l] = d), jq.post(ajaxurl, c, function (e) {
				if (v.removeClass("loading"), content.removeClass("loading"), e[0] + e[1] === "-1") n.append(jq(e.substr(2, e.length)).hide().fadeIn(200));
				else {
					var t = n.parent();
					n.fadeOut(200, function () {
						0 === t.children("ul").length && (t.hasClass("activity-comments") ? t.prepend("<ul></ul>") : t.append("<ul></ul>"));
						var i = jq.trim(e);
						t.children("ul").append(jq(i).hide().fadeIn(200)), n.children("textarea").val(""), t.parent().addClass("has-comments")
					}), jq("#" + n.attr("id") + " textarea").val(""), u = Number(jq("#activity-" + o[2] + " a.acomment-reply span").html()) + 1, jq("#activity-" + o[2] + " a.acomment-reply span").html(u), (p = t.parents(".activity-comments").find(".show-all a")) && p.html(BP_DTheme.show_x_comments.replace("%d", u))
				}
				jq(v).prop("disabled", !1), jq(content).prop("disabled", !1)
			}), !1) : v.hasClass("acomment-delete") ? (m = v.attr("href"), h = v.parent().parent(), n = h.parents("div.activity-comments").children("form"), j = m.split("_wpnonce="), j = j[1], l = m.split("cid="), l = l[1].split("&"), l = l[0], v.addClass("loading"), jq(".activity-comments ul .error").remove(), h.parents(".activity-comments").append(n), jq.post(ajaxurl, {
				action: "delete_activity_comment",
				cookie: bp_get_cookies(),
				_wpnonce: j,
				id: l
			}, function (e) {
				if (e[0] + e[1] === "-1") h.prepend(jq(e.substr(2, e.length)).hide().fadeIn(200));
				else {
					var t, i, a, s = jq("#" + h.attr("id") + " ul").children("li"),
						n = 0;
					jq(s).each(function () {
						jq(this).is(":hidden") || n++
					}), h.fadeOut(200, function () {
						h.remove()
					}), i = (t = jq("#" + h.parents("#activity-stream > li").attr("id") + " a.acomment-reply span")).html() - (1 + n), t.html(i), (a = h.parents(".activity-comments").find(".show-all a")) && a.html(BP_DTheme.show_x_comments.replace("%d", i)), 0 === i && jq(h.parents("#activity-stream > li")).removeClass("has-comments")
				}
			}), !1) : v.hasClass("spam-activity-comment") ? (m = v.attr("href"), h = v.parent().parent(), v.addClass("loading"), jq(".activity-comments ul div.error").remove(), h.parents(".activity-comments").append(h.parents(".activity-comments").children("form")), jq.post(ajaxurl, {
				action: "bp_spam_activity_comment",
				cookie: encodeURIComponent(document.cookie),
				_wpnonce: m.split("_wpnonce=")[1],
				id: m.split("cid=")[1].split("&")[0]
			}, function (e) {
				if (e[0] + e[1] === "-1") h.prepend(jq(e.substr(2, e.length)).hide().fadeIn(200));
				else {
					var t, i = jq("#" + h.attr("id") + " ul").children("li"),
						a = 0;
					jq(i).each(function () {
						jq(this).is(":hidden") || a++
					}), h.fadeOut(200), t = h.parents("#activity-stream > li"), jq("#" + t.attr("id") + " a.acomment-reply span").html(jq("#" + t.attr("id") + " a.acomment-reply span").html() - (1 + a))
				}
			}), !1) : v.parent().hasClass("show-all") ? (v.parent().addClass("loading"), setTimeout(function () {
				v.parent().parent().children("li").fadeIn(200, function () {
					v.parent().remove()
				})
			}, 600), !1) : v.hasClass("ac-reply-cancel") ? (jq(v).closest(".ac-form").slideUp(200), !1) : void 0
		}), jq(document).keydown(function (e) {
			(e = e || window.event).target ? element = e.target : e.srcElement && (element = e.srcElement), 3 === element.nodeType && (element = element.parentNode), !0 !== e.ctrlKey && !0 !== e.altKey && !0 !== e.metaKey && 27 === (e.keyCode ? e.keyCode : e.which) && "TEXTAREA" === element.tagName && jq(element).hasClass("ac-input") && jq(element).parent().parent().parent().slideUp(200)
		}), jq(".dir-search, .groups-members-search").on("click", function (e) {
			if (!jq(this).hasClass("no-ajax")) {
				var t, i, a, s, n = jq(e.target);
				return "submit" === n.attr("type") ? (t = jq(".item-list-tabs li.selected").attr("id").split("-"), i = t[0], a = null, s = n.parent().find("#" + i + "_search").val(), "groups-members-search" === e.currentTarget.className && (i = "group_members", a = "groups/single/members"), bp_filter_request(i, jq.cookie("bp-" + i + "-filter"), jq.cookie("bp-" + i + "-scope"), "div." + i, s, 1, jq.cookie("bp-" + i + "-extras"), null, a), !1) : void 0
			}
		}), jq("div.item-list-tabs").on("click", function (e) {
			if (jq("body").hasClass("type") && jq("body").hasClass("directory") && jq(this).addClass("no-ajax"), !jq(this).hasClass("no-ajax") && !jq(e.target).hasClass("no-ajax")) {
				var t, i, a, s, n, r = "SPAN" === e.target.nodeName ? e.target.parentNode : e.target,
					o = jq(r).parent();
				return "LI" !== o[0].nodeName || o.hasClass("last") ? void 0 : (t = o.attr("id").split("-"), "activity" !== (i = t[0]) && (a = t[1], s = jq("#" + i + "-order-select select").val(), n = jq("#" + i + "_search").val(), bp_filter_request(i, s, a, "div." + i, n, 1, jq.cookie("bp-" + i + "-extras")), !1))
			}
		}), jq("li.filter select").change(function () {
			var e, t, i, a, s, n, r, o;
			return e = jq(jq(".item-list-tabs li.selected").length ? ".item-list-tabs li.selected" : this), t = e.attr("id").split("-"), i = t[0], a = t[1], s = jq(this).val(), n = !1, r = null, jq(".dir-search input").length && (n = jq(".dir-search input").val()), (o = jq(".groups-members-search input")).length && (n = o.val(), i = "members", a = "groups"), "members" === i && "groups" === a && (i = "group_members", r = "groups/single/members"), "friends" === i && (i = "members"), bp_filter_request(i, s, a, "div." + i, n, 1, jq.cookie("bp-" + i + "-extras"), null, r), !1
		}), jq("#buddypress").on("click", function (e) {
			var t, i, a, s, n, r, o, l, c, d = jq(e.target);
			return !!d.hasClass("button") || (d.parent().parent().hasClass("pagination") && !d.parent().parent().hasClass("no-ajax") ? !d.hasClass("dots") && !d.hasClass("current") && (t = jq(jq(".item-list-tabs li.selected").length ? ".item-list-tabs li.selected" : "li.filter select"), i = t.attr("id").split("-"), a = i[0], s = !1, n = jq(d).closest(".pagination-links").attr("id"), r = null, jq("div.dir-search input").length && (s = !(s = jq(".dir-search input")).val() && bp_get_querystring(s.attr("name")) ? jq(".dir-search input").prop("placeholder") : s.val()), o = jq(d).hasClass("next") || jq(d).hasClass("prev") ? jq(".pagination span.current").html() : jq(d).html(), o = Number(o.replace(/\D/g, "")), jq(d).hasClass("next") ? o++ : jq(d).hasClass("prev") && o--, (l = jq(".groups-members-search input")).length && (s = l.val(), a = "members"), "members" === a && "groups" === i[1] && (a = "group_members", r = "groups/single/members"), "admin" === a && jq("body").hasClass("membership-requests") && (a = "requests"), c = -1 !== n.indexOf("pag-bottom") ? "pag-bottom" : null, bp_filter_request(a, jq.cookie("bp-" + a + "-filter"), jq.cookie("bp-" + a + "-scope"), "div." + a, s, o, jq.cookie("bp-" + a + "-extras"), c, r), !1) : void 0)
		}), jq("#send-invite-form").on("click", "#invite-list input", function () {
			var e, t, i = jq("#send-invite-form > .invite").length;
			jq(".ajax-loader").toggle(), i && jq(this).parents("ul").find("input").prop("disabled", !0), e = jq(this).val(), t = !0 === jq(this).prop("checked") ? "invite" : "uninvite", i || jq(".item-list-tabs li.selected").addClass("loading"), jq.post(ajaxurl, {
				action: "groups_invite_user",
				friend_action: t,
				cookie: bp_get_cookies(),
				_wpnonce: jq("#_wpnonce_invite_uninvite_user").val(),
				friend_id: e,
				group_id: jq("#group_id").val()
			}, function (a) {
				jq("#message") && jq("#message").hide(), i ? bp_filter_request("invite", "bp-invite-filter", "bp-invite-scope", "div.invite", !1, 1, "", "", "") : (jq(".ajax-loader").toggle(), "invite" === t ? jq("#friend-list").append(a) : "uninvite" === t && jq("#friend-list li#uid-" + e).remove(), jq(".item-list-tabs li.selected").removeClass("loading"))
			})
		}), jq("#send-invite-form").on("click", "a.remove", function () {
			var e = jq("#send-invite-form > .invite").length,
				t = jq(this).attr("id");
			return jq(".ajax-loader").toggle(), t = t.split("-"), t = t[1], jq.post(ajaxurl, {
				action: "groups_invite_user",
				friend_action: "uninvite",
				cookie: bp_get_cookies(),
				_wpnonce: jq("#_wpnonce_invite_uninvite_user").val(),
				friend_id: t,
				group_id: jq("#group_id").val()
			}, function (i) {
				e ? bp_filter_request("invite", "bp-invite-filter", "bp-invite-scope", "div.invite", !1, 1, "", "", "") : (jq(".ajax-loader").toggle(), jq("#friend-list #uid-" + t).remove(), jq("#invite-list #f-" + t).prop("checked", !1))
			}), !1
		}), jq(".visibility-toggle-link").on("click", function (e) {
			e.preventDefault(), jq(this).attr("aria-expanded", "true").parent().hide().addClass("field-visibility-settings-hide").siblings(".field-visibility-settings").show().addClass("field-visibility-settings-open")
		}), jq(".field-visibility-settings-close").on("click", function (e) {
			e.preventDefault(), jq(".visibility-toggle-link").attr("aria-expanded", "false");
			var t = jq(this).parent(),
				i = t.find("input:checked").parent().text();
			t.hide().removeClass("field-visibility-settings-open").siblings(".field-visibility-settings-toggle").children(".current-visibility-level").text(i).end().show().removeClass("field-visibility-settings-hide")
		}), jq("#profile-edit-form input:not(:submit), #profile-edit-form textarea, #profile-edit-form select, #signup_form input:not(:submit), #signup_form textarea, #signup_form select").change(function () {
			var e = !0;
			jq("#profile-edit-form input:submit, #signup_form input:submit").on("click", function () {
				e = !1
			}), window.onbeforeunload = function (t) {
				if (e) return BP_DTheme.unsaved_changes
			}
		}), jq("#friend-list a.accept, #friend-list a.reject").on("click", function () {
			var e, t = jq(this),
				i = jq(this).parents("#friend-list li"),
				a = jq(this).parents("li div.action"),
				s = i.attr("id").substr(11, i.attr("id").length),
				n = t.attr("href").split("_wpnonce=")[1];
			return !jq(this).hasClass("accepted") && !jq(this).hasClass("rejected") && (jq(this).hasClass("accept") ? (e = "accept_friendship", a.children("a.reject").css("visibility", "hidden")) : (e = "reject_friendship", a.children("a.accept").css("visibility", "hidden")), t.addClass("loading"), jq.post(ajaxurl, {
				action: e,
				cookie: bp_get_cookies(),
				id: s,
				_wpnonce: n
			}, function (e) {
				t.removeClass("loading"), e[0] + e[1] === "-1" ? (i.prepend(e.substr(2, e.length)), i.children("#message").hide().fadeIn(200)) : t.fadeOut(100, function () {
					jq(this).hasClass("accept") ? (a.children("a.reject").hide(), jq(this).html(BP_DTheme.accepted).contents().unwrap()) : (a.children("a.accept").hide(), jq(this).html(BP_DTheme.rejected).contents().unwrap())
				})
			}), !1)
		}), jq("#members-dir-list, #members-group-list, #item-header").on("click", ".friendship-button a", function () {
			jq(this).parent().addClass("loading");
			var e = jq(this).attr("id"),
				t = jq(this).attr("href"),
				i = jq(this);
			return e = e.split("-"), e = e[1], t = t.split("?_wpnonce="), t = t[1].split("&"), t = t[0], jq.post(ajaxurl, {
				action: "addremove_friend",
				cookie: bp_get_cookies(),
				fid: e,
				_wpnonce: t
			}, function (e) {
				var t = i.attr("rel");
				parentdiv = i.parent(), "add" === t ? jq(parentdiv).fadeOut(200, function () {
					parentdiv.removeClass("add_friend"), parentdiv.removeClass("loading"), parentdiv.addClass("pending_friend"), parentdiv.fadeIn(200).html(e)
				}) : "remove" === t && jq(parentdiv).fadeOut(200, function () {
					parentdiv.removeClass("remove_friend"), parentdiv.removeClass("loading"), parentdiv.addClass("add"), parentdiv.fadeIn(200).html(e)
				})
			}), !1
		}), jq("#buddypress").on("click", ".group-button .leave-group", function () {
			if (!1 === confirm(BP_DTheme.leave_group_confirm)) return !1
		}), jq("#groups-dir-list").on("click", ".group-button a", function () {
			var e = jq(this).parent().attr("id"),
				t = jq(this).attr("href"),
				i = jq(this);
			return e = e.split("-"), e = e[1], t = t.split("?_wpnonce="), t = t[1].split("&"), t = t[0], (!i.hasClass("leave-group") || !1 !== confirm(BP_DTheme.leave_group_confirm)) && (jq.post(ajaxurl, {
				action: "joinleave_group",
				cookie: bp_get_cookies(),
				gid: e,
				_wpnonce: t
			}, function (e) {
				var t = i.parent();
				jq("body.directory").length ? jq(t).fadeOut(200, function () {
					t.fadeIn(200).html(e);
					var a = jq("#groups-personal span"),
						s = 1;
					i.hasClass("leave-group") ? (t.hasClass("hidden") && t.closest("li").slideUp(200), s = 0) : i.hasClass("request-membership") && (s = !1), a.length && !1 !== s && (s ? a.text(1 + (a.text() >> 0)) : a.text((a.text() >> 0) - 1))
				}) : window.location.reload()
			}), !1)
		}), jq("#groups-list li.hidden").each(function () {
			"none" === jq(this).css("display") && jq(this).css("cssText", "display: list-item !important")
		}), jq("#buddypress").on("click", ".pending", function () {
			return !1
		}), jq("body").hasClass("register")) {
		var s = jq("#signup_with_blog");
		s.prop("checked") || jq("#blog-details").toggle(), s.change(function () {
			jq("#blog-details").toggle()
		})
	}
	jq(".message-search").on("click", function (e) {
		if (!jq(this).hasClass("no-ajax")) {
			var t, i = jq(e.target);
			return "submit" === i.attr("type") || "button" === i.attr("type") ? (t = "messages", bp_filter_request(t, jq.cookie("bp-" + t + "-filter"), jq.cookie("bp-" + t + "-scope"), "div." + t, jq("#messages_search").val(), 1, jq.cookie("bp-" + t + "-extras")), !1) : void 0
		}
	}), jq("#send_reply_button").click(function () {
		var e = jq("#messages_order").val() || "ASC",
			t = jq("#message-recipients").offset(),
			i = jq("#send_reply_button");
		return jq(i).addClass("loading").prop("disabled", !0), jq.post(ajaxurl, {
			action: "messages_send_reply",
			cookie: bp_get_cookies(),
			_wpnonce: jq("#send_message_nonce").val(),
			content: jq("#message_content").val(),
			send_to: jq("#send_to").val(),
			subject: jq("#subject").val(),
			thread_id: jq("#thread_id").val()
		}, function (a) {
			a[0] + a[1] === "-1" ? jq("#send-reply").prepend(a.substr(2, a.length)) : (jq("#send-reply #message").remove(), jq("#message_content").val(""), "ASC" === e ? jq("#send-reply").before(a) : (jq("#message-recipients").after(a), jq(window).scrollTop(t.top)), jq(".new-message").hide().slideDown(200, function () {
				jq(".new-message").removeClass("new-message")
			})), jq(i).removeClass("loading").prop("disabled", !1)
		}), !1
	}), jq("body.messages #item-body div.messages").on("change", "#message-type-select", function () {
		var e = this.value,
			t = jq('td input[type="checkbox"]'),
			i = "checked";
		switch (t.each(function (e) {
			t[e].checked = ""
		}), e) {
			case "unread":
				t = jq('tr.unread td input[type="checkbox"]');
				break;
			case "read":
				t = jq('tr.read td input[type="checkbox"]');
				break;
			case "":
				i = ""
		}
		t.each(function (e) {
			t[e].checked = i
		})
	}), jq("#select-all-messages").click(function (e) {
		this.checked ? jq(".message-check").each(function () {
			this.checked = !0
		}) : jq(".message-check").each(function () {
			this.checked = !1
		})
	}), jq("#messages-bulk-manage").attr("disabled", "disabled"), jq("#messages-select").on("change", function () {
		jq("#messages-bulk-manage").attr("disabled", jq(this).val().length <= 0)
	}), starAction = function () {
		var e = jq(this);
		return jq.post(ajaxurl, {
			action: "messages_star",
			message_id: e.data("message-id"),
			star_status: e.data("star-status"),
			nonce: e.data("star-nonce"),
			bulk: e.data("star-bulk")
		}, function (t) {
			1 === parseInt(t, 10) && ("unstar" === e.data("star-status") ? (e.data("star-status", "star"), e.removeClass("message-action-unstar").addClass("message-action-star"), e.find(".bp-screen-reader-text").text(BP_PM_Star.strings.text_star), 1 === BP_PM_Star.is_single_thread ? e.attr("data-bp-tooltip", BP_PM_Star.strings.title_star) : e.attr("data-bp-tooltip", BP_PM_Star.strings.title_star_thread)) : (e.data("star-status", "unstar"), e.removeClass("message-action-star").addClass("message-action-unstar"), e.find(".bp-screen-reader-text").text(BP_PM_Star.strings.text_unstar), 1 === BP_PM_Star.is_single_thread ? e.attr("data-bp-tooltip", BP_PM_Star.strings.title_unstar) : e.attr("data-bp-tooltip", BP_PM_Star.strings.title_unstar_thread)))
		}), !1
	}, jq("#message-threads").on("click", "td.thread-star a", starAction), jq("#message-thread").on("click", ".message-star-actions a", starAction), jq("#message-threads td.bulk-select-check :checkbox").on("change", function () {
		var e = jq(this),
			t = e.closest("tr").find(".thread-star a");
		e.prop("checked") ? "unstar" === t.data("star-status") ? BP_PM_Star.star_counter++ : BP_PM_Star.unstar_counter++ : "unstar" === t.data("star-status") ? BP_PM_Star.star_counter-- : BP_PM_Star.unstar_counter--, BP_PM_Star.star_counter > 0 && 0 === parseInt(BP_PM_Star.unstar_counter, 10) ? jq('option[value="star"]').hide() : jq('option[value="star"]').show(), BP_PM_Star.unstar_counter > 0 && 0 === parseInt(BP_PM_Star.star_counter, 10) ? jq('option[value="unstar"]').hide() : jq('option[value="unstar"]').show()
	}), jq("#select-all-notifications").click(function (e) {
		this.checked ? jq(".notification-check").each(function () {
			this.checked = !0
		}) : jq(".notification-check").each(function () {
			this.checked = !1
		})
	}), jq("#notification-bulk-manage").attr("disabled", "disabled"), jq("#notification-select").on("change", function () {
		jq("#notification-bulk-manage").attr("disabled", jq(this).val().length <= 0)
	}), jq("#close-notice").on("click", function () {
		return jq(this).addClass("loading"), jq("#sidebar div.error").remove(), jq.post(ajaxurl, {
			action: "messages_close_notice",
			notice_id: jq(".notice").attr("rel").substr(2, jq(".notice").attr("rel").length),
			nonce: jq("#close-notice-nonce").val()
		}, function (e) {
			jq("#close-notice").removeClass("loading"), e[0] + e[1] === "-1" ? (jq(".notice").prepend(e.substr(2, e.length)), jq("#sidebar div.error").hide().fadeIn(200)) : jq(".notice").slideUp(100)
		}), !1
	}), jq("#wp-admin-bar ul.main-nav li, #nav li").mouseover(function () {
		jq(this).addClass("sfhover")
	}), jq("#wp-admin-bar ul.main-nav li, #nav li").mouseout(function () {
		jq(this).removeClass("sfhover")
	}), jq("#wp-admin-bar-logout, a.logout").on("click", function () {
		jq.removeCookie("bp-activity-scope", {
			path: "/",
			secure: "https:" === window.location.protocol
		}), jq.removeCookie("bp-activity-filter", {
			path: "/",
			secure: "https:" === window.location.protocol
		}), jq.removeCookie("bp-activity-oldestpage", {
			path: "/",
			secure: "https:" === window.location.protocol
		});
		var e = ["members", "groups", "blogs", "forums"];
		jq(e).each(function (t) {
			jq.removeCookie("bp-" + e[t] + "-scope", {
				path: "/",
				secure: "https:" === window.location.protocol
			}), jq.removeCookie("bp-" + e[t] + "-filter", {
				path: "/",
				secure: "https:" === window.location.protocol
			}), jq.removeCookie("bp-" + e[t] + "-extras", {
				path: "/",
				secure: "https:" === window.location.protocol
			})
		})
	}), jq("body").hasClass("no-js") && jq("body").attr("class", jq("body").attr("class").replace(/no-js/, "js")), "undefined" != typeof wp && void 0 !== wp.heartbeat && void 0 !== BP_DTheme.pulse && (wp.heartbeat.interval(Number(BP_DTheme.pulse)), jq.fn.extend({
		"heartbeat-send": function () {
			return this.bind("heartbeat-send.buddypress")
		}
	}));
	var n = 0;
	jq(document).on("heartbeat-send.buddypress", function (e, t) {
		n = 0, jq("#buddypress ul.activity-list li").first().prop("id") && (timestamp = jq("#buddypress ul.activity-list li").first().prop("class").match(/date-recorded-([0-9]+)/), timestamp && (n = timestamp[1])), (0 === activity_last_recorded || Number(n) > activity_last_recorded) && (activity_last_recorded = Number(n)), t.bp_activity_last_recorded = activity_last_recorded, last_recorded_search = bp_get_querystring("s"), last_recorded_search && (t.bp_activity_last_recorded_search_terms = last_recorded_search)
	}), jq(document).on("heartbeat-tick", function (e, t) {
		t.bp_activity_newest_activities && (newest_activities = t.bp_activity_newest_activities.activities + newest_activities, activity_last_recorded = Number(t.bp_activity_newest_activities.last_recorded), jq("#buddypress ul.activity-list li").first().hasClass("load-newest") || jq("#buddypress ul.activity-list").prepend('<li class="load-newest"><a href="#newest">' + BP_DTheme.newest + "</a></li>"))
	})
});
(function (a) {
	if (typeof define === "function" && define.amd && define.amd.jQuery) {
		define(["jquery"], a)
	} else {
		a(jQuery)
	}
}(function (f) {
	var y = "1.6.9",
		p = "left",
		o = "right",
		e = "up",
		x = "down",
		c = "in",
		A = "out",
		m = "none",
		s = "auto",
		l = "swipe",
		t = "pinch",
		B = "tap",
		j = "doubletap",
		b = "longtap",
		z = "hold",
		E = "horizontal",
		u = "vertical",
		i = "all",
		r = 10,
		g = "start",
		k = "move",
		h = "end",
		q = "cancel",
		a = "ontouchstart" in window,
		v = window.navigator.msPointerEnabled && !window.navigator.pointerEnabled,
		d = window.navigator.pointerEnabled || window.navigator.msPointerEnabled,
		C = "TouchSwipe";
	var n = {
		fingers: 1,
		threshold: 75,
		cancelThreshold: null,
		pinchThreshold: 20,
		maxTimeThreshold: null,
		fingerReleaseThreshold: 250,
		longTapThreshold: 500,
		doubleTapThreshold: 200,
		swipe: null,
		swipeLeft: null,
		swipeRight: null,
		swipeUp: null,
		swipeDown: null,
		swipeStatus: null,
		pinchIn: null,
		pinchOut: null,
		pinchStatus: null,
		click: null,
		tap: null,
		doubleTap: null,
		longTap: null,
		hold: null,
		triggerOnTouchEnd: true,
		triggerOnTouchLeave: false,
		allowPageScroll: "auto",
		fallbackToMouseEvents: true,
		excludedElements: "label, button, input, select, textarea, a, .noSwipe",
		preventDefaultEvents: true
	};
	f.fn.swipetp = function (H) {
		var G = f(this),
			F = G.data(C);
		if (F && typeof H === "string") {
			if (F[H]) {
				return F[H].apply(this, Array.prototype.slice.call(arguments, 1))
			} else {
				f.error("Method " + H + " does not exist on jQuery.swipetp")
			}
		} else {
			if (!F && (typeof H === "object" || !H)) {
				return w.apply(this, arguments)
			}
		}
		return G
	};
	f.fn.swipetp.version = y;
	f.fn.swipetp.defaults = n;
	f.fn.swipetp.phases = {
		PHASE_START: g,
		PHASE_MOVE: k,
		PHASE_END: h,
		PHASE_CANCEL: q
	};
	f.fn.swipetp.directions = {
		LEFT: p,
		RIGHT: o,
		UP: e,
		DOWN: x,
		IN: c,
		OUT: A
	};
	f.fn.swipetp.pageScroll = {
		NONE: m,
		HORIZONTAL: E,
		VERTICAL: u,
		AUTO: s
	};
	f.fn.swipetp.fingers = {
		ONE: 1,
		TWO: 2,
		THREE: 3,
		ALL: i
	};

	function w(F) {
		if (F && (F.allowPageScroll === undefined && (F.swipe !== undefined || F.swipeStatus !== undefined))) {
			F.allowPageScroll = m
		}
		if (F.click !== undefined && F.tap === undefined) {
			F.tap = F.click
		}
		if (!F) {
			F = {}
		}
		F = f.extend({}, f.fn.swipetp.defaults, F);
		return this.each(function () {
			var H = f(this);
			var G = H.data(C);
			if (!G) {
				G = new D(this, F);
				H.data(C, G)
			}
		})
	}

	function D(a5, aw) {
		var aA = (a || d || !aw.fallbackToMouseEvents),
			K = aA ? (d ? (v ? "MSPointerDown" : "pointerdown") : "touchstart") : "mousedown",
			az = aA ? (d ? (v ? "MSPointerMove" : "pointermove") : "touchmove") : "mousemove",
			V = aA ? (d ? (v ? "MSPointerUp" : "pointerup") : "touchend") : "mouseup",
			T = aA ? null : "mouseleave",
			aE = (d ? (v ? "MSPointerCancel" : "pointercancel") : "touchcancel");
		var ah = 0,
			aQ = null,
			ac = 0,
			a2 = 0,
			a0 = 0,
			H = 1,
			ar = 0,
			aK = 0,
			N = null;
		var aS = f(a5);
		var aa = "start";
		var X = 0;
		var aR = null;
		var U = 0,
			a3 = 0,
			a6 = 0,
			ae = 0,
			O = 0;
		var aX = null,
			ag = null;
		try {
			aS.bind(K, aO);
			aS.bind(aE, ba)
		} catch (al) {
			f.error("events not supported " + K + "," + aE + " on jQuery.swipetp")
		}
		this.enable = function () {
			aS.bind(K, aO);
			aS.bind(aE, ba);
			return aS
		};
		this.disable = function () {
			aL();
			return aS
		};
		this.destroy = function () {
			aL();
			aS.data(C, null);
			aS = null
		};
		this.option = function (bd, bc) {
			if (aw[bd] !== undefined) {
				if (bc === undefined) {
					return aw[bd]
				} else {
					aw[bd] = bc
				}
			} else {
				f.error("Option " + bd + " does not exist on jQuery.swipetp.options")
			}
			return null
		};

		function aO(be) {
			if (aC()) {
				return
			}
			if (f(be.target).closest(aw.excludedElements, aS).length > 0) {
				return
			}
			var bf = be.originalEvent ? be.originalEvent : be;
			var bd, bg = bf.touches,
				bc = bg ? bg[0] : bf;
			aa = g;
			if (bg) {
				X = bg.length
			} else {
				be.preventDefault()
			}
			ah = 0;
			aQ = null;
			aK = null;
			ac = 0;
			a2 = 0;
			a0 = 0;
			H = 1;
			ar = 0;
			aR = ak();
			N = ab();
			S();
			if (!bg || (X === aw.fingers || aw.fingers === i) || aY()) {
				aj(0, bc);
				U = au();
				if (X == 2) {
					aj(1, bg[1]);
					a2 = a0 = av(aR[0].start, aR[1].start)
				}
				if (aw.swipeStatus || aw.pinchStatus) {
					bd = P(bf, aa)
				}
			} else {
				bd = false
			}
			if (bd === false) {
				aa = q;
				P(bf, aa);
				return bd
			} else {
				if (aw.hold) {
					ag = setTimeout(f.proxy(function () {
						aS.trigger("hold", [bf.target]);
						if (aw.hold) {
							bd = aw.hold.call(aS, bf, bf.target)
						}
					}, this), aw.longTapThreshold)
				}
				ap(true)
			}
			return null
		}

		function a4(bf) {
			var bi = bf.originalEvent ? bf.originalEvent : bf;
			if (aa === h || aa === q || an()) {
				return
			}
			var be, bj = bi.touches,
				bd = bj ? bj[0] : bi;
			var bg = aI(bd);
			a3 = au();
			if (bj) {
				X = bj.length
			}
			if (aw.hold) {
				clearTimeout(ag)
			}
			aa = k;
			if (X == 2) {
				if (a2 == 0) {
					aj(1, bj[1]);
					a2 = a0 = av(aR[0].start, aR[1].start)
				} else {
					aI(bj[1]);
					a0 = av(aR[0].end, aR[1].end);
					aK = at(aR[0].end, aR[1].end)
				}
				H = a8(a2, a0);
				ar = Math.abs(a2 - a0)
			}
			if ((X === aw.fingers || aw.fingers === i) || !bj || aY()) {
				aQ = aM(bg.start, bg.end);
				am(bf, aQ);
				ah = aT(bg.start, bg.end);
				ac = aN();
				aJ(aQ, ah);
				if (aw.swipeStatus || aw.pinchStatus) {
					be = P(bi, aa)
				}
				if (!aw.triggerOnTouchEnd || aw.triggerOnTouchLeave) {
					var bc = true;
					if (aw.triggerOnTouchLeave) {
						var bh = aZ(this);
						bc = F(bg.end, bh)
					}
					if (!aw.triggerOnTouchEnd && bc) {
						aa = aD(k)
					} else {
						if (aw.triggerOnTouchLeave && !bc) {
							aa = aD(h)
						}
					}
					if (aa == q || aa == h) {
						P(bi, aa)
					}
				}
			} else {
				aa = q;
				P(bi, aa)
			}
			if (be === false) {
				aa = q;
				P(bi, aa)
			}
		}

		function M(bc) {
			var bd = bc.originalEvent ? bc.originalEvent : bc,
				be = bd.touches;
			if (be) {
				if (be.length) {
					G();
					return true
				}
			}
			if (an()) {
				X = ae
			}
			a3 = au();
			ac = aN();
			if (bb() || !ao()) {
				aa = q;
				P(bd, aa)
			} else {
				if (aw.triggerOnTouchEnd || (aw.triggerOnTouchEnd == false && aa === k)) {
					bc.preventDefault();
					aa = h;
					P(bd, aa)
				} else {
					if (!aw.triggerOnTouchEnd && a7()) {
						aa = h;
						aG(bd, aa, B)
					} else {
						if (aa === k) {
							aa = q;
							P(bd, aa)
						}
					}
				}
			}
			ap(false);
			return null
		}

		function ba() {
			X = 0;
			a3 = 0;
			U = 0;
			a2 = 0;
			a0 = 0;
			H = 1;
			S();
			ap(false)
		}

		function L(bc) {
			var bd = bc.originalEvent ? bc.originalEvent : bc;
			if (aw.triggerOnTouchLeave) {
				aa = aD(h);
				P(bd, aa)
			}
		}

		function aL() {
			aS.unbind(K, aO);
			aS.unbind(aE, ba);
			aS.unbind(az, a4);
			aS.unbind(V, M);
			if (T) {
				aS.unbind(T, L)
			}
			ap(false)
		}

		function aD(bg) {
			var bf = bg;
			var be = aB();
			var bd = ao();
			var bc = bb();
			if (!be || bc) {
				bf = q
			} else {
				if (bd && bg == k && (!aw.triggerOnTouchEnd || aw.triggerOnTouchLeave)) {
					bf = h
				} else {
					if (!bd && bg == h && aw.triggerOnTouchLeave) {
						bf = q
					}
				}
			}
			return bf
		}

		function P(be, bc) {
			var bd, bf = be.touches;
			if ((J() || W()) || (Q() || aY())) {
				if (J() || W()) {
					bd = aG(be, bc, l)
				}
				if ((Q() || aY()) && bd !== false) {
					bd = aG(be, bc, t)
				}
			} else {
				if (aH() && bd !== false) {
					bd = aG(be, bc, j)
				} else {
					if (aq() && bd !== false) {
						bd = aG(be, bc, b)
					} else {
						if (ai() && bd !== false) {
							bd = aG(be, bc, B)
						}
					}
				}
			}
			if (bc === q) {
				ba(be)
			}
			if (bc === h) {
				if (bf) {
					if (!bf.length) {
						ba(be)
					}
				} else {
					ba(be)
				}
			}
			return bd
		}

		function aG(bf, bc, be) {
			var bd;
			if (be == l) {
				aS.trigger("swipeStatus", [bc, aQ || null, ah || 0, ac || 0, X, aR]);
				if (aw.swipeStatus) {
					bd = aw.swipeStatus.call(aS, bf, bc, aQ || null, ah || 0, ac || 0, X, aR);
					if (bd === false) {
						return false
					}
				}
				if (bc == h && aW()) {
					aS.trigger("swipe", [aQ, ah, ac, X, aR]);
					if (aw.swipe) {
						bd = aw.swipe.call(aS, bf, aQ, ah, ac, X, aR);
						if (bd === false) {
							return false
						}
					}
					switch (aQ) {
						case p:
							aS.trigger("swipeLeft", [aQ, ah, ac, X, aR]);
							if (aw.swipeLeft) {
								bd = aw.swipeLeft.call(aS, bf, aQ, ah, ac, X, aR)
							}
							break;
						case o:
							aS.trigger("swipeRight", [aQ, ah, ac, X, aR]);
							if (aw.swipeRight) {
								bd = aw.swipeRight.call(aS, bf, aQ, ah, ac, X, aR)
							}
							break;
						case e:
							aS.trigger("swipeUp", [aQ, ah, ac, X, aR]);
							if (aw.swipeUp) {
								bd = aw.swipeUp.call(aS, bf, aQ, ah, ac, X, aR)
							}
							break;
						case x:
							aS.trigger("swipeDown", [aQ, ah, ac, X, aR]);
							if (aw.swipeDown) {
								bd = aw.swipeDown.call(aS, bf, aQ, ah, ac, X, aR)
							}
							break
					}
				}
			}
			if (be == t) {
				aS.trigger("pinchStatus", [bc, aK || null, ar || 0, ac || 0, X, H, aR]);
				if (aw.pinchStatus) {
					bd = aw.pinchStatus.call(aS, bf, bc, aK || null, ar || 0, ac || 0, X, H, aR);
					if (bd === false) {
						return false
					}
				}
				if (bc == h && a9()) {
					switch (aK) {
						case c:
							aS.trigger("pinchIn", [aK || null, ar || 0, ac || 0, X, H, aR]);
							if (aw.pinchIn) {
								bd = aw.pinchIn.call(aS, bf, aK || null, ar || 0, ac || 0, X, H, aR)
							}
							break;
						case A:
							aS.trigger("pinchOut", [aK || null, ar || 0, ac || 0, X, H, aR]);
							if (aw.pinchOut) {
								bd = aw.pinchOut.call(aS, bf, aK || null, ar || 0, ac || 0, X, H, aR)
							}
							break
					}
				}
			}
			if (be == B) {
				if (bc === q || bc === h) {
					clearTimeout(aX);
					clearTimeout(ag);
					if (Z() && !I()) {
						O = au();
						aX = setTimeout(f.proxy(function () {
							O = null;
							aS.trigger("tap", [bf.target]);
							if (aw.tap) {
								bd = aw.tap.call(aS, bf, bf.target)
							}
						}, this), aw.doubleTapThreshold)
					} else {
						O = null;
						aS.trigger("tap", [bf.target]);
						if (aw.tap) {
							bd = aw.tap.call(aS, bf, bf.target)
						}
					}
				}
			} else {
				if (be == j) {
					if (bc === q || bc === h) {
						clearTimeout(aX);
						O = null;
						aS.trigger("doubletap", [bf.target]);
						if (aw.doubleTap) {
							bd = aw.doubleTap.call(aS, bf, bf.target)
						}
					}
				} else {
					if (be == b) {
						if (bc === q || bc === h) {
							clearTimeout(aX);
							O = null;
							aS.trigger("longtap", [bf.target]);
							if (aw.longTap) {
								bd = aw.longTap.call(aS, bf, bf.target)
							}
						}
					}
				}
			}
			return bd
		}

		function ao() {
			var bc = true;
			if (aw.threshold !== null) {
				bc = ah >= aw.threshold
			}
			return bc
		}

		function bb() {
			var bc = false;
			if (aw.cancelThreshold !== null && aQ !== null) {
				bc = (aU(aQ) - ah) >= aw.cancelThreshold
			}
			return bc
		}

		function af() {
			if (aw.pinchThreshold !== null) {
				return ar >= aw.pinchThreshold
			}
			return true
		}

		function aB() {
			var bc;
			if (aw.maxTimeThreshold) {
				if (ac >= aw.maxTimeThreshold) {
					bc = false
				} else {
					bc = true
				}
			} else {
				bc = true
			}
			return bc
		}

		function am(bc, bd) {
			if (aw.preventDefaultEvents === false) {
				return
			}
			if (aw.allowPageScroll === m) {
				bc.preventDefault()
			} else {
				var be = aw.allowPageScroll === s;
				switch (bd) {
					case p:
						if ((aw.swipeLeft && be) || (!be && aw.allowPageScroll != E)) {
							bc.preventDefault()
						}
						break;
					case o:
						if ((aw.swipeRight && be) || (!be && aw.allowPageScroll != E)) {
							bc.preventDefault()
						}
						break;
					case e:
						if ((aw.swipeUp && be) || (!be && aw.allowPageScroll != u)) {
							bc.preventDefault()
						}
						break;
					case x:
						if ((aw.swipeDown && be) || (!be && aw.allowPageScroll != u)) {
							bc.preventDefault()
						}
						break
				}
			}
		}

		function a9() {
			var bd = aP();
			var bc = Y();
			var be = af();
			return bd && bc && be
		}

		function aY() {
			return !!(aw.pinchStatus || aw.pinchIn || aw.pinchOut)
		}

		function Q() {
			return !!(a9() && aY())
		}

		function aW() {
			var bf = aB();
			var bh = ao();
			var be = aP();
			var bc = Y();
			var bd = bb();
			var bg = !bd && bc && be && bh && bf;
			return bg
		}

		function W() {
			return !!(aw.swipe || aw.swipeStatus || aw.swipeLeft || aw.swipeRight || aw.swipeUp || aw.swipeDown)
		}

		function J() {
			return !!(aW() && W())
		}

		function aP() {
			return ((X === aw.fingers || aw.fingers === i) || !a)
		}

		function Y() {
			return aR[0].end.x !== 0
		}

		function a7() {
			return !!(aw.tap)
		}

		function Z() {
			return !!(aw.doubleTap)
		}

		function aV() {
			return !!(aw.longTap)
		}

		function R() {
			if (O == null) {
				return false
			}
			var bc = au();
			return (Z() && ((bc - O) <= aw.doubleTapThreshold))
		}

		function I() {
			return R()
		}

		function ay() {
			return ((X === 1 || !a) && (isNaN(ah) || ah < aw.threshold))
		}

		function a1() {
			return ((ac > aw.longTapThreshold) && (ah < r))
		}

		function ai() {
			return !!(ay() && a7())
		}

		function aH() {
			return !!(R() && Z())
		}

		function aq() {
			return !!(a1() && aV())
		}

		function G() {
			a6 = au();
			ae = event.touches.length + 1
		}

		function S() {
			a6 = 0;
			ae = 0
		}

		function an() {
			var bc = false;
			if (a6) {
				var bd = au() - a6;
				if (bd <= aw.fingerReleaseThreshold) {
					bc = true
				}
			}
			return bc
		}

		function aC() {
			return !!(aS.data(C + "_intouch") === true)
		}

		function ap(bc) {
			if (bc === true) {
				aS.bind(az, a4);
				aS.bind(V, M);
				if (T) {
					aS.bind(T, L)
				}
			} else {
				aS.unbind(az, a4, false);
				aS.unbind(V, M, false);
				if (T) {
					aS.unbind(T, L, false)
				}
			}
			aS.data(C + "_intouch", bc === true)
		}

		function aj(bd, bc) {
			var be = bc.identifier !== undefined ? bc.identifier : 0;
			aR[bd].identifier = be;
			aR[bd].start.x = aR[bd].end.x = bc.pageX || bc.clientX;
			aR[bd].start.y = aR[bd].end.y = bc.pageY || bc.clientY;
			return aR[bd]
		}

		function aI(bc) {
			var be = bc.identifier !== undefined ? bc.identifier : 0;
			var bd = ad(be);
			bd.end.x = bc.pageX || bc.clientX;
			bd.end.y = bc.pageY || bc.clientY;
			return bd
		}

		function ad(bd) {
			for (var bc = 0; bc < aR.length; bc++) {
				if (aR[bc].identifier == bd) {
					return aR[bc]
				}
			}
		}

		function ak() {
			var bc = [];
			for (var bd = 0; bd <= 5; bd++) {
				bc.push({
					start: {
						x: 0,
						y: 0
					},
					end: {
						x: 0,
						y: 0
					},
					identifier: 0
				})
			}
			return bc
		}

		function aJ(bc, bd) {
			bd = Math.max(bd, aU(bc));
			N[bc].distance = bd
		}

		function aU(bc) {
			if (N[bc]) {
				return N[bc].distance
			}
			return undefined
		}

		function ab() {
			var bc = {};
			bc[p] = ax(p);
			bc[o] = ax(o);
			bc[e] = ax(e);
			bc[x] = ax(x);
			return bc
		}

		function ax(bc) {
			return {
				direction: bc,
				distance: 0
			}
		}

		function aN() {
			return a3 - U
		}

		function av(bf, be) {
			var bd = Math.abs(bf.x - be.x);
			var bc = Math.abs(bf.y - be.y);
			return Math.round(Math.sqrt(bd * bd + bc * bc))
		}

		function a8(bc, bd) {
			var be = (bd / bc) * 1;
			return be.toFixed(2)
		}

		function at() {
			if (H < 1) {
				return A
			} else {
				return c
			}
		}

		function aT(bd, bc) {
			return Math.round(Math.sqrt(Math.pow(bc.x - bd.x, 2) + Math.pow(bc.y - bd.y, 2)))
		}

		function aF(bf, bd) {
			var bc = bf.x - bd.x;
			var bh = bd.y - bf.y;
			var be = Math.atan2(bh, bc);
			var bg = Math.round(be * 180 / Math.PI);
			if (bg < 0) {
				bg = 360 - Math.abs(bg)
			}
			return bg
		}

		function aM(bd, bc) {
			var be = aF(bd, bc);
			if ((be <= 45) && (be >= 0)) {
				return p
			} else {
				if ((be <= 360) && (be >= 315)) {
					return p
				} else {
					if ((be >= 135) && (be <= 225)) {
						return o
					} else {
						if ((be > 45) && (be < 135)) {
							return x
						} else {
							return e
						}
					}
				}
			}
		}

		function au() {
			var bc = new Date();
			return bc.getTime()
		}

		function aZ(bc) {
			bc = f(bc);
			var be = bc.offset();
			var bd = {
				left: be.left,
				right: be.left + bc.outerWidth(),
				top: be.top,
				bottom: be.top + bc.outerHeight()
			};
			return bd
		}

		function F(bc, bd) {
			return (bc.x > bd.left && bc.x < bd.right && bc.y > bd.top && bc.y < bd.bottom)
		}
	}
}));
if (typeof (console) === 'undefined') {
	var console = {};
	console.log = console.error = console.info = console.debug = console.warn = console.trace = console.dir = console.dirxml = console.group = console.groupEnd = console.time = console.timeEnd = console.assert = console.profile = console.groupCollapsed = function () {};
}
if (window.tplogs == true)
	try {
		console.groupCollapsed("ThemePunch GreenSocks Logs");
	} catch (e) {}
var oldgs = window.GreenSockGlobals;
oldgs_queue = window._gsQueue;
var punchgs = window.GreenSockGlobals = {};
if (window.tplogs == true)
	try {
		console.info("Build GreenSock SandBox for ThemePunch Plugins");
		console.info("GreenSock TweenLite Engine Initalised by ThemePunch Plugin");
	} catch (e) {}
	/*!
	 * VERSION: 1.19.1
	 * DATE: 2017-01-17
	 * UPDATES AND DOCS AT: http://greensock.com
	 *
	 * @license Copyright (c) 2008-2017, GreenSock. All rights reserved.
	 * This work is subject to the terms at http://greensock.com/standard-license or for
	 * Club GreenSock members, the software agreement that was issued with your membership.
	 *
	 * @author: Jack Doyle, jack@greensock.com
	 */
	! function (a, b) {
		"use strict";
		var c = {},
			d = a.document,
			e = a.GreenSockGlobals = a.GreenSockGlobals || a;
		if (!e.TweenLite) {
			var f, g, h, i, j, k = function (a) {
					var b, c = a.split("."),
						d = e;
					for (b = 0; b < c.length; b++) d[c[b]] = d = d[c[b]] || {};
					return d
				},
				l = k("com.greensock"),
				m = 1e-10,
				n = function (a) {
					var b, c = [],
						d = a.length;
					for (b = 0; b !== d; c.push(a[b++]));
					return c
				},
				o = function () {},
				p = function () {
					var a = Object.prototype.toString,
						b = a.call([]);
					return function (c) {
						return null != c && (c instanceof Array || "object" == typeof c && !!c.push && a.call(c) === b)
					}
				}(),
				q = {},
				r = function (d, f, g, h) {
					this.sc = q[d] ? q[d].sc : [], q[d] = this, this.gsClass = null, this.func = g;
					var i = [];
					this.check = function (j) {
						for (var l, m, n, o, p, s = f.length, t = s; --s > -1;)(l = q[f[s]] || new r(f[s], [])).gsClass ? (i[s] = l.gsClass, t--) : j && l.sc.push(this);
						if (0 === t && g) {
							if (m = ("com.greensock." + d).split("."), n = m.pop(), o = k(m.join("."))[n] = this.gsClass = g.apply(g, i), h)
								if (e[n] = c[n] = o, p = "undefined" != typeof module && module.exports, !p && "function" == typeof define && define.amd) define((a.GreenSockAMDPath ? a.GreenSockAMDPath + "/" : "") + d.split(".").pop(), [], function () {
									return o
								});
								else if (p)
								if (d === b) {
									module.exports = c[b] = o;
									for (s in c) o[s] = c[s]
								} else c[b] && (c[b][n] = o);
							for (s = 0; s < this.sc.length; s++) this.sc[s].check()
						}
					}, this.check(!0)
				},
				s = a._gsDefine = function (a, b, c, d) {
					return new r(a, b, c, d)
				},
				t = l._class = function (a, b, c) {
					return b = b || function () {}, s(a, [], function () {
						return b
					}, c), b
				};
			s.globals = e;
			var u = [0, 0, 1, 1],
				v = t("easing.Ease", function (a, b, c, d) {
					this._func = a, this._type = c || 0, this._power = d || 0, this._params = b ? u.concat(b) : u
				}, !0),
				w = v.map = {},
				x = v.register = function (a, b, c, d) {
					for (var e, f, g, h, i = b.split(","), j = i.length, k = (c || "easeIn,easeOut,easeInOut").split(","); --j > -1;)
						for (f = i[j], e = d ? t("easing." + f, null, !0) : l.easing[f] || {}, g = k.length; --g > -1;) h = k[g], w[f + "." + h] = w[h + f] = e[h] = a.getRatio ? a : a[h] || new a
				};
			for (h = v.prototype, h._calcEnd = !1, h.getRatio = function (a) {
					if (this._func) return this._params[0] = a, this._func.apply(null, this._params);
					var b = this._type,
						c = this._power,
						d = 1 === b ? 1 - a : 2 === b ? a : .5 > a ? 2 * a : 2 * (1 - a);
					return 1 === c ? d *= d : 2 === c ? d *= d * d : 3 === c ? d *= d * d * d : 4 === c && (d *= d * d * d * d), 1 === b ? 1 - d : 2 === b ? d : .5 > a ? d / 2 : 1 - d / 2
				}, f = ["Linear", "Quad", "Cubic", "Quart", "Quint,Strong"], g = f.length; --g > -1;) h = f[g] + ",Power" + g, x(new v(null, null, 1, g), h, "easeOut", !0), x(new v(null, null, 2, g), h, "easeIn" + (0 === g ? ",easeNone" : "")), x(new v(null, null, 3, g), h, "easeInOut");
			w.linear = l.easing.Linear.easeIn, w.swing = l.easing.Quad.easeInOut;
			var y = t("events.EventDispatcher", function (a) {
				this._listeners = {}, this._eventTarget = a || this
			});
			h = y.prototype, h.addEventListener = function (a, b, c, d, e) {
				e = e || 0;
				var f, g, h = this._listeners[a],
					k = 0;
				for (this !== i || j || i.wake(), null == h && (this._listeners[a] = h = []), g = h.length; --g > -1;) f = h[g], f.c === b && f.s === c ? h.splice(g, 1) : 0 === k && f.pr < e && (k = g + 1);
				h.splice(k, 0, {
					c: b,
					s: c,
					up: d,
					pr: e
				})
			}, h.removeEventListener = function (a, b) {
				var c, d = this._listeners[a];
				if (d)
					for (c = d.length; --c > -1;)
						if (d[c].c === b) return void d.splice(c, 1)
			}, h.dispatchEvent = function (a) {
				var b, c, d, e = this._listeners[a];
				if (e)
					for (b = e.length, b > 1 && (e = e.slice(0)), c = this._eventTarget; --b > -1;) d = e[b], d && (d.up ? d.c.call(d.s || c, {
						type: a,
						target: c
					}) : d.c.call(d.s || c))
			};
			var z = a.requestAnimationFrame,
				A = a.cancelAnimationFrame,
				B = Date.now || function () {
					return (new Date).getTime()
				},
				C = B();
			for (f = ["ms", "moz", "webkit", "o"], g = f.length; --g > -1 && !z;) z = a[f[g] + "RequestAnimationFrame"], A = a[f[g] + "CancelAnimationFrame"] || a[f[g] + "CancelRequestAnimationFrame"];
			t("Ticker", function (a, b) {
				var c, e, f, g, h, k = this,
					l = B(),
					n = b !== !1 && z ? "auto" : !1,
					p = 500,
					q = 33,
					r = "tick",
					s = function (a) {
						var b, d, i = B() - C;
						i > p && (l += i - q), C += i, k.time = (C - l) / 1e3, b = k.time - h, (!c || b > 0 || a === !0) && (k.frame++, h += b + (b >= g ? .004 : g - b), d = !0), a !== !0 && (f = e(s)), d && k.dispatchEvent(r)
					};
				y.call(k), k.time = k.frame = 0, k.tick = function () {
					s(!0)
				}, k.lagSmoothing = function (a, b) {
					p = a || 1 / m, q = Math.min(b, p, 0)
				}, k.sleep = function () {
					null != f && (n && A ? A(f) : clearTimeout(f), e = o, f = null, k === i && (j = !1))
				}, k.wake = function (a) {
					null !== f ? k.sleep() : a ? l += -C + (C = B()) : k.frame > 10 && (C = B() - p + 5), e = 0 === c ? o : n && z ? z : function (a) {
						return setTimeout(a, 1e3 * (h - k.time) + 1 | 0)
					}, k === i && (j = !0), s(2)
				}, k.fps = function (a) {
					return arguments.length ? (c = a, g = 1 / (c || 60), h = this.time + g, void k.wake()) : c
				}, k.useRAF = function (a) {
					return arguments.length ? (k.sleep(), n = a, void k.fps(c)) : n
				}, k.fps(a), setTimeout(function () {
					"auto" === n && k.frame < 5 && "hidden" !== d.visibilityState && k.useRAF(!1)
				}, 1500)
			}), h = l.Ticker.prototype = new l.events.EventDispatcher, h.constructor = l.Ticker;
			var D = t("core.Animation", function (a, b) {
				if (this.vars = b = b || {}, this._duration = this._totalDuration = a || 0, this._delay = Number(b.delay) || 0, this._timeScale = 1, this._active = b.immediateRender === !0, this.data = b.data, this._reversed = b.reversed === !0, W) {
					j || i.wake();
					var c = this.vars.useFrames ? V : W;
					c.add(this, c._time), this.vars.paused && this.paused(!0)
				}
			});
			i = D.ticker = new l.Ticker, h = D.prototype, h._dirty = h._gc = h._initted = h._paused = !1, h._totalTime = h._time = 0, h._rawPrevTime = -1, h._next = h._last = h._onUpdate = h._timeline = h.timeline = null, h._paused = !1;
			var E = function () {
				j && B() - C > 2e3 && i.wake(), setTimeout(E, 2e3)
			};
			E(), h.play = function (a, b) {
				return null != a && this.seek(a, b), this.reversed(!1).paused(!1)
			}, h.pause = function (a, b) {
				return null != a && this.seek(a, b), this.paused(!0)
			}, h.resume = function (a, b) {
				return null != a && this.seek(a, b), this.paused(!1)
			}, h.seek = function (a, b) {
				return this.totalTime(Number(a), b !== !1)
			}, h.restart = function (a, b) {
				return this.reversed(!1).paused(!1).totalTime(a ? -this._delay : 0, b !== !1, !0)
			}, h.reverse = function (a, b) {
				return null != a && this.seek(a || this.totalDuration(), b), this.reversed(!0).paused(!1)
			}, h.render = function (a, b, c) {}, h.invalidate = function () {
				return this._time = this._totalTime = 0, this._initted = this._gc = !1, this._rawPrevTime = -1, (this._gc || !this.timeline) && this._enabled(!0), this
			}, h.isActive = function () {
				var a, b = this._timeline,
					c = this._startTime;
				return !b || !this._gc && !this._paused && b.isActive() && (a = b.rawTime(!0)) >= c && a < c + this.totalDuration() / this._timeScale
			}, h._enabled = function (a, b) {
				return j || i.wake(), this._gc = !a, this._active = this.isActive(), b !== !0 && (a && !this.timeline ? this._timeline.add(this, this._startTime - this._delay) : !a && this.timeline && this._timeline._remove(this, !0)), !1
			}, h._kill = function (a, b) {
				return this._enabled(!1, !1)
			}, h.kill = function (a, b) {
				return this._kill(a, b), this
			}, h._uncache = function (a) {
				for (var b = a ? this : this.timeline; b;) b._dirty = !0, b = b.timeline;
				return this
			}, h._swapSelfInParams = function (a) {
				for (var b = a.length, c = a.concat(); --b > -1;) "{self}" === a[b] && (c[b] = this);
				return c
			}, h._callback = function (a) {
				var b = this.vars,
					c = b[a],
					d = b[a + "Params"],
					e = b[a + "Scope"] || b.callbackScope || this,
					f = d ? d.length : 0;
				switch (f) {
					case 0:
						c.call(e);
						break;
					case 1:
						c.call(e, d[0]);
						break;
					case 2:
						c.call(e, d[0], d[1]);
						break;
					default:
						c.apply(e, d)
				}
			}, h.eventCallback = function (a, b, c, d) {
				if ("on" === (a || "").substr(0, 2)) {
					var e = this.vars;
					if (1 === arguments.length) return e[a];
					null == b ? delete e[a] : (e[a] = b, e[a + "Params"] = p(c) && -1 !== c.join("").indexOf("{self}") ? this._swapSelfInParams(c) : c, e[a + "Scope"] = d), "onUpdate" === a && (this._onUpdate = b)
				}
				return this
			}, h.delay = function (a) {
				return arguments.length ? (this._timeline.smoothChildTiming && this.startTime(this._startTime + a - this._delay), this._delay = a, this) : this._delay
			}, h.duration = function (a) {
				return arguments.length ? (this._duration = this._totalDuration = a, this._uncache(!0), this._timeline.smoothChildTiming && this._time > 0 && this._time < this._duration && 0 !== a && this.totalTime(this._totalTime * (a / this._duration), !0), this) : (this._dirty = !1, this._duration)
			}, h.totalDuration = function (a) {
				return this._dirty = !1, arguments.length ? this.duration(a) : this._totalDuration
			}, h.time = function (a, b) {
				return arguments.length ? (this._dirty && this.totalDuration(), this.totalTime(a > this._duration ? this._duration : a, b)) : this._time
			}, h.totalTime = function (a, b, c) {
				if (j || i.wake(), !arguments.length) return this._totalTime;
				if (this._timeline) {
					if (0 > a && !c && (a += this.totalDuration()), this._timeline.smoothChildTiming) {
						this._dirty && this.totalDuration();
						var d = this._totalDuration,
							e = this._timeline;
						if (a > d && !c && (a = d), this._startTime = (this._paused ? this._pauseTime : e._time) - (this._reversed ? d - a : a) / this._timeScale, e._dirty || this._uncache(!1), e._timeline)
							for (; e._timeline;) e._timeline._time !== (e._startTime + e._totalTime) / e._timeScale && e.totalTime(e._totalTime, !0), e = e._timeline
					}
					this._gc && this._enabled(!0, !1), (this._totalTime !== a || 0 === this._duration) && (J.length && Y(), this.render(a, b, !1), J.length && Y())
				}
				return this
			}, h.progress = h.totalProgress = function (a, b) {
				var c = this.duration();
				return arguments.length ? this.totalTime(c * a, b) : c ? this._time / c : this.ratio
			}, h.startTime = function (a) {
				return arguments.length ? (a !== this._startTime && (this._startTime = a, this.timeline && this.timeline._sortChildren && this.timeline.add(this, a - this._delay)), this) : this._startTime
			}, h.endTime = function (a) {
				return this._startTime + (0 != a ? this.totalDuration() : this.duration()) / this._timeScale
			}, h.timeScale = function (a) {
				if (!arguments.length) return this._timeScale;
				if (a = a || m, this._timeline && this._timeline.smoothChildTiming) {
					var b = this._pauseTime,
						c = b || 0 === b ? b : this._timeline.totalTime();
					this._startTime = c - (c - this._startTime) * this._timeScale / a
				}
				return this._timeScale = a, this._uncache(!1)
			}, h.reversed = function (a) {
				return arguments.length ? (a != this._reversed && (this._reversed = a, this.totalTime(this._timeline && !this._timeline.smoothChildTiming ? this.totalDuration() - this._totalTime : this._totalTime, !0)), this) : this._reversed
			}, h.paused = function (a) {
				if (!arguments.length) return this._paused;
				var b, c, d = this._timeline;
				return a != this._paused && d && (j || a || i.wake(), b = d.rawTime(), c = b - this._pauseTime, !a && d.smoothChildTiming && (this._startTime += c, this._uncache(!1)), this._pauseTime = a ? b : null, this._paused = a, this._active = this.isActive(), !a && 0 !== c && this._initted && this.duration() && (b = d.smoothChildTiming ? this._totalTime : (b - this._startTime) / this._timeScale, this.render(b, b === this._totalTime, !0))), this._gc && !a && this._enabled(!0, !1), this
			};
			var F = t("core.SimpleTimeline", function (a) {
				D.call(this, 0, a), this.autoRemoveChildren = this.smoothChildTiming = !0
			});
			h = F.prototype = new D, h.constructor = F, h.kill()._gc = !1, h._first = h._last = h._recent = null, h._sortChildren = !1, h.add = h.insert = function (a, b, c, d) {
				var e, f;
				if (a._startTime = Number(b || 0) + a._delay, a._paused && this !== a._timeline && (a._pauseTime = a._startTime + (this.rawTime() - a._startTime) / a._timeScale), a.timeline && a.timeline._remove(a, !0), a.timeline = a._timeline = this, a._gc && a._enabled(!0, !0), e = this._last, this._sortChildren)
					for (f = a._startTime; e && e._startTime > f;) e = e._prev;
				return e ? (a._next = e._next, e._next = a) : (a._next = this._first, this._first = a), a._next ? a._next._prev = a : this._last = a, a._prev = e, this._recent = a, this._timeline && this._uncache(!0), this
			}, h._remove = function (a, b) {
				return a.timeline === this && (b || a._enabled(!1, !0), a._prev ? a._prev._next = a._next : this._first === a && (this._first = a._next), a._next ? a._next._prev = a._prev : this._last === a && (this._last = a._prev), a._next = a._prev = a.timeline = null, a === this._recent && (this._recent = this._last), this._timeline && this._uncache(!0)), this
			}, h.render = function (a, b, c) {
				var d, e = this._first;
				for (this._totalTime = this._time = this._rawPrevTime = a; e;) d = e._next, (e._active || a >= e._startTime && !e._paused) && (e._reversed ? e.render((e._dirty ? e.totalDuration() : e._totalDuration) - (a - e._startTime) * e._timeScale, b, c) : e.render((a - e._startTime) * e._timeScale, b, c)), e = d
			}, h.rawTime = function () {
				return j || i.wake(), this._totalTime
			};
			var G = t("TweenLite", function (b, c, d) {
					if (D.call(this, c, d), this.render = G.prototype.render, null == b) throw "Cannot tween a null target.";
					this.target = b = "string" != typeof b ? b : G.selector(b) || b;
					var e, f, g, h = b.jquery || b.length && b !== a && b[0] && (b[0] === a || b[0].nodeType && b[0].style && !b.nodeType),
						i = this.vars.overwrite;
					if (this._overwrite = i = null == i ? U[G.defaultOverwrite] : "number" == typeof i ? i >> 0 : U[i], (h || b instanceof Array || b.push && p(b)) && "number" != typeof b[0])
						for (this._targets = g = n(b), this._propLookup = [], this._siblings = [], e = 0; e < g.length; e++) f = g[e], f ? "string" != typeof f ? f.length && f !== a && f[0] && (f[0] === a || f[0].nodeType && f[0].style && !f.nodeType) ? (g.splice(e--, 1), this._targets = g = g.concat(n(f))) : (this._siblings[e] = Z(f, this, !1), 1 === i && this._siblings[e].length > 1 && _(f, this, null, 1, this._siblings[e])) : (f = g[e--] = G.selector(f), "string" == typeof f && g.splice(e + 1, 1)) : g.splice(e--, 1);
					else this._propLookup = {}, this._siblings = Z(b, this, !1), 1 === i && this._siblings.length > 1 && _(b, this, null, 1, this._siblings);
					(this.vars.immediateRender || 0 === c && 0 === this._delay && this.vars.immediateRender !== !1) && (this._time = -m, this.render(Math.min(0, -this._delay)))
				}, !0),
				H = function (b) {
					return b && b.length && b !== a && b[0] && (b[0] === a || b[0].nodeType && b[0].style && !b.nodeType)
				},
				I = function (a, b) {
					var c, d = {};
					for (c in a) T[c] || c in b && "transform" !== c && "x" !== c && "y" !== c && "width" !== c && "height" !== c && "className" !== c && "border" !== c || !(!Q[c] || Q[c] && Q[c]._autoCSS) || (d[c] = a[c], delete a[c]);
					a.css = d
				};
			h = G.prototype = new D, h.constructor = G, h.kill()._gc = !1, h.ratio = 0, h._firstPT = h._targets = h._overwrittenProps = h._startAt = null, h._notifyPluginsOfEnabled = h._lazy = !1, G.version = "1.19.1", G.defaultEase = h._ease = new v(null, null, 1, 1), G.defaultOverwrite = "auto", G.ticker = i, G.autoSleep = 120, G.lagSmoothing = function (a, b) {
				i.lagSmoothing(a, b)
			}, G.selector = a.$ || a.jQuery || function (b) {
				var c = a.$ || a.jQuery;
				return c ? (G.selector = c, c(b)) : "undefined" == typeof d ? b : d.querySelectorAll ? d.querySelectorAll(b) : d.getElementById("#" === b.charAt(0) ? b.substr(1) : b)
			};
			var J = [],
				K = {},
				L = /(?:(-|-=|\+=)?\d*\.?\d*(?:e[\-+]?\d+)?)[0-9]/gi,
				M = function (a) {
					for (var b, c = this._firstPT, d = 1e-6; c;) b = c.blob ? 1 === a ? this.end : a ? this.join("") : this.start : c.c * a + c.s, c.m ? b = c.m(b, this._target || c.t) : d > b && b > -d && !c.blob && (b = 0), c.f ? c.fp ? c.t[c.p](c.fp, b) : c.t[c.p](b) : c.t[c.p] = b, c = c._next
				},
				N = function (a, b, c, d) {
					var e, f, g, h, i, j, k, l = [],
						m = 0,
						n = "",
						o = 0;
					for (l.start = a, l.end = b, a = l[0] = a + "", b = l[1] = b + "", c && (c(l), a = l[0], b = l[1]), l.length = 0, e = a.match(L) || [], f = b.match(L) || [], d && (d._next = null, d.blob = 1, l._firstPT = l._applyPT = d), i = f.length, h = 0; i > h; h++) k = f[h], j = b.substr(m, b.indexOf(k, m) - m), n += j || !h ? j : ",", m += j.length, o ? o = (o + 1) % 5 : "rgba(" === j.substr(-5) && (o = 1), k === e[h] || e.length <= h ? n += k : (n && (l.push(n), n = ""), g = parseFloat(e[h]), l.push(g), l._firstPT = {
						_next: l._firstPT,
						t: l,
						p: l.length - 1,
						s: g,
						c: ("=" === k.charAt(1) ? parseInt(k.charAt(0) + "1", 10) * parseFloat(k.substr(2)) : parseFloat(k) - g) || 0,
						f: 0,
						m: o && 4 > o ? Math.round : 0
					}), m += k.length;
					return n += b.substr(m), n && l.push(n), l.setRatio = M, l
				},
				O = function (a, b, c, d, e, f, g, h, i) {
					"function" == typeof d && (d = d(i || 0, a));
					var j, k = typeof a[b],
						l = "function" !== k ? "" : b.indexOf("set") || "function" != typeof a["get" + b.substr(3)] ? b : "get" + b.substr(3),
						m = "get" !== c ? c : l ? g ? a[l](g) : a[l]() : a[b],
						n = "string" == typeof d && "=" === d.charAt(1),
						o = {
							t: a,
							p: b,
							s: m,
							f: "function" === k,
							pg: 0,
							n: e || b,
							m: f ? "function" == typeof f ? f : Math.round : 0,
							pr: 0,
							c: n ? parseInt(d.charAt(0) + "1", 10) * parseFloat(d.substr(2)) : parseFloat(d) - m || 0
						};
					return ("number" != typeof m || "number" != typeof d && !n) && (g || isNaN(m) || !n && isNaN(d) || "boolean" == typeof m || "boolean" == typeof d ? (o.fp = g, j = N(m, n ? o.s + o.c : d, h || G.defaultStringFilter, o), o = {
						t: j,
						p: "setRatio",
						s: 0,
						c: 1,
						f: 2,
						pg: 0,
						n: e || b,
						pr: 0,
						m: 0
					}) : (o.s = parseFloat(m), n || (o.c = parseFloat(d) - o.s || 0))), o.c ? ((o._next = this._firstPT) && (o._next._prev = o), this._firstPT = o, o) : void 0
				},
				P = G._internals = {
					isArray: p,
					isSelector: H,
					lazyTweens: J,
					blobDif: N
				},
				Q = G._plugins = {},
				R = P.tweenLookup = {},
				S = 0,
				T = P.reservedProps = {
					ease: 1,
					delay: 1,
					overwrite: 1,
					onComplete: 1,
					onCompleteParams: 1,
					onCompleteScope: 1,
					useFrames: 1,
					runBackwards: 1,
					startAt: 1,
					onUpdate: 1,
					onUpdateParams: 1,
					onUpdateScope: 1,
					onStart: 1,
					onStartParams: 1,
					onStartScope: 1,
					onReverseComplete: 1,
					onReverseCompleteParams: 1,
					onReverseCompleteScope: 1,
					onRepeat: 1,
					onRepeatParams: 1,
					onRepeatScope: 1,
					easeParams: 1,
					yoyo: 1,
					immediateRender: 1,
					repeat: 1,
					repeatDelay: 1,
					data: 1,
					paused: 1,
					reversed: 1,
					autoCSS: 1,
					lazy: 1,
					onOverwrite: 1,
					callbackScope: 1,
					stringFilter: 1,
					id: 1
				},
				U = {
					none: 0,
					all: 1,
					auto: 2,
					concurrent: 3,
					allOnStart: 4,
					preexisting: 5,
					"true": 1,
					"false": 0
				},
				V = D._rootFramesTimeline = new F,
				W = D._rootTimeline = new F,
				X = 30,
				Y = P.lazyRender = function () {
					var a, b = J.length;
					for (K = {}; --b > -1;) a = J[b], a && a._lazy !== !1 && (a.render(a._lazy[0], a._lazy[1], !0), a._lazy = !1);
					J.length = 0
				};
			W._startTime = i.time, V._startTime = i.frame, W._active = V._active = !0, setTimeout(Y, 1), D._updateRoot = G.render = function () {
				var a, b, c;
				if (J.length && Y(), W.render((i.time - W._startTime) * W._timeScale, !1, !1), V.render((i.frame - V._startTime) * V._timeScale, !1, !1), J.length && Y(), i.frame >= X) {
					X = i.frame + (parseInt(G.autoSleep, 10) || 120);
					for (c in R) {
						for (b = R[c].tweens, a = b.length; --a > -1;) b[a]._gc && b.splice(a, 1);
						0 === b.length && delete R[c]
					}
					if (c = W._first, (!c || c._paused) && G.autoSleep && !V._first && 1 === i._listeners.tick.length) {
						for (; c && c._paused;) c = c._next;
						c || i.sleep()
					}
				}
			}, i.addEventListener("tick", D._updateRoot);
			var Z = function (a, b, c) {
					var d, e, f = a._gsTweenID;
					if (R[f || (a._gsTweenID = f = "t" + S++)] || (R[f] = {
							target: a,
							tweens: []
						}), b && (d = R[f].tweens, d[e = d.length] = b, c))
						for (; --e > -1;) d[e] === b && d.splice(e, 1);
					return R[f].tweens
				},
				$ = function (a, b, c, d) {
					var e, f, g = a.vars.onOverwrite;
					return g && (e = g(a, b, c, d)), g = G.onOverwrite, g && (f = g(a, b, c, d)), e !== !1 && f !== !1
				},
				_ = function (a, b, c, d, e) {
					var f, g, h, i;
					if (1 === d || d >= 4) {
						for (i = e.length, f = 0; i > f; f++)
							if ((h = e[f]) !== b) h._gc || h._kill(null, a, b) && (g = !0);
							else if (5 === d) break;
						return g
					}
					var j, k = b._startTime + m,
						l = [],
						n = 0,
						o = 0 === b._duration;
					for (f = e.length; --f > -1;)(h = e[f]) === b || h._gc || h._paused || (h._timeline !== b._timeline ? (j = j || aa(b, 0, o), 0 === aa(h, j, o) && (l[n++] = h)) : h._startTime <= k && h._startTime + h.totalDuration() / h._timeScale > k && ((o || !h._initted) && k - h._startTime <= 2e-10 || (l[n++] = h)));
					for (f = n; --f > -1;)
						if (h = l[f], 2 === d && h._kill(c, a, b) && (g = !0), 2 !== d || !h._firstPT && h._initted) {
							if (2 !== d && !$(h, b)) continue;
							h._enabled(!1, !1) && (g = !0)
						}
					return g
				},
				aa = function (a, b, c) {
					for (var d = a._timeline, e = d._timeScale, f = a._startTime; d._timeline;) {
						if (f += d._startTime, e *= d._timeScale, d._paused) return -100;
						d = d._timeline
					}
					return f /= e, f > b ? f - b : c && f === b || !a._initted && 2 * m > f - b ? m : (f += a.totalDuration() / a._timeScale / e) > b + m ? 0 : f - b - m
				};
			h._init = function () {
				var a, b, c, d, e, f, g = this.vars,
					h = this._overwrittenProps,
					i = this._duration,
					j = !!g.immediateRender,
					k = g.ease;
				if (g.startAt) {
					this._startAt && (this._startAt.render(-1, !0), this._startAt.kill()), e = {};
					for (d in g.startAt) e[d] = g.startAt[d];
					if (e.overwrite = !1, e.immediateRender = !0, e.lazy = j && g.lazy !== !1, e.startAt = e.delay = null, this._startAt = G.to(this.target, 0, e), j)
						if (this._time > 0) this._startAt = null;
						else if (0 !== i) return
				} else if (g.runBackwards && 0 !== i)
					if (this._startAt) this._startAt.render(-1, !0), this._startAt.kill(), this._startAt = null;
					else {
						0 !== this._time && (j = !1), c = {};
						for (d in g) T[d] && "autoCSS" !== d || (c[d] = g[d]);
						if (c.overwrite = 0, c.data = "isFromStart", c.lazy = j && g.lazy !== !1, c.immediateRender = j, this._startAt = G.to(this.target, 0, c), j) {
							if (0 === this._time) return
						} else this._startAt._init(), this._startAt._enabled(!1), this.vars.immediateRender && (this._startAt = null)
					}
				if (this._ease = k = k ? k instanceof v ? k : "function" == typeof k ? new v(k, g.easeParams) : w[k] || G.defaultEase : G.defaultEase, g.easeParams instanceof Array && k.config && (this._ease = k.config.apply(k, g.easeParams)), this._easeType = this._ease._type, this._easePower = this._ease._power, this._firstPT = null, this._targets)
					for (f = this._targets.length, a = 0; f > a; a++) this._initProps(this._targets[a], this._propLookup[a] = {}, this._siblings[a], h ? h[a] : null, a) && (b = !0);
				else b = this._initProps(this.target, this._propLookup, this._siblings, h, 0);
				if (b && G._onPluginEvent("_onInitAllProps", this), h && (this._firstPT || "function" != typeof this.target && this._enabled(!1, !1)), g.runBackwards)
					for (c = this._firstPT; c;) c.s += c.c, c.c = -c.c, c = c._next;
				this._onUpdate = g.onUpdate, this._initted = !0
			}, h._initProps = function (b, c, d, e, f) {
				var g, h, i, j, k, l;
				if (null == b) return !1;
				K[b._gsTweenID] && Y(), this.vars.css || b.style && b !== a && b.nodeType && Q.css && this.vars.autoCSS !== !1 && I(this.vars, b);
				for (g in this.vars)
					if (l = this.vars[g], T[g]) l && (l instanceof Array || l.push && p(l)) && -1 !== l.join("").indexOf("{self}") && (this.vars[g] = l = this._swapSelfInParams(l, this));
					else if (Q[g] && (j = new Q[g])._onInitTween(b, this.vars[g], this, f)) {
					for (this._firstPT = k = {
							_next: this._firstPT,
							t: j,
							p: "setRatio",
							s: 0,
							c: 1,
							f: 1,
							n: g,
							pg: 1,
							pr: j._priority,
							m: 0
						}, h = j._overwriteProps.length; --h > -1;) c[j._overwriteProps[h]] = this._firstPT;
					(j._priority || j._onInitAllProps) && (i = !0), (j._onDisable || j._onEnable) && (this._notifyPluginsOfEnabled = !0), k._next && (k._next._prev = k)
				} else c[g] = O.call(this, b, g, "get", l, g, 0, null, this.vars.stringFilter, f);
				return e && this._kill(e, b) ? this._initProps(b, c, d, e, f) : this._overwrite > 1 && this._firstPT && d.length > 1 && _(b, this, c, this._overwrite, d) ? (this._kill(c, b), this._initProps(b, c, d, e, f)) : (this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration) && (K[b._gsTweenID] = !0), i)
			}, h.render = function (a, b, c) {
				var d, e, f, g, h = this._time,
					i = this._duration,
					j = this._rawPrevTime;
				if (a >= i - 1e-7 && a >= 0) this._totalTime = this._time = i, this.ratio = this._ease._calcEnd ? this._ease.getRatio(1) : 1, this._reversed || (d = !0, e = "onComplete", c = c || this._timeline.autoRemoveChildren), 0 === i && (this._initted || !this.vars.lazy || c) && (this._startTime === this._timeline._duration && (a = 0), (0 > j || 0 >= a && a >= -1e-7 || j === m && "isPause" !== this.data) && j !== a && (c = !0, j > m && (e = "onReverseComplete")), this._rawPrevTime = g = !b || a || j === a ? a : m);
				else if (1e-7 > a) this._totalTime = this._time = 0, this.ratio = this._ease._calcEnd ? this._ease.getRatio(0) : 0, (0 !== h || 0 === i && j > 0) && (e = "onReverseComplete", d = this._reversed), 0 > a && (this._active = !1, 0 === i && (this._initted || !this.vars.lazy || c) && (j >= 0 && (j !== m || "isPause" !== this.data) && (c = !0), this._rawPrevTime = g = !b || a || j === a ? a : m)), this._initted || (c = !0);
				else if (this._totalTime = this._time = a, this._easeType) {
					var k = a / i,
						l = this._easeType,
						n = this._easePower;
					(1 === l || 3 === l && k >= .5) && (k = 1 - k), 3 === l && (k *= 2), 1 === n ? k *= k : 2 === n ? k *= k * k : 3 === n ? k *= k * k * k : 4 === n && (k *= k * k * k * k), 1 === l ? this.ratio = 1 - k : 2 === l ? this.ratio = k : .5 > a / i ? this.ratio = k / 2 : this.ratio = 1 - k / 2
				} else this.ratio = this._ease.getRatio(a / i);
				if (this._time !== h || c) {
					if (!this._initted) {
						if (this._init(), !this._initted || this._gc) return;
						if (!c && this._firstPT && (this.vars.lazy !== !1 && this._duration || this.vars.lazy && !this._duration)) return this._time = this._totalTime = h, this._rawPrevTime = j, J.push(this), void(this._lazy = [a, b]);
						this._time && !d ? this.ratio = this._ease.getRatio(this._time / i) : d && this._ease._calcEnd && (this.ratio = this._ease.getRatio(0 === this._time ? 0 : 1))
					}
					for (this._lazy !== !1 && (this._lazy = !1), this._active || !this._paused && this._time !== h && a >= 0 && (this._active = !0), 0 === h && (this._startAt && (a >= 0 ? this._startAt.render(a, b, c) : e || (e = "_dummyGS")), this.vars.onStart && (0 !== this._time || 0 === i) && (b || this._callback("onStart"))), f = this._firstPT; f;) f.f ? f.t[f.p](f.c * this.ratio + f.s) : f.t[f.p] = f.c * this.ratio + f.s, f = f._next;
					this._onUpdate && (0 > a && this._startAt && a !== -1e-4 && this._startAt.render(a, b, c), b || (this._time !== h || d || c) && this._callback("onUpdate")), e && (!this._gc || c) && (0 > a && this._startAt && !this._onUpdate && a !== -1e-4 && this._startAt.render(a, b, c), d && (this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !b && this.vars[e] && this._callback(e), 0 === i && this._rawPrevTime === m && g !== m && (this._rawPrevTime = 0))
				}
			}, h._kill = function (a, b, c) {
				if ("all" === a && (a = null), null == a && (null == b || b === this.target)) return this._lazy = !1, this._enabled(!1, !1);
				b = "string" != typeof b ? b || this._targets || this.target : G.selector(b) || b;
				var d, e, f, g, h, i, j, k, l, m = c && this._time && c._startTime === this._startTime && this._timeline === c._timeline;
				if ((p(b) || H(b)) && "number" != typeof b[0])
					for (d = b.length; --d > -1;) this._kill(a, b[d], c) && (i = !0);
				else {
					if (this._targets) {
						for (d = this._targets.length; --d > -1;)
							if (b === this._targets[d]) {
								h = this._propLookup[d] || {}, this._overwrittenProps = this._overwrittenProps || [], e = this._overwrittenProps[d] = a ? this._overwrittenProps[d] || {} : "all";
								break
							}
					} else {
						if (b !== this.target) return !1;
						h = this._propLookup, e = this._overwrittenProps = a ? this._overwrittenProps || {} : "all"
					}
					if (h) {
						if (j = a || h, k = a !== e && "all" !== e && a !== h && ("object" != typeof a || !a._tempKill), c && (G.onOverwrite || this.vars.onOverwrite)) {
							for (f in j) h[f] && (l || (l = []), l.push(f));
							if ((l || !a) && !$(this, c, b, l)) return !1
						}
						for (f in j)(g = h[f]) && (m && (g.f ? g.t[g.p](g.s) : g.t[g.p] = g.s, i = !0), g.pg && g.t._kill(j) && (i = !0), g.pg && 0 !== g.t._overwriteProps.length || (g._prev ? g._prev._next = g._next : g === this._firstPT && (this._firstPT = g._next), g._next && (g._next._prev = g._prev), g._next = g._prev = null), delete h[f]), k && (e[f] = 1);
						!this._firstPT && this._initted && this._enabled(!1, !1)
					}
				}
				return i
			}, h.invalidate = function () {
				return this._notifyPluginsOfEnabled && G._onPluginEvent("_onDisable", this), this._firstPT = this._overwrittenProps = this._startAt = this._onUpdate = null, this._notifyPluginsOfEnabled = this._active = this._lazy = !1, this._propLookup = this._targets ? {} : [], D.prototype.invalidate.call(this), this.vars.immediateRender && (this._time = -m, this.render(Math.min(0, -this._delay))), this
			}, h._enabled = function (a, b) {
				if (j || i.wake(), a && this._gc) {
					var c, d = this._targets;
					if (d)
						for (c = d.length; --c > -1;) this._siblings[c] = Z(d[c], this, !0);
					else this._siblings = Z(this.target, this, !0)
				}
				return D.prototype._enabled.call(this, a, b), this._notifyPluginsOfEnabled && this._firstPT ? G._onPluginEvent(a ? "_onEnable" : "_onDisable", this) : !1
			}, G.to = function (a, b, c) {
				return new G(a, b, c)
			}, G.from = function (a, b, c) {
				return c.runBackwards = !0, c.immediateRender = 0 != c.immediateRender, new G(a, b, c)
			}, G.fromTo = function (a, b, c, d) {
				return d.startAt = c, d.immediateRender = 0 != d.immediateRender && 0 != c.immediateRender, new G(a, b, d)
			}, G.delayedCall = function (a, b, c, d, e) {
				return new G(b, 0, {
					delay: a,
					onComplete: b,
					onCompleteParams: c,
					callbackScope: d,
					onReverseComplete: b,
					onReverseCompleteParams: c,
					immediateRender: !1,
					lazy: !1,
					useFrames: e,
					overwrite: 0
				})
			}, G.set = function (a, b) {
				return new G(a, 0, b)
			}, G.getTweensOf = function (a, b) {
				if (null == a) return [];
				a = "string" != typeof a ? a : G.selector(a) || a;
				var c, d, e, f;
				if ((p(a) || H(a)) && "number" != typeof a[0]) {
					for (c = a.length, d = []; --c > -1;) d = d.concat(G.getTweensOf(a[c], b));
					for (c = d.length; --c > -1;)
						for (f = d[c], e = c; --e > -1;) f === d[e] && d.splice(c, 1)
				} else
					for (d = Z(a).concat(), c = d.length; --c > -1;)(d[c]._gc || b && !d[c].isActive()) && d.splice(c, 1);
				return d
			}, G.killTweensOf = G.killDelayedCallsTo = function (a, b, c) {
				"object" == typeof b && (c = b, b = !1);
				for (var d = G.getTweensOf(a, b), e = d.length; --e > -1;) d[e]._kill(c, a)
			};
			var ba = t("plugins.TweenPlugin", function (a, b) {
				this._overwriteProps = (a || "").split(","), this._propName = this._overwriteProps[0], this._priority = b || 0, this._super = ba.prototype
			}, !0);
			if (h = ba.prototype, ba.version = "1.19.0", ba.API = 2, h._firstPT = null, h._addTween = O, h.setRatio = M, h._kill = function (a) {
					var b, c = this._overwriteProps,
						d = this._firstPT;
					if (null != a[this._propName]) this._overwriteProps = [];
					else
						for (b = c.length; --b > -1;) null != a[c[b]] && c.splice(b, 1);
					for (; d;) null != a[d.n] && (d._next && (d._next._prev = d._prev), d._prev ? (d._prev._next = d._next, d._prev = null) : this._firstPT === d && (this._firstPT = d._next)), d = d._next;
					return !1
				}, h._mod = h._roundProps = function (a) {
					for (var b, c = this._firstPT; c;) b = a[this._propName] || null != c.n && a[c.n.split(this._propName + "_").join("")], b && "function" == typeof b && (2 === c.f ? c.t._applyPT.m = b : c.m = b), c = c._next
				}, G._onPluginEvent = function (a, b) {
					var c, d, e, f, g, h = b._firstPT;
					if ("_onInitAllProps" === a) {
						for (; h;) {
							for (g = h._next, d = e; d && d.pr > h.pr;) d = d._next;
							(h._prev = d ? d._prev : f) ? h._prev._next = h: e = h, (h._next = d) ? d._prev = h : f = h, h = g
						}
						h = b._firstPT = e
					}
					for (; h;) h.pg && "function" == typeof h.t[a] && h.t[a]() && (c = !0), h = h._next;
					return c
				}, ba.activate = function (a) {
					for (var b = a.length; --b > -1;) a[b].API === ba.API && (Q[(new a[b])._propName] = a[b]);
					return !0
				}, s.plugin = function (a) {
					if (!(a && a.propName && a.init && a.API)) throw "illegal plugin definition.";
					var b, c = a.propName,
						d = a.priority || 0,
						e = a.overwriteProps,
						f = {
							init: "_onInitTween",
							set: "setRatio",
							kill: "_kill",
							round: "_mod",
							mod: "_mod",
							initAll: "_onInitAllProps"
						},
						g = t("plugins." + c.charAt(0).toUpperCase() + c.substr(1) + "Plugin", function () {
							ba.call(this, c, d), this._overwriteProps = e || []
						}, a.global === !0),
						h = g.prototype = new ba(c);
					h.constructor = g, g.API = a.API;
					for (b in f) "function" == typeof a[b] && (h[f[b]] = a[b]);
					return g.version = a.version, ba.activate([g]), g
				}, f = a._gsQueue) {
				for (g = 0; g < f.length; g++) f[g]();
				for (h in q) q[h].func || a.console.log("GSAP encountered missing dependency: " + h)
			}
			j = !1
		}
	}("undefined" != typeof module && module.exports && "undefined" != typeof global ? global : this || window, "TweenLite");
/*!
 * VERSION: 1.17.0
 * DATE: 2015-05-27
 * UPDATES AND DOCS AT: http://greensock.com
 *
 * @license Copyright (c) 2008-2015, GreenSock. All rights reserved.
 * This work is subject to the terms at http://greensock.com/standard-license or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 *
 * @author: Jack Doyle, jack@greensock.com
 */
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : this || window;
(_gsScope._gsQueue || (_gsScope._gsQueue = [])).push(function () {
		"use strict";
		_gsScope._gsDefine("TimelineLite", ["core.Animation", "core.SimpleTimeline", "TweenLite"], function (t, e, i) {
			var s = function (t) {
					e.call(this, t), this._labels = {}, this.autoRemoveChildren = this.vars.autoRemoveChildren === !0, this.smoothChildTiming = this.vars.smoothChildTiming === !0, this._sortChildren = !0, this._onUpdate = this.vars.onUpdate;
					var i, s, r = this.vars;
					for (s in r) i = r[s], h(i) && -1 !== i.join("").indexOf("{self}") && (r[s] = this._swapSelfInParams(i));
					h(r.tweens) && this.add(r.tweens, 0, r.align, r.stagger)
				},
				r = 1e-10,
				n = i._internals,
				a = s._internals = {},
				o = n.isSelector,
				h = n.isArray,
				l = n.lazyTweens,
				_ = n.lazyRender,
				u = [],
				f = _gsScope._gsDefine.globals,
				c = function (t) {
					var e, i = {};
					for (e in t) i[e] = t[e];
					return i
				},
				p = a.pauseCallback = function (t, e, i, s) {
					var n, a = t._timeline,
						o = a._totalTime,
						h = t._startTime,
						l = 0 > t._rawPrevTime || 0 === t._rawPrevTime && a._reversed,
						_ = l ? 0 : r,
						f = l ? r : 0;
					if (e || !this._forcingPlayhead) {
						for (a.pause(h), n = t._prev; n && n._startTime === h;) n._rawPrevTime = f, n = n._prev;
						for (n = t._next; n && n._startTime === h;) n._rawPrevTime = _, n = n._next;
						e && e.apply(s || a.vars.callbackScope || a, i || u), (this._forcingPlayhead || !a._paused) && a.seek(o)
					}
				},
				m = function (t) {
					var e, i = [],
						s = t.length;
					for (e = 0; e !== s; i.push(t[e++]));
					return i
				},
				d = s.prototype = new e;
			return s.version = "1.17.0", d.constructor = s, d.kill()._gc = d._forcingPlayhead = !1, d.to = function (t, e, s, r) {
				var n = s.repeat && f.TweenMax || i;
				return e ? this.add(new n(t, e, s), r) : this.set(t, s, r)
			}, d.from = function (t, e, s, r) {
				return this.add((s.repeat && f.TweenMax || i).from(t, e, s), r)
			}, d.fromTo = function (t, e, s, r, n) {
				var a = r.repeat && f.TweenMax || i;
				return e ? this.add(a.fromTo(t, e, s, r), n) : this.set(t, r, n)
			}, d.staggerTo = function (t, e, r, n, a, h, l, _) {
				var u, f = new s({
					onComplete: h,
					onCompleteParams: l,
					callbackScope: _,
					smoothChildTiming: this.smoothChildTiming
				});
				for ("string" == typeof t && (t = i.selector(t) || t), t = t || [], o(t) && (t = m(t)), n = n || 0, 0 > n && (t = m(t), t.reverse(), n *= -1), u = 0; t.length > u; u++) r.startAt && (r.startAt = c(r.startAt)), f.to(t[u], e, c(r), u * n);
				return this.add(f, a)
			}, d.staggerFrom = function (t, e, i, s, r, n, a, o) {
				return i.immediateRender = 0 != i.immediateRender, i.runBackwards = !0, this.staggerTo(t, e, i, s, r, n, a, o)
			}, d.staggerFromTo = function (t, e, i, s, r, n, a, o, h) {
				return s.startAt = i, s.immediateRender = 0 != s.immediateRender && 0 != i.immediateRender, this.staggerTo(t, e, s, r, n, a, o, h)
			}, d.call = function (t, e, s, r) {
				return this.add(i.delayedCall(0, t, e, s), r)
			}, d.set = function (t, e, s) {
				return s = this._parseTimeOrLabel(s, 0, !0), null == e.immediateRender && (e.immediateRender = s === this._time && !this._paused), this.add(new i(t, 0, e), s)
			}, s.exportRoot = function (t, e) {
				t = t || {}, null == t.smoothChildTiming && (t.smoothChildTiming = !0);
				var r, n, a = new s(t),
					o = a._timeline;
				for (null == e && (e = !0), o._remove(a, !0), a._startTime = 0, a._rawPrevTime = a._time = a._totalTime = o._time, r = o._first; r;) n = r._next, e && r instanceof i && r.target === r.vars.onComplete || a.add(r, r._startTime - r._delay), r = n;
				return o.add(a, 0), a
			}, d.add = function (r, n, a, o) {
				var l, _, u, f, c, p;
				if ("number" != typeof n && (n = this._parseTimeOrLabel(n, 0, !0, r)), !(r instanceof t)) {
					if (r instanceof Array || r && r.push && h(r)) {
						for (a = a || "normal", o = o || 0, l = n, _ = r.length, u = 0; _ > u; u++) h(f = r[u]) && (f = new s({
							tweens: f
						})), this.add(f, l), "string" != typeof f && "function" != typeof f && ("sequence" === a ? l = f._startTime + f.totalDuration() / f._timeScale : "start" === a && (f._startTime -= f.delay())), l += o;
						return this._uncache(!0)
					}
					if ("string" == typeof r) return this.addLabel(r, n);
					if ("function" != typeof r) throw "Cannot add " + r + " into the timeline; it is not a tween, timeline, function, or string.";
					r = i.delayedCall(0, r)
				}
				if (e.prototype.add.call(this, r, n), (this._gc || this._time === this._duration) && !this._paused && this._duration < this.duration())
					for (c = this, p = c.rawTime() > r._startTime; c._timeline;) p && c._timeline.smoothChildTiming ? c.totalTime(c._totalTime, !0) : c._gc && c._enabled(!0, !1), c = c._timeline;
				return this
			}, d.remove = function (e) {
				if (e instanceof t) return this._remove(e, !1);
				if (e instanceof Array || e && e.push && h(e)) {
					for (var i = e.length; --i > -1;) this.remove(e[i]);
					return this
				}
				return "string" == typeof e ? this.removeLabel(e) : this.kill(null, e)
			}, d._remove = function (t, i) {
				e.prototype._remove.call(this, t, i);
				var s = this._last;
				return s ? this._time > s._startTime + s._totalDuration / s._timeScale && (this._time = this.duration(), this._totalTime = this._totalDuration) : this._time = this._totalTime = this._duration = this._totalDuration = 0, this
			}, d.append = function (t, e) {
				return this.add(t, this._parseTimeOrLabel(null, e, !0, t))
			}, d.insert = d.insertMultiple = function (t, e, i, s) {
				return this.add(t, e || 0, i, s)
			}, d.appendMultiple = function (t, e, i, s) {
				return this.add(t, this._parseTimeOrLabel(null, e, !0, t), i, s)
			}, d.addLabel = function (t, e) {
				return this._labels[t] = this._parseTimeOrLabel(e), this
			}, d.addPause = function (t, e, s, r) {
				var n = i.delayedCall(0, p, ["{self}", e, s, r], this);
				return n.data = "isPause", this.add(n, t)
			}, d.removeLabel = function (t) {
				return delete this._labels[t], this
			}, d.getLabelTime = function (t) {
				return null != this._labels[t] ? this._labels[t] : -1
			}, d._parseTimeOrLabel = function (e, i, s, r) {
				var n;
				if (r instanceof t && r.timeline === this) this.remove(r);
				else if (r && (r instanceof Array || r.push && h(r)))
					for (n = r.length; --n > -1;) r[n] instanceof t && r[n].timeline === this && this.remove(r[n]);
				if ("string" == typeof i) return this._parseTimeOrLabel(i, s && "number" == typeof e && null == this._labels[i] ? e - this.duration() : 0, s);
				if (i = i || 0, "string" != typeof e || !isNaN(e) && null == this._labels[e]) null == e && (e = this.duration());
				else {
					if (n = e.indexOf("="), -1 === n) return null == this._labels[e] ? s ? this._labels[e] = this.duration() + i : i : this._labels[e] + i;
					i = parseInt(e.charAt(n - 1) + "1", 10) * Number(e.substr(n + 1)), e = n > 1 ? this._parseTimeOrLabel(e.substr(0, n - 1), 0, s) : this.duration()
				}
				return Number(e) + i
			}, d.seek = function (t, e) {
				return this.totalTime("number" == typeof t ? t : this._parseTimeOrLabel(t), e !== !1)
			}, d.stop = function () {
				return this.paused(!0)
			}, d.gotoAndPlay = function (t, e) {
				return this.play(t, e)
			}, d.gotoAndStop = function (t, e) {
				return this.pause(t, e)
			}, d.render = function (t, e, i) {
				this._gc && this._enabled(!0, !1);
				var s, n, a, o, h, u = this._dirty ? this.totalDuration() : this._totalDuration,
					f = this._time,
					c = this._startTime,
					p = this._timeScale,
					m = this._paused;
				if (t >= u) this._totalTime = this._time = u, this._reversed || this._hasPausedChild() || (n = !0, o = "onComplete", h = !!this._timeline.autoRemoveChildren, 0 === this._duration && (0 === t || 0 > this._rawPrevTime || this._rawPrevTime === r) && this._rawPrevTime !== t && this._first && (h = !0, this._rawPrevTime > r && (o = "onReverseComplete"))), this._rawPrevTime = this._duration || !e || t || this._rawPrevTime === t ? t : r, t = u + 1e-4;
				else if (1e-7 > t)
					if (this._totalTime = this._time = 0, (0 !== f || 0 === this._duration && this._rawPrevTime !== r && (this._rawPrevTime > 0 || 0 > t && this._rawPrevTime >= 0)) && (o = "onReverseComplete", n = this._reversed), 0 > t) this._active = !1, this._timeline.autoRemoveChildren && this._reversed ? (h = n = !0, o = "onReverseComplete") : this._rawPrevTime >= 0 && this._first && (h = !0), this._rawPrevTime = t;
					else {
						if (this._rawPrevTime = this._duration || !e || t || this._rawPrevTime === t ? t : r, 0 === t && n)
							for (s = this._first; s && 0 === s._startTime;) s._duration || (n = !1), s = s._next;
						t = 0, this._initted || (h = !0)
					}
				else this._totalTime = this._time = this._rawPrevTime = t;
				if (this._time !== f && this._first || i || h) {
					if (this._initted || (this._initted = !0), this._active || !this._paused && this._time !== f && t > 0 && (this._active = !0), 0 === f && this.vars.onStart && 0 !== this._time && (e || this._callback("onStart")), this._time >= f)
						for (s = this._first; s && (a = s._next, !this._paused || m);)(s._active || s._startTime <= this._time && !s._paused && !s._gc) && (s._reversed ? s.render((s._dirty ? s.totalDuration() : s._totalDuration) - (t - s._startTime) * s._timeScale, e, i) : s.render((t - s._startTime) * s._timeScale, e, i)), s = a;
					else
						for (s = this._last; s && (a = s._prev, !this._paused || m);)(s._active || f >= s._startTime && !s._paused && !s._gc) && (s._reversed ? s.render((s._dirty ? s.totalDuration() : s._totalDuration) - (t - s._startTime) * s._timeScale, e, i) : s.render((t - s._startTime) * s._timeScale, e, i)), s = a;
					this._onUpdate && (e || (l.length && _(), this._callback("onUpdate"))), o && (this._gc || (c === this._startTime || p !== this._timeScale) && (0 === this._time || u >= this.totalDuration()) && (n && (l.length && _(), this._timeline.autoRemoveChildren && this._enabled(!1, !1), this._active = !1), !e && this.vars[o] && this._callback(o)))
				}
			}, d._hasPausedChild = function () {
				for (var t = this._first; t;) {
					if (t._paused || t instanceof s && t._hasPausedChild()) return !0;
					t = t._next
				}
				return !1
			}, d.getChildren = function (t, e, s, r) {
				r = r || -9999999999;
				for (var n = [], a = this._first, o = 0; a;) r > a._startTime || (a instanceof i ? e !== !1 && (n[o++] = a) : (s !== !1 && (n[o++] = a), t !== !1 && (n = n.concat(a.getChildren(!0, e, s)), o = n.length))), a = a._next;
				return n
			}, d.getTweensOf = function (t, e) {
				var s, r, n = this._gc,
					a = [],
					o = 0;
				for (n && this._enabled(!0, !0), s = i.getTweensOf(t), r = s.length; --r > -1;)(s[r].timeline === this || e && this._contains(s[r])) && (a[o++] = s[r]);
				return n && this._enabled(!1, !0), a
			}, d.recent = function () {
				return this._recent
			}, d._contains = function (t) {
				for (var e = t.timeline; e;) {
					if (e === this) return !0;
					e = e.timeline
				}
				return !1
			}, d.shiftChildren = function (t, e, i) {
				i = i || 0;
				for (var s, r = this._first, n = this._labels; r;) r._startTime >= i && (r._startTime += t), r = r._next;
				if (e)
					for (s in n) n[s] >= i && (n[s] += t);
				return this._uncache(!0)
			}, d._kill = function (t, e) {
				if (!t && !e) return this._enabled(!1, !1);
				for (var i = e ? this.getTweensOf(e) : this.getChildren(!0, !0, !1), s = i.length, r = !1; --s > -1;) i[s]._kill(t, e) && (r = !0);
				return r
			}, d.clear = function (t) {
				var e = this.getChildren(!1, !0, !0),
					i = e.length;
				for (this._time = this._totalTime = 0; --i > -1;) e[i]._enabled(!1, !1);
				return t !== !1 && (this._labels = {}), this._uncache(!0)
			}, d.invalidate = function () {
				for (var e = this._first; e;) e.invalidate(), e = e._next;
				return t.prototype.invalidate.call(this)
			}, d._enabled = function (t, i) {
				if (t === this._gc)
					for (var s = this._first; s;) s._enabled(t, !0), s = s._next;
				return e.prototype._enabled.call(this, t, i)
			}, d.totalTime = function () {
				this._forcingPlayhead = !0;
				var e = t.prototype.totalTime.apply(this, arguments);
				return this._forcingPlayhead = !1, e
			}, d.duration = function (t) {
				return arguments.length ? (0 !== this.duration() && 0 !== t && this.timeScale(this._duration / t), this) : (this._dirty && this.totalDuration(), this._duration)
			}, d.totalDuration = function (t) {
				if (!arguments.length) {
					if (this._dirty) {
						for (var e, i, s = 0, r = this._last, n = 999999999999; r;) e = r._prev, r._dirty && r.totalDuration(), r._startTime > n && this._sortChildren && !r._paused ? this.add(r, r._startTime - r._delay) : n = r._startTime, 0 > r._startTime && !r._paused && (s -= r._startTime, this._timeline.smoothChildTiming && (this._startTime += r._startTime / this._timeScale), this.shiftChildren(-r._startTime, !1, -9999999999), n = 0), i = r._startTime + r._totalDuration / r._timeScale, i > s && (s = i), r = e;
						this._duration = this._totalDuration = s, this._dirty = !1
					}
					return this._totalDuration
				}
				return 0 !== this.totalDuration() && 0 !== t && this.timeScale(this._totalDuration / t), this
			}, d.paused = function (e) {
				if (!e)
					for (var i = this._first, s = this._time; i;) i._startTime === s && "isPause" === i.data && (i._rawPrevTime = 0), i = i._next;
				return t.prototype.paused.apply(this, arguments)
			}, d.usesFrames = function () {
				for (var e = this._timeline; e._timeline;) e = e._timeline;
				return e === t._rootFramesTimeline
			}, d.rawTime = function () {
				return this._paused ? this._totalTime : (this._timeline.rawTime() - this._startTime) * this._timeScale
			}, s
		}, !0)
	}), _gsScope._gsDefine && _gsScope._gsQueue.pop()(),
	function (t) {
		"use strict";
		var e = function () {
			return (_gsScope.GreenSockGlobals || _gsScope)[t]
		};
		"function" == typeof define && define.amd ? define(["TweenLite"], e) : "undefined" != typeof module && module.exports && (require("./TweenLite.js"), module.exports = e())
	}("TimelineLite");
/*!
 * VERSION: 1.15.5
 * DATE: 2016-07-08
 * UPDATES AND DOCS AT: http://greensock.com
 *
 * @license Copyright (c) 2008-2016, GreenSock. All rights reserved.
 * This work is subject to the terms at http://greensock.com/standard-license or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 *
 * @author: Jack Doyle, jack@greensock.com
 **/
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : this || window;
(_gsScope._gsQueue || (_gsScope._gsQueue = [])).push(function () {
		"use strict";
		_gsScope._gsDefine("easing.Back", ["easing.Ease"], function (a) {
			var b, c, d, e = _gsScope.GreenSockGlobals || _gsScope,
				f = e.com.greensock,
				g = 2 * Math.PI,
				h = Math.PI / 2,
				i = f._class,
				j = function (b, c) {
					var d = i("easing." + b, function () {}, !0),
						e = d.prototype = new a;
					return e.constructor = d, e.getRatio = c, d
				},
				k = a.register || function () {},
				l = function (a, b, c, d, e) {
					var f = i("easing." + a, {
						easeOut: new b,
						easeIn: new c,
						easeInOut: new d
					}, !0);
					return k(f, a), f
				},
				m = function (a, b, c) {
					this.t = a, this.v = b, c && (this.next = c, c.prev = this, this.c = c.v - b, this.gap = c.t - a)
				},
				n = function (b, c) {
					var d = i("easing." + b, function (a) {
							this._p1 = a || 0 === a ? a : 1.70158, this._p2 = 1.525 * this._p1
						}, !0),
						e = d.prototype = new a;
					return e.constructor = d, e.getRatio = c, e.config = function (a) {
						return new d(a)
					}, d
				},
				o = l("Back", n("BackOut", function (a) {
					return (a -= 1) * a * ((this._p1 + 1) * a + this._p1) + 1
				}), n("BackIn", function (a) {
					return a * a * ((this._p1 + 1) * a - this._p1)
				}), n("BackInOut", function (a) {
					return (a *= 2) < 1 ? .5 * a * a * ((this._p2 + 1) * a - this._p2) : .5 * ((a -= 2) * a * ((this._p2 + 1) * a + this._p2) + 2)
				})),
				p = i("easing.SlowMo", function (a, b, c) {
					b = b || 0 === b ? b : .7, null == a ? a = .7 : a > 1 && (a = 1), this._p = 1 !== a ? b : 0, this._p1 = (1 - a) / 2, this._p2 = a, this._p3 = this._p1 + this._p2, this._calcEnd = c === !0
				}, !0),
				q = p.prototype = new a;
			return q.constructor = p, q.getRatio = function (a) {
				var b = a + (.5 - a) * this._p;
				return a < this._p1 ? this._calcEnd ? 1 - (a = 1 - a / this._p1) * a : b - (a = 1 - a / this._p1) * a * a * a * b : a > this._p3 ? this._calcEnd ? 1 - (a = (a - this._p3) / this._p1) * a : b + (a - b) * (a = (a - this._p3) / this._p1) * a * a * a : this._calcEnd ? 1 : b
			}, p.ease = new p(.7, .7), q.config = p.config = function (a, b, c) {
				return new p(a, b, c)
			}, b = i("easing.SteppedEase", function (a) {
				a = a || 1, this._p1 = 1 / a, this._p2 = a + 1
			}, !0), q = b.prototype = new a, q.constructor = b, q.getRatio = function (a) {
				return 0 > a ? a = 0 : a >= 1 && (a = .999999999), (this._p2 * a >> 0) * this._p1
			}, q.config = b.config = function (a) {
				return new b(a)
			}, c = i("easing.RoughEase", function (b) {
				b = b || {};
				for (var c, d, e, f, g, h, i = b.taper || "none", j = [], k = 0, l = 0 | (b.points || 20), n = l, o = b.randomize !== !1, p = b.clamp === !0, q = b.template instanceof a ? b.template : null, r = "number" == typeof b.strength ? .4 * b.strength : .4; --n > -1;) c = o ? Math.random() : 1 / l * n, d = q ? q.getRatio(c) : c, "none" === i ? e = r : "out" === i ? (f = 1 - c, e = f * f * r) : "in" === i ? e = c * c * r : .5 > c ? (f = 2 * c, e = f * f * .5 * r) : (f = 2 * (1 - c), e = f * f * .5 * r), o ? d += Math.random() * e - .5 * e : n % 2 ? d += .5 * e : d -= .5 * e, p && (d > 1 ? d = 1 : 0 > d && (d = 0)), j[k++] = {
					x: c,
					y: d
				};
				for (j.sort(function (a, b) {
						return a.x - b.x
					}), h = new m(1, 1, null), n = l; --n > -1;) g = j[n], h = new m(g.x, g.y, h);
				this._prev = new m(0, 0, 0 !== h.t ? h : h.next)
			}, !0), q = c.prototype = new a, q.constructor = c, q.getRatio = function (a) {
				var b = this._prev;
				if (a > b.t) {
					for (; b.next && a >= b.t;) b = b.next;
					b = b.prev
				} else
					for (; b.prev && a <= b.t;) b = b.prev;
				return this._prev = b, b.v + (a - b.t) / b.gap * b.c
			}, q.config = function (a) {
				return new c(a)
			}, c.ease = new c, l("Bounce", j("BounceOut", function (a) {
				return 1 / 2.75 > a ? 7.5625 * a * a : 2 / 2.75 > a ? 7.5625 * (a -= 1.5 / 2.75) * a + .75 : 2.5 / 2.75 > a ? 7.5625 * (a -= 2.25 / 2.75) * a + .9375 : 7.5625 * (a -= 2.625 / 2.75) * a + .984375
			}), j("BounceIn", function (a) {
				return (a = 1 - a) < 1 / 2.75 ? 1 - 7.5625 * a * a : 2 / 2.75 > a ? 1 - (7.5625 * (a -= 1.5 / 2.75) * a + .75) : 2.5 / 2.75 > a ? 1 - (7.5625 * (a -= 2.25 / 2.75) * a + .9375) : 1 - (7.5625 * (a -= 2.625 / 2.75) * a + .984375)
			}), j("BounceInOut", function (a) {
				var b = .5 > a;
				return a = b ? 1 - 2 * a : 2 * a - 1, a = 1 / 2.75 > a ? 7.5625 * a * a : 2 / 2.75 > a ? 7.5625 * (a -= 1.5 / 2.75) * a + .75 : 2.5 / 2.75 > a ? 7.5625 * (a -= 2.25 / 2.75) * a + .9375 : 7.5625 * (a -= 2.625 / 2.75) * a + .984375, b ? .5 * (1 - a) : .5 * a + .5
			})), l("Circ", j("CircOut", function (a) {
				return Math.sqrt(1 - (a -= 1) * a)
			}), j("CircIn", function (a) {
				return -(Math.sqrt(1 - a * a) - 1)
			}), j("CircInOut", function (a) {
				return (a *= 2) < 1 ? -.5 * (Math.sqrt(1 - a * a) - 1) : .5 * (Math.sqrt(1 - (a -= 2) * a) + 1)
			})), d = function (b, c, d) {
				var e = i("easing." + b, function (a, b) {
						this._p1 = a >= 1 ? a : 1, this._p2 = (b || d) / (1 > a ? a : 1), this._p3 = this._p2 / g * (Math.asin(1 / this._p1) || 0), this._p2 = g / this._p2
					}, !0),
					f = e.prototype = new a;
				return f.constructor = e, f.getRatio = c, f.config = function (a, b) {
					return new e(a, b)
				}, e
			}, l("Elastic", d("ElasticOut", function (a) {
				return this._p1 * Math.pow(2, -10 * a) * Math.sin((a - this._p3) * this._p2) + 1
			}, .3), d("ElasticIn", function (a) {
				return -(this._p1 * Math.pow(2, 10 * (a -= 1)) * Math.sin((a - this._p3) * this._p2))
			}, .3), d("ElasticInOut", function (a) {
				return (a *= 2) < 1 ? -.5 * (this._p1 * Math.pow(2, 10 * (a -= 1)) * Math.sin((a - this._p3) * this._p2)) : this._p1 * Math.pow(2, -10 * (a -= 1)) * Math.sin((a - this._p3) * this._p2) * .5 + 1
			}, .45)), l("Expo", j("ExpoOut", function (a) {
				return 1 - Math.pow(2, -10 * a)
			}), j("ExpoIn", function (a) {
				return Math.pow(2, 10 * (a - 1)) - .001
			}), j("ExpoInOut", function (a) {
				return (a *= 2) < 1 ? .5 * Math.pow(2, 10 * (a - 1)) : .5 * (2 - Math.pow(2, -10 * (a - 1)))
			})), l("Sine", j("SineOut", function (a) {
				return Math.sin(a * h)
			}), j("SineIn", function (a) {
				return -Math.cos(a * h) + 1
			}), j("SineInOut", function (a) {
				return -.5 * (Math.cos(Math.PI * a) - 1)
			})), i("easing.EaseLookup", {
				find: function (b) {
					return a.map[b]
				}
			}, !0), k(e.SlowMo, "SlowMo", "ease,"), k(c, "RoughEase", "ease,"), k(b, "SteppedEase", "ease,"), o
		}, !0)
	}), _gsScope._gsDefine && _gsScope._gsQueue.pop()(),
	function () {
		"use strict";
		var a = function () {
			return _gsScope.GreenSockGlobals || _gsScope
		};
		"function" == typeof define && define.amd ? define(["TweenLite"], a) : "undefined" != typeof module && module.exports && (require("../TweenLite.js"), module.exports = a())
	}();
/*!
 * VERSION: 1.19.1
 * DATE: 2017-01-17
 * UPDATES AND DOCS AT: http://greensock.com
 *
 * @license Copyright (c) 2008-2017, GreenSock. All rights reserved.
 * This work is subject to the terms at http://greensock.com/standard-license or for
 * Club GreenSock members, the software agreement that was issued with your membership.
 *
 * @author: Jack Doyle, jack@greensock.com
 */
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : this || window;
(_gsScope._gsQueue || (_gsScope._gsQueue = [])).push(function () {
		"use strict";
		_gsScope._gsDefine("plugins.CSSPlugin", ["plugins.TweenPlugin", "TweenLite"], function (a, b) {
			var c, d, e, f, g = function () {
					a.call(this, "css"), this._overwriteProps.length = 0, this.setRatio = g.prototype.setRatio
				},
				h = _gsScope._gsDefine.globals,
				i = {},
				j = g.prototype = new a("css");
			j.constructor = g, g.version = "1.19.1", g.API = 2, g.defaultTransformPerspective = 0, g.defaultSkewType = "compensated", g.defaultSmoothOrigin = !0, j = "px", g.suffixMap = {
				top: j,
				right: j,
				bottom: j,
				left: j,
				width: j,
				height: j,
				fontSize: j,
				padding: j,
				margin: j,
				perspective: j,
				lineHeight: ""
			};
			var k, l, m, n, o, p, q, r, s = /(?:\-|\.|\b)(\d|\.|e\-)+/g,
				t = /(?:\d|\-\d|\.\d|\-\.\d|\+=\d|\-=\d|\+=.\d|\-=\.\d)+/g,
				u = /(?:\+=|\-=|\-|\b)[\d\-\.]+[a-zA-Z0-9]*(?:%|\b)/gi,
				v = /(?![+-]?\d*\.?\d+|[+-]|e[+-]\d+)[^0-9]/g,
				w = /(?:\d|\-|\+|=|#|\.)*/g,
				x = /opacity *= *([^)]*)/i,
				y = /opacity:([^;]*)/i,
				z = /alpha\(opacity *=.+?\)/i,
				A = /^(rgb|hsl)/,
				B = /([A-Z])/g,
				C = /-([a-z])/gi,
				D = /(^(?:url\(\"|url\())|(?:(\"\))$|\)$)/gi,
				E = function (a, b) {
					return b.toUpperCase()
				},
				F = /(?:Left|Right|Width)/i,
				G = /(M11|M12|M21|M22)=[\d\-\.e]+/gi,
				H = /progid\:DXImageTransform\.Microsoft\.Matrix\(.+?\)/i,
				I = /,(?=[^\)]*(?:\(|$))/gi,
				J = /[\s,\(]/i,
				K = Math.PI / 180,
				L = 180 / Math.PI,
				M = {},
				N = {
					style: {}
				},
				O = _gsScope.document || {
					createElement: function () {
						return N
					}
				},
				P = function (a, b) {
					return O.createElementNS ? O.createElementNS(b || "http://www.w3.org/1999/xhtml", a) : O.createElement(a)
				},
				Q = P("div"),
				R = P("img"),
				S = g._internals = {
					_specialProps: i
				},
				T = (_gsScope.navigator || {}).userAgent || "",
				U = function () {
					var a = T.indexOf("Android"),
						b = P("a");
					return m = -1 !== T.indexOf("Safari") && -1 === T.indexOf("Chrome") && (-1 === a || parseFloat(T.substr(a + 8, 2)) > 3), o = m && parseFloat(T.substr(T.indexOf("Version/") + 8, 2)) < 6, n = -1 !== T.indexOf("Firefox"), (/MSIE ([0-9]{1,}[\.0-9]{0,})/.exec(T) || /Trident\/.*rv:([0-9]{1,}[\.0-9]{0,})/.exec(T)) && (p = parseFloat(RegExp.$1)), b ? (b.style.cssText = "top:1px;opacity:.55;", /^0.55/.test(b.style.opacity)) : !1
				}(),
				V = function (a) {
					return x.test("string" == typeof a ? a : (a.currentStyle ? a.currentStyle.filter : a.style.filter) || "") ? parseFloat(RegExp.$1) / 100 : 1
				},
				W = function (a) {
					_gsScope.console && console.log(a)
				},
				X = "",
				Y = "",
				Z = function (a, b) {
					b = b || Q;
					var c, d, e = b.style;
					if (void 0 !== e[a]) return a;
					for (a = a.charAt(0).toUpperCase() + a.substr(1), c = ["O", "Moz", "ms", "Ms", "Webkit"], d = 5; --d > -1 && void 0 === e[c[d] + a];);
					return d >= 0 ? (Y = 3 === d ? "ms" : c[d], X = "-" + Y.toLowerCase() + "-", Y + a) : null
				},
				$ = O.defaultView ? O.defaultView.getComputedStyle : function () {},
				_ = g.getStyle = function (a, b, c, d, e) {
					var f;
					return U || "opacity" !== b ? (!d && a.style[b] ? f = a.style[b] : (c = c || $(a)) ? f = c[b] || c.getPropertyValue(b) || c.getPropertyValue(b.replace(B, "-$1").toLowerCase()) : a.currentStyle && (f = a.currentStyle[b]), null == e || f && "none" !== f && "auto" !== f && "auto auto" !== f ? f : e) : V(a)
				},
				aa = S.convertToPixels = function (a, c, d, e, f) {
					if ("px" === e || !e) return d;
					if ("auto" === e || !d) return 0;
					var h, i, j, k = F.test(c),
						l = a,
						m = Q.style,
						n = 0 > d,
						o = 1 === d;
					if (n && (d = -d), o && (d *= 100), "%" === e && -1 !== c.indexOf("border")) h = d / 100 * (k ? a.clientWidth : a.clientHeight);
					else {
						if (m.cssText = "border:0 solid red;position:" + _(a, "position") + ";line-height:0;", "%" !== e && l.appendChild && "v" !== e.charAt(0) && "rem" !== e) m[k ? "borderLeftWidth" : "borderTopWidth"] = d + e;
						else {
							if (l = a.parentNode || O.body, i = l._gsCache, j = b.ticker.frame, i && k && i.time === j) return i.width * d / 100;
							m[k ? "width" : "height"] = d + e
						}
						l.appendChild(Q), h = parseFloat(Q[k ? "offsetWidth" : "offsetHeight"]), l.removeChild(Q), k && "%" === e && g.cacheWidths !== !1 && (i = l._gsCache = l._gsCache || {}, i.time = j, i.width = h / d * 100), 0 !== h || f || (h = aa(a, c, d, e, !0))
					}
					return o && (h /= 100), n ? -h : h
				},
				ba = S.calculateOffset = function (a, b, c) {
					if ("absolute" !== _(a, "position", c)) return 0;
					var d = "left" === b ? "Left" : "Top",
						e = _(a, "margin" + d, c);
					return a["offset" + d] - (aa(a, b, parseFloat(e), e.replace(w, "")) || 0)
				},
				ca = function (a, b) {
					var c, d, e, f = {};
					if (b = b || $(a, null))
						if (c = b.length)
							for (; --c > -1;) e = b[c], (-1 === e.indexOf("-transform") || Da === e) && (f[e.replace(C, E)] = b.getPropertyValue(e));
						else
							for (c in b)(-1 === c.indexOf("Transform") || Ca === c) && (f[c] = b[c]);
					else if (b = a.currentStyle || a.style)
						for (c in b) "string" == typeof c && void 0 === f[c] && (f[c.replace(C, E)] = b[c]);
					return U || (f.opacity = V(a)), d = Ra(a, b, !1), f.rotation = d.rotation, f.skewX = d.skewX, f.scaleX = d.scaleX, f.scaleY = d.scaleY, f.x = d.x, f.y = d.y, Fa && (f.z = d.z, f.rotationX = d.rotationX, f.rotationY = d.rotationY, f.scaleZ = d.scaleZ), f.filters && delete f.filters, f
				},
				da = function (a, b, c, d, e) {
					var f, g, h, i = {},
						j = a.style;
					for (g in c) "cssText" !== g && "length" !== g && isNaN(g) && (b[g] !== (f = c[g]) || e && e[g]) && -1 === g.indexOf("Origin") && ("number" == typeof f || "string" == typeof f) && (i[g] = "auto" !== f || "left" !== g && "top" !== g ? "" !== f && "auto" !== f && "none" !== f || "string" != typeof b[g] || "" === b[g].replace(v, "") ? f : 0 : ba(a, g), void 0 !== j[g] && (h = new sa(j, g, j[g], h)));
					if (d)
						for (g in d) "className" !== g && (i[g] = d[g]);
					return {
						difs: i,
						firstMPT: h
					}
				},
				ea = {
					width: ["Left", "Right"],
					height: ["Top", "Bottom"]
				},
				fa = ["marginLeft", "marginRight", "marginTop", "marginBottom"],
				ga = function (a, b, c) {
					if ("svg" === (a.nodeName + "").toLowerCase()) return (c || $(a))[b] || 0;
					if (a.getCTM && Oa(a)) return a.getBBox()[b] || 0;
					var d = parseFloat("width" === b ? a.offsetWidth : a.offsetHeight),
						e = ea[b],
						f = e.length;
					for (c = c || $(a, null); --f > -1;) d -= parseFloat(_(a, "padding" + e[f], c, !0)) || 0, d -= parseFloat(_(a, "border" + e[f] + "Width", c, !0)) || 0;
					return d
				},
				ha = function (a, b) {
					if ("contain" === a || "auto" === a || "auto auto" === a) return a + " ";
					(null == a || "" === a) && (a = "0 0");
					var c, d = a.split(" "),
						e = -1 !== a.indexOf("left") ? "0%" : -1 !== a.indexOf("right") ? "100%" : d[0],
						f = -1 !== a.indexOf("top") ? "0%" : -1 !== a.indexOf("bottom") ? "100%" : d[1];
					if (d.length > 3 && !b) {
						for (d = a.split(", ").join(",").split(","), a = [], c = 0; c < d.length; c++) a.push(ha(d[c]));
						return a.join(",")
					}
					return null == f ? f = "center" === e ? "50%" : "0" : "center" === f && (f = "50%"), ("center" === e || isNaN(parseFloat(e)) && -1 === (e + "").indexOf("=")) && (e = "50%"), a = e + " " + f + (d.length > 2 ? " " + d[2] : ""), b && (b.oxp = -1 !== e.indexOf("%"), b.oyp = -1 !== f.indexOf("%"), b.oxr = "=" === e.charAt(1), b.oyr = "=" === f.charAt(1), b.ox = parseFloat(e.replace(v, "")), b.oy = parseFloat(f.replace(v, "")), b.v = a), b || a
				},
				ia = function (a, b) {
					return "function" == typeof a && (a = a(r, q)), "string" == typeof a && "=" === a.charAt(1) ? parseInt(a.charAt(0) + "1", 10) * parseFloat(a.substr(2)) : parseFloat(a) - parseFloat(b) || 0
				},
				ja = function (a, b) {
					return "function" == typeof a && (a = a(r, q)), null == a ? b : "string" == typeof a && "=" === a.charAt(1) ? parseInt(a.charAt(0) + "1", 10) * parseFloat(a.substr(2)) + b : parseFloat(a) || 0
				},
				ka = function (a, b, c, d) {
					var e, f, g, h, i, j = 1e-6;
					return "function" == typeof a && (a = a(r, q)), null == a ? h = b : "number" == typeof a ? h = a : (e = 360, f = a.split("_"), i = "=" === a.charAt(1), g = (i ? parseInt(a.charAt(0) + "1", 10) * parseFloat(f[0].substr(2)) : parseFloat(f[0])) * (-1 === a.indexOf("rad") ? 1 : L) - (i ? 0 : b), f.length && (d && (d[c] = b + g), -1 !== a.indexOf("short") && (g %= e, g !== g % (e / 2) && (g = 0 > g ? g + e : g - e)), -1 !== a.indexOf("_cw") && 0 > g ? g = (g + 9999999999 * e) % e - (g / e | 0) * e : -1 !== a.indexOf("ccw") && g > 0 && (g = (g - 9999999999 * e) % e - (g / e | 0) * e)), h = b + g), j > h && h > -j && (h = 0), h
				},
				la = {
					aqua: [0, 255, 255],
					lime: [0, 255, 0],
					silver: [192, 192, 192],
					black: [0, 0, 0],
					maroon: [128, 0, 0],
					teal: [0, 128, 128],
					blue: [0, 0, 255],
					navy: [0, 0, 128],
					white: [255, 255, 255],
					fuchsia: [255, 0, 255],
					olive: [128, 128, 0],
					yellow: [255, 255, 0],
					orange: [255, 165, 0],
					gray: [128, 128, 128],
					purple: [128, 0, 128],
					green: [0, 128, 0],
					red: [255, 0, 0],
					pink: [255, 192, 203],
					cyan: [0, 255, 255],
					transparent: [255, 255, 255, 0]
				},
				ma = function (a, b, c) {
					return a = 0 > a ? a + 1 : a > 1 ? a - 1 : a, 255 * (1 > 6 * a ? b + (c - b) * a * 6 : .5 > a ? c : 2 > 3 * a ? b + (c - b) * (2 / 3 - a) * 6 : b) + .5 | 0
				},
				na = g.parseColor = function (a, b) {
					var c, d, e, f, g, h, i, j, k, l, m;
					if (a)
						if ("number" == typeof a) c = [a >> 16, a >> 8 & 255, 255 & a];
						else {
							if ("," === a.charAt(a.length - 1) && (a = a.substr(0, a.length - 1)), la[a]) c = la[a];
							else if ("#" === a.charAt(0)) 4 === a.length && (d = a.charAt(1), e = a.charAt(2), f = a.charAt(3), a = "#" + d + d + e + e + f + f), a = parseInt(a.substr(1), 16), c = [a >> 16, a >> 8 & 255, 255 & a];
							else if ("hsl" === a.substr(0, 3))
								if (c = m = a.match(s), b) {
									if (-1 !== a.indexOf("=")) return a.match(t)
								} else g = Number(c[0]) % 360 / 360, h = Number(c[1]) / 100, i = Number(c[2]) / 100, e = .5 >= i ? i * (h + 1) : i + h - i * h, d = 2 * i - e, c.length > 3 && (c[3] = Number(a[3])), c[0] = ma(g + 1 / 3, d, e), c[1] = ma(g, d, e), c[2] = ma(g - 1 / 3, d, e);
							else c = a.match(s) || la.transparent;
							c[0] = Number(c[0]), c[1] = Number(c[1]), c[2] = Number(c[2]), c.length > 3 && (c[3] = Number(c[3]))
						}
					else c = la.black;
					return b && !m && (d = c[0] / 255, e = c[1] / 255, f = c[2] / 255, j = Math.max(d, e, f), k = Math.min(d, e, f), i = (j + k) / 2, j === k ? g = h = 0 : (l = j - k, h = i > .5 ? l / (2 - j - k) : l / (j + k), g = j === d ? (e - f) / l + (f > e ? 6 : 0) : j === e ? (f - d) / l + 2 : (d - e) / l + 4, g *= 60), c[0] = g + .5 | 0, c[1] = 100 * h + .5 | 0, c[2] = 100 * i + .5 | 0), c
				},
				oa = function (a, b) {
					var c, d, e, f = a.match(pa) || [],
						g = 0,
						h = f.length ? "" : a;
					for (c = 0; c < f.length; c++) d = f[c], e = a.substr(g, a.indexOf(d, g) - g), g += e.length + d.length, d = na(d, b), 3 === d.length && d.push(1), h += e + (b ? "hsla(" + d[0] + "," + d[1] + "%," + d[2] + "%," + d[3] : "rgba(" + d.join(",")) + ")";
					return h + a.substr(g)
				},
				pa = "(?:\\b(?:(?:rgb|rgba|hsl|hsla)\\(.+?\\))|\\B#(?:[0-9a-f]{3}){1,2}\\b";
			for (j in la) pa += "|" + j + "\\b";
			pa = new RegExp(pa + ")", "gi"), g.colorStringFilter = function (a) {
				var b, c = a[0] + a[1];
				pa.test(c) && (b = -1 !== c.indexOf("hsl(") || -1 !== c.indexOf("hsla("), a[0] = oa(a[0], b), a[1] = oa(a[1], b)), pa.lastIndex = 0
			}, b.defaultStringFilter || (b.defaultStringFilter = g.colorStringFilter);
			var qa = function (a, b, c, d) {
					if (null == a) return function (a) {
						return a
					};
					var e, f = b ? (a.match(pa) || [""])[0] : "",
						g = a.split(f).join("").match(u) || [],
						h = a.substr(0, a.indexOf(g[0])),
						i = ")" === a.charAt(a.length - 1) ? ")" : "",
						j = -1 !== a.indexOf(" ") ? " " : ",",
						k = g.length,
						l = k > 0 ? g[0].replace(s, "") : "";
					return k ? e = b ? function (a) {
						var b, m, n, o;
						if ("number" == typeof a) a += l;
						else if (d && I.test(a)) {
							for (o = a.replace(I, "|").split("|"), n = 0; n < o.length; n++) o[n] = e(o[n]);
							return o.join(",")
						}
						if (b = (a.match(pa) || [f])[0], m = a.split(b).join("").match(u) || [], n = m.length, k > n--)
							for (; ++n < k;) m[n] = c ? m[(n - 1) / 2 | 0] : g[n];
						return h + m.join(j) + j + b + i + (-1 !== a.indexOf("inset") ? " inset" : "")
					} : function (a) {
						var b, f, m;
						if ("number" == typeof a) a += l;
						else if (d && I.test(a)) {
							for (f = a.replace(I, "|").split("|"), m = 0; m < f.length; m++) f[m] = e(f[m]);
							return f.join(",")
						}
						if (b = a.match(u) || [], m = b.length, k > m--)
							for (; ++m < k;) b[m] = c ? b[(m - 1) / 2 | 0] : g[m];
						return h + b.join(j) + i
					} : function (a) {
						return a
					}
				},
				ra = function (a) {
					return a = a.split(","),
						function (b, c, d, e, f, g, h) {
							var i, j = (c + "").split(" ");
							for (h = {}, i = 0; 4 > i; i++) h[a[i]] = j[i] = j[i] || j[(i - 1) / 2 >> 0];
							return e.parse(b, h, f, g)
						}
				},
				sa = (S._setPluginRatio = function (a) {
					this.plugin.setRatio(a);
					for (var b, c, d, e, f, g = this.data, h = g.proxy, i = g.firstMPT, j = 1e-6; i;) b = h[i.v], i.r ? b = Math.round(b) : j > b && b > -j && (b = 0), i.t[i.p] = b, i = i._next;
					if (g.autoRotate && (g.autoRotate.rotation = g.mod ? g.mod(h.rotation, this.t) : h.rotation), 1 === a || 0 === a)
						for (i = g.firstMPT, f = 1 === a ? "e" : "b"; i;) {
							if (c = i.t, c.type) {
								if (1 === c.type) {
									for (e = c.xs0 + c.s + c.xs1, d = 1; d < c.l; d++) e += c["xn" + d] + c["xs" + (d + 1)];
									c[f] = e
								}
							} else c[f] = c.s + c.xs0;
							i = i._next
						}
				}, function (a, b, c, d, e) {
					this.t = a, this.p = b, this.v = c, this.r = e, d && (d._prev = this, this._next = d)
				}),
				ta = (S._parseToProxy = function (a, b, c, d, e, f) {
					var g, h, i, j, k, l = d,
						m = {},
						n = {},
						o = c._transform,
						p = M;
					for (c._transform = null, M = b, d = k = c.parse(a, b, d, e), M = p, f && (c._transform = o, l && (l._prev = null, l._prev && (l._prev._next = null))); d && d !== l;) {
						if (d.type <= 1 && (h = d.p, n[h] = d.s + d.c, m[h] = d.s, f || (j = new sa(d, "s", h, j, d.r), d.c = 0), 1 === d.type))
							for (g = d.l; --g > 0;) i = "xn" + g, h = d.p + "_" + i, n[h] = d.data[i], m[h] = d[i], f || (j = new sa(d, i, h, j, d.rxp[i]));
						d = d._next
					}
					return {
						proxy: m,
						end: n,
						firstMPT: j,
						pt: k
					}
				}, S.CSSPropTween = function (a, b, d, e, g, h, i, j, k, l, m) {
					this.t = a, this.p = b, this.s = d, this.c = e, this.n = i || b, a instanceof ta || f.push(this.n), this.r = j, this.type = h || 0, k && (this.pr = k, c = !0), this.b = void 0 === l ? d : l, this.e = void 0 === m ? d + e : m, g && (this._next = g, g._prev = this)
				}),
				ua = function (a, b, c, d, e, f) {
					var g = new ta(a, b, c, d - c, e, -1, f);
					return g.b = c, g.e = g.xs0 = d, g
				},
				va = g.parseComplex = function (a, b, c, d, e, f, h, i, j, l) {
					c = c || f || "", "function" == typeof d && (d = d(r, q)), h = new ta(a, b, 0, 0, h, l ? 2 : 1, null, !1, i, c, d), d += "", e && pa.test(d + c) && (d = [c, d], g.colorStringFilter(d), c = d[0], d = d[1]);
					var m, n, o, p, u, v, w, x, y, z, A, B, C, D = c.split(", ").join(",").split(" "),
						E = d.split(", ").join(",").split(" "),
						F = D.length,
						G = k !== !1;
					for ((-1 !== d.indexOf(",") || -1 !== c.indexOf(",")) && (D = D.join(" ").replace(I, ", ").split(" "), E = E.join(" ").replace(I, ", ").split(" "), F = D.length), F !== E.length && (D = (f || "").split(" "), F = D.length), h.plugin = j, h.setRatio = l, pa.lastIndex = 0, m = 0; F > m; m++)
						if (p = D[m], u = E[m], x = parseFloat(p), x || 0 === x) h.appendXtra("", x, ia(u, x), u.replace(t, ""), G && -1 !== u.indexOf("px"), !0);
						else if (e && pa.test(p)) B = u.indexOf(")") + 1, B = ")" + (B ? u.substr(B) : ""), C = -1 !== u.indexOf("hsl") && U, p = na(p, C), u = na(u, C), y = p.length + u.length > 6, y && !U && 0 === u[3] ? (h["xs" + h.l] += h.l ? " transparent" : "transparent", h.e = h.e.split(E[m]).join("transparent")) : (U || (y = !1), C ? h.appendXtra(y ? "hsla(" : "hsl(", p[0], ia(u[0], p[0]), ",", !1, !0).appendXtra("", p[1], ia(u[1], p[1]), "%,", !1).appendXtra("", p[2], ia(u[2], p[2]), y ? "%," : "%" + B, !1) : h.appendXtra(y ? "rgba(" : "rgb(", p[0], u[0] - p[0], ",", !0, !0).appendXtra("", p[1], u[1] - p[1], ",", !0).appendXtra("", p[2], u[2] - p[2], y ? "," : B, !0), y && (p = p.length < 4 ? 1 : p[3], h.appendXtra("", p, (u.length < 4 ? 1 : u[3]) - p, B, !1))), pa.lastIndex = 0;
					else if (v = p.match(s)) {
						if (w = u.match(t), !w || w.length !== v.length) return h;
						for (o = 0, n = 0; n < v.length; n++) A = v[n], z = p.indexOf(A, o), h.appendXtra(p.substr(o, z - o), Number(A), ia(w[n], A), "", G && "px" === p.substr(z + A.length, 2), 0 === n), o = z + A.length;
						h["xs" + h.l] += p.substr(o)
					} else h["xs" + h.l] += h.l || h["xs" + h.l] ? " " + u : u;
					if (-1 !== d.indexOf("=") && h.data) {
						for (B = h.xs0 + h.data.s, m = 1; m < h.l; m++) B += h["xs" + m] + h.data["xn" + m];
						h.e = B + h["xs" + m]
					}
					return h.l || (h.type = -1, h.xs0 = h.e), h.xfirst || h
				},
				wa = 9;
			for (j = ta.prototype, j.l = j.pr = 0; --wa > 0;) j["xn" + wa] = 0, j["xs" + wa] = "";
			j.xs0 = "", j._next = j._prev = j.xfirst = j.data = j.plugin = j.setRatio = j.rxp = null, j.appendXtra = function (a, b, c, d, e, f) {
				var g = this,
					h = g.l;
				return g["xs" + h] += f && (h || g["xs" + h]) ? " " + a : a || "", c || 0 === h || g.plugin ? (g.l++, g.type = g.setRatio ? 2 : 1, g["xs" + g.l] = d || "", h > 0 ? (g.data["xn" + h] = b + c, g.rxp["xn" + h] = e, g["xn" + h] = b, g.plugin || (g.xfirst = new ta(g, "xn" + h, b, c, g.xfirst || g, 0, g.n, e, g.pr), g.xfirst.xs0 = 0), g) : (g.data = {
					s: b + c
				}, g.rxp = {}, g.s = b, g.c = c, g.r = e, g)) : (g["xs" + h] += b + (d || ""), g)
			};
			var xa = function (a, b) {
					b = b || {}, this.p = b.prefix ? Z(a) || a : a, i[a] = i[this.p] = this, this.format = b.formatter || qa(b.defaultValue, b.color, b.collapsible, b.multi), b.parser && (this.parse = b.parser), this.clrs = b.color, this.multi = b.multi, this.keyword = b.keyword, this.dflt = b.defaultValue, this.pr = b.priority || 0
				},
				ya = S._registerComplexSpecialProp = function (a, b, c) {
					"object" != typeof b && (b = {
						parser: c
					});
					var d, e, f = a.split(","),
						g = b.defaultValue;
					for (c = c || [g], d = 0; d < f.length; d++) b.prefix = 0 === d && b.prefix, b.defaultValue = c[d] || g, e = new xa(f[d], b)
				},
				za = S._registerPluginProp = function (a) {
					if (!i[a]) {
						var b = a.charAt(0).toUpperCase() + a.substr(1) + "Plugin";
						ya(a, {
							parser: function (a, c, d, e, f, g, j) {
								var k = h.com.greensock.plugins[b];
								return k ? (k._cssRegister(), i[d].parse(a, c, d, e, f, g, j)) : (W("Error: " + b + " js file not loaded."), f)
							}
						})
					}
				};
			j = xa.prototype, j.parseComplex = function (a, b, c, d, e, f) {
				var g, h, i, j, k, l, m = this.keyword;
				if (this.multi && (I.test(c) || I.test(b) ? (h = b.replace(I, "|").split("|"), i = c.replace(I, "|").split("|")) : m && (h = [b], i = [c])), i) {
					for (j = i.length > h.length ? i.length : h.length, g = 0; j > g; g++) b = h[g] = h[g] || this.dflt, c = i[g] = i[g] || this.dflt, m && (k = b.indexOf(m), l = c.indexOf(m), k !== l && (-1 === l ? h[g] = h[g].split(m).join("") : -1 === k && (h[g] += " " + m)));
					b = h.join(", "), c = i.join(", ")
				}
				return va(a, this.p, b, c, this.clrs, this.dflt, d, this.pr, e, f)
			}, j.parse = function (a, b, c, d, f, g, h) {
				return this.parseComplex(a.style, this.format(_(a, this.p, e, !1, this.dflt)), this.format(b), f, g)
			}, g.registerSpecialProp = function (a, b, c) {
				ya(a, {
					parser: function (a, d, e, f, g, h, i) {
						var j = new ta(a, e, 0, 0, g, 2, e, !1, c);
						return j.plugin = h, j.setRatio = b(a, d, f._tween, e), j
					},
					priority: c
				})
			}, g.useSVGTransformAttr = !0;
			var Aa, Ba = "scaleX,scaleY,scaleZ,x,y,z,skewX,skewY,rotation,rotationX,rotationY,perspective,xPercent,yPercent".split(","),
				Ca = Z("transform"),
				Da = X + "transform",
				Ea = Z("transformOrigin"),
				Fa = null !== Z("perspective"),
				Ga = S.Transform = function () {
					this.perspective = parseFloat(g.defaultTransformPerspective) || 0, this.force3D = g.defaultForce3D !== !1 && Fa ? g.defaultForce3D || "auto" : !1
				},
				Ha = _gsScope.SVGElement,
				Ia = function (a, b, c) {
					var d, e = O.createElementNS("http://www.w3.org/2000/svg", a),
						f = /([a-z])([A-Z])/g;
					for (d in c) e.setAttributeNS(null, d.replace(f, "$1-$2").toLowerCase(), c[d]);
					return b.appendChild(e), e
				},
				Ja = O.documentElement || {},
				Ka = function () {
					var a, b, c, d = p || /Android/i.test(T) && !_gsScope.chrome;
					return O.createElementNS && !d && (a = Ia("svg", Ja), b = Ia("rect", a, {
						width: 100,
						height: 50,
						x: 100
					}), c = b.getBoundingClientRect().width, b.style[Ea] = "50% 50%", b.style[Ca] = "scaleX(0.5)", d = c === b.getBoundingClientRect().width && !(n && Fa), Ja.removeChild(a)), d
				}(),
				La = function (a, b, c, d, e, f) {
					var h, i, j, k, l, m, n, o, p, q, r, s, t, u, v = a._gsTransform,
						w = Qa(a, !0);
					v && (t = v.xOrigin, u = v.yOrigin), (!d || (h = d.split(" ")).length < 2) && (n = a.getBBox(), 0 === n.x && 0 === n.y && n.width + n.height === 0 && (n = {
						x: parseFloat(a.hasAttribute("x") ? a.getAttribute("x") : a.hasAttribute("cx") ? a.getAttribute("cx") : 0) || 0,
						y: parseFloat(a.hasAttribute("y") ? a.getAttribute("y") : a.hasAttribute("cy") ? a.getAttribute("cy") : 0) || 0,
						width: 0,
						height: 0
					}), b = ha(b).split(" "), h = [(-1 !== b[0].indexOf("%") ? parseFloat(b[0]) / 100 * n.width : parseFloat(b[0])) + n.x, (-1 !== b[1].indexOf("%") ? parseFloat(b[1]) / 100 * n.height : parseFloat(b[1])) + n.y]), c.xOrigin = k = parseFloat(h[0]), c.yOrigin = l = parseFloat(h[1]), d && w !== Pa && (m = w[0], n = w[1], o = w[2], p = w[3], q = w[4], r = w[5], s = m * p - n * o, s && (i = k * (p / s) + l * (-o / s) + (o * r - p * q) / s, j = k * (-n / s) + l * (m / s) - (m * r - n * q) / s, k = c.xOrigin = h[0] = i, l = c.yOrigin = h[1] = j)), v && (f && (c.xOffset = v.xOffset, c.yOffset = v.yOffset, v = c), e || e !== !1 && g.defaultSmoothOrigin !== !1 ? (i = k - t, j = l - u, v.xOffset += i * w[0] + j * w[2] - i, v.yOffset += i * w[1] + j * w[3] - j) : v.xOffset = v.yOffset = 0), f || a.setAttribute("data-svg-origin", h.join(" "))
				},
				Ma = function (a) {
					var b, c = P("svg", this.ownerSVGElement.getAttribute("xmlns") || "http://www.w3.org/2000/svg"),
						d = this.parentNode,
						e = this.nextSibling,
						f = this.style.cssText;
					if (Ja.appendChild(c), c.appendChild(this), this.style.display = "block", a) try {
						b = this.getBBox(), this._originalGetBBox = this.getBBox, this.getBBox = Ma
					} catch (g) {} else this._originalGetBBox && (b = this._originalGetBBox());
					return e ? d.insertBefore(this, e) : d.appendChild(this), Ja.removeChild(c), this.style.cssText = f, b
				},
				Na = function (a) {
					try {
						return a.getBBox()
					} catch (b) {
						return Ma.call(a, !0)
					}
				},
				Oa = function (a) {
					return !(!(Ha && a.getCTM && Na(a)) || a.parentNode && !a.ownerSVGElement)
				},
				Pa = [1, 0, 0, 1, 0, 0],
				Qa = function (a, b) {
					var c, d, e, f, g, h, i = a._gsTransform || new Ga,
						j = 1e5,
						k = a.style;
					if (Ca ? d = _(a, Da, null, !0) : a.currentStyle && (d = a.currentStyle.filter.match(G), d = d && 4 === d.length ? [d[0].substr(4), Number(d[2].substr(4)), Number(d[1].substr(4)), d[3].substr(4), i.x || 0, i.y || 0].join(",") : ""), c = !d || "none" === d || "matrix(1, 0, 0, 1, 0, 0)" === d, c && Ca && ((h = "none" === $(a).display) || !a.parentNode) && (h && (f = k.display, k.display = "block"), a.parentNode || (g = 1, Ja.appendChild(a)), d = _(a, Da, null, !0), c = !d || "none" === d || "matrix(1, 0, 0, 1, 0, 0)" === d, f ? k.display = f : h && Va(k, "display"), g && Ja.removeChild(a)), (i.svg || a.getCTM && Oa(a)) && (c && -1 !== (k[Ca] + "").indexOf("matrix") && (d = k[Ca], c = 0), e = a.getAttribute("transform"), c && e && (-1 !== e.indexOf("matrix") ? (d = e, c = 0) : -1 !== e.indexOf("translate") && (d = "matrix(1,0,0,1," + e.match(/(?:\-|\b)[\d\-\.e]+\b/gi).join(",") + ")", c = 0))), c) return Pa;
					for (e = (d || "").match(s) || [], wa = e.length; --wa > -1;) f = Number(e[wa]), e[wa] = (g = f - (f |= 0)) ? (g * j + (0 > g ? -.5 : .5) | 0) / j + f : f;
					return b && e.length > 6 ? [e[0], e[1], e[4], e[5], e[12], e[13]] : e
				},
				Ra = S.getTransform = function (a, c, d, e) {
					if (a._gsTransform && d && !e) return a._gsTransform;
					var f, h, i, j, k, l, m = d ? a._gsTransform || new Ga : new Ga,
						n = m.scaleX < 0,
						o = 2e-5,
						p = 1e5,
						q = Fa ? parseFloat(_(a, Ea, c, !1, "0 0 0").split(" ")[2]) || m.zOrigin || 0 : 0,
						r = parseFloat(g.defaultTransformPerspective) || 0;
					if (m.svg = !(!a.getCTM || !Oa(a)), m.svg && (La(a, _(a, Ea, c, !1, "50% 50%") + "", m, a.getAttribute("data-svg-origin")), Aa = g.useSVGTransformAttr || Ka), f = Qa(a), f !== Pa) {
						if (16 === f.length) {
							var s, t, u, v, w, x = f[0],
								y = f[1],
								z = f[2],
								A = f[3],
								B = f[4],
								C = f[5],
								D = f[6],
								E = f[7],
								F = f[8],
								G = f[9],
								H = f[10],
								I = f[12],
								J = f[13],
								K = f[14],
								M = f[11],
								N = Math.atan2(D, H);
							m.zOrigin && (K = -m.zOrigin, I = F * K - f[12], J = G * K - f[13], K = H * K + m.zOrigin - f[14]), m.rotationX = N * L, N && (v = Math.cos(-N), w = Math.sin(-N), s = B * v + F * w, t = C * v + G * w, u = D * v + H * w, F = B * -w + F * v, G = C * -w + G * v, H = D * -w + H * v, M = E * -w + M * v, B = s, C = t, D = u), N = Math.atan2(-z, H), m.rotationY = N * L, N && (v = Math.cos(-N), w = Math.sin(-N), s = x * v - F * w, t = y * v - G * w, u = z * v - H * w, G = y * w + G * v, H = z * w + H * v, M = A * w + M * v, x = s, y = t, z = u), N = Math.atan2(y, x), m.rotation = N * L, N && (v = Math.cos(-N), w = Math.sin(-N), x = x * v + B * w, t = y * v + C * w, C = y * -w + C * v, D = z * -w + D * v, y = t), m.rotationX && Math.abs(m.rotationX) + Math.abs(m.rotation) > 359.9 && (m.rotationX = m.rotation = 0, m.rotationY = 180 - m.rotationY), m.scaleX = (Math.sqrt(x * x + y * y) * p + .5 | 0) / p, m.scaleY = (Math.sqrt(C * C + G * G) * p + .5 | 0) / p, m.scaleZ = (Math.sqrt(D * D + H * H) * p + .5 | 0) / p, m.rotationX || m.rotationY ? m.skewX = 0 : (m.skewX = B || C ? Math.atan2(B, C) * L + m.rotation : m.skewX || 0, Math.abs(m.skewX) > 90 && Math.abs(m.skewX) < 270 && (n ? (m.scaleX *= -1, m.skewX += m.rotation <= 0 ? 180 : -180, m.rotation += m.rotation <= 0 ? 180 : -180) : (m.scaleY *= -1, m.skewX += m.skewX <= 0 ? 180 : -180))), m.perspective = M ? 1 / (0 > M ? -M : M) : 0, m.x = I, m.y = J, m.z = K, m.svg && (m.x -= m.xOrigin - (m.xOrigin * x - m.yOrigin * B), m.y -= m.yOrigin - (m.yOrigin * y - m.xOrigin * C))
						} else if (!Fa || e || !f.length || m.x !== f[4] || m.y !== f[5] || !m.rotationX && !m.rotationY) {
							var O = f.length >= 6,
								P = O ? f[0] : 1,
								Q = f[1] || 0,
								R = f[2] || 0,
								S = O ? f[3] : 1;
							m.x = f[4] || 0, m.y = f[5] || 0, i = Math.sqrt(P * P + Q * Q), j = Math.sqrt(S * S + R * R), k = P || Q ? Math.atan2(Q, P) * L : m.rotation || 0, l = R || S ? Math.atan2(R, S) * L + k : m.skewX || 0, Math.abs(l) > 90 && Math.abs(l) < 270 && (n ? (i *= -1, l += 0 >= k ? 180 : -180, k += 0 >= k ? 180 : -180) : (j *= -1, l += 0 >= l ? 180 : -180)), m.scaleX = i, m.scaleY = j, m.rotation = k, m.skewX = l, Fa && (m.rotationX = m.rotationY = m.z = 0, m.perspective = r, m.scaleZ = 1), m.svg && (m.x -= m.xOrigin - (m.xOrigin * P + m.yOrigin * R), m.y -= m.yOrigin - (m.xOrigin * Q + m.yOrigin * S))
						}
						m.zOrigin = q;
						for (h in m) m[h] < o && m[h] > -o && (m[h] = 0)
					}
					return d && (a._gsTransform = m, m.svg && (Aa && a.style[Ca] ? b.delayedCall(.001, function () {
						Va(a.style, Ca)
					}) : !Aa && a.getAttribute("transform") && b.delayedCall(.001, function () {
						a.removeAttribute("transform")
					}))), m
				},
				Sa = function (a) {
					var b, c, d = this.data,
						e = -d.rotation * K,
						f = e + d.skewX * K,
						g = 1e5,
						h = (Math.cos(e) * d.scaleX * g | 0) / g,
						i = (Math.sin(e) * d.scaleX * g | 0) / g,
						j = (Math.sin(f) * -d.scaleY * g | 0) / g,
						k = (Math.cos(f) * d.scaleY * g | 0) / g,
						l = this.t.style,
						m = this.t.currentStyle;
					if (m) {
						c = i, i = -j, j = -c, b = m.filter, l.filter = "";
						var n, o, q = this.t.offsetWidth,
							r = this.t.offsetHeight,
							s = "absolute" !== m.position,
							t = "progid:DXImageTransform.Microsoft.Matrix(M11=" + h + ", M12=" + i + ", M21=" + j + ", M22=" + k,
							u = d.x + q * d.xPercent / 100,
							v = d.y + r * d.yPercent / 100;
						if (null != d.ox && (n = (d.oxp ? q * d.ox * .01 : d.ox) - q / 2, o = (d.oyp ? r * d.oy * .01 : d.oy) - r / 2, u += n - (n * h + o * i), v += o - (n * j + o * k)), s ? (n = q / 2, o = r / 2, t += ", Dx=" + (n - (n * h + o * i) + u) + ", Dy=" + (o - (n * j + o * k) + v) + ")") : t += ", sizingMethod='auto expand')", -1 !== b.indexOf("DXImageTransform.Microsoft.Matrix(") ? l.filter = b.replace(H, t) : l.filter = t + " " + b, (0 === a || 1 === a) && 1 === h && 0 === i && 0 === j && 1 === k && (s && -1 === t.indexOf("Dx=0, Dy=0") || x.test(b) && 100 !== parseFloat(RegExp.$1) || -1 === b.indexOf(b.indexOf("Alpha")) && l.removeAttribute("filter")), !s) {
							var y, z, A, B = 8 > p ? 1 : -1;
							for (n = d.ieOffsetX || 0, o = d.ieOffsetY || 0, d.ieOffsetX = Math.round((q - ((0 > h ? -h : h) * q + (0 > i ? -i : i) * r)) / 2 + u), d.ieOffsetY = Math.round((r - ((0 > k ? -k : k) * r + (0 > j ? -j : j) * q)) / 2 + v), wa = 0; 4 > wa; wa++) z = fa[wa], y = m[z], c = -1 !== y.indexOf("px") ? parseFloat(y) : aa(this.t, z, parseFloat(y), y.replace(w, "")) || 0, A = c !== d[z] ? 2 > wa ? -d.ieOffsetX : -d.ieOffsetY : 2 > wa ? n - d.ieOffsetX : o - d.ieOffsetY, l[z] = (d[z] = Math.round(c - A * (0 === wa || 2 === wa ? 1 : B))) + "px"
						}
					}
				},
				Ta = S.set3DTransformRatio = S.setTransformRatio = function (a) {
					var b, c, d, e, f, g, h, i, j, k, l, m, o, p, q, r, s, t, u, v, w, x, y, z = this.data,
						A = this.t.style,
						B = z.rotation,
						C = z.rotationX,
						D = z.rotationY,
						E = z.scaleX,
						F = z.scaleY,
						G = z.scaleZ,
						H = z.x,
						I = z.y,
						J = z.z,
						L = z.svg,
						M = z.perspective,
						N = z.force3D,
						O = z.skewY,
						P = z.skewX;
					if (O && (P += O, B += O), ((1 === a || 0 === a) && "auto" === N && (this.tween._totalTime === this.tween._totalDuration || !this.tween._totalTime) || !N) && !J && !M && !D && !C && 1 === G || Aa && L || !Fa) return void(B || P || L ? (B *= K, x = P * K, y = 1e5, c = Math.cos(B) * E, f = Math.sin(B) * E, d = Math.sin(B - x) * -F, g = Math.cos(B - x) * F, x && "simple" === z.skewType && (b = Math.tan(x - O * K), b = Math.sqrt(1 + b * b), d *= b, g *= b, O && (b = Math.tan(O * K), b = Math.sqrt(1 + b * b), c *= b, f *= b)), L && (H += z.xOrigin - (z.xOrigin * c + z.yOrigin * d) + z.xOffset, I += z.yOrigin - (z.xOrigin * f + z.yOrigin * g) + z.yOffset, Aa && (z.xPercent || z.yPercent) && (q = this.t.getBBox(), H += .01 * z.xPercent * q.width, I += .01 * z.yPercent * q.height), q = 1e-6, q > H && H > -q && (H = 0), q > I && I > -q && (I = 0)), u = (c * y | 0) / y + "," + (f * y | 0) / y + "," + (d * y | 0) / y + "," + (g * y | 0) / y + "," + H + "," + I + ")", L && Aa ? this.t.setAttribute("transform", "matrix(" + u) : A[Ca] = (z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) matrix(" : "matrix(") + u) : A[Ca] = (z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) matrix(" : "matrix(") + E + ",0,0," + F + "," + H + "," + I + ")");
					if (n && (q = 1e-4, q > E && E > -q && (E = G = 2e-5), q > F && F > -q && (F = G = 2e-5), !M || z.z || z.rotationX || z.rotationY || (M = 0)), B || P) B *= K, r = c = Math.cos(B), s = f = Math.sin(B), P && (B -= P * K, r = Math.cos(B), s = Math.sin(B), "simple" === z.skewType && (b = Math.tan((P - O) * K), b = Math.sqrt(1 + b * b), r *= b, s *= b, z.skewY && (b = Math.tan(O * K), b = Math.sqrt(1 + b * b), c *= b, f *= b))), d = -s, g = r;
					else {
						if (!(D || C || 1 !== G || M || L)) return void(A[Ca] = (z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) translate3d(" : "translate3d(") + H + "px," + I + "px," + J + "px)" + (1 !== E || 1 !== F ? " scale(" + E + "," + F + ")" : ""));
						c = g = 1, d = f = 0
					}
					k = 1, e = h = i = j = l = m = 0, o = M ? -1 / M : 0, p = z.zOrigin, q = 1e-6, v = ",", w = "0", B = D * K, B && (r = Math.cos(B), s = Math.sin(B), i = -s, l = o * -s, e = c * s, h = f * s, k = r, o *= r, c *= r, f *= r), B = C * K, B && (r = Math.cos(B), s = Math.sin(B), b = d * r + e * s, t = g * r + h * s, j = k * s, m = o * s, e = d * -s + e * r, h = g * -s + h * r, k *= r, o *= r, d = b, g = t), 1 !== G && (e *= G, h *= G, k *= G, o *= G), 1 !== F && (d *= F, g *= F, j *= F, m *= F), 1 !== E && (c *= E, f *= E, i *= E, l *= E), (p || L) && (p && (H += e * -p, I += h * -p, J += k * -p + p), L && (H += z.xOrigin - (z.xOrigin * c + z.yOrigin * d) + z.xOffset, I += z.yOrigin - (z.xOrigin * f + z.yOrigin * g) + z.yOffset), q > H && H > -q && (H = w), q > I && I > -q && (I = w), q > J && J > -q && (J = 0)), u = z.xPercent || z.yPercent ? "translate(" + z.xPercent + "%," + z.yPercent + "%) matrix3d(" : "matrix3d(", u += (q > c && c > -q ? w : c) + v + (q > f && f > -q ? w : f) + v + (q > i && i > -q ? w : i), u += v + (q > l && l > -q ? w : l) + v + (q > d && d > -q ? w : d) + v + (q > g && g > -q ? w : g), C || D || 1 !== G ? (u += v + (q > j && j > -q ? w : j) + v + (q > m && m > -q ? w : m) + v + (q > e && e > -q ? w : e), u += v + (q > h && h > -q ? w : h) + v + (q > k && k > -q ? w : k) + v + (q > o && o > -q ? w : o) + v) : u += ",0,0,0,0,1,0,", u += H + v + I + v + J + v + (M ? 1 + -J / M : 1) + ")", A[Ca] = u
				};
			j = Ga.prototype, j.x = j.y = j.z = j.skewX = j.skewY = j.rotation = j.rotationX = j.rotationY = j.zOrigin = j.xPercent = j.yPercent = j.xOffset = j.yOffset = 0, j.scaleX = j.scaleY = j.scaleZ = 1, ya("transform,scale,scaleX,scaleY,scaleZ,x,y,z,rotation,rotationX,rotationY,rotationZ,skewX,skewY,shortRotation,shortRotationX,shortRotationY,shortRotationZ,transformOrigin,svgOrigin,transformPerspective,directionalRotation,parseTransform,force3D,skewType,xPercent,yPercent,smoothOrigin", {
				parser: function (a, b, c, d, f, h, i) {
					if (d._lastParsedTransform === i) return f;
					d._lastParsedTransform = i;
					var j, k = i.scale && "function" == typeof i.scale ? i.scale : 0;
					"function" == typeof i[c] && (j = i[c], i[c] = b), k && (i.scale = k(r, a));
					var l, m, n, o, p, s, t, u, v, w = a._gsTransform,
						x = a.style,
						y = 1e-6,
						z = Ba.length,
						A = i,
						B = {},
						C = "transformOrigin",
						D = Ra(a, e, !0, A.parseTransform),
						E = A.transform && ("function" == typeof A.transform ? A.transform(r, q) : A.transform);
					if (d._transform = D, E && "string" == typeof E && Ca) m = Q.style, m[Ca] = E, m.display = "block", m.position = "absolute", O.body.appendChild(Q), l = Ra(Q, null, !1), D.svg && (s = D.xOrigin, t = D.yOrigin, l.x -= D.xOffset, l.y -= D.yOffset, (A.transformOrigin || A.svgOrigin) && (E = {}, La(a, ha(A.transformOrigin), E, A.svgOrigin, A.smoothOrigin, !0), s = E.xOrigin, t = E.yOrigin, l.x -= E.xOffset - D.xOffset, l.y -= E.yOffset - D.yOffset), (s || t) && (u = Qa(Q, !0), l.x -= s - (s * u[0] + t * u[2]), l.y -= t - (s * u[1] + t * u[3]))), O.body.removeChild(Q), l.perspective || (l.perspective = D.perspective), null != A.xPercent && (l.xPercent = ja(A.xPercent, D.xPercent)), null != A.yPercent && (l.yPercent = ja(A.yPercent, D.yPercent));
					else if ("object" == typeof A) {
						if (l = {
								scaleX: ja(null != A.scaleX ? A.scaleX : A.scale, D.scaleX),
								scaleY: ja(null != A.scaleY ? A.scaleY : A.scale, D.scaleY),
								scaleZ: ja(A.scaleZ, D.scaleZ),
								x: ja(A.x, D.x),
								y: ja(A.y, D.y),
								z: ja(A.z, D.z),
								xPercent: ja(A.xPercent, D.xPercent),
								yPercent: ja(A.yPercent, D.yPercent),
								perspective: ja(A.transformPerspective, D.perspective)
							}, p = A.directionalRotation, null != p)
							if ("object" == typeof p)
								for (m in p) A[m] = p[m];
							else A.rotation = p;
						"string" == typeof A.x && -1 !== A.x.indexOf("%") && (l.x = 0, l.xPercent = ja(A.x, D.xPercent)), "string" == typeof A.y && -1 !== A.y.indexOf("%") && (l.y = 0, l.yPercent = ja(A.y, D.yPercent)), l.rotation = ka("rotation" in A ? A.rotation : "shortRotation" in A ? A.shortRotation + "_short" : "rotationZ" in A ? A.rotationZ : D.rotation, D.rotation, "rotation", B), Fa && (l.rotationX = ka("rotationX" in A ? A.rotationX : "shortRotationX" in A ? A.shortRotationX + "_short" : D.rotationX || 0, D.rotationX, "rotationX", B), l.rotationY = ka("rotationY" in A ? A.rotationY : "shortRotationY" in A ? A.shortRotationY + "_short" : D.rotationY || 0, D.rotationY, "rotationY", B)), l.skewX = ka(A.skewX, D.skewX), l.skewY = ka(A.skewY, D.skewY)
					}
					for (Fa && null != A.force3D && (D.force3D = A.force3D, o = !0), D.skewType = A.skewType || D.skewType || g.defaultSkewType, n = D.force3D || D.z || D.rotationX || D.rotationY || l.z || l.rotationX || l.rotationY || l.perspective, n || null == A.scale || (l.scaleZ = 1); --z > -1;) v = Ba[z], E = l[v] - D[v], (E > y || -y > E || null != A[v] || null != M[v]) && (o = !0, f = new ta(D, v, D[v], E, f), v in B && (f.e = B[v]), f.xs0 = 0, f.plugin = h, d._overwriteProps.push(f.n));
					return E = A.transformOrigin, D.svg && (E || A.svgOrigin) && (s = D.xOffset, t = D.yOffset, La(a, ha(E), l, A.svgOrigin, A.smoothOrigin), f = ua(D, "xOrigin", (w ? D : l).xOrigin, l.xOrigin, f, C), f = ua(D, "yOrigin", (w ? D : l).yOrigin, l.yOrigin, f, C), (s !== D.xOffset || t !== D.yOffset) && (f = ua(D, "xOffset", w ? s : D.xOffset, D.xOffset, f, C), f = ua(D, "yOffset", w ? t : D.yOffset, D.yOffset, f, C)), E = "0px 0px"), (E || Fa && n && D.zOrigin) && (Ca ? (o = !0, v = Ea, E = (E || _(a, v, e, !1, "50% 50%")) + "", f = new ta(x, v, 0, 0, f, -1, C), f.b = x[v], f.plugin = h, Fa ? (m = D.zOrigin, E = E.split(" "), D.zOrigin = (E.length > 2 && (0 === m || "0px" !== E[2]) ? parseFloat(E[2]) : m) || 0, f.xs0 = f.e = E[0] + " " + (E[1] || "50%") + " 0px", f = new ta(D, "zOrigin", 0, 0, f, -1, f.n), f.b = m, f.xs0 = f.e = D.zOrigin) : f.xs0 = f.e = E) : ha(E + "", D)), o && (d._transformType = D.svg && Aa || !n && 3 !== this._transformType ? 2 : 3), j && (i[c] = j), k && (i.scale = k), f
				},
				prefix: !0
			}), ya("boxShadow", {
				defaultValue: "0px 0px 0px 0px #999",
				prefix: !0,
				color: !0,
				multi: !0,
				keyword: "inset"
			}), ya("borderRadius", {
				defaultValue: "0px",
				parser: function (a, b, c, f, g, h) {
					b = this.format(b);
					var i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y = ["borderTopLeftRadius", "borderTopRightRadius", "borderBottomRightRadius", "borderBottomLeftRadius"],
						z = a.style;
					for (q = parseFloat(a.offsetWidth), r = parseFloat(a.offsetHeight), i = b.split(" "), j = 0; j < y.length; j++) this.p.indexOf("border") && (y[j] = Z(y[j])), m = l = _(a, y[j], e, !1, "0px"), -1 !== m.indexOf(" ") && (l = m.split(" "), m = l[0], l = l[1]), n = k = i[j], o = parseFloat(m), t = m.substr((o + "").length), u = "=" === n.charAt(1), u ? (p = parseInt(n.charAt(0) + "1", 10), n = n.substr(2), p *= parseFloat(n), s = n.substr((p + "").length - (0 > p ? 1 : 0)) || "") : (p = parseFloat(n), s = n.substr((p + "").length)), "" === s && (s = d[c] || t), s !== t && (v = aa(a, "borderLeft", o, t), w = aa(a, "borderTop", o, t), "%" === s ? (m = v / q * 100 + "%", l = w / r * 100 + "%") : "em" === s ? (x = aa(a, "borderLeft", 1, "em"), m = v / x + "em", l = w / x + "em") : (m = v + "px", l = w + "px"), u && (n = parseFloat(m) + p + s, k = parseFloat(l) + p + s)), g = va(z, y[j], m + " " + l, n + " " + k, !1, "0px", g);
					return g
				},
				prefix: !0,
				formatter: qa("0px 0px 0px 0px", !1, !0)
			}), ya("borderBottomLeftRadius,borderBottomRightRadius,borderTopLeftRadius,borderTopRightRadius", {
				defaultValue: "0px",
				parser: function (a, b, c, d, f, g) {
					return va(a.style, c, this.format(_(a, c, e, !1, "0px 0px")), this.format(b), !1, "0px", f)
				},
				prefix: !0,
				formatter: qa("0px 0px", !1, !0)
			}), ya("backgroundPosition", {
				defaultValue: "0 0",
				parser: function (a, b, c, d, f, g) {
					var h, i, j, k, l, m, n = "background-position",
						o = e || $(a, null),
						q = this.format((o ? p ? o.getPropertyValue(n + "-x") + " " + o.getPropertyValue(n + "-y") : o.getPropertyValue(n) : a.currentStyle.backgroundPositionX + " " + a.currentStyle.backgroundPositionY) || "0 0"),
						r = this.format(b);
					if (-1 !== q.indexOf("%") != (-1 !== r.indexOf("%")) && r.split(",").length < 2 && (m = _(a, "backgroundImage").replace(D, ""), m && "none" !== m)) {
						for (h = q.split(" "), i = r.split(" "), R.setAttribute("src", m), j = 2; --j > -1;) q = h[j], k = -1 !== q.indexOf("%"), k !== (-1 !== i[j].indexOf("%")) && (l = 0 === j ? a.offsetWidth - R.width : a.offsetHeight - R.height, h[j] = k ? parseFloat(q) / 100 * l + "px" : parseFloat(q) / l * 100 + "%");
						q = h.join(" ")
					}
					return this.parseComplex(a.style, q, r, f, g)
				},
				formatter: ha
			}), ya("backgroundSize", {
				defaultValue: "0 0",
				formatter: function (a) {
					return a += "", ha(-1 === a.indexOf(" ") ? a + " " + a : a)
				}
			}), ya("perspective", {
				defaultValue: "0px",
				prefix: !0
			}), ya("perspectiveOrigin", {
				defaultValue: "50% 50%",
				prefix: !0
			}), ya("transformStyle", {
				prefix: !0
			}), ya("backfaceVisibility", {
				prefix: !0
			}), ya("userSelect", {
				prefix: !0
			}), ya("margin", {
				parser: ra("marginTop,marginRight,marginBottom,marginLeft")
			}), ya("padding", {
				parser: ra("paddingTop,paddingRight,paddingBottom,paddingLeft")
			}), ya("clip", {
				defaultValue: "rect(0px,0px,0px,0px)",
				parser: function (a, b, c, d, f, g) {
					var h, i, j;
					return 9 > p ? (i = a.currentStyle, j = 8 > p ? " " : ",", h = "rect(" + i.clipTop + j + i.clipRight + j + i.clipBottom + j + i.clipLeft + ")", b = this.format(b).split(",").join(j)) : (h = this.format(_(a, this.p, e, !1, this.dflt)), b = this.format(b)), this.parseComplex(a.style, h, b, f, g)
				}
			}), ya("textShadow", {
				defaultValue: "0px 0px 0px #999",
				color: !0,
				multi: !0
			}), ya("autoRound,strictUnits", {
				parser: function (a, b, c, d, e) {
					return e
				}
			}), ya("border", {
				defaultValue: "0px solid #000",
				parser: function (a, b, c, d, f, g) {
					var h = _(a, "borderTopWidth", e, !1, "0px"),
						i = this.format(b).split(" "),
						j = i[0].replace(w, "");
					return "px" !== j && (h = parseFloat(h) / aa(a, "borderTopWidth", 1, j) + j), this.parseComplex(a.style, this.format(h + " " + _(a, "borderTopStyle", e, !1, "solid") + " " + _(a, "borderTopColor", e, !1, "#000")), i.join(" "), f, g)
				},
				color: !0,
				formatter: function (a) {
					var b = a.split(" ");
					return b[0] + " " + (b[1] || "solid") + " " + (a.match(pa) || ["#000"])[0]
				}
			}), ya("borderWidth", {
				parser: ra("borderTopWidth,borderRightWidth,borderBottomWidth,borderLeftWidth")
			}), ya("float,cssFloat,styleFloat", {
				parser: function (a, b, c, d, e, f) {
					var g = a.style,
						h = "cssFloat" in g ? "cssFloat" : "styleFloat";
					return new ta(g, h, 0, 0, e, -1, c, !1, 0, g[h], b)
				}
			});
			var Ua = function (a) {
				var b, c = this.t,
					d = c.filter || _(this.data, "filter") || "",
					e = this.s + this.c * a | 0;
				100 === e && (-1 === d.indexOf("atrix(") && -1 === d.indexOf("radient(") && -1 === d.indexOf("oader(") ? (c.removeAttribute("filter"), b = !_(this.data, "filter")) : (c.filter = d.replace(z, ""), b = !0)), b || (this.xn1 && (c.filter = d = d || "alpha(opacity=" + e + ")"), -1 === d.indexOf("pacity") ? 0 === e && this.xn1 || (c.filter = d + " alpha(opacity=" + e + ")") : c.filter = d.replace(x, "opacity=" + e))
			};
			ya("opacity,alpha,autoAlpha", {
				defaultValue: "1",
				parser: function (a, b, c, d, f, g) {
					var h = parseFloat(_(a, "opacity", e, !1, "1")),
						i = a.style,
						j = "autoAlpha" === c;
					return "string" == typeof b && "=" === b.charAt(1) && (b = ("-" === b.charAt(0) ? -1 : 1) * parseFloat(b.substr(2)) + h), j && 1 === h && "hidden" === _(a, "visibility", e) && 0 !== b && (h = 0), U ? f = new ta(i, "opacity", h, b - h, f) : (f = new ta(i, "opacity", 100 * h, 100 * (b - h), f), f.xn1 = j ? 1 : 0, i.zoom = 1, f.type = 2, f.b = "alpha(opacity=" + f.s + ")", f.e = "alpha(opacity=" + (f.s + f.c) + ")", f.data = a, f.plugin = g, f.setRatio = Ua), j && (f = new ta(i, "visibility", 0, 0, f, -1, null, !1, 0, 0 !== h ? "inherit" : "hidden", 0 === b ? "hidden" : "inherit"), f.xs0 = "inherit", d._overwriteProps.push(f.n), d._overwriteProps.push(c)), f
				}
			});
			var Va = function (a, b) {
					b && (a.removeProperty ? (("ms" === b.substr(0, 2) || "webkit" === b.substr(0, 6)) && (b = "-" + b), a.removeProperty(b.replace(B, "-$1").toLowerCase())) : a.removeAttribute(b))
				},
				Wa = function (a) {
					if (this.t._gsClassPT = this, 1 === a || 0 === a) {
						this.t.setAttribute("class", 0 === a ? this.b : this.e);
						for (var b = this.data, c = this.t.style; b;) b.v ? c[b.p] = b.v : Va(c, b.p), b = b._next;
						1 === a && this.t._gsClassPT === this && (this.t._gsClassPT = null)
					} else this.t.getAttribute("class") !== this.e && this.t.setAttribute("class", this.e)
				};
			ya("className", {
				parser: function (a, b, d, f, g, h, i) {
					var j, k, l, m, n, o = a.getAttribute("class") || "",
						p = a.style.cssText;
					if (g = f._classNamePT = new ta(a, d, 0, 0, g, 2), g.setRatio = Wa, g.pr = -11, c = !0, g.b = o, k = ca(a, e), l = a._gsClassPT) {
						for (m = {}, n = l.data; n;) m[n.p] = 1, n = n._next;
						l.setRatio(1)
					}
					return a._gsClassPT = g, g.e = "=" !== b.charAt(1) ? b : o.replace(new RegExp("(?:\\s|^)" + b.substr(2) + "(?![\\w-])"), "") + ("+" === b.charAt(0) ? " " + b.substr(2) : ""), a.setAttribute("class", g.e), j = da(a, k, ca(a), i, m), a.setAttribute("class", o), g.data = j.firstMPT, a.style.cssText = p, g = g.xfirst = f.parse(a, j.difs, g, h)
				}
			});
			var Xa = function (a) {
				if ((1 === a || 0 === a) && this.data._totalTime === this.data._totalDuration && "isFromStart" !== this.data.data) {
					var b, c, d, e, f, g = this.t.style,
						h = i.transform.parse;
					if ("all" === this.e) g.cssText = "", e = !0;
					else
						for (b = this.e.split(" ").join("").split(","), d = b.length; --d > -1;) c = b[d], i[c] && (i[c].parse === h ? e = !0 : c = "transformOrigin" === c ? Ea : i[c].p), Va(g, c);
					e && (Va(g, Ca), f = this.t._gsTransform, f && (f.svg && (this.t.removeAttribute("data-svg-origin"), this.t.removeAttribute("transform")), delete this.t._gsTransform))
				}
			};
			for (ya("clearProps", {
					parser: function (a, b, d, e, f) {
						return f = new ta(a, d, 0, 0, f, 2), f.setRatio = Xa, f.e = b, f.pr = -10, f.data = e._tween, c = !0, f
					}
				}), j = "bezier,throwProps,physicsProps,physics2D".split(","), wa = j.length; wa--;) za(j[wa]);
			j = g.prototype, j._firstPT = j._lastParsedTransform = j._transform = null, j._onInitTween = function (a, b, h, j) {
				if (!a.nodeType) return !1;
				this._target = q = a, this._tween = h, this._vars = b, r = j, k = b.autoRound, c = !1, d = b.suffixMap || g.suffixMap, e = $(a, ""), f = this._overwriteProps;
				var n, p, s, t, u, v, w, x, z, A = a.style;
				if (l && "" === A.zIndex && (n = _(a, "zIndex", e), ("auto" === n || "" === n) && this._addLazySet(A, "zIndex", 0)), "string" == typeof b && (t = A.cssText, n = ca(a, e), A.cssText = t + ";" + b, n = da(a, n, ca(a)).difs, !U && y.test(b) && (n.opacity = parseFloat(RegExp.$1)), b = n, A.cssText = t), b.className ? this._firstPT = p = i.className.parse(a, b.className, "className", this, null, null, b) : this._firstPT = p = this.parse(a, b, null), this._transformType) {
					for (z = 3 === this._transformType, Ca ? m && (l = !0, "" === A.zIndex && (w = _(a, "zIndex", e), ("auto" === w || "" === w) && this._addLazySet(A, "zIndex", 0)), o && this._addLazySet(A, "WebkitBackfaceVisibility", this._vars.WebkitBackfaceVisibility || (z ? "visible" : "hidden"))) : A.zoom = 1, s = p; s && s._next;) s = s._next;
					x = new ta(a, "transform", 0, 0, null, 2), this._linkCSSP(x, null, s), x.setRatio = Ca ? Ta : Sa, x.data = this._transform || Ra(a, e, !0), x.tween = h, x.pr = -1, f.pop()
				}
				if (c) {
					for (; p;) {
						for (v = p._next, s = t; s && s.pr > p.pr;) s = s._next;
						(p._prev = s ? s._prev : u) ? p._prev._next = p: t = p, (p._next = s) ? s._prev = p : u = p, p = v
					}
					this._firstPT = t
				}
				return !0
			}, j.parse = function (a, b, c, f) {
				var g, h, j, l, m, n, o, p, s, t, u = a.style;
				for (g in b) n = b[g], "function" == typeof n && (n = n(r, q)), h = i[g], h ? c = h.parse(a, n, g, this, c, f, b) : (m = _(a, g, e) + "", s = "string" == typeof n, "color" === g || "fill" === g || "stroke" === g || -1 !== g.indexOf("Color") || s && A.test(n) ? (s || (n = na(n), n = (n.length > 3 ? "rgba(" : "rgb(") + n.join(",") + ")"), c = va(u, g, m, n, !0, "transparent", c, 0, f)) : s && J.test(n) ? c = va(u, g, m, n, !0, null, c, 0, f) : (j = parseFloat(m), o = j || 0 === j ? m.substr((j + "").length) : "", ("" === m || "auto" === m) && ("width" === g || "height" === g ? (j = ga(a, g, e), o = "px") : "left" === g || "top" === g ? (j = ba(a, g, e), o = "px") : (j = "opacity" !== g ? 0 : 1, o = "")), t = s && "=" === n.charAt(1), t ? (l = parseInt(n.charAt(0) + "1", 10), n = n.substr(2), l *= parseFloat(n), p = n.replace(w, "")) : (l = parseFloat(n), p = s ? n.replace(w, "") : ""), "" === p && (p = g in d ? d[g] : o), n = l || 0 === l ? (t ? l + j : l) + p : b[g], o !== p && "" !== p && (l || 0 === l) && j && (j = aa(a, g, j, o), "%" === p ? (j /= aa(a, g, 100, "%") / 100, b.strictUnits !== !0 && (m = j + "%")) : "em" === p || "rem" === p || "vw" === p || "vh" === p ? j /= aa(a, g, 1, p) : "px" !== p && (l = aa(a, g, l, p), p = "px"), t && (l || 0 === l) && (n = l + j + p)), t && (l += j), !j && 0 !== j || !l && 0 !== l ? void 0 !== u[g] && (n || n + "" != "NaN" && null != n) ? (c = new ta(u, g, l || j || 0, 0, c, -1, g, !1, 0, m, n), c.xs0 = "none" !== n || "display" !== g && -1 === g.indexOf("Style") ? n : m) : W("invalid " + g + " tween value: " + b[g]) : (c = new ta(u, g, j, l - j, c, 0, g, k !== !1 && ("px" === p || "zIndex" === g), 0, m, n), c.xs0 = p))), f && c && !c.plugin && (c.plugin = f);
				return c
			}, j.setRatio = function (a) {
				var b, c, d, e = this._firstPT,
					f = 1e-6;
				if (1 !== a || this._tween._time !== this._tween._duration && 0 !== this._tween._time)
					if (a || this._tween._time !== this._tween._duration && 0 !== this._tween._time || this._tween._rawPrevTime === -1e-6)
						for (; e;) {
							if (b = e.c * a + e.s, e.r ? b = Math.round(b) : f > b && b > -f && (b = 0), e.type)
								if (1 === e.type)
									if (d = e.l, 2 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2;
									else if (3 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2 + e.xn2 + e.xs3;
							else if (4 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2 + e.xn2 + e.xs3 + e.xn3 + e.xs4;
							else if (5 === d) e.t[e.p] = e.xs0 + b + e.xs1 + e.xn1 + e.xs2 + e.xn2 + e.xs3 + e.xn3 + e.xs4 + e.xn4 + e.xs5;
							else {
								for (c = e.xs0 + b + e.xs1, d = 1; d < e.l; d++) c += e["xn" + d] + e["xs" + (d + 1)];
								e.t[e.p] = c
							} else -1 === e.type ? e.t[e.p] = e.xs0 : e.setRatio && e.setRatio(a);
							else e.t[e.p] = b + e.xs0;
							e = e._next
						} else
							for (; e;) 2 !== e.type ? e.t[e.p] = e.b : e.setRatio(a), e = e._next;
					else
						for (; e;) {
							if (2 !== e.type)
								if (e.r && -1 !== e.type)
									if (b = Math.round(e.s + e.c), e.type) {
										if (1 === e.type) {
											for (d = e.l, c = e.xs0 + b + e.xs1, d = 1; d < e.l; d++) c += e["xn" + d] + e["xs" + (d + 1)];
											e.t[e.p] = c
										}
									} else e.t[e.p] = b + e.xs0;
							else e.t[e.p] = e.e;
							else e.setRatio(a);
							e = e._next
						}
			}, j._enableTransforms = function (a) {
				this._transform = this._transform || Ra(this._target, e, !0), this._transformType = this._transform.svg && Aa || !a && 3 !== this._transformType ? 2 : 3
			};
			var Ya = function (a) {
				this.t[this.p] = this.e, this.data._linkCSSP(this, this._next, null, !0)
			};
			j._addLazySet = function (a, b, c) {
				var d = this._firstPT = new ta(a, b, 0, 0, this._firstPT, 2);
				d.e = c, d.setRatio = Ya, d.data = this
			}, j._linkCSSP = function (a, b, c, d) {
				return a && (b && (b._prev = a), a._next && (a._next._prev = a._prev), a._prev ? a._prev._next = a._next : this._firstPT === a && (this._firstPT = a._next, d = !0), c ? c._next = a : d || null !== this._firstPT || (this._firstPT = a), a._next = b, a._prev = c), a
			}, j._mod = function (a) {
				for (var b = this._firstPT; b;) "function" == typeof a[b.p] && a[b.p] === Math.round && (b.r = 1), b = b._next
			}, j._kill = function (b) {
				var c, d, e, f = b;
				if (b.autoAlpha || b.alpha) {
					f = {};
					for (d in b) f[d] = b[d];
					f.opacity = 1, f.autoAlpha && (f.visibility = 1)
				}
				for (b.className && (c = this._classNamePT) && (e = c.xfirst, e && e._prev ? this._linkCSSP(e._prev, c._next, e._prev._prev) : e === this._firstPT && (this._firstPT = c._next), c._next && this._linkCSSP(c._next, c._next._next, e._prev), this._classNamePT = null), c = this._firstPT; c;) c.plugin && c.plugin !== d && c.plugin._kill && (c.plugin._kill(b), d = c.plugin), c = c._next;
				return a.prototype._kill.call(this, f)
			};
			var Za = function (a, b, c) {
				var d, e, f, g;
				if (a.slice)
					for (e = a.length; --e > -1;) Za(a[e], b, c);
				else
					for (d = a.childNodes, e = d.length; --e > -1;) f = d[e], g = f.type, f.style && (b.push(ca(f)), c && c.push(f)), 1 !== g && 9 !== g && 11 !== g || !f.childNodes.length || Za(f, b, c)
			};
			return g.cascadeTo = function (a, c, d) {
				var e, f, g, h, i = b.to(a, c, d),
					j = [i],
					k = [],
					l = [],
					m = [],
					n = b._internals.reservedProps;
				for (a = i._targets || i.target, Za(a, k, m), i.render(c, !0, !0), Za(a, l), i.render(0, !0, !0), i._enabled(!0), e = m.length; --e > -1;)
					if (f = da(m[e], k[e], l[e]), f.firstMPT) {
						f = f.difs;
						for (g in d) n[g] && (f[g] = d[g]);
						h = {};
						for (g in f) h[g] = k[e][g];
						j.push(b.fromTo(m[e], c, h, f))
					}
				return j
			}, a.activate([g]), g
		}, !0)
	}), _gsScope._gsDefine && _gsScope._gsQueue.pop()(),
	function (a) {
		"use strict";
		var b = function () {
			return (_gsScope.GreenSockGlobals || _gsScope)[a]
		};
		"function" == typeof define && define.amd ? define(["TweenLite"], b) : "undefined" != typeof module && module.exports && (require("../TweenLite.js"), module.exports = b())
	}("CSSPlugin");
/*!
 * VERSION: 0.5.6
 * DATE: 2017-01-17
 * UPDATES AND DOCS AT: http://greensock.com
 *
 * @license Copyright (c) 2008-2017, GreenSock. All rights reserved.
 * SplitText is a Club GreenSock membership benefit; You must have a valid membership to use
 * this code without violating the terms of use. Visit http://greensock.com/club/ to sign up or get more details.
 * This work is subject to the software agreement that was issued with your membership.
 *
 * @author: Jack Doyle, jack@greensock.com
 */
var _gsScope = "undefined" != typeof module && module.exports && "undefined" != typeof global ? global : this || window;
! function (a) {
	"use strict";
	var b = a.GreenSockGlobals || a,
		c = function (a) {
			var c, d = a.split("."),
				e = b;
			for (c = 0; c < d.length; c++) e[d[c]] = e = e[d[c]] || {};
			return e
		},
		d = c("com.greensock.utils"),
		e = function (a) {
			var b = a.nodeType,
				c = "";
			if (1 === b || 9 === b || 11 === b) {
				if ("string" == typeof a.textContent) return a.textContent;
				for (a = a.firstChild; a; a = a.nextSibling) c += e(a)
			} else if (3 === b || 4 === b) return a.nodeValue;
			return c
		},
		f = document,
		g = f.defaultView ? f.defaultView.getComputedStyle : function () {},
		h = /([A-Z])/g,
		i = function (a, b, c, d) {
			var e;
			return (c = c || g(a, null)) ? (a = c.getPropertyValue(b.replace(h, "-$1").toLowerCase()), e = a || c.length ? a : c[b]) : a.currentStyle && (c = a.currentStyle, e = c[b]), d ? e : parseInt(e, 10) || 0
		},
		j = function (a) {
			return a.length && a[0] && (a[0].nodeType && a[0].style && !a.nodeType || a[0].length && a[0][0]) ? !0 : !1
		},
		k = function (a) {
			var b, c, d, e = [],
				f = a.length;
			for (b = 0; f > b; b++)
				if (c = a[b], j(c))
					for (d = c.length, d = 0; d < c.length; d++) e.push(c[d]);
				else e.push(c);
			return e
		},
		l = /(?:\r|\n|\t\t)/g,
		m = /(?:\s\s+)/g,
		n = 55296,
		o = 56319,
		p = 56320,
		q = 127462,
		r = 127487,
		s = 127995,
		t = 127999,
		u = function (a) {
			return (a.charCodeAt(0) - n << 10) + (a.charCodeAt(1) - p) + 65536
		},
		v = f.all && !f.addEventListener,
		w = " style='position:relative;display:inline-block;" + (v ? "*display:inline;*zoom:1;'" : "'"),
		x = function (a, b) {
			a = a || "";
			var c = -1 !== a.indexOf("++"),
				d = 1;
			return c && (a = a.split("++").join("")),
				function () {
					return "<" + b + w + (a ? " class='" + a + (c ? d++ : "") + "'>" : ">")
				}
		},
		y = d.SplitText = b.SplitText = function (a, b) {
			if ("string" == typeof a && (a = y.selector(a)), !a) throw "cannot split a null element.";
			this.elements = j(a) ? k(a) : [a], this.chars = [], this.words = [], this.lines = [], this._originals = [], this.vars = b || {}, this.split(b)
		},
		z = function (a, b, c) {
			var d = a.nodeType;
			if (1 === d || 9 === d || 11 === d)
				for (a = a.firstChild; a; a = a.nextSibling) z(a, b, c);
			else(3 === d || 4 === d) && (a.nodeValue = a.nodeValue.split(b).join(c))
		},
		A = function (a, b) {
			for (var c = b.length; --c > -1;) a.push(b[c])
		},
		B = function (a) {
			var b, c = [],
				d = a.length;
			for (b = 0; b !== d; c.push(a[b++]));
			return c
		},
		C = function (a, b, c) {
			for (var d; a && a !== b;) {
				if (d = a._next || a.nextSibling) return d.textContent.charAt(0) === c;
				a = a.parentNode || a._parent
			}
			return !1
		},
		D = function (a) {
			var b, c, d = B(a.childNodes),
				e = d.length;
			for (b = 0; e > b; b++) c = d[b], c._isSplit ? D(c) : (b && 3 === c.previousSibling.nodeType ? c.previousSibling.nodeValue += 3 === c.nodeType ? c.nodeValue : c.firstChild.nodeValue : 3 !== c.nodeType && a.insertBefore(c.firstChild, c), a.removeChild(c))
		},
		E = function (a, b, c, d, e, h, j) {
			var k, l, m, n, o, p, q, r, s, t, u, v, w = g(a),
				x = i(a, "paddingLeft", w),
				y = -999,
				B = i(a, "borderBottomWidth", w) + i(a, "borderTopWidth", w),
				E = i(a, "borderLeftWidth", w) + i(a, "borderRightWidth", w),
				F = i(a, "paddingTop", w) + i(a, "paddingBottom", w),
				G = i(a, "paddingLeft", w) + i(a, "paddingRight", w),
				H = .2 * i(a, "fontSize"),
				I = i(a, "textAlign", w, !0),
				J = [],
				K = [],
				L = [],
				M = b.wordDelimiter || " ",
				N = b.span ? "span" : "div",
				O = b.type || b.split || "chars,words,lines",
				P = e && -1 !== O.indexOf("lines") ? [] : null,
				Q = -1 !== O.indexOf("words"),
				R = -1 !== O.indexOf("chars"),
				S = "absolute" === b.position || b.absolute === !0,
				T = b.linesClass,
				U = -1 !== (T || "").indexOf("++"),
				V = [];
			for (P && 1 === a.children.length && a.children[0]._isSplit && (a = a.children[0]), U && (T = T.split("++").join("")), l = a.getElementsByTagName("*"), m = l.length, o = [], k = 0; m > k; k++) o[k] = l[k];
			if (P || S)
				for (k = 0; m > k; k++) n = o[k], p = n.parentNode === a, (p || S || R && !Q) && (v = n.offsetTop, P && p && Math.abs(v - y) > H && "BR" !== n.nodeName && (q = [], P.push(q), y = v), S && (n._x = n.offsetLeft, n._y = v, n._w = n.offsetWidth, n._h = n.offsetHeight), P && ((n._isSplit && p || !R && p || Q && p || !Q && n.parentNode.parentNode === a && !n.parentNode._isSplit) && (q.push(n), n._x -= x, C(n, a, M) && (n._wordEnd = !0)), "BR" === n.nodeName && n.nextSibling && "BR" === n.nextSibling.nodeName && P.push([])));
			for (k = 0; m > k; k++) n = o[k], p = n.parentNode === a, "BR" !== n.nodeName ? (S && (s = n.style, Q || p || (n._x += n.parentNode._x, n._y += n.parentNode._y), s.left = n._x + "px", s.top = n._y + "px", s.position = "absolute", s.display = "block", s.width = n._w + 1 + "px", s.height = n._h + "px"), !Q && R ? n._isSplit ? (n._next = n.nextSibling, n.parentNode.appendChild(n)) : n.parentNode._isSplit ? (n._parent = n.parentNode, !n.previousSibling && n.firstChild && (n.firstChild._isFirst = !0), n.nextSibling && " " === n.nextSibling.textContent && !n.nextSibling.nextSibling && V.push(n.nextSibling), n._next = n.nextSibling && n.nextSibling._isFirst ? null : n.nextSibling, n.parentNode.removeChild(n), o.splice(k--, 1), m--) : p || (v = !n.nextSibling && C(n.parentNode, a, M), n.parentNode._parent && n.parentNode._parent.appendChild(n), v && n.parentNode.appendChild(f.createTextNode(" ")), b.span && (n.style.display = "inline"), J.push(n)) : n.parentNode._isSplit && !n._isSplit && "" !== n.innerHTML ? K.push(n) : R && !n._isSplit && (b.span && (n.style.display = "inline"), J.push(n))) : P || S ? (n.parentNode && n.parentNode.removeChild(n), o.splice(k--, 1), m--) : Q || a.appendChild(n);
			for (k = V.length; --k > -1;) V[k].parentNode.removeChild(V[k]);
			if (P) {
				for (S && (t = f.createElement(N), a.appendChild(t), u = t.offsetWidth + "px", v = t.offsetParent === a ? 0 : a.offsetLeft, a.removeChild(t)), s = a.style.cssText, a.style.cssText = "display:none;"; a.firstChild;) a.removeChild(a.firstChild);
				for (r = " " === M && (!S || !Q && !R), k = 0; k < P.length; k++) {
					for (q = P[k], t = f.createElement(N), t.style.cssText = "display:block;text-align:" + I + ";position:" + (S ? "absolute;" : "relative;"), T && (t.className = T + (U ? k + 1 : "")), L.push(t), m = q.length, l = 0; m > l; l++) "BR" !== q[l].nodeName && (n = q[l], t.appendChild(n), r && n._wordEnd && t.appendChild(f.createTextNode(" ")), S && (0 === l && (t.style.top = n._y + "px", t.style.left = x + v + "px"), n.style.top = "0px", v && (n.style.left = n._x - v + "px")));
					0 === m ? t.innerHTML = "&nbsp;" : Q || R || (D(t), z(t, String.fromCharCode(160), " ")), S && (t.style.width = u, t.style.height = n._h + "px"), a.appendChild(t)
				}
				a.style.cssText = s
			}
			S && (j > a.clientHeight && (a.style.height = j - F + "px", a.clientHeight < j && (a.style.height = j + B + "px")), h > a.clientWidth && (a.style.width = h - G + "px", a.clientWidth < h && (a.style.width = h + E + "px"))), A(c, J), A(d, K), A(e, L)
		},
		F = function (a, b, c, d) {
			var g, h, i, j, k, p, v, w, x, y = b.span ? "span" : "div",
				A = b.type || b.split || "chars,words,lines",
				B = (-1 !== A.indexOf("words"), -1 !== A.indexOf("chars")),
				C = "absolute" === b.position || b.absolute === !0,
				D = b.wordDelimiter || " ",
				E = " " !== D ? "" : C ? "&#173; " : " ",
				F = b.span ? "</span>" : "</div>",
				G = !0,
				H = f.createElement("div"),
				I = a.parentNode;
			for (I.insertBefore(H, a), H.textContent = a.nodeValue, I.removeChild(a), a = H, g = e(a), v = -1 !== g.indexOf("<"), b.reduceWhiteSpace !== !1 && (g = g.replace(m, " ").replace(l, "")), v && (g = g.split("<").join("{{LT}}")), k = g.length, h = (" " === g.charAt(0) ? E : "") + c(), i = 0; k > i; i++)
				if (p = g.charAt(i), p === D && g.charAt(i - 1) !== D && i) {
					for (h += G ? F : "", G = !1; g.charAt(i + 1) === D;) h += E, i++;
					i === k - 1 ? h += E : ")" !== g.charAt(i + 1) && (h += E + c(), G = !0)
				} else "{" === p && "{{LT}}" === g.substr(i, 6) ? (h += B ? d() + "{{LT}}</" + y + ">" : "{{LT}}", i += 5) : p.charCodeAt(0) >= n && p.charCodeAt(0) <= o || g.charCodeAt(i + 1) >= 65024 && g.charCodeAt(i + 1) <= 65039 ? (w = u(g.substr(i, 2)), x = u(g.substr(i + 2, 2)), j = w >= q && r >= w && x >= q && r >= x || x >= s && t >= x ? 4 : 2, h += B && " " !== p ? d() + g.substr(i, j) + "</" + y + ">" : g.substr(i, j), i += j - 1) : h += B && " " !== p ? d() + p + "</" + y + ">" : p;
			a.outerHTML = h + (G ? F : ""), v && z(I, "{{LT}}", "<")
		},
		G = function (a, b, c, d) {
			var e, f, g = B(a.childNodes),
				h = g.length,
				j = "absolute" === b.position || b.absolute === !0;
			if (3 !== a.nodeType || h > 1) {
				for (b.absolute = !1, e = 0; h > e; e++) f = g[e], (3 !== f.nodeType || /\S+/.test(f.nodeValue)) && (j && 3 !== f.nodeType && "inline" === i(f, "display", null, !0) && (f.style.display = "inline-block", f.style.position = "relative"), f._isSplit = !0, G(f, b, c, d));
				return b.absolute = j, void(a._isSplit = !0)
			}
			F(a, b, c, d)
		},
		H = y.prototype;
	H.split = function (a) {
		this.isSplit && this.revert(), this.vars = a = a || this.vars, this._originals.length = this.chars.length = this.words.length = this.lines.length = 0;
		for (var b, c, d, e = this.elements.length, f = a.span ? "span" : "div", g = ("absolute" === a.position || a.absolute === !0, x(a.wordsClass, f)), h = x(a.charsClass, f); --e > -1;) d = this.elements[e], this._originals[e] = d.innerHTML, b = d.clientHeight, c = d.clientWidth, G(d, a, g, h), E(d, a, this.chars, this.words, this.lines, c, b);
		return this.chars.reverse(), this.words.reverse(), this.lines.reverse(), this.isSplit = !0, this
	}, H.revert = function () {
		if (!this._originals) throw "revert() call wasn't scoped properly.";
		for (var a = this._originals.length; --a > -1;) this.elements[a].innerHTML = this._originals[a];
		return this.chars = [], this.words = [], this.lines = [], this.isSplit = !1, this
	}, y.selector = a.$ || a.jQuery || function (b) {
		var c = a.$ || a.jQuery;
		return c ? (y.selector = c, c(b)) : "undefined" == typeof document ? b : document.querySelectorAll ? document.querySelectorAll(b) : document.getElementById("#" === b.charAt(0) ? b.substr(1) : b)
	}, y.version = "0.5.6"
}(_gsScope),
function (a) {
	"use strict";
	var b = function () {
		return (_gsScope.GreenSockGlobals || _gsScope)[a]
	};
	"function" == typeof define && define.amd ? define([], b) : "undefined" != typeof module && module.exports && (module.exports = b())
}("SplitText");
try {
	window.GreenSockGlobals = null;
	window._gsQueue = null;
	window._gsDefine = null;
	delete(window.GreenSockGlobals);
	delete(window._gsQueue);
	delete(window._gsDefine);
} catch (e) {}
try {
	window.GreenSockGlobals = oldgs;
	window._gsQueue = oldgs_queue;
} catch (e) {}
if (window.tplogs == true)
	try {
		console.groupEnd();
	} catch (e) {}
	(function (e, t) {
		e.waitForImages = {
			hasImageProperties: ["backgroundImage", "listStyleImage", "borderImage", "borderCornerImage"]
		};
		e.expr[":"].uncached = function (t) {
			var n = document.createElement("img");
			n.src = t.src;
			return e(t).is('img[src!=""]') && !n.complete
		};
		e.fn.waitForImages = function (t, n, r) {
			if (e.isPlainObject(arguments[0])) {
				n = t.each;
				r = t.waitForAll;
				t = t.finished
			}
			t = t || e.noop;
			n = n || e.noop;
			r = !!r;
			if (!e.isFunction(t) || !e.isFunction(n)) {
				throw new TypeError("An invalid callback was supplied.")
			}
			return this.each(function () {
				var i = e(this),
					s = [];
				if (r) {
					var o = e.waitForImages.hasImageProperties || [],
						u = /url\((['"]?)(.*?)\1\)/g;
					i.find("*").each(function () {
						var t = e(this);
						if (t.is("img:uncached")) {
							s.push({
								src: t.attr("src"),
								element: t[0]
							})
						}
						e.each(o, function (e, n) {
							var r = t.css(n);
							if (!r) {
								return true
							}
							var i;
							while (i = u.exec(r)) {
								s.push({
									src: i[2],
									element: t[0]
								})
							}
						})
					})
				} else {
					i.find("img:uncached").each(function () {
						s.push({
							src: this.src,
							element: this
						})
					})
				}
				var f = s.length,
					l = 0;
				if (f == 0) {
					t.call(i[0])
				}
				e.each(s, function (r, s) {
					var o = new Image;
					e(o).bind("load error", function (e) {
						l++;
						n.call(s.element, l, f, e.type == "load");
						if (l == f) {
							t.call(i[0]);
							return false
						}
					});
					o.src = s.src
				})
			})
		};
	})(jQuery);
! function (jQuery, undefined) {
	"use strict";
	var version = {
		core: "5.4.8",
		"revolution.extensions.actions.min.js": "2.1.0",
		"revolution.extensions.carousel.min.js": "1.2.1",
		"revolution.extensions.kenburn.min.js": "1.3.1",
		"revolution.extensions.layeranimation.min.js": "3.6.5",
		"revolution.extensions.navigation.min.js": "1.3.5",
		"revolution.extensions.parallax.min.js": "2.2.3",
		"revolution.extensions.slideanims.min.js": "1.8",
		"revolution.extensions.video.min.js": "2.2.2"
	};
	jQuery.fn.extend({
		revolution: function (i) {
			var e = {
				delay: 9e3,
				responsiveLevels: 4064,
				visibilityLevels: [2048, 1024, 778, 480],
				gridwidth: 960,
				gridheight: 500,
				minHeight: 0,
				autoHeight: "off",
				sliderType: "standard",
				sliderLayout: "auto",
				fullScreenAutoWidth: "off",
				fullScreenAlignForce: "off",
				fullScreenOffsetContainer: "",
				fullScreenOffset: "0",
				hideCaptionAtLimit: 0,
				hideAllCaptionAtLimit: 0,
				hideSliderAtLimit: 0,
				disableProgressBar: "off",
				stopAtSlide: -1,
				stopAfterLoops: -1,
				shadow: 0,
				dottedOverlay: "none",
				startDelay: 0,
				lazyType: "smart",
				spinner: "spinner0",
				shuffle: "off",
				viewPort: {
					enable: !1,
					outof: "wait",
					visible_area: "60%",
					presize: !1
				},
				fallbacks: {
					isJoomla: !1,
					panZoomDisableOnMobile: "off",
					simplifyAll: "on",
					nextSlideOnWindowFocus: "off",
					disableFocusListener: !0,
					ignoreHeightChanges: "off",
					ignoreHeightChangesSize: 0,
					allowHTML5AutoPlayOnAndroid: !0
				},
				parallax: {
					type: "off",
					levels: [10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85],
					origo: "enterpoint",
					speed: 400,
					bgparallax: "off",
					opacity: "on",
					disable_onmobile: "off",
					ddd_shadow: "on",
					ddd_bgfreeze: "off",
					ddd_overflow: "visible",
					ddd_layer_overflow: "visible",
					ddd_z_correction: 65,
					ddd_path: "mouse"
				},
				scrolleffect: {
					fade: "off",
					blur: "off",
					scale: "off",
					grayscale: "off",
					maxblur: 10,
					on_layers: "off",
					on_slidebg: "off",
					on_static_layers: "off",
					on_parallax_layers: "off",
					on_parallax_static_layers: "off",
					direction: "both",
					multiplicator: 1.35,
					multiplicator_layers: .5,
					tilt: 30,
					disable_on_mobile: "on"
				},
				carousel: {
					easing: punchgs.Power3.easeInOut,
					speed: 800,
					showLayersAllTime: "off",
					horizontal_align: "center",
					vertical_align: "center",
					infinity: "on",
					space: 0,
					maxVisibleItems: 3,
					stretch: "off",
					fadeout: "on",
					maxRotation: 0,
					minScale: 0,
					vary_fade: "off",
					vary_rotation: "on",
					vary_scale: "off",
					border_radius: "0px",
					padding_top: 0,
					padding_bottom: 0
				},
				navigation: {
					keyboardNavigation: "off",
					keyboard_direction: "horizontal",
					mouseScrollNavigation: "off",
					onHoverStop: "on",
					touch: {
						touchenabled: "off",
						touchOnDesktop: "off",
						swipe_treshold: 75,
						swipe_min_touches: 1,
						drag_block_vertical: !1,
						swipe_direction: "horizontal"
					},
					arrows: {
						style: "",
						enable: !1,
						hide_onmobile: !1,
						hide_onleave: !0,
						hide_delay: 200,
						hide_delay_mobile: 1200,
						hide_under: 0,
						hide_over: 9999,
						tmp: "",
						rtl: !1,
						left: {
							h_align: "left",
							v_align: "center",
							h_offset: 20,
							v_offset: 0,
							container: "slider"
						},
						right: {
							h_align: "right",
							v_align: "center",
							h_offset: 20,
							v_offset: 0,
							container: "slider"
						}
					},
					bullets: {
						container: "slider",
						rtl: !1,
						style: "",
						enable: !1,
						hide_onmobile: !1,
						hide_onleave: !0,
						hide_delay: 200,
						hide_delay_mobile: 1200,
						hide_under: 0,
						hide_over: 9999,
						direction: "horizontal",
						h_align: "left",
						v_align: "center",
						space: 0,
						h_offset: 20,
						v_offset: 0,
						tmp: '<span class="tp-bullet-image"></span><span class="tp-bullet-title"></span>'
					},
					thumbnails: {
						container: "slider",
						rtl: !1,
						style: "",
						enable: !1,
						width: 100,
						height: 50,
						min_width: 100,
						wrapper_padding: 2,
						wrapper_color: "#f5f5f5",
						wrapper_opacity: 1,
						tmp: '<span class="tp-thumb-image"></span><span class="tp-thumb-title"></span>',
						visibleAmount: 5,
						hide_onmobile: !1,
						hide_onleave: !0,
						hide_delay: 200,
						hide_delay_mobile: 1200,
						hide_under: 0,
						hide_over: 9999,
						direction: "horizontal",
						span: !1,
						position: "inner",
						space: 2,
						h_align: "left",
						v_align: "center",
						h_offset: 20,
						v_offset: 0
					},
					tabs: {
						container: "slider",
						rtl: !1,
						style: "",
						enable: !1,
						width: 100,
						min_width: 100,
						height: 50,
						wrapper_padding: 10,
						wrapper_color: "#f5f5f5",
						wrapper_opacity: 1,
						tmp: '<span class="tp-tab-image"></span>',
						visibleAmount: 5,
						hide_onmobile: !1,
						hide_onleave: !0,
						hide_delay: 200,
						hide_delay_mobile: 1200,
						hide_under: 0,
						hide_over: 9999,
						direction: "horizontal",
						span: !1,
						space: 0,
						position: "inner",
						h_align: "left",
						v_align: "center",
						h_offset: 20,
						v_offset: 0
					}
				},
				extensions: "extensions/",
				extensions_suffix: ".min.js",
				debugMode: !1
			};
			return i = jQuery.extend(!0, {}, e, i), this.each(function () {
				var e = jQuery(this);
				i.minHeight = i.minHeight != undefined ? parseInt(i.minHeight, 0) : i.minHeight, i.scrolleffect.on = "on" === i.scrolleffect.fade || "on" === i.scrolleffect.scale || "on" === i.scrolleffect.blur || "on" === i.scrolleffect.grayscale, "hero" == i.sliderType && e.find(">ul>li").each(function (e) {
					0 < e && jQuery(this).remove()
				}), i.jsFileLocation = i.jsFileLocation || getScriptLocation("themepunch.revolution.min.js"), i.jsFileLocation = i.jsFileLocation + i.extensions, i.scriptsneeded = getNeededScripts(i, e), i.curWinRange = 0, i.rtl = !0, i.navigation != undefined && i.navigation.touch != undefined && (i.navigation.touch.swipe_min_touches = 5 < i.navigation.touch.swipe_min_touches ? 1 : i.navigation.touch.swipe_min_touches), jQuery(this).on("scriptsloaded", function () {
					if (i.modulesfailing) return e.html('<div style="margin:auto;line-height:40px;font-size:14px;color:#fff;padding:15px;background:#e74c3c;margin:20px 0px;">!! Error at loading Slider Revolution 5.0 Extrensions.' + i.errorm + "</div>").show(), !1;
					_R.migration != undefined && (i = _R.migration(e, i)), punchgs.force3D = !0, "on" !== i.simplifyAll && punchgs.TweenLite.lagSmoothing(1e3, 16), prepareOptions(e, i), initSlider(e, i)
				}), e[0].opt = i, waitForScripts(e, i)
			})
		},
		getRSVersion: function (e) {
			if (!0 === e) return jQuery("body").data("tp_rs_version");
			var i = jQuery("body").data("tp_rs_version"),
				t = "";
			for (var a in t += "---------------------------------------------------------\n", t += "    Currently Loaded Slider Revolution & SR Modules :\n", t += "---------------------------------------------------------\n", i) t += i[a].alias + ": " + i[a].ver + "\n";
			return t += "---------------------------------------------------------\n"
		},
		revremoveslide: function (r) {
			return this.each(function () {
				var e = jQuery(this),
					i = e[0].opt;
				if (!(r < 0 || r > i.slideamount) && e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && i && 0 < i.li.length && (0 < r || r <= i.li.length)) {
					var t = jQuery(i.li[r]),
						a = t.data("index"),
						n = !1;
					i.slideamount = i.slideamount - 1, i.realslideamount = i.realslideamount - 1, removeNavWithLiref(".tp-bullet", a, i), removeNavWithLiref(".tp-tab", a, i), removeNavWithLiref(".tp-thumb", a, i), t.hasClass("active-revslide") && (n = !0), t.remove(), i.li = removeArray(i.li, r), i.carousel && i.carousel.slides && (i.carousel.slides = removeArray(i.carousel.slides, r)), i.thumbs = removeArray(i.thumbs, r), _R.updateNavIndexes && _R.updateNavIndexes(i), n && e.revnext(), punchgs.TweenLite.set(i.li, {
						minWidth: "99%"
					}), punchgs.TweenLite.set(i.li, {
						minWidth: "100%"
					})
				}
			})
		},
		revaddcallback: function (e) {
			return this.each(function () {
				this.opt && (this.opt.callBackArray === undefined && (this.opt.callBackArray = new Array), this.opt.callBackArray.push(e))
			})
		},
		revgetparallaxproc: function () {
			return jQuery(this)[0].opt.scrollproc
		},
		revdebugmode: function () {
			return this.each(function () {
				var e = jQuery(this);
				e[0].opt.debugMode = !0, containerResized(e, e[0].opt)
			})
		},
		revscroll: function (i) {
			return this.each(function () {
				var e = jQuery(this);
				jQuery("body,html").animate({
					scrollTop: e.offset().top + e.height() - i + "px"
				}, {
					duration: 400
				})
			})
		},
		revredraw: function (e) {
			return this.each(function () {
				var e = jQuery(this);
				containerResized(e, e[0].opt)
			})
		},
		revkill: function (e) {
			var i = this,
				t = jQuery(this);
			if (punchgs.TweenLite.killDelayedCallsTo(_R.showHideNavElements), t != undefined && 0 < t.length && 0 < jQuery("body").find("#" + t.attr("id")).length) {
				t.data("conthover", 1), t.data("conthover-changed", 1), t.trigger("revolution.slide.onpause");
				var a = t.parent().find(".tp-bannertimer"),
					n = t[0].opt;
				n.tonpause = !0, t.trigger("stoptimer");
				var r = "resize.revslider-" + t.attr("id");
				jQuery(window).unbind(r), punchgs.TweenLite.killTweensOf(t.find("*"), !1), punchgs.TweenLite.killTweensOf(t, !1), t.unbind("hover, mouseover, mouseenter,mouseleave, resize");
				r = "resize.revslider-" + t.attr("id");
				jQuery(window).off(r), t.find("*").each(function () {
					var e = jQuery(this);
					e.unbind("on, hover, mouseenter,mouseleave,mouseover, resize,restarttimer, stoptimer"), e.off("on, hover, mouseenter,mouseleave,mouseover, resize"), e.data("mySplitText", null), e.data("ctl", null), e.data("tween") != undefined && e.data("tween").kill(), e.data("kenburn") != undefined && e.data("kenburn").kill(), e.data("timeline_out") != undefined && e.data("timeline_out").kill(), e.data("timeline") != undefined && e.data("timeline").kill(), e.remove(), e.empty(), e = null
				}), punchgs.TweenLite.killTweensOf(t.find("*"), !1), punchgs.TweenLite.killTweensOf(t, !1), a.remove();
				try {
					t.closest(".forcefullwidth_wrapper_tp_banner").remove()
				} catch (e) {}
				try {
					t.closest(".rev_slider_wrapper").remove()
				} catch (e) {}
				try {
					t.remove()
				} catch (e) {}
				return t.empty(), t.html(), n = t = null, delete i.c, delete i.opt, delete i.container, !0
			}
			return !1
		},
		revpause: function () {
			return this.each(function () {
				var e = jQuery(this);
				e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && (e.data("conthover", 1), e.data("conthover-changed", 1), e.trigger("revolution.slide.onpause"), e[0].opt.tonpause = !0, e.trigger("stoptimer"))
			})
		},
		revresume: function () {
			return this.each(function () {
				var e = jQuery(this);
				e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && (e.data("conthover", 0), e.data("conthover-changed", 1), e.trigger("revolution.slide.onresume"), e[0].opt.tonpause = !1, e.trigger("starttimer"))
			})
		},
		revstart: function () {
			var e = jQuery(this);
			if (e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && e[0].opt !== undefined) return e[0].opt.sliderisrunning ? (console.log("Slider Is Running Already"), !1) : ((e[0].opt.c = e)[0].opt.ul = e.find(">ul"), runSlider(e, e[0].opt), !0)
		},
		revnext: function () {
			return this.each(function () {
				var e = jQuery(this);
				e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && _R.callingNewSlide(e, 1)
			})
		},
		revprev: function () {
			return this.each(function () {
				var e = jQuery(this);
				e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && _R.callingNewSlide(e, -1)
			})
		},
		revmaxslide: function () {
			return jQuery(this).find(".tp-revslider-mainul >li").length
		},
		revcurrentslide: function () {
			var e = jQuery(this);
			if (e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length) return parseInt(e[0].opt.act, 0) + 1
		},
		revlastslide: function () {
			return jQuery(this).find(".tp-revslider-mainul >li").length
		},
		revshowslide: function (i) {
			return this.each(function () {
				var e = jQuery(this);
				e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && _R.callingNewSlide(e, "to" + (i - 1))
			})
		},
		revcallslidewithid: function (i) {
			return this.each(function () {
				var e = jQuery(this);
				e != undefined && 0 < e.length && 0 < jQuery("body").find("#" + e.attr("id")).length && _R.callingNewSlide(e, i)
			})
		}
	});
	var _R = jQuery.fn.revolution;
	jQuery.extend(!0, _R, {
		getversion: function () {
			return version
		},
		compare_version: function (e) {
			var i = jQuery("body").data("tp_rs_version");
			return (i = i === undefined ? new Object : i).Core === undefined && (i.Core = new Object, i.Core.alias = "Slider Revolution Core", i.Core.name = "jquery.themepunch.revolution.min.js", i.Core.ver = _R.getversion().core), "stop" != e.check && (_R.getversion().core < e.min_core ? (e.check === undefined && (console.log("%cSlider Revolution Warning (Core:" + _R.getversion().core + ")", "color:#c0392b;font-weight:bold;"), console.log("%c     Core is older than expected (" + e.min_core + ") from " + e.alias, "color:#333"), console.log("%c     Please update Slider Revolution to the latest version.", "color:#333"), console.log("%c     It might be required to purge and clear Server/Client side Caches.", "color:#333")), e.check = "stop") : _R.getversion()[e.name] != undefined && e.version < _R.getversion()[e.name] && (e.check === undefined && (console.log("%cSlider Revolution Warning (Core:" + _R.getversion().core + ")", "color:#c0392b;font-weight:bold;"), console.log("%c     " + e.alias + " (" + e.version + ") is older than requiered (" + _R.getversion()[e.name] + ")", "color:#333"), console.log("%c     Please update Slider Revolution to the latest version.", "color:#333"), console.log("%c     It might be required to purge and clear Server/Client side Caches.", "color:#333")), e.check = "stop")), i[e.alias] === undefined && (i[e.alias] = new Object, i[e.alias].alias = e.alias, i[e.alias].ver = e.version, i[e.alias].name = e.name), jQuery("body").data("tp_rs_version", i), e
		},
		currentSlideIndex: function (e) {
			var i = e.c.find(".active-revslide").index();
			return i = -1 == i ? 0 : i
		},
		simp: function (e, i, t) {
			var a = Math.abs(e) - Math.floor(Math.abs(e / i)) * i;
			return t ? a : e < 0 ? -1 * a : a
		},
		iOSVersion: function () {
			var e = !1;
			return navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i) || navigator.userAgent.match(/iPad/i) ? navigator.userAgent.match(/OS 4_\d like Mac OS X/i) && (e = !0) : e = !1, e
		},
		isIE: function (e, i) {
			var t = jQuery('<div style="display:none;"/>').appendTo(jQuery("body"));
			t.html("\x3c!--[if " + (i || "") + " IE " + (e || "") + "]><a>&nbsp;</a><![endif]--\x3e");
			var a = t.find("a").length;
			return t.remove(), a
		},
		is_mobile: function () {
			var e = ["android", "webos", "iphone", "ipad", "blackberry", "Android", "webos", , "iPod", "iPhone", "iPad", "Blackberry", "BlackBerry"],
				i = !1;
			for (var t in e) 1 < navigator.userAgent.split(e[t]).length && (i = !0);
			return i
		},
		is_android: function () {
			var e = ["android", "Android"],
				i = !1;
			for (var t in e) 1 < navigator.userAgent.split(e[t]).length && (i = !0);
			return i
		},
		callBackHandling: function (e, t, a) {
			try {
				e.callBackArray && jQuery.each(e.callBackArray, function (e, i) {
					i && i.inmodule && i.inmodule === t && i.atposition && i.atposition === a && i.callback && i.callback.call()
				})
			} catch (e) {
				console.log("Call Back Failed")
			}
		},
		get_browser: function () {
			var e, i = navigator.appName,
				t = navigator.userAgent,
				a = t.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
			return a && null != (e = t.match(/version\/([\.\d]+)/i)) && (a[2] = e[1]), (a = a ? [a[1], a[2]] : [i, navigator.appVersion, "-?"])[0]
		},
		get_browser_version: function () {
			var e, i = navigator.appName,
				t = navigator.userAgent,
				a = t.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
			return a && null != (e = t.match(/version\/([\.\d]+)/i)) && (a[2] = e[1]), (a = a ? [a[1], a[2]] : [i, navigator.appVersion, "-?"])[1]
		},
		isSafari11: function () {
			var e = jQuery.trim(_R.get_browser().toLowerCase());
			return -1 === jQuery.trim(navigator.userAgent.toLowerCase()).search("edge") && "msie" !== e && e.match(/safari|chrome/)
		},
		getHorizontalOffset: function (e, i) {
			var t = gWiderOut(e, ".outer-left"),
				a = gWiderOut(e, ".outer-right");
			switch (i) {
				case "left":
					return t;
				case "right":
					return a;
				case "both":
					return t + a
			}
		},
		callingNewSlide: function (e, i) {
			var t = 0 < e.find(".next-revslide").length ? e.find(".next-revslide").index() : 0 < e.find(".processing-revslide").length ? e.find(".processing-revslide").index() : e.find(".active-revslide").index(),
				a = 0,
				n = e[0].opt;
			e.find(".next-revslide").removeClass("next-revslide"), e.find(".active-revslide").hasClass("tp-invisible-slide") && (t = n.last_shown_slide), i && jQuery.isNumeric(i) || i.match(/to/g) ? (a = 1 === i || -1 === i ? (a = t + i) < 0 ? n.slideamount - 1 : a >= n.slideamount ? 0 : a : (i = jQuery.isNumeric(i) ? i : parseInt(i.split("to")[1], 0)) < 0 ? 0 : i > n.slideamount - 1 ? n.slideamount - 1 : i, e.find(".tp-revslider-slidesli:eq(" + a + ")").addClass("next-revslide")) : i && e.find(".tp-revslider-slidesli").each(function () {
				var e = jQuery(this);
				e.data("index") === i && e.addClass("next-revslide")
			}), a = e.find(".next-revslide").index(), e.trigger("revolution.nextslide.waiting"), t === a && t === n.last_shown_slide || a !== t && -1 != a ? swapSlide(e) : e.find(".next-revslide").removeClass("next-revslide")
		},
		slotSize: function (e, i) {
			i.slotw = Math.ceil(i.width / i.slots), "fullscreen" == i.sliderLayout ? i.sloth = Math.ceil(jQuery(window).height() / i.slots) : i.sloth = Math.ceil(i.height / i.slots), "on" == i.autoHeight && e !== undefined && "" !== e && (i.sloth = Math.ceil(e.height() / i.slots))
		},
		setSize: function (e) {
			var i = (e.top_outer || 0) + (e.bottom_outer || 0),
				t = parseInt(e.carousel.padding_top || 0, 0),
				a = parseInt(e.carousel.padding_bottom || 0, 0),
				n = e.gridheight[e.curWinRange],
				r = 0,
				o = -1 === e.nextSlide || e.nextSlide === undefined ? 0 : e.nextSlide;
			if (e.paddings = e.paddings === undefined ? {
					top: parseInt(e.c.parent().css("paddingTop"), 0) || 0,
					bottom: parseInt(e.c.parent().css("paddingBottom"), 0) || 0
				} : e.paddings, e.rowzones && 0 < e.rowzones.length)
				for (var s = 0; s < e.rowzones[o].length; s++) r += e.rowzones[o][s][0].offsetHeight;
			if (n = (n = n < e.minHeight ? e.minHeight : n) < r ? r : n, "fullwidth" == e.sliderLayout && "off" == e.autoHeight && punchgs.TweenLite.set(e.c, {
					maxHeight: n + "px"
				}), e.c.css({
					marginTop: t,
					marginBottom: a
				}), e.width = e.ul.width(), e.height = e.ul.height(), setScale(e), e.height = Math.round(e.gridheight[e.curWinRange] * (e.width / e.gridwidth[e.curWinRange])), e.height > e.gridheight[e.curWinRange] && "on" != e.autoHeight && (e.height = e.gridheight[e.curWinRange]), "fullscreen" == e.sliderLayout || e.infullscreenmode) {
				e.height = e.bw * e.gridheight[e.curWinRange];
				e.c.parent().width();
				var l = jQuery(window).height();
				if (e.fullScreenOffsetContainer != undefined) {
					try {
						var d = e.fullScreenOffsetContainer.split(",");
						d && jQuery.each(d, function (e, i) {
							l = 0 < jQuery(i).length ? l - jQuery(i).outerHeight(!0) : l
						})
					} catch (e) {}
					try {
						1 < e.fullScreenOffset.split("%").length && e.fullScreenOffset != undefined && 0 < e.fullScreenOffset.length ? l -= jQuery(window).height() * parseInt(e.fullScreenOffset, 0) / 100 : e.fullScreenOffset != undefined && 0 < e.fullScreenOffset.length && (l -= parseInt(e.fullScreenOffset, 0))
					} catch (e) {}
				}
				l = l < e.minHeight ? e.minHeight : l, l -= i, e.c.parent().height(l), e.c.closest(".rev_slider_wrapper").height(l), e.c.css({
					height: "100%"
				}), e.height = l, e.minHeight != undefined && e.height < e.minHeight && (e.height = e.minHeight), e.height = parseInt(r, 0) > parseInt(e.height, 0) ? r : e.height
			} else e.minHeight != undefined && e.height < e.minHeight && (e.height = e.minHeight), e.height = parseInt(r, 0) > parseInt(e.height, 0) ? r : e.height, e.c.height(e.height);
			var c = {
				height: t + a + i + e.height + e.paddings.top + e.paddings.bottom
			};
			e.c.closest(".forcefullwidth_wrapper_tp_banner").find(".tp-fullwidth-forcer").css(c), e.c.closest(".rev_slider_wrapper").css(c), setScale(e)
		},
		enterInViewPort: function (t) {
			t.waitForCountDown && (countDown(t.c, t), t.waitForCountDown = !1), t.waitForFirstSlide && (swapSlide(t.c), t.waitForFirstSlide = !1, setTimeout(function () {
				t.c.removeClass("tp-waitforfirststart")
			}, 500)), "playing" != t.sliderlaststatus && t.sliderlaststatus != undefined || t.c.trigger("starttimer"), t.lastplayedvideos != undefined && 0 < t.lastplayedvideos.length && jQuery.each(t.lastplayedvideos, function (e, i) {
				_R.playVideo(i, t)
			})
		},
		leaveViewPort: function (t) {
			t.sliderlaststatus = t.sliderstatus, t.c.trigger("stoptimer"), t.playingvideos != undefined && 0 < t.playingvideos.length && (t.lastplayedvideos = jQuery.extend(!0, [], t.playingvideos), t.playingvideos && jQuery.each(t.playingvideos, function (e, i) {
				t.leaveViewPortBasedStop = !0, _R.stopVideo && _R.stopVideo(i, t)
			}))
		},
		unToggleState: function (e) {
			e != undefined && 0 < e.length && jQuery.each(e, function (e, i) {
				i.removeClass("rs-toggle-content-active")
			})
		},
		toggleState: function (e) {
			e != undefined && 0 < e.length && jQuery.each(e, function (e, i) {
				i.addClass("rs-toggle-content-active")
			})
		},
		swaptoggleState: function (e) {
			e != undefined && 0 < e.length && jQuery.each(e, function (e, i) {
				jQuery(i).hasClass("rs-toggle-content-active") ? jQuery(i).removeClass("rs-toggle-content-active") : jQuery(i).addClass("rs-toggle-content-active")
			})
		},
		lastToggleState: function (e) {
			var t = 0;
			return e != undefined && 0 < e.length && jQuery.each(e, function (e, i) {
				t = i.hasClass("rs-toggle-content-active")
			}), t
		}
	});
	var _ISM = _R.is_mobile(),
		_ANDROID = _R.is_android(),
		checkIDS = function (e, i) {
			if (e.anyid = e.anyid === undefined ? [] : e.anyid, -1 != jQuery.inArray(i.attr("id"), e.anyid)) {
				var t = i.attr("id") + "_" + Math.round(9999 * Math.random());
				i.attr("id", t)
			}
			e.anyid.push(i.attr("id"))
		},
		removeArray = function (e, t) {
			var a = [];
			return jQuery.each(e, function (e, i) {
				e != t && a.push(i)
			}), a
		},
		removeNavWithLiref = function (e, i, t) {
			t.c.find(e).each(function () {
				var e = jQuery(this);
				e.data("liref") === i && e.remove()
			})
		},
		lAjax = function (i, t) {
			return !jQuery("body").data(i) && (t.filesystem ? (t.errorm === undefined && (t.errorm = "<br>Local Filesystem Detected !<br>Put this to your header:"), console.warn("Local Filesystem detected !"), t.errorm = t.errorm + '<br>&lt;script type="text/javascript" src="' + t.jsFileLocation + i + t.extensions_suffix + '"&gt;&lt;/script&gt;', console.warn(t.jsFileLocation + i + t.extensions_suffix + " could not be loaded !"), console.warn("Please use a local Server or work online or make sure that you load all needed Libraries manually in your Document."), console.log(" "), !(t.modulesfailing = !0)) : (jQuery.ajax({
				url: t.jsFileLocation + i + t.extensions_suffix + "?version=" + version.core,
				dataType: "script",
				cache: !0,
				error: function (e) {
					console.warn("Slider Revolution 5.0 Error !"), console.error("Failure at Loading:" + i + t.extensions_suffix + " on Path:" + t.jsFileLocation), console.info(e)
				}
			}), void jQuery("body").data(i, !0)))
		},
		getNeededScripts = function (t, e) {
			var i = new Object,
				a = t.navigation;
			return i.kenburns = !1, i.parallax = !1, i.carousel = !1, i.navigation = !1, i.videos = !1, i.actions = !1, i.layeranim = !1, i.migration = !1, e.data("version") && e.data("version").toString().match(/5./gi) ? (e.find("img").each(function () {
				"on" == jQuery(this).data("kenburns") && (i.kenburns = !0)
			}), ("carousel" == t.sliderType || "on" == a.keyboardNavigation || "on" == a.mouseScrollNavigation || "on" == a.touch.touchenabled || a.arrows.enable || a.bullets.enable || a.thumbnails.enable || a.tabs.enable) && (i.navigation = !0), e.find(".tp-caption, .tp-static-layer, .rs-background-video-layer").each(function () {
				var e = jQuery(this);
				(e.data("ytid") != undefined || 0 < e.find("iframe").length && 0 < e.find("iframe").attr("src").toLowerCase().indexOf("youtube")) && (i.videos = !0), (e.data("vimeoid") != undefined || 0 < e.find("iframe").length && 0 < e.find("iframe").attr("src").toLowerCase().indexOf("vimeo")) && (i.videos = !0), e.data("actions") !== undefined && (i.actions = !0), i.layeranim = !0
			}), e.find("li").each(function () {
				jQuery(this).data("link") && jQuery(this).data("link") != undefined && (i.layeranim = !0, i.actions = !0)
			}), !i.videos && (0 < e.find(".rs-background-video-layer").length || 0 < e.find(".tp-videolayer").length || 0 < e.find(".tp-audiolayer").length || 0 < e.find("iframe").length || 0 < e.find("video").length) && (i.videos = !0), "carousel" == t.sliderType && (i.carousel = !0), ("off" !== t.parallax.type || t.viewPort.enable || "true" == t.viewPort.enable || "true" === t.scrolleffect.on || t.scrolleffect.on) && (i.parallax = !0)) : (i.kenburns = !0, i.parallax = !0, i.carousel = !1, i.navigation = !0, i.videos = !0, i.actions = !0, i.layeranim = !0, i.migration = !0), "hero" == t.sliderType && (i.carousel = !1, i.navigation = !1), window.location.href.match(/file:/gi) && (i.filesystem = !0, t.filesystem = !0), i.videos && void 0 === _R.isVideoPlaying && lAjax("revolution.extension.video", t), i.carousel && void 0 === _R.prepareCarousel && lAjax("revolution.extension.carousel", t), i.carousel || void 0 !== _R.animateSlide || lAjax("revolution.extension.slideanims", t), i.actions && void 0 === _R.checkActions && lAjax("revolution.extension.actions", t), i.layeranim && void 0 === _R.handleStaticLayers && lAjax("revolution.extension.layeranimation", t), i.kenburns && void 0 === _R.stopKenBurn && lAjax("revolution.extension.kenburn", t), i.navigation && void 0 === _R.createNavigation && lAjax("revolution.extension.navigation", t), i.migration && void 0 === _R.migration && lAjax("revolution.extension.migration", t), i.parallax && void 0 === _R.checkForParallax && lAjax("revolution.extension.parallax", t), t.addons != undefined && 0 < t.addons.length && jQuery.each(t.addons, function (e, i) {
				"object" == typeof i && i.fileprefix != undefined && lAjax(i.fileprefix, t)
			}), i
		},
		waitForScripts = function (e, i) {
			var t = !0,
				a = i.scriptsneeded;
			i.addons != undefined && 0 < i.addons.length && jQuery.each(i.addons, function (e, i) {
				"object" == typeof i && i.init != undefined && _R[i.init] === undefined && (t = !1)
			}), a.filesystem || "undefined" != typeof punchgs && t && (!a.kenburns || a.kenburns && void 0 !== _R.stopKenBurn) && (!a.navigation || a.navigation && void 0 !== _R.createNavigation) && (!a.carousel || a.carousel && void 0 !== _R.prepareCarousel) && (!a.videos || a.videos && void 0 !== _R.resetVideo) && (!a.actions || a.actions && void 0 !== _R.checkActions) && (!a.layeranim || a.layeranim && void 0 !== _R.handleStaticLayers) && (!a.migration || a.migration && void 0 !== _R.migration) && (!a.parallax || a.parallax && void 0 !== _R.checkForParallax) && (a.carousel || !a.carousel && void 0 !== _R.animateSlide) ? e.trigger("scriptsloaded") : setTimeout(function () {
				waitForScripts(e, i)
			}, 50)
		},
		getScriptLocation = function (e) {
			var i = new RegExp("themepunch.revolution.min.js", "gi"),
				t = "";
			return jQuery("script").each(function () {
				var e = jQuery(this).attr("src");
				e && e.match(i) && (t = e)
			}), t = (t = (t = t.replace("jquery.themepunch.revolution.min.js", "")).replace("jquery.themepunch.revolution.js", "")).split("?")[0]
		},
		setCurWinRange = function (e, i) {
			var t = 9999,
				a = 0,
				n = 0,
				r = 0,
				o = jQuery(window).width(),
				s = i && 9999 == e.responsiveLevels ? e.visibilityLevels : e.responsiveLevels;
			s && s.length && jQuery.each(s, function (e, i) {
				o < i && (0 == a || i < a) && (r = e, a = t = i), i < o && a < i && (a = i, n = e)
			}), a < t && (r = n), i ? e.forcedWinRange = r : e.curWinRange = r
		},
		prepareOptions = function (e, i) {
			i.carousel.maxVisibleItems = i.carousel.maxVisibleItems < 1 ? 999 : i.carousel.maxVisibleItems, i.carousel.vertical_align = "top" === i.carousel.vertical_align ? "0%" : "bottom" === i.carousel.vertical_align ? "100%" : "50%"
		},
		gWiderOut = function (e, i) {
			var t = 0;
			return e.find(i).each(function () {
				var e = jQuery(this);
				!e.hasClass("tp-forcenotvisible") && t < e.outerWidth() && (t = e.outerWidth())
			}), t
		},
		initSlider = function (container, opt) {
			if (container == undefined) return !1;
			container.data("aimg") != undefined && ("enabled" == container.data("aie8") && _R.isIE(8) || "enabled" == container.data("amobile") && _ISM) && container.html('<img class="tp-slider-alternative-image" src="' + container.data("aimg") + '">'), container.find(">ul").addClass("tp-revslider-mainul"), opt.c = container, opt.ul = container.find(".tp-revslider-mainul"), opt.ul.find(">li").each(function (e) {
				var i = jQuery(this);
				"on" == i.data("hideslideonmobile") && _ISM && i.remove(), (i.data("invisible") || !0 === i.data("invisible")) && (i.addClass("tp-invisible-slide"), i.appendTo(opt.ul))
			}), opt.addons != undefined && 0 < opt.addons.length && jQuery.each(opt.addons, function (i, obj) {
				"object" == typeof obj && obj.init != undefined && _R[obj.init](eval(obj.params))
			}), opt.cid = container.attr("id"), opt.ul.css({
				visibility: "visible"
			}), opt.slideamount = opt.ul.find(">li").not(".tp-invisible-slide").length, opt.realslideamount = opt.ul.find(">li").length, opt.slayers = container.find(".tp-static-layers"), opt.slayers.data("index", "staticlayers"), 1 != opt.waitForInit && (container[0].opt = opt, runSlider(container, opt))
		},
		onFullScreenChange = function () {
			jQuery("body").data("rs-fullScreenMode", !jQuery("body").data("rs-fullScreenMode")), jQuery("body").data("rs-fullScreenMode") && setTimeout(function () {
				jQuery(window).trigger("resize")
			}, 200)
		},
		runSlider = function (t, x) {
			if (x.sliderisrunning = !0, x.ul.find(">li").each(function (e) {
					jQuery(this).data("originalindex", e)
				}), x.allli = x.ul.find(">li"), jQuery.each(x.allli, function (e, i) {
					(i = jQuery(i)).data("origindex", i.index())
				}), x.li = x.ul.find(">li").not(".tp-invisible-slide"), "on" == x.shuffle) {
				var e = new Object,
					i = x.ul.find(">li:first-child");
				e.fstransition = i.data("fstransition"), e.fsmasterspeed = i.data("fsmasterspeed"), e.fsslotamount = i.data("fsslotamount");
				for (var a = 0; a < x.slideamount; a++) {
					var n = Math.round(Math.random() * x.slideamount);
					x.ul.find(">li:eq(" + n + ")").prependTo(x.ul)
				}
				var r = x.ul.find(">li:first-child");
				r.data("fstransition", e.fstransition), r.data("fsmasterspeed", e.fsmasterspeed), r.data("fsslotamount", e.fsslotamount), x.allli = x.ul.find(">li"), x.li = x.ul.find(">li").not(".tp-invisible-slide")
			}
			if (x.inli = x.ul.find(">li.tp-invisible-slide"), x.thumbs = new Array, x.slots = 4, x.act = -1, x.firststart = 1, x.loadqueue = new Array, x.syncload = 0, x.conw = t.width(), x.conh = t.height(), 1 < x.responsiveLevels.length ? x.responsiveLevels[0] = 9999 : x.responsiveLevels = 9999, jQuery.each(x.allli, function (e, i) {
					var t = (i = jQuery(i)).find(".rev-slidebg") || i.find("img").first(),
						a = 0;
					i.addClass("tp-revslider-slidesli"), i.data("index") === undefined && i.data("index", "rs-" + Math.round(999999 * Math.random()));
					var n = new Object;
					n.params = new Array, n.id = i.data("index"), n.src = i.data("thumb") !== undefined ? i.data("thumb") : t.data("lazyload") !== undefined ? t.data("lazyload") : t.attr("src"), i.data("title") !== undefined && n.params.push({
						from: RegExp("\\{\\{title\\}\\}", "g"),
						to: i.data("title")
					}), i.data("description") !== undefined && n.params.push({
						from: RegExp("\\{\\{description\\}\\}", "g"),
						to: i.data("description")
					});
					for (a = 1; a <= 10; a++) i.data("param" + a) !== undefined && n.params.push({
						from: RegExp("\\{\\{param" + a + "\\}\\}", "g"),
						to: i.data("param" + a)
					});
					if (x.thumbs.push(n), i.data("link") != undefined) {
						var r = i.data("link"),
							o = i.data("target") || "_self",
							s = "back" === i.data("slideindex") ? 0 : 60,
							l = i.data("linktoslide"),
							d = l;
						l != undefined && "next" != l && "prev" != l && x.allli.each(function () {
							var e = jQuery(this);
							e.data("origindex") + 1 == d && (l = e.data("index"))
						}), "slide" != r && (l = "no");
						var c = '<div class="tp-caption slidelink" style="cursor:pointer;width:100%;height:100%;z-index:' + s + ';" data-x="center" data-y="center" data-basealign="slide" ',
							u = ' data-frames=\'[{"delay":0,"speed":100,"frame":"0","from":"opacity:0;","to":"o:1;","ease":"Power3.easeInOut"},{"delay":"wait","speed":300,"frame":"999","to":"opacity:0;","ease":"Power3.easeInOut"}]\'';
						c = "no" == l ? c + u + " >" : c + "data-actions='" + ("scroll_under" === l ? '[{"event":"click","action":"scrollbelow","offset":"100px","delay":"0"}]' : "prev" === l ? '[{"event":"click","action":"jumptoslide","slide":"prev","delay":"0.2"}]' : "next" === l ? '[{"event":"click","action":"jumptoslide","slide":"next","delay":"0.2"}]' : '[{"event":"click","action":"jumptoslide","slide":"' + l + '","delay":"0.2"}]') + "'" + u + " >", c += '<a style="width:100%;height:100%;display:block"', c = "slide" != r ? c + ' target="' + o + '" href="' + r + '"' : c, c += '><span style="width:100%;height:100%;display:block"></span></a></div>', i.append(c)
					}
				}), x.rle = x.responsiveLevels.length || 1, x.gridwidth = cArray(x.gridwidth, x.rle), x.gridheight = cArray(x.gridheight, x.rle), "on" == x.simplifyAll && (_R.isIE(8) || _R.iOSVersion()) && (t.find(".tp-caption").each(function () {
					var e = jQuery(this);
					e.removeClass("customin customout").addClass("fadein fadeout"), e.data("splitin", ""), e.data("speed", 400)
				}), x.allli.each(function () {
					var e = jQuery(this);
					e.data("transition", "fade"), e.data("masterspeed", 500), e.data("slotamount", 1), (e.find(".rev-slidebg") || e.find(">img").first()).data("kenburns", "off")
				})), x.desktop = !navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry|BB10|mobi|tablet|opera mini|nexus 7)/i), x.autoHeight = "fullscreen" == x.sliderLayout ? "on" : x.autoHeight, "fullwidth" == x.sliderLayout && "off" == x.autoHeight && t.css({
					maxHeight: x.gridheight[x.curWinRange] + "px"
				}), "auto" != x.sliderLayout && 0 == t.closest(".forcefullwidth_wrapper_tp_banner").length && ("fullscreen" !== x.sliderLayout || "on" != x.fullScreenAutoWidth)) {
				var o = t.parent(),
					s = o.css("marginBottom"),
					l = o.css("marginTop"),
					d = t.attr("id") + "_forcefullwidth";
				s = s === undefined ? 0 : s, l = l === undefined ? 0 : l, o.wrap('<div class="forcefullwidth_wrapper_tp_banner" id="' + d + '" style="position:relative;width:100%;height:auto;margin-top:' + l + ";margin-bottom:" + s + '"></div>'), t.closest(".forcefullwidth_wrapper_tp_banner").append('<div class="tp-fullwidth-forcer" style="width:100%;height:' + t.height() + 'px"></div>'), t.parent().css({
					marginTop: "0px",
					marginBottom: "0px"
				}), t.parent().css({
					position: "absolute"
				})
			}
			if (x.shadow !== undefined && 0 < x.shadow && (t.parent().addClass("tp-shadow" + x.shadow), t.parent().append('<div class="tp-shadowcover"></div>'), t.parent().find(".tp-shadowcover").css({
					backgroundColor: t.parent().css("backgroundColor"),
					backgroundImage: t.parent().css("backgroundImage")
				})), setCurWinRange(x), setCurWinRange(x, !0), !t.hasClass("revslider-initialised")) {
				t.addClass("revslider-initialised"), t.addClass("tp-simpleresponsive"), t.attr("id") == undefined && t.attr("id", "revslider-" + Math.round(1e3 * Math.random() + 5)), checkIDS(x, t), x.firefox13 = !1, x.ie = !jQuery.support.opacity, x.ie9 = 9 == document.documentMode, x.origcd = x.delay;
				var c = jQuery.fn.jquery.split("."),
					u = parseFloat(c[0]),
					p = parseFloat(c[1]);
				parseFloat(c[2] || "0");
				1 == u && p < 7 && t.html('<div style="text-align:center; padding:40px 0px; font-size:20px; color:#992222;"> The Current Version of jQuery:' + c + " <br>Please update your jQuery Version to min. 1.7 in Case you wish to use the Revolution Slider Plugin</div>"), 1 < u && (x.ie = !1);
				var j = new Object;
				j.addedyt = 0, j.addedvim = 0, j.addedvid = 0, x.scrolleffect.on && (x.scrolleffect.layers = new Array), t.find(".tp-caption, .rs-background-video-layer").each(function (e) {
					var n = jQuery(this),
						i = n.data(),
						t = i.autoplayonlyfirsttime,
						a = i.autoplay,
						r = (i.videomp4 !== undefined || i.videowebm !== undefined || i.videoogv, n.hasClass("tp-audiolayer")),
						o = i.videoloop,
						s = !0,
						l = !1;
					i.startclasses = n.attr("class"), i.isparallaxlayer = 0 <= i.startclasses.indexOf("rs-parallax"), n.hasClass("tp-static-layer") && _R.handleStaticLayers && (_R.handleStaticLayers(n, x), x.scrolleffect.on && ("on" === x.scrolleffect.on_parallax_static_layers && i.isparallaxlayer || "on" === x.scrolleffect.on_static_layers && !i.isparallaxlayer) && (l = !0), s = !1);
					var d = n.data("noposteronmobile") || n.data("noPosterOnMobile") || n.data("posteronmobile") || n.data("posterOnMobile") || n.data("posterOnMObile");
					n.data("noposteronmobile", d);
					var c = 0;
					if (n.find("iframe").each(function () {
							punchgs.TweenLite.set(jQuery(this), {
								autoAlpha: 0
							}), c++
						}), 0 < c && n.data("iframes", !0), n.hasClass("tp-caption")) {
						var u = n.hasClass("slidelink") ? "width:100% !important;height:100% !important;" : "",
							p = n.data(),
							f = "",
							h = p.type,
							g = "row" === h || "column" === h ? "relative" : "absolute",
							v = "";
						"row" === h ? (n.addClass("rev_row").removeClass("tp-resizeme"), v = "rev_row_wrap") : "column" === h ? (f = p.verticalalign === undefined ? " vertical-align:bottom;" : " vertical-align:" + p.verticalalign + ";", v = "rev_column", n.addClass("rev_column_inner").removeClass("tp-resizeme"), n.data("width", "auto"), punchgs.TweenLite.set(n, {
							width: "auto"
						})) : "group" === h && n.removeClass("tp-resizeme");
						var m = "",
							y = "";
						"row" !== h && "group" !== h && "column" !== h ? (m = "display:" + n.css("display") + ";", 0 < n.closest(".rev_column").length ? (n.addClass("rev_layer_in_column"), s = !1) : 0 < n.closest(".rev_group").length && (n.addClass("rev_layer_in_group"), s = !1)) : "column" === h && (s = !1), p.wrapper_class !== undefined && (v = v + " " + p.wrapper_class), p.wrapper_id !== undefined && (y = 'id="' + p.wrapper_id + '"');
						var w = "";
						n.hasClass("tp-no-events") && (w = ";pointer-events:none"), n.wrap("<div " + y + ' class="tp-parallax-wrap ' + v + '" style="' + f + " " + u + "position:" + g + ";" + m + ";visibility:hidden" + w + '"><div class="tp-loop-wrap" style="' + u + "position:" + g + ";" + m + ';"><div class="tp-mask-wrap" style="' + u + "position:" + g + ";" + m + ';" ></div></div></div>'), s && x.scrolleffect.on && ("on" === x.scrolleffect.on_parallax_layers && i.isparallaxlayer || "on" === x.scrolleffect.on_layers && !i.isparallaxlayer) && x.scrolleffect.layers.push(n.parent()), l && x.scrolleffect.layers.push(n.parent()), "column" === h && (n.append('<div class="rev_column_bg rev_column_bg_man_sized" style="visibility:hidden"></div>'), n.closest(".tp-parallax-wrap").append('<div class="rev_column_bg rev_column_bg_auto_sized"></div>'));
						var b = n.closest(".tp-loop-wrap");
						jQuery.each(["pendulum", "rotate", "slideloop", "pulse", "wave"], function (e, i) {
							var t = n.find(".rs-" + i),
								a = t.data() || "";
							"" != a && (b.data(a), b.addClass("rs-" + i), t.children(0).unwrap(), n.data("loopanimation", "on"))
						}), n.attr("id") === undefined && n.attr("id", "layer-" + Math.round(999999999 * Math.random())), checkIDS(x, n), punchgs.TweenLite.set(n, {
							visibility: "hidden"
						})
					}
					var _ = n.data("actions");
					_ !== undefined && _R.checkActions(n, x, _), checkHoverDependencies(n, x), _R.checkVideoApis && (j = _R.checkVideoApis(n, x, j)), r || 1 != t && "true" != t && "1sttime" != a || "loopandnoslidestop" == o || n.closest("li.tp-revslider-slidesli").addClass("rs-pause-timer-once"), r || 1 != a && "true" != a && "on" != a && "no1sttime" != a || "loopandnoslidestop" == o || n.closest("li.tp-revslider-slidesli").addClass("rs-pause-timer-always")
				}), t[0].addEventListener("mouseenter", function () {
					t.trigger("tp-mouseenter"), x.overcontainer = !0
				}, {
					passive: !0
				}), t[0].addEventListener("mouseover", function () {
					t.trigger("tp-mouseover"), x.overcontainer = !0
				}, {
					passive: !0
				}), t[0].addEventListener("mouseleave", function () {
					t.trigger("tp-mouseleft"), x.overcontainer = !1
				}, {
					passive: !0
				}), t.find(".tp-caption video").each(function (e) {
					var i = jQuery(this);
					i.removeClass("video-js vjs-default-skin"), i.attr("preload", ""), i.css({
						display: "none"
					})
				}), "standard" !== x.sliderType && (x.lazyType = "all"), loadImages(t.find(".tp-static-layers"), x, 0, !0), waitForCurrentImages(t.find(".tp-static-layers"), x, function () {
					t.find(".tp-static-layers img").each(function () {
						var e = jQuery(this),
							i = e.data("lazyload") != undefined ? e.data("lazyload") : e.attr("src"),
							t = getLoadObj(x, i);
						e.attr("src", t.src)
					})
				}), x.rowzones = [], x.allli.each(function (e) {
					var i = jQuery(this);
					x.rowzones[e] = [], i.find(".rev_row_zone").each(function () {
						x.rowzones[e].push(jQuery(this))
					}), "all" != x.lazyType && ("smart" != x.lazyType || 0 != e && 1 != e && e != x.slideamount && e != x.slideamount - 1) || (loadImages(i, x, e), waitForCurrentImages(i, x, function () {}))
				});
				var f = getUrlVars("#")[0];
				if (f.length < 9 && 1 < f.split("slide").length) {
					var h = parseInt(f.split("slide")[1], 0);
					h < 1 && (h = 1), h > x.slideamount && (h = x.slideamount), x.startWithSlide = h - 1
				}
				t.append('<div class="tp-loader ' + x.spinner + '"><div class="dot1"></div><div class="dot2"></div><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>'), x.loader = t.find(".tp-loader"), 0 === t.find(".tp-bannertimer").length && t.append('<div class="tp-bannertimer" style="visibility:hidden"></div>'), t.find(".tp-bannertimer").css({
					width: "0%"
				}), x.ul.css({
					display: "block"
				}), prepareSlides(t, x), ("off" !== x.parallax.type || x.scrolleffect.on) && _R.checkForParallax && _R.checkForParallax(t, x), _R.setSize(x), "hero" !== x.sliderType && _R.createNavigation && _R.createNavigation(t, x), _R.resizeThumbsTabs && _R.resizeThumbsTabs && _R.resizeThumbsTabs(x), contWidthManager(x);
				var g = x.viewPort;
				x.inviewport = !1, g != undefined && g.enable && (jQuery.isNumeric(g.visible_area) || -1 !== g.visible_area.indexOf("%") && (g.visible_area = parseInt(g.visible_area) / 100), _R.scrollTicker && _R.scrollTicker(x, t)), "carousel" === x.sliderType && _R.prepareCarousel && (punchgs.TweenLite.set(x.ul, {
					opacity: 0
				}), _R.prepareCarousel(x, new punchgs.TimelineLite, undefined, 0), x.onlyPreparedSlide = !0), setTimeout(function () {
					if (!g.enable || g.enable && x.inviewport || g.enable && !x.inviewport && "wait" == !g.outof) swapSlide(t);
					else if (x.c.addClass("tp-waitforfirststart"), x.waitForFirstSlide = !0, g.presize) {
						var e = jQuery(x.li[0]);
						loadImages(e, x, 0, !0), waitForCurrentImages(e.find(".tp-layers"), x, function () {
							_R.animateTheCaptions({
								slide: e,
								opt: x,
								preset: !0
							})
						})
					}
					_R.manageNavigation && _R.manageNavigation(x), 1 < x.slideamount && (!g.enable || g.enable && x.inviewport ? countDown(t, x) : x.waitForCountDown = !0), setTimeout(function () {
						t.trigger("revolution.slide.onloaded")
					}, 100)
				}, x.startDelay), x.startDelay = 0, jQuery("body").data("rs-fullScreenMode", !1), window.addEventListener("fullscreenchange", onFullScreenChange, {
					passive: !0
				}), window.addEventListener("mozfullscreenchange", onFullScreenChange, {
					passive: !0
				}), window.addEventListener("webkitfullscreenchange", onFullScreenChange, {
					passive: !0
				});
				var v = "resize.revslider-" + t.attr("id");
				jQuery(window).on(v, function () {
					if (t == undefined) return !1;
					0 != jQuery("body").find(t) && contWidthManager(x);
					var e = !1;
					if ("fullscreen" == x.sliderLayout) {
						var i = jQuery(window).height();
						"mobile" == x.fallbacks.ignoreHeightChanges && _ISM || "always" == x.fallbacks.ignoreHeightChanges ? (x.fallbacks.ignoreHeightChangesSize = x.fallbacks.ignoreHeightChangesSize == undefined ? 0 : x.fallbacks.ignoreHeightChangesSize, e = i != x.lastwindowheight && Math.abs(i - x.lastwindowheight) > x.fallbacks.ignoreHeightChangesSize) : e = i != x.lastwindowheight
					}(t.outerWidth(!0) != x.width || t.is(":hidden") || e) && (x.lastwindowheight = jQuery(window).height(), containerResized(t, x))
				}), hideSliderUnder(t, x), contWidthManager(x), x.fallbacks.disableFocusListener || "true" == x.fallbacks.disableFocusListener || !0 === x.fallbacks.disableFocusListener || (t.addClass("rev_redraw_on_blurfocus"), tabBlurringCheck())
			}
		},
		cArray = function (e, i) {
			if (!jQuery.isArray(e)) {
				var t = e;
				(e = new Array).push(t)
			}
			if (e.length < i) {
				t = e[e.length - 1];
				for (var a = 0; a < i - e.length + 2; a++) e.push(t)
			}
			return e
		},
		checkHoverDependencies = function (e, n) {
			var i = e.data();
			("sliderenter" === i.start || i.frames !== undefined && i.frames[0] != undefined && "sliderenter" === i.frames[0].delay) && (n.layersonhover === undefined && (n.c.on("tp-mouseenter", function () {
				n.layersonhover && jQuery.each(n.layersonhover, function (e, i) {
					var t = i.data("closestli") || i.closest(".tp-revslider-slidesli"),
						a = i.data("staticli") || i.closest(".tp-static-layers");
					i.data("closestli") === undefined && (i.data("closestli", t), i.data("staticli", a)), (0 < t.length && t.hasClass("active-revslide") || t.hasClass("processing-revslide") || 0 < a.length) && (i.data("animdirection", "in"), _R.playAnimationFrame && _R.playAnimationFrame({
						caption: i,
						opt: n,
						frame: "frame_0",
						triggerdirection: "in",
						triggerframein: "frame_0",
						triggerframeout: "frame_999"
					}), i.data("triggerstate", "on"))
				})
			}), n.c.on("tp-mouseleft", function () {
				n.layersonhover && jQuery.each(n.layersonhover, function (e, i) {
					i.data("animdirection", "out"), i.data("triggered", !0), i.data("triggerstate", "off"), _R.stopVideo && _R.stopVideo(i, n), _R.playAnimationFrame && _R.playAnimationFrame({
						caption: i,
						opt: n,
						frame: "frame_999",
						triggerdirection: "out",
						triggerframein: "frame_0",
						triggerframeout: "frame_999"
					})
				})
			}), n.layersonhover = new Array), n.layersonhover.push(e))
		},
		contWidthManager = function (e) {
			var i = _R.getHorizontalOffset(e.c, "left");
			if ("auto" == e.sliderLayout || "fullscreen" === e.sliderLayout && "on" == e.fullScreenAutoWidth) "fullscreen" == e.sliderLayout && "on" == e.fullScreenAutoWidth ? punchgs.TweenLite.set(e.ul, {
				left: 0,
				width: e.c.width()
			}) : punchgs.TweenLite.set(e.ul, {
				left: i,
				width: e.c.width() - _R.getHorizontalOffset(e.c, "both")
			});
			else {
				var t = Math.ceil(e.c.closest(".forcefullwidth_wrapper_tp_banner").offset().left - i);
				punchgs.TweenLite.set(e.c.parent(), {
					left: 0 - t + "px",
					width: jQuery(window).width() - _R.getHorizontalOffset(e.c, "both")
				})
			}
			e.slayers && "fullwidth" != e.sliderLayout && "fullscreen" != e.sliderLayout && punchgs.TweenLite.set(e.slayers, {
				left: i
			})
		},
		cv = function (e, i) {
			return e === undefined ? i : e
		},
		hideSliderUnder = function (e, i, t) {
			var a = e.parent();
			jQuery(window).width() < i.hideSliderAtLimit ? (e.trigger("stoptimer"), "none" != a.css("display") && a.data("olddisplay", a.css("display")), a.css({
				display: "none"
			})) : e.is(":hidden") && t && (a.data("olddisplay") != undefined && "undefined" != a.data("olddisplay") && "none" != a.data("olddisplay") ? a.css({
				display: a.data("olddisplay")
			}) : a.css({
				display: "block"
			}), e.trigger("restarttimer"), setTimeout(function () {
				containerResized(e, i)
			}, 150)), _R.hideUnHideNav && _R.hideUnHideNav(i)
		},
		containerResized = function (e, i) {
			if (e.trigger("revolution.slide.beforeredraw"), 1 == i.infullscreenmode && (i.minHeight = jQuery(window).height()), setCurWinRange(i), setCurWinRange(i, !0), !_R.resizeThumbsTabs || !0 === _R.resizeThumbsTabs(i)) {
				if (hideSliderUnder(e, i, !0), contWidthManager(i), "carousel" == i.sliderType && _R.prepareCarousel(i, !0), e === undefined) return !1;
				_R.setSize(i), i.conw = i.c.width(), i.conh = i.infullscreenmode ? i.minHeight : i.c.height();
				var t = e.find(".active-revslide .slotholder"),
					a = e.find(".processing-revslide .slotholder");
				removeSlots(e, i, e, 2), "standard" === i.sliderType && (punchgs.TweenLite.set(a.find(".defaultimg"), {
					opacity: 0
				}), t.find(".defaultimg").css({
					opacity: 1
				})), "carousel" === i.sliderType && i.lastconw != i.conw && (clearTimeout(i.pcartimer), i.pcartimer = setTimeout(function () {
					_R.prepareCarousel(i, !0), "carousel" == i.sliderType && "on" === i.carousel.showLayersAllTime && jQuery.each(i.li, function (e) {
						_R.animateTheCaptions({
							slide: jQuery(i.li[e]),
							opt: i,
							recall: !0
						})
					})
				}, 100), i.lastconw = i.conw), _R.manageNavigation && _R.manageNavigation(i), _R.animateTheCaptions && 0 < e.find(".active-revslide").length && _R.animateTheCaptions({
					slide: e.find(".active-revslide"),
					opt: i,
					recall: !0
				}), "on" == a.data("kenburns") && _R.startKenBurn(a, i, a.data("kbtl") !== undefined ? a.data("kbtl").progress() : 0), "on" == t.data("kenburns") && _R.startKenBurn(t, i, t.data("kbtl") !== undefined ? t.data("kbtl").progress() : 0), _R.animateTheCaptions && 0 < e.find(".processing-revslide").length && _R.animateTheCaptions({
					slide: e.find(".processing-revslide"),
					opt: i,
					recall: !0
				}), _R.manageNavigation && _R.manageNavigation(i)
			}
			e.trigger("revolution.slide.afterdraw")
		},
		setScale = function (e) {
			e.bw = e.width / e.gridwidth[e.curWinRange], e.bh = e.height / e.gridheight[e.curWinRange], e.bh > e.bw ? e.bh = e.bw : e.bw = e.bh, (1 < e.bh || 1 < e.bw) && (e.bw = 1, e.bh = 1)
		},
		prepareSlides = function (e, u) {
			if (e.find(".tp-caption").each(function () {
					var e = jQuery(this);
					e.data("transition") !== undefined && e.addClass(e.data("transition"))
				}), u.ul.css({
					overflow: "hidden",
					width: "100%",
					height: "100%",
					maxHeight: e.parent().css("maxHeight")
				}), "on" == u.autoHeight && (u.ul.css({
					overflow: "hidden",
					width: "100%",
					height: "100%",
					maxHeight: "none"
				}), e.css({
					maxHeight: "none"
				}), e.parent().css({
					maxHeight: "none"
				})), u.allli.each(function (e) {
					var i = jQuery(this),
						t = i.data("originalindex");
					(u.startWithSlide != undefined && t == u.startWithSlide || u.startWithSlide === undefined && 0 == e) && i.addClass("next-revslide"), i.css({
						width: "100%",
						height: "100%",
						overflow: "hidden"
					})
				}), "carousel" === u.sliderType) {
				u.ul.css({
					overflow: "visible"
				}).wrap('<div class="tp-carousel-wrapper" style="width:100%;height:100%;position:absolute;top:0px;left:0px;overflow:hidden;"></div>');
				var i = '<div style="clear:both;display:block;width:100%;height:1px;position:relative;margin-bottom:-1px"></div>';
				u.c.parent().prepend(i), u.c.parent().append(i), _R.prepareCarousel(u)
			}
			e.parent().css({
				overflow: "visible"
			}), u.allli.find(">img").each(function (e) {
				var i = jQuery(this),
					t = i.closest("li"),
					a = t.find(".rs-background-video-layer");
				a.addClass("defaultvid").css({
					zIndex: 30
				}), i.addClass("defaultimg"), "on" == u.fallbacks.panZoomDisableOnMobile && _ISM && (i.data("kenburns", "off"), i.data("bgfit", "cover"));
				var n = t.data("mediafilter");
				n = "none" === n || n === undefined ? "" : n, i.wrap('<div class="slotholder" style="position:absolute; top:0px; left:0px; z-index:0;width:100%;height:100%;"></div>'), a.appendTo(t.find(".slotholder"));
				var r = i.data();
				i.closest(".slotholder").data(r), 0 < a.length && r.bgparallax != undefined && (a.data("bgparallax", r.bgparallax), a.data("showcoveronpause", "on")), "none" != u.dottedOverlay && u.dottedOverlay != undefined && i.closest(".slotholder").append('<div class="tp-dottedoverlay ' + u.dottedOverlay + '"></div>');
				var o = i.attr("src");
				r.src = o, r.bgfit = r.bgfit || "cover", r.bgrepeat = r.bgrepeat || "no-repeat", r.bgposition = r.bgposition || "center center";
				i.closest(".slotholder");
				var s = i.data("bgcolor"),
					l = "";
				l = s !== undefined && 0 <= s.indexOf("gradient") ? '"background:' + s + ';width:100%;height:100%;"' : '"background-color:' + s + ";background-repeat:" + r.bgrepeat + ";background-image:url(" + o + ");background-size:" + r.bgfit + ";background-position:" + r.bgposition + ';width:100%;height:100%;"', i.data("mediafilter", n), n = "on" === i.data("kenburns") ? "" : n;
				var d = jQuery('<div class="tp-bgimg defaultimg ' + n + '" data-bgcolor="' + s + '" style=' + l + "></div>");
				i.parent().append(d);
				var c = document.createComment("Runtime Modification - Img tag is Still Available for SEO Goals in Source - " + i.get(0).outerHTML);
				i.replaceWith(c), d.data(r), d.attr("src", o), "standard" !== u.sliderType && "undefined" !== u.sliderType || d.css({
					opacity: 0
				})
			}), u.scrolleffect.on && "on" === u.scrolleffect.on_slidebg && (u.allslotholder = new Array, u.allli.find(".slotholder").each(function () {
				jQuery(this).wrap('<div style="display:block;position:absolute;top:0px;left:0px;width:100%;height:100%" class="slotholder_fadeoutwrap"></div>')
			}), u.allslotholder = u.c.find(".slotholder_fadeoutwrap"))
		},
		removeSlots = function (e, i, t, a) {
			i.removePrepare = i.removePrepare + a, t.find(".slot, .slot-circle-wrapper").each(function () {
				jQuery(this).remove()
			}), i.transition = 0, i.removePrepare = 0
		},
		cutParams = function (e) {
			var i = e;
			return e != undefined && 0 < e.length && (i = e.split("?")[0]), i
		},
		relativeRedir = function (e) {
			return location.pathname.replace(/(.*)\/[^/]*/, "$1/" + e)
		},
		abstorel = function (e, i) {
			var t = e.split("/"),
				a = i.split("/");
			t.pop();
			for (var n = 0; n < a.length; n++) "." != a[n] && (".." == a[n] ? t.pop() : t.push(a[n]));
			return t.join("/")
		},
		imgLoaded = function (l, e, d) {
			e.syncload--, e.loadqueue && jQuery.each(e.loadqueue, function (e, i) {
				var t = i.src.replace(/\.\.\/\.\.\//gi, ""),
					a = self.location.href,
					n = document.location.origin,
					r = a.substring(0, a.length - 1) + "/" + t,
					o = n + "/" + t,
					s = abstorel(self.location.href, i.src);
				a = a.substring(0, a.length - 1) + t, (cutParams(n += t) === cutParams(decodeURIComponent(l.src)) || cutParams(a) === cutParams(decodeURIComponent(l.src)) || cutParams(s) === cutParams(decodeURIComponent(l.src)) || cutParams(o) === cutParams(decodeURIComponent(l.src)) || cutParams(r) === cutParams(decodeURIComponent(l.src)) || cutParams(i.src) === cutParams(decodeURIComponent(l.src)) || cutParams(i.src).replace(/^.*\/\/[^\/]+/, "") === cutParams(decodeURIComponent(l.src)).replace(/^.*\/\/[^\/]+/, "") || "file://" === window.location.origin && cutParams(l.src).match(new RegExp(t))) && (i.progress = d, i.width = l.width, i.height = l.height)
			}), progressImageLoad(e)
		},
		progressImageLoad = function (a) {
			3 != a.syncload && a.loadqueue && jQuery.each(a.loadqueue, function (e, i) {
				if (i.progress.match(/prepared/g) && a.syncload <= 3) {
					if (a.syncload++, "img" == i.type) {
						var t = new Image;
						t.onload = function () {
							imgLoaded(this, a, "loaded"), i.error = !1
						}, t.onerror = function () {
							imgLoaded(this, a, "failed"), i.error = !0
						}, t.src = i.src
					} else jQuery.get(i.src, function (e) {
						i.innerHTML = (new XMLSerializer).serializeToString(e.documentElement), i.progress = "loaded", a.syncload--, progressImageLoad(a)
					}).fail(function () {
						i.progress = "failed", a.syncload--, progressImageLoad(a)
					});
					i.progress = "inload"
				}
			})
		},
		addToLoadQueue = function (t, e, i, a, n) {
			var r = !1;
			if (e.loadqueue && jQuery.each(e.loadqueue, function (e, i) {
					i.src === t && (r = !0)
				}), !r) {
				var o = new Object;
				o.src = t, o.starttoload = jQuery.now(), o.type = a || "img", o.prio = i, o.progress = "prepared", o.static = n, e.loadqueue.push(o)
			}
		},
		loadImages = function (e, a, n, r) {
			e.find("img,.defaultimg, .tp-svg-layer").each(function () {
				var e = jQuery(this),
					i = e.data("lazyload") !== undefined && "undefined" !== e.data("lazyload") ? e.data("lazyload") : e.data("svg_src") != undefined ? e.data("svg_src") : e.attr("src"),
					t = e.data("svg_src") != undefined ? "svg" : "img";
				e.data("start-to-load", jQuery.now()), addToLoadQueue(i, a, n, t, r)
			}), progressImageLoad(a)
		},
		getLoadObj = function (e, t) {
			var a = new Object;
			return e.loadqueue && jQuery.each(e.loadqueue, function (e, i) {
				i.src == t && (a = i)
			}), a
		},
		waitForCurrentImages = function (o, s, e) {
			var l = !1;
			o.find("img,.defaultimg, .tp-svg-layer").each(function () {
				var e = jQuery(this),
					i = e.data("lazyload") != undefined ? e.data("lazyload") : e.data("svg_src") != undefined ? e.data("svg_src") : e.attr("src"),
					t = getLoadObj(s, i);
				if (e.data("loaded") === undefined && t !== undefined && t.progress && t.progress.match(/loaded/g)) {
					if (e.attr("src", t.src), "img" == t.type)
						if (e.hasClass("defaultimg")) _R.isIE(8) ? defimg.attr("src", t.src) : -1 == t.src.indexOf("images/transparent.png") && -1 == t.src.indexOf("assets/transparent.png") || e.data("bgcolor") === undefined ? e.css({
							backgroundImage: 'url("' + t.src + '")'
						}) : e.data("bgcolor") !== undefined && e.css({
							background: e.data("bgcolor")
						}), o.data("owidth", t.width), o.data("oheight", t.height), o.find(".slotholder").data("owidth", t.width), o.find(".slotholder").data("oheight", t.height);
						else {
							var a = e.data("ww"),
								n = e.data("hh");
							e.data("owidth", t.width), e.data("oheight", t.height), a = a == undefined || "auto" == a || "" == a ? t.width : a, n = n == undefined || "auto" == n || "" == n ? t.height : n, !jQuery.isNumeric(a) && 0 < a.indexOf("%") && (n = a), e.data("ww", a), e.data("hh", n)
						}
					else "svg" == t.type && "loaded" == t.progress && (e.append('<div class="tp-svg-innercontainer"></div>'), e.find(".tp-svg-innercontainer").append(t.innerHTML));
					e.data("loaded", !0)
				}
				if (t && t.progress && t.progress.match(/inprogress|inload|prepared/g) && (!t.error && jQuery.now() - e.data("start-to-load") < 5e3 ? l = !0 : (t.progress = "failed", t.reported_img || (t.reported_img = !0, console.warn(i + "  Could not be loaded !")))), 1 == s.youtubeapineeded && (!window.YT || YT.Player == undefined) && (l = !0, 5e3 < jQuery.now() - s.youtubestarttime && 1 != s.youtubewarning)) {
					s.youtubewarning = !0;
					var r = "YouTube Api Could not be loaded !";
					"https:" === location.protocol && (r += " Please Check and Renew SSL Certificate !"), console.error(r), s.c.append('<div style="position:absolute;top:50%;width:100%;color:#e74c3c;  font-size:16px; text-align:center; padding:15px;background:#000; display:block;"><strong>' + r + "</strong></div>")
				}
				if (1 == s.vimeoapineeded && !window.Vimeo && (l = !0, 5e3 < jQuery.now() - s.vimeostarttime && 1 != s.vimeowarning)) {
					s.vimeowarning = !0;
					r = "Vimeo Api Could not be loaded !";
					"https:" === location.protocol && (r += " Please Check and Renew SSL Certificate !"), console.error(r), s.c.append('<div style="position:absolute;top:50%;width:100%;color:#e74c3c;  font-size:16px; text-align:center; padding:15px;background:#000; display:block;"><strong>' + r + "</strong></div>")
				}
			}), !_ISM && s.audioqueue && 0 < s.audioqueue.length && jQuery.each(s.audioqueue, function (e, i) {
				i.status && "prepared" === i.status && jQuery.now() - i.start < i.waittime && (l = !0)
			}), jQuery.each(s.loadqueue, function (e, i) {
				!0 !== i.static || "loaded" == i.progress && "failed" !== i.progress || ("failed" == i.progress ? i.reported || (i.reported = !0, console.warn("Static Image " + i.src + "  Could not be loaded in time. Error Exists:" + i.error)) : !i.error && jQuery.now() - i.starttoload < 5e3 ? l = !0 : i.reported || (i.reported = !0, console.warn("Static Image " + i.src + "  Could not be loaded within 5s! Error Exists:" + i.error)))
			}), l ? punchgs.TweenLite.delayedCall(.18, waitForCurrentImages, [o, s, e]) : punchgs.TweenLite.delayedCall(.18, e)
		},
		swapSlide = function (e) {
			var i = e[0].opt;
			if (clearTimeout(i.waitWithSwapSlide), 0 < e.find(".processing-revslide").length) return i.waitWithSwapSlide = setTimeout(function () {
				swapSlide(e)
			}, 150), !1;
			var t = e.find(".active-revslide"),
				a = e.find(".next-revslide"),
				n = a.find(".defaultimg");
			if ("carousel" !== i.sliderType || i.carousel.fadein || (punchgs.TweenLite.to(i.ul, 1, {
					opacity: 1
				}), i.carousel.fadein = !0), a.index() === t.index() && !0 !== i.onlyPreparedSlide) return a.removeClass("next-revslide"), !1;
			!0 === i.onlyPreparedSlide && (i.onlyPreparedSlide = !1, jQuery(i.li[0]).addClass("processing-revslide")), a.removeClass("next-revslide").addClass("processing-revslide"), -1 === a.index() && "carousel" === i.sliderType && (a = jQuery(i.li[0])), a.data("slide_on_focus_amount", a.data("slide_on_focus_amount") + 1 || 1), "on" == i.stopLoop && a.index() == i.lastslidetoshow - 1 && (e.find(".tp-bannertimer").css({
				visibility: "hidden"
			}), e.trigger("revolution.slide.onstop"), i.noloopanymore = 1), a.index() === i.slideamount - 1 && (i.looptogo = i.looptogo - 1, i.looptogo <= 0 && (i.stopLoop = "on")), i.tonpause = !0, e.trigger("stoptimer"), i.cd = 0, "off" === i.spinner && (i.loader !== undefined ? i.loader.css({
				display: "none"
			}) : i.loadertimer = setTimeout(function () {
				i.loader !== undefined && i.loader.css({
					display: "block"
				})
			}, 50)), loadImages(a, i, 1), _R.preLoadAudio && _R.preLoadAudio(a, i, 1), waitForCurrentImages(a, i, function () {
				a.find(".rs-background-video-layer").each(function () {
					var e = jQuery(this);
					e.hasClass("HasListener") || (e.data("bgvideo", 1), _R.manageVideoLayer && _R.manageVideoLayer(e, i)), 0 == e.find(".rs-fullvideo-cover").length && e.append('<div class="rs-fullvideo-cover"></div>')
				}), swapSlideProgress(n, e)
			})
		},
		swapSlideProgress = function (e, i) {
			var t = i.find(".active-revslide"),
				a = i.find(".processing-revslide"),
				n = t.find(".slotholder"),
				r = a.find(".slotholder"),
				o = i[0].opt;
			o.tonpause = !1, o.cd = 0, clearTimeout(o.loadertimer), o.loader !== undefined && o.loader.css({
				display: "none"
			}), _R.setSize(o), _R.slotSize(e, o), _R.manageNavigation && _R.manageNavigation(o);
			var s = {};
			s.nextslide = a, s.currentslide = t, i.trigger("revolution.slide.onbeforeswap", s), o.transition = 1, o.videoplaying = !1, a.data("delay") != undefined ? (o.cd = 0, o.delay = a.data("delay")) : o.delay = o.origcd, "true" == a.data("ssop") || !0 === a.data("ssop") ? o.ssop = !0 : o.ssop = !1, i.trigger("nulltimer");
			var l = t.index(),
				d = a.index();
			o.sdir = d < l ? 1 : 0, "arrow" == o.sc_indicator && (0 == l && d == o.slideamount - 1 && (o.sdir = 1), l == o.slideamount - 1 && 0 == d && (o.sdir = 0)), o.lsdir = o.lsdir === undefined ? o.sdir : o.lsdir, o.dirc = o.lsdir != o.sdir, o.lsdir = o.sdir, t.index() != a.index() && 1 != o.firststart && _R.removeTheCaptions && _R.removeTheCaptions(t, o), a.hasClass("rs-pause-timer-once") || a.hasClass("rs-pause-timer-always") ? o.videoplaying = !0 : i.trigger("restarttimer"), a.removeClass("rs-pause-timer-once");
			var c;
			if (o.currentSlide = t.index(), o.nextSlide = a.index(), "carousel" == o.sliderType) c = new punchgs.TimelineLite, _R.prepareCarousel(o, c), letItFree(i, r, n, a, t, c), o.transition = 0, o.firststart = 0;
			else {
				(c = new punchgs.TimelineLite({
					onComplete: function () {
						letItFree(i, r, n, a, t, c)
					}
				})).add(punchgs.TweenLite.set(r.find(".defaultimg"), {
					opacity: 0
				})), c.pause(), _R.animateTheCaptions && _R.animateTheCaptions({
					slide: a,
					opt: o,
					preset: !0
				}), 1 == o.firststart && (punchgs.TweenLite.set(t, {
					autoAlpha: 0
				}), o.firststart = 0), punchgs.TweenLite.set(t, {
					zIndex: 18
				}), punchgs.TweenLite.set(a, {
					autoAlpha: 0,
					zIndex: 20
				}), "prepared" == a.data("differentissplayed") && (a.data("differentissplayed", "done"), a.data("transition", a.data("savedtransition")), a.data("slotamount", a.data("savedslotamount")), a.data("masterspeed", a.data("savedmasterspeed"))), a.data("fstransition") != undefined && "done" != a.data("differentissplayed") && (a.data("savedtransition", a.data("transition")), a.data("savedslotamount", a.data("slotamount")), a.data("savedmasterspeed", a.data("masterspeed")), a.data("transition", a.data("fstransition")), a.data("slotamount", a.data("fsslotamount")), a.data("masterspeed", a.data("fsmasterspeed")), a.data("differentissplayed", "prepared")), a.data("transition") == undefined && a.data("transition", "random"), 0;
				var u = a.data("transition") !== undefined ? a.data("transition").split(",") : "fade",
					p = a.data("nexttransid") == undefined ? -1 : a.data("nexttransid");
				"on" == a.data("randomtransition") ? p = Math.round(Math.random() * u.length) : p += 1, p == u.length && (p = 0), a.data("nexttransid", p);
				var f = u[p];
				o.ie && ("boxfade" == f && (f = "boxslide"), "slotfade-vertical" == f && (f = "slotzoom-vertical"), "slotfade-horizontal" == f && (f = "slotzoom-horizontal")), _R.isIE(8) && (f = 11), c = _R.animateSlide(0, f, i, a, t, r, n, c), "on" == r.data("kenburns") && (_R.startKenBurn(r, o), c.add(punchgs.TweenLite.set(r, {
					autoAlpha: 0
				}))), c.pause()
			}
			_R.scrollHandling && (_R.scrollHandling(o, !0, 0), c.eventCallback("onUpdate", function () {
				_R.scrollHandling(o, !0, 0)
			})), "off" != o.parallax.type && o.parallax.firstgo == undefined && _R.scrollHandling && (o.parallax.firstgo = !0, o.lastscrolltop = -999, _R.scrollHandling(o, !0, 0), setTimeout(function () {
				o.lastscrolltop = -999, _R.scrollHandling(o, !0, 0)
			}, 210), setTimeout(function () {
				o.lastscrolltop = -999, _R.scrollHandling(o, !0, 0)
			}, 420)), _R.animateTheCaptions ? "carousel" === o.sliderType && "on" === o.carousel.showLayersAllTime ? (jQuery.each(o.li, function (e) {
				o.carousel.allLayersStarted ? _R.animateTheCaptions({
					slide: jQuery(o.li[e]),
					opt: o,
					recall: !0
				}) : o.li[e] === a ? _R.animateTheCaptions({
					slide: jQuery(o.li[e]),
					maintimeline: c,
					opt: o,
					startslideanimat: 0
				}) : _R.animateTheCaptions({
					slide: jQuery(o.li[e]),
					opt: o,
					startslideanimat: 0
				})
			}), o.carousel.allLayersStarted = !0) : _R.animateTheCaptions({
				slide: a,
				opt: o,
				maintimeline: c,
				startslideanimat: 0
			}) : c != undefined && setTimeout(function () {
				c.resume()
			}, 30), punchgs.TweenLite.to(a, .001, {
				autoAlpha: 1
			})
		},
		letItFree = function (e, i, t, a, n, r) {
			var o = e[0].opt;
			"carousel" === o.sliderType || (o.removePrepare = 0, punchgs.TweenLite.to(i.find(".defaultimg"), .001, {
				zIndex: 20,
				autoAlpha: 1,
				onComplete: function () {
					removeSlots(e, o, a, 1)
				}
			}), a.index() != n.index() && punchgs.TweenLite.to(n, .2, {
				zIndex: 18,
				autoAlpha: 0,
				onComplete: function () {
					removeSlots(e, o, n, 1)
				}
			})), e.find(".active-revslide").removeClass("active-revslide"), e.find(".processing-revslide").removeClass("processing-revslide").addClass("active-revslide"), o.act = a.index(), o.c.attr("data-slideactive", e.find(".active-revslide").data("index")), "scroll" != o.parallax.type && "scroll+mouse" != o.parallax.type && "mouse+scroll" != o.parallax.type || (o.lastscrolltop = -999, _R.scrollHandling(o)), r.clear(), t.data("kbtl") != undefined && (t.data("kbtl").reverse(), t.data("kbtl").timeScale(25)), "on" == i.data("kenburns") && (i.data("kbtl") != undefined ? (i.data("kbtl").timeScale(1), i.data("kbtl").play()) : _R.startKenBurn(i, o)), a.find(".rs-background-video-layer").each(function (e) {
				if (_ISM && !o.fallbacks.allowHTML5AutoPlayOnAndroid) return !1;
				var i = jQuery(this);
				_R.resetVideo(i, o, !1, !0), punchgs.TweenLite.fromTo(i, 1, {
					autoAlpha: 0
				}, {
					autoAlpha: 1,
					ease: punchgs.Power3.easeInOut,
					delay: .2,
					onComplete: function () {
						_R.animcompleted && _R.animcompleted(i, o)
					}
				})
			}), n.find(".rs-background-video-layer").each(function (e) {
				if (_ISM) return !1;
				var i = jQuery(this);
				_R.stopVideo && (_R.resetVideo(i, o), _R.stopVideo(i, o)), punchgs.TweenLite.to(i, 1, {
					autoAlpha: 0,
					ease: punchgs.Power3.easeInOut,
					delay: .2
				})
			});
			var s = {};
			if (s.slideIndex = a.index() + 1, s.slideLIIndex = a.index(), s.slide = a, s.currentslide = a, s.prevslide = n, o.last_shown_slide = n.index(), e.trigger("revolution.slide.onchange", s), e.trigger("revolution.slide.onafterswap", s), o.startWithSlide !== undefined && "done" !== o.startWithSlide && "carousel" === o.sliderType) {
				for (var l = o.startWithSlide, d = 0; d <= o.li.length - 1; d++) {
					jQuery(o.li[d]).data("originalindex") === o.startWithSlide && (l = d)
				}
				0 !== l && _R.callingNewSlide(o.c, l), o.startWithSlide = "done"
			}
			o.duringslidechange = !1;
			var c = n.data("slide_on_focus_amount"),
				u = n.data("hideafterloop");
			0 != u && u <= c && o.c.revremoveslide(n.index());
			var p = -1 === o.nextSlide || o.nextSlide === undefined ? 0 : o.nextSlide;
			o.rowzones != undefined && (p = p > o.rowzones.length ? o.rowzones.length : p), o.rowzones != undefined && 0 < o.rowzones.length && o.rowzones[p] != undefined && 0 <= p && p <= o.rowzones.length && 0 < o.rowzones[p].length && _R.setSize(o)
		},
		removeAllListeners = function (e, i) {
			e.children().each(function () {
				try {
					jQuery(this).die("click")
				} catch (e) {}
				try {
					jQuery(this).die("mouseenter")
				} catch (e) {}
				try {
					jQuery(this).die("mouseleave")
				} catch (e) {}
				try {
					jQuery(this).unbind("hover")
				} catch (e) {}
			});
			try {
				e.die("click", "mouseenter", "mouseleave")
			} catch (e) {}
			clearInterval(i.cdint), e = null
		},
		countDown = function (e, i) {
			i.cd = 0, i.loop = 0, i.stopAfterLoops != undefined && -1 < i.stopAfterLoops ? i.looptogo = i.stopAfterLoops : i.looptogo = 9999999, i.stopAtSlide != undefined && -1 < i.stopAtSlide ? i.lastslidetoshow = i.stopAtSlide : i.lastslidetoshow = 999, i.stopLoop = "off", 0 == i.looptogo && (i.stopLoop = "on");
			var t = e.find(".tp-bannertimer");
			e.on("stoptimer", function () {
				var e = jQuery(this).find(".tp-bannertimer");
				e[0].tween.pause(), "on" == i.disableProgressBar && e.css({
					visibility: "hidden"
				}), i.sliderstatus = "paused", _R.unToggleState(i.slidertoggledby)
			}), e.on("starttimer", function () {
				i.forcepause_viatoggle || (1 != i.conthover && 1 != i.videoplaying && i.width > i.hideSliderAtLimit && 1 != i.tonpause && 1 != i.overnav && 1 != i.ssop && (1 === i.noloopanymore || i.viewPort.enable && !i.inviewport || (t.css({
					visibility: "visible"
				}), t[0].tween.resume(), i.sliderstatus = "playing")), "on" == i.disableProgressBar && t.css({
					visibility: "hidden"
				}), _R.toggleState(i.slidertoggledby))
			}), e.on("restarttimer", function () {
				if (!i.forcepause_viatoggle) {
					var e = jQuery(this).find(".tp-bannertimer");
					if (i.mouseoncontainer && "on" == i.navigation.onHoverStop && !_ISM) return !1;
					1 === i.noloopanymore || i.viewPort.enable && !i.inviewport || 1 == i.ssop || (e.css({
						visibility: "visible"
					}), e[0].tween.kill(), e[0].tween = punchgs.TweenLite.fromTo(e, i.delay / 1e3, {
						width: "0%"
					}, {
						force3D: "auto",
						width: "100%",
						ease: punchgs.Linear.easeNone,
						onComplete: a,
						delay: 1
					}), i.sliderstatus = "playing"), "on" == i.disableProgressBar && e.css({
						visibility: "hidden"
					}), _R.toggleState(i.slidertoggledby)
				}
			}), e.on("nulltimer", function () {
				t[0].tween.kill(), t[0].tween = punchgs.TweenLite.fromTo(t, i.delay / 1e3, {
					width: "0%"
				}, {
					force3D: "auto",
					width: "100%",
					ease: punchgs.Linear.easeNone,
					onComplete: a,
					delay: 1
				}), t[0].tween.pause(0), "on" == i.disableProgressBar && t.css({
					visibility: "hidden"
				}), i.sliderstatus = "paused"
			});
			var a = function () {
				0 == jQuery("body").find(e).length && (removeAllListeners(e, i), clearInterval(i.cdint)), e.trigger("revolution.slide.slideatend"), 1 == e.data("conthover-changed") && (i.conthover = e.data("conthover"), e.data("conthover-changed", 0)), _R.callingNewSlide(e, 1)
			};
			t[0].tween = punchgs.TweenLite.fromTo(t, i.delay / 1e3, {
				width: "0%"
			}, {
				force3D: "auto",
				width: "100%",
				ease: punchgs.Linear.easeNone,
				onComplete: a,
				delay: 1
			}), 1 < i.slideamount && (0 != i.stopAfterLoops || 1 != i.stopAtSlide) ? e.trigger("starttimer") : (i.noloopanymore = 1, e.trigger("nulltimer")), e.on("tp-mouseenter", function () {
				i.mouseoncontainer = !0, "on" != i.navigation.onHoverStop || _ISM || (e.trigger("stoptimer"), e.trigger("revolution.slide.onpause"))
			}), e.on("tp-mouseleft", function () {
				i.mouseoncontainer = !1, 1 != e.data("conthover") && "on" == i.navigation.onHoverStop && (1 == i.viewPort.enable && i.inviewport || 0 == i.viewPort.enable) && (e.trigger("revolution.slide.onresume"), e.trigger("starttimer"))
			})
		},
		vis = function () {
			var i, t, e = {
				hidden: "visibilitychange",
				webkitHidden: "webkitvisibilitychange",
				mozHidden: "mozvisibilitychange",
				msHidden: "msvisibilitychange"
			};
			for (i in e)
				if (i in document) {
					t = e[i];
					break
				}
			return function (e) {
				return e && document.addEventListener(t, e, {
					pasive: !0
				}), !document[i]
			}
		}(),
		restartOnFocus = function () {
			jQuery(".rev_redraw_on_blurfocus").each(function () {
				var e = jQuery(this)[0].opt;
				if (e == undefined || e.c == undefined || 0 === e.c.length) return !1;
				1 != e.windowfocused && (e.windowfocused = !0, punchgs.TweenLite.delayedCall(.3, function () {
					"on" == e.fallbacks.nextSlideOnWindowFocus && e.c.revnext(), e.c.revredraw(), "playing" == e.lastsliderstatus && e.c.revresume()
				}))
			})
		},
		lastStatBlur = function () {
			jQuery(".rev_redraw_on_blurfocus").each(function () {
				var e = jQuery(this)[0].opt;
				e.windowfocused = !1, e.lastsliderstatus = e.sliderstatus, e.c.revpause();
				var i = e.c.find(".active-revslide .slotholder"),
					t = e.c.find(".processing-revslide .slotholder");
				"on" == t.data("kenburns") && _R.stopKenBurn(t, e), "on" == i.data("kenburns") && _R.stopKenBurn(i, e)
			})
		},
		tabBlurringCheck = function () {
			var e = document.documentMode === undefined,
				i = window.chrome;
			1 !== jQuery("body").data("revslider_focus_blur_listener") && (jQuery("body").data("revslider_focus_blur_listener", 1), e && !i ? jQuery(window).on("focusin", function () {
				restartOnFocus()
			}).on("focusout", function () {
				lastStatBlur()
			}) : window.addEventListener ? (window.addEventListener("focus", function (e) {
				restartOnFocus()
			}, {
				capture: !1,
				passive: !0
			}), window.addEventListener("blur", function (e) {
				lastStatBlur()
			}, {
				capture: !1,
				passive: !0
			})) : (window.attachEvent("focus", function (e) {
				restartOnFocus()
			}), window.attachEvent("blur", function (e) {
				lastStatBlur()
			})))
		},
		getUrlVars = function (e) {
			for (var i, t = [], a = window.location.href.slice(window.location.href.indexOf(e) + 1).split("_"), n = 0; n < a.length; n++) a[n] = a[n].replace("%3D", "="), i = a[n].split("="), t.push(i[0]), t[i[0]] = i[1];
			return t
		}
}(jQuery);;
jQuery(function ($) {
	function get_cart_option() {
		return LP_WooCommerce_Payment.woocommerce_cart_option;
	}
	$('form.purchase-course').submit(function (event) {
		event.preventDefault();
		var $form = $(this),
			$button = $('button.purchase-button, button.button-add-to-cart , button.button-purchase-course', this),
			$view_cart = $('.view-cart-button', this),
			$clicked = $form.find('input:focus, button:focus'),
			addToCart = $clicked.hasClass('button-add-to-cart'),
			errorHandler = function () {
				$button.removeClass('loading');
				$('body, html').css('overflow', 'visible');
			};
		$button.removeClass('added').addClass('loading');
		$form.find('#learn-press-wc-message, input[name="purchase-course"]').remove();
		var ajax_url = '';
		if ($form.find('input[name="course_url"]').length) {
			ajax_url = $form.find('input[name="course_url"]').val();
			var concat_sign = '&';
			if (/\?/.test(ajax_url) == false) {
				concat_sign = '?';
			}
			ajax_url += concat_sign + 'r=' + Math.random();
		} else {
			ajax_url = window.location.href.addQueryVar('r', Math.random());
		}
		$.ajax({
			url: ajax_url,
			data: $(this).serialize(),
			error: errorHandler,
			dataType: 'text',
			success: function (response) {
				response = LP.parseJSON(response);
				if (response.added_to_cart == 'yes') {
					var $form = $('form.purchase-course input[name="add-to-cart"][value="' + response.course_id + '"]').parent("form");
					if (response.message && !response.single_purchase) {
						var $message = $(response.message).addClass('woocommerce-message');
						$form.parent('.lp-course-buttons').prepend($('<div id="learn-press-wc-message"></div>').append($message));
					}
					if (response.redirect) {
						LP.reload(response.redirect);
					} else {
						$form.hide();
					}
					$('body, html').css('overflow', 'visible');
					$(document.body).trigger('wc_fragment_refresh');
				} else {
					errorHandler();
				}
			}
		});
		return false;
	});
	var x = $('#learn-press-checkout').on('learn_press_checkout_place_order', function () {
		var $form = $(this),
			chosen = $('input[type="radio"]:checked', $form);
		$form.find('input[name="woocommerce_chosen_method"]').remove();
		if (chosen.val() == 'woocommerce') {
			$form.append('<input type="hidden" name="woocommerce_chosen_method" value="' + chosen.data('method') + '"/>');
		}
	});
});
(function (e) {
	var t;
	t = function (t, n, r, i, s, o, u) {
		this.$el = e(n);
		this.gmt = o;
		this.showText = u;
		this.end = t;
		this.active = false;
		this.interval = 1e3;
		this.speed = i;
		if (jQuery.isFunction(s)) this.callBack = s;
		else this.callBack = null;
		this.localization = {
			days: "days",
			hours: "hours",
			minutes: "minutes",
			seconds: "seconds"
		};
		e.extend(this.localization, this.localization, r)
	};
	t.prototype = {
		getCounterNumbers: function () {
			var e = {
					days: {
						tens: 0,
						units: 0,
						hundreds: 0
					},
					hours: {
						tens: 0,
						units: 0
					},
					minutes: {
						tens: 0,
						units: 0
					},
					seconds: {
						tens: 0,
						units: 0
					}
				},
				t = 1e3 * 60 * 60 * 24,
				n = 1e3 * 60 * 60,
				r = 1e3 * 60,
				i = 1e3,
				s = 0;
			var o = new Date;
			var u = o.getTimezoneOffset() / 60 + this.gmt;
			var a = this.end.getTime() - o.getTime() - u * 60 * 6e4;
			if (a <= 0) return e;
			var f = Math.min(Math.floor(a / t), 999);
			s = a % t;
			e.days.hundreds = Math.floor(f / 100);
			var l = f % 100;
			e.days.tens = Math.floor(l / 10);
			e.days.units = l % 10;
			var c = Math.floor(s / n);
			s = s % n;
			e.hours.tens = Math.floor(c / 10);
			e.hours.units = c % 10;
			var h = Math.floor(s / r);
			s = s % r;
			e.minutes.tens = Math.floor(h / 10);
			e.minutes.units = h % 10;
			var p = Math.floor(s / 1e3);
			e.seconds.tens = Math.floor(p / 10);
			e.seconds.units = p % 10;
			return e
		},
		updatePart: function (t) {
			var n = this.getCounterNumbers();
			var r = e("." + t, this.$el);
			if (t == "days") {
				this.setDayHundreds(n.days.hundreds > 0);
				if (r.find(".number.hundreds.show").html() != n[t].hundreds) {
					var i = e(".n1.hundreds", r);
					var s = e(".n2.hundreds", r);
					this.scrollNumber(i, s, n[t].hundreds)
				}
			}
			if (r.find(".number.tens.show").html() != n[t].tens) {
				var i = e(".n1.tens", r);
				var s = e(".n2.tens", r);
				this.scrollNumber(i, s, n[t].tens)
			}
			if (r.find(".number.units.show").html() != n[t].units) {
				var i = e(".n1.units", r);
				var s = e(".n2.units", r);
				this.scrollNumber(i, s, n[t].units)
			}
		},
		timeOut: function () {
			var e = new Date;
			var t = e.getTimezoneOffset() / 60 + this.gmt;
			var n = this.end.getTime() - e.getTime() - t * 60 * 6e4;
			if (n <= 0) return true;
			return false
		},
		setDayHundreds: function (t) {
			if (t) e(".counter.days", this.$el).addClass("with-hundreds");
			else e(".counter.days", this.$el).removeClass("with-hundreds")
		},
		updateCounter: function () {
			this.updatePart("days");
			this.updatePart("hours");
			this.updatePart("minutes");
			this.updatePart("seconds");
			if (this.timeOut()) {
				this.active = false;
				if (this.callBack) this.callBack(this)
			}
		},
		localize: function (t) {
			if (e.isPlainObject(t)) e.extend(this.localization, this.localization, t);
			e(".days", this.$el).siblings(".counter-caption").text(this.localization.days);
			e(".hours", this.$el).siblings(".counter-caption").text(this.localization.hours);
			e(".minutes", this.$el).siblings(".counter-caption").text(this.localization.minutes);
			e(".seconds", this.$el).siblings(".counter-caption").text(this.localization.seconds)
		},
		start: function (e) {
			if (e) this.interval = e;
			var t = this.interval;
			this.active = true;
			var n = this;
			setTimeout(function () {
				n.updateCounter();
				if (n.active) n.start()
			}, t)
		},
		stop: function () {
			this.active = false
		},
		scrollNumber: function (e, t, n) {
			if (e.hasClass("show")) {
				t.removeClass("hidden-down").css("top", "-100%").text(n).stop().animate({
					top: 0
				}, this.speed, function () {
					t.addClass("show")
				});
				e.stop().animate({
					top: "100%"
				}, this.speed, function () {
					e.removeClass("show").addClass("hidden-down")
				})
			} else {
				e.removeClass("hidden-down").css("top", "-100%").text(n).stop().animate({
					top: 0
				}, this.speed, function () {
					e.addClass("show")
				});
				t.stop().animate({
					top: "100%"
				}, this.speed, function () {
					t.removeClass("show").addClass("hidden-down")
				})
			}
		}
	};
	jQuery.fn.mbComingsoon = function (n) {
		var r = {
			interval: 1e3,
			callBack: null,
			localization: {
				days: "days",
				hours: "hours",
				minutes: "minutes",
				seconds: "seconds"
			},
			speed: 500,
			gmt: 0,
			showText: 1
		};
		var i = {};
		var s = '   <div class="counter-group" id="myCounter">' + '       <div class="counter-block">' + '           <div class="counter days">' + '               <div class="number show n1 hundreds">0</div>' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 hundreds">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + '           <div class="counter-caption">days</div>' + "       </div>" + '       <div class="counter-block">' + '           <div class="counter hours">' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + '           <div class="counter-caption">hours</div>' + "       </div>" + '       <div class="counter-block">' + '           <div class="counter minutes">' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + '           <div class="counter-caption">minutes</div>' + "       </div>" + '       <div class="counter-block">' + '           <div class="counter seconds">' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + '           <div class="counter-caption">seconds</div>' + "       </div>" + "   </div>";
		var o = '   <div class="counter-group" id="myCounter">' + '       <div class="counter-block">' + '           <div class="counter days">' + '               <div class="number show n1 hundreds">0</div>' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 hundreds">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + "       </div>" + '       <div class="counter-block">' + '           <div class="counter hours">' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + "       </div>" + '       <div class="counter-block">' + '           <div class="counter minutes">' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + "       </div>" + '       <div class="counter-block">' + '           <div class="counter seconds">' + '               <div class="number show n1 tens">0</div>' + '               <div class="number show n1 units">0</div>' + '               <div class="number hidden-up n2 tens">0</div>' + '               <div class="number hidden-up n2 units">0</div>' + "           </div>" + "       </div>" + "   </div>";
		return this.each(function () {
			var u = e(this);
			var a = u.data("mbComingsoon");
			if (!a) {
				if (n instanceof Date) i.expiryDate = n;
				else if (e.isPlainObject(n)) e.extend(i, r, n);
				else if (typeof n == "string") i.expiryDate = new Date(n);
				if (!i.expiryDate) throw new Error("Expiry date is required!");
				a = new t(i.expiryDate, u, i.localization, i.speed, i.callBack, i.gmt, i.showText);
				if (i.showText) {
					u.html(s)
				} else {
					u.html(o)
				}
				a.localize();
				a.start()
			} else if (n == "start") a.start();
			else if (n == "stop") a.stop();
			else if (e.isPlainObject(n)) {
				if (n.expiryDate instanceof Date) a.end = n.expiryDate;
				if (e.isNumeric(n.interval)) a.interval = n.interval;
				if (e.isNumeric(n.gmt)) a.gmt = n.gmt;
				if (e.isNumeric(n.showText)) a.showText = n.showText;
				if (e.isFunction(n.callBack)) a.callBack = n.callBack;
				if (e.isPlainObject(n.localization)) this.localize(n.localization)
			}
		})
	}
})(jQuery);
(function ($) {
	$.fn.lp_course_countdown = function () {
		var countdowns = this;
		for (var i = 0; i < countdowns.length; i++) {
			var _countdown = $(countdowns[i]);
			var speed = _countdown.attr('data-speed');
			var remain = parseInt(_countdown.attr('data-timestamp-remain')) * 1000;
			var gmt = _countdown.attr('data-gmt');
			var time = _countdown.attr('data-time');
			var showtext = _countdown.attr('data-showtext');
			var current_time = new Date();
			var expiryTime = current_time.getTime() + remain;
			var expiryDate = new Date(expiryTime);
			var gmt = -expiryDate.getTimezoneOffset() / 60;
			var options = {
				expiryDate: expiryDate,
				speed: speed ? speed : 500,
				gmt: parseFloat(gmt),
				showText: (showtext == 'yes') ? 1 : 0,
				localization: {
					days: lp_coming_soon_translation.days,
					hours: lp_coming_soon_translation.hours,
					minutes: lp_coming_soon_translation.minutes,
					seconds: lp_coming_soon_translation.seconds
				}
			};
			_countdown.mbComingsoon(options);
		}
	};
	$(document).ready(function () {
		$('.learnpress-course-coming-soon').lp_course_countdown();
	});
})(jQuery);
if (!Object.prototype.watchChange) {
	var isFunction = function (fn) {
		return fn && {}.toString.call(fn) === '[object Function]';
	};
	Object.defineProperty(Object.prototype, 'watchChange', {
		enumerable: false,
		configurable: true,
		writable: false,
		value: function (prop, handler) {
			var obj = this;

			function x(prop, handler) {
				var oldval = obj[prop],
					newval = oldval,
					getter = function () {
						return newval;
					},
					setter = function (val) {
						return newval = handler.call(obj, prop, oldval, val);
					};
				if (delete obj[prop]) {
					Object.defineProperty(obj, prop, {
						get: getter,
						set: setter,
						enumerable: true,
						configurable: true
					});
				}
			}
			if (isFunction(prop)) {
				for (var k in this) {
					new x(k, prop);
				}
			} else {
				new x(prop, handler)
			}
		}
	});
}
if (!Object.prototype.unwatchChange) {
	Object.defineProperty(Object.prototype, 'unwatchChange', {
		enumerable: false,
		configurable: true,
		writable: false,
		value: function (prop) {
			var val = this[prop];
			delete this[prop];
			this[prop] = val;
		}
	});
};
(function ($) {
	$.alerts = {
		verticalOffset: -75,
		horizontalOffset: 0,
		repositionOnResize: true,
		overlayOpacity: .01,
		overlayColor: '#FFF',
		draggable: true,
		okButton: '&nbsp;OK&nbsp;',
		cancelButton: '&nbsp;Cancel&nbsp;',
		dialogClass: null,
		alert: function (message, title, callback) {
			if (title == null) title = 'Alert';
			$.alerts._show(title, message, null, 'alert', function (result) {
				if (callback) callback(result);
			});
		},
		confirm: function (message, title, callback) {
			if (title == null) title = 'Confirm';
			$.alerts._show(title, message, null, 'confirm', function (result) {
				if (callback) callback(result);
			});
		},
		prompt: function (message, value, title, callback) {
			if (title == null) title = 'Prompt';
			$.alerts._show(title, message, value, 'prompt', function (result) {
				if (callback) callback(result);
			});
		},
		_show: function (title, msg, value, type, callback) {
			$.alerts._hide();
			$.alerts._overlay('show');
			$("BODY").append('<div id="popup_container">' + '<h1 id="popup_title"></h1>' + '<div id="popup_content">' + '<div id="popup_message"></div>' + '</div>' + '</div>');
			if ($.alerts.dialogClass) $("#popup_container").addClass($.alerts.dialogClass);
			var pos = ($.browser.msie && parseInt($.browser.version) <= 6) ? 'absolute' : 'fixed';
			$("#popup_container").css({
				position: pos,
				zIndex: 99999,
				padding: 0,
				margin: 0
			});
			$("#popup_title").text(title);
			$("#popup_content").addClass(type);
			$("#popup_message").text(msg);
			$("#popup_message").html($("#popup_message").text().replace(/\n/g, '<br />'));
			$("#popup_container").css({
				minWidth: $("#popup_container").outerWidth(),
				maxWidth: $("#popup_container").outerWidth()
			});
			$.alerts._reposition();
			$.alerts._maintainPosition(true);
			switch (type) {
				case 'alert':
					$("#popup_message").after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /></div>');
					$("#popup_ok").click(function () {
						$.alerts._hide();
						callback(true);
					});
					$("#popup_ok").focus().keypress(function (e) {
						if (e.keyCode == 13 || e.keyCode == 27) $("#popup_ok").trigger('click');
					});
					break;
				case 'confirm':
					$("#popup_message").after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>');
					$("#popup_ok").click(function () {
						$.alerts._hide();
						if (callback) callback(true);
					});
					$("#popup_cancel").click(function () {
						$.alerts._hide();
						if (callback) callback(false);
					});
					$("#popup_ok").focus();
					$("#popup_ok, #popup_cancel").keypress(function (e) {
						if (e.keyCode == 13) $("#popup_ok").trigger('click');
						if (e.keyCode == 27) $("#popup_cancel").trigger('click');
					});
					break;
				case 'prompt':
					$("#popup_message").append('<br /><input type="text" size="30" id="popup_prompt" />').after('<div id="popup_panel"><input type="button" value="' + $.alerts.okButton + '" id="popup_ok" /> <input type="button" value="' + $.alerts.cancelButton + '" id="popup_cancel" /></div>');
					$("#popup_prompt").width($("#popup_message").width());
					$("#popup_ok").click(function () {
						var val = $("#popup_prompt").val();
						$.alerts._hide();
						if (callback) callback(val);
					});
					$("#popup_cancel").click(function () {
						$.alerts._hide();
						if (callback) callback(null);
					});
					$("#popup_prompt, #popup_ok, #popup_cancel").keypress(function (e) {
						if (e.keyCode == 13) $("#popup_ok").trigger('click');
						if (e.keyCode == 27) $("#popup_cancel").trigger('click');
					});
					if (value) $("#popup_prompt").val(value);
					$("#popup_prompt").focus().select();
					break;
			}
			if ($.alerts.draggable) {
				try {
					$("#popup_container").draggable({
						handle: $("#popup_title")
					});
					$("#popup_title").css({
						cursor: 'move'
					});
				} catch (e) {}
			}
		},
		_hide: function () {
			$("#popup_container").remove();
			$.alerts._overlay('hide');
			$.alerts._maintainPosition(false);
		},
		_overlay: function (status) {
			switch (status) {
				case 'show':
					$.alerts._overlay('hide');
					$("BODY").append('<div id="popup_overlay"></div>');
					$("#popup_overlay").css({
						position: 'absolute',
						zIndex: 99998,
						top: '0px',
						left: '0px',
						width: '100%',
						height: $(document).height(),
						background: $.alerts.overlayColor,
						opacity: $.alerts.overlayOpacity
					});
					break;
				case 'hide':
					$("#popup_overlay").remove();
					break;
			}
		},
		_reposition: function () {
			var top = (($(window).height() / 2) - ($("#popup_container").outerHeight() / 2)) + $.alerts.verticalOffset;
			var left = (($(window).width() / 2) - ($("#popup_container").outerWidth() / 2)) + $.alerts.horizontalOffset;
			if (top < 0) top = 0;
			if (left < 0) left = 0;
			if ($.browser.msie && parseInt($.browser.version) <= 6) top = top + $(window).scrollTop();
			$("#popup_container").css({
				top: top + 'px',
				left: left + 'px'
			});
			$("#popup_overlay").height($(document).height());
		},
		_maintainPosition: function (status) {
			if ($.alerts.repositionOnResize) {
				switch (status) {
					case true:
						$(window).bind('resize', $.alerts._reposition);
						break;
					case false:
						$(window).unbind('resize', $.alerts._reposition);
						break;
				}
			}
		}
	}
	jAlert = function (message, title, callback) {
		$.alerts.alert(message, title, callback);
	}
	jConfirm = function (message, title, callback) {
		$.alerts.confirm(message, title, callback);
	};
	jPrompt = function (message, value, title, callback) {
		$.alerts.prompt(message, value, title, callback);
	};
})(jQuery);;
(function ($) {
	$.circleBar = function (el, options) {
		this.options = $.extend({
			value: 0
		}, options || {});
		var that = this,
			$bg = $(el),
			$bg50 = $(el).find('.before'),
			$bg100 = $(el).find('.after'),
			bgColor = '#DDD',
			activeColor = '#FF0000';

		function draw() {
			var deg = that.options.value * 360 / 100;
			$bg.removeClass('bg50 bg100')
			if (that.options.value <= 50) {
				$bg.addClass('bg50');
				$bg50.css('transform', 'rotate(' + (-135 + deg) + 'deg)');
			} else {
				$bg.addClass('bg100');
				$bg100.css('transform', 'rotate(' + (-135 + ((that.options.value - 50) * 180 / 50)) + 'deg)');
			}
		}
		draw();
		this.value = function (val) {
			if (val) {
				that.options.value = val;
				draw();
				return $bg;
			}
			return that.options.value;
		}
	}
	$.fn.circleBar = function (options, val) {
		if (typeof options === 'string') {
			var $circleBar = $(this).data('circleBar');
			if (!$circleBar) {
				return null;
			}
			if ($circleBar[options]) {
				return $circleBar[options].apply($circleBar, [val]);
			}
		}
		return $.each(this, function () {
			var $circleBar = $(this).data('circleBar');
			if (!$circleBar) {
				$circleBar = new $.circleBar(this, options);
				$(this).data('circleBar', $circleBar);
			}
		})
	}
	$(document).ready(function () {
		var i = 0;
		var $c = $('.quiz-result-overall').circleBar({
			value: 45
		});
		var t = setInterval(function () {
			$c.circleBar('value', i++);
			if (i > 100) {
				clearInterval(t);
			}
		}, 40)
	})
})(jQuery);

function getUserSetting(a, b) {
	var c = getAllUserSettings();
	return c.hasOwnProperty(a) ? c[a] : "undefined" != typeof b ? b : ""
}

function setUserSetting(a, b, c) {
	if ("object" != typeof userSettings) return !1;
	var d = userSettings.uid,
		e = wpCookies.getHash("wp-settings-" + d),
		f = userSettings.url,
		g = !!userSettings.secure;
	return a = a.toString().replace(/[^A-Za-z0-9_-]/g, ""), b = "number" == typeof b ? parseInt(b, 10) : b.toString().replace(/[^A-Za-z0-9_-]/g, ""), e = e || {}, c ? delete e[a] : e[a] = b, wpCookies.setHash("wp-settings-" + d, e, 31536e3, f, "", g), wpCookies.set("wp-settings-time-" + d, userSettings.time, 31536e3, f, "", g), a
}

function deleteUserSetting(a) {
	return setUserSetting(a, "", 1)
}

function getAllUserSettings() {
	return "object" != typeof userSettings ? {} : wpCookies.getHash("wp-settings-" + userSettings.uid) || {}
}
var wpCookies = {
	each: function (a, b, c) {
		var d, e;
		if (!a) return 0;
		if (c = c || a, "undefined" != typeof a.length) {
			for (d = 0, e = a.length; d < e; d++)
				if (b.call(c, a[d], d, a) === !1) return 0
		} else
			for (d in a)
				if (a.hasOwnProperty(d) && b.call(c, a[d], d, a) === !1) return 0;
		return 1
	},
	getHash: function (a) {
		var b, c = this.get(a);
		return c && this.each(c.split("&"), function (a) {
			a = a.split("="), b = b || {}, b[a[0]] = a[1]
		}), b
	},
	setHash: function (a, b, c, d, e, f) {
		var g = "";
		this.each(b, function (a, b) {
			g += (g ? "&" : "") + b + "=" + a
		}), this.set(a, g, c, d, e, f)
	},
	get: function (a) {
		var b, c, d = document.cookie,
			e = a + "=";
		if (d) {
			if (c = d.indexOf("; " + e), c === -1) {
				if (c = d.indexOf(e), 0 !== c) return null
			} else c += 2;
			return b = d.indexOf(";", c), b === -1 && (b = d.length), decodeURIComponent(d.substring(c + e.length, b))
		}
	},
	set: function (a, b, c, d, e, f) {
		var g = new Date;
		"object" == typeof c && c.toGMTString ? c = c.toGMTString() : parseInt(c, 10) ? (g.setTime(g.getTime() + 1e3 * parseInt(c, 10)), c = g.toGMTString()) : c = "", document.cookie = a + "=" + encodeURIComponent(b) + (c ? "; expires=" + c : "") + (d ? "; path=" + d : "") + (e ? "; domain=" + e : "") + (f ? "; secure" : "")
	},
	remove: function (a, b, c, d) {
		this.set(a, "", -1e3, b, c, d)
	}
};
if (typeof window.LP == 'undefined') {
	window.LP = window.LearnPress = {};
}
(function ($) {
	window.LP.Event_Callback = function (self) {
		var callbacks = {};
		this.on = function (event, callback) {
			var namespaces = event.split('.'),
				namespace = '';
			if (namespaces.length > 1) {
				event = namespaces[0];
				namespace = namespaces[1];
			}
			if (!callbacks[event]) {
				callbacks[event] = [
					[], {}
				];
			}
			if (namespace) {
				if (!callbacks[event][1][namespace]) {
					callbacks[event][1][namespace] = [];
				}
				callbacks[event][1][namespace].push(callback);
			} else {
				callbacks[event][0].push(callback);
			}
			return self;
		};
		this.off = function (event, callback) {
			var namespaces = event.split('.'),
				namespace = '';
			if (namespaces.length > 1) {
				event = namespaces[0];
				namespace = namespaces[1];
			}
			if (!callbacks[event]) {
				return self;
			}
			var at = -1;
			if (!namespace) {
				if ($.isFunction(callback)) {
					at = callbacks[event][0].indexOf(callback);
					if (at < 0) {
						return self;
					}
					callbacks[event][0].splice(at, 1);
				} else {
					callbacks[event][0] = [];
				}
			} else {
				if (!callbacks[event][1][namespace]) {
					return self;
				}
				if ($.isFunction(callback)) {
					at = callbacks[event][1][namespace].indexOf(callback);
					if (at < 0) {
						return self;
					}
					callbacks[event][1][namespace].splice(at, 1);
				} else {
					callbacks[event][1][namespace] = [];
				}
			}
			return self;
		};
		this.callEvent = function (event, callbackArgs) {
			if (!callbacks[event]) {
				return;
			}
			if (callbacks[event][0]) {
				for (var i = 0; i < callbacks[event][0].length; i++) {
					$.isFunction(callbacks[event][0][i]) && callbacks[event][i][0].apply(self, callbackArgs);
				}
			}
			if (callbacks[event][1]) {
				for (var i in callbacks[event][1]) {
					for (var j = 0; j < callbacks[event][1][i].length; j++) {
						$.isFunction(callbacks[event][1][i][j]) && callbacks[event][1][i][j].apply(self, callbackArgs);
					}
				}
			}
		}
	};
	$.fn.serializeJSON = function (path) {
		var isInput = $(this).is('input') || $(this).is('select') || $(this).is('textarea');
		var unIndexed = isInput ? $(this).serializeArray() : $(this).find('input, select, textarea').serializeArray(),
			indexed = {},
			validate = /(\[([a-zA-Z0-9_-]+)?\]?)/g,
			arrayKeys = {},
			end = false;
		$.each(unIndexed, function () {
			var that = this,
				match = this.name.match(/^([0-9a-zA-Z_-]+)/);
			if (!match) {
				return;
			}
			var keys = this.name.match(validate),
				objPath = "indexed['" + match[0] + "']";
			if (keys) {
				if (typeof indexed[match[0]] != 'object') {
					indexed[match[0]] = {};
				}
				$.each(keys, function (i, prop) {
					prop = prop.replace(/\]|\[/g, '');
					var rawPath = objPath.replace(/'|\[|\]/g, ''),
						objExp = '',
						preObjPath = objPath;
					if (prop == '') {
						if (arrayKeys[rawPath] == undefined) {
							arrayKeys[rawPath] = 0;
						} else {
							arrayKeys[rawPath]++;
						}
						objPath += "['" + arrayKeys[rawPath] + "']";
					} else {
						if (!isNaN(prop)) {
							arrayKeys[rawPath] = prop;
						}
						objPath += "['" + prop + "']";
					}
					try {
						if (i == keys.length - 1) {
							objExp = objPath + "=that.value;";
							end = true;
						} else {
							objExp = objPath + "={}";
							end = false;
						}
						var evalString = "" + "if( typeof " + objPath + " == 'undefined'){" + objExp + ";" + "}else{" + "if(end){" + "if(typeof " + preObjPath + "!='object'){" + preObjPath + "={};}" +
							objExp + "}" + "}";
						eval(evalString);
					} catch (e) {
						console.log('Error:' + e + "\n" + objExp);
					}
				})
			} else {
				indexed[match[0]] = this.value;
			}
		});
		if (path) {
			path = "['" + path.replace('.', "']['") + "']";
			var c = 'try{indexed = indexed' + path + '}catch(ex){console.log(c, ex);}';
			eval(c);
		}
		return indexed;
	};
	$.fn.LP_Tooltip = function (options) {
		options = $.extend({}, {
			offset: [0, 0]
		}, options || {});
		return $.each(this, function () {
			var $el = $(this),
				content = $el.data('content');
			if (!content || ($el.data('LP_Tooltip') !== undefined)) {
				return;
			}
			console.log(content);
			var $tooltip = null;
			$el.hover(function (e) {
				$tooltip = $('<div class="learn-press-tooltip-bubble"/>').html(content).appendTo($('body')).hide();
				var position = $el.offset();
				if ($.isArray(options.offset)) {
					var top = options.offset[1],
						left = options.offset[0];
					if ($.isNumeric(left)) {
						position.left += left;
					} else {}
					if ($.isNumeric(top)) {
						position.top += top;
					} else {}
				}
				$tooltip.css({
					top: position.top,
					left: position.left
				});
				$tooltip.fadeIn();
			}, function () {
				$tooltip && $tooltip.remove();
			});
			$el.data('tooltip', true);
		});
	};
	$.fn.hasEvent = function (name) {
		var events = $(this).data('events');
		if (typeof events.LP == 'undefined') {
			return false;
		}
		for (i = 0; i < events.LP.length; i++) {
			if (events.LP[i].namespace == name) {
				return true;
			}
		}
		return false;
	};
	$.fn.dataToJSON = function () {
		var json = {};
		$.each(this[0].attributes, function () {
			var m = this.name.match(/^data-(.*)/);
			if (m) {
				json[m[1]] = this.value;
			}
		});
		return json;
	};
	String.prototype.getQueryVar = function (name) {
		name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
		var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
			results = regex.exec(this);
		return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
	};
	String.prototype.addQueryVar = function (name, value) {
		var url = this,
			m = url.split('#');
		url = m[0];
		if (name.match(/\[/)) {
			url += url.match(/\?/) ? '&' : '?';
			url += name + '=' + value;
		} else {
			if ((url.indexOf('&' + name + '=') != -1) || (url.indexOf('?' + name + '=') != -1)) {
				url = url.replace(new RegExp(name + "=([^&#]*)", 'g'), name + '=' + value);
			} else {
				url += url.match(/\?/) ? '&' : '?';
				url += name + '=' + value;
			}
		}
		return url + (m[1] ? '#' + m[1] : '');
	};
	String.prototype.removeQueryVar = function (name) {
		var url = this;
		var m = url.split('#');
		url = m[0];
		name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
		var regex = new RegExp("[\\?&]" + name + "([\[][^=]*)?=([^&#]*)", 'g');
		url = url.replace(regex, '');
		return url + (m[1] ? '#' + m[1] : '');
	};
	if ($.isEmptyObject("") == false) {
		$.isEmptyObject = function (a) {
			for (prop in a) {
				if (a.hasOwnProperty(prop)) {
					return false;
				}
			}
			return true;
		};
	}
	LP.MessageBox = {
		$block: null,
		$window: null,
		events: {},
		instances: [],
		instance: null,
		quickConfirm: function (elem, args) {
			var $e = $(elem);
			$('[learn-press-quick-confirm]').each(function () {
				($ins = $(this).data('quick-confirm')) && (console.log($ins), $ins.destroy());
			});
			!$e.attr('learn-press-quick-confirm') && $e.attr('learn-press-quick-confirm', 'true').data('quick-confirm', new(function (elem, args) {
				var $elem = $(elem),
					$div = $('<span class="learn-press-quick-confirm"></span>').insertAfter($elem),
					offset = $(elem).position() || {
						left: 0,
						top: 0
					},
					timerOut = null,
					timerHide = null,
					n = 3,
					hide = function () {
						$div.fadeOut('fast', function () {
							$(this).remove();
							$div.parent().css('position', '');
						});
						$elem.removeAttr('learn-press-quick-confirm').data('quick-confirm', undefined);
						stop();
					},
					stop = function () {
						timerHide && clearInterval(timerHide);
						timerOut && clearInterval(timerOut);
					},
					start = function () {
						timerOut = setInterval(function () {
							if (--n == 0) {
								hide.call($div[0]);
								$.isFunction(args.onCancel) && args.onCancel(args.data);
								stop();
							}
							$div.find('span').html(' (' + n + ')');
						}, 1000);
						timerHide = setInterval(function () {
							if (!$elem.is(':visible') || $elem.css("visibility") == 'hidden') {
								stop();
								$div.remove();
								$div.parent().css('position', '');
								$.isFunction(args.onCancel) && args.onCancel(args.data);
							}
						}, 350);
					};
				args = $.extend({
					message: '',
					data: null,
					onOk: null,
					onCancel: null,
					offset: {
						top: 0,
						left: 0
					}
				}, args || {});
				$div.html(args.message || $elem.attr('data-confirm-remove') || 'Are you sure?').append('<span> (' + n + ')</span>').css({});
				$div.click(function () {
					$.isFunction(args.onOk) && args.onOk(args.data);
					hide();
				}).hover(function () {
					stop();
				}, function () {
					start();
				});
				$div.css({
					left: ((offset.left + $elem.outerWidth()) - $div.outerWidth()) + args.offset.left,
					top: offset.top + $elem.outerHeight() + args.offset.top + 5
				}).hide().fadeIn('fast');
				start();
				this.destroy = function () {
					$div.remove();
					$elem.removeAttr('learn-press-quick-confirm').data('quick-confirm', undefined);
					stop();
				};
			})(elem, args));
		},
		show: function (message, args) {
			$.proxy(function () {
				args = $.extend({
					title: '',
					buttons: '',
					events: false,
					autohide: false,
					message: message,
					data: false,
					id: LP.uniqueId(),
					onHide: null
				}, args || {});
				this.instances.push(args);
				this.instance = args;
				var $doc = $(document),
					$body = $(document.body);
				if (!this.$block) {
					this.$block = $('<div id="learn-press-message-box-block"></div>').appendTo($body);
				}
				if (!this.$window) {
					this.$window = $('<div id="learn-press-message-box-window"><div id="message-box-wrap"></div> </div>').insertAfter(this.$block);
					this.$window.click(function () {});
				}
				this._createWindow(message, args.title, args.buttons);
				this.$block.show();
				this.$window.show().attr('instance', args.id);
				$(window).bind('resize.message-box', $.proxy(this.update, this)).bind('scroll.message-box', $.proxy(this.update, this));
				this.update(true);
				if (args.autohide) {
					setTimeout(function () {
						LP.MessageBox.hide();
						$.isFunction(args.onHide) && args.onHide.call(LP.MessageBox, args);
					}, args.autohide);
				}
			}, this)();
		},
		blockUI: function (message) {
			message = (message !== false ? (message ? message : 'Wait a moment') : '') + '<div class="message-box-animation"></div>';
			this.show(message);
		},
		hide: function (delay, instance) {
			if (instance) {
				this._removeInstance(instance.id);
			} else if (this.instance) {
				this._removeInstance(this.instance.id);
			}
			if (this.instances.length === 0) {
				if (this.$block) {
					this.$block.hide();
				}
				if (this.$window) {
					this.$window.hide();
				}
				$(window).unbind('resize.message-box', this.update).unbind('scroll.message-box', this.update);
			} else {
				if (this.instance) {
					this._createWindow(this.instance.message, this.instance.title, this.instance.buttons);
				}
			}
		},
		update: function (force) {
			var that = this,
				$wrap = this.$window.find('#message-box-wrap'),
				timer = $wrap.data('timer'),
				_update = function () {
					LP.Hook.doAction('learn_press_message_box_before_resize', that);
					var $content = $wrap.find('.message-box-content').css("height", "").css('overflow', 'hidden'),
						width = $wrap.outerWidth(),
						height = $wrap.outerHeight(),
						contentHeight = $content.height(),
						windowHeight = $(window).height(),
						top = $wrap.offset().top;
					if (contentHeight > windowHeight - 50) {
						$content.css({
							height: windowHeight - 25
						});
						height = $wrap.outerHeight();
					} else {
						$content.css("height", "").css('overflow', '');
					}
					$wrap.css({
						marginTop: ($(window).height() - height) / 2
					});
					LP.Hook.doAction('learn_press_message_box_resize', height, that);
				};
			if (force) _update();
			timer && clearTimeout(timer);
			timer = setTimeout(_update, 250);
		},
		_removeInstance: function (id) {
			for (var i = 0; i < this.instances.length; i++) {
				if (this.instances[i].id === id) {
					this.instances.splice(i, 1);
					var len = this.instances.length;
					if (len) {
						this.instance = this.instances[len - 1];
						this.$window.attr('instance', this.instance.id);
					} else {
						this.instance = false;
						this.$window.removeAttr('instance');
					}
					break;
				}
			}
		},
		_getInstance: function (id) {
			for (var i = 0; i < this.instances.length; i++) {
				if (this.instances[i].id === id) {
					return this.instances[i];
				}
			}
		},
		_createWindow: function (message, title, buttons) {
			var $wrap = this.$window.find('#message-box-wrap').html('');
			if (title) {
				$wrap.append('<h3 class="message-box-title">' + title + '</h3>');
			}
			$wrap.append($('<div class="message-box-content"></div>').html(message));
			if (buttons) {
				var $buttons = $('<div class="message-box-buttons"></div>');
				switch (buttons) {
					case 'yesNo':
						$buttons.append(this._createButton(LP_Settings.localize.button_yes, 'yes'));
						$buttons.append(this._createButton(LP_Settings.localize.button_no, 'no'));
						break;
					case 'okCancel':
						$buttons.append(this._createButton(LP_Settings.localize.button_ok, 'ok'));
						$buttons.append(this._createButton(LP_Settings.localize.button_cancel, 'cancel'));
						break;
					default:
						$buttons.append(this._createButton(LP_Settings.localize.button_ok, 'ok'));
				}
				$wrap.append($buttons);
			}
		},
		_createButton: function (title, type) {
			var $button = $('<button type="button" class="button message-box-button message-box-button-' + type + '">' + title + '</button>'),
				callback = 'on' + (type.substr(0, 1).toUpperCase() + type.substr(1));
			$button.data('callback', callback).click(function () {
				var instance = $(this).data('instance'),
					callback = instance.events[$(this).data('callback')];
				if ($.type(callback) === 'function') {
					if (callback.apply(LP.MessageBox, [instance]) === false) {} else {
						LP.MessageBox.hide(null, instance);
					}
				} else {
					LP.MessageBox.hide(null, instance);
				}
			}).data('instance', this.instance);
			return $button;
		}
	};
	LP.Hook = {
		hooks: {
			action: {},
			filter: {}
		},
		addAction: function (action, callable, priority, tag) {
			this.addHook('action', action, callable, priority, tag);
			return this;
		},
		addFilter: function (action, callable, priority, tag) {
			this.addHook('filter', action, callable, priority, tag);
			return this;
		},
		doAction: function (action) {
			this.doHook('action', action, arguments);
			return this;
		},
		applyFilters: function (action) {
			return this.doHook('filter', action, arguments);
		},
		removeAction: function (action, tag) {
			this.removeHook('action', action, tag);
			return this;
		},
		removeFilter: function (action, priority, tag) {
			this.removeHook('filter', action, priority, tag);
			return this;
		},
		addHook: function (hookType, action, callable, priority, tag) {
			if (undefined === this.hooks[hookType][action]) {
				this.hooks[hookType][action] = [];
			}
			var hooks = this.hooks[hookType][action];
			if (undefined === tag) {
				tag = action + '_' + hooks.length;
			}
			this.hooks[hookType][action].push({
				tag: tag,
				callable: callable,
				priority: priority
			});
			return this;
		},
		doHook: function (hookType, action, args) {
			args = Array.prototype.slice.call(args, 1);
			if (undefined !== this.hooks[hookType][action]) {
				var hooks = this.hooks[hookType][action],
					hook;
				hooks.sort(function (a, b) {
					return a["priority"] - b["priority"];
				});
				for (var i = 0; i < hooks.length; i++) {
					hook = hooks[i].callable;
					if (typeof hook !== 'function')
						hook = window[hook];
					if ('action' === hookType) {
						hook.apply(null, args);
					} else {
						args[0] = hook.apply(null, args);
					}
				}
			}
			if ('filter' === hookType) {
				return args[0];
			}
			return this;
		},
		removeHook: function (hookType, action, priority, tag) {
			if (undefined !== this.hooks[hookType][action]) {
				var hooks = this.hooks[hookType][action];
				for (var i = hooks.length - 1; i >= 0; i--) {
					if ((undefined === tag || tag === hooks[i].tag) && (undefined === priority || priority === hooks[i].priority)) {
						hooks.splice(i, 1);
					}
				}
			}
			return this;
		}
	};
	LP = $.extend({
		setUrl: function (url, ember, title) {
			if (url) {
				history.pushState({}, title, url);
				LP.Hook.doAction('learn_press_set_location_url', url);
			}
		},
		toggleGroupSection: function (el, target) {
			var $el = $(el),
				isHide = $el.hasClass('hide-if-js');
			if (isHide) {
				$el.hide().removeClass('hide-if-js');
			}
			$el.removeClass('hide-if-js').slideToggle(function () {
				var $this = $(this);
				if ($this.is(':visible')) {
					$(target).addClass('toggle-on').removeClass('toggle-off');
				} else {
					$(target).addClass('toggle-off').removeClass('toggle-on');
				}
			});
		},
		overflow: function (el, v) {
			var $el = $(el),
				overflow = $el.css('overflow');
			if (v) {
				$el.css('overflow', v).data('overflow', overflow);
			} else {
				$el.css('overflow', $el.data('overflow'));
			}
		},
		getUrl: function () {
			return window.location.href;
		},
		addQueryVar: function (name, value, url) {
			return (url === undefined ? window.location.href : url).addQueryVar(name, value);
		},
		removeQueryVar: function (name, url) {
			return (url === undefined ? window.location.href : url).removeQueryVar(name);
		},
		reload: function (url) {
			if (!url) {
				url = window.location.href;
			}
			window.location.href = url;
		},
		parseResponse: function (response, type) {
			var m = response.match(/<-- LP_AJAX_START -->(.*)<-- LP_AJAX_END -->/);
			if (m) {
				response = m[1];
			}
			return (type || "json") === "json" ? this.parseJSON(response) : response;
		},
		parseJSON: function (data) {
			var m = (data + '').match(/<-- LP_AJAX_START -->(.*)<-- LP_AJAX_END -->/);
			try {
				if (m) {
					data = $.parseJSON(m[1]);
				} else {
					data = $.parseJSON(data);
				}
			} catch (e) {
				data = {};
			}
			return data;
		},
		ajax: function (args) {
			var type = args.type || 'post',
				dataType = args.dataType || 'json',
				data = args.action ? $.extend(args.data, {
					'lp-ajax': args.action
				}) : args.data,
				beforeSend = args.beforeSend || function () {},
				url = args.url || window.location.href;
			$.ajax({
				data: data,
				url: url,
				type: type,
				dataType: 'html',
				beforeSend: beforeSend.apply(null, args),
				success: function (raw) {
					var response = LP.parseResponse(raw, dataType);
					$.isFunction(args.success) && args.success(response, raw);
				},
				error: function () {
					$.isFunction(args.error) && args.error.apply(null, LP.funcArgs2Array());
				}
			});
		},
		doAjax: function (args) {
			var type = args.type || 'post',
				dataType = args.dataType || 'json',
				action = ((args.prefix === undefined) || 'learnpress_') + args.action,
				data = args.action ? $.extend(args.data, {
					action: action
				}) : args.data;
			$.ajax({
				data: data,
				url: (args.url || window.location.href),
				type: type,
				dataType: 'html',
				success: function (raw) {
					var response = LP.parseResponse(raw, dataType);
					$.isFunction(args.success) && args.success(response, raw);
				},
				error: function () {
					$.isFunction(args.error) && args.error.apply(null, LP.funcArgs2Array());
				}
			});
		},
		funcArgs2Array: function (args) {
			var arr = [];
			for (var i = 0; i < args.length; i++) {
				arr.push(args[i]);
			}
			return arr;
		},
		addFilter: function (action, callback) {
			var $doc = $(document),
				event = 'LP.' + action;
			$doc.on(event, callback);
			LP.log($doc.data('events'));
			return this;
		},
		applyFilters: function () {
			var $doc = $(document),
				action = arguments[0],
				args = this.funcArgs2Array(arguments);
			if ($doc.hasEvent(action)) {
				args[0] = 'LP.' + action;
				return $doc.triggerHandler.apply($doc, args);
			}
			return args[1];
		},
		addAction: function (action, callback) {
			return this.addFilter(action, callback);
		},
		doAction: function () {
			var $doc = $(document),
				action = arguments[0],
				args = this.funcArgs2Array(arguments);
			if ($doc.hasEvent(action)) {
				args[0] = 'LP.' + action;
				$doc.trigger.apply($doc, args);
			}
		},
		toElement: function (element, args) {
			if ($(element).length === 0) {
				return;
			}
			args = $.extend({
				delay: 300,
				duration: 'slow',
				offset: 50,
				container: null,
				callback: null,
				invisible: false
			}, args || {});
			var $container = $(args.container),
				rootTop = 0;
			if ($container.length === 0) {
				$container = $('body, html');
			}
			rootTop = $container.offset().top;
			var to = ($(element).offset().top + $container.scrollTop()) - rootTop - args.offset;

			function isElementInView(element, fullyInView) {
				var pageTop = $container.scrollTop();
				var pageBottom = pageTop + $container.height();
				var elementTop = $(element).offset().top - $container.offset().top;
				var elementBottom = elementTop + $(element).height();
				if (fullyInView === true) {
					return ((pageTop < elementTop) && (pageBottom > elementBottom));
				} else {
					return ((elementTop <= pageBottom) && (elementBottom >= pageTop));
				}
			}
			if (args.invisible && isElementInView(element, true)) {
				return;
			}
			$container.fadeIn(10).delay(args.delay).animate({
				scrollTop: to
			}, args.duration, args.callback);
		},
		uniqueId: function (prefix, more_entropy) {
			if (typeof prefix === 'undefined') {
				prefix = '';
			}
			var retId;
			var formatSeed = function (seed, reqWidth) {
				seed = parseInt(seed, 10).toString(16);
				if (reqWidth < seed.length) {
					return seed.slice(seed.length - reqWidth);
				}
				if (reqWidth > seed.length) {
					return new Array(1 + (reqWidth - seed.length)).join('0') + seed;
				}
				return seed;
			};
			if (!this.php_js) {
				this.php_js = {};
			}
			if (!this.php_js.uniqidSeed) {
				this.php_js.uniqidSeed = Math.floor(Math.random() * 0x75bcd15);
			}
			this.php_js.uniqidSeed++;
			retId = prefix;
			retId += formatSeed(parseInt(new Date().getTime() / 1000, 10), 8);
			retId += formatSeed(this.php_js.uniqidSeed, 5);
			if (more_entropy) {
				retId += (Math.random() * 10).toFixed(8).toString();
			}
			return retId;
		},
		log: function () {
			for (var i = 0, n = arguments.length; i < n; i++) {
				console.log(arguments[i]);
			}
		},
		blockContent: function () {
			if ($('#learn-press-block-content').length === 0) {
				$(LP.template('learn-press-template-block-content', {})).appendTo($('body'));
			}
			LP.hideMainScrollbar().addClass('block-content');
			$(document).trigger('learn_press_block_content');
		},
		unblockContent: function () {
			setTimeout(function () {
				LP.showMainScrollbar().removeClass('block-content');
				$(document).trigger('learn_press_unblock_content');
			}, 350);
		},
		hideMainScrollbar: function (el) {
			if (!el) {
				el = 'html, body';
			}
			var $el = $(el);
			$el.each(function () {
				var $root = $(this),
					overflow = $root.css('overflow');
				$root.css('overflow', 'hidden').attr('overflow', overflow);
			});
			return $el;
		},
		showMainScrollbar: function (el) {
			if (!el) {
				el = 'html, body';
			}
			var $el = $(el);
			$el.each(function () {
				var $root = $(this),
					overflow = $root.attr('overflow');
				$root.css('overflow', overflow).removeAttr('overflow');
			});
			return $el;
		},
		template: typeof _ !== 'undefined' ? _.memoize(function (id, data) {
			var compiled, options = {
				evaluate: /<#([\s\S]+?)#>/g,
				interpolate: /\{\{\{([\s\S]+?)\}\}\}/g,
				escape: /\{\{([^\}]+?)\}\}(?!\})/g,
				variable: 'data'
			};
			var tmpl = function (data) {
				compiled = compiled || _.template($('#' + id).html(), null, options);
				return compiled(data);
			};
			return data ? tmpl(data) : tmpl;
		}, function (a, b) {
			return a + '-' + JSON.stringify(b);
		}) : function () {
			return '';
		},
		alert: function (localize, callback) {
			var title = '',
				message = '';
			if (typeof localize === 'string') {
				message = localize;
			} else {
				if (typeof localize['title'] !== 'undefined') {
					title = localize['title'];
				}
				if (typeof localize['message'] !== 'undefined') {
					message = localize['message'];
				}
			}
			$.alerts.alert(message, title, function (e) {
				LP._on_alert_hide();
				callback && callback(e);
			});
			this._on_alert_show();
		},
		confirm: function (localize, callback) {
			var title = '',
				message = '';
			if (typeof localize === 'string') {
				message = localize;
			} else {
				if (typeof localize['title'] !== 'undefined') {
					title = localize['title'];
				}
				if (typeof localize['message'] !== 'undefined') {
					message = localize['message'];
				}
			}
			$.alerts.confirm(message, title, function (e) {
				LP._on_alert_hide();
				callback && callback(e);
			});
			this._on_alert_show();
		},
		_on_alert_show: function () {
			var $container = $('#popup_container'),
				$placeholder = $('<span id="popup_container_placeholder" />').insertAfter($container).data('xxx', $container);
			$container.stop().css('top', '-=50').css('opacity', '0').animate({
				top: '+=50',
				opacity: 1
			}, 250);
		},
		_on_alert_hide: function () {
			var $holder = $("#popup_container_placeholder"),
				$container = $holder.data('xxx');
			if ($container) {
				$container.replaceWith($holder);
			}
			$container.appendTo($(document.body))
			$container.stop().animate({
				top: '+=50',
				opacity: 0
			}, 250, function () {
				$(this).remove();
			});
		},
		sendMessage: function (data, object, targetOrigin, transfer) {
			if ($.isPlainObject(data)) {
				data = JSON.stringify(data);
			}
			object = object || window;
			targetOrigin = targetOrigin || '*';
			object.postMessage(data, targetOrigin, transfer);
		},
		receiveMessage: function (event, b) {
			var target = event.origin || event.originalEvent.origin,
				data = event.data || event.originalEvent.data || '';
			if (typeof data === 'string' || data instanceof String) {
				if (data.indexOf('{') === 0) {
					data = LP.parseJSON(data);
				}
			}
			LP.Hook.doAction('learn_press_receive_message', data, target);
		}
	}, LP);
	$.fn.rows = function () {
		var h = $(this).height();
		var lh = $(this).css('line-height').replace("px", "");
		$(this).attr({
			height: h,
			'line-height': lh
		});
		return Math.floor(h / parseInt(lh));
	};
	$.fn.checkLines = function (p) {
		return this.each(function () {
			var $e = $(this),
				rows = $e.rows();
			p.call(this, rows);
		});
	};
	$.fn.findNext = function (selector) {
		var $selector = $(selector),
			$root = this.first(),
			index = $selector.index($root),
			$next = $selector.eq(index + 1);
		return $next.length ? $next : false;
	};
	$.fn.findPrev = function (selector) {
		var $selector = $(selector),
			$root = this.first(),
			index = $selector.index($root),
			$prev = $selector.eq(index - 1);
		return $prev.length ? $prev : false;
	};
	$.each(['progress'], function (i, property) {
		$.Tween.propHooks[property] = {
			get: function (tween) {
				return $(tween.elem).css('transform');
			},
			set: function (tween) {
				if (tween.now < 180) {
					$(this).find('.progress-circle').removeClass('gt-50');
				} else {
					$(this).find('.progress-circle').addClass('gt-50');
				}
				$(tween.elem).find('.fill').css({
					transform: 'rotate(' + tween.end + 'deg)'
				});
			}
		};
	});
	$.fn.progress = function (v) {
		return this.each(function () {
			var t = parseInt(v / 100 * 360),
				timer = null,
				$this = $(this);
			if (t < 180) {
				$this.find('.progress-circle').removeClass('gt-50');
			} else {
				$this.find('.progress-circle').addClass('gt-50');
			}
			$this.find('.fill').css({
				transform: 'rotate(' + t + 'deg)'
			});
		});
	};
	var xxx = 0;

	function QuickTip(el, options) {
		var $el = $(el),
			uniId = $el.attr('data-id') || LP.uniqueId();
		options = $.extend({
			event: 'hover',
			autoClose: true,
			single: true,
			closeInterval: 1000,
			arrowOffset: null,
			tipClass: ''
		}, options, $el.data());
		$el.attr('data-id', uniId);
		var content = $el.attr('data-content-tip') || $el.html(),
			$tip = $('<div class="learn-press-tip-floating">' + content + '</div>'),
			t = null,
			closeInterval = 0,
			useData = false,
			arrowOffset = options.arrowOffset === 'el' ? $el.outerWidth() / 2 : 8,
			$content = $('#__' + uniId);
		if ($content.length === 0) {
			$(document.body).append($('<div />').attr('id', '__' + uniId).html(content).css('display', 'none'))
		}
		content = $content.html();
		$tip.addClass(options.tipClass);
		$el.data('content-tip', content);
		if ($el.attr('data-content-tip')) {
			useData = true;
		}
		closeInterval = options.closeInterval;
		if (options.autoClose === false) {
			$tip.append('<a class="close"></a>');
			$tip.on('click', '.close', function () {
				close();
			})
		}

		function show() {
			if (t) {
				clearTimeout(t);
				return;
			}
			if (options.single) {
				$('.learn-press-tip').not($el).QuickTip('close');
			}
			$tip.appendTo(document.body);
			var pos = $el.offset();
			$tip.css({
				top: pos.top - $tip.outerHeight() - 8,
				left: pos.left - $tip.outerWidth() / 2 + arrowOffset
			});
		}

		function hide() {
			t && clearTimeout(t);
			t = setTimeout(function () {
				$tip.detach();
				t = null;
			}, closeInterval);
		}

		function close() {
			closeInterval = 0;
			hide();
			closeInterval = options.closeInterval;
		}

		function open() {
			show();
		}
		if (!useData) {
			$el.html('');
		}
		if (options.event === 'click') {
			$el.on('click', function (e) {
				e.stopPropagation();
				show();
			})
		}
		$(document).on('learn-press/close-all-quick-tip', function () {
			close();
		});
		$el.hover(function (e) {
			e.stopPropagation();
			if (options.event !== 'click') {
				show();
			}
		}, function (e) {
			e.stopPropagation();
			if (options.autoClose) {
				hide();
			}
		}).addClass('ready');
		return {
			close: close,
			open: open
		}
	}
	$.fn.QuickTip = function (options) {
		return $.each(this, function () {
			var $tip = $(this).data('quick-tip');
			if (!$tip) {
				$tip = new QuickTip(this, options);
				$(this).data('quick-tip', $tip);
			}
			if ($.type(options) === 'string') {
				$tip[options] && $tip[options].apply($tip);
			}
		})
	}

	function __initSubtabs() {
		$('.learn-press-subtabs').each(function () {
			var $tabContainer = $(this),
				$tabs = $tabContainer.find('a'),
				current = null;
			$tabs.click(function (e) {
				var $tab = $(this),
					$contentID = $tab.attr('href');
				$tab.parent().addClass('current').siblings().removeClass('current');
				current = $($contentID).addClass('current');
				current.siblings().removeClass('current');
				e.preventDefault();
			}).filter(function () {
				return $(this).attr('href') === window.location.hash;
			}).trigger('click');
			if (!current) {
				$tabs.first().trigger('click');
			}
		});
	}
	$(document).ready(function () {
		if (typeof $.alerts !== 'undefined') {
			$.alerts.overlayColor = '#000';
			$.alerts.overlayOpacity = 0.5;
			$.alerts.okButton = lpGlobalSettings.localize.button_ok;
			$.alerts.cancelButton = lpGlobalSettings.localize.button_cancel;
		}
		$('.learn-press-message.fixed').each(function () {
			var $el = $(this),
				options = $el.data();
			(function ($el, options) {
				if (options.delayIn) {
					setTimeout(function () {
						$el.show().hide().fadeIn();
					}, options.delayIn);
				}
				if (options.delayOut) {
					setTimeout(function () {
						$el.fadeOut();
					}, options.delayOut + (options.delayIn || 0));
				}
			})($el, options);
		});
		$(document).on('input', '#meta-box-tab-course_payment', function (e) {
			var _self = $(this),
				_price = $('#_lp_price'),
				_sale_price = $('#_lp_sale_price'),
				_target = $(e.target).attr('id');
			_self.find('#field-_lp_price div, #field-_lp_sale_price div').remove('.learn-press-tip-floating');
			if (parseInt(_sale_price.val()) >= parseInt(_price.val())) {
				if (_target === '_lp_price') {
					_price.parent('.rwmb-input').append('<div class="learn-press-tip-floating">' + lpAdminCourseEditorSettings.i18n.notice_price + '</div>');
				} else if (_target === '_lp_sale_price') {
					_sale_price.parent('.rwmb-input').append('<div class="learn-press-tip-floating">' + lpAdminCourseEditorSettings.i18n.notice_sale_price + '</div>');
				}
			}
		});
		$(document).on('change', '#_lp_sale_start', function (e) {
			var _sale_start_date = $(this),
				_sale_end_date = $('#_lp_sale_end'),
				_start_date = Date.parse(_sale_start_date.val()),
				_end_date = Date.parse(_sale_end_date.val()),
				_parent_start = _sale_start_date.parent('.rwmb-input'),
				_parent_end = _sale_end_date.parent('.rwmb-input');
			if (!_start_date) {
				_parent_start.append('<div class="learn-press-tip-floating">' + lpAdminCourseEditorSettings.i18n.notice_invalid_date + '</div>')
			}
			$('#field-_lp_sale_start div, #field-_lp_sale_end div').remove('.learn-press-tip-floating');
			if (_start_date > _end_date) {
				_parent_start.append('<div class="learn-press-tip-floating">' + lpAdminCourseEditorSettings.i18n.notice_sale_start_date + '</div>')
			}
		});
		$(document).on('change', '#_lp_sale_end', function (e) {
			var _sale_end_date = $(this),
				_sale_start_date = $('#_lp_sale_start'),
				_start_date = Date.parse(_sale_start_date.val()),
				_end_date = Date.parse(_sale_end_date.val()),
				_parent_start = _sale_start_date.parent('.rwmb-input'),
				_parent_end = _sale_end_date.parent('.rwmb-input');
			if (!_end_date) {
				_parent_end.append('<div class="learn-press-tip-floating">' + lpAdminCourseEditorSettings.i18n.notice_invalid_date + '</div>')
			}
			$('#field-_lp_sale_start div, #field-_lp_sale_end div').remove('.learn-press-tip-floating');
			if (_start_date > _end_date) {
				_parent_end.append('<div class="learn-press-tip-floating">' + lpAdminCourseEditorSettings.i18n.notice_sale_end_date + '</div>')
			}
		});
		$('body').on('click', '.learn-press-nav-tabs li a', function (e) {
			e.preventDefault();
			var $tab = $(this),
				url = '';
			$tab.closest('li').addClass('active').siblings().removeClass('active');
			$($tab.attr('data-tab')).addClass('active').siblings().removeClass('active');
			$(document).trigger('learn-press/nav-tabs/clicked', $tab);
		});
		setTimeout(function () {
			$('.learn-press-nav-tabs li.active:not(.default) a').trigger('click');
		}, 300);
		$('body.course-item-popup').parent().css('overflow', 'hidden');
		(function () {
			var timer = null,
				callback = function () {
					$('.auto-check-lines').checkLines(function (r) {
						if (r > 1) {
							$(this).removeClass('single-lines');
						} else {
							$(this).addClass('single-lines');
						}
						$(this).attr('rows', r);
					});
				};
			$(window).on('resize.check-lines', function () {
				if (timer) {
					timer && clearTimeout(timer);
					timer = setTimeout(callback, 300);
				} else {
					callback();
				}
			});
		})();
		$('.learn-press-tooltip, .lp-passing-conditional').LP_Tooltip({
			offset: [24, 24]
		});
		$('.learn-press-icon').LP_Tooltip({
			offset: [30, 30]
		});
		$('.learn-press-message[data-autoclose]').each(function () {
			var $el = $(this),
				delay = parseInt($el.data('autoclose'));
			if (delay) {
				setTimeout(function ($el) {
					$el.fadeOut();
				}, delay, $el);
			}
		});
		$(document).on('click', function () {
			$(document).trigger('learn-press/close-all-quick-tip')
		})
	});
	LearnPress = LP;
})(jQuery);;
(function (root, factory) {
	if (typeof define === 'function' && define.amd) {
		define(['jquery'], factory);
	} else {
		factory(root.jQuery);
	}
}(this, function ($) {
	'use strict';
	var debug = false;
	var browser = {
		data: {
			index: 0,
			name: 'scrollbar'
		},
		macosx: /mac/i.test(navigator.platform),
		mobile: /android|webos|iphone|ipad|ipod|blackberry/i.test(navigator.userAgent),
		overlay: null,
		scroll: null,
		scrolls: [],
		webkit: /webkit/i.test(navigator.userAgent) && !/edge\/\d+/i.test(navigator.userAgent)
	};
	browser.scrolls.add = function (instance) {
		this.remove(instance).push(instance);
	};
	browser.scrolls.remove = function (instance) {
		while ($.inArray(instance, this) >= 0) {
			this.splice($.inArray(instance, this), 1);
		}
		return this;
	};
	var defaults = {
		"autoScrollSize": true,
		"autoUpdate": true,
		"debug": false,
		"disableBodyScroll": false,
		"duration": 200,
		"ignoreMobile": false,
		"ignoreOverlay": false,
		"scrollStep": 30,
		"showArrows": false,
		"stepScrolling": true,
		"scrollx": null,
		"scrolly": null,
		"onDestroy": null,
		"onInit": null,
		"onScroll": null,
		"onUpdate": null
	};
	var BaseScrollbar = function (container) {
		if (!browser.scroll) {
			browser.overlay = isScrollOverlaysContent();
			browser.scroll = getBrowserScrollSize();
			updateScrollbars();
			$(window).resize(function () {
				var forceUpdate = false;
				if (browser.scroll && (browser.scroll.height || browser.scroll.width)) {
					var scroll = getBrowserScrollSize();
					if (scroll.height !== browser.scroll.height || scroll.width !== browser.scroll.width) {
						browser.scroll = scroll;
						forceUpdate = true;
					}
				}
				updateScrollbars(forceUpdate);
			});
		}
		this.container = container;
		this.namespace = '.scrollbar_' + browser.data.index++;
		this.options = $.extend({}, defaults, window.jQueryScrollbarOptions || {});
		this.scrollTo = null;
		this.scrollx = {};
		this.scrolly = {};
		container.data(browser.data.name, this);
		browser.scrolls.add(this);
	};
	BaseScrollbar.prototype = {
		destroy: function () {
			if (!this.wrapper) {
				return;
			}
			this.container.removeData(browser.data.name);
			browser.scrolls.remove(this);
			var scrollLeft = this.container.scrollLeft();
			var scrollTop = this.container.scrollTop();
			this.container.insertBefore(this.wrapper).css({
				"height": "",
				"margin": "",
				"max-height": ""
			}).removeClass('scroll-content scroll-scrollx_visible scroll-scrolly_visible').off(this.namespace).scrollLeft(scrollLeft).scrollTop(scrollTop);
			this.scrollx.scroll.removeClass('scroll-scrollx_visible').find('div').andSelf().off(this.namespace);
			this.scrolly.scroll.removeClass('scroll-scrolly_visible').find('div').andSelf().off(this.namespace);
			this.wrapper.remove();
			$(document).add('body').off(this.namespace);
			if ($.isFunction(this.options.onDestroy)) {
				this.options.onDestroy.apply(this, [this.container]);
			}
		},
		init: function (options) {
			var S = this,
				c = this.container,
				cw = this.containerWrapper || c,
				namespace = this.namespace,
				o = $.extend(this.options, options || {}),
				s = {
					x: this.scrollx,
					y: this.scrolly
				},
				w = this.wrapper;
			var initScroll = {
				"scrollLeft": c.scrollLeft(),
				"scrollTop": c.scrollTop()
			};
			if ((browser.mobile && o.ignoreMobile) || (browser.overlay && o.ignoreOverlay) || (browser.macosx && !browser.webkit)) {}
			if (!w) {
				this.wrapper = w = $('<div>').addClass('scroll-wrapper').addClass(c.attr('class')).css('position', c.css('position') == 'absolute' ? 'absolute' : 'relative').insertBefore(c).append(c);
				if (c.is('textarea')) {
					this.containerWrapper = cw = $('<div>').insertBefore(c).append(c);
					w.addClass('scroll-textarea');
				}
				cw.addClass('scroll-content').css({
					"height": "auto",
					"margin-bottom": browser.scroll.height * -1 + 'px',
					"margin-right": browser.scroll.width * -1 + 'px',
					"max-height": ""
				});
				c.on('scroll' + namespace, function (event) {
					if ($.isFunction(o.onScroll)) {
						o.onScroll.call(S, {
							"maxScroll": s.y.maxScrollOffset,
							"scroll": c.scrollTop(),
							"size": s.y.size,
							"visible": s.y.visible
						}, {
							"maxScroll": s.x.maxScrollOffset,
							"scroll": c.scrollLeft(),
							"size": s.x.size,
							"visible": s.x.visible
						});
					}
					s.x.isVisible && s.x.scroll.bar.css('left', c.scrollLeft() * s.x.kx + 'px');
					s.y.isVisible && s.y.scroll.bar.css('top', c.scrollTop() * s.y.kx + 'px');
				});
				w.on('scroll' + namespace, function () {
					w.scrollTop(0).scrollLeft(0);
				});
				if (o.disableBodyScroll) {
					var handleMouseScroll = function (event) {
						isVerticalScroll(event) ? s.y.isVisible && s.y.mousewheel(event) : s.x.isVisible && s.x.mousewheel(event);
					};
					w.on('MozMousePixelScroll' + namespace, handleMouseScroll);
					w.on('mousewheel' + namespace, handleMouseScroll);
					if (browser.mobile) {
						w.on('touchstart' + namespace, function (event) {
							var touch = event.originalEvent.touches && event.originalEvent.touches[0] || event;
							var originalTouch = {
								"pageX": touch.pageX,
								"pageY": touch.pageY
							};
							var originalScroll = {
								"left": c.scrollLeft(),
								"top": c.scrollTop()
							};
							$(document).on('touchmove' + namespace, function (event) {
								var touch = event.originalEvent.targetTouches && event.originalEvent.targetTouches[0] || event;
								c.scrollLeft(originalScroll.left + originalTouch.pageX - touch.pageX);
								c.scrollTop(originalScroll.top + originalTouch.pageY - touch.pageY);
								event.preventDefault();
							});
							$(document).on('touchend' + namespace, function () {
								$(document).off(namespace);
							});
						});
					}
				}
				if ($.isFunction(o.onInit)) {
					o.onInit.apply(this, [c]);
				}
			} else {
				cw.css({
					"height": "auto",
					"margin-bottom": browser.scroll.height * -1 + 'px',
					"margin-right": browser.scroll.width * -1 + 'px',
					"max-height": ""
				});
			}
			$.each(s, function (d, scrollx) {
				var scrollCallback = null;
				var scrollForward = 1;
				var scrollOffset = (d === 'x') ? 'scrollLeft' : 'scrollTop';
				var scrollStep = o.scrollStep;
				var scrollTo = function () {
					var currentOffset = c[scrollOffset]();
					c[scrollOffset](currentOffset + scrollStep);
					if (scrollForward == 1 && (currentOffset + scrollStep) >= scrollToValue)
						currentOffset = c[scrollOffset]();
					if (scrollForward == -1 && (currentOffset + scrollStep) <= scrollToValue)
						currentOffset = c[scrollOffset]();
					if (c[scrollOffset]() == currentOffset && scrollCallback) {
						scrollCallback();
					}
				}
				var scrollToValue = 0;
				if (!scrollx.scroll) {
					scrollx.scroll = S._getScroll(o['scroll' + d]).addClass('scroll-' + d);
					if (o.showArrows) {
						scrollx.scroll.addClass('scroll-element_arrows_visible');
					}
					scrollx.mousewheel = function (event) {
						if (!scrollx.isVisible || (d === 'x' && isVerticalScroll(event))) {
							return true;
						}
						if (d === 'y' && !isVerticalScroll(event)) {
							s.x.mousewheel(event);
							return true;
						}
						var delta = event.originalEvent.wheelDelta * -1 || event.originalEvent.detail;
						var maxScrollValue = scrollx.size - scrollx.visible - scrollx.offset;
						if ((delta > 0 && scrollToValue < maxScrollValue) || (delta < 0 && scrollToValue > 0)) {
							scrollToValue = scrollToValue + delta;
							if (scrollToValue < 0)
								scrollToValue = 0;
							if (scrollToValue > maxScrollValue)
								scrollToValue = maxScrollValue;
							S.scrollTo = S.scrollTo || {};
							S.scrollTo[scrollOffset] = scrollToValue;
							setTimeout(function () {
								if (S.scrollTo) {
									c.stop().animate(S.scrollTo, 240, 'linear', function () {
										scrollToValue = c[scrollOffset]();
									});
									S.scrollTo = null;
								}
							}, 1);
						}
						event.preventDefault();
						return false;
					};
					scrollx.scroll.on('MozMousePixelScroll' + namespace, scrollx.mousewheel).on('mousewheel' + namespace, scrollx.mousewheel).on('mouseenter' + namespace, function () {
						scrollToValue = c[scrollOffset]();
					});
					scrollx.scroll.find('.scroll-arrow, .scroll-element_track').on('mousedown' + namespace, function (event) {
						if (event.which != 1)
							return true;
						scrollForward = 1;
						var data = {
							"eventOffset": event[(d === 'x') ? 'pageX' : 'pageY'],
							"maxScrollValue": scrollx.size - scrollx.visible - scrollx.offset,
							"scrollbarOffset": scrollx.scroll.bar.offset()[(d === 'x') ? 'left' : 'top'],
							"scrollbarSize": scrollx.scroll.bar[(d === 'x') ? 'outerWidth' : 'outerHeight']()
						};
						var timeout = 0,
							timer = 0;
						if ($(this).hasClass('scroll-arrow')) {
							scrollForward = $(this).hasClass("scroll-arrow_more") ? 1 : -1;
							scrollStep = o.scrollStep * scrollForward;
							scrollToValue = scrollForward > 0 ? data.maxScrollValue : 0;
						} else {
							scrollForward = (data.eventOffset > (data.scrollbarOffset + data.scrollbarSize) ? 1 : (data.eventOffset < data.scrollbarOffset ? -1 : 0));
							scrollStep = Math.round(scrollx.visible * 0.75) * scrollForward;
							scrollToValue = (data.eventOffset - data.scrollbarOffset -
								(o.stepScrolling ? (scrollForward == 1 ? data.scrollbarSize : 0) : Math.round(data.scrollbarSize / 2)));
							scrollToValue = c[scrollOffset]() + (scrollToValue / scrollx.kx);
						}
						S.scrollTo = S.scrollTo || {};
						S.scrollTo[scrollOffset] = o.stepScrolling ? c[scrollOffset]() + scrollStep : scrollToValue;
						if (o.stepScrolling) {
							scrollCallback = function () {
								scrollToValue = c[scrollOffset]();
								clearInterval(timer);
								clearTimeout(timeout);
								timeout = 0;
								timer = 0;
							};
							timeout = setTimeout(function () {
								timer = setInterval(scrollTo, 40);
							}, o.duration + 100);
						}
						setTimeout(function () {
							if (S.scrollTo) {
								c.animate(S.scrollTo, o.duration);
								S.scrollTo = null;
							}
						}, 1);
						return S._handleMouseDown(scrollCallback, event);
					});
					scrollx.scroll.bar.on('mousedown' + namespace, function (event) {
						if (event.which != 1)
							return true;
						var eventPosition = event[(d === 'x') ? 'pageX' : 'pageY'];
						var initOffset = c[scrollOffset]();
						scrollx.scroll.addClass('scroll-draggable');
						$(document).on('mousemove' + namespace, function (event) {
							var diff = parseInt((event[(d === 'x') ? 'pageX' : 'pageY'] - eventPosition) / scrollx.kx, 10);
							c[scrollOffset](initOffset + diff);
						});
						return S._handleMouseDown(function () {
							scrollx.scroll.removeClass('scroll-draggable');
							scrollToValue = c[scrollOffset]();
						}, event);
					});
				}
			});
			$.each(s, function (d, scrollx) {
				var scrollClass = 'scroll-scroll' + d + '_visible';
				var scrolly = (d == "x") ? s.y : s.x;
				scrollx.scroll.removeClass(scrollClass);
				scrolly.scroll.removeClass(scrollClass);
				cw.removeClass(scrollClass);
			});
			$.each(s, function (d, scrollx) {
				$.extend(scrollx, (d == "x") ? {
					"offset": parseInt(c.css('left'), 10) || 0,
					"size": c.prop('scrollWidth'),
					"visible": w.width()
				} : {
					"offset": parseInt(c.css('top'), 10) || 0,
					"size": c.prop('scrollHeight'),
					"visible": w.height()
				});
			});
			this._updateScroll('x', this.scrollx);
			this._updateScroll('y', this.scrolly);
			if ($.isFunction(o.onUpdate)) {
				o.onUpdate.apply(this, [c]);
			}
			$.each(s, function (d, scrollx) {
				var cssOffset = (d === 'x') ? 'left' : 'top';
				var cssFullSize = (d === 'x') ? 'outerWidth' : 'outerHeight';
				var cssSize = (d === 'x') ? 'width' : 'height';
				var offset = parseInt(c.css(cssOffset), 10) || 0;
				var AreaSize = scrollx.size;
				var AreaVisible = scrollx.visible + offset;
				var scrollSize = scrollx.scroll.size[cssFullSize]() + (parseInt(scrollx.scroll.size.css(cssOffset), 10) || 0);
				if (o.autoScrollSize) {
					scrollx.scrollbarSize = parseInt(scrollSize * AreaVisible / AreaSize, 10);
					scrollx.scroll.bar.css(cssSize, scrollx.scrollbarSize + 'px');
				}
				scrollx.scrollbarSize = scrollx.scroll.bar[cssFullSize]();
				scrollx.kx = ((scrollSize - scrollx.scrollbarSize) / (AreaSize - AreaVisible)) || 1;
				scrollx.maxScrollOffset = AreaSize - AreaVisible;
			});
			c.scrollLeft(initScroll.scrollLeft).scrollTop(initScroll.scrollTop).trigger('scroll');
		},
		_getScroll: function (scroll) {
			var types = {
				advanced: ['<div class="scroll-element">', '<div class="scroll-element_corner"></div>', '<div class="scroll-arrow scroll-arrow_less"></div>', '<div class="scroll-arrow scroll-arrow_more"></div>', '<div class="scroll-element_outer">', '<div class="scroll-element_size"></div>', '<div class="scroll-element_inner-wrapper">', '<div class="scroll-element_inner scroll-element_track">', '<div class="scroll-element_inner-bottom"></div>', '</div>', '</div>', '<div class="scroll-bar">', '<div class="scroll-bar_body">', '<div class="scroll-bar_body-inner"></div>', '</div>', '<div class="scroll-bar_bottom"></div>', '<div class="scroll-bar_center"></div>', '</div>', '</div>', '</div>'].join(''),
				simple: ['<div class="scroll-element">', '<div class="scroll-element_outer">', '<div class="scroll-element_size"></div>', '<div class="scroll-element_track"></div>', '<div class="scroll-bar"></div>', '</div>', '</div>'].join('')
			};
			if (types[scroll]) {
				scroll = types[scroll];
			}
			if (!scroll) {
				scroll = types['simple'];
			}
			if (typeof (scroll) == 'string') {
				scroll = $(scroll).appendTo(this.wrapper);
			} else {
				scroll = $(scroll);
			}
			$.extend(scroll, {
				bar: scroll.find('.scroll-bar'),
				size: scroll.find('.scroll-element_size'),
				track: scroll.find('.scroll-element_track')
			});
			return scroll;
		},
		_handleMouseDown: function (callback, event) {
			var namespace = this.namespace;
			$(document).on('blur' + namespace, function () {
				$(document).add('body').off(namespace);
				callback && callback();
			});
			$(document).on('dragstart' + namespace, function (event) {
				event.preventDefault();
				return false;
			});
			$(document).on('mouseup' + namespace, function () {
				$(document).add('body').off(namespace);
				callback && callback();
			});
			$('body').on('selectstart' + namespace, function (event) {
				event.preventDefault();
				return false;
			});
			event && event.preventDefault();
			return false;
		},
		_updateScroll: function (d, scrollx) {
			var container = this.container,
				containerWrapper = this.containerWrapper || container,
				scrollClass = 'scroll-scroll' + d + '_visible',
				scrolly = (d === 'x') ? this.scrolly : this.scrollx,
				offset = parseInt(this.container.css((d === 'x') ? 'left' : 'top'), 10) || 0,
				wrapper = this.wrapper;
			var AreaSize = scrollx.size;
			var AreaVisible = scrollx.visible + offset;
			scrollx.isVisible = (AreaSize - AreaVisible) > 1;
			if (scrollx.isVisible) {
				scrollx.scroll.addClass(scrollClass);
				scrolly.scroll.addClass(scrollClass);
				containerWrapper.addClass(scrollClass);
			} else {
				scrollx.scroll.removeClass(scrollClass);
				scrolly.scroll.removeClass(scrollClass);
				containerWrapper.removeClass(scrollClass);
			}
			if (d === 'y') {
				if (container.is('textarea') || AreaSize < AreaVisible) {
					containerWrapper.css({
						"height": (AreaVisible + browser.scroll.height) + 'px',
						"max-height": "none"
					});
				} else {
					containerWrapper.css({
						"max-height": (AreaVisible + browser.scroll.height) + 'px'
					});
				}
			}
			if (scrollx.size != container.prop('scrollWidth') || scrolly.size != container.prop('scrollHeight') || scrollx.visible != wrapper.width() || scrolly.visible != wrapper.height() || scrollx.offset != (parseInt(container.css('left'), 10) || 0) || scrolly.offset != (parseInt(container.css('top'), 10) || 0)) {
				$.extend(this.scrollx, {
					"offset": parseInt(container.css('left'), 10) || 0,
					"size": container.prop('scrollWidth'),
					"visible": wrapper.width()
				});
				$.extend(this.scrolly, {
					"offset": parseInt(container.css('top'), 10) || 0,
					"size": this.container.prop('scrollHeight'),
					"visible": wrapper.height()
				});
				this._updateScroll(d === 'x' ? 'y' : 'x', scrolly);
			}
		}
	};
	var CustomScrollbar = BaseScrollbar;
	$.fn.scrollbar = function (command, args) {
		if (typeof command !== 'string') {
			args = command;
			command = 'init';
		}
		if (typeof args === 'undefined') {
			args = [];
		}
		if (!$.isArray(args)) {
			args = [args];
		}
		this.not('body, .scroll-wrapper').each(function () {
			var element = $(this),
				instance = element.data(browser.data.name);
			if (instance || command === 'init') {
				if (!instance) {
					instance = new CustomScrollbar(element);
				}
				if (instance[command]) {
					instance[command].apply(instance, args);
				}
			}
		});
		return this;
	};
	$.fn.scrollbar.options = defaults;
	var updateScrollbars = (function () {
		var timer = 0,
			timerCounter = 0;
		return function (force) {
			var i, container, options, scroll, wrapper, scrollx, scrolly;
			for (i = 0; i < browser.scrolls.length; i++) {
				scroll = browser.scrolls[i];
				container = scroll.container;
				options = scroll.options;
				wrapper = scroll.wrapper;
				scrollx = scroll.scrollx;
				scrolly = scroll.scrolly;
				if (force || (options.autoUpdate && wrapper && wrapper.is(':visible') && (container.prop('scrollWidth') != scrollx.size || container.prop('scrollHeight') != scrolly.size || wrapper.width() != scrollx.visible || wrapper.height() != scrolly.visible))) {
					scroll.init();
					if (options.debug) {
						window.console && console.log({
							scrollHeight: container.prop('scrollHeight') + ':' + scroll.scrolly.size,
							scrollWidth: container.prop('scrollWidth') + ':' + scroll.scrollx.size,
							visibleHeight: wrapper.height() + ':' + scroll.scrolly.visible,
							visibleWidth: wrapper.width() + ':' + scroll.scrollx.visible
						}, true);
						timerCounter++;
					}
				}
			}
			if (debug && timerCounter > 10) {
				window.console && console.log('Scroll updates exceed 10');
				updateScrollbars = function () {};
			} else {
				clearTimeout(timer);
				timer = setTimeout(updateScrollbars, 300);
			}
		};
	})();

	function getBrowserScrollSize(actualSize) {
		if (browser.webkit && !actualSize) {
			return {
				"height": 0,
				"width": 0
			};
		}
		if (!browser.data.outer) {
			var css = {
				"border": "none",
				"box-sizing": "content-box",
				"height": "200px",
				"margin": "0",
				"padding": "0",
				"width": "200px"
			};
			browser.data.inner = $("<div>").css($.extend({}, css));
			browser.data.outer = $("<div>").css($.extend({
				"left": "-1000px",
				"overflow": "scroll",
				"position": "absolute",
				"top": "-1000px"
			}, css)).append(browser.data.inner).appendTo("body");
		}
		browser.data.outer.scrollLeft(1000).scrollTop(1000);
		return {
			"height": Math.ceil((browser.data.outer.offset().top - browser.data.inner.offset().top) || 0),
			"width": Math.ceil((browser.data.outer.offset().left - browser.data.inner.offset().left) || 0)
		};
	}

	function isScrollOverlaysContent() {
		var scrollSize = getBrowserScrollSize(true);
		return !(scrollSize.height || scrollSize.width);
	}

	function isVerticalScroll(event) {
		var e = event.originalEvent;
		if (e.axis && e.axis === e.HORIZONTAL_AXIS)
			return false;
		if (e.wheelDeltaX)
			return false;
		return true;
	}
	if (window.angular) {
		(function (angular) {
			angular.module('jQueryScrollbar', []).provider('jQueryScrollbar', function () {
				var defaultOptions = defaults;
				return {
					setOptions: function (options) {
						angular.extend(defaultOptions, options);
					},
					$get: function () {
						return {
							options: angular.copy(defaultOptions)
						};
					}
				};
			}).directive('jqueryScrollbar', ['jQueryScrollbar', '$parse', function (jQueryScrollbar, $parse) {
				return {
					"restrict": "AC",
					"link": function (scope, element, attrs) {
						var model = $parse(attrs.jqueryScrollbar),
							options = model(scope);
						element.scrollbar(options || jQueryScrollbar.options).on('$destroy', function () {
							element.scrollbar('destroy');
						});
					}
				};
			}]);
		})(window.angular);
	}
}));;
(function ($) {
	$(document).ready(function () {
		$('.learn-press-tip').QuickTip();
	})
})(jQuery);;
(function ($, LP, _) {
	'use strict';

	function LP_Storage(key) {
		var storage = window.localStorage;
		this.key = key;
		this.get = function (id) {
			var val = storage.getItem(this.key) || '',
				sections = val.split(',');
			if (id) {
				id = id + '';
				var pos = sections.indexOf(id);
				if (pos >= 0) {
					return sections[pos];
				}
			}
			return sections;
		}
		this.set = function (sections) {
			if (typeof sections !== 'string') {
				sections = sections.join(',');
			}
			storage.setItem(this.key, sections);
			return sections.split(',');
		}
		this.hasSection = function (id) {
			id = id + '';
			var sections = this.get(),
				at = sections.indexOf(id);
			return at >= 0 ? at : false;
		}
		this.add = function (id) {
			id = id + '';
			var sections = this.get();
			if (this.hasSection(id)) {
				return;
			}
			sections.push(id);
			this.set(sections);
			return sections;
		}
		this.remove = function (id) {
			id = id + '';
			var at = this.hasSection(id);
			if (at !== false) {
				var sections = this.get();
				sections.splice(at, 1);
				this.set(sections);
				return sections;
			}
			return false;
		}
	}

	function LP_Course(settings) {
		var sectionStorage = new LP_Storage('sections'),
			$body = $('body'),
			$content = $('.content-item-scrollable'),
			$curriculum = $('#learn-press-course-curriculum'),
			$contentItem = $('#learn-press-content-item'),
			$curriculumScrollable = $curriculum.find('.curriculum-scrollable'),
			$header = $('#course-item-content-header'),
			$footer = $('#course-item-content-footer'),
			$courseItems = $curriculum.find('.course-item'),
			isShowingHeader = true,
			fullScreen, contentTop = 0,
			headerTimer, inPopup = false;

		function toggleAnswerOptions(event) {
			var $el = $(event.target),
				$chk;
			if ($el.is('input.option-check')) {
				return;
			}
			$chk = $el.closest('.answer-option').find('input.option-check');
			if (!$chk.length) {
				return;
			}
			if ($chk.is(':disabled')) {
				return;
			}
			if ($chk.is(':checkbox')) {
				$chk[0].checked = !$chk[0].checked;
			} else {
				$chk[0].checked = true;
			}
		}

		function toggleSection() {
			var id = $(this).closest('.section').data('section-id');
			$(this).siblings('.section-content').slideToggle(function () {
				if ($(this).is(':visible')) {
					sectionStorage.remove(id);
				} else {
					sectionStorage.add(id);
				}
			});
		}

		function initSections() {
			var $activeSection = $('.course-item.current').closest('.section'),
				sections = $('.curriculum-sections').find('.section'),
				sectionId = $activeSection.data('section-id'),
				hiddenSections = [];
			if ($activeSection) {
				hiddenSections = sectionStorage.remove(sectionId);
			} else {
				hiddenSections = sectionStorage.get();
			}
			for (var i = 0; i < hiddenSections.length; i++) {
				sections.filter('[data-section-id="' + hiddenSections[i] + '"]').find('.section-content').hide();
			}
		}

		function prepareForm(form) {
			var $answerOptions = $('.answer-options'),
				$form = $(form),
				data = $answerOptions.serializeJSON(),
				$hidden = $('<input type="hidden" name="question-data" />').val(JSON.stringify(data));
			if (($form.attr('method') + '').toLowerCase() !== 'post') {
				return;
			}
			$form.find('input[name="question-data"]').remove();
			return $form.append($hidden).append($('<div />').append($answerOptions.clone()).hide());
		}

		function onTabCourseClick(e, tab) {
			if ($(document.body).hasClass('course-item-popup')) {
				return;
			}
			var $tab = $(tab),
				$parent = $tab.closest('.course-nav');
			if ($parent.siblings().length === 0) {
				return;
			}
			LP.setUrl($tab.attr('href'))
		}

		function onSearchInputKeypress(e) {
			if (e.type === 'keypress' && e.keyCode === 13) {
				return false;
			}
			var s = this.value,
				r = new RegExp(s, 'ig');
			$courseItems.map(function () {
				var $item = $(this),
					itemName = $item.find('.item-name').text();
				if (itemName.match(r) || !s.length) {
					$item.show();
				} else {
					$item.hide();
				}
			});
			$('.section').show().each(function () {
				if (s.length) {
					if (!$(this).find('.section-content').children(':visible').length) {
						$(this).hide();
					} else {
						$(this).show();
					}
				} else {
					$(this).show();
				}
			});
			$(this).closest('.course-item-search').toggleClass('has-keyword', !!this.value.length);
		}

		function onClearSearchInputClick(e) {
			var $form = $(this).closest('.course-item-search');
			$form.find('input').val('').trigger('keyup')
		}

		function onClickQM() {
			$('#qm').css({
				'z-index': 999999999,
				position: 'relative'
			});
			$('html, body').css('overflow', 'auto');
		}

		function getCurriculumWidth() {
			return $curriculum.outerWidth();
		}

		function maybeShowCurriculum(e) {
			return;
			var offset = $(this).offset(),
				offsetX = e.pageX - offset.left,
				curriculumWidth = getCurriculumWidth();
			if (!fullScreen || (offsetX > 50)) {
				return;
			}
			timeoutToClose();
			if (!isShowingHeader) {
				$curriculum.stop().animate({
					left: 0
				});
				$contentItem.stop().animate({
					left: curriculumWidth
				});
				$footer.stop().animate({
					left: curriculumWidth
				}, function () {
					$(document, window).trigger('learn-press/toggle-content-item');
				});
				$header.find('.course-item-search').show();
				toggleEventShowCurriculum(true);
				isShowingHeader = true;
			}
		}

		function toggleEventShowCurriculum(b) {
			$(document)[b ? 'off' : 'on']('mousemove.maybe-show-curriculum', 'body', maybeShowCurriculum);
		}

		function timeoutToClose() {
			headerTimer && clearTimeout(headerTimer);
			headerTimer = setTimeout(function () {
				var curriculumWidth = getCurriculumWidth();
				if (!fullScreen) {
					return;
				}
				$curriculum.stop().animate({
					left: -curriculumWidth
				});
				$contentItem.stop().animate({
					left: 0
				});
				$footer.stop().animate({
					left: 0
				}, function () {
					$(document, window).trigger('learn-press/toggle-content-item');
				});
				$header.find('.course-item-search').hide();
				isShowingHeader = false;
				toggleEventShowCurriculum();
			}, 3000);
		}

		function toggleContentItem(e) {
			e.preventDefault();
			var curriculumWidth = getCurriculumWidth();
			fullScreen = $body.toggleClass('full-screen-content-item').hasClass('full-screen-content-item');
			$curriculum.stop().animate({
				left: fullScreen ? -curriculumWidth : 0
			});
			$contentItem.stop().animate({
				left: fullScreen ? 0 : curriculumWidth
			});
			$footer.stop().animate({
				left: fullScreen ? 0 : curriculumWidth
			}, function () {
				$(document, window).trigger('learn-press/toggle-content-item');
			});
			isShowingHeader = !fullScreen;
			window.localStorage && window.localStorage.setItem('lp-full-screen', fullScreen ? 'yes' : 'no');
			fullScreen && toggleEventShowCurriculum();
			$header.find('.course-title').stop().animate({
				marginLeft: fullScreen ? -curriculumWidth : 0
			})
			$header.find('.course-item-search').stop().animate({
				opacity: fullScreen ? 0 : 1
			});
		}

		function initEvents() {
			$(document).on('learn-press/nav-tabs/clicked', onTabCourseClick).on('keyup keypress', '.course-item-search input', onSearchInputKeypress).on('click', '.course-item-search button', onClearSearchInputClick).on('click', '#wp-admin-bar-query-monitor', onClickQM).on('click', '.answer-options .answer-option', toggleAnswerOptions).on('click', '.section-header', toggleSection).on('submit', 'form.lp-form', function () {
				prepareForm(this);
			}).on('click', '.toggle-content-item', toggleContentItem);
			$curriculum.hover(function () {
				headerTimer && clearTimeout(headerTimer);
			}, function () {
				if (fullScreen) timeoutToClose();
			})
		}

		function initScrollbar() {
			$content.addClass('scrollbar-light').scrollbar({
				scrollx: false
			});
			$content.parent().css({
				position: 'absolute',
				top: 0,
				bottom: $('#course-item-content-footer:visible').outerHeight() || 0,
				width: '100%'
			}).css('opacity', 1).end().css('opacity', 1);
			$curriculumScrollable.addClass('scrollbar-light').scrollbar({
				scrollx: false
			});
			$curriculumScrollable.parent().css({
				position: 'absolute',
				top: 0,
				bottom: 0,
				width: '100%'
			}).css('opacity', 1).end().css('opacity', 1);
		}

		function fitVideo() {
			var $wrapContent = $('.content-item-summary.content-item-video');
			if (!$wrapContent.length) {
				return;
			}
			var $entryVideo = $wrapContent.find('.entry-video'),
				$frame = $entryVideo.find('iframe'),
				width = $frame.attr('width'),
				height = $frame.attr('height'),
				ratio = 1,
				contentHeight, timer;

			function resizeVideo() {
				var frameWidth = $frame.width();
				contentHeight = frameWidth * ratio;
				$frame.css({
					height: contentHeight,
					marginLeft: ($entryVideo.width() - frameWidth) / 2
				});
				$wrapContent.css({
					paddingTop: contentHeight
				});
			}
			if (!$entryVideo.length) {
				return false;
			}
			if (width && height) {
				if (width.indexOf('%') === -1 && height.indexOf('%') === -1) {
					ratio = height / width;
				}
			}
			$(window).on('resize.fit-content-video learn-press/toggle-content-item', function () {
				timer && clearTimeout(timer);
				timer = setTimeout(resizeVideo, 250);
			}).trigger('resize.fit-content-video');
			$('.content-item-scrollable').scroll(function () {
				$(this).find('.entry-video').css('padding-top', this.scrollTop);
			});
		}

		function init() {
			inPopup = $body.hasClass('course-item-popup');
			initSections();
			initEvents();
			if (!inPopup) {
				return;
			}
			$contentItem.appendTo($body);
			$curriculum.appendTo($body);
			if ($('#wpadminbar').length) {
				$body.addClass('wpadminbar');
				contentTop = 32;
			}
			initScrollbar();
			fitVideo();
			fullScreen = window.localStorage && 'yes' === window.localStorage.getItem('lp-full-screen');
			if ($(window).width() <= 768) {
				fullScreen = true;
			}
			if (fullScreen) {
				var curriculumWidth = getCurriculumWidth();
				$body.addClass('full-screen-content-item');
				$contentItem.css('left', 0);
				$curriculum.css('left', -curriculumWidth);
				$footer.css('left', 0);
				isShowingHeader = !fullScreen;
				$header.find('.course-title').css({
					marginLeft: fullScreen ? -curriculumWidth : 0
				})
				$header.find('.course-item-search').css({
					opacity: fullScreen ? 0 : 1
				});
				toggleEventShowCurriculum();
			}
			setTimeout(function () {
				var $cs = $body.find('.curriculum-sections').parent();
				$cs.scrollTo($cs.find('.course-item.current'), 100);
				if (window.location.hash) {
					$('.content-item-scrollable:last').scrollTo($(window.location.hash));
				}
			}, 300);
			$body.css('opacity', 1);
		}
		new LP.Alerts();
		init();
	}
	LP.Alerts = function () {
		this.isShowing = false;
		var $doc = $(document),
			self = this,
			trigger = function (action, args) {
				var triggered = $doc.triggerHandler(action, args);
				if (triggered !== undefined) {
					return triggered;
				}
				return $.isArray(args) ? args[0] : undefined;
			},
			confirmHandle = function (e) {
				try {
					var $form = $(this),
						message = $form.data('confirm'),
						action = $form.data('action');
					message = trigger('learn-press/confirm-message', [message, action]);
					if (!message) {
						return true;
					}
					jConfirm(message, '', function (confirm) {
						confirm && $form.off('submit.learn-press-confirm', confirmHandle).submit();
						self.isShowing = false;
					});
					self.isShowing = true;
					return false;
				} catch (ex) {
					console.log(ex)
				}
				return true;
			}
		this.watchChange('isShowing', function (prop, oldVal, newVal) {
			if (newVal) {
				setTimeout(function () {
					$.alerts._reposition();
					$('#popup_container').addClass('ready')
				}, 30)
				var $a = $('<a href="" class="close"><i class="fa fa-times"></i></a>')
				$('#popup_container').append($a);
				$a.on('click', function () {
					$.alerts._hide();
					return false;
				});
			}
			$(document.body).toggleClass('confirm', newVal);
			return newVal;
		});
		var $forms = $('form[data-confirm]').on('submit.learn-press-confirm', confirmHandle);
	}
	$(document).ready(function () {
		$(document).ready(function () {
			new LP_Course({});
			$(this).on('submit', 'form[name="course-external-link"]', function () {
				var redirect = $(this).attr('action');
				if (redirect) {
					window.location.href = redirect;
					return false;
				}
			})
		});
	});
})
(jQuery, LP, _);;
/*!
 * jQuery.scrollTo
 * Copyright (c) 2007-2015 Ariel Flesler - aflesler ○ gmail • com | http://flesler.blogspot.com
 * Licensed under MIT
 * http://flesler.blogspot.com/2007/10/jqueryscrollto.html
 * @projectDescription Lightweight, cross-browser and highly customizable animated scrolling with jQuery
 * @author Ariel Flesler
 * @version 2.1.2
 */
(function (factory) {
	'use strict';
	if (typeof define === 'function' && define.amd) {
		define(['jquery'], factory);
	} else if (typeof module !== 'undefined' && module.exports) {
		module.exports = factory(require('jquery'));
	} else {
		factory(jQuery);
	}
})(function ($) {
	'use strict';
	var $scrollTo = $.scrollTo = function (target, duration, settings) {
		return $(window).scrollTo(target, duration, settings);
	};
	$scrollTo.defaults = {
		axis: 'xy',
		duration: 0,
		limit: true
	};

	function isWin(elem) {
		return !elem.nodeName || $.inArray(elem.nodeName.toLowerCase(), ['iframe', '#document', 'html', 'body']) !== -1;
	}
	$.fn.scrollTo = function (target, duration, settings) {
		if (typeof duration === 'object') {
			settings = duration;
			duration = 0;
		}
		if (typeof settings === 'function') {
			settings = {
				onAfter: settings
			};
		}
		if (target === 'max') {
			target = 9e9;
		}
		settings = $.extend({}, $scrollTo.defaults, settings);
		duration = duration || settings.duration;
		var queue = settings.queue && settings.axis.length > 1;
		if (queue) {
			duration /= 2;
		}
		settings.offset = both(settings.offset);
		settings.over = both(settings.over);
		return this.each(function () {
			if (target === null) return;
			var win = isWin(this),
				elem = win ? this.contentWindow || window : this,
				$elem = $(elem),
				targ = target,
				attr = {},
				toff;
			switch (typeof targ) {
				case 'number':
				case 'string':
					if (/^([+-]=?)?\d+(\.\d+)?(px|%)?$/.test(targ)) {
						targ = both(targ);
						break;
					}
					targ = win ? $(targ) : $(targ, elem);
				case 'object':
					if (targ.length === 0) return;
					if (targ.is || targ.style) {
						toff = (targ = $(targ)).offset();
					}
			}
			var offset = $.isFunction(settings.offset) && settings.offset(elem, targ) || settings.offset;
			$.each(settings.axis.split(''), function (i, axis) {
				var Pos = axis === 'x' ? 'Left' : 'Top',
					pos = Pos.toLowerCase(),
					key = 'scroll' + Pos,
					prev = $elem[key](),
					max = $scrollTo.max(elem, axis);
				if (toff) {
					attr[key] = toff[pos] + (win ? 0 : prev - $elem.offset()[pos]);
					if (settings.margin) {
						attr[key] -= parseInt(targ.css('margin' + Pos), 10) || 0;
						attr[key] -= parseInt(targ.css('border' + Pos + 'Width'), 10) || 0;
					}
					attr[key] += offset[pos] || 0;
					if (settings.over[pos]) {
						attr[key] += targ[axis === 'x' ? 'width' : 'height']() * settings.over[pos];
					}
				} else {
					var val = targ[pos];
					attr[key] = val.slice && val.slice(-1) === '%' ? parseFloat(val) / 100 * max : val;
				}
				if (settings.limit && /^\d+$/.test(attr[key])) {
					attr[key] = attr[key] <= 0 ? 0 : Math.min(attr[key], max);
				}
				if (!i && settings.axis.length > 1) {
					if (prev === attr[key]) {
						attr = {};
					} else if (queue) {
						animate(settings.onAfterFirst);
						attr = {};
					}
				}
			});
			animate(settings.onAfter);

			function animate(callback) {
				var opts = $.extend({}, settings, {
					queue: true,
					duration: duration,
					complete: callback && function () {
						callback.call(elem, targ, settings);
					}
				});
				$elem.animate(attr, opts);
			}
		});
	};
	$scrollTo.max = function (elem, axis) {
		var Dim = axis === 'x' ? 'Width' : 'Height',
			scroll = 'scroll' + Dim;
		if (!isWin(elem))
			return elem[scroll] - $(elem)[Dim.toLowerCase()]();
		var size = 'client' + Dim,
			doc = elem.ownerDocument || elem.document,
			html = doc.documentElement,
			body = doc.body;
		return Math.max(html[scroll], body[scroll]) - Math.min(html[size], body[size]);
	};

	function both(val) {
		return $.isFunction(val) || $.isPlainObject(val) ? val : {
			top: val,
			left: val
		};
	}
	$.Tween.propHooks.scrollLeft = $.Tween.propHooks.scrollTop = {
		get: function (t) {
			return $(t.elem)[t.prop]();
		},
		set: function (t) {
			var curr = this.get(t);
			if (t.options.interrupt && t._last && t._last !== curr) {
				return $(t.elem).stop();
			}
			var next = Math.round(t.now);
			if (curr !== next) {
				$(t.elem)[t.prop](next);
				t._last = this.get(t);
			}
		}
	};
	return $scrollTo;
});
if (typeof jQuery === 'undefined') {
	console.log('jQuery is not defined');
} else {
	(function ($) {
		$(document).ready(function () {
			$('form[name="become-teacher-form"]').each(function () {
				var $form = $(this),
					$submit = $form.find('button[type="submit"]'),
					hideMessages = function () {
						$('.learn-press-error, .learn-press-message').fadeOut('fast', function () {
							$(this).remove()
						});
					},
					showMessages = function (messages) {
						var m = [];
						if ($.isPlainObject(messages)) {
							for (var i in messages) {
								m.push($(messages[i]));
							}
						} else if ($.isArray(messages)) {
							m = messages.reverse();
						} else {
							m = [messages];
						}
						for (var i = 0; i < m.length; i++) {
							$(m[i]).insertBefore($form);
						}
					},
					blockForm = function (block) {
						return $form.find('input, select, button, textarea').prop('disabled', !!block)
					},
					beforeSend = function () {
						hideMessages();
						blockForm(true).filter($submit).data('origin-text', $submit.text()).html($submit.data('text'));
					},
					ajaxSuccess = function (response) {
						response = LP.parseJSON(response);
						if (response.message) {
							showMessages(response.message)
						}
						blockForm().filter($submit).html($submit.data('origin-text'));
						if (response.result === 'success') {
							$form.remove();
						} else {
							$submit.prop('disabled', false);
							$submit.html($submit.data('text'));
						}
					},
					ajaxError = function (response) {
						response = LP.parseJSON(response);
						if (response.message) {
							showMessages(response.message)
						}
						blockForm().filter($submit).html($submit.data('origin-text'));
					};
				$form.submit(function () {
					if ($form.triggerHandler('become_teacher_send') !== false) {
						$.ajax({
							url: window.location.href.addQueryVar('lp-ajax', 'request-become-a-teacher'),
							data: $form.serialize(),
							dataType: 'text',
							type: 'post',
							beforeSend: beforeSend,
							success: ajaxSuccess,
							error: ajaxError
						});
					}
					return false;
				});
			})
		});
	})(jQuery);
};
(function ($) {
	'use strict';
	if (typeof wpcf7 === 'undefined' || wpcf7 === null) {
		return;
	}
	wpcf7 = $.extend({
		cached: 0,
		inputs: []
	}, wpcf7);
	$(function () {
		wpcf7.supportHtml5 = (function () {
			var features = {};
			var input = document.createElement('input');
			features.placeholder = 'placeholder' in input;
			var inputTypes = ['email', 'url', 'tel', 'number', 'range', 'date'];
			$.each(inputTypes, function (index, value) {
				input.setAttribute('type', value);
				features[value] = input.type !== 'text';
			});
			return features;
		})();
		$('div.wpcf7 > form').each(function () {
			var $form = $(this);
			wpcf7.initForm($form);
			if (wpcf7.cached) {
				wpcf7.refill($form);
			}
		});
	});
	wpcf7.getId = function (form) {
		return parseInt($('input[name="_wpcf7"]', form).val(), 10);
	};
	wpcf7.initForm = function (form) {
		var $form = $(form);
		$form.submit(function (event) {
			if (!wpcf7.supportHtml5.placeholder) {
				$('[placeholder].placeheld', $form).each(function (i, n) {
					$(n).val('').removeClass('placeheld');
				});
			}
			if (typeof window.FormData === 'function') {
				wpcf7.submit($form);
				event.preventDefault();
			}
		});
		$('.wpcf7-submit', $form).after('<span class="ajax-loader"></span>');
		wpcf7.toggleSubmit($form);
		$form.on('click', '.wpcf7-acceptance', function () {
			wpcf7.toggleSubmit($form);
		});
		$('.wpcf7-exclusive-checkbox', $form).on('click', 'input:checkbox', function () {
			var name = $(this).attr('name');
			$form.find('input:checkbox[name="' + name + '"]').not(this).prop('checked', false);
		});
		$('.wpcf7-list-item.has-free-text', $form).each(function () {
			var $freetext = $(':input.wpcf7-free-text', this);
			var $wrap = $(this).closest('.wpcf7-form-control');
			if ($(':checkbox, :radio', this).is(':checked')) {
				$freetext.prop('disabled', false);
			} else {
				$freetext.prop('disabled', true);
			}
			$wrap.on('change', ':checkbox, :radio', function () {
				var $cb = $('.has-free-text', $wrap).find(':checkbox, :radio');
				if ($cb.is(':checked')) {
					$freetext.prop('disabled', false).focus();
				} else {
					$freetext.prop('disabled', true);
				}
			});
		});
		if (!wpcf7.supportHtml5.placeholder) {
			$('[placeholder]', $form).each(function () {
				$(this).val($(this).attr('placeholder'));
				$(this).addClass('placeheld');
				$(this).focus(function () {
					if ($(this).hasClass('placeheld')) {
						$(this).val('').removeClass('placeheld');
					}
				});
				$(this).blur(function () {
					if ('' === $(this).val()) {
						$(this).val($(this).attr('placeholder'));
						$(this).addClass('placeheld');
					}
				});
			});
		}
		if (wpcf7.jqueryUi && !wpcf7.supportHtml5.date) {
			$form.find('input.wpcf7-date[type="date"]').each(function () {
				$(this).datepicker({
					dateFormat: 'yy-mm-dd',
					minDate: new Date($(this).attr('min')),
					maxDate: new Date($(this).attr('max'))
				});
			});
		}
		if (wpcf7.jqueryUi && !wpcf7.supportHtml5.number) {
			$form.find('input.wpcf7-number[type="number"]').each(function () {
				$(this).spinner({
					min: $(this).attr('min'),
					max: $(this).attr('max'),
					step: $(this).attr('step')
				});
			});
		}
		$('.wpcf7-character-count', $form).each(function () {
			var $count = $(this);
			var name = $count.attr('data-target-name');
			var down = $count.hasClass('down');
			var starting = parseInt($count.attr('data-starting-value'), 10);
			var maximum = parseInt($count.attr('data-maximum-value'), 10);
			var minimum = parseInt($count.attr('data-minimum-value'), 10);
			var updateCount = function (target) {
				var $target = $(target);
				var length = $target.val().length;
				var count = down ? starting - length : length;
				$count.attr('data-current-value', count);
				$count.text(count);
				if (maximum && maximum < length) {
					$count.addClass('too-long');
				} else {
					$count.removeClass('too-long');
				}
				if (minimum && length < minimum) {
					$count.addClass('too-short');
				} else {
					$count.removeClass('too-short');
				}
			};
			$(':input[name="' + name + '"]', $form).each(function () {
				updateCount(this);
				$(this).keyup(function () {
					updateCount(this);
				});
			});
		});
		$form.on('change', '.wpcf7-validates-as-url', function () {
			var val = $.trim($(this).val());
			if (val && !val.match(/^[a-z][a-z0-9.+-]*:/i) && -1 !== val.indexOf('.')) {
				val = val.replace(/^\/+/, '');
				val = 'http://' + val;
			}
			$(this).val(val);
		});
	};
	wpcf7.submit = function (form) {
		if (typeof window.FormData !== 'function') {
			return;
		}
		var $form = $(form);
		$('.ajax-loader', $form).addClass('is-active');
		wpcf7.clearResponse($form);
		var formData = new FormData($form.get(0));
		var detail = {
			id: $form.closest('div.wpcf7').attr('id'),
			status: 'init',
			inputs: [],
			formData: formData
		};
		$.each($form.serializeArray(), function (i, field) {
			if ('_wpcf7' == field.name) {
				detail.contactFormId = field.value;
			} else if ('_wpcf7_version' == field.name) {
				detail.pluginVersion = field.value;
			} else if ('_wpcf7_locale' == field.name) {
				detail.contactFormLocale = field.value;
			} else if ('_wpcf7_unit_tag' == field.name) {
				detail.unitTag = field.value;
			} else if ('_wpcf7_container_post' == field.name) {
				detail.containerPostId = field.value;
			} else if (field.name.match(/^_wpcf7_\w+_free_text_/)) {
				var owner = field.name.replace(/^_wpcf7_\w+_free_text_/, '');
				detail.inputs.push({
					name: owner + '-free-text',
					value: field.value
				});
			} else if (field.name.match(/^_/)) {} else {
				detail.inputs.push(field);
			}
		});
		wpcf7.triggerEvent($form.closest('div.wpcf7'), 'beforesubmit', detail);
		var ajaxSuccess = function (data, status, xhr, $form) {
			detail.id = $(data.into).attr('id');
			detail.status = data.status;
			detail.apiResponse = data;
			var $message = $('.wpcf7-response-output', $form);
			switch (data.status) {
				case 'validation_failed':
					$.each(data.invalidFields, function (i, n) {
						$(n.into, $form).each(function () {
							wpcf7.notValidTip(this, n.message);
							$('.wpcf7-form-control', this).addClass('wpcf7-not-valid');
							$('[aria-invalid]', this).attr('aria-invalid', 'true');
						});
					});
					$message.addClass('wpcf7-validation-errors');
					$form.addClass('invalid');
					wpcf7.triggerEvent(data.into, 'invalid', detail);
					break;
				case 'acceptance_missing':
					$message.addClass('wpcf7-acceptance-missing');
					$form.addClass('unaccepted');
					wpcf7.triggerEvent(data.into, 'unaccepted', detail);
					break;
				case 'spam':
					$message.addClass('wpcf7-spam-blocked');
					$form.addClass('spam');
					$('[name="g-recaptcha-response"]', $form).each(function () {
						if ('' === $(this).val()) {
							var $recaptcha = $(this).closest('.wpcf7-form-control-wrap');
							wpcf7.notValidTip($recaptcha, wpcf7.recaptcha.messages.empty);
						}
					});
					wpcf7.triggerEvent(data.into, 'spam', detail);
					break;
				case 'aborted':
					$message.addClass('wpcf7-aborted');
					$form.addClass('aborted');
					wpcf7.triggerEvent(data.into, 'aborted', detail);
					break;
				case 'mail_sent':
					$message.addClass('wpcf7-mail-sent-ok');
					$form.addClass('sent');
					wpcf7.triggerEvent(data.into, 'mailsent', detail);
					break;
				case 'mail_failed':
					$message.addClass('wpcf7-mail-sent-ng');
					$form.addClass('failed');
					wpcf7.triggerEvent(data.into, 'mailfailed', detail);
					break;
				default:
					var customStatusClass = 'custom-' +
						data.status.replace(/[^0-9a-z]+/i, '-');
					$message.addClass('wpcf7-' + customStatusClass);
					$form.addClass(customStatusClass);
			}
			wpcf7.refill($form, data);
			wpcf7.triggerEvent(data.into, 'submit', detail);
			if ('mail_sent' == data.status) {
				$form.each(function () {
					this.reset();
				});
				wpcf7.toggleSubmit($form);
			}
			if (!wpcf7.supportHtml5.placeholder) {
				$form.find('[placeholder].placeheld').each(function (i, n) {
					$(n).val($(n).attr('placeholder'));
				});
			}
			$message.html('').append(data.message).slideDown('fast');
			$message.attr('role', 'alert');
			$('.screen-reader-response', $form.closest('.wpcf7')).each(function () {
				var $response = $(this);
				$response.html('').attr('role', '').append(data.message);
				if (data.invalidFields) {
					var $invalids = $('<ul></ul>');
					$.each(data.invalidFields, function (i, n) {
						if (n.idref) {
							var $li = $('<li></li>').append($('<a></a>').attr('href', '#' + n.idref).append(n.message));
						} else {
							var $li = $('<li></li>').append(n.message);
						}
						$invalids.append($li);
					});
					$response.append($invalids);
				}
				$response.attr('role', 'alert').focus();
			});
		};
		$.ajax({
			type: 'POST',
			url: wpcf7.apiSettings.getRoute('/contact-forms/' + wpcf7.getId($form) + '/feedback'),
			data: formData,
			dataType: 'json',
			processData: false,
			contentType: false
		}).done(function (data, status, xhr) {
			ajaxSuccess(data, status, xhr, $form);
			$('.ajax-loader', $form).removeClass('is-active');
		}).fail(function (xhr, status, error) {
			var $e = $('<div class="ajax-error"></div>').text(error.message);
			$form.after($e);
		});
	};
	wpcf7.triggerEvent = function (target, name, detail) {
		var $target = $(target);
		var event = new CustomEvent('wpcf7' + name, {
			bubbles: true,
			detail: detail
		});
		$target.get(0).dispatchEvent(event);
		$target.trigger('wpcf7:' + name, detail);
		$target.trigger(name + '.wpcf7', detail);
	};
	wpcf7.toggleSubmit = function (form, state) {
		var $form = $(form);
		var $submit = $('input:submit', $form);
		if (typeof state !== 'undefined') {
			$submit.prop('disabled', !state);
			return;
		}
		if ($form.hasClass('wpcf7-acceptance-as-validation')) {
			return;
		}
		$submit.prop('disabled', false);
		$('.wpcf7-acceptance', $form).each(function () {
			var $span = $(this);
			var $input = $('input:checkbox', $span);
			if (!$span.hasClass('optional')) {
				if ($span.hasClass('invert') && $input.is(':checked') || !$span.hasClass('invert') && !$input.is(':checked')) {
					$submit.prop('disabled', true);
					return false;
				}
			}
		});
	};
	wpcf7.notValidTip = function (target, message) {
		var $target = $(target);
		$('.wpcf7-not-valid-tip', $target).remove();
		$('<span role="alert" class="wpcf7-not-valid-tip"></span>').text(message).appendTo($target);
		if ($target.is('.use-floating-validation-tip *')) {
			var fadeOut = function (target) {
				$(target).not(':hidden').animate({
					opacity: 0
				}, 'fast', function () {
					$(this).css({
						'z-index': -100
					});
				});
			};
			$target.on('mouseover', '.wpcf7-not-valid-tip', function () {
				fadeOut(this);
			});
			$target.on('focus', ':input', function () {
				fadeOut($('.wpcf7-not-valid-tip', $target));
			});
		}
	};
	wpcf7.refill = function (form, data) {
		var $form = $(form);
		var refillCaptcha = function ($form, items) {
			$.each(items, function (i, n) {
				$form.find(':input[name="' + i + '"]').val('');
				$form.find('img.wpcf7-captcha-' + i).attr('src', n);
				var match = /([0-9]+)\.(png|gif|jpeg)$/.exec(n);
				$form.find('input:hidden[name="_wpcf7_captcha_challenge_' + i + '"]').attr('value', match[1]);
			});
		};
		var refillQuiz = function ($form, items) {
			$.each(items, function (i, n) {
				$form.find(':input[name="' + i + '"]').val('');
				$form.find(':input[name="' + i + '"]').siblings('span.wpcf7-quiz-label').text(n[0]);
				$form.find('input:hidden[name="_wpcf7_quiz_answer_' + i + '"]').attr('value', n[1]);
			});
		};
		if (typeof data === 'undefined') {
			$.ajax({
				type: 'GET',
				url: wpcf7.apiSettings.getRoute('/contact-forms/' + wpcf7.getId($form) + '/refill'),
				beforeSend: function (xhr) {
					var nonce = $form.find(':input[name="_wpnonce"]').val();
					if (nonce) {
						xhr.setRequestHeader('X-WP-Nonce', nonce);
					}
				},
				dataType: 'json'
			}).done(function (data, status, xhr) {
				if (data.captcha) {
					refillCaptcha($form, data.captcha);
				}
				if (data.quiz) {
					refillQuiz($form, data.quiz);
				}
			});
		} else {
			if (data.captcha) {
				refillCaptcha($form, data.captcha);
			}
			if (data.quiz) {
				refillQuiz($form, data.quiz);
			}
		}
	};
	wpcf7.clearResponse = function (form) {
		var $form = $(form);
		$form.removeClass('invalid spam sent failed');
		$form.siblings('.screen-reader-response').html('').attr('role', '');
		$('.wpcf7-not-valid-tip', $form).remove();
		$('[aria-invalid]', $form).attr('aria-invalid', 'false');
		$('.wpcf7-form-control', $form).removeClass('wpcf7-not-valid');
		$('.wpcf7-response-output', $form).hide().empty().removeAttr('role').removeClass('wpcf7-mail-sent-ok wpcf7-mail-sent-ng wpcf7-validation-errors wpcf7-spam-blocked');
	};
	wpcf7.apiSettings.getRoute = function (path) {
		var url = wpcf7.apiSettings.root;
		url = url.replace(wpcf7.apiSettings.namespace, wpcf7.apiSettings.namespace + path);
		return url;
	};
})(jQuery);
(function () {
	if (typeof window.CustomEvent === "function") return false;

	function CustomEvent(event, params) {
		params = params || {
			bubbles: false,
			cancelable: false,
			detail: undefined
		};
		var evt = document.createEvent('CustomEvent');
		evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
		return evt;
	}
	CustomEvent.prototype = window.Event.prototype;
	window.CustomEvent = CustomEvent;
})();
/*!
 * jQuery blockUI plugin
 * Version 2.70.0-2014.11.23
 * Requires jQuery v1.7 or later
 *
 * Examples at: http://malsup.com/jquery/block/
 * Copyright (c) 2007-2013 M. Alsup
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
 *
 * Thanks to Amir-Hossein Sobhi for some excellent contributions!
 */
! function () {
	"use strict";

	function e(e) {
		function t(t, n) {
			var s, h, k = t == window,
				y = n && n.message !== undefined ? n.message : undefined;
			if (!(n = e.extend({}, e.blockUI.defaults, n || {})).ignoreIfBlocked || !e(t).data("blockUI.isBlocked")) {
				if (n.overlayCSS = e.extend({}, e.blockUI.defaults.overlayCSS, n.overlayCSS || {}), s = e.extend({}, e.blockUI.defaults.css, n.css || {}), n.onOverlayClick && (n.overlayCSS.cursor = "pointer"), h = e.extend({}, e.blockUI.defaults.themedCSS, n.themedCSS || {}), y = y === undefined ? n.message : y, k && p && o(window, {
						fadeOut: 0
					}), y && "string" != typeof y && (y.parentNode || y.jquery)) {
					var m = y.jquery ? y[0] : y,
						g = {};
					e(t).data("blockUI.history", g), g.el = m, g.parent = m.parentNode, g.display = m.style.display, g.position = m.style.position, g.parent && g.parent.removeChild(m)
				}
				e(t).data("blockUI.onUnblock", n.onUnblock);
				var v, I, w, U, x = n.baseZ;
				v = e(r || n.forceIframe ? '<iframe class="blockUI" style="z-index:' + x++ + ';display:none;border:none;margin:0;padding:0;position:absolute;width:100%;height:100%;top:0;left:0" src="' + n.iframeSrc + '"></iframe>' : '<div class="blockUI" style="display:none"></div>'), I = e(n.theme ? '<div class="blockUI blockOverlay ui-widget-overlay" style="z-index:' + x++ + ';display:none"></div>' : '<div class="blockUI blockOverlay" style="z-index:' + x++ + ';display:none;border:none;margin:0;padding:0;width:100%;height:100%;top:0;left:0"></div>'), n.theme && k ? (U = '<div class="blockUI ' + n.blockMsgClass + ' blockPage ui-dialog ui-widget ui-corner-all" style="z-index:' + (x + 10) + ';display:none;position:fixed">', n.title && (U += '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (n.title || "&nbsp;") + "</div>"), U += '<div class="ui-widget-content ui-dialog-content"></div>', U += "</div>") : n.theme ? (U = '<div class="blockUI ' + n.blockMsgClass + ' blockElement ui-dialog ui-widget ui-corner-all" style="z-index:' + (x + 10) + ';display:none;position:absolute">', n.title && (U += '<div class="ui-widget-header ui-dialog-titlebar ui-corner-all blockTitle">' + (n.title || "&nbsp;") + "</div>"), U += '<div class="ui-widget-content ui-dialog-content"></div>', U += "</div>") : U = k ? '<div class="blockUI ' + n.blockMsgClass + ' blockPage" style="z-index:' + (x + 10) + ';display:none;position:fixed"></div>' : '<div class="blockUI ' + n.blockMsgClass + ' blockElement" style="z-index:' + (x + 10) + ';display:none;position:absolute"></div>', w = e(U), y && (n.theme ? (w.css(h), w.addClass("ui-widget-content")) : w.css(s)), n.theme || I.css(n.overlayCSS), I.css("position", k ? "fixed" : "absolute"), (r || n.forceIframe) && v.css("opacity", 0);
				var C = [v, I, w],
					S = e(k ? "body" : t);
				e.each(C, function () {
					this.appendTo(S)
				}), n.theme && n.draggable && e.fn.draggable && w.draggable({
					handle: ".ui-dialog-titlebar",
					cancel: "li"
				});
				var O = f && (!e.support.boxModel || e("object,embed", k ? null : t).length > 0);
				if (u || O) {
					if (k && n.allowBodyStretch && e.support.boxModel && e("html,body").css("height", "100%"), (u || !e.support.boxModel) && !k) var E = a(t, "borderTopWidth"),
						T = a(t, "borderLeftWidth"),
						M = E ? "(0 - " + E + ")" : 0,
						B = T ? "(0 - " + T + ")" : 0;
					e.each(C, function (e, t) {
						var o = t[0].style;
						if (o.position = "absolute", e < 2) k ? o.setExpression("height", "Math.max(document.body.scrollHeight, document.body.offsetHeight) - (jQuery.support.boxModel?0:" + n.quirksmodeOffsetHack + ') + "px"') : o.setExpression("height", 'this.parentNode.offsetHeight + "px"'), k ? o.setExpression("width", 'jQuery.support.boxModel && document.documentElement.clientWidth || document.body.clientWidth + "px"') : o.setExpression("width", 'this.parentNode.offsetWidth + "px"'), B && o.setExpression("left", B), M && o.setExpression("top", M);
						else if (n.centerY) k && o.setExpression("top", '(document.documentElement.clientHeight || document.body.clientHeight) / 2 - (this.offsetHeight / 2) + (blah = document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + "px"'), o.marginTop = 0;
						else if (!n.centerY && k) {
							var i = "((document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop) + " + (n.css && n.css.top ? parseInt(n.css.top, 10) : 0) + ') + "px"';
							o.setExpression("top", i)
						}
					})
				}
				if (y && (n.theme ? w.find(".ui-widget-content").append(y) : w.append(y), (y.jquery || y.nodeType) && e(y).show()), (r || n.forceIframe) && n.showOverlay && v.show(), n.fadeIn) {
					var j = n.onBlock ? n.onBlock : c,
						H = n.showOverlay && !y ? j : c,
						z = y ? j : c;
					n.showOverlay && I._fadeIn(n.fadeIn, H), y && w._fadeIn(n.fadeIn, z)
				} else n.showOverlay && I.show(), y && w.show(), n.onBlock && n.onBlock.bind(w)();
				if (i(1, t, n), k ? (p = w[0], b = e(n.focusableElements, p), n.focusInput && setTimeout(l, 20)) : d(w[0], n.centerX, n.centerY), n.timeout) {
					var W = setTimeout(function () {
						k ? e.unblockUI(n) : e(t).unblock(n)
					}, n.timeout);
					e(t).data("blockUI.timeout", W)
				}
			}
		}

		function o(t, o) {
			var s, l = t == window,
				d = e(t),
				a = d.data("blockUI.history"),
				c = d.data("blockUI.timeout");
			c && (clearTimeout(c), d.removeData("blockUI.timeout")), o = e.extend({}, e.blockUI.defaults, o || {}), i(0, t, o), null === o.onUnblock && (o.onUnblock = d.data("blockUI.onUnblock"), d.removeData("blockUI.onUnblock"));
			var r;
			r = l ? e(document.body).children().filter(".blockUI").add("body > .blockUI") : d.find(">.blockUI"), o.cursorReset && (r.length > 1 && (r[1].style.cursor = o.cursorReset), r.length > 2 && (r[2].style.cursor = o.cursorReset)), l && (p = b = null), o.fadeOut ? (s = r.length, r.stop().fadeOut(o.fadeOut, function () {
				0 == --s && n(r, a, o, t)
			})) : n(r, a, o, t)
		}

		function n(t, o, n, i) {
			var s = e(i);
			if (!s.data("blockUI.isBlocked")) {
				t.each(function (e, t) {
					this.parentNode && this.parentNode.removeChild(this)
				}), o && o.el && (o.el.style.display = o.display, o.el.style.position = o.position, o.el.style.cursor = "default", o.parent && o.parent.appendChild(o.el), s.removeData("blockUI.history")), s.data("blockUI.static") && s.css("position", "static"), "function" == typeof n.onUnblock && n.onUnblock(i, n);
				var l = e(document.body),
					d = l.width(),
					a = l[0].style.width;
				l.width(d - 1).width(d), l[0].style.width = a
			}
		}

		function i(t, o, n) {
			var i = o == window,
				l = e(o);
			if ((t || (!i || p) && (i || l.data("blockUI.isBlocked"))) && (l.data("blockUI.isBlocked", t), i && n.bindEvents && (!t || n.showOverlay))) {
				var d = "mousedown mouseup keydown keypress keyup touchstart touchend touchmove";
				t ? e(document).bind(d, n, s) : e(document).unbind(d, s)
			}
		}

		function s(t) {
			if ("keydown" === t.type && t.keyCode && 9 == t.keyCode && p && t.data.constrainTabKey) {
				var o = b,
					n = !t.shiftKey && t.target === o[o.length - 1],
					i = t.shiftKey && t.target === o[0];
				if (n || i) return setTimeout(function () {
					l(i)
				}, 10), !1
			}
			var s = t.data,
				d = e(t.target);
			return d.hasClass("blockOverlay") && s.onOverlayClick && s.onOverlayClick(t), d.parents("div." + s.blockMsgClass).length > 0 || 0 === d.parents().children().filter("div.blockUI").length
		}

		function l(e) {
			if (b) {
				var t = b[!0 === e ? b.length - 1 : 0];
				t && t.focus()
			}
		}

		function d(e, t, o) {
			var n = e.parentNode,
				i = e.style,
				s = (n.offsetWidth - e.offsetWidth) / 2 - a(n, "borderLeftWidth"),
				l = (n.offsetHeight - e.offsetHeight) / 2 - a(n, "borderTopWidth");
			t && (i.left = s > 0 ? s + "px" : "0"), o && (i.top = l > 0 ? l + "px" : "0")
		}

		function a(t, o) {
			return parseInt(e.css(t, o), 10) || 0
		}
		e.fn._fadeIn = e.fn.fadeIn;
		var c = e.noop || function () {},
			r = /MSIE/.test(navigator.userAgent),
			u = /MSIE 6.0/.test(navigator.userAgent) && !/MSIE 8.0/.test(navigator.userAgent),
			f = (document.documentMode, e.isFunction(document.createElement("div").style.setExpression));
		e.blockUI = function (e) {
			t(window, e)
		}, e.unblockUI = function (e) {
			o(window, e)
		}, e.growlUI = function (t, o, n, i) {
			var s = e('<div class="growlUI"></div>');
			t && s.append("<h1>" + t + "</h1>"), o && s.append("<h2>" + o + "</h2>"), n === undefined && (n = 3e3);
			var l = function (t) {
				t = t || {}, e.blockUI({
					message: s,
					fadeIn: "undefined" != typeof t.fadeIn ? t.fadeIn : 700,
					fadeOut: "undefined" != typeof t.fadeOut ? t.fadeOut : 1e3,
					timeout: "undefined" != typeof t.timeout ? t.timeout : n,
					centerY: !1,
					showOverlay: !1,
					onUnblock: i,
					css: e.blockUI.defaults.growlCSS
				})
			};
			l();
			s.css("opacity");
			s.mouseover(function () {
				l({
					fadeIn: 0,
					timeout: 3e4
				});
				var t = e(".blockMsg");
				t.stop(), t.fadeTo(300, 1)
			}).mouseout(function () {
				e(".blockMsg").fadeOut(1e3)
			})
		}, e.fn.block = function (o) {
			if (this[0] === window) return e.blockUI(o), this;
			var n = e.extend({}, e.blockUI.defaults, o || {});
			return this.each(function () {
				var t = e(this);
				n.ignoreIfBlocked && t.data("blockUI.isBlocked") || t.unblock({
					fadeOut: 0
				})
			}), this.each(function () {
				"static" == e.css(this, "position") && (this.style.position = "relative", e(this).data("blockUI.static", !0)), this.style.zoom = 1, t(this, o)
			})
		}, e.fn.unblock = function (t) {
			return this[0] === window ? (e.unblockUI(t), this) : this.each(function () {
				o(this, t)
			})
		}, e.blockUI.version = 2.7, e.blockUI.defaults = {
			message: "<h1>Please wait...</h1>",
			title: null,
			draggable: !0,
			theme: !1,
			css: {
				padding: 0,
				margin: 0,
				width: "30%",
				top: "40%",
				left: "35%",
				textAlign: "center",
				color: "#000",
				border: "3px solid #aaa",
				backgroundColor: "#fff",
				cursor: "wait"
			},
			themedCSS: {
				width: "30%",
				top: "40%",
				left: "35%"
			},
			overlayCSS: {
				backgroundColor: "#000",
				opacity: .6,
				cursor: "wait"
			},
			cursorReset: "default",
			growlCSS: {
				width: "350px",
				top: "10px",
				left: "",
				right: "10px",
				border: "none",
				padding: "5px",
				opacity: .6,
				cursor: "default",
				color: "#fff",
				backgroundColor: "#000",
				"-webkit-border-radius": "10px",
				"-moz-border-radius": "10px",
				"border-radius": "10px"
			},
			iframeSrc: /^https/i.test(window.location.href || "") ? "javascript:false" : "about:blank",
			forceIframe: !1,
			baseZ: 1e3,
			centerX: !0,
			centerY: !0,
			allowBodyStretch: !0,
			bindEvents: !0,
			constrainTabKey: !0,
			fadeIn: 200,
			fadeOut: 400,
			timeout: 0,
			showOverlay: !0,
			focusInput: !0,
			focusableElements: ":input:enabled:visible",
			onBlock: null,
			onUnblock: null,
			onOverlayClick: null,
			quirksmodeOffsetHack: 4,
			blockMsgClass: "blockMsg",
			ignoreIfBlocked: !1
		};
		var p = null,
			b = []
	}
	"function" == typeof define && define.amd && define.amd.jQuery ? define(["jquery"], e) : e(jQuery)
}();
jQuery(function (e) {
	if ("undefined" == typeof wc_add_to_cart_params) return !1;
	var t = function () {
		e(document.body).on("click", ".add_to_cart_button", this.onAddToCart).on("click", ".remove_from_cart_button", this.onRemoveFromCart).on("added_to_cart", this.updateButton).on("added_to_cart", this.updateCartPage).on("added_to_cart removed_from_cart", this.updateFragments)
	};
	t.prototype.onAddToCart = function (t) {
		var a = e(this);
		if (a.is(".ajax_add_to_cart")) {
			if (!a.attr("data-product_id")) return !0;
			t.preventDefault(), a.removeClass("added"), a.addClass("loading");
			var o = {};
			e.each(a.data(), function (t, a) {
				o[t] = a
			}), e(document.body).trigger("adding_to_cart", [a, o]), e.post(wc_add_to_cart_params.wc_ajax_url.toString().replace("%%endpoint%%", "add_to_cart"), o, function (t) {
				t && (t.error && t.product_url ? window.location = t.product_url : "yes" !== wc_add_to_cart_params.cart_redirect_after_add ? e(document.body).trigger("added_to_cart", [t.fragments, t.cart_hash, a]) : window.location = wc_add_to_cart_params.cart_url)
			})
		}
	}, t.prototype.onRemoveFromCart = function (t) {
		var a = e(this),
			o = a.closest(".woocommerce-mini-cart-item");
		t.preventDefault(), o.block({
			message: null,
			overlayCSS: {
				opacity: .6
			}
		}), e.post(wc_add_to_cart_params.wc_ajax_url.toString().replace("%%endpoint%%", "remove_from_cart"), {
			cart_item_key: a.data("cart_item_key")
		}, function (t) {
			t && t.fragments ? e(document.body).trigger("removed_from_cart", [t.fragments, t.cart_hash, a]) : window.location = a.attr("href")
		}).fail(function () {
			window.location = a.attr("href")
		})
	}, t.prototype.updateButton = function (t, a, o, r) {
		(r = void 0 !== r && r) && (r.removeClass("loading"), r.addClass("added"), wc_add_to_cart_params.is_cart || 0 !== r.parent().find(".added_to_cart").length || r.after(' <a href="' + wc_add_to_cart_params.cart_url + '" class="added_to_cart wc-forward" title="' + wc_add_to_cart_params.i18n_view_cart + '">' + wc_add_to_cart_params.i18n_view_cart + "</a>"), e(document.body).trigger("wc_cart_button_updated", [r]))
	}, t.prototype.updateCartPage = function () {
		var t = window.location.toString().replace("add-to-cart", "added-to-cart");
		e(".shop_table.cart").load(t + " .shop_table.cart:eq(0) > *", function () {
			e(".shop_table.cart").stop(!0).css("opacity", "1").unblock(), e(document.body).trigger("cart_page_refreshed")
		}), e(".cart_totals").load(t + " .cart_totals:eq(0) > *", function () {
			e(".cart_totals").stop(!0).css("opacity", "1").unblock(), e(document.body).trigger("cart_totals_refreshed")
		})
	}, t.prototype.updateFragments = function (t, a) {
		a && (e.each(a, function (t) {
			e(t).addClass("updating").fadeTo("400", "0.6").block({
				message: null,
				overlayCSS: {
					opacity: .6
				}
			})
		}), e.each(a, function (t, a) {
			e(t).replaceWith(a), e(t).stop(!0).css("opacity", "1").unblock()
		}), e(document.body).trigger("wc_fragments_loaded"))
	}, new t
});
/*!
 * JavaScript Cookie v2.1.4
 * https://github.com/js-cookie/js-cookie
 *
 * Copyright 2006, 2015 Klaus Hartl & Fagner Brack
 * Released under the MIT license
 */
! function (e) {
	var n = !1;
	if ("function" == typeof define && define.amd && (define(e), n = !0), "object" == typeof exports && (module.exports = e(), n = !0), !n) {
		var o = window.Cookies,
			t = window.Cookies = e();
		t.noConflict = function () {
			return window.Cookies = o, t
		}
	}
}(function () {
	function e() {
		for (var e = 0, n = {}; e < arguments.length; e++) {
			var o = arguments[e];
			for (var t in o) n[t] = o[t]
		}
		return n
	}

	function n(o) {
		function t(n, r, i) {
			var c;
			if ("undefined" != typeof document) {
				if (arguments.length > 1) {
					if ("number" == typeof (i = e({
							path: "/"
						}, t.defaults, i)).expires) {
						var a = new Date;
						a.setMilliseconds(a.getMilliseconds() + 864e5 * i.expires), i.expires = a
					}
					i.expires = i.expires ? i.expires.toUTCString() : "";
					try {
						c = JSON.stringify(r), /^[\{\[]/.test(c) && (r = c)
					} catch (m) {}
					r = o.write ? o.write(r, n) : encodeURIComponent(String(r)).replace(/%(23|24|26|2B|3A|3C|3E|3D|2F|3F|40|5B|5D|5E|60|7B|7D|7C)/g, decodeURIComponent), n = (n = (n = encodeURIComponent(String(n))).replace(/%(23|24|26|2B|5E|60|7C)/g, decodeURIComponent)).replace(/[\(\)]/g, escape);
					var f = "";
					for (var s in i) i[s] && (f += "; " + s, !0 !== i[s] && (f += "=" + i[s]));
					return document.cookie = n + "=" + r + f
				}
				n || (c = {});
				for (var p = document.cookie ? document.cookie.split("; ") : [], d = /(%[0-9A-Z]{2})+/g, u = 0; u < p.length; u++) {
					var l = p[u].split("="),
						C = l.slice(1).join("=");
					'"' === C.charAt(0) && (C = C.slice(1, -1));
					try {
						var g = l[0].replace(d, decodeURIComponent);
						if (C = o.read ? o.read(C, g) : o(C, g) || C.replace(d, decodeURIComponent), this.json) try {
							C = JSON.parse(C)
						} catch (m) {}
						if (n === g) {
							c = C;
							break
						}
						n || (c[g] = C)
					} catch (m) {}
				}
				return c
			}
		}
		return t.set = t, t.get = function (e) {
			return t.call(t, e)
		}, t.getJSON = function () {
			return t.apply({
				json: !0
			}, [].slice.call(arguments))
		}, t.defaults = {}, t.remove = function (n, o) {
			t(n, "", e(o, {
				expires: -1
			}))
		}, t.withConverter = n, t
	}
	return n(function () {})
});
jQuery(function (i) {
	i(".woocommerce-ordering").on("change", "select.orderby", function () {
		i(this).closest("form").submit()
	}), i("input.qty:not(.product-quantity input.qty)").each(function () {
		var o = parseFloat(i(this).attr("min"));
		0 <= o && parseFloat(i(this).val()) < o && i(this).val(o)
	}), i(".woocommerce-store-notice__dismiss-link").click(function () {
		Cookies.set("store_notice", "hidden", {
			path: "/"
		}), i(".woocommerce-store-notice").hide()
	}), "hidden" === Cookies.get("store_notice") ? i(".woocommerce-store-notice").hide() : i(".woocommerce-store-notice").show(), i(document.body).on("click", function () {
		i(".woocommerce-input-wrapper span.description:visible").prop("aria-hidden", !0).slideUp(250)
	}), i(".woocommerce-input-wrapper").on("click", function (o) {
		o.stopPropagation()
	}), i(".woocommerce-input-wrapper :input").on("keydown", function (o) {
		var e = i(this).parent().find("span.description");
		if (27 === o.which && e.length && e.is(":visible")) return e.prop("aria-hidden", !0).slideUp(250), o.preventDefault(), !1
	}).on("click focus", function () {
		var o = i(this).parent(),
			e = o.find("span.description");
		o.addClass("currentTarget"), i(".woocommerce-input-wrapper:not(.currentTarget) span.description:visible").prop("aria-hidden", !0).slideUp(250), e.length && e.is(":hidden") && e.prop("aria-hidden", !1).slideDown(250), o.removeClass("currentTarget")
	}), i.scroll_to_notices = function (o) {
		o.length && i("html, body").animate({
			scrollTop: o.offset().top - 100
		}, 1e3)
	}
});
jQuery(function (n) {
	if ("undefined" == typeof wc_cart_fragments_params) return !1;
	var t = !0,
		o = wc_cart_fragments_params.cart_hash_key;
	try {
		t = "sessionStorage" in window && null !== window.sessionStorage, window.sessionStorage.setItem("wc", "test"), window.sessionStorage.removeItem("wc"), window.localStorage.setItem("wc", "test"), window.localStorage.removeItem("wc")
	} catch (w) {
		t = !1
	}

	function a() {
		t && sessionStorage.setItem("wc_cart_created", (new Date).getTime())
	}

	function s(e) {
		t && (localStorage.setItem(o, e), sessionStorage.setItem(o, e))
	}
	var e = {
		url: wc_cart_fragments_params.wc_ajax_url.toString().replace("%%endpoint%%", "get_refreshed_fragments"),
		type: "POST",
		success: function (e) {
			e && e.fragments && (n.each(e.fragments, function (e, t) {
				n(e).replaceWith(t)
			}), t && (sessionStorage.setItem(wc_cart_fragments_params.fragment_name, JSON.stringify(e.fragments)), s(e.cart_hash), e.cart_hash && a()), n(document.body).trigger("wc_fragments_refreshed"))
		}
	};

	function r() {
		n.ajax(e)
	}
	if (t) {
		var i = null;
		n(document.body).on("wc_fragment_refresh updated_wc_div", function () {
			r()
		}), n(document.body).on("added_to_cart removed_from_cart", function (e, t, n) {
			var r = sessionStorage.getItem(o);
			null !== r && r !== undefined && "" !== r || a(), sessionStorage.setItem(wc_cart_fragments_params.fragment_name, JSON.stringify(t)), s(n)
		}), n(document.body).on("wc_fragments_refreshed", function () {
			clearTimeout(i), i = setTimeout(r, 864e5)
		}), n(window).on("storage onstorage", function (e) {
			o === e.originalEvent.key && localStorage.getItem(o) !== sessionStorage.getItem(o) && r()
		}), n(window).on("pageshow", function (e) {
			e.originalEvent.persisted && (n(".widget_shopping_cart_content").empty(), n(document.body).trigger("wc_fragment_refresh"))
		});
		try {
			var c = n.parseJSON(sessionStorage.getItem(wc_cart_fragments_params.fragment_name)),
				_ = sessionStorage.getItem(o),
				g = Cookies.get("woocommerce_cart_hash"),
				m = sessionStorage.getItem("wc_cart_created");
			if (null !== _ && _ !== undefined && "" !== _ || (_ = ""), null !== g && g !== undefined && "" !== g || (g = ""), _ && (null === m || m === undefined || "" === m)) throw "No cart_created";
			if (m) {
				var d = 1 * m + 864e5,
					f = (new Date).getTime();
				if (d < f) throw "Fragment expired";
				i = setTimeout(r, d - f)
			}
			if (!c || !c["div.widget_shopping_cart_content"] || _ !== g) throw "No fragment";
			n.each(c, function (e, t) {
				n(e).replaceWith(t)
			}), n(document.body).trigger("wc_fragments_loaded")
		} catch (w) {
			r()
		}
	} else r();
	0 < Cookies.get("woocommerce_items_in_cart") ? n(".hide_cart_widget_if_empty").closest(".widget_shopping_cart").show() : n(".hide_cart_widget_if_empty").closest(".widget_shopping_cart").hide(), n(document.body).on("adding_to_cart", function () {
		n(".hide_cart_widget_if_empty").closest(".widget_shopping_cart").show()
	})
});
/*!
 * jQuery UI Core 1.11.4
 * http://jqueryui.com
 *
 * Copyright jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/category/ui-core/
 */
! function (a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : a(jQuery)
}(function (a) {
	function b(b, d) {
		var e, f, g, h = b.nodeName.toLowerCase();
		return "area" === h ? (e = b.parentNode, f = e.name, !(!b.href || !f || "map" !== e.nodeName.toLowerCase()) && (g = a("img[usemap='#" + f + "']")[0], !!g && c(g))) : (/^(input|select|textarea|button|object)$/.test(h) ? !b.disabled : "a" === h ? b.href || d : d) && c(b)
	}

	function c(b) {
		return a.expr.filters.visible(b) && !a(b).parents().addBack().filter(function () {
			return "hidden" === a.css(this, "visibility")
		}).length
	}
	a.ui = a.ui || {}, a.extend(a.ui, {
		version: "1.11.4",
		keyCode: {
			BACKSPACE: 8,
			COMMA: 188,
			DELETE: 46,
			DOWN: 40,
			END: 35,
			ENTER: 13,
			ESCAPE: 27,
			HOME: 36,
			LEFT: 37,
			PAGE_DOWN: 34,
			PAGE_UP: 33,
			PERIOD: 190,
			RIGHT: 39,
			SPACE: 32,
			TAB: 9,
			UP: 38
		}
	}), a.fn.extend({
		scrollParent: function (b) {
			var c = this.css("position"),
				d = "absolute" === c,
				e = b ? /(auto|scroll|hidden)/ : /(auto|scroll)/,
				f = this.parents().filter(function () {
					var b = a(this);
					return (!d || "static" !== b.css("position")) && e.test(b.css("overflow") + b.css("overflow-y") + b.css("overflow-x"))
				}).eq(0);
			return "fixed" !== c && f.length ? f : a(this[0].ownerDocument || document)
		},
		uniqueId: function () {
			var a = 0;
			return function () {
				return this.each(function () {
					this.id || (this.id = "ui-id-" + ++a)
				})
			}
		}(),
		removeUniqueId: function () {
			return this.each(function () {
				/^ui-id-\d+$/.test(this.id) && a(this).removeAttr("id")
			})
		}
	}), a.extend(a.expr[":"], {
		data: a.expr.createPseudo ? a.expr.createPseudo(function (b) {
			return function (c) {
				return !!a.data(c, b)
			}
		}) : function (b, c, d) {
			return !!a.data(b, d[3])
		},
		focusable: function (c) {
			return b(c, !isNaN(a.attr(c, "tabindex")))
		},
		tabbable: function (c) {
			var d = a.attr(c, "tabindex"),
				e = isNaN(d);
			return (e || d >= 0) && b(c, !e)
		}
	}), a("<a>").outerWidth(1).jquery || a.each(["Width", "Height"], function (b, c) {
		function d(b, c, d, f) {
			return a.each(e, function () {
				c -= parseFloat(a.css(b, "padding" + this)) || 0, d && (c -= parseFloat(a.css(b, "border" + this + "Width")) || 0), f && (c -= parseFloat(a.css(b, "margin" + this)) || 0)
			}), c
		}
		var e = "Width" === c ? ["Left", "Right"] : ["Top", "Bottom"],
			f = c.toLowerCase(),
			g = {
				innerWidth: a.fn.innerWidth,
				innerHeight: a.fn.innerHeight,
				outerWidth: a.fn.outerWidth,
				outerHeight: a.fn.outerHeight
			};
		a.fn["inner" + c] = function (b) {
			return void 0 === b ? g["inner" + c].call(this) : this.each(function () {
				a(this).css(f, d(this, b) + "px")
			})
		}, a.fn["outer" + c] = function (b, e) {
			return "number" != typeof b ? g["outer" + c].call(this, b) : this.each(function () {
				a(this).css(f, d(this, b, !0, e) + "px")
			})
		}
	}), a.fn.addBack || (a.fn.addBack = function (a) {
		return this.add(null == a ? this.prevObject : this.prevObject.filter(a))
	}), a("<a>").data("a-b", "a").removeData("a-b").data("a-b") && (a.fn.removeData = function (b) {
		return function (c) {
			return arguments.length ? b.call(this, a.camelCase(c)) : b.call(this)
		}
	}(a.fn.removeData)), a.ui.ie = !!/msie [\w.]+/.exec(navigator.userAgent.toLowerCase()), a.fn.extend({
		focus: function (b) {
			return function (c, d) {
				return "number" == typeof c ? this.each(function () {
					var b = this;
					setTimeout(function () {
						a(b).focus(), d && d.call(b)
					}, c)
				}) : b.apply(this, arguments)
			}
		}(a.fn.focus),
		disableSelection: function () {
			var a = "onselectstart" in document.createElement("div") ? "selectstart" : "mousedown";
			return function () {
				return this.bind(a + ".ui-disableSelection", function (a) {
					a.preventDefault()
				})
			}
		}(),
		enableSelection: function () {
			return this.unbind(".ui-disableSelection")
		},
		zIndex: function (b) {
			if (void 0 !== b) return this.css("zIndex", b);
			if (this.length)
				for (var c, d, e = a(this[0]); e.length && e[0] !== document;) {
					if (c = e.css("position"), ("absolute" === c || "relative" === c || "fixed" === c) && (d = parseInt(e.css("zIndex"), 10), !isNaN(d) && 0 !== d)) return d;
					e = e.parent()
				}
			return 0
		}
	}), a.ui.plugin = {
		add: function (b, c, d) {
			var e, f = a.ui[b].prototype;
			for (e in d) f.plugins[e] = f.plugins[e] || [], f.plugins[e].push([c, d[e]])
		},
		call: function (a, b, c, d) {
			var e, f = a.plugins[b];
			if (f && (d || a.element[0].parentNode && 11 !== a.element[0].parentNode.nodeType))
				for (e = 0; e < f.length; e++) a.options[f[e][0]] && f[e][1].apply(a.element, c)
		}
	}
});
window.wp = window.wp || {},
	function (a) {
		var b = "undefined" == typeof _wpUtilSettings ? {} : _wpUtilSettings;
		wp.template = _.memoize(function (b) {
			var c, d = {
				evaluate: /<#([\s\S]+?)#>/g,
				interpolate: /\{\{\{([\s\S]+?)\}\}\}/g,
				escape: /\{\{([^\}]+?)\}\}(?!\})/g,
				variable: "data"
			};
			return function (e) {
				return (c = c || _.template(a("#tmpl-" + b).html(), d))(e)
			}
		}), wp.ajax = {
			settings: b.ajax || {},
			post: function (a, b) {
				return wp.ajax.send({
					data: _.isObject(a) ? a : _.extend(b || {}, {
						action: a
					})
				})
			},
			send: function (b, c) {
				var d, e;
				return _.isObject(b) ? c = b : (c = c || {}, c.data = _.extend(c.data || {}, {
					action: b
				})), c = _.defaults(c || {}, {
					type: "POST",
					url: wp.ajax.settings.url,
					context: this
				}), e = a.Deferred(function (b) {
					c.success && b.done(c.success), c.error && b.fail(c.error), delete c.success, delete c.error, b.jqXHR = a.ajax(c).done(function (a) {
						"1" !== a && 1 !== a || (a = {
							success: !0
						}), _.isObject(a) && !_.isUndefined(a.success) ? b[a.success ? "resolveWith" : "rejectWith"](this, [a.data]) : b.rejectWith(this, [a])
					}).fail(function () {
						b.rejectWith(this, arguments)
					})
				}), d = e.promise(), d.abort = function () {
					return e.jqXHR.abort(), this
				}, d
			}
		}
	}(jQuery);
(function (t) {
	var e = typeof self == "object" && self.self === self && self || typeof global == "object" && global.global === global && global;
	if (typeof define === "function" && define.amd) {
		define(["underscore", "jquery", "exports"], function (i, r, n) {
			e.Backbone = t(e, n, i, r)
		})
	} else if (typeof exports !== "undefined") {
		var i = require("underscore"),
			r;
		try {
			r = require("jquery")
		} catch (n) {}
		t(e, exports, i, r)
	} else {
		e.Backbone = t(e, {}, e._, e.jQuery || e.Zepto || e.ender || e.$)
	}
})(function (t, e, i, r) {
	var n = t.Backbone;
	var s = Array.prototype.slice;
	e.VERSION = "1.3.3";
	e.$ = r;
	e.noConflict = function () {
		t.Backbone = n;
		return this
	};
	e.emulateHTTP = false;
	e.emulateJSON = false;
	var a = function (t, e, r) {
		switch (t) {
			case 1:
				return function () {
					return i[e](this[r])
				};
			case 2:
				return function (t) {
					return i[e](this[r], t)
				};
			case 3:
				return function (t, n) {
					return i[e](this[r], o(t, this), n)
				};
			case 4:
				return function (t, n, s) {
					return i[e](this[r], o(t, this), n, s)
				};
			default:
				return function () {
					var t = s.call(arguments);
					t.unshift(this[r]);
					return i[e].apply(i, t)
				}
		}
	};
	var h = function (t, e, r) {
		i.each(e, function (e, n) {
			if (i[n]) t.prototype[n] = a(e, n, r)
		})
	};
	var o = function (t, e) {
		if (i.isFunction(t)) return t;
		if (i.isObject(t) && !e._isModel(t)) return l(t);
		if (i.isString(t)) return function (e) {
			return e.get(t)
		};
		return t
	};
	var l = function (t) {
		var e = i.matches(t);
		return function (t) {
			return e(t.attributes)
		}
	};
	var u = e.Events = {};
	var c = /\s+/;
	var f = function (t, e, r, n, s) {
		var a = 0,
			h;
		if (r && typeof r === "object") {
			if (n !== void 0 && "context" in s && s.context === void 0) s.context = n;
			for (h = i.keys(r); a < h.length; a++) {
				e = f(t, e, h[a], r[h[a]], s)
			}
		} else if (r && c.test(r)) {
			for (h = r.split(c); a < h.length; a++) {
				e = t(e, h[a], n, s)
			}
		} else {
			e = t(e, r, n, s)
		}
		return e
	};
	u.on = function (t, e, i) {
		return d(this, t, e, i)
	};
	var d = function (t, e, i, r, n) {
		t._events = f(v, t._events || {}, e, i, {
			context: r,
			ctx: t,
			listening: n
		});
		if (n) {
			var s = t._listeners || (t._listeners = {});
			s[n.id] = n
		}
		return t
	};
	u.listenTo = function (t, e, r) {
		if (!t) return this;
		var n = t._listenId || (t._listenId = i.uniqueId("l"));
		var s = this._listeningTo || (this._listeningTo = {});
		var a = s[n];
		if (!a) {
			var h = this._listenId || (this._listenId = i.uniqueId("l"));
			a = s[n] = {
				obj: t,
				objId: n,
				id: h,
				listeningTo: s,
				count: 0
			}
		}
		d(t, e, r, this, a);
		return this
	};
	var v = function (t, e, i, r) {
		if (i) {
			var n = t[e] || (t[e] = []);
			var s = r.context,
				a = r.ctx,
				h = r.listening;
			if (h) h.count++;
			n.push({
				callback: i,
				context: s,
				ctx: s || a,
				listening: h
			})
		}
		return t
	};
	u.off = function (t, e, i) {
		if (!this._events) return this;
		this._events = f(g, this._events, t, e, {
			context: i,
			listeners: this._listeners
		});
		return this
	};
	u.stopListening = function (t, e, r) {
		var n = this._listeningTo;
		if (!n) return this;
		var s = t ? [t._listenId] : i.keys(n);
		for (var a = 0; a < s.length; a++) {
			var h = n[s[a]];
			if (!h) break;
			h.obj.off(e, r, this)
		}
		return this
	};
	var g = function (t, e, r, n) {
		if (!t) return;
		var s = 0,
			a;
		var h = n.context,
			o = n.listeners;
		if (!e && !r && !h) {
			var l = i.keys(o);
			for (; s < l.length; s++) {
				a = o[l[s]];
				delete o[a.id];
				delete a.listeningTo[a.objId]
			}
			return
		}
		var u = e ? [e] : i.keys(t);
		for (; s < u.length; s++) {
			e = u[s];
			var c = t[e];
			if (!c) break;
			var f = [];
			for (var d = 0; d < c.length; d++) {
				var v = c[d];
				if (r && r !== v.callback && r !== v.callback._callback || h && h !== v.context) {
					f.push(v)
				} else {
					a = v.listening;
					if (a && --a.count === 0) {
						delete o[a.id];
						delete a.listeningTo[a.objId]
					}
				}
			}
			if (f.length) {
				t[e] = f
			} else {
				delete t[e]
			}
		}
		return t
	};
	u.once = function (t, e, r) {
		var n = f(p, {}, t, e, i.bind(this.off, this));
		if (typeof t === "string" && r == null) e = void 0;
		return this.on(n, e, r)
	};
	u.listenToOnce = function (t, e, r) {
		var n = f(p, {}, e, r, i.bind(this.stopListening, this, t));
		return this.listenTo(t, n)
	};
	var p = function (t, e, r, n) {
		if (r) {
			var s = t[e] = i.once(function () {
				n(e, s);
				r.apply(this, arguments)
			});
			s._callback = r
		}
		return t
	};
	u.trigger = function (t) {
		if (!this._events) return this;
		var e = Math.max(0, arguments.length - 1);
		var i = Array(e);
		for (var r = 0; r < e; r++) i[r] = arguments[r + 1];
		f(m, this._events, t, void 0, i);
		return this
	};
	var m = function (t, e, i, r) {
		if (t) {
			var n = t[e];
			var s = t.all;
			if (n && s) s = s.slice();
			if (n) _(n, r);
			if (s) _(s, [e].concat(r))
		}
		return t
	};
	var _ = function (t, e) {
		var i, r = -1,
			n = t.length,
			s = e[0],
			a = e[1],
			h = e[2];
		switch (e.length) {
			case 0:
				while (++r < n)(i = t[r]).callback.call(i.ctx);
				return;
			case 1:
				while (++r < n)(i = t[r]).callback.call(i.ctx, s);
				return;
			case 2:
				while (++r < n)(i = t[r]).callback.call(i.ctx, s, a);
				return;
			case 3:
				while (++r < n)(i = t[r]).callback.call(i.ctx, s, a, h);
				return;
			default:
				while (++r < n)(i = t[r]).callback.apply(i.ctx, e);
				return
		}
	};
	u.bind = u.on;
	u.unbind = u.off;
	i.extend(e, u);
	var y = e.Model = function (t, e) {
		var r = t || {};
		e || (e = {});
		this.cid = i.uniqueId(this.cidPrefix);
		this.attributes = {};
		if (e.collection) this.collection = e.collection;
		if (e.parse) r = this.parse(r, e) || {};
		var n = i.result(this, "defaults");
		r = i.defaults(i.extend({}, n, r), n);
		this.set(r, e);
		this.changed = {};
		this.initialize.apply(this, arguments)
	};
	i.extend(y.prototype, u, {
		changed: null,
		validationError: null,
		idAttribute: "id",
		cidPrefix: "c",
		initialize: function () {},
		toJSON: function (t) {
			return i.clone(this.attributes)
		},
		sync: function () {
			return e.sync.apply(this, arguments)
		},
		get: function (t) {
			return this.attributes[t]
		},
		escape: function (t) {
			return i.escape(this.get(t))
		},
		has: function (t) {
			return this.get(t) != null
		},
		matches: function (t) {
			return !!i.iteratee(t, this)(this.attributes)
		},
		set: function (t, e, r) {
			if (t == null) return this;
			var n;
			if (typeof t === "object") {
				n = t;
				r = e
			} else {
				(n = {})[t] = e
			}
			r || (r = {});
			if (!this._validate(n, r)) return false;
			var s = r.unset;
			var a = r.silent;
			var h = [];
			var o = this._changing;
			this._changing = true;
			if (!o) {
				this._previousAttributes = i.clone(this.attributes);
				this.changed = {}
			}
			var l = this.attributes;
			var u = this.changed;
			var c = this._previousAttributes;
			for (var f in n) {
				e = n[f];
				if (!i.isEqual(l[f], e)) h.push(f);
				if (!i.isEqual(c[f], e)) {
					u[f] = e
				} else {
					delete u[f]
				}
				s ? delete l[f] : l[f] = e
			}
			if (this.idAttribute in n) this.id = this.get(this.idAttribute);
			if (!a) {
				if (h.length) this._pending = r;
				for (var d = 0; d < h.length; d++) {
					this.trigger("change:" + h[d], this, l[h[d]], r)
				}
			}
			if (o) return this;
			if (!a) {
				while (this._pending) {
					r = this._pending;
					this._pending = false;
					this.trigger("change", this, r)
				}
			}
			this._pending = false;
			this._changing = false;
			return this
		},
		unset: function (t, e) {
			return this.set(t, void 0, i.extend({}, e, {
				unset: true
			}))
		},
		clear: function (t) {
			var e = {};
			for (var r in this.attributes) e[r] = void 0;
			return this.set(e, i.extend({}, t, {
				unset: true
			}))
		},
		hasChanged: function (t) {
			if (t == null) return !i.isEmpty(this.changed);
			return i.has(this.changed, t)
		},
		changedAttributes: function (t) {
			if (!t) return this.hasChanged() ? i.clone(this.changed) : false;
			var e = this._changing ? this._previousAttributes : this.attributes;
			var r = {};
			for (var n in t) {
				var s = t[n];
				if (i.isEqual(e[n], s)) continue;
				r[n] = s
			}
			return i.size(r) ? r : false
		},
		previous: function (t) {
			if (t == null || !this._previousAttributes) return null;
			return this._previousAttributes[t]
		},
		previousAttributes: function () {
			return i.clone(this._previousAttributes)
		},
		fetch: function (t) {
			t = i.extend({
				parse: true
			}, t);
			var e = this;
			var r = t.success;
			t.success = function (i) {
				var n = t.parse ? e.parse(i, t) : i;
				if (!e.set(n, t)) return false;
				if (r) r.call(t.context, e, i, t);
				e.trigger("sync", e, i, t)
			};
			B(this, t);
			return this.sync("read", this, t)
		},
		save: function (t, e, r) {
			var n;
			if (t == null || typeof t === "object") {
				n = t;
				r = e
			} else {
				(n = {})[t] = e
			}
			r = i.extend({
				validate: true,
				parse: true
			}, r);
			var s = r.wait;
			if (n && !s) {
				if (!this.set(n, r)) return false
			} else if (!this._validate(n, r)) {
				return false
			}
			var a = this;
			var h = r.success;
			var o = this.attributes;
			r.success = function (t) {
				a.attributes = o;
				var e = r.parse ? a.parse(t, r) : t;
				if (s) e = i.extend({}, n, e);
				if (e && !a.set(e, r)) return false;
				if (h) h.call(r.context, a, t, r);
				a.trigger("sync", a, t, r)
			};
			B(this, r);
			if (n && s) this.attributes = i.extend({}, o, n);
			var l = this.isNew() ? "create" : r.patch ? "patch" : "update";
			if (l === "patch" && !r.attrs) r.attrs = n;
			var u = this.sync(l, this, r);
			this.attributes = o;
			return u
		},
		destroy: function (t) {
			t = t ? i.clone(t) : {};
			var e = this;
			var r = t.success;
			var n = t.wait;
			var s = function () {
				e.stopListening();
				e.trigger("destroy", e, e.collection, t)
			};
			t.success = function (i) {
				if (n) s();
				if (r) r.call(t.context, e, i, t);
				if (!e.isNew()) e.trigger("sync", e, i, t)
			};
			var a = false;
			if (this.isNew()) {
				i.defer(t.success)
			} else {
				B(this, t);
				a = this.sync("delete", this, t)
			}
			if (!n) s();
			return a
		},
		url: function () {
			var t = i.result(this, "urlRoot") || i.result(this.collection, "url") || F();
			if (this.isNew()) return t;
			var e = this.get(this.idAttribute);
			return t.replace(/[^\/]$/, "$&/") + encodeURIComponent(e)
		},
		parse: function (t, e) {
			return t
		},
		clone: function () {
			return new this.constructor(this.attributes)
		},
		isNew: function () {
			return !this.has(this.idAttribute)
		},
		isValid: function (t) {
			return this._validate({}, i.extend({}, t, {
				validate: true
			}))
		},
		_validate: function (t, e) {
			if (!e.validate || !this.validate) return true;
			t = i.extend({}, this.attributes, t);
			var r = this.validationError = this.validate(t, e) || null;
			if (!r) return true;
			this.trigger("invalid", this, r, i.extend(e, {
				validationError: r
			}));
			return false
		}
	});
	var b = {
		keys: 1,
		values: 1,
		pairs: 1,
		invert: 1,
		pick: 0,
		omit: 0,
		chain: 1,
		isEmpty: 1
	};
	h(y, b, "attributes");
	var x = e.Collection = function (t, e) {
		e || (e = {});
		if (e.model) this.model = e.model;
		if (e.comparator !== void 0) this.comparator = e.comparator;
		this._reset();
		this.initialize.apply(this, arguments);
		if (t) this.reset(t, i.extend({
			silent: true
		}, e))
	};
	var w = {
		add: true,
		remove: true,
		merge: true
	};
	var E = {
		add: true,
		remove: false
	};
	var I = function (t, e, i) {
		i = Math.min(Math.max(i, 0), t.length);
		var r = Array(t.length - i);
		var n = e.length;
		var s;
		for (s = 0; s < r.length; s++) r[s] = t[s + i];
		for (s = 0; s < n; s++) t[s + i] = e[s];
		for (s = 0; s < r.length; s++) t[s + n + i] = r[s]
	};
	i.extend(x.prototype, u, {
		model: y,
		initialize: function () {},
		toJSON: function (t) {
			return this.map(function (e) {
				return e.toJSON(t)
			})
		},
		sync: function () {
			return e.sync.apply(this, arguments)
		},
		add: function (t, e) {
			return this.set(t, i.extend({
				merge: false
			}, e, E))
		},
		remove: function (t, e) {
			e = i.extend({}, e);
			var r = !i.isArray(t);
			t = r ? [t] : t.slice();
			var n = this._removeModels(t, e);
			if (!e.silent && n.length) {
				e.changes = {
					added: [],
					merged: [],
					removed: n
				};
				this.trigger("update", this, e)
			}
			return r ? n[0] : n
		},
		set: function (t, e) {
			if (t == null) return;
			e = i.extend({}, w, e);
			if (e.parse && !this._isModel(t)) {
				t = this.parse(t, e) || []
			}
			var r = !i.isArray(t);
			t = r ? [t] : t.slice();
			var n = e.at;
			if (n != null) n = +n;
			if (n > this.length) n = this.length;
			if (n < 0) n += this.length + 1;
			var s = [];
			var a = [];
			var h = [];
			var o = [];
			var l = {};
			var u = e.add;
			var c = e.merge;
			var f = e.remove;
			var d = false;
			var v = this.comparator && n == null && e.sort !== false;
			var g = i.isString(this.comparator) ? this.comparator : null;
			var p, m;
			for (m = 0; m < t.length; m++) {
				p = t[m];
				var _ = this.get(p);
				if (_) {
					if (c && p !== _) {
						var y = this._isModel(p) ? p.attributes : p;
						if (e.parse) y = _.parse(y, e);
						_.set(y, e);
						h.push(_);
						if (v && !d) d = _.hasChanged(g)
					}
					if (!l[_.cid]) {
						l[_.cid] = true;
						s.push(_)
					}
					t[m] = _
				} else if (u) {
					p = t[m] = this._prepareModel(p, e);
					if (p) {
						a.push(p);
						this._addReference(p, e);
						l[p.cid] = true;
						s.push(p)
					}
				}
			}
			if (f) {
				for (m = 0; m < this.length; m++) {
					p = this.models[m];
					if (!l[p.cid]) o.push(p)
				}
				if (o.length) this._removeModels(o, e)
			}
			var b = false;
			var x = !v && u && f;
			if (s.length && x) {
				b = this.length !== s.length || i.some(this.models, function (t, e) {
					return t !== s[e]
				});
				this.models.length = 0;
				I(this.models, s, 0);
				this.length = this.models.length
			} else if (a.length) {
				if (v) d = true;
				I(this.models, a, n == null ? this.length : n);
				this.length = this.models.length
			}
			if (d) this.sort({
				silent: true
			});
			if (!e.silent) {
				for (m = 0; m < a.length; m++) {
					if (n != null) e.index = n + m;
					p = a[m];
					p.trigger("add", p, this, e)
				}
				if (d || b) this.trigger("sort", this, e);
				if (a.length || o.length || h.length) {
					e.changes = {
						added: a,
						removed: o,
						merged: h
					};
					this.trigger("update", this, e)
				}
			}
			return r ? t[0] : t
		},
		reset: function (t, e) {
			e = e ? i.clone(e) : {};
			for (var r = 0; r < this.models.length; r++) {
				this._removeReference(this.models[r], e)
			}
			e.previousModels = this.models;
			this._reset();
			t = this.add(t, i.extend({
				silent: true
			}, e));
			if (!e.silent) this.trigger("reset", this, e);
			return t
		},
		push: function (t, e) {
			return this.add(t, i.extend({
				at: this.length
			}, e))
		},
		pop: function (t) {
			var e = this.at(this.length - 1);
			return this.remove(e, t)
		},
		unshift: function (t, e) {
			return this.add(t, i.extend({
				at: 0
			}, e))
		},
		shift: function (t) {
			var e = this.at(0);
			return this.remove(e, t)
		},
		slice: function () {
			return s.apply(this.models, arguments)
		},
		get: function (t) {
			if (t == null) return void 0;
			return this._byId[t] || this._byId[this.modelId(t.attributes || t)] || t.cid && this._byId[t.cid]
		},
		has: function (t) {
			return this.get(t) != null
		},
		at: function (t) {
			if (t < 0) t += this.length;
			return this.models[t]
		},
		where: function (t, e) {
			return this[e ? "find" : "filter"](t)
		},
		findWhere: function (t) {
			return this.where(t, true)
		},
		sort: function (t) {
			var e = this.comparator;
			if (!e) throw new Error("Cannot sort a set without a comparator");
			t || (t = {});
			var r = e.length;
			if (i.isFunction(e)) e = i.bind(e, this);
			if (r === 1 || i.isString(e)) {
				this.models = this.sortBy(e)
			} else {
				this.models.sort(e)
			}
			if (!t.silent) this.trigger("sort", this, t);
			return this
		},
		pluck: function (t) {
			return this.map(t + "")
		},
		fetch: function (t) {
			t = i.extend({
				parse: true
			}, t);
			var e = t.success;
			var r = this;
			t.success = function (i) {
				var n = t.reset ? "reset" : "set";
				r[n](i, t);
				if (e) e.call(t.context, r, i, t);
				r.trigger("sync", r, i, t)
			};
			B(this, t);
			return this.sync("read", this, t)
		},
		create: function (t, e) {
			e = e ? i.clone(e) : {};
			var r = e.wait;
			t = this._prepareModel(t, e);
			if (!t) return false;
			if (!r) this.add(t, e);
			var n = this;
			var s = e.success;
			e.success = function (t, e, i) {
				if (r) n.add(t, i);
				if (s) s.call(i.context, t, e, i)
			};
			t.save(null, e);
			return t
		},
		parse: function (t, e) {
			return t
		},
		clone: function () {
			return new this.constructor(this.models, {
				model: this.model,
				comparator: this.comparator
			})
		},
		modelId: function (t) {
			return t[this.model.prototype.idAttribute || "id"]
		},
		_reset: function () {
			this.length = 0;
			this.models = [];
			this._byId = {}
		},
		_prepareModel: function (t, e) {
			if (this._isModel(t)) {
				if (!t.collection) t.collection = this;
				return t
			}
			e = e ? i.clone(e) : {};
			e.collection = this;
			var r = new this.model(t, e);
			if (!r.validationError) return r;
			this.trigger("invalid", this, r.validationError, e);
			return false
		},
		_removeModels: function (t, e) {
			var i = [];
			for (var r = 0; r < t.length; r++) {
				var n = this.get(t[r]);
				if (!n) continue;
				var s = this.indexOf(n);
				this.models.splice(s, 1);
				this.length--;
				delete this._byId[n.cid];
				var a = this.modelId(n.attributes);
				if (a != null) delete this._byId[a];
				if (!e.silent) {
					e.index = s;
					n.trigger("remove", n, this, e)
				}
				i.push(n);
				this._removeReference(n, e)
			}
			return i
		},
		_isModel: function (t) {
			return t instanceof y
		},
		_addReference: function (t, e) {
			this._byId[t.cid] = t;
			var i = this.modelId(t.attributes);
			if (i != null) this._byId[i] = t;
			t.on("all", this._onModelEvent, this)
		},
		_removeReference: function (t, e) {
			delete this._byId[t.cid];
			var i = this.modelId(t.attributes);
			if (i != null) delete this._byId[i];
			if (this === t.collection) delete t.collection;
			t.off("all", this._onModelEvent, this)
		},
		_onModelEvent: function (t, e, i, r) {
			if (e) {
				if ((t === "add" || t === "remove") && i !== this) return;
				if (t === "destroy") this.remove(e, r);
				if (t === "change") {
					var n = this.modelId(e.previousAttributes());
					var s = this.modelId(e.attributes);
					if (n !== s) {
						if (n != null) delete this._byId[n];
						if (s != null) this._byId[s] = e
					}
				}
			}
			this.trigger.apply(this, arguments)
		}
	});
	var S = {
		forEach: 3,
		each: 3,
		map: 3,
		collect: 3,
		reduce: 0,
		foldl: 0,
		inject: 0,
		reduceRight: 0,
		foldr: 0,
		find: 3,
		detect: 3,
		filter: 3,
		select: 3,
		reject: 3,
		every: 3,
		all: 3,
		some: 3,
		any: 3,
		include: 3,
		includes: 3,
		contains: 3,
		invoke: 0,
		max: 3,
		min: 3,
		toArray: 1,
		size: 1,
		first: 3,
		head: 3,
		take: 3,
		initial: 3,
		rest: 3,
		tail: 3,
		drop: 3,
		last: 3,
		without: 0,
		difference: 0,
		indexOf: 3,
		shuffle: 1,
		lastIndexOf: 3,
		isEmpty: 1,
		chain: 1,
		sample: 3,
		partition: 3,
		groupBy: 3,
		countBy: 3,
		sortBy: 3,
		indexBy: 3,
		findIndex: 3,
		findLastIndex: 3
	};
	h(x, S, "models");
	var k = e.View = function (t) {
		this.cid = i.uniqueId("view");
		i.extend(this, i.pick(t, P));
		this._ensureElement();
		this.initialize.apply(this, arguments)
	};
	var T = /^(\S+)\s*(.*)$/;
	var P = ["model", "collection", "el", "id", "attributes", "className", "tagName", "events"];
	i.extend(k.prototype, u, {
		tagName: "div",
		$: function (t) {
			return this.$el.find(t)
		},
		initialize: function () {},
		render: function () {
			return this
		},
		remove: function () {
			this._removeElement();
			this.stopListening();
			return this
		},
		_removeElement: function () {
			this.$el.remove()
		},
		setElement: function (t) {
			this.undelegateEvents();
			this._setElement(t);
			this.delegateEvents();
			return this
		},
		_setElement: function (t) {
			this.$el = t instanceof e.$ ? t : e.$(t);
			this.el = this.$el[0]
		},
		delegateEvents: function (t) {
			t || (t = i.result(this, "events"));
			if (!t) return this;
			this.undelegateEvents();
			for (var e in t) {
				var r = t[e];
				if (!i.isFunction(r)) r = this[r];
				if (!r) continue;
				var n = e.match(T);
				this.delegate(n[1], n[2], i.bind(r, this))
			}
			return this
		},
		delegate: function (t, e, i) {
			this.$el.on(t + ".delegateEvents" + this.cid, e, i);
			return this
		},
		undelegateEvents: function () {
			if (this.$el) this.$el.off(".delegateEvents" + this.cid);
			return this
		},
		undelegate: function (t, e, i) {
			this.$el.off(t + ".delegateEvents" + this.cid, e, i);
			return this
		},
		_createElement: function (t) {
			return document.createElement(t)
		},
		_ensureElement: function () {
			if (!this.el) {
				var t = i.extend({}, i.result(this, "attributes"));
				if (this.id) t.id = i.result(this, "id");
				if (this.className) t["class"] = i.result(this, "className");
				this.setElement(this._createElement(i.result(this, "tagName")));
				this._setAttributes(t)
			} else {
				this.setElement(i.result(this, "el"))
			}
		},
		_setAttributes: function (t) {
			this.$el.attr(t)
		}
	});
	e.sync = function (t, r, n) {
		var s = H[t];
		i.defaults(n || (n = {}), {
			emulateHTTP: e.emulateHTTP,
			emulateJSON: e.emulateJSON
		});
		var a = {
			type: s,
			dataType: "json"
		};
		if (!n.url) {
			a.url = i.result(r, "url") || F()
		}
		if (n.data == null && r && (t === "create" || t === "update" || t === "patch")) {
			a.contentType = "application/json";
			a.data = JSON.stringify(n.attrs || r.toJSON(n))
		}
		if (n.emulateJSON) {
			a.contentType = "application/x-www-form-urlencoded";
			a.data = a.data ? {
				model: a.data
			} : {}
		}
		if (n.emulateHTTP && (s === "PUT" || s === "DELETE" || s === "PATCH")) {
			a.type = "POST";
			if (n.emulateJSON) a.data._method = s;
			var h = n.beforeSend;
			n.beforeSend = function (t) {
				t.setRequestHeader("X-HTTP-Method-Override", s);
				if (h) return h.apply(this, arguments)
			}
		}
		if (a.type !== "GET" && !n.emulateJSON) {
			a.processData = false
		}
		var o = n.error;
		n.error = function (t, e, i) {
			n.textStatus = e;
			n.errorThrown = i;
			if (o) o.call(n.context, t, e, i)
		};
		var l = n.xhr = e.ajax(i.extend(a, n));
		r.trigger("request", r, l, n);
		return l
	};
	var H = {
		create: "POST",
		update: "PUT",
		patch: "PATCH",
		"delete": "DELETE",
		read: "GET"
	};
	e.ajax = function () {
		return e.$.ajax.apply(e.$, arguments)
	};
	var $ = e.Router = function (t) {
		t || (t = {});
		if (t.routes) this.routes = t.routes;
		this._bindRoutes();
		this.initialize.apply(this, arguments)
	};
	var A = /\((.*?)\)/g;
	var C = /(\(\?)?:\w+/g;
	var R = /\*\w+/g;
	var j = /[\-{}\[\]+?.,\\\^$|#\s]/g;
	i.extend($.prototype, u, {
		initialize: function () {},
		route: function (t, r, n) {
			if (!i.isRegExp(t)) t = this._routeToRegExp(t);
			if (i.isFunction(r)) {
				n = r;
				r = ""
			}
			if (!n) n = this[r];
			var s = this;
			e.history.route(t, function (i) {
				var a = s._extractParameters(t, i);
				if (s.execute(n, a, r) !== false) {
					s.trigger.apply(s, ["route:" + r].concat(a));
					s.trigger("route", r, a);
					e.history.trigger("route", s, r, a)
				}
			});
			return this
		},
		execute: function (t, e, i) {
			if (t) t.apply(this, e)
		},
		navigate: function (t, i) {
			e.history.navigate(t, i);
			return this
		},
		_bindRoutes: function () {
			if (!this.routes) return;
			this.routes = i.result(this, "routes");
			var t, e = i.keys(this.routes);
			while ((t = e.pop()) != null) {
				this.route(t, this.routes[t])
			}
		},
		_routeToRegExp: function (t) {
			t = t.replace(j, "\\$&").replace(A, "(?:$1)?").replace(C, function (t, e) {
				return e ? t : "([^/?]+)"
			}).replace(R, "([^?]*?)");
			return new RegExp("^" + t + "(?:\\?([\\s\\S]*))?$")
		},
		_extractParameters: function (t, e) {
			var r = t.exec(e).slice(1);
			return i.map(r, function (t, e) {
				if (e === r.length - 1) return t || null;
				return t ? decodeURIComponent(t) : null
			})
		}
	});
	var N = e.History = function () {
		this.handlers = [];
		this.checkUrl = i.bind(this.checkUrl, this);
		if (typeof window !== "undefined") {
			this.location = window.location;
			this.history = window.history
		}
	};
	var M = /^[#\/]|\s+$/g;
	var O = /^\/+|\/+$/g;
	var U = /#.*$/;
	N.started = false;
	i.extend(N.prototype, u, {
		interval: 50,
		atRoot: function () {
			var t = this.location.pathname.replace(/[^\/]$/, "$&/");
			return t === this.root && !this.getSearch()
		},
		matchRoot: function () {
			var t = this.decodeFragment(this.location.pathname);
			var e = t.slice(0, this.root.length - 1) + "/";
			return e === this.root
		},
		decodeFragment: function (t) {
			return decodeURI(t.replace(/%25/g, "%2525"))
		},
		getSearch: function () {
			var t = this.location.href.replace(/#.*/, "").match(/\?.+/);
			return t ? t[0] : ""
		},
		getHash: function (t) {
			var e = (t || this).location.href.match(/#(.*)$/);
			return e ? e[1] : ""
		},
		getPath: function () {
			var t = this.decodeFragment(this.location.pathname + this.getSearch()).slice(this.root.length - 1);
			return t.charAt(0) === "/" ? t.slice(1) : t
		},
		getFragment: function (t) {
			if (t == null) {
				if (this._usePushState || !this._wantsHashChange) {
					t = this.getPath()
				} else {
					t = this.getHash()
				}
			}
			return t.replace(M, "")
		},
		start: function (t) {
			if (N.started) throw new Error("Backbone.history has already been started");
			N.started = true;
			this.options = i.extend({
				root: "/"
			}, this.options, t);
			this.root = this.options.root;
			this._wantsHashChange = this.options.hashChange !== false;
			this._hasHashChange = "onhashchange" in window && (document.documentMode === void 0 || document.documentMode > 7);
			this._useHashChange = this._wantsHashChange && this._hasHashChange;
			this._wantsPushState = !!this.options.pushState;
			this._hasPushState = !!(this.history && this.history.pushState);
			this._usePushState = this._wantsPushState && this._hasPushState;
			this.fragment = this.getFragment();
			this.root = ("/" + this.root + "/").replace(O, "/");
			if (this._wantsHashChange && this._wantsPushState) {
				if (!this._hasPushState && !this.atRoot()) {
					var e = this.root.slice(0, -1) || "/";
					this.location.replace(e + "#" + this.getPath());
					return true
				} else if (this._hasPushState && this.atRoot()) {
					this.navigate(this.getHash(), {
						replace: true
					})
				}
			}
			if (!this._hasHashChange && this._wantsHashChange && !this._usePushState) {
				this.iframe = document.createElement("iframe");
				this.iframe.src = "javascript:0";
				this.iframe.style.display = "none";
				this.iframe.tabIndex = -1;
				var r = document.body;
				var n = r.insertBefore(this.iframe, r.firstChild).contentWindow;
				n.document.open();
				n.document.close();
				n.location.hash = "#" + this.fragment
			}
			var s = window.addEventListener || function (t, e) {
				return attachEvent("on" + t, e)
			};
			if (this._usePushState) {
				s("popstate", this.checkUrl, false)
			} else if (this._useHashChange && !this.iframe) {
				s("hashchange", this.checkUrl, false)
			} else if (this._wantsHashChange) {
				this._checkUrlInterval = setInterval(this.checkUrl, this.interval)
			}
			if (!this.options.silent) return this.loadUrl()
		},
		stop: function () {
			var t = window.removeEventListener || function (t, e) {
				return detachEvent("on" + t, e)
			};
			if (this._usePushState) {
				t("popstate", this.checkUrl, false)
			} else if (this._useHashChange && !this.iframe) {
				t("hashchange", this.checkUrl, false)
			}
			if (this.iframe) {
				document.body.removeChild(this.iframe);
				this.iframe = null
			}
			if (this._checkUrlInterval) clearInterval(this._checkUrlInterval);
			N.started = false
		},
		route: function (t, e) {
			this.handlers.unshift({
				route: t,
				callback: e
			})
		},
		checkUrl: function (t) {
			var e = this.getFragment();
			if (e === this.fragment && this.iframe) {
				e = this.getHash(this.iframe.contentWindow)
			}
			if (e === this.fragment) return false;
			if (this.iframe) this.navigate(e);
			this.loadUrl()
		},
		loadUrl: function (t) {
			if (!this.matchRoot()) return false;
			t = this.fragment = this.getFragment(t);
			return i.some(this.handlers, function (e) {
				if (e.route.test(t)) {
					e.callback(t);
					return true
				}
			})
		},
		navigate: function (t, e) {
			if (!N.started) return false;
			if (!e || e === true) e = {
				trigger: !!e
			};
			t = this.getFragment(t || "");
			var i = this.root;
			if (t === "" || t.charAt(0) === "?") {
				i = i.slice(0, -1) || "/"
			}
			var r = i + t;
			t = this.decodeFragment(t.replace(U, ""));
			if (this.fragment === t) return;
			this.fragment = t;
			if (this._usePushState) {
				this.history[e.replace ? "replaceState" : "pushState"]({}, document.title, r)
			} else if (this._wantsHashChange) {
				this._updateHash(this.location, t, e.replace);
				if (this.iframe && t !== this.getHash(this.iframe.contentWindow)) {
					var n = this.iframe.contentWindow;
					if (!e.replace) {
						n.document.open();
						n.document.close()
					}
					this._updateHash(n.location, t, e.replace)
				}
			} else {
				return this.location.assign(r)
			}
			if (e.trigger) return this.loadUrl(t)
		},
		_updateHash: function (t, e, i) {
			if (i) {
				var r = t.href.replace(/(javascript:|#).*$/, "");
				t.replace(r + "#" + e)
			} else {
				t.hash = "#" + e
			}
		}
	});
	e.history = new N;
	var q = function (t, e) {
		var r = this;
		var n;
		if (t && i.has(t, "constructor")) {
			n = t.constructor
		} else {
			n = function () {
				return r.apply(this, arguments)
			}
		}
		i.extend(n, r, e);
		n.prototype = i.create(r.prototype, t);
		n.prototype.constructor = n;
		n.__super__ = r.prototype;
		return n
	};
	y.extend = x.extend = $.extend = k.extend = N.extend = q;
	var F = function () {
		throw new Error('A "url" property or function must be specified')
	};
	var B = function (t, e) {
		var i = e.error;
		e.error = function (r) {
			if (i) i.call(e.context, t, r, e);
			t.trigger("error", t, r, e)
		}
	};
	return e
});
(function () {
	var j = false;
	window.JQClass = function () {};
	JQClass.classes = {};
	JQClass.extend = function extender(f) {
		var g = this.prototype;
		j = true;
		var h = new this();
		j = false;
		for (var i in f) {
			h[i] = typeof f[i] == 'function' && typeof g[i] == 'function' ? (function (d, e) {
				return function () {
					var b = this._super;
					this._super = function (a) {
						return g[d].apply(this, a || [])
					};
					var c = e.apply(this, arguments);
					this._super = b;
					return c
				}
			})(i, f[i]) : f[i]
		}

		function JQClass() {
			if (!j && this._init) {
				this._init.apply(this, arguments)
			}
		}
		JQClass.prototype = h;
		JQClass.prototype.constructor = JQClass;
		JQClass.extend = extender;
		return JQClass
	}
})();
(function ($) {
	JQClass.classes.JQPlugin = JQClass.extend({
		name: 'plugin',
		defaultOptions: {},
		regionalOptions: {},
		_getters: [],
		_getMarker: function () {
			return 'is-' + this.name
		},
		_init: function () {
			$.extend(this.defaultOptions, (this.regionalOptions && this.regionalOptions['']) || {});
			var c = camelCase(this.name);
			$[c] = this;
			$.fn[c] = function (a) {
				var b = Array.prototype.slice.call(arguments, 1);
				if ($[c]._isNotChained(a, b)) {
					return $[c][a].apply($[c], [this[0]].concat(b))
				}
				return this.each(function () {
					if (typeof a === 'string') {
						if (a[0] === '_' || !$[c][a]) {
							throw 'Unknown method: ' + a;
						}
						$[c][a].apply($[c], [this].concat(b))
					} else {
						$[c]._attach(this, a)
					}
				})
			}
		},
		setDefaults: function (a) {
			$.extend(this.defaultOptions, a || {})
		},
		_isNotChained: function (a, b) {
			if (a === 'option' && (b.length === 0 || (b.length === 1 && typeof b[0] === 'string'))) {
				return true
			}
			return $.inArray(a, this._getters) > -1
		},
		_attach: function (a, b) {
			a = $(a);
			if (a.hasClass(this._getMarker())) {
				return
			}
			a.addClass(this._getMarker());
			b = $.extend({}, this.defaultOptions, this._getMetadata(a), b || {});
			var c = $.extend({
				name: this.name,
				elem: a,
				options: b
			}, this._instSettings(a, b));
			a.data(this.name, c);
			this._postAttach(a, c);
			this.option(a, b)
		},
		_instSettings: function (a, b) {
			return {}
		},
		_postAttach: function (a, b) {},
		_getMetadata: function (d) {
			try {
				var f = d.data(this.name.toLowerCase()) || '';
				f = f.replace(/'/g, '"');
				f = f.replace(/([a-zA-Z0-9]+):/g, function (a, b, i) {
					var c = f.substring(0, i).match(/"/g);
					return (!c || c.length % 2 === 0 ? '"' + b + '":' : b + ':')
				});
				f = $.parseJSON('{' + f + '}');
				for (var g in f) {
					var h = f[g];
					if (typeof h === 'string' && h.match(/^new Date\((.*)\)$/)) {
						f[g] = eval(h)
					}
				}
				return f
			} catch (e) {
				return {}
			}
		},
		_getInst: function (a) {
			return $(a).data(this.name) || {}
		},
		option: function (a, b, c) {
			a = $(a);
			var d = a.data(this.name);
			if (!b || (typeof b === 'string' && c == null)) {
				var e = (d || {}).options;
				return (e && b ? e[b] : e)
			}
			if (!a.hasClass(this._getMarker())) {
				return
			}
			var e = b || {};
			if (typeof b === 'string') {
				e = {};
				e[b] = c
			}
			this._optionsChanged(a, d, e);
			$.extend(d.options, e)
		},
		_optionsChanged: function (a, b, c) {},
		destroy: function (a) {
			a = $(a);
			if (!a.hasClass(this._getMarker())) {
				return
			}
			this._preDestroy(a, this._getInst(a));
			a.removeData(this.name).removeClass(this._getMarker())
		},
		_preDestroy: function (a, b) {}
	});

	function camelCase(c) {
		return c.replace(/-([a-z])/g, function (a, b) {
			return b.toUpperCase()
		})
	}
	$.JQPlugin = {
		createPlugin: function (a, b) {
			if (typeof a === 'object') {
				b = a;
				a = 'JQPlugin'
			}
			a = camelCase(a);
			var c = camelCase(b.name);
			JQClass.classes[c] = JQClass.classes[a].extend(b);
			new JQClass.classes[c]()
		}
	}
})(jQuery);
(function ($) {
	var w = 'countdown';
	var Y = 0;
	var O = 1;
	var W = 2;
	var D = 3;
	var H = 4;
	var M = 5;
	var S = 6;
	$.JQPlugin.createPlugin({
		name: w,
		defaultOptions: {
			until: null,
			since: null,
			timezone: null,
			serverSync: null,
			format: 'dHMS',
			layout: '',
			compact: false,
			padZeroes: false,
			significant: 0,
			description: '',
			expiryUrl: '',
			expiryText: '',
			alwaysExpire: false,
			onExpiry: null,
			onTick: null,
			tickInterval: 1
		},
		regionalOptions: {
			'': {
				labels: ['Years', 'Months', 'Weeks', 'Days', 'Hours', 'Minutes', 'Seconds'],
				labels1: ['Year', 'Month', 'Week', 'Day', 'Hour', 'Minute', 'Second'],
				compactLabels: ['y', 'm', 'w', 'd'],
				whichLabels: null,
				digits: ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'],
				timeSeparator: ':',
				isRTL: false
			}
		},
		_getters: ['getTimes'],
		_rtlClass: w + '-rtl',
		_sectionClass: w + '-section',
		_amountClass: w + '-amount',
		_periodClass: w + '-period',
		_rowClass: w + '-row',
		_holdingClass: w + '-holding',
		_showClass: w + '-show',
		_descrClass: w + '-descr',
		_timerElems: [],
		_init: function () {
			var c = this;
			this._super();
			this._serverSyncs = [];
			var d = (typeof Date.now == 'function' ? Date.now : function () {
				return new Date().getTime()
			});
			var e = (window.performance && typeof window.performance.now == 'function');

			function timerCallBack(a) {
				var b = (a < 1e12 ? (e ? (performance.now() + performance.timing.navigationStart) : d()) : a || d());
				if (b - g >= 1000) {
					c._updateElems();
					g = b
				}
				f(timerCallBack)
			}
			var f = window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || null;
			var g = 0;
			if (!f || $.noRequestAnimationFrame) {
				$.noRequestAnimationFrame = null;
				setInterval(function () {
					c._updateElems()
				}, 980)
			} else {
				g = window.animationStartTime || window.webkitAnimationStartTime || window.mozAnimationStartTime || window.oAnimationStartTime || window.msAnimationStartTime || d();
				f(timerCallBack)
			}
		},
		UTCDate: function (a, b, c, e, f, g, h, i) {
			if (typeof b == 'object' && b.constructor == Date) {
				i = b.getMilliseconds();
				h = b.getSeconds();
				g = b.getMinutes();
				f = b.getHours();
				e = b.getDate();
				c = b.getMonth();
				b = b.getFullYear()
			}
			var d = new Date();
			d.setUTCFullYear(b);
			d.setUTCDate(1);
			d.setUTCMonth(c || 0);
			d.setUTCDate(e || 1);
			d.setUTCHours(f || 0);
			d.setUTCMinutes((g || 0) - (Math.abs(a) < 30 ? a * 60 : a));
			d.setUTCSeconds(h || 0);
			d.setUTCMilliseconds(i || 0);
			return d
		},
		periodsToSeconds: function (a) {
			return a[0] * 31557600 + a[1] * 2629800 + a[2] * 604800 + a[3] * 86400 + a[4] * 3600 + a[5] * 60 + a[6]
		},
		resync: function () {
			var d = this;
			$('.' + this._getMarker()).each(function () {
				var a = $.data(this, d.name);
				if (a.options.serverSync) {
					var b = null;
					for (var i = 0; i < d._serverSyncs.length; i++) {
						if (d._serverSyncs[i][0] == a.options.serverSync) {
							b = d._serverSyncs[i];
							break
						}
					}
					if (b[2] == null) {
						var c = ($.isFunction(a.options.serverSync) ? a.options.serverSync.apply(this, []) : null);
						b[2] = (c ? new Date().getTime() - c.getTime() : 0) - b[1]
					}
					if (a._since) {
						a._since.setMilliseconds(a._since.getMilliseconds() + b[2])
					}
					a._until.setMilliseconds(a._until.getMilliseconds() + b[2])
				}
			});
			for (var i = 0; i < d._serverSyncs.length; i++) {
				if (d._serverSyncs[i][2] != null) {
					d._serverSyncs[i][1] += d._serverSyncs[i][2];
					delete d._serverSyncs[i][2]
				}
			}
		},
		_instSettings: function (a, b) {
			return {
				_periods: [0, 0, 0, 0, 0, 0, 0]
			}
		},
		_addElem: function (a) {
			if (!this._hasElem(a)) {
				this._timerElems.push(a)
			}
		},
		_hasElem: function (a) {
			return ($.inArray(a, this._timerElems) > -1)
		},
		_removeElem: function (b) {
			this._timerElems = $.map(this._timerElems, function (a) {
				return (a == b ? null : a)
			})
		},
		_updateElems: function () {
			for (var i = this._timerElems.length - 1; i >= 0; i--) {
				this._updateCountdown(this._timerElems[i])
			}
		},
		_optionsChanged: function (a, b, c) {
			if (c.layout) {
				c.layout = c.layout.replace(/&lt;/g, '<').replace(/&gt;/g, '>')
			}
			this._resetExtraLabels(b.options, c);
			var d = (b.options.timezone != c.timezone);
			$.extend(b.options, c);
			this._adjustSettings(a, b, c.until != null || c.since != null || d);
			var e = new Date();
			if ((b._since && b._since < e) || (b._until && b._until > e)) {
				this._addElem(a[0])
			}
			this._updateCountdown(a, b)
		},
		_updateCountdown: function (a, b) {
			a = a.jquery ? a : $(a);
			b = b || this._getInst(a);
			if (!b) {
				return
			}
			a.html(this._generateHTML(b)).toggleClass(this._rtlClass, b.options.isRTL);
			if ($.isFunction(b.options.onTick)) {
				var c = b._hold != 'lap' ? b._periods : this._calculatePeriods(b, b._show, b.options.significant, new Date());
				if (b.options.tickInterval == 1 || this.periodsToSeconds(c) % b.options.tickInterval == 0) {
					b.options.onTick.apply(a[0], [c])
				}
			}
			var d = b._hold != 'pause' && (b._since ? b._now.getTime() < b._since.getTime() : b._now.getTime() >= b._until.getTime());
			if (d && !b._expiring) {
				b._expiring = true;
				if (this._hasElem(a[0]) || b.options.alwaysExpire) {
					this._removeElem(a[0]);
					if ($.isFunction(b.options.onExpiry)) {
						b.options.onExpiry.apply(a[0], [])
					}
					if (b.options.expiryText) {
						var e = b.options.layout;
						b.options.layout = b.options.expiryText;
						this._updateCountdown(a[0], b);
						b.options.layout = e
					}
					if (b.options.expiryUrl) {
						window.location = b.options.expiryUrl
					}
				}
				b._expiring = false
			} else if (b._hold == 'pause') {
				this._removeElem(a[0])
			}
		},
		_resetExtraLabels: function (a, b) {
			for (var n in b) {
				if (n.match(/[Ll]abels[02-9]|compactLabels1/)) {
					a[n] = b[n]
				}
			}
			for (var n in a) {
				if (n.match(/[Ll]abels[02-9]|compactLabels1/) && typeof b[n] === 'undefined') {
					a[n] = null
				}
			}
		},
		_adjustSettings: function (a, b, c) {
			var d = null;
			for (var i = 0; i < this._serverSyncs.length; i++) {
				if (this._serverSyncs[i][0] == b.options.serverSync) {
					d = this._serverSyncs[i][1];
					break
				}
			}
			if (d != null) {
				var e = (b.options.serverSync ? d : 0);
				var f = new Date()
			} else {
				var g = ($.isFunction(b.options.serverSync) ? b.options.serverSync.apply(a[0], []) : null);
				var f = new Date();
				var e = (g ? f.getTime() - g.getTime() : 0);
				this._serverSyncs.push([b.options.serverSync, e])
			}
			var h = b.options.timezone;
			h = (h == null ? -f.getTimezoneOffset() : h);
			if (c || (!c && b._until == null && b._since == null)) {
				b._since = b.options.since;
				if (b._since != null) {
					b._since = this.UTCDate(h, this._determineTime(b._since, null));
					if (b._since && e) {
						b._since.setMilliseconds(b._since.getMilliseconds() + e)
					}
				}
				b._until = this.UTCDate(h, this._determineTime(b.options.until, f));
				if (e) {
					b._until.setMilliseconds(b._until.getMilliseconds() + e)
				}
			}
			b._show = this._determineShow(b)
		},
		_preDestroy: function (a, b) {
			this._removeElem(a[0]);
			a.empty()
		},
		pause: function (a) {
			this._hold(a, 'pause')
		},
		lap: function (a) {
			this._hold(a, 'lap')
		},
		resume: function (a) {
			this._hold(a, null)
		},
		toggle: function (a) {
			var b = $.data(a, this.name) || {};
			this[!b._hold ? 'pause' : 'resume'](a)
		},
		toggleLap: function (a) {
			var b = $.data(a, this.name) || {};
			this[!b._hold ? 'lap' : 'resume'](a)
		},
		_hold: function (a, b) {
			var c = $.data(a, this.name);
			if (c) {
				if (c._hold == 'pause' && !b) {
					c._periods = c._savePeriods;
					var d = (c._since ? '-' : '+');
					c[c._since ? '_since' : '_until'] = this._determineTime(d + c._periods[0] + 'y' + d + c._periods[1] + 'o' + d + c._periods[2] + 'w' + d + c._periods[3] + 'd' + d + c._periods[4] + 'h' + d + c._periods[5] + 'm' + d + c._periods[6] + 's');
					this._addElem(a)
				}
				c._hold = b;
				c._savePeriods = (b == 'pause' ? c._periods : null);
				$.data(a, this.name, c);
				this._updateCountdown(a, c)
			}
		},
		getTimes: function (a) {
			var b = $.data(a, this.name);
			return (!b ? null : (b._hold == 'pause' ? b._savePeriods : (!b._hold ? b._periods : this._calculatePeriods(b, b._show, b.options.significant, new Date()))))
		},
		_determineTime: function (k, l) {
			var m = this;
			var n = function (a) {
				var b = new Date();
				b.setTime(b.getTime() + a * 1000);
				return b
			};
			var o = function (a) {
				a = a.toLowerCase();
				var b = new Date();
				var c = b.getFullYear();
				var d = b.getMonth();
				var e = b.getDate();
				var f = b.getHours();
				var g = b.getMinutes();
				var h = b.getSeconds();
				var i = /([+-]?[0-9]+)\s*(s|m|h|d|w|o|y)?/g;
				var j = i.exec(a);
				while (j) {
					switch (j[2] || 's') {
						case 's':
							h += parseInt(j[1], 10);
							break;
						case 'm':
							g += parseInt(j[1], 10);
							break;
						case 'h':
							f += parseInt(j[1], 10);
							break;
						case 'd':
							e += parseInt(j[1], 10);
							break;
						case 'w':
							e += parseInt(j[1], 10) * 7;
							break;
						case 'o':
							d += parseInt(j[1], 10);
							e = Math.min(e, m._getDaysInMonth(c, d));
							break;
						case 'y':
							c += parseInt(j[1], 10);
							e = Math.min(e, m._getDaysInMonth(c, d));
							break
					}
					j = i.exec(a)
				}
				return new Date(c, d, e, f, g, h, 0)
			};
			var p = (k == null ? l : (typeof k == 'string' ? o(k) : (typeof k == 'number' ? n(k) : k)));
			if (p) p.setMilliseconds(0);
			return p
		},
		_getDaysInMonth: function (a, b) {
			return 32 - new Date(a, b, 32).getDate()
		},
		_normalLabels: function (a) {
			return a
		},
		_generateHTML: function (c) {
			var d = this;
			c._periods = (c._hold ? c._periods : this._calculatePeriods(c, c._show, c.options.significant, new Date()));
			var e = false;
			var f = 0;
			var g = c.options.significant;
			var h = $.extend({}, c._show);
			for (var i = Y; i <= S; i++) {
				e |= (c._show[i] == '?' && c._periods[i] > 0);
				h[i] = (c._show[i] == '?' && !e ? null : c._show[i]);
				f += (h[i] ? 1 : 0);
				g -= (c._periods[i] > 0 ? 1 : 0)
			}
			var j = [false, false, false, false, false, false, false];
			for (var i = S; i >= Y; i--) {
				if (c._show[i]) {
					if (c._periods[i]) {
						j[i] = true
					} else {
						j[i] = g > 0;
						g--
					}
				}
			}
			var k = (c.options.compact ? c.options.compactLabels : c.options.labels);
			var l = c.options.whichLabels || this._normalLabels;
			var m = function (a) {
				var b = c.options['compactLabels' + l(c._periods[a])];
				return (h[a] ? d._translateDigits(c, c._periods[a]) + (b ? b[a] : k[a]) + ' ' : '')
			};
			var n = (c.options.padZeroes ? 2 : 1);
			var o = function (a) {
				var b = c.options['labels' + l(c._periods[a])];
				return ((!c.options.significant && h[a]) || (c.options.significant && j[a]) ? '<span class="' + d._sectionClass + '">' + '<span class="' + d._amountClass + '">' + d._minDigits(c, c._periods[a], n) + '</span>' + '<span class="' + d._periodClass + '">' + (b ? b[a] : k[a]) + '</span></span>' : '')
			};
			return (c.options.layout ? this._buildLayout(c, h, c.options.layout, c.options.compact, c.options.significant, j) : ((c.options.compact ? '<span class="' + this._rowClass + ' ' + this._amountClass + (c._hold ? ' ' + this._holdingClass : '') + '">' + m(Y) + m(O) + m(W) + m(D) + (h[H] ? this._minDigits(c, c._periods[H], 2) : '') + (h[M] ? (h[H] ? c.options.timeSeparator : '') + this._minDigits(c, c._periods[M], 2) : '') + (h[S] ? (h[H] || h[M] ? c.options.timeSeparator : '') + this._minDigits(c, c._periods[S], 2) : '') : '<span class="' + this._rowClass + ' ' + this._showClass + (c.options.significant || f) + (c._hold ? ' ' + this._holdingClass : '') + '">' + o(Y) + o(O) + o(W) + o(D) + o(H) + o(M) + o(S)) + '</span>' + (c.options.description ? '<span class="' + this._rowClass + ' ' + this._descrClass + '">' + c.options.description + '</span>' : '')))
		},
		_buildLayout: function (c, d, e, f, g, h) {
			var j = c.options[f ? 'compactLabels' : 'labels'];
			var k = c.options.whichLabels || this._normalLabels;
			var l = function (a) {
				return (c.options[(f ? 'compactLabels' : 'labels') + k(c._periods[a])] || j)[a]
			};
			var m = function (a, b) {
				return c.options.digits[Math.floor(a / b) % 10]
			};
			var o = {
				desc: c.options.description,
				sep: c.options.timeSeparator,
				yl: l(Y),
				yn: this._minDigits(c, c._periods[Y], 1),
				ynn: this._minDigits(c, c._periods[Y], 2),
				ynnn: this._minDigits(c, c._periods[Y], 3),
				y1: m(c._periods[Y], 1),
				y10: m(c._periods[Y], 10),
				y100: m(c._periods[Y], 100),
				y1000: m(c._periods[Y], 1000),
				ol: l(O),
				on: this._minDigits(c, c._periods[O], 1),
				onn: this._minDigits(c, c._periods[O], 2),
				onnn: this._minDigits(c, c._periods[O], 3),
				o1: m(c._periods[O], 1),
				o10: m(c._periods[O], 10),
				o100: m(c._periods[O], 100),
				o1000: m(c._periods[O], 1000),
				wl: l(W),
				wn: this._minDigits(c, c._periods[W], 1),
				wnn: this._minDigits(c, c._periods[W], 2),
				wnnn: this._minDigits(c, c._periods[W], 3),
				w1: m(c._periods[W], 1),
				w10: m(c._periods[W], 10),
				w100: m(c._periods[W], 100),
				w1000: m(c._periods[W], 1000),
				dl: l(D),
				dn: this._minDigits(c, c._periods[D], 1),
				dnn: this._minDigits(c, c._periods[D], 2),
				dnnn: this._minDigits(c, c._periods[D], 3),
				d1: m(c._periods[D], 1),
				d10: m(c._periods[D], 10),
				d100: m(c._periods[D], 100),
				d1000: m(c._periods[D], 1000),
				hl: l(H),
				hn: this._minDigits(c, c._periods[H], 1),
				hnn: this._minDigits(c, c._periods[H], 2),
				hnnn: this._minDigits(c, c._periods[H], 3),
				h1: m(c._periods[H], 1),
				h10: m(c._periods[H], 10),
				h100: m(c._periods[H], 100),
				h1000: m(c._periods[H], 1000),
				ml: l(M),
				mn: this._minDigits(c, c._periods[M], 1),
				mnn: this._minDigits(c, c._periods[M], 2),
				mnnn: this._minDigits(c, c._periods[M], 3),
				m1: m(c._periods[M], 1),
				m10: m(c._periods[M], 10),
				m100: m(c._periods[M], 100),
				m1000: m(c._periods[M], 1000),
				sl: l(S),
				sn: this._minDigits(c, c._periods[S], 1),
				snn: this._minDigits(c, c._periods[S], 2),
				snnn: this._minDigits(c, c._periods[S], 3),
				s1: m(c._periods[S], 1),
				s10: m(c._periods[S], 10),
				s100: m(c._periods[S], 100),
				s1000: m(c._periods[S], 1000)
			};
			var p = e;
			for (var i = Y; i <= S; i++) {
				var q = 'yowdhms'.charAt(i);
				var r = new RegExp('\\{' + q + '<\\}([\\s\\S]*)\\{' + q + '>\\}', 'g');
				p = p.replace(r, ((!g && d[i]) || (g && h[i]) ? '$1' : ''))
			}
			$.each(o, function (n, v) {
				var a = new RegExp('\\{' + n + '\\}', 'g');
				p = p.replace(a, v)
			});
			return p
		},
		_minDigits: function (a, b, c) {
			b = '' + b;
			if (b.length >= c) {
				return this._translateDigits(a, b)
			}
			b = '0000000000' + b;
			return this._translateDigits(a, b.substr(b.length - c))
		},
		_translateDigits: function (b, c) {
			return ('' + c).replace(/[0-9]/g, function (a) {
				return b.options.digits[a]
			})
		},
		_determineShow: function (a) {
			var b = a.options.format;
			var c = [];
			c[Y] = (b.match('y') ? '?' : (b.match('Y') ? '!' : null));
			c[O] = (b.match('o') ? '?' : (b.match('O') ? '!' : null));
			c[W] = (b.match('w') ? '?' : (b.match('W') ? '!' : null));
			c[D] = (b.match('d') ? '?' : (b.match('D') ? '!' : null));
			c[H] = (b.match('h') ? '?' : (b.match('H') ? '!' : null));
			c[M] = (b.match('m') ? '?' : (b.match('M') ? '!' : null));
			c[S] = (b.match('s') ? '?' : (b.match('S') ? '!' : null));
			return c
		},
		_calculatePeriods: function (c, d, e, f) {
			c._now = f;
			c._now.setMilliseconds(0);
			var g = new Date(c._now.getTime());
			if (c._since) {
				if (f.getTime() < c._since.getTime()) {
					c._now = f = g
				} else {
					f = c._since
				}
			} else {
				g.setTime(c._until.getTime());
				if (f.getTime() > c._until.getTime()) {
					c._now = f = g
				}
			}
			var h = [0, 0, 0, 0, 0, 0, 0];
			if (d[Y] || d[O]) {
				var i = this._getDaysInMonth(f.getFullYear(), f.getMonth());
				var j = this._getDaysInMonth(g.getFullYear(), g.getMonth());
				var k = (g.getDate() == f.getDate() || (g.getDate() >= Math.min(i, j) && f.getDate() >= Math.min(i, j)));
				var l = function (a) {
					return (a.getHours() * 60 + a.getMinutes()) * 60 + a.getSeconds()
				};
				var m = Math.max(0, (g.getFullYear() - f.getFullYear()) * 12 + g.getMonth() - f.getMonth() + ((g.getDate() < f.getDate() && !k) || (k && l(g) < l(f)) ? -1 : 0));
				h[Y] = (d[Y] ? Math.floor(m / 12) : 0);
				h[O] = (d[O] ? m - h[Y] * 12 : 0);
				f = new Date(f.getTime());
				var n = (f.getDate() == i);
				var o = this._getDaysInMonth(f.getFullYear() + h[Y], f.getMonth() + h[O]);
				if (f.getDate() > o) {
					f.setDate(o)
				}
				f.setFullYear(f.getFullYear() + h[Y]);
				f.setMonth(f.getMonth() + h[O]);
				if (n) {
					f.setDate(o)
				}
			}
			var p = Math.floor((g.getTime() - f.getTime()) / 1000);
			var q = function (a, b) {
				h[a] = (d[a] ? Math.floor(p / b) : 0);
				p -= h[a] * b
			};
			q(W, 604800);
			q(D, 86400);
			q(H, 3600);
			q(M, 60);
			q(S, 1);
			if (p > 0 && !c._since) {
				var r = [1, 12, 4.3482, 7, 24, 60, 60];
				var s = S;
				var t = 1;
				for (var u = S; u >= Y; u--) {
					if (d[u]) {
						if (h[s] >= t) {
							h[s] = 0;
							p = 1
						}
						if (p > 0) {
							h[u]++;
							p = 0;
							s = u;
							t = 1
						}
					}
					t *= r[u]
				}
			}
			if (e) {
				for (var u = Y; u <= S; u++) {
					if (e && h[u]) {
						e--
					} else if (!e) {
						h[u] = 0
					}
				}
			}
			return h
		}
	})
})(jQuery);
"function" !== typeof Object.create && (Object.create = function (f) {
	function g() {}
	g.prototype = f;
	return new g
});
(function (f, g, k) {
	var l = {
		init: function (a, b) {
			this.$elem = f(b);
			this.options = f.extend({}, f.fn.owlCarousel.options, this.$elem.data(), a);
			this.userOptions = a;
			this.loadContent()
		},
		loadContent: function () {
			function a(a) {
				var d, e = "";
				if ("function" === typeof b.options.jsonSuccess) b.options.jsonSuccess.apply(this, [a]);
				else {
					for (d in a.owl) a.owl.hasOwnProperty(d) && (e += a.owl[d].item);
					b.$elem.html(e)
				}
				b.logIn()
			}
			var b = this,
				e;
			"function" === typeof b.options.beforeInit && b.options.beforeInit.apply(this, [b.$elem]);
			"string" === typeof b.options.jsonPath ? (e = b.options.jsonPath, f.getJSON(e, a)) : b.logIn()
		},
		logIn: function () {
			this.$elem.data("owl-originalStyles", this.$elem.attr("style"));
			this.$elem.data("owl-originalClasses", this.$elem.attr("class"));
			this.$elem.css({
				opacity: 0
			});
			this.orignalItems = this.options.items;
			this.checkBrowser();
			this.wrapperWidth = 0;
			this.checkVisible = null;
			this.setVars()
		},
		setVars: function () {
			if (0 === this.$elem.children().length) return !1;
			this.baseClass();
			this.eventTypes();
			this.$userItems = this.$elem.children();
			this.itemsAmount = this.$userItems.length;
			this.wrapItems();
			this.$owlItems = this.$elem.find(".owl-item");
			this.$owlWrapper = this.$elem.find(".owl-wrapper");
			this.playDirection = "next";
			this.prevItem = 0;
			this.prevArr = [0];
			this.currentItem = 0;
			this.customEvents();
			this.onStartup()
		},
		onStartup: function () {
			this.updateItems();
			this.calculateAll();
			this.buildControls();
			this.updateControls();
			this.response();
			this.moveEvents();
			this.stopOnHover();
			this.owlStatus();
			!1 !== this.options.transitionStyle && this.transitionTypes(this.options.transitionStyle);
			!0 === this.options.autoPlay && (this.options.autoPlay = 5E3);
			this.play();
			this.$elem.find(".owl-wrapper").css("display", "block");
			this.$elem.is(":visible") ? this.$elem.css("opacity", 1) : this.watchVisibility();
			this.onstartup = !1;
			this.eachMoveUpdate();
			"function" === typeof this.options.afterInit && this.options.afterInit.apply(this, [this.$elem])
		},
		eachMoveUpdate: function () {
			!0 === this.options.lazyLoad && this.lazyLoad();
			!0 === this.options.autoHeight && this.autoHeight();
			this.onVisibleItems();
			"function" === typeof this.options.afterAction && this.options.afterAction.apply(this, [this.$elem])
		},
		updateVars: function () {
			"function" === typeof this.options.beforeUpdate && this.options.beforeUpdate.apply(this, [this.$elem]);
			this.watchVisibility();
			this.updateItems();
			this.calculateAll();
			this.updatePosition();
			this.updateControls();
			this.eachMoveUpdate();
			"function" === typeof this.options.afterUpdate && this.options.afterUpdate.apply(this, [this.$elem])
		},
		reload: function () {
			var a = this;
			g.setTimeout(function () {
				a.updateVars()
			}, 0)
		},
		watchVisibility: function () {
			var a = this;
			if (!1 === a.$elem.is(":visible")) a.$elem.css({
				opacity: 0
			}), g.clearInterval(a.autoPlayInterval), g.clearInterval(a.checkVisible);
			else return !1;
			a.checkVisible = g.setInterval(function () {
				a.$elem.is(":visible") && (a.reload(), a.$elem.animate({
					opacity: 1
				}, 200), g.clearInterval(a.checkVisible))
			}, 500)
		},
		wrapItems: function () {
			this.$userItems.wrapAll('<div class="owl-wrapper">').wrap('<div class="owl-item"></div>');
			this.$elem.find(".owl-wrapper").wrap('<div class="owl-wrapper-outer">');
			this.wrapperOuter = this.$elem.find(".owl-wrapper-outer");
			this.$elem.css("display", "block")
		},
		baseClass: function () {
			var a = this.$elem.hasClass(this.options.baseClass),
				b = this.$elem.hasClass(this.options.theme);
			a || this.$elem.addClass(this.options.baseClass);
			b || this.$elem.addClass(this.options.theme)
		},
		updateItems: function () {
			var a, b;
			if (!1 === this.options.responsive) return !1;
			if (!0 === this.options.singleItem) return this.options.items = this.orignalItems = 1, this.options.itemsCustom = !1, this.options.itemsDesktop = !1, this.options.itemsDesktopSmall = !1, this.options.itemsTablet = !1, this.options.itemsTabletSmall = !1, this.options.itemsMobile = !1;
			a = f(this.options.responsiveBaseWidth).width();
			a > (this.options.itemsDesktop[0] || this.orignalItems) && (this.options.items = this.orignalItems);
			if (!1 !== this.options.itemsCustom)
				for (this.options.itemsCustom.sort(function (a, b) {
						return a[0] - b[0]
					}), b = 0; b < this.options.itemsCustom.length; b += 1) this.options.itemsCustom[b][0] <= a && (this.options.items = this.options.itemsCustom[b][1]);
			else a <= this.options.itemsDesktop[0] && !1 !== this.options.itemsDesktop && (this.options.items = this.options.itemsDesktop[1]), a <= this.options.itemsDesktopSmall[0] && !1 !== this.options.itemsDesktopSmall && (this.options.items = this.options.itemsDesktopSmall[1]), a <= this.options.itemsTablet[0] && !1 !== this.options.itemsTablet && (this.options.items = this.options.itemsTablet[1]), a <= this.options.itemsTabletSmall[0] && !1 !== this.options.itemsTabletSmall && (this.options.items = this.options.itemsTabletSmall[1]), a <= this.options.itemsMobile[0] && !1 !== this.options.itemsMobile && (this.options.items = this.options.itemsMobile[1]);
			this.options.items > this.itemsAmount && !0 === this.options.itemsScaleUp && (this.options.items = this.itemsAmount)
		},
		response: function () {
			var a = this,
				b, e;
			if (!0 !== a.options.responsive) return !1;
			e = f(g).width();
			a.resizer = function () {
				f(g).width() !== e && (!1 !== a.options.autoPlay && g.clearInterval(a.autoPlayInterval), g.clearTimeout(b), b = g.setTimeout(function () {
					e = f(g).width();
					a.updateVars()
				}, a.options.responsiveRefreshRate))
			};
			f(g).resize(a.resizer)
		},
		updatePosition: function () {
			this.jumpTo(this.currentItem);
			!1 !== this.options.autoPlay && this.checkAp()
		},
		appendItemsSizes: function () {
			var a = this,
				b = 0,
				e = a.itemsAmount - a.options.items;
			a.$owlItems.each(function (c) {
				var d = f(this);
				d.css({
					width: a.itemWidth
				}).data("owl-item", Number(c));
				if (0 === c % a.options.items || c === e) c > e || (b += 1);
				d.data("owl-roundPages", b)
			})
		},
		appendWrapperSizes: function () {
			this.$owlWrapper.css({
				width: this.$owlItems.length * this.itemWidth * 2,
				left: 0
			});
			this.appendItemsSizes()
		},
		calculateAll: function () {
			this.calculateWidth();
			this.appendWrapperSizes();
			this.loops();
			this.max()
		},
		calculateWidth: function () {
			this.itemWidth = Math.round(this.$elem.width() / this.options.items)
		},
		max: function () {
			var a = -1 * (this.itemsAmount * this.itemWidth - this.options.items * this.itemWidth);
			this.options.items > this.itemsAmount ? this.maximumPixels = a = this.maximumItem = 0 : (this.maximumItem = this.itemsAmount - this.options.items, this.maximumPixels = a);
			return a
		},
		min: function () {
			return 0
		},
		loops: function () {
			var a = 0,
				b = 0,
				e, c;
			this.positionsInArray = [0];
			this.pagesInArray = [];
			for (e = 0; e < this.itemsAmount; e += 1) b += this.itemWidth, this.positionsInArray.push(-b), !0 === this.options.scrollPerPage && (c = f(this.$owlItems[e]), c = c.data("owl-roundPages"), c !== a && (this.pagesInArray[a] = this.positionsInArray[e], a = c))
		},
		buildControls: function () {
			if (!0 === this.options.navigation || !0 === this.options.pagination) this.owlControls = f('<div class="owl-controls"/>').toggleClass("clickable", !this.browser.isTouch).appendTo(this.$elem);
			!0 === this.options.pagination && this.buildPagination();
			!0 === this.options.navigation && this.buildButtons()
		},
		buildButtons: function () {
			var a = this,
				b = f('<div class="owl-buttons"/>');
			a.owlControls.append(b);
			a.buttonPrev = f("<div/>", {
				"class": "owl-prev",
				html: a.options.navigationText[0] || ""
			});
			a.buttonNext = f("<div/>", {
				"class": "owl-next",
				html: a.options.navigationText[1] || ""
			});
			b.append(a.buttonPrev).append(a.buttonNext);
			b.on("touchstart.owlControls mousedown.owlControls", 'div[class^="owl"]', function (a) {
				a.preventDefault()
			});
			b.on("touchend.owlControls mouseup.owlControls", 'div[class^="owl"]', function (b) {
				b.preventDefault();
				f(this).hasClass("owl-next") ? a.next() : a.prev()
			})
		},
		buildPagination: function () {
			var a = this;
			a.paginationWrapper = f('<div class="owl-pagination"/>');
			a.owlControls.append(a.paginationWrapper);
			a.paginationWrapper.on("touchend.owlControls mouseup.owlControls", ".owl-page", function (b) {
				b.preventDefault();
				Number(f(this).data("owl-page")) !== a.currentItem && a.goTo(Number(f(this).data("owl-page")), !0)
			})
		},
		updatePagination: function () {
			var a, b, e, c, d, g;
			if (!1 === this.options.pagination) return !1;
			this.paginationWrapper.html("");
			a = 0;
			b = this.itemsAmount - this.itemsAmount % this.options.items;
			for (c = 0; c < this.itemsAmount; c += 1) 0 === c % this.options.items && (a += 1, b === c && (e = this.itemsAmount - this.options.items), d = f("<div/>", {
				"class": "owl-page"
			}), g = f("<span></span>", {
				text: !0 === this.options.paginationNumbers ? a : "",
				"class": !0 === this.options.paginationNumbers ? "owl-numbers" : ""
			}), d.append(g), d.data("owl-page", b === c ? e : c), d.data("owl-roundPages", a), this.paginationWrapper.append(d));
			this.checkPagination()
		},
		checkPagination: function () {
			var a = this;
			if (!1 === a.options.pagination) return !1;
			a.paginationWrapper.find(".owl-page").each(function () {
				f(this).data("owl-roundPages") === f(a.$owlItems[a.currentItem]).data("owl-roundPages") && (a.paginationWrapper.find(".owl-page").removeClass("active"), f(this).addClass("active"))
			})
		},
		checkNavigation: function () {
			if (!1 === this.options.navigation) return !1;
			!1 === this.options.rewindNav && (0 === this.currentItem && 0 === this.maximumItem ? (this.buttonPrev.addClass("disabled"), this.buttonNext.addClass("disabled")) : 0 === this.currentItem && 0 !== this.maximumItem ? (this.buttonPrev.addClass("disabled"), this.buttonNext.removeClass("disabled")) : this.currentItem === this.maximumItem ? (this.buttonPrev.removeClass("disabled"), this.buttonNext.addClass("disabled")) : 0 !== this.currentItem && this.currentItem !== this.maximumItem && (this.buttonPrev.removeClass("disabled"), this.buttonNext.removeClass("disabled")))
		},
		updateControls: function () {
			this.updatePagination();
			this.checkNavigation();
			this.owlControls && (this.options.items >= this.itemsAmount ? this.owlControls.hide() : this.owlControls.show())
		},
		destroyControls: function () {
			this.owlControls && this.owlControls.remove()
		},
		next: function (a) {
			if (this.isTransition) return !1;
			this.currentItem += !0 === this.options.scrollPerPage ? this.options.items : 1;
			if (this.currentItem > this.maximumItem + (!0 === this.options.scrollPerPage ? this.options.items - 1 : 0))
				if (!0 === this.options.rewindNav) this.currentItem = 0, a = "rewind";
				else return this.currentItem = this.maximumItem, !1;
			this.goTo(this.currentItem, a)
		},
		prev: function (a) {
			if (this.isTransition) return !1;
			this.currentItem = !0 === this.options.scrollPerPage && 0 < this.currentItem && this.currentItem < this.options.items ? 0 : this.currentItem - (!0 === this.options.scrollPerPage ? this.options.items : 1);
			if (0 > this.currentItem)
				if (!0 === this.options.rewindNav) this.currentItem = this.maximumItem, a = "rewind";
				else return this.currentItem = 0, !1;
			this.goTo(this.currentItem, a)
		},
		goTo: function (a, b, e) {
			var c = this;
			if (c.isTransition) return !1;
			"function" === typeof c.options.beforeMove && c.options.beforeMove.apply(this, [c.$elem]);
			a >= c.maximumItem ? a = c.maximumItem : 0 >= a && (a = 0);
			c.currentItem = c.owl.currentItem = a;
			if (!1 !== c.options.transitionStyle && "drag" !== e && 1 === c.options.items && !0 === c.browser.support3d) return c.swapSpeed(0), !0 === c.browser.support3d ? c.transition3d(c.positionsInArray[a]) : c.css2slide(c.positionsInArray[a], 1), c.afterGo(), c.singleItemTransition(), !1;
			a = c.positionsInArray[a];
			!0 === c.browser.support3d ? (c.isCss3Finish = !1, !0 === b ? (c.swapSpeed("paginationSpeed"), g.setTimeout(function () {
				c.isCss3Finish = !0
			}, c.options.paginationSpeed)) : "rewind" === b ? (c.swapSpeed(c.options.rewindSpeed), g.setTimeout(function () {
				c.isCss3Finish = !0
			}, c.options.rewindSpeed)) : (c.swapSpeed("slideSpeed"), g.setTimeout(function () {
				c.isCss3Finish = !0
			}, c.options.slideSpeed)), c.transition3d(a)) : !0 === b ? c.css2slide(a, c.options.paginationSpeed) : "rewind" === b ? c.css2slide(a, c.options.rewindSpeed) : c.css2slide(a, c.options.slideSpeed);
			c.afterGo()
		},
		jumpTo: function (a) {
			"function" === typeof this.options.beforeMove && this.options.beforeMove.apply(this, [this.$elem]);
			a >= this.maximumItem || -1 === a ? a = this.maximumItem : 0 >= a && (a = 0);
			this.swapSpeed(0);
			!0 === this.browser.support3d ? this.transition3d(this.positionsInArray[a]) : this.css2slide(this.positionsInArray[a], 1);
			this.currentItem = this.owl.currentItem = a;
			this.afterGo()
		},
		afterGo: function () {
			this.prevArr.push(this.currentItem);
			this.prevItem = this.owl.prevItem = this.prevArr[this.prevArr.length - 2];
			this.prevArr.shift(0);
			this.prevItem !== this.currentItem && (this.checkPagination(), this.checkNavigation(), this.eachMoveUpdate(), !1 !== this.options.autoPlay && this.checkAp());
			"function" === typeof this.options.afterMove && this.prevItem !== this.currentItem && this.options.afterMove.apply(this, [this.$elem])
		},
		stop: function () {
			this.apStatus = "stop";
			g.clearInterval(this.autoPlayInterval)
		},
		checkAp: function () {
			"stop" !== this.apStatus && this.play()
		},
		play: function () {
			var a = this;
			a.apStatus = "play";
			if (!1 === a.options.autoPlay) return !1;
			g.clearInterval(a.autoPlayInterval);
			a.autoPlayInterval = g.setInterval(function () {
				a.next(!0)
			}, a.options.autoPlay)
		},
		swapSpeed: function (a) {
			"slideSpeed" === a ? this.$owlWrapper.css(this.addCssSpeed(this.options.slideSpeed)) : "paginationSpeed" === a ? this.$owlWrapper.css(this.addCssSpeed(this.options.paginationSpeed)) : "string" !== typeof a && this.$owlWrapper.css(this.addCssSpeed(a))
		},
		addCssSpeed: function (a) {
			return {
				"-webkit-transition": "all " + a + "ms ease",
				"-moz-transition": "all " + a + "ms ease",
				"-o-transition": "all " + a + "ms ease",
				transition: "all " + a + "ms ease"
			}
		},
		removeTransition: function () {
			return {
				"-webkit-transition": "",
				"-moz-transition": "",
				"-o-transition": "",
				transition: ""
			}
		},
		doTranslate: function (a) {
			return {
				"-webkit-transform": "translate3d(" + a + "px, 0px, 0px)",
				"-moz-transform": "translate3d(" + a + "px, 0px, 0px)",
				"-o-transform": "translate3d(" + a + "px, 0px, 0px)",
				"-ms-transform": "translate3d(" +
					a + "px, 0px, 0px)",
				transform: "translate3d(" + a + "px, 0px,0px)"
			}
		},
		transition3d: function (a) {
			this.$owlWrapper.css(this.doTranslate(a))
		},
		css2move: function (a) {
			this.$owlWrapper.css({
				left: a
			})
		},
		css2slide: function (a, b) {
			var e = this;
			e.isCssFinish = !1;
			e.$owlWrapper.stop(!0, !0).animate({
				left: a
			}, {
				duration: b || e.options.slideSpeed,
				complete: function () {
					e.isCssFinish = !0
				}
			})
		},
		checkBrowser: function () {
			var a = k.createElement("div");
			a.style.cssText = "  -moz-transform:translate3d(0px, 0px, 0px); -ms-transform:translate3d(0px, 0px, 0px); -o-transform:translate3d(0px, 0px, 0px); -webkit-transform:translate3d(0px, 0px, 0px); transform:translate3d(0px, 0px, 0px)";
			a = a.style.cssText.match(/translate3d\(0px, 0px, 0px\)/g);
			this.browser = {
				support3d: null !== a && 1 === a.length,
				isTouch: "ontouchstart" in g || g.navigator.msMaxTouchPoints
			}
		},
		moveEvents: function () {
			if (!1 !== this.options.mouseDrag || !1 !== this.options.touchDrag) this.gestures(), this.disabledEvents()
		},
		eventTypes: function () {
			var a = ["s", "e", "x"];
			this.ev_types = {};
			!0 === this.options.mouseDrag && !0 === this.options.touchDrag ? a = ["touchstart.owl mousedown.owl", "touchmove.owl mousemove.owl", "touchend.owl touchcancel.owl mouseup.owl"] : !1 === this.options.mouseDrag && !0 === this.options.touchDrag ? a = ["touchstart.owl", "touchmove.owl", "touchend.owl touchcancel.owl"] : !0 === this.options.mouseDrag && !1 === this.options.touchDrag && (a = ["mousedown.owl", "mousemove.owl", "mouseup.owl"]);
			this.ev_types.start = a[0];
			this.ev_types.move = a[1];
			this.ev_types.end = a[2]
		},
		disabledEvents: function () {
			this.$elem.on("dragstart.owl", function (a) {
				a.preventDefault()
			});
			this.$elem.on("mousedown.disableTextSelect", function (a) {
				return f(a.target).is("input, textarea, select, option")
			})
		},
		gestures: function () {
			function a(a) {
				if (void 0 !== a.touches) return {
					x: a.touches[0].pageX,
					y: a.touches[0].pageY
				};
				if (void 0 === a.touches) {
					if (void 0 !== a.pageX) return {
						x: a.pageX,
						y: a.pageY
					};
					if (void 0 === a.pageX) return {
						x: a.clientX,
						y: a.clientY
					}
				}
			}

			function b(a) {
				"on" === a ? (f(k).on(d.ev_types.move, e), f(k).on(d.ev_types.end, c)) : "off" === a && (f(k).off(d.ev_types.move), f(k).off(d.ev_types.end))
			}

			function e(b) {
				b = b.originalEvent || b || g.event;
				d.newPosX = a(b).x - h.offsetX;
				d.newPosY = a(b).y - h.offsetY;
				d.newRelativeX = d.newPosX - h.relativePos;
				"function" === typeof d.options.startDragging && !0 !== h.dragging && 0 !== d.newRelativeX && (h.dragging = !0, d.options.startDragging.apply(d, [d.$elem]));
				(8 < d.newRelativeX || -8 > d.newRelativeX) && !0 === d.browser.isTouch && (void 0 !== b.preventDefault ? b.preventDefault() : b.returnValue = !1, h.sliding = !0);
				(10 < d.newPosY || -10 > d.newPosY) && !1 === h.sliding && f(k).off("touchmove.owl");
				d.newPosX = Math.max(Math.min(d.newPosX, d.newRelativeX / 5), d.maximumPixels + d.newRelativeX / 5);
				!0 === d.browser.support3d ? d.transition3d(d.newPosX) : d.css2move(d.newPosX)
			}

			function c(a) {
				a = a.originalEvent || a || g.event;
				var c;
				a.target = a.target || a.srcElement;
				h.dragging = !1;
				!0 !== d.browser.isTouch && d.$owlWrapper.removeClass("grabbing");
				d.dragDirection = 0 > d.newRelativeX ? d.owl.dragDirection = "left" : d.owl.dragDirection = "right";
				0 !== d.newRelativeX && (c = d.getNewPosition(), d.goTo(c, !1, "drag"), h.targetElement === a.target && !0 !== d.browser.isTouch && (f(a.target).on("click.disable", function (a) {
					a.stopImmediatePropagation();
					a.stopPropagation();
					a.preventDefault();
					f(a.target).off("click.disable")
				}), a = f._data(a.target, "events").click, c = a.pop(), a.splice(0, 0, c)));
				b("off")
			}
			var d = this,
				h = {
					offsetX: 0,
					offsetY: 0,
					baseElWidth: 0,
					relativePos: 0,
					position: null,
					minSwipe: null,
					maxSwipe: null,
					sliding: null,
					dargging: null,
					targetElement: null
				};
			d.isCssFinish = !0;
			d.$elem.on(d.ev_types.start, ".owl-wrapper", function (c) {
				c = c.originalEvent || c || g.event;
				var e;
				if (3 === c.which) return !1;
				if (!(d.itemsAmount <= d.options.items)) {
					if (!1 === d.isCssFinish && !d.options.dragBeforeAnimFinish || !1 === d.isCss3Finish && !d.options.dragBeforeAnimFinish) return !1;
					!1 !== d.options.autoPlay && g.clearInterval(d.autoPlayInterval);
					!0 === d.browser.isTouch || d.$owlWrapper.hasClass("grabbing") || d.$owlWrapper.addClass("grabbing");
					d.newPosX = 0;
					d.newRelativeX = 0;
					f(this).css(d.removeTransition());
					e = f(this).position();
					h.relativePos = e.left;
					h.offsetX = a(c).x - e.left;
					h.offsetY = a(c).y - e.top;
					b("on");
					h.sliding = !1;
					h.targetElement = c.target || c.srcElement
				}
			})
		},
		getNewPosition: function () {
			var a = this.closestItem();
			a > this.maximumItem ? a = this.currentItem = this.maximumItem : 0 <= this.newPosX && (this.currentItem = a = 0);
			return a
		},
		closestItem: function () {
			var a = this,
				b = !0 === a.options.scrollPerPage ? a.pagesInArray : a.positionsInArray,
				e = a.newPosX,
				c = null;
			f.each(b, function (d, g) {
				e - a.itemWidth / 20 > b[d + 1] && e - a.itemWidth / 20 < g && "left" === a.moveDirection() ? (c = g, a.currentItem = !0 === a.options.scrollPerPage ? f.inArray(c, a.positionsInArray) : d) : e + a.itemWidth / 20 < g && e + a.itemWidth / 20 > (b[d + 1] || b[d] - a.itemWidth) && "right" === a.moveDirection() && (!0 === a.options.scrollPerPage ? (c = b[d + 1] || b[b.length - 1], a.currentItem = f.inArray(c, a.positionsInArray)) : (c = b[d + 1], a.currentItem = d + 1))
			});
			return a.currentItem
		},
		moveDirection: function () {
			var a;
			0 > this.newRelativeX ? (a = "right", this.playDirection = "next") : (a = "left", this.playDirection = "prev");
			return a
		},
		customEvents: function () {
			var a = this;
			a.$elem.on("owl.next", function () {
				a.next()
			});
			a.$elem.on("owl.prev", function () {
				a.prev()
			});
			a.$elem.on("owl.play", function (b, e) {
				a.options.autoPlay = e;
				a.play();
				a.hoverStatus = "play"
			});
			a.$elem.on("owl.stop", function () {
				a.stop();
				a.hoverStatus = "stop"
			});
			a.$elem.on("owl.goTo", function (b, e) {
				a.goTo(e)
			});
			a.$elem.on("owl.jumpTo", function (b, e) {
				a.jumpTo(e)
			})
		},
		stopOnHover: function () {
			var a = this;
			!0 === a.options.stopOnHover && !0 !== a.browser.isTouch && !1 !== a.options.autoPlay && (a.$elem.on("mouseover", function () {
				a.stop()
			}), a.$elem.on("mouseout", function () {
				"stop" !== a.hoverStatus && a.play()
			}))
		},
		lazyLoad: function () {
			var a, b, e, c, d;
			if (!1 === this.options.lazyLoad) return !1;
			for (a = 0; a < this.itemsAmount; a += 1) b = f(this.$owlItems[a]), "loaded" !== b.data("owl-loaded") && (e = b.data("owl-item"), c = b.find(".lazyOwl"), "string" !== typeof c.data("src") ? b.data("owl-loaded", "loaded") : (void 0 === b.data("owl-loaded") && (c.hide(), b.addClass("loading").data("owl-loaded", "checked")), (d = !0 === this.options.lazyFollow ? e >= this.currentItem : !0) && e < this.currentItem + this.options.items && c.length && this.lazyPreload(b, c)))
		},
		lazyPreload: function (a, b) {
			function e() {
				a.data("owl-loaded", "loaded").removeClass("loading");
				b.removeAttr("data-src");
				"fade" === d.options.lazyEffect ? b.fadeIn(400) : b.show();
				"function" === typeof d.options.afterLazyLoad && d.options.afterLazyLoad.apply(this, [d.$elem])
			}

			function c() {
				f += 1;
				d.completeImg(b.get(0)) || !0 === k ? e() : 100 >= f ? g.setTimeout(c, 100) : e()
			}
			var d = this,
				f = 0,
				k;
			"DIV" === b.prop("tagName") ? (b.css("background-image", "url(" + b.data("src") + ")"), k = !0) : b[0].src = b.data("src");
			c()
		},
		autoHeight: function () {
			function a() {
				var a = f(e.$owlItems[e.currentItem]).height();
				e.wrapperOuter.css("height", a + "px");
				e.wrapperOuter.hasClass("autoHeight") || g.setTimeout(function () {
					e.wrapperOuter.addClass("autoHeight")
				}, 0)
			}

			function b() {
				d += 1;
				e.completeImg(c.get(0)) ? a() : 100 >= d ? g.setTimeout(b, 100) : e.wrapperOuter.css("height", "")
			}
			var e = this,
				c = f(e.$owlItems[e.currentItem]).find("img"),
				d;
			void 0 !== c.get(0) ? (d = 0, b()) : a()
		},
		completeImg: function (a) {
			return !a.complete || "undefined" !== typeof a.naturalWidth && 0 === a.naturalWidth ? !1 : !0
		},
		onVisibleItems: function () {
			var a;
			!0 === this.options.addClassActive && this.$owlItems.removeClass("active");
			this.visibleItems = [];
			for (a = this.currentItem; a < this.currentItem + this.options.items; a += 1) this.visibleItems.push(a), !0 === this.options.addClassActive && f(this.$owlItems[a]).addClass("active");
			this.owl.visibleItems = this.visibleItems
		},
		transitionTypes: function (a) {
			this.outClass = "owl-" + a + "-out";
			this.inClass = "owl-" + a + "-in"
		},
		singleItemTransition: function () {
			var a = this,
				b = a.outClass,
				e = a.inClass,
				c = a.$owlItems.eq(a.currentItem),
				d = a.$owlItems.eq(a.prevItem),
				f = Math.abs(a.positionsInArray[a.currentItem]) + a.positionsInArray[a.prevItem],
				g = Math.abs(a.positionsInArray[a.currentItem]) + a.itemWidth / 2;
			a.isTransition = !0;
			a.$owlWrapper.addClass("owl-origin").css({
				"-webkit-transform-origin": g + "px",
				"-moz-perspective-origin": g +
					"px",
				"perspective-origin": g + "px"
			});
			d.css({
				position: "relative",
				left: f + "px"
			}).addClass(b).on("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend", function () {
				a.endPrev = !0;
				d.off("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend");
				a.clearTransStyle(d, b)
			});
			c.addClass(e).on("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend", function () {
				a.endCurrent = !0;
				c.off("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend");
				a.clearTransStyle(c, e)
			})
		},
		clearTransStyle: function (a, b) {
			a.css({
				position: "",
				left: ""
			}).removeClass(b);
			this.endPrev && this.endCurrent && (this.$owlWrapper.removeClass("owl-origin"), this.isTransition = this.endCurrent = this.endPrev = !1)
		},
		owlStatus: function () {
			this.owl = {
				userOptions: this.userOptions,
				baseElement: this.$elem,
				userItems: this.$userItems,
				owlItems: this.$owlItems,
				currentItem: this.currentItem,
				prevItem: this.prevItem,
				visibleItems: this.visibleItems,
				isTouch: this.browser.isTouch,
				browser: this.browser,
				dragDirection: this.dragDirection
			}
		},
		clearEvents: function () {
			this.$elem.off(".owl owl mousedown.disableTextSelect");
			f(k).off(".owl owl");
			f(g).off("resize", this.resizer)
		},
		unWrap: function () {
			0 !== this.$elem.children().length && (this.$owlWrapper.unwrap(), this.$userItems.unwrap().unwrap(), this.owlControls && this.owlControls.remove());
			this.clearEvents();
			this.$elem.attr("style", this.$elem.data("owl-originalStyles") || "").attr("class", this.$elem.data("owl-originalClasses"))
		},
		destroy: function () {
			this.stop();
			g.clearInterval(this.checkVisible);
			this.unWrap();
			this.$elem.removeData()
		},
		reinit: function (a) {
			a = f.extend({}, this.userOptions, a);
			this.unWrap();
			this.init(a, this.$elem)
		},
		addItem: function (a, b) {
			var e;
			if (!a) return !1;
			if (0 === this.$elem.children().length) return this.$elem.append(a), this.setVars(), !1;
			this.unWrap();
			e = void 0 === b || -1 === b ? -1 : b;
			e >= this.$userItems.length || -1 === e ? this.$userItems.eq(-1).after(a) : this.$userItems.eq(e).before(a);
			this.setVars()
		},
		removeItem: function (a) {
			if (0 === this.$elem.children().length) return !1;
			a = void 0 === a || -1 === a ? -1 : a;
			this.unWrap();
			this.$userItems.eq(a).remove();
			this.setVars()
		}
	};
	f.fn.owlCarousel = function (a) {
		return this.each(function () {
			if (!0 === f(this).data("owl-init")) return !1;
			f(this).data("owl-init", !0);
			var b = Object.create(l);
			b.init(a, this);
			f.data(this, "owlCarousel", b)
		})
	};
	f.fn.owlCarousel.options = {
		items: 5,
		itemsCustom: !1,
		itemsDesktop: [1199, 4],
		itemsDesktopSmall: [979, 3],
		itemsTablet: [768, 2],
		itemsTabletSmall: !1,
		itemsMobile: [479, 1],
		singleItem: !1,
		itemsScaleUp: !1,
		slideSpeed: 200,
		paginationSpeed: 800,
		rewindSpeed: 1E3,
		autoPlay: !1,
		stopOnHover: !1,
		navigation: !1,
		navigationText: ["prev", "next"],
		rewindNav: !0,
		scrollPerPage: !1,
		pagination: !0,
		paginationNumbers: !1,
		responsive: !0,
		responsiveRefreshRate: 200,
		responsiveBaseWidth: g,
		baseClass: "owl-carousel",
		theme: "owl-theme",
		lazyLoad: !1,
		lazyFollow: !0,
		lazyEffect: "fade",
		autoHeight: !1,
		jsonPath: !1,
		jsonSuccess: !1,
		dragBeforeAnimFinish: !0,
		mouseDrag: !0,
		touchDrag: !0,
		addClassActive: !1,
		transitionStyle: !1,
		beforeUpdate: !1,
		afterUpdate: !1,
		beforeInit: !1,
		afterInit: !1,
		beforeMove: !1,
		afterMove: !1,
		afterAction: !1,
		startDragging: !1,
		afterLazyLoad: !1
	}
})(jQuery, window, document);
/*! Magnific Popup - v1.0.0 - 2015-12-16
 * http://dimsemenov.com/plugins/magnific-popup/
 * Copyright (c) 2015 Dmitry Semenov; */
! function (a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : a("object" == typeof exports ? require("jquery") : window.jQuery || window.Zepto)
}(function (a) {
	var b, c, d, e, f, g, h = "Close",
		i = "BeforeClose",
		j = "AfterClose",
		k = "BeforeAppend",
		l = "MarkupParse",
		m = "Open",
		n = "Change",
		o = "mfp",
		p = "." + o,
		q = "mfp-ready",
		r = "mfp-removing",
		s = "mfp-prevent-close",
		t = function () {},
		u = !!window.jQuery,
		v = a(window),
		w = function (a, c) {
			b.ev.on(o + a + p, c)
		},
		x = function (b, c, d, e) {
			var f = document.createElement("div");
			return f.className = "mfp-" + b, d && (f.innerHTML = d), e ? c && c.appendChild(f) : (f = a(f), c && f.appendTo(c)), f
		},
		y = function (c, d) {
			b.ev.triggerHandler(o + c, d), b.st.callbacks && (c = c.charAt(0).toLowerCase() + c.slice(1), b.st.callbacks[c] && b.st.callbacks[c].apply(b, a.isArray(d) ? d : [d]))
		},
		z = function (c) {
			return c === g && b.currTemplate.closeBtn || (b.currTemplate.closeBtn = a(b.st.closeMarkup.replace("%title%", b.st.tClose)), g = c), b.currTemplate.closeBtn
		},
		A = function () {
			a.magnificPopup.instance || (b = new t, b.init(), a.magnificPopup.instance = b)
		},
		B = function () {
			var a = document.createElement("p").style,
				b = ["ms", "O", "Moz", "Webkit"];
			if (void 0 !== a.transition) return !0;
			for (; b.length;)
				if (b.pop() + "Transition" in a) return !0;
			return !1
		};
	t.prototype = {
		constructor: t,
		init: function () {
			var c = navigator.appVersion;
			b.isIE7 = -1 !== c.indexOf("MSIE 7."), b.isIE8 = -1 !== c.indexOf("MSIE 8."), b.isLowIE = b.isIE7 || b.isIE8, b.isAndroid = /android/gi.test(c), b.isIOS = /iphone|ipad|ipod/gi.test(c), b.supportsTransition = B(), b.probablyMobile = b.isAndroid || b.isIOS || /(Opera Mini)|Kindle|webOS|BlackBerry|(Opera Mobi)|(Windows Phone)|IEMobile/i.test(navigator.userAgent), d = a(document), b.popupsCache = {}
		},
		open: function (c) {
			var e;
			if (c.isObj === !1) {
				b.items = c.items.toArray(), b.index = 0;
				var g, h = c.items;
				for (e = 0; e < h.length; e++)
					if (g = h[e], g.parsed && (g = g.el[0]), g === c.el[0]) {
						b.index = e;
						break
					}
			} else b.items = a.isArray(c.items) ? c.items : [c.items], b.index = c.index || 0;
			if (b.isOpen) return void b.updateItemHTML();
			b.types = [], f = "", c.mainEl && c.mainEl.length ? b.ev = c.mainEl.eq(0) : b.ev = d, c.key ? (b.popupsCache[c.key] || (b.popupsCache[c.key] = {}), b.currTemplate = b.popupsCache[c.key]) : b.currTemplate = {}, b.st = a.extend(!0, {}, a.magnificPopup.defaults, c), b.fixedContentPos = "auto" === b.st.fixedContentPos ? !b.probablyMobile : b.st.fixedContentPos, b.st.modal && (b.st.closeOnContentClick = !1, b.st.closeOnBgClick = !1, b.st.showCloseBtn = !1, b.st.enableEscapeKey = !1), b.bgOverlay || (b.bgOverlay = x("bg").on("click" + p, function () {
				b.close()
			}), b.wrap = x("wrap").attr("tabindex", -1).on("click" + p, function (a) {
				b._checkIfClose(a.target) && b.close()
			}), b.container = x("container", b.wrap)), b.contentContainer = x("content"), b.st.preloader && (b.preloader = x("preloader", b.container, b.st.tLoading));
			var i = a.magnificPopup.modules;
			for (e = 0; e < i.length; e++) {
				var j = i[e];
				j = j.charAt(0).toUpperCase() + j.slice(1), b["init" + j].call(b)
			}
			y("BeforeOpen"), b.st.showCloseBtn && (b.st.closeBtnInside ? (w(l, function (a, b, c, d) {
				c.close_replaceWith = z(d.type)
			}), f += " mfp-close-btn-in") : b.wrap.append(z())), b.st.alignTop && (f += " mfp-align-top"), b.fixedContentPos ? b.wrap.css({
				overflow: b.st.overflowY,
				overflowX: "hidden",
				overflowY: b.st.overflowY
			}) : b.wrap.css({
				top: v.scrollTop(),
				position: "absolute"
			}), (b.st.fixedBgPos === !1 || "auto" === b.st.fixedBgPos && !b.fixedContentPos) && b.bgOverlay.css({
				height: d.height(),
				position: "absolute"
			}), b.st.enableEscapeKey && d.on("keyup" + p, function (a) {
				27 === a.keyCode && b.close()
			}), v.on("resize" + p, function () {
				b.updateSize()
			}), b.st.closeOnContentClick || (f += " mfp-auto-cursor"), f && b.wrap.addClass(f);
			var k = b.wH = v.height(),
				n = {};
			if (b.fixedContentPos && b._hasScrollBar(k)) {
				var o = b._getScrollbarSize();
				o && (n.marginRight = o)
			}
			b.fixedContentPos && (b.isIE7 ? a("body, html").css("overflow", "hidden") : n.overflow = "hidden");
			var r = b.st.mainClass;
			return b.isIE7 && (r += " mfp-ie7"), r && b._addClassToMFP(r), b.updateItemHTML(), y("BuildControls"), a("html").css(n), b.bgOverlay.add(b.wrap).prependTo(b.st.prependTo || a(document.body)), b._lastFocusedEl = document.activeElement, setTimeout(function () {
				b.content ? (b._addClassToMFP(q), b._setFocus()) : b.bgOverlay.addClass(q), d.on("focusin" + p, b._onFocusIn)
			}, 16), b.isOpen = !0, b.updateSize(k), y(m), c
		},
		close: function () {
			b.isOpen && (y(i), b.isOpen = !1, b.st.removalDelay && !b.isLowIE && b.supportsTransition ? (b._addClassToMFP(r), setTimeout(function () {
				b._close()
			}, b.st.removalDelay)) : b._close())
		},
		_close: function () {
			y(h);
			var c = r + " " + q + " ";
			if (b.bgOverlay.detach(), b.wrap.detach(), b.container.empty(), b.st.mainClass && (c += b.st.mainClass + " "), b._removeClassFromMFP(c), b.fixedContentPos) {
				var e = {
					marginRight: ""
				};
				b.isIE7 ? a("body, html").css("overflow", "") : e.overflow = "", a("html").css(e)
			}
			d.off("keyup" + p + " focusin" + p), b.ev.off(p), b.wrap.attr("class", "mfp-wrap").removeAttr("style"), b.bgOverlay.attr("class", "mfp-bg"), b.container.attr("class", "mfp-container"), !b.st.showCloseBtn || b.st.closeBtnInside && b.currTemplate[b.currItem.type] !== !0 || b.currTemplate.closeBtn && b.currTemplate.closeBtn.detach(), b.st.autoFocusLast && b._lastFocusedEl && a(b._lastFocusedEl).focus(), b.currItem = null, b.content = null, b.currTemplate = null, b.prevHeight = 0, y(j)
		},
		updateSize: function (a) {
			if (b.isIOS) {
				var c = document.documentElement.clientWidth / window.innerWidth,
					d = window.innerHeight * c;
				b.wrap.css("height", d), b.wH = d
			} else b.wH = a || v.height();
			b.fixedContentPos || b.wrap.css("height", b.wH), y("Resize")
		},
		updateItemHTML: function () {
			var c = b.items[b.index];
			b.contentContainer.detach(), b.content && b.content.detach(), c.parsed || (c = b.parseEl(b.index));
			var d = c.type;
			if (y("BeforeChange", [b.currItem ? b.currItem.type : "", d]), b.currItem = c, !b.currTemplate[d]) {
				var f = b.st[d] ? b.st[d].markup : !1;
				y("FirstMarkupParse", f), f ? b.currTemplate[d] = a(f) : b.currTemplate[d] = !0
			}
			e && e !== c.type && b.container.removeClass("mfp-" + e + "-holder");
			var g = b["get" + d.charAt(0).toUpperCase() + d.slice(1)](c, b.currTemplate[d]);
			b.appendContent(g, d), c.preloaded = !0, y(n, c), e = c.type, b.container.prepend(b.contentContainer), y("AfterChange")
		},
		appendContent: function (a, c) {
			b.content = a, a ? b.st.showCloseBtn && b.st.closeBtnInside && b.currTemplate[c] === !0 ? b.content.find(".mfp-close").length || b.content.append(z()) : b.content = a : b.content = "", y(k), b.container.addClass("mfp-" + c + "-holder"), b.contentContainer.append(b.content)
		},
		parseEl: function (c) {
			var d, e = b.items[c];
			if (e.tagName ? e = {
					el: a(e)
				} : (d = e.type, e = {
					data: e,
					src: e.src
				}), e.el) {
				for (var f = b.types, g = 0; g < f.length; g++)
					if (e.el.hasClass("mfp-" + f[g])) {
						d = f[g];
						break
					}
				e.src = e.el.attr("data-mfp-src"), e.src || (e.src = e.el.attr("href"))
			}
			return e.type = d || b.st.type || "inline", e.index = c, e.parsed = !0, b.items[c] = e, y("ElementParse", e), b.items[c]
		},
		addGroup: function (a, c) {
			var d = function (d) {
				d.mfpEl = this, b._openClick(d, a, c)
			};
			c || (c = {});
			var e = "click.magnificPopup";
			c.mainEl = a, c.items ? (c.isObj = !0, a.off(e).on(e, d)) : (c.isObj = !1, c.delegate ? a.off(e).on(e, c.delegate, d) : (c.items = a, a.off(e).on(e, d)))
		},
		_openClick: function (c, d, e) {
			var f = void 0 !== e.midClick ? e.midClick : a.magnificPopup.defaults.midClick;
			if (f || !(2 === c.which || c.ctrlKey || c.metaKey || c.altKey || c.shiftKey)) {
				var g = void 0 !== e.disableOn ? e.disableOn : a.magnificPopup.defaults.disableOn;
				if (g)
					if (a.isFunction(g)) {
						if (!g.call(b)) return !0
					} else if (v.width() < g) return !0;
				c.type && (c.preventDefault(), b.isOpen && c.stopPropagation()), e.el = a(c.mfpEl), e.delegate && (e.items = d.find(e.delegate)), b.open(e)
			}
		},
		updateStatus: function (a, d) {
			if (b.preloader) {
				c !== a && b.container.removeClass("mfp-s-" + c), d || "loading" !== a || (d = b.st.tLoading);
				var e = {
					status: a,
					text: d
				};
				y("UpdateStatus", e), a = e.status, d = e.text, b.preloader.html(d), b.preloader.find("a").on("click", function (a) {
					a.stopImmediatePropagation()
				}), b.container.addClass("mfp-s-" + a), c = a
			}
		},
		_checkIfClose: function (c) {
			if (!a(c).hasClass(s)) {
				var d = b.st.closeOnContentClick,
					e = b.st.closeOnBgClick;
				if (d && e) return !0;
				if (!b.content || a(c).hasClass("mfp-close") || b.preloader && c === b.preloader[0]) return !0;
				if (c === b.content[0] || a.contains(b.content[0], c)) {
					if (d) return !0
				} else if (e && a.contains(document, c)) return !0;
				return !1
			}
		},
		_addClassToMFP: function (a) {
			b.bgOverlay.addClass(a), b.wrap.addClass(a)
		},
		_removeClassFromMFP: function (a) {
			this.bgOverlay.removeClass(a), b.wrap.removeClass(a)
		},
		_hasScrollBar: function (a) {
			return (b.isIE7 ? d.height() : document.body.scrollHeight) > (a || v.height())
		},
		_setFocus: function () {
			(b.st.focus ? b.content.find(b.st.focus).eq(0) : b.wrap).focus()
		},
		_onFocusIn: function (c) {
			return c.target === b.wrap[0] || a.contains(b.wrap[0], c.target) ? void 0 : (b._setFocus(), !1)
		},
		_parseMarkup: function (b, c, d) {
			var e;
			d.data && (c = a.extend(d.data, c)), y(l, [b, c, d]), a.each(c, function (a, c) {
				if (void 0 === c || c === !1) return !0;
				if (e = a.split("_"), e.length > 1) {
					var d = b.find(p + "-" + e[0]);
					if (d.length > 0) {
						var f = e[1];
						"replaceWith" === f ? d[0] !== c[0] && d.replaceWith(c) : "img" === f ? d.is("img") ? d.attr("src", c) : d.replaceWith('<img src="' + c + '" class="' + d.attr("class") + '" />') : d.attr(e[1], c)
					}
				} else b.find(p + "-" + a).html(c)
			})
		},
		_getScrollbarSize: function () {
			if (void 0 === b.scrollbarSize) {
				var a = document.createElement("div");
				a.style.cssText = "width: 99px; height: 99px; overflow: scroll; position: absolute; top: -9999px;", document.body.appendChild(a), b.scrollbarSize = a.offsetWidth - a.clientWidth, document.body.removeChild(a)
			}
			return b.scrollbarSize
		}
	}, a.magnificPopup = {
		instance: null,
		proto: t.prototype,
		modules: [],
		open: function (b, c) {
			return A(), b = b ? a.extend(!0, {}, b) : {}, b.isObj = !0, b.index = c || 0, this.instance.open(b)
		},
		close: function () {
			return a.magnificPopup.instance && a.magnificPopup.instance.close()
		},
		registerModule: function (b, c) {
			c.options && (a.magnificPopup.defaults[b] = c.options), a.extend(this.proto, c.proto), this.modules.push(b)
		},
		defaults: {
			disableOn: 0,
			key: null,
			midClick: !1,
			mainClass: "",
			preloader: !0,
			focus: "",
			closeOnContentClick: !1,
			closeOnBgClick: !0,
			closeBtnInside: !0,
			showCloseBtn: !0,
			enableEscapeKey: !0,
			modal: !1,
			alignTop: !1,
			removalDelay: 0,
			prependTo: null,
			fixedContentPos: "auto",
			fixedBgPos: "auto",
			overflowY: "auto",
			closeMarkup: '<button title="%title%" type="button" class="mfp-close">&#215;</button>',
			tClose: "Close (Esc)",
			tLoading: "Loading...",
			autoFocusLast: !0
		}
	}, a.fn.magnificPopup = function (c) {
		A();
		var d = a(this);
		if ("string" == typeof c)
			if ("open" === c) {
				var e, f = u ? d.data("magnificPopup") : d[0].magnificPopup,
					g = parseInt(arguments[1], 10) || 0;
				f.items ? e = f.items[g] : (e = d, f.delegate && (e = e.find(f.delegate)), e = e.eq(g)), b._openClick({
					mfpEl: e
				}, d, f)
			} else b.isOpen && b[c].apply(b, Array.prototype.slice.call(arguments, 1));
		else c = a.extend(!0, {}, c), u ? d.data("magnificPopup", c) : d[0].magnificPopup = c, b.addGroup(d, c);
		return d
	};
	var C, D, E, F = "inline",
		G = function () {
			E && (D.after(E.addClass(C)).detach(), E = null)
		};
	a.magnificPopup.registerModule(F, {
		options: {
			hiddenClass: "hide",
			markup: "",
			tNotFound: "Content not found"
		},
		proto: {
			initInline: function () {
				b.types.push(F), w(h + "." + F, function () {
					G()
				})
			},
			getInline: function (c, d) {
				if (G(), c.src) {
					var e = b.st.inline,
						f = a(c.src);
					if (f.length) {
						var g = f[0].parentNode;
						g && g.tagName && (D || (C = e.hiddenClass, D = x(C), C = "mfp-" + C), E = f.after(D).detach().removeClass(C)), b.updateStatus("ready")
					} else b.updateStatus("error", e.tNotFound), f = a("<div>");
					return c.inlineElement = f, f
				}
				return b.updateStatus("ready"), b._parseMarkup(d, {}, c), d
			}
		}
	});
	var H, I = "ajax",
		J = function () {
			H && a(document.body).removeClass(H)
		},
		K = function () {
			J(), b.req && b.req.abort()
		};
	a.magnificPopup.registerModule(I, {
		options: {
			settings: null,
			cursor: "mfp-ajax-cur",
			tError: '<a href="%url%">The content</a> could not be loaded.'
		},
		proto: {
			initAjax: function () {
				b.types.push(I), H = b.st.ajax.cursor, w(h + "." + I, K), w("BeforeChange." + I, K)
			},
			getAjax: function (c) {
				H && a(document.body).addClass(H), b.updateStatus("loading");
				var d = a.extend({
					url: c.src,
					success: function (d, e, f) {
						var g = {
							data: d,
							xhr: f
						};
						y("ParseAjax", g), b.appendContent(a(g.data), I), c.finished = !0, J(), b._setFocus(), setTimeout(function () {
							b.wrap.addClass(q)
						}, 16), b.updateStatus("ready"), y("AjaxContentAdded")
					},
					error: function () {
						J(), c.finished = c.loadError = !0, b.updateStatus("error", b.st.ajax.tError.replace("%url%", c.src))
					}
				}, b.st.ajax.settings);
				return b.req = a.ajax(d), ""
			}
		}
	});
	var L, M = function (c) {
		if (c.data && void 0 !== c.data.title) return c.data.title;
		var d = b.st.image.titleSrc;
		if (d) {
			if (a.isFunction(d)) return d.call(b, c);
			if (c.el) return c.el.attr(d) || ""
		}
		return ""
	};
	a.magnificPopup.registerModule("image", {
		options: {
			markup: '<div class="mfp-figure"><div class="mfp-close"></div><figure><div class="mfp-img"></div><figcaption><div class="mfp-bottom-bar"><div class="mfp-title"></div><div class="mfp-counter"></div></div></figcaption></figure></div>',
			cursor: "mfp-zoom-out-cur",
			titleSrc: "title",
			verticalFit: !0,
			tError: '<a href="%url%">The image</a> could not be loaded.'
		},
		proto: {
			initImage: function () {
				var c = b.st.image,
					d = ".image";
				b.types.push("image"), w(m + d, function () {
					"image" === b.currItem.type && c.cursor && a(document.body).addClass(c.cursor)
				}), w(h + d, function () {
					c.cursor && a(document.body).removeClass(c.cursor), v.off("resize" + p)
				}), w("Resize" + d, b.resizeImage), b.isLowIE && w("AfterChange", b.resizeImage)
			},
			resizeImage: function () {
				var a = b.currItem;
				if (a && a.img && b.st.image.verticalFit) {
					var c = 0;
					b.isLowIE && (c = parseInt(a.img.css("padding-top"), 10) + parseInt(a.img.css("padding-bottom"), 10)), a.img.css("max-height", b.wH - c)
				}
			},
			_onImageHasSize: function (a) {
				a.img && (a.hasSize = !0, L && clearInterval(L), a.isCheckingImgSize = !1, y("ImageHasSize", a), a.imgHidden && (b.content && b.content.removeClass("mfp-loading"), a.imgHidden = !1))
			},
			findImageSize: function (a) {
				var c = 0,
					d = a.img[0],
					e = function (f) {
						L && clearInterval(L), L = setInterval(function () {
							return d.naturalWidth > 0 ? void b._onImageHasSize(a) : (c > 200 && clearInterval(L), c++, void(3 === c ? e(10) : 40 === c ? e(50) : 100 === c && e(500)))
						}, f)
					};
				e(1)
			},
			getImage: function (c, d) {
				var e = 0,
					f = function () {
						c && (c.img[0].complete ? (c.img.off(".mfploader"), c === b.currItem && (b._onImageHasSize(c), b.updateStatus("ready")), c.hasSize = !0, c.loaded = !0, y("ImageLoadComplete")) : (e++, 200 > e ? setTimeout(f, 100) : g()))
					},
					g = function () {
						c && (c.img.off(".mfploader"), c === b.currItem && (b._onImageHasSize(c), b.updateStatus("error", h.tError.replace("%url%", c.src))), c.hasSize = !0, c.loaded = !0, c.loadError = !0)
					},
					h = b.st.image,
					i = d.find(".mfp-img");
				if (i.length) {
					var j = document.createElement("img");
					j.className = "mfp-img", c.el && c.el.find("img").length && (j.alt = c.el.find("img").attr("alt")), c.img = a(j).on("load.mfploader", f).on("error.mfploader", g), j.src = c.src, i.is("img") && (c.img = c.img.clone()), j = c.img[0], j.naturalWidth > 0 ? c.hasSize = !0 : j.width || (c.hasSize = !1)
				}
				return b._parseMarkup(d, {
					title: M(c),
					img_replaceWith: c.img
				}, c), b.resizeImage(), c.hasSize ? (L && clearInterval(L), c.loadError ? (d.addClass("mfp-loading"), b.updateStatus("error", h.tError.replace("%url%", c.src))) : (d.removeClass("mfp-loading"), b.updateStatus("ready")), d) : (b.updateStatus("loading"), c.loading = !0, c.hasSize || (c.imgHidden = !0, d.addClass("mfp-loading"), b.findImageSize(c)), d)
			}
		}
	});
	var N, O = function () {
		return void 0 === N && (N = void 0 !== document.createElement("p").style.MozTransform), N
	};
	a.magnificPopup.registerModule("zoom", {
		options: {
			enabled: !1,
			easing: "ease-in-out",
			duration: 300,
			opener: function (a) {
				return a.is("img") ? a : a.find("img")
			}
		},
		proto: {
			initZoom: function () {
				var a, c = b.st.zoom,
					d = ".zoom";
				if (c.enabled && b.supportsTransition) {
					var e, f, g = c.duration,
						j = function (a) {
							var b = a.clone().removeAttr("style").removeAttr("class").addClass("mfp-animated-image"),
								d = "all " + c.duration / 1e3 + "s " + c.easing,
								e = {
									position: "fixed",
									zIndex: 9999,
									left: 0,
									top: 0,
									"-webkit-backface-visibility": "hidden"
								},
								f = "transition";
							return e["-webkit-" + f] = e["-moz-" + f] = e["-o-" + f] = e[f] = d, b.css(e), b
						},
						k = function () {
							b.content.css("visibility", "visible")
						};
					w("BuildControls" + d, function () {
						if (b._allowZoom()) {
							if (clearTimeout(e), b.content.css("visibility", "hidden"), a = b._getItemToZoom(), !a) return void k();
							f = j(a), f.css(b._getOffset()), b.wrap.append(f), e = setTimeout(function () {
								f.css(b._getOffset(!0)), e = setTimeout(function () {
									k(), setTimeout(function () {
										f.remove(), a = f = null, y("ZoomAnimationEnded")
									}, 16)
								}, g)
							}, 16)
						}
					}), w(i + d, function () {
						if (b._allowZoom()) {
							if (clearTimeout(e), b.st.removalDelay = g, !a) {
								if (a = b._getItemToZoom(), !a) return;
								f = j(a)
							}
							f.css(b._getOffset(!0)), b.wrap.append(f), b.content.css("visibility", "hidden"), setTimeout(function () {
								f.css(b._getOffset())
							}, 16)
						}
					}), w(h + d, function () {
						b._allowZoom() && (k(), f && f.remove(), a = null)
					})
				}
			},
			_allowZoom: function () {
				return "image" === b.currItem.type
			},
			_getItemToZoom: function () {
				return b.currItem.hasSize ? b.currItem.img : !1
			},
			_getOffset: function (c) {
				var d;
				d = c ? b.currItem.img : b.st.zoom.opener(b.currItem.el || b.currItem);
				var e = d.offset(),
					f = parseInt(d.css("padding-top"), 10),
					g = parseInt(d.css("padding-bottom"), 10);
				e.top -= a(window).scrollTop() - f;
				var h = {
					width: d.width(),
					height: (u ? d.innerHeight() : d[0].offsetHeight) - g - f
				};
				return O() ? h["-moz-transform"] = h.transform = "translate(" + e.left + "px," + e.top + "px)" : (h.left = e.left, h.top = e.top), h
			}
		}
	});
	var P = "iframe",
		Q = "//about:blank",
		R = function (a) {
			if (b.currTemplate[P]) {
				var c = b.currTemplate[P].find("iframe");
				c.length && (a || (c[0].src = Q), b.isIE8 && c.css("display", a ? "block" : "none"))
			}
		};
	a.magnificPopup.registerModule(P, {
		options: {
			markup: '<div class="mfp-iframe-scaler"><div class="mfp-close"></div><iframe class="mfp-iframe" src="//about:blank" frameborder="0" allowfullscreen></iframe></div>',
			srcAction: "iframe_src",
			patterns: {
				youtube: {
					index: "youtube.com",
					id: "v=",
					src: "//www.youtube.com/embed/%id%?autoplay=1"
				},
				vimeo: {
					index: "vimeo.com/",
					id: "/",
					src: "//player.vimeo.com/video/%id%?autoplay=1"
				},
				gmaps: {
					index: "//maps.google.",
					src: "%id%&output=embed"
				}
			}
		},
		proto: {
			initIframe: function () {
				b.types.push(P), w("BeforeChange", function (a, b, c) {
					b !== c && (b === P ? R() : c === P && R(!0))
				}), w(h + "." + P, function () {
					R()
				})
			},
			getIframe: function (c, d) {
				var e = c.src,
					f = b.st.iframe;
				a.each(f.patterns, function () {
					return e.indexOf(this.index) > -1 ? (this.id && (e = "string" == typeof this.id ? e.substr(e.lastIndexOf(this.id) + this.id.length, e.length) : this.id.call(this, e)), e = this.src.replace("%id%", e), !1) : void 0
				});
				var g = {};
				return f.srcAction && (g[f.srcAction] = e), b._parseMarkup(d, g, c), b.updateStatus("ready"), d
			}
		}
	});
	var S = function (a) {
			var c = b.items.length;
			return a > c - 1 ? a - c : 0 > a ? c + a : a
		},
		T = function (a, b, c) {
			return a.replace(/%curr%/gi, b + 1).replace(/%total%/gi, c)
		};
	a.magnificPopup.registerModule("gallery", {
		options: {
			enabled: !1,
			arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"></button>',
			preload: [0, 2],
			navigateByImgClick: !0,
			arrows: !0,
			tPrev: "Previous (Left arrow key)",
			tNext: "Next (Right arrow key)",
			tCounter: "%curr% of %total%"
		},
		proto: {
			initGallery: function () {
				var c = b.st.gallery,
					e = ".mfp-gallery",
					g = Boolean(a.fn.mfpFastClick);
				return b.direction = !0, c && c.enabled ? (f += " mfp-gallery", w(m + e, function () {
					c.navigateByImgClick && b.wrap.on("click" + e, ".mfp-img", function () {
						return b.items.length > 1 ? (b.next(), !1) : void 0
					}), d.on("keydown" + e, function (a) {
						37 === a.keyCode ? b.prev() : 39 === a.keyCode && b.next()
					})
				}), w("UpdateStatus" + e, function (a, c) {
					c.text && (c.text = T(c.text, b.currItem.index, b.items.length))
				}), w(l + e, function (a, d, e, f) {
					var g = b.items.length;
					e.counter = g > 1 ? T(c.tCounter, f.index, g) : ""
				}), w("BuildControls" + e, function () {
					if (b.items.length > 1 && c.arrows && !b.arrowLeft) {
						var d = c.arrowMarkup,
							e = b.arrowLeft = a(d.replace(/%title%/gi, c.tPrev).replace(/%dir%/gi, "left")).addClass(s),
							f = b.arrowRight = a(d.replace(/%title%/gi, c.tNext).replace(/%dir%/gi, "right")).addClass(s),
							h = g ? "mfpFastClick" : "click";
						e[h](function () {
							b.prev()
						}), f[h](function () {
							b.next()
						}), b.isIE7 && (x("b", e[0], !1, !0), x("a", e[0], !1, !0), x("b", f[0], !1, !0), x("a", f[0], !1, !0)), b.container.append(e.add(f))
					}
				}), w(n + e, function () {
					b._preloadTimeout && clearTimeout(b._preloadTimeout), b._preloadTimeout = setTimeout(function () {
						b.preloadNearbyImages(), b._preloadTimeout = null
					}, 16)
				}), void w(h + e, function () {
					d.off(e), b.wrap.off("click" + e), b.arrowLeft && g && b.arrowLeft.add(b.arrowRight).destroyMfpFastClick(), b.arrowRight = b.arrowLeft = null
				})) : !1
			},
			next: function () {
				b.direction = !0, b.index = S(b.index + 1), b.updateItemHTML()
			},
			prev: function () {
				b.direction = !1, b.index = S(b.index - 1), b.updateItemHTML()
			},
			goTo: function (a) {
				b.direction = a >= b.index, b.index = a, b.updateItemHTML()
			},
			preloadNearbyImages: function () {
				var a, c = b.st.gallery.preload,
					d = Math.min(c[0], b.items.length),
					e = Math.min(c[1], b.items.length);
				for (a = 1; a <= (b.direction ? e : d); a++) b._preloadItem(b.index + a);
				for (a = 1; a <= (b.direction ? d : e); a++) b._preloadItem(b.index - a)
			},
			_preloadItem: function (c) {
				if (c = S(c), !b.items[c].preloaded) {
					var d = b.items[c];
					d.parsed || (d = b.parseEl(c)), y("LazyLoad", d), "image" === d.type && (d.img = a('<img class="mfp-img" />').on("load.mfploader", function () {
						d.hasSize = !0
					}).on("error.mfploader", function () {
						d.hasSize = !0, d.loadError = !0, y("LazyLoadError", d)
					}).attr("src", d.src)), d.preloaded = !0
				}
			}
		}
	});
	var U = "retina";
	a.magnificPopup.registerModule(U, {
			options: {
				replaceSrc: function (a) {
					return a.src.replace(/\.\w+$/, function (a) {
						return "@2x" + a
					})
				},
				ratio: 1
			},
			proto: {
				initRetina: function () {
					if (window.devicePixelRatio > 1) {
						var a = b.st.retina,
							c = a.ratio;
						c = isNaN(c) ? c() : c, c > 1 && (w("ImageHasSize." + U, function (a, b) {
							b.img.css({
								"max-width": b.img[0].naturalWidth / c,
								width: "100%"
							})
						}), w("ElementParse." + U, function (b, d) {
							d.src = a.replaceSrc(d, c)
						}))
					}
				}
			}
		}),
		function () {
			var b = 1e3,
				c = "ontouchstart" in window,
				d = function () {
					v.off("touchmove" + f + " touchend" + f)
				},
				e = "mfpFastClick",
				f = "." + e;
			a.fn.mfpFastClick = function (e) {
				return a(this).each(function () {
					var g, h = a(this);
					if (c) {
						var i, j, k, l, m, n;
						h.on("touchstart" + f, function (a) {
							l = !1, n = 1, m = a.originalEvent ? a.originalEvent.touches[0] : a.touches[0], j = m.clientX, k = m.clientY, v.on("touchmove" + f, function (a) {
								m = a.originalEvent ? a.originalEvent.touches : a.touches, n = m.length, m = m[0], (Math.abs(m.clientX - j) > 10 || Math.abs(m.clientY - k) > 10) && (l = !0, d())
							}).on("touchend" + f, function (a) {
								d(), l || n > 1 || (g = !0, a.preventDefault(), clearTimeout(i), i = setTimeout(function () {
									g = !1
								}, b), e())
							})
						})
					}
					h.on("click" + f, function () {
						g || e()
					})
				})
			}, a.fn.destroyMfpFastClick = function () {
				a(this).off("touchstart" + f + " click" + f), c && v.off("touchmove" + f + " touchend" + f)
			}
		}(), A()
});
! function (a) {
	var b = {
		init: function () {
			var b = a(document);
			b.on("click", ".event-load-booking-form", this.load_form_register), b.on("submit", "form.event_register:not(.active)", this.book_event_form), this.sanitize_form_field(), b.on("submit", "#event-lightbox .event-auth-form", this.ajax_login)
		},
		load_form_register: function (c) {
			c.preventDefault();
			var d = a(this),
				e = d.attr("data-event");
			return a.ajax({
				url: WPEMS.ajaxurl,
				type: "POST",
				dataType: "html",
				async: !1,
				data: {
					event_id: e,
					nonce: WPEMS.register_button,
					action: "load_form_register"
				},
				beforeSend: function () {
					d.append('<i class="event-icon-spinner2 spinner"></i>')
				}
			}).always(function () {
				d.find(".event-icon-spinner2").remove()
			}).done(function (a) {
				b.lightbox(a)
			}).fail(function () {}), !1
		},
		book_event_form: function (c) {
			c.preventDefault();
			var d = a(this),
				e = d.serializeArray(),
				f = d.find('button[type="submit"]'),
				g = d.find(".tp-event-notice");
			return a.ajax({
				url: WPEMS.ajaxurl,
				type: "POST",
				data: e,
				dataType: "json",
				beforeSend: function () {
					g.slideUp().remove(), f.addClass("event-register-loading"), d.addClass("active")
				}
			}).done(function (c) {
				return f.removeClass("event-register-loading"), "undefined" == typeof c.status ? void b.set_message(d, WPEMS.something_wrong) : (c.status === !0 && "undefined" != typeof c.url && "" !== c.url && (window.location.href = c.url), c.status === !0 && "" == c.url && "undefined" != typeof c.event && (a.magnificPopup.close(), a(".woocommerce-message").hide(), setTimeout(function () {
					a(".entry-register, .event_register_foot").append('<div class="woocommerce-message">' + WPEMS.woo_cart_url + "<p>“" + c.event + "”" + WPEMS.add_to_cart + "</p></div>")
				}, 100)), "undefined" != typeof c.message ? void b.set_message(d, c.message) : void 0)
			}).fail(function () {
				f.removeClass("event-register-loading"), b.set_message(d, WPEMS.something_wrong)
			}).always(function () {
				d.removeClass("active")
			}), !1
		},
		set_message: function (a, b) {
			var c = '<div class="tp-event-notice error">';
			c += '<div class="event_auth_register_message_error">' + b + "</div>", c += "</div>", a.find(".event_register_foot").append(c)
		},
		sanitize_form_field: function () {
			for (var b = a(".form-row.form-required"), c = 0; c < b.length; c++) {
				var d = a(b[c]),
					e = d.find("input");
				e.on("blur", function (b) {
					b.preventDefault();
					var c = a(this),
						d = c.parents(".form-row:first");
					d.hasClass("form-required") && ("" == c.val() ? d.removeClass("validated").addClass("has-error") : d.removeClass("has-error").addClass("validated"))
				})
			}
		},
		lightbox: function (c) {
			var d = [];
			d.push('<div id="event-lightbox">'), d.push(c), d.push("</div>"), a.magnificPopup.open({
				items: {
					type: "inline",
					src: a(d.join(""))
				},
				mainClass: "event-lightbox-wrap",
				callbacks: {
					open: function () {
						var c = a("#event-lightbox");
						c.addClass("event-fade");
						var d = setTimeout(function () {
							c.addClass("event-in"), clearTimeout(d), b.sanitize_form_field()
						}, 100)
					},
					close: function () {
						var b = a("#event-lightbox");
						b.remove()
					}
				}
			})
		},
		ajax_login: function (b) {
			b.preventDefault();
			var c = a(this),
				d = c.find("#wp-submit"),
				e = a("#event-lightbox"),
				f = c.serializeArray();
			return a.ajax({
				url: WPEMS.ajaxurl,
				type: "POST",
				data: f,
				async: !1,
				beforeSend: function () {
					e.find(".tp-event-notice").slideUp().remove(), d.addClass("event-register-loading")
				}
			}).always(function () {
				d.find(".event-icon-spinner2").remove()
			}).done(function (a) {
				"undefined" != typeof a.notices && c.before(a.notices), "undefined" != typeof a.status && a.status === !0 && ("undefined" != typeof a.redirect && a.redirect ? window.location.href = a.redirect : window.location.reload())
			}).fail(function (a, b, d) {
				var e = '<ul class="tp-event-notice error">';
				e += "<li>" + a + "</li>", e += "</ul>", c.before(res.notices)
			}), !1
		}
	};
	a(document).ready(function () {
		b.init();
		for (var c = a(".tp_event_counter"), d = 0; d < c.length; d++) {
			var e = a(c[d]).attr("data-time");
			e = new Date(e);
			var f = new Date(e - 60 * WPEMS.gmt_offset * 60 * 1e3);
			a(c[d]).countdown({
				labels: WPEMS.l18n.labels,
				labels1: WPEMS.l18n.label1,
				until: f,
				serverSync: WPEMS.current_time
			})
		}
		for (var g = a(".tp_event_owl_carousel"), d = 0; d < g.length; d++) {
			var h = a(g[d]).attr("data-countdown"),
				i = {
					navigation: !0,
					slideSpeed: 300,
					paginationSpeed: 400,
					singleItem: !0
				};
			"undefined" != typeof h && (h = JSON.parse(h), a.extend(i, h), a.each(i, function (a, b) {
				"true" === b ? i[a] = !0 : "false" === b && (i[a] = !1)
			})), "undefined" == typeof i.slide || i.slide === !0 ? a(g[d]).owlCarousel(i) : a(g[d]).removeClass("owl-carousel")
		}
	})
}(jQuery);
if ("undefined" == typeof jQuery) throw new Error("Bootstrap's JavaScript requires jQuery"); + function (t) {
	"use strict";

	function e() {
		var t = document.createElement("bootstrap"),
			e = {
				WebkitTransition: "webkitTransitionEnd",
				MozTransition: "transitionend",
				OTransition: "oTransitionEnd otransitionend",
				transition: "transitionend"
			};
		for (var i in e)
			if (void 0 !== t.style[i]) return {
				end: e[i]
			};
		return !1
	}
	t.fn.emulateTransitionEnd = function (e) {
		var i = !1,
			o = this;
		t(this).one("bsTransitionEnd", function () {
			i = !0
		});
		var s = function () {
			i || t(o).trigger(t.support.transition.end)
		};
		return setTimeout(s, e), this
	}, t(function () {
		t.support.transition = e(), t.support.transition && (t.event.special.bsTransitionEnd = {
			bindType: t.support.transition.end,
			delegateType: t.support.transition.end,
			handle: function (e) {
				return t(e.target).is(this) ? e.handleObj.handler.apply(this, arguments) : void 0
			}
		})
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var i = t(this),
				s = i.data("bs.alert");
			s || i.data("bs.alert", s = new o(this)), "string" == typeof e && s[e].call(i)
		})
	}
	var i = '[data-dismiss="alert"]',
		o = function (e) {
			t(e).on("click", i, this.close)
		};
	o.VERSION = "3.2.0", o.prototype.close = function (e) {
		function i() {
			n.detach().trigger("closed.bs.alert").remove()
		}
		var o = t(this),
			s = o.attr("data-target");
		s || (s = o.attr("href"), s = s && s.replace(/.*(?=#[^\s]*$)/, ""));
		var n = t(s);
		e && e.preventDefault(), n.length || (n = o.hasClass("alert") ? o : o.parent()), n.trigger(e = t.Event("close.bs.alert")), e.isDefaultPrevented() || (n.removeClass("in"), t.support.transition && n.hasClass("fade") ? n.one("bsTransitionEnd", i).emulateTransitionEnd(150) : i())
	};
	var s = t.fn.alert;
	t.fn.alert = e, t.fn.alert.Constructor = o, t.fn.alert.noConflict = function () {
		return t.fn.alert = s, this
	}, t(document).on("click.bs.alert.data-api", i, o.prototype.close)
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.button"),
				n = "object" == typeof e && e;
			s || o.data("bs.button", s = new i(this, n)), "toggle" == e ? s.toggle() : e && s.setState(e)
		})
	}
	var i = function (e, o) {
		this.$element = t(e), this.options = t.extend({}, i.DEFAULTS, o), this.isLoading = !1
	};
	i.VERSION = "3.2.0", i.DEFAULTS = {
		loadingText: "loading..."
	}, i.prototype.setState = function (e) {
		var i = "disabled",
			o = this.$element,
			s = o.is("input") ? "val" : "html",
			n = o.data();
		e += "Text", null == n.resetText && o.data("resetText", o[s]()), o[s](null == n[e] ? this.options[e] : n[e]), setTimeout(t.proxy(function () {
			"loadingText" == e ? (this.isLoading = !0, o.addClass(i).attr(i, i)) : this.isLoading && (this.isLoading = !1, o.removeClass(i).removeAttr(i))
		}, this), 0)
	}, i.prototype.toggle = function () {
		var t = !0,
			e = this.$element.closest('[data-toggle="buttons"]');
		if (e.length) {
			var i = this.$element.find("input");
			"radio" == i.prop("type") && (i.prop("checked") && this.$element.hasClass("active") ? t = !1 : e.find(".active").removeClass("active")), t && i.prop("checked", !this.$element.hasClass("active")).trigger("change")
		}
		t && this.$element.toggleClass("active")
	};
	var o = t.fn.button;
	t.fn.button = e, t.fn.button.Constructor = i, t.fn.button.noConflict = function () {
		return t.fn.button = o, this
	}, t(document).on("click.bs.button.data-api", '[data-toggle^="button"]', function (i) {
		var o = t(i.target);
		o.hasClass("btn") || (o = o.closest(".btn")), e.call(o, "toggle"), i.preventDefault()
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.carousel"),
				n = t.extend({}, i.DEFAULTS, o.data(), "object" == typeof e && e),
				r = "string" == typeof e ? e : n.slide;
			s || o.data("bs.carousel", s = new i(this, n)), "number" == typeof e ? s.to(e) : r ? s[r]() : n.interval && s.pause().cycle()
		})
	}
	var i = function (e, i) {
		this.$element = t(e).on("keydown.bs.carousel", t.proxy(this.keydown, this)), this.$indicators = this.$element.find(".carousel-indicators"), this.options = i, this.paused = this.sliding = this.interval = this.$active = this.$items = null, "hover" == this.options.pause && this.$element.on("mouseenter.bs.carousel", t.proxy(this.pause, this)).on("mouseleave.bs.carousel", t.proxy(this.cycle, this))
	};
	i.VERSION = "3.2.0", i.DEFAULTS = {
		interval: 5e3,
		pause: "hover",
		wrap: !0
	}, i.prototype.keydown = function (t) {
		switch (t.which) {
			case 37:
				this.prev();
				break;
			case 39:
				this.next();
				break;
			default:
				return
		}
		t.preventDefault()
	}, i.prototype.cycle = function (e) {
		return e || (this.paused = !1), this.interval && clearInterval(this.interval), this.options.interval && !this.paused && (this.interval = setInterval(t.proxy(this.next, this), this.options.interval)), this
	}, i.prototype.getItemIndex = function (t) {
		return this.$items = t.parent().children(".item"), this.$items.index(t || this.$active)
	}, i.prototype.to = function (e) {
		var i = this,
			o = this.getItemIndex(this.$active = this.$element.find(".item.active"));
		return e > this.$items.length - 1 || 0 > e ? void 0 : this.sliding ? this.$element.one("slid.bs.carousel", function () {
			i.to(e)
		}) : o == e ? this.pause().cycle() : this.slide(e > o ? "next" : "prev", t(this.$items[e]))
	}, i.prototype.pause = function (e) {
		return e || (this.paused = !0), this.$element.find(".next, .prev").length && t.support.transition && (this.$element.trigger(t.support.transition.end), this.cycle(!0)), this.interval = clearInterval(this.interval), this
	}, i.prototype.next = function () {
		return this.sliding ? void 0 : this.slide("next")
	}, i.prototype.prev = function () {
		return this.sliding ? void 0 : this.slide("prev")
	}, i.prototype.slide = function (e, i) {
		var o = this.$element.find(".item.active"),
			s = i || o[e](),
			n = this.interval,
			r = "next" == e ? "left" : "right",
			a = "next" == e ? "first" : "last",
			l = this;
		if (!s.length) {
			if (!this.options.wrap) return;
			s = this.$element.find(".item")[a]()
		}
		if (s.hasClass("active")) return this.sliding = !1;
		var h = s[0],
			p = t.Event("slide.bs.carousel", {
				relatedTarget: h,
				direction: r
			});
		if (this.$element.trigger(p), !p.isDefaultPrevented()) {
			if (this.sliding = !0, n && this.pause(), this.$indicators.length) {
				this.$indicators.find(".active").removeClass("active");
				var c = t(this.$indicators.children()[this.getItemIndex(s)]);
				c && c.addClass("active")
			}
			var d = t.Event("slid.bs.carousel", {
				relatedTarget: h,
				direction: r
			});
			return t.support.transition && this.$element.hasClass("slide") ? (s.addClass(e), s[0].offsetWidth, o.addClass(r), s.addClass(r), o.one("bsTransitionEnd", function () {
				s.removeClass([e, r].join(" ")).addClass("active"), o.removeClass(["active", r].join(" ")), l.sliding = !1, setTimeout(function () {
					l.$element.trigger(d)
				}, 0)
			}).emulateTransitionEnd(1e3 * o.css("transition-duration").slice(0, -1))) : (o.removeClass("active"), s.addClass("active"), this.sliding = !1, this.$element.trigger(d)), n && this.cycle(), this
		}
	};
	var o = t.fn.carousel;
	t.fn.carousel = e, t.fn.carousel.Constructor = i, t.fn.carousel.noConflict = function () {
		return t.fn.carousel = o, this
	}, t(document).on("click.bs.carousel.data-api", "[data-slide], [data-slide-to]", function (i) {
		var o, s = t(this),
			n = t(s.attr("data-target") || (o = s.attr("href")) && o.replace(/.*(?=#[^\s]+$)/, ""));
		if (n.hasClass("carousel")) {
			var r = t.extend({}, n.data(), s.data()),
				a = s.attr("data-slide-to");
			a && (r.interval = !1), e.call(n, r), a && n.data("bs.carousel").to(a), i.preventDefault()
		}
	}), t(window).on("load", function () {
		t('[data-ride="carousel"]').each(function () {
			var i = t(this);
			e.call(i, i.data())
		})
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.collapse"),
				n = t.extend({}, i.DEFAULTS, o.data(), "object" == typeof e && e);
			!s && n.toggle && "show" == e && (e = !e), s || o.data("bs.collapse", s = new i(this, n)), "string" == typeof e && s[e]()
		})
	}
	var i = function (e, o) {
		this.$element = t(e), this.options = t.extend({}, i.DEFAULTS, o), this.transitioning = null, this.options.parent && (this.$parent = t(this.options.parent)), this.options.toggle && this.toggle()
	};
	i.VERSION = "3.2.0", i.DEFAULTS = {
		toggle: !0
	}, i.prototype.dimension = function () {
		var t = this.$element.hasClass("width");
		return t ? "width" : "height"
	}, i.prototype.show = function () {
		if (!this.transitioning && !this.$element.hasClass("in")) {
			var i = t.Event("show.bs.collapse");
			if (this.$element.trigger(i), !i.isDefaultPrevented()) {
				var o = this.$parent && this.$parent.find("> .panel > .in");
				if (o && o.length) {
					var s = o.data("bs.collapse");
					if (s && s.transitioning) return;
					e.call(o, "hide"), s || o.data("bs.collapse", null)
				}
				var n = this.dimension();
				this.$element.removeClass("collapse").addClass("collapsing")[n](0), this.transitioning = 1;
				var r = function () {
					this.$element.removeClass("collapsing").addClass("collapse in")[n](""), this.transitioning = 0, this.$element.trigger("shown.bs.collapse")
				};
				if (!t.support.transition) return r.call(this);
				var a = t.camelCase(["scroll", n].join("-"));
				this.$element.one("bsTransitionEnd", t.proxy(r, this)).emulateTransitionEnd(350)[n](this.$element[0][a])
			}
		}
	}, i.prototype.hide = function () {
		if (!this.transitioning && this.$element.hasClass("in")) {
			var e = t.Event("hide.bs.collapse");
			if (this.$element.trigger(e), !e.isDefaultPrevented()) {
				var i = this.dimension();
				this.$element[i](this.$element[i]())[0].offsetHeight, this.$element.addClass("collapsing").removeClass("collapse").removeClass("in"), this.transitioning = 1;
				var o = function () {
					this.transitioning = 0, this.$element.trigger("hidden.bs.collapse").removeClass("collapsing").addClass("collapse")
				};
				return t.support.transition ? void this.$element[i](0).one("bsTransitionEnd", t.proxy(o, this)).emulateTransitionEnd(350) : o.call(this)
			}
		}
	}, i.prototype.toggle = function () {
		this[this.$element.hasClass("in") ? "hide" : "show"]()
	};
	var o = t.fn.collapse;
	t.fn.collapse = e, t.fn.collapse.Constructor = i, t.fn.collapse.noConflict = function () {
		return t.fn.collapse = o, this
	}, t(document).on("click.bs.collapse.data-api", '[data-toggle="collapse"]', function (i) {
		var o, s = t(this),
			n = s.attr("data-target") || i.preventDefault() || (o = s.attr("href")) && o.replace(/.*(?=#[^\s]+$)/, ""),
			r = t(n),
			a = r.data("bs.collapse"),
			l = a ? "toggle" : s.data(),
			h = s.attr("data-parent"),
			p = h && t(h);
		a && a.transitioning || (p && p.find('[data-toggle="collapse"][data-parent="' + h + '"]').not(s).addClass("collapsed"), s[r.hasClass("in") ? "addClass" : "removeClass"]("collapsed")), e.call(r, l)
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		e && 3 === e.which || (t(s).remove(), t(n).each(function () {
			var o = i(t(this)),
				s = {
					relatedTarget: this
				};
			o.hasClass("open") && (o.trigger(e = t.Event("hide.bs.dropdown", s)), e.isDefaultPrevented() || o.removeClass("open").trigger("hidden.bs.dropdown", s))
		}))
	}

	function i(e) {
		var i = e.attr("data-target");
		i || (i = e.attr("href"), i = i && /#[A-Za-z]/.test(i) && i.replace(/.*(?=#[^\s]*$)/, ""));
		var o = i && t(i);
		return o && o.length ? o : e.parent()
	}

	function o(e) {
		return this.each(function () {
			var i = t(this),
				o = i.data("bs.dropdown");
			o || i.data("bs.dropdown", o = new r(this)), "string" == typeof e && o[e].call(i)
		})
	}
	var s = ".dropdown-backdrop",
		n = '[data-toggle="dropdown"]',
		r = function (e) {
			t(e).on("click.bs.dropdown", this.toggle)
		};
	r.VERSION = "3.2.0", r.prototype.toggle = function (o) {
		var s = t(this);
		if (!s.is(".disabled, :disabled")) {
			var n = i(s),
				r = n.hasClass("open");
			if (e(), !r) {
				"ontouchstart" in document.documentElement && !n.closest(".navbar-nav").length && t('<div class="dropdown-backdrop"/>').insertAfter(t(this)).on("click", e);
				var a = {
					relatedTarget: this
				};
				if (n.trigger(o = t.Event("show.bs.dropdown", a)), o.isDefaultPrevented()) return;
				s.trigger("focus"), n.toggleClass("open").trigger("shown.bs.dropdown", a)
			}
			return !1
		}
	}, r.prototype.keydown = function (e) {
		if (/(38|40|27)/.test(e.keyCode)) {
			var o = t(this);
			if (e.preventDefault(), e.stopPropagation(), !o.is(".disabled, :disabled")) {
				var s = i(o),
					r = s.hasClass("open");
				if (!r || r && 27 == e.keyCode) return 27 == e.which && s.find(n).trigger("focus"), o.trigger("click");
				var a = " li:not(.divider):visible a",
					l = s.find('[role="menu"]' + a + ', [role="listbox"]' + a);
				if (l.length) {
					var h = l.index(l.filter(":focus"));
					38 == e.keyCode && h > 0 && h--, 40 == e.keyCode && h < l.length - 1 && h++, ~h || (h = 0), l.eq(h).trigger("focus")
				}
			}
		}
	};
	var a = t.fn.dropdown;
	t.fn.dropdown = o, t.fn.dropdown.Constructor = r, t.fn.dropdown.noConflict = function () {
		return t.fn.dropdown = a, this
	}, t(document).on("click.bs.dropdown.data-api", e).on("click.bs.dropdown.data-api", ".dropdown form", function (t) {
		t.stopPropagation()
	}).on("click.bs.dropdown.data-api", n, r.prototype.toggle).on("keydown.bs.dropdown.data-api", n + ', [role="menu"], [role="listbox"]', r.prototype.keydown)
}(jQuery), + function (t) {
	"use strict";

	function e(e, o) {
		return this.each(function () {
			var s = t(this),
				n = s.data("bs.modal"),
				r = t.extend({}, i.DEFAULTS, s.data(), "object" == typeof e && e);
			n || s.data("bs.modal", n = new i(this, r)), "string" == typeof e ? n[e](o) : r.show && n.show(o)
		})
	}
	var i = function (e, i) {
		this.options = i, this.$body = t(document.body), this.$element = t(e), this.$backdrop = this.isShown = null, this.scrollbarWidth = 0, this.options.remote && this.$element.find(".modal-content").load(this.options.remote, t.proxy(function () {
			this.$element.trigger("loaded.bs.modal")
		}, this))
	};
	i.VERSION = "3.2.0", i.DEFAULTS = {
		backdrop: !0,
		keyboard: !0,
		show: !0
	}, i.prototype.toggle = function (t) {
		return this.isShown ? this.hide() : this.show(t)
	}, i.prototype.show = function (e) {
		var i = this,
			o = t.Event("show.bs.modal", {
				relatedTarget: e
			});
		this.$element.trigger(o), this.isShown || o.isDefaultPrevented() || (this.isShown = !0, this.checkScrollbar(), this.$body.addClass("modal-open"), this.setScrollbar(), this.escape(), this.$element.on("click.dismiss.bs.modal", '[data-dismiss="modal"]', t.proxy(this.hide, this)), this.backdrop(function () {
			var o = t.support.transition && i.$element.hasClass("fade");
			i.$element.parent().length || i.$element.appendTo(i.$body), i.$element.show().scrollTop(0), o && i.$element[0].offsetWidth, i.$element.addClass("in").attr("aria-hidden", !1), i.enforceFocus();
			var s = t.Event("shown.bs.modal", {
				relatedTarget: e
			});
			o ? i.$element.find(".modal-dialog").one("bsTransitionEnd", function () {
				i.$element.trigger("focus").trigger(s)
			}).emulateTransitionEnd(300) : i.$element.trigger("focus").trigger(s)
		}))
	}, i.prototype.hide = function (e) {
		e && e.preventDefault(), e = t.Event("hide.bs.modal"), this.$element.trigger(e), this.isShown && !e.isDefaultPrevented() && (this.isShown = !1, this.$body.removeClass("modal-open"), this.resetScrollbar(), this.escape(), t(document).off("focusin.bs.modal"), this.$element.removeClass("in").attr("aria-hidden", !0).off("click.dismiss.bs.modal"), t.support.transition && this.$element.hasClass("fade") ? this.$element.one("bsTransitionEnd", t.proxy(this.hideModal, this)).emulateTransitionEnd(300) : this.hideModal())
	}, i.prototype.enforceFocus = function () {
		t(document).off("focusin.bs.modal").on("focusin.bs.modal", t.proxy(function (t) {
			this.$element[0] === t.target || this.$element.has(t.target).length || this.$element.trigger("focus")
		}, this))
	}, i.prototype.escape = function () {
		this.isShown && this.options.keyboard ? this.$element.on("keyup.dismiss.bs.modal", t.proxy(function (t) {
			27 == t.which && this.hide()
		}, this)) : this.isShown || this.$element.off("keyup.dismiss.bs.modal")
	}, i.prototype.hideModal = function () {
		var t = this;
		this.$element.hide(), this.backdrop(function () {
			t.$element.trigger("hidden.bs.modal")
		})
	}, i.prototype.removeBackdrop = function () {
		this.$backdrop && this.$backdrop.remove(), this.$backdrop = null
	}, i.prototype.backdrop = function (e) {
		var i = this,
			o = this.$element.hasClass("fade") ? "fade" : "";
		if (this.isShown && this.options.backdrop) {
			var s = t.support.transition && o;
			if (this.$backdrop = t('<div class="modal-backdrop ' + o + '" />').appendTo(this.$body), this.$element.on("click.dismiss.bs.modal", t.proxy(function (t) {
					t.target === t.currentTarget && ("static" == this.options.backdrop ? this.$element[0].focus.call(this.$element[0]) : this.hide.call(this))
				}, this)), s && this.$backdrop[0].offsetWidth, this.$backdrop.addClass("in"), !e) return;
			s ? this.$backdrop.one("bsTransitionEnd", e).emulateTransitionEnd(150) : e()
		} else if (!this.isShown && this.$backdrop) {
			this.$backdrop.removeClass("in");
			var n = function () {
				i.removeBackdrop(), e && e()
			};
			t.support.transition && this.$element.hasClass("fade") ? this.$backdrop.one("bsTransitionEnd", n).emulateTransitionEnd(150) : n()
		} else e && e()
	}, i.prototype.checkScrollbar = function () {
		document.body.clientWidth >= window.innerWidth || (this.scrollbarWidth = this.scrollbarWidth || this.measureScrollbar())
	}, i.prototype.setScrollbar = function () {
		var t = parseInt(this.$body.css("padding-right") || 0, 10);
		this.scrollbarWidth && this.$body.css("padding-right", t + this.scrollbarWidth)
	}, i.prototype.resetScrollbar = function () {
		this.$body.css("padding-right", "")
	}, i.prototype.measureScrollbar = function () {
		var t = document.createElement("div");
		t.className = "modal-scrollbar-measure", this.$body.append(t);
		var e = t.offsetWidth - t.clientWidth;
		return this.$body[0].removeChild(t), e
	};
	var o = t.fn.modal;
	t.fn.modal = e, t.fn.modal.Constructor = i, t.fn.modal.noConflict = function () {
		return t.fn.modal = o, this
	}, t(document).on("click.bs.modal.data-api", '[data-toggle="modal"]', function (i) {
		var o = t(this),
			s = o.attr("href"),
			n = t(o.attr("data-target") || s && s.replace(/.*(?=#[^\s]+$)/, "")),
			r = n.data("bs.modal") ? "toggle" : t.extend({
				remote: !/#/.test(s) && s
			}, n.data(), o.data());
		o.is("a") && i.preventDefault(), n.one("show.bs.modal", function (t) {
			t.isDefaultPrevented() || n.one("hidden.bs.modal", function () {
				o.is(":visible") && o.trigger("focus")
			})
		}), e.call(n, r, this)
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.tooltip"),
				n = "object" == typeof e && e;
			(s || "destroy" != e) && (s || o.data("bs.tooltip", s = new i(this, n)), "string" == typeof e && s[e]())
		})
	}
	var i = function (t, e) {
		this.type = this.options = this.enabled = this.timeout = this.hoverState = this.$element = null, this.init("tooltip", t, e)
	};
	i.VERSION = "3.2.0", i.DEFAULTS = {
		animation: !0,
		placement: "top",
		selector: !1,
		template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',
		trigger: "hover focus",
		title: "",
		delay: 0,
		html: !1,
		container: !1,
		viewport: {
			selector: "body",
			padding: 0
		}
	}, i.prototype.init = function (e, i, o) {
		this.enabled = !0, this.type = e, this.$element = t(i), this.options = this.getOptions(o), this.$viewport = this.options.viewport && t(this.options.viewport.selector || this.options.viewport);
		for (var s = this.options.trigger.split(" "), n = s.length; n--;) {
			var r = s[n];
			if ("click" == r) this.$element.on("click." + this.type, this.options.selector, t.proxy(this.toggle, this));
			else if ("manual" != r) {
				var a = "hover" == r ? "mouseenter" : "focusin",
					l = "hover" == r ? "mouseleave" : "focusout";
				this.$element.on(a + "." + this.type, this.options.selector, t.proxy(this.enter, this)), this.$element.on(l + "." + this.type, this.options.selector, t.proxy(this.leave, this))
			}
		}
		this.options.selector ? this._options = t.extend({}, this.options, {
			trigger: "manual",
			selector: ""
		}) : this.fixTitle()
	}, i.prototype.getDefaults = function () {
		return i.DEFAULTS
	}, i.prototype.getOptions = function (e) {
		return e = t.extend({}, this.getDefaults(), this.$element.data(), e), e.delay && "number" == typeof e.delay && (e.delay = {
			show: e.delay,
			hide: e.delay
		}), e
	}, i.prototype.getDelegateOptions = function () {
		var e = {},
			i = this.getDefaults();
		return this._options && t.each(this._options, function (t, o) {
			i[t] != o && (e[t] = o)
		}), e
	}, i.prototype.enter = function (e) {
		var i = e instanceof this.constructor ? e : t(e.currentTarget).data("bs." + this.type);
		return i || (i = new this.constructor(e.currentTarget, this.getDelegateOptions()), t(e.currentTarget).data("bs." + this.type, i)), clearTimeout(i.timeout), i.hoverState = "in", i.options.delay && i.options.delay.show ? void(i.timeout = setTimeout(function () {
			"in" == i.hoverState && i.show()
		}, i.options.delay.show)) : i.show()
	}, i.prototype.leave = function (e) {
		var i = e instanceof this.constructor ? e : t(e.currentTarget).data("bs." + this.type);
		return i || (i = new this.constructor(e.currentTarget, this.getDelegateOptions()), t(e.currentTarget).data("bs." + this.type, i)), clearTimeout(i.timeout), i.hoverState = "out", i.options.delay && i.options.delay.hide ? void(i.timeout = setTimeout(function () {
			"out" == i.hoverState && i.hide()
		}, i.options.delay.hide)) : i.hide()
	}, i.prototype.show = function () {
		var e = t.Event("show.bs." + this.type);
		if (this.hasContent() && this.enabled) {
			this.$element.trigger(e);
			var i = t.contains(document.documentElement, this.$element[0]);
			if (e.isDefaultPrevented() || !i) return;
			var o = this,
				s = this.tip(),
				n = this.getUID(this.type);
			this.setContent(), s.attr("id", n), this.$element.attr("aria-describedby", n), this.options.animation && s.addClass("fade");
			var r = "function" == typeof this.options.placement ? this.options.placement.call(this, s[0], this.$element[0]) : this.options.placement,
				a = /\s?auto?\s?/i,
				l = a.test(r);
			l && (r = r.replace(a, "") || "top"), s.detach().css({
				top: 0,
				left: 0,
				display: "block"
			}).addClass(r).data("bs." + this.type, this), this.options.container ? s.appendTo(this.options.container) : s.insertAfter(this.$element);
			var h = this.getPosition(),
				p = s[0].offsetWidth,
				c = s[0].offsetHeight;
			if (l) {
				var d = r,
					f = this.$element.parent(),
					u = this.getPosition(f);
				r = "bottom" == r && h.top + h.height + c - u.scroll > u.height ? "top" : "top" == r && h.top - u.scroll - c < 0 ? "bottom" : "right" == r && h.right + p > u.width ? "left" : "left" == r && h.left - p < u.left ? "right" : r, s.removeClass(d).addClass(r)
			}
			var g = this.getCalculatedOffset(r, h, p, c);
			this.applyPlacement(g, r);
			var v = function () {
				o.$element.trigger("shown.bs." + o.type), o.hoverState = null
			};
			t.support.transition && this.$tip.hasClass("fade") ? s.one("bsTransitionEnd", v).emulateTransitionEnd(150) : v()
		}
	}, i.prototype.applyPlacement = function (e, i) {
		var o = this.tip(),
			s = o[0].offsetWidth,
			n = o[0].offsetHeight,
			r = parseInt(o.css("margin-top"), 10),
			a = parseInt(o.css("margin-left"), 10);
		isNaN(r) && (r = 0), isNaN(a) && (a = 0), e.top = e.top + r, e.left = e.left + a, t.offset.setOffset(o[0], t.extend({
			using: function (t) {
				o.css({
					top: Math.round(t.top),
					left: Math.round(t.left)
				})
			}
		}, e), 0), o.addClass("in");
		var l = o[0].offsetWidth,
			h = o[0].offsetHeight;
		"top" == i && h != n && (e.top = e.top + n - h);
		var p = this.getViewportAdjustedDelta(i, e, l, h);
		p.left ? e.left += p.left : e.top += p.top;
		var c = p.left ? 2 * p.left - s + l : 2 * p.top - n + h,
			d = p.left ? "left" : "top",
			f = p.left ? "offsetWidth" : "offsetHeight";
		o.offset(e), this.replaceArrow(c, o[0][f], d)
	}, i.prototype.replaceArrow = function (t, e, i) {
		this.arrow().css(i, t ? 50 * (1 - t / e) + "%" : "")
	}, i.prototype.setContent = function () {
		var t = this.tip(),
			e = this.getTitle();
		t.find(".tooltip-inner")[this.options.html ? "html" : "text"](e), t.removeClass("fade in top bottom left right")
	}, i.prototype.hide = function () {
		function e() {
			"in" != i.hoverState && o.detach(), i.$element.trigger("hidden.bs." + i.type)
		}
		var i = this,
			o = this.tip(),
			s = t.Event("hide.bs." + this.type);
		return this.$element.removeAttr("aria-describedby"), this.$element.trigger(s), s.isDefaultPrevented() ? void 0 : (o.removeClass("in"), t.support.transition && this.$tip.hasClass("fade") ? o.one("bsTransitionEnd", e).emulateTransitionEnd(150) : e(), this.hoverState = null, this)
	}, i.prototype.fixTitle = function () {
		var t = this.$element;
		(t.attr("title") || "string" != typeof t.attr("data-original-title")) && t.attr("data-original-title", t.attr("title") || "").attr("title", "")
	}, i.prototype.hasContent = function () {
		return this.getTitle()
	}, i.prototype.getPosition = function (e) {
		e = e || this.$element;
		var i = e[0],
			o = "BODY" == i.tagName;
		return t.extend({}, "function" == typeof i.getBoundingClientRect ? i.getBoundingClientRect() : null, {
			scroll: o ? document.documentElement.scrollTop || document.body.scrollTop : e.scrollTop(),
			width: o ? t(window).width() : e.outerWidth(),
			height: o ? t(window).height() : e.outerHeight()
		}, o ? {
			top: 0,
			left: 0
		} : e.offset())
	}, i.prototype.getCalculatedOffset = function (t, e, i, o) {
		return "bottom" == t ? {
			top: e.top + e.height,
			left: e.left + e.width / 2 - i / 2
		} : "top" == t ? {
			top: e.top - o,
			left: e.left + e.width / 2 - i / 2
		} : "left" == t ? {
			top: e.top + e.height / 2 - o / 2,
			left: e.left - i
		} : {
			top: e.top + e.height / 2 - o / 2,
			left: e.left + e.width
		}
	}, i.prototype.getViewportAdjustedDelta = function (t, e, i, o) {
		var s = {
			top: 0,
			left: 0
		};
		if (!this.$viewport) return s;
		var n = this.options.viewport && this.options.viewport.padding || 0,
			r = this.getPosition(this.$viewport);
		if (/right|left/.test(t)) {
			var a = e.top - n - r.scroll,
				l = e.top + n - r.scroll + o;
			a < r.top ? s.top = r.top - a : l > r.top + r.height && (s.top = r.top + r.height - l)
		} else {
			var h = e.left - n,
				p = e.left + n + i;
			h < r.left ? s.left = r.left - h : p > r.width && (s.left = r.left + r.width - p)
		}
		return s
	}, i.prototype.getTitle = function () {
		var t, e = this.$element,
			i = this.options;
		return t = e.attr("data-original-title") || ("function" == typeof i.title ? i.title.call(e[0]) : i.title)
	}, i.prototype.getUID = function (t) {
		do t += ~~(1e6 * Math.random()); while (document.getElementById(t));
		return t
	}, i.prototype.tip = function () {
		return this.$tip = this.$tip || t(this.options.template)
	}, i.prototype.arrow = function () {
		return this.$arrow = this.$arrow || this.tip().find(".tooltip-arrow")
	}, i.prototype.validate = function () {
		this.$element[0].parentNode || (this.hide(), this.$element = null, this.options = null)
	}, i.prototype.enable = function () {
		this.enabled = !0
	}, i.prototype.disable = function () {
		this.enabled = !1
	}, i.prototype.toggleEnabled = function () {
		this.enabled = !this.enabled
	}, i.prototype.toggle = function (e) {
		var i = this;
		e && (i = t(e.currentTarget).data("bs." + this.type), i || (i = new this.constructor(e.currentTarget, this.getDelegateOptions()), t(e.currentTarget).data("bs." + this.type, i))), i.tip().hasClass("in") ? i.leave(i) : i.enter(i)
	}, i.prototype.destroy = function () {
		clearTimeout(this.timeout), this.hide().$element.off("." + this.type).removeData("bs." + this.type)
	};
	var o = t.fn.tooltip;
	t.fn.tooltip = e, t.fn.tooltip.Constructor = i, t.fn.tooltip.noConflict = function () {
		return t.fn.tooltip = o, this
	}
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.popover"),
				n = "object" == typeof e && e;
			(s || "destroy" != e) && (s || o.data("bs.popover", s = new i(this, n)), "string" == typeof e && s[e]())
		})
	}
	var i = function (t, e) {
		this.init("popover", t, e)
	};
	if (!t.fn.tooltip) throw new Error("Popover requires tooltip.js");
	i.VERSION = "3.2.0", i.DEFAULTS = t.extend({}, t.fn.tooltip.Constructor.DEFAULTS, {
		placement: "right",
		trigger: "click",
		content: "",
		template: '<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
	}), i.prototype = t.extend({}, t.fn.tooltip.Constructor.prototype), i.prototype.constructor = i, i.prototype.getDefaults = function () {
		return i.DEFAULTS
	}, i.prototype.setContent = function () {
		var t = this.tip(),
			e = this.getTitle(),
			i = this.getContent();
		t.find(".popover-title")[this.options.html ? "html" : "text"](e), t.find(".popover-content").empty()[this.options.html ? "string" == typeof i ? "html" : "append" : "text"](i), t.removeClass("fade top bottom left right in"), t.find(".popover-title").html() || t.find(".popover-title").hide()
	}, i.prototype.hasContent = function () {
		return this.getTitle() || this.getContent()
	}, i.prototype.getContent = function () {
		var t = this.$element,
			e = this.options;
		return t.attr("data-content") || ("function" == typeof e.content ? e.content.call(t[0]) : e.content)
	}, i.prototype.arrow = function () {
		return this.$arrow = this.$arrow || this.tip().find(".arrow")
	}, i.prototype.tip = function () {
		return this.$tip || (this.$tip = t(this.options.template)), this.$tip
	};
	var o = t.fn.popover;
	t.fn.popover = e, t.fn.popover.Constructor = i, t.fn.popover.noConflict = function () {
		return t.fn.popover = o, this
	}
}(jQuery), + function (t) {
	"use strict";

	function e(i, o) {
		var s = t.proxy(this.process, this);
		this.$body = t("body"), this.$scrollElement = t(t(i).is("body") ? window : i), this.options = t.extend({}, e.DEFAULTS, o), this.selector = (this.options.target || "") + " .nav li > a", this.offsets = [], this.targets = [], this.activeTarget = null, this.scrollHeight = 0, this.$scrollElement.on("scroll.bs.scrollspy", s), this.refresh(), this.process()
	}

	function i(i) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.scrollspy"),
				n = "object" == typeof i && i;
			s || o.data("bs.scrollspy", s = new e(this, n)), "string" == typeof i && s[i]()
		})
	}
	e.VERSION = "3.2.0", e.DEFAULTS = {
		offset: 10
	}, e.prototype.getScrollHeight = function () {
		return this.$scrollElement[0].scrollHeight || Math.max(this.$body[0].scrollHeight, document.documentElement.scrollHeight)
	}, e.prototype.refresh = function () {
		var e = "offset",
			i = 0;
		t.isWindow(this.$scrollElement[0]) || (e = "position", i = this.$scrollElement.scrollTop()), this.offsets = [], this.targets = [], this.scrollHeight = this.getScrollHeight();
		var o = this;
		this.$body.find(this.selector).map(function () {
			var o = t(this),
				s = o.data("target") || o.attr("href"),
				n = /^#./.test(s) && t(s);
			return n && n.length && n.is(":visible") && [
				[n[e]().top + i, s]
			] || null
		}).sort(function (t, e) {
			return t[0] - e[0]
		}).each(function () {
			o.offsets.push(this[0]), o.targets.push(this[1])
		})
	}, e.prototype.process = function () {
		var t, e = this.$scrollElement.scrollTop() + this.options.offset,
			i = this.getScrollHeight(),
			o = this.options.offset + i - this.$scrollElement.height(),
			s = this.offsets,
			n = this.targets,
			r = this.activeTarget;
		if (this.scrollHeight != i && this.refresh(), e >= o) return r != (t = n[n.length - 1]) && this.activate(t);
		if (r && e <= s[0]) return r != (t = n[0]) && this.activate(t);
		for (t = s.length; t--;) r != n[t] && e >= s[t] && (!s[t + 1] || e <= s[t + 1]) && this.activate(n[t])
	}, e.prototype.activate = function (e) {
		this.activeTarget = e, t(this.selector).parentsUntil(this.options.target, ".active").removeClass("active");
		var i = this.selector + '[data-target="' + e + '"],' + this.selector + '[href="' + e + '"]',
			o = t(i).parents("li").addClass("active");
		o.parent(".dropdown-menu").length && (o = o.closest("li.dropdown").addClass("active")), o.trigger("activate.bs.scrollspy")
	};
	var o = t.fn.scrollspy;
	t.fn.scrollspy = i, t.fn.scrollspy.Constructor = e, t.fn.scrollspy.noConflict = function () {
		return t.fn.scrollspy = o, this
	}, t(window).on("load.bs.scrollspy.data-api", function () {
		t('[data-spy="scroll"]').each(function () {
			var e = t(this);
			i.call(e, e.data())
		})
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.tab");
			s || o.data("bs.tab", s = new i(this)), "string" == typeof e && s[e]()
		})
	}
	var i = function (e) {
		this.element = t(e)
	};
	i.VERSION = "3.2.0", i.prototype.show = function () {
		var e = this.element,
			i = e.closest("ul:not(.dropdown-menu)"),
			o = e.data("target");
		if (o || (o = e.attr("href"), o = o && o.replace(/.*(?=#[^\s]*$)/, "")), !e.parent("li").hasClass("active")) {
			var s = i.find(".active:last a")[0],
				n = t.Event("show.bs.tab", {
					relatedTarget: s
				});
			if (e.trigger(n), !n.isDefaultPrevented()) {
				var r = t(o);
				this.activate(e.closest("li"), i), this.activate(r, r.parent(), function () {
					e.trigger({
						type: "shown.bs.tab",
						relatedTarget: s
					})
				})
			}
		}
	}, i.prototype.activate = function (e, i, o) {
		function s() {
			n.removeClass("active").find("> .dropdown-menu > .active").removeClass("active"), e.addClass("active"), r ? (e[0].offsetWidth, e.addClass("in")) : e.removeClass("fade"), e.parent(".dropdown-menu") && e.closest("li.dropdown").addClass("active"), o && o()
		}
		var n = i.find("> .active"),
			r = o && t.support.transition && n.hasClass("fade");
		r ? n.one("bsTransitionEnd", s).emulateTransitionEnd(150) : s(), n.removeClass("in")
	};
	var o = t.fn.tab;
	t.fn.tab = e, t.fn.tab.Constructor = i, t.fn.tab.noConflict = function () {
		return t.fn.tab = o, this
	}, t(document).on("click.bs.tab.data-api", '[data-toggle="tab"], [data-toggle="pill"]', function (i) {
		i.preventDefault(), e.call(t(this), "show")
	})
}(jQuery), + function (t) {
	"use strict";

	function e(e) {
		return this.each(function () {
			var o = t(this),
				s = o.data("bs.affix"),
				n = "object" == typeof e && e;
			s || o.data("bs.affix", s = new i(this, n)), "string" == typeof e && s[e]()
		})
	}
	var i = function (e, o) {
		this.options = t.extend({}, i.DEFAULTS, o), this.$target = t(this.options.target).on("scroll.bs.affix.data-api", t.proxy(this.checkPosition, this)).on("click.bs.affix.data-api", t.proxy(this.checkPositionWithEventLoop, this)), this.$element = t(e), this.affixed = this.unpin = this.pinnedOffset = null, this.checkPosition()
	};
	i.VERSION = "3.2.0", i.RESET = "affix affix-top affix-bottom", i.DEFAULTS = {
		offset: 0,
		target: window
	}, i.prototype.getPinnedOffset = function () {
		if (this.pinnedOffset) return this.pinnedOffset;
		this.$element.removeClass(i.RESET).addClass("affix");
		var t = this.$target.scrollTop(),
			e = this.$element.offset();
		return this.pinnedOffset = e.top - t
	}, i.prototype.checkPositionWithEventLoop = function () {
		setTimeout(t.proxy(this.checkPosition, this), 1)
	}, i.prototype.checkPosition = function () {
		if (this.$element.is(":visible")) {
			var e = t(document).height(),
				o = this.$target.scrollTop(),
				s = this.$element.offset(),
				n = this.options.offset,
				r = n.top,
				a = n.bottom;
			"object" != typeof n && (a = r = n), "function" == typeof r && (r = n.top(this.$element)), "function" == typeof a && (a = n.bottom(this.$element));
			var l = null != this.unpin && o + this.unpin <= s.top ? !1 : null != a && s.top + this.$element.height() >= e - a ? "bottom" : null != r && r >= o ? "top" : !1;
			if (this.affixed !== l) {
				null != this.unpin && this.$element.css("top", "");
				var h = "affix" + (l ? "-" + l : ""),
					p = t.Event(h + ".bs.affix");
				this.$element.trigger(p), p.isDefaultPrevented() || (this.affixed = l, this.unpin = "bottom" == l ? this.getPinnedOffset() : null, this.$element.removeClass(i.RESET).addClass(h).trigger(t.Event(h.replace("affix", "affixed"))), "bottom" == l && this.$element.offset({
					top: e - this.$element.height() - a
				}))
			}
		}
	};
	var o = t.fn.affix;
	t.fn.affix = e, t.fn.affix.Constructor = i, t.fn.affix.noConflict = function () {
		return t.fn.affix = o, this
	}, t(window).on("load", function () {
		t('[data-spy="affix"]').each(function () {
			var i = t(this),
				o = i.data();
			o.offset = o.offset || {}, o.offsetBottom && (o.offset.bottom = o.offsetBottom), o.offsetTop && (o.offset.top = o.offsetTop), e.call(i, o)
		})
	})
}(jQuery);
(function () {
	var a, c, b;
	if (a = document.getElementById("site-navigation"))
		if (c = a.getElementsByTagName("button")[0], "undefined" !== typeof c)
			if (b = a.getElementsByTagName("ul")[0], "undefined" === typeof b) c.style.display = "none";
			else {
				b.setAttribute("aria-expanded", "false"); - 1 === b.className.indexOf("nav-menu") && (b.className += " nav-menu");
				c.onclick = function () {
					-1 !== a.className.indexOf("toggled") ? (a.className = a.className.replace(" toggled", ""), c.setAttribute("aria-expanded", "false"), b.setAttribute("aria-expanded", "false")) : (a.className += " toggled", c.setAttribute("aria-expanded", "true"), b.setAttribute("aria-expanded", "true"))
				};
				var d = -1 < navigator.userAgent.toLowerCase().indexOf("webkit"),
					e = -1 < navigator.userAgent.toLowerCase().indexOf("opera"),
					f = -1 < navigator.userAgent.toLowerCase().indexOf("msie");
				(d || e || f) && document.getElementById && window.addEventListener && window.addEventListener("hashchange", function () {
					var a = document.getElementById(location.hash.substring(1));
					a && (/^(?:a|select|input|button|textarea)$/i.test(a.tagName) || (a.tabIndex = -1), a.focus())
				}, !1)
			}
})();
/*!
 * imagesLoaded PACKAGED v3.1.8
 * JavaScript is all like "You images are done yet or what?"
 * MIT License
 */
(function () {
	function e() {}

	function t(e, t) {
		for (var n = e.length; n--;)
			if (e[n].listener === t) return n;
		return -1
	}

	function n(e) {
		return function () {
			return this[e].apply(this, arguments)
		}
	}
	var i = e.prototype,
		r = this,
		o = r.EventEmitter;
	i.getListeners = function (e) {
		var t, n, i = this._getEvents();
		if ("object" == typeof e) {
			t = {};
			for (n in i) i.hasOwnProperty(n) && e.test(n) && (t[n] = i[n])
		} else t = i[e] || (i[e] = []);
		return t
	}, i.flattenListeners = function (e) {
		var t, n = [];
		for (t = 0; e.length > t; t += 1) n.push(e[t].listener);
		return n
	}, i.getListenersAsObject = function (e) {
		var t, n = this.getListeners(e);
		return n instanceof Array && (t = {}, t[e] = n), t || n
	}, i.addListener = function (e, n) {
		var i, r = this.getListenersAsObject(e),
			o = "object" == typeof n;
		for (i in r) r.hasOwnProperty(i) && -1 === t(r[i], n) && r[i].push(o ? n : {
			listener: n,
			once: !1
		});
		return this
	}, i.on = n("addListener"), i.addOnceListener = function (e, t) {
		return this.addListener(e, {
			listener: t,
			once: !0
		})
	}, i.once = n("addOnceListener"), i.defineEvent = function (e) {
		return this.getListeners(e), this
	}, i.defineEvents = function (e) {
		for (var t = 0; e.length > t; t += 1) this.defineEvent(e[t]);
		return this
	}, i.removeListener = function (e, n) {
		var i, r, o = this.getListenersAsObject(e);
		for (r in o) o.hasOwnProperty(r) && (i = t(o[r], n), -1 !== i && o[r].splice(i, 1));
		return this
	}, i.off = n("removeListener"), i.addListeners = function (e, t) {
		return this.manipulateListeners(!1, e, t)
	}, i.removeListeners = function (e, t) {
		return this.manipulateListeners(!0, e, t)
	}, i.manipulateListeners = function (e, t, n) {
		var i, r, o = e ? this.removeListener : this.addListener,
			s = e ? this.removeListeners : this.addListeners;
		if ("object" != typeof t || t instanceof RegExp)
			for (i = n.length; i--;) o.call(this, t, n[i]);
		else
			for (i in t) t.hasOwnProperty(i) && (r = t[i]) && ("function" == typeof r ? o.call(this, i, r) : s.call(this, i, r));
		return this
	}, i.removeEvent = function (e) {
		var t, n = typeof e,
			i = this._getEvents();
		if ("string" === n) delete i[e];
		else if ("object" === n)
			for (t in i) i.hasOwnProperty(t) && e.test(t) && delete i[t];
		else delete this._events;
		return this
	}, i.removeAllListeners = n("removeEvent"), i.emitEvent = function (e, t) {
		var n, i, r, o, s = this.getListenersAsObject(e);
		for (r in s)
			if (s.hasOwnProperty(r))
				for (i = s[r].length; i--;) n = s[r][i], n.once === !0 && this.removeListener(e, n.listener), o = n.listener.apply(this, t || []), o === this._getOnceReturnValue() && this.removeListener(e, n.listener);
		return this
	}, i.trigger = n("emitEvent"), i.emit = function (e) {
		var t = Array.prototype.slice.call(arguments, 1);
		return this.emitEvent(e, t)
	}, i.setOnceReturnValue = function (e) {
		return this._onceReturnValue = e, this
	}, i._getOnceReturnValue = function () {
		return this.hasOwnProperty("_onceReturnValue") ? this._onceReturnValue : !0
	}, i._getEvents = function () {
		return this._events || (this._events = {})
	}, e.noConflict = function () {
		return r.EventEmitter = o, e
	}, "function" == typeof define && define.amd ? define("eventEmitter/EventEmitter", [], function () {
		return e
	}) : "object" == typeof module && module.exports ? module.exports = e : this.EventEmitter = e
}).call(this),
	function (e) {
		function t(t) {
			var n = e.event;
			return n.target = n.target || n.srcElement || t, n
		}
		var n = document.documentElement,
			i = function () {};
		n.addEventListener ? i = function (e, t, n) {
			e.addEventListener(t, n, !1)
		} : n.attachEvent && (i = function (e, n, i) {
			e[n + i] = i.handleEvent ? function () {
				var n = t(e);
				i.handleEvent.call(i, n)
			} : function () {
				var n = t(e);
				i.call(e, n)
			}, e.attachEvent("on" + n, e[n + i])
		});
		var r = function () {};
		n.removeEventListener ? r = function (e, t, n) {
			e.removeEventListener(t, n, !1)
		} : n.detachEvent && (r = function (e, t, n) {
			e.detachEvent("on" + t, e[t + n]);
			try {
				delete e[t + n]
			} catch (i) {
				e[t + n] = void 0
			}
		});
		var o = {
			bind: i,
			unbind: r
		};
		"function" == typeof define && define.amd ? define("eventie/eventie", o) : e.eventie = o
	}(this),
	function (e, t) {
		"function" == typeof define && define.amd ? define(["eventEmitter/EventEmitter", "eventie/eventie"], function (n, i) {
			return t(e, n, i)
		}) : "object" == typeof exports ? module.exports = t(e, require("wolfy87-eventemitter"), require("eventie")) : e.imagesLoaded = t(e, e.EventEmitter, e.eventie)
	}(window, function (e, t, n) {
		function i(e, t) {
			for (var n in t) e[n] = t[n];
			return e
		}

		function r(e) {
			return "[object Array]" === d.call(e)
		}

		function o(e) {
			var t = [];
			if (r(e)) t = e;
			else if ("number" == typeof e.length)
				for (var n = 0, i = e.length; i > n; n++) t.push(e[n]);
			else t.push(e);
			return t
		}

		function s(e, t, n) {
			if (!(this instanceof s)) return new s(e, t);
			"string" == typeof e && (e = document.querySelectorAll(e)), this.elements = o(e), this.options = i({}, this.options), "function" == typeof t ? n = t : i(this.options, t), n && this.on("always", n), this.getImages(), a && (this.jqDeferred = new a.Deferred);
			var r = this;
			setTimeout(function () {
				r.check()
			})
		}

		function f(e) {
			this.img = e
		}

		function c(e) {
			this.src = e, v[e] = this
		}
		var a = e.jQuery,
			u = e.console,
			h = u !== void 0,
			d = Object.prototype.toString;
		s.prototype = new t, s.prototype.options = {}, s.prototype.getImages = function () {
			this.images = [];
			for (var e = 0, t = this.elements.length; t > e; e++) {
				var n = this.elements[e];
				"IMG" === n.nodeName && this.addImage(n);
				var i = n.nodeType;
				if (i && (1 === i || 9 === i || 11 === i))
					for (var r = n.querySelectorAll("img"), o = 0, s = r.length; s > o; o++) {
						var f = r[o];
						this.addImage(f)
					}
			}
		}, s.prototype.addImage = function (e) {
			var t = new f(e);
			this.images.push(t)
		}, s.prototype.check = function () {
			function e(e, r) {
				return t.options.debug && h && u.log("confirm", e, r), t.progress(e), n++, n === i && t.complete(), !0
			}
			var t = this,
				n = 0,
				i = this.images.length;
			if (this.hasAnyBroken = !1, !i) return this.complete(), void 0;
			for (var r = 0; i > r; r++) {
				var o = this.images[r];
				o.on("confirm", e), o.check()
			}
		}, s.prototype.progress = function (e) {
			this.hasAnyBroken = this.hasAnyBroken || !e.isLoaded;
			var t = this;
			setTimeout(function () {
				t.emit("progress", t, e), t.jqDeferred && t.jqDeferred.notify && t.jqDeferred.notify(t, e)
			})
		}, s.prototype.complete = function () {
			var e = this.hasAnyBroken ? "fail" : "done";
			this.isComplete = !0;
			var t = this;
			setTimeout(function () {
				if (t.emit(e, t), t.emit("always", t), t.jqDeferred) {
					var n = t.hasAnyBroken ? "reject" : "resolve";
					t.jqDeferred[n](t)
				}
			})
		}, a && (a.fn.imagesLoaded = function (e, t) {
			var n = new s(this, e, t);
			return n.jqDeferred.promise(a(this))
		}), f.prototype = new t, f.prototype.check = function () {
			var e = v[this.img.src] || new c(this.img.src);
			if (e.isConfirmed) return this.confirm(e.isLoaded, "cached was confirmed"), void 0;
			if (this.img.complete && void 0 !== this.img.naturalWidth) return this.confirm(0 !== this.img.naturalWidth, "naturalWidth"), void 0;
			var t = this;
			e.on("confirm", function (e, n) {
				return t.confirm(e.isLoaded, n), !0
			}), e.check()
		}, f.prototype.confirm = function (e, t) {
			this.isLoaded = e, this.emit("confirm", this, t)
		};
		var v = {};
		return c.prototype = new t, c.prototype.check = function () {
			if (!this.isChecked) {
				var e = new Image;
				n.bind(e, "load", this), n.bind(e, "error", this), e.src = this.src, this.isChecked = !0
			}
		}, c.prototype.handleEvent = function (e) {
			var t = "on" + e.type;
			this[t] && this[t](e)
		}, c.prototype.onload = function (e) {
			this.confirm(!0, "onload"), this.unbindProxyEvents(e)
		}, c.prototype.onerror = function (e) {
			this.confirm(!1, "onerror"), this.unbindProxyEvents(e)
		}, c.prototype.confirm = function (e, t) {
			this.isConfirmed = !0, this.isLoaded = e, this.emit("confirm", this, t)
		}, c.prototype.unbindProxyEvents = function (e) {
			n.unbind(e.target, "load", this), n.unbind(e.target, "error", this)
		}, s
	});
! function ($) {
	var e = !0;
	$.flexslider = function (t, a) {
		var n = $(t);
		void 0 === a.rtl && "rtl" == $("html").attr("dir") && (a.rtl = !0), n.vars = $.extend({}, $.flexslider.defaults, a);
		var i = n.vars.namespace,
			r = window.navigator && window.navigator.msPointerEnabled && window.MSGesture,
			s = ("ontouchstart" in window || r || window.DocumentTouch && document instanceof DocumentTouch) && n.vars.touch,
			o = "click touchend MSPointerUp keyup",
			l = "",
			c, d = "vertical" === n.vars.direction,
			u = n.vars.reverse,
			v = n.vars.itemWidth > 0,
			p = "fade" === n.vars.animation,
			m = "" !== n.vars.asNavFor,
			f = {};
		$.data(t, "flexslider", n), f = {
			init: function () {
				n.animating = !1, n.currentSlide = parseInt(n.vars.startAt ? n.vars.startAt : 0, 10), isNaN(n.currentSlide) && (n.currentSlide = 0), n.animatingTo = n.currentSlide, n.atEnd = 0 === n.currentSlide || n.currentSlide === n.last, n.containerSelector = n.vars.selector.substr(0, n.vars.selector.search(" ")), n.slides = $(n.vars.selector, n), n.container = $(n.containerSelector, n), n.count = n.slides.length, n.syncExists = $(n.vars.sync).length > 0, "slide" === n.vars.animation && (n.vars.animation = "swing"), n.prop = d ? "top" : n.vars.rtl ? "marginRight" : "marginLeft", n.args = {}, n.manualPause = !1, n.stopped = !1, n.started = !1, n.startTimeout = null, n.transitions = !n.vars.video && !p && n.vars.useCSS && function () {
					var e = document.createElement("div"),
						t = ["perspectiveProperty", "WebkitPerspective", "MozPerspective", "OPerspective", "msPerspective"];
					for (var a in t)
						if (void 0 !== e.style[t[a]]) return n.pfx = t[a].replace("Perspective", "").toLowerCase(), n.prop = "-" + n.pfx + "-transform", !0;
					return !1
				}(), n.isFirefox = navigator.userAgent.toLowerCase().indexOf("firefox") > -1, n.ensureAnimationEnd = "", "" !== n.vars.controlsContainer && (n.controlsContainer = $(n.vars.controlsContainer).length > 0 && $(n.vars.controlsContainer)), "" !== n.vars.manualControls && (n.manualControls = $(n.vars.manualControls).length > 0 && $(n.vars.manualControls)), "" !== n.vars.customDirectionNav && (n.customDirectionNav = 2 === $(n.vars.customDirectionNav).length && $(n.vars.customDirectionNav)), n.vars.randomize && (n.slides.sort(function () {
					return Math.round(Math.random()) - .5
				}), n.container.empty().append(n.slides)), n.doMath(), n.setup("init"), n.vars.controlNav && f.controlNav.setup(), n.vars.directionNav && f.directionNav.setup(), n.vars.keyboard && (1 === $(n.containerSelector).length || n.vars.multipleKeyboard) && $(document).bind("keyup", function (e) {
					var t = e.keyCode;
					if (!n.animating && (39 === t || 37 === t)) {
						var a = n.vars.rtl ? 37 === t ? n.getTarget("next") : 39 === t && n.getTarget("prev") : 39 === t ? n.getTarget("next") : 37 === t && n.getTarget("prev");
						n.flexAnimate(a, n.vars.pauseOnAction)
					}
				}), n.vars.mousewheel && n.bind("mousewheel", function (e, t, a, i) {
					e.preventDefault();
					var r = t < 0 ? n.getTarget("next") : n.getTarget("prev");
					n.flexAnimate(r, n.vars.pauseOnAction)
				}), n.vars.pausePlay && f.pausePlay.setup(), n.vars.slideshow && n.vars.pauseInvisible && f.pauseInvisible.init(), n.vars.slideshow && (n.vars.pauseOnHover && n.hover(function () {
					n.manualPlay || n.manualPause || n.pause()
				}, function () {
					n.manualPause || n.manualPlay || n.stopped || n.play()
				}), n.vars.pauseInvisible && f.pauseInvisible.isHidden() || (n.vars.initDelay > 0 ? n.startTimeout = setTimeout(n.play, n.vars.initDelay) : n.play())), m && f.asNav.setup(), s && n.vars.touch && f.touch(), (!p || p && n.vars.smoothHeight) && $(window).bind("resize orientationchange focus", f.resize), n.find("img").attr("draggable", "false"), setTimeout(function () {
					n.vars.start(n)
				}, 200)
			},
			asNav: {
				setup: function () {
					n.asNav = !0, n.animatingTo = Math.floor(n.currentSlide / n.move), n.currentItem = n.currentSlide, n.slides.removeClass(i + "active-slide").eq(n.currentItem).addClass(i + "active-slide"), r ? (t._slider = n, n.slides.each(function () {
						var e = this;
						e._gesture = new MSGesture, e._gesture.target = e, e.addEventListener("MSPointerDown", function (e) {
							e.preventDefault(), e.currentTarget._gesture && e.currentTarget._gesture.addPointer(e.pointerId)
						}, !1), e.addEventListener("MSGestureTap", function (e) {
							e.preventDefault();
							var t = $(this),
								a = t.index();
							$(n.vars.asNavFor).data("flexslider").animating || t.hasClass("active") || (n.direction = n.currentItem < a ? "next" : "prev", n.flexAnimate(a, n.vars.pauseOnAction, !1, !0, !0))
						})
					})) : n.slides.on(o, function (e) {
						e.preventDefault();
						var t = $(this),
							a = t.index(),
							r;
						r = n.vars.rtl ? -1 * (t.offset().right - $(n).scrollLeft()) : t.offset().left - $(n).scrollLeft(), r <= 0 && t.hasClass(i + "active-slide") ? n.flexAnimate(n.getTarget("prev"), !0) : $(n.vars.asNavFor).data("flexslider").animating || t.hasClass(i + "active-slide") || (n.direction = n.currentItem < a ? "next" : "prev", n.flexAnimate(a, n.vars.pauseOnAction, !1, !0, !0))
					})
				}
			},
			controlNav: {
				setup: function () {
					n.manualControls ? f.controlNav.setupManual() : f.controlNav.setupPaging()
				},
				setupPaging: function () {
					var e = "thumbnails" === n.vars.controlNav ? "control-thumbs" : "control-paging",
						t = 1,
						a, r;
					if (n.controlNavScaffold = $('<ol class="' + i + "control-nav " + i + e + '"></ol>'), n.pagingCount > 1)
						for (var s = 0; s < n.pagingCount; s++) {
							r = n.slides.eq(s), void 0 === r.attr("data-thumb-alt") && r.attr("data-thumb-alt", "");
							var c = "" !== r.attr("data-thumb-alt") ? c = ' alt="' + r.attr("data-thumb-alt") + '"' : "";
							if (a = "thumbnails" === n.vars.controlNav ? '<img src="' + r.attr("data-thumb") + '"' + c + "/>" : '<a href="#">' + t + "</a>", "thumbnails" === n.vars.controlNav && !0 === n.vars.thumbCaptions) {
								var d = r.attr("data-thumbcaption");
								"" !== d && void 0 !== d && (a += '<span class="' + i + 'caption">' + d + "</span>")
							}
							n.controlNavScaffold.append("<li>" + a + "</li>"), t++
						}
					n.controlsContainer ? $(n.controlsContainer).append(n.controlNavScaffold) : n.append(n.controlNavScaffold), f.controlNav.set(), f.controlNav.active(), n.controlNavScaffold.delegate("a, img", o, function (e) {
						if (e.preventDefault(), "" === l || l === e.type) {
							var t = $(this),
								a = n.controlNav.index(t);
							t.hasClass(i + "active") || (n.direction = a > n.currentSlide ? "next" : "prev", n.flexAnimate(a, n.vars.pauseOnAction))
						}
						"" === l && (l = e.type), f.setToClearWatchedEvent()
					})
				},
				setupManual: function () {
					n.controlNav = n.manualControls, f.controlNav.active(), n.controlNav.bind(o, function (e) {
						if (e.preventDefault(), "" === l || l === e.type) {
							var t = $(this),
								a = n.controlNav.index(t);
							t.hasClass(i + "active") || (a > n.currentSlide ? n.direction = "next" : n.direction = "prev", n.flexAnimate(a, n.vars.pauseOnAction))
						}
						"" === l && (l = e.type), f.setToClearWatchedEvent()
					})
				},
				set: function () {
					var e = "thumbnails" === n.vars.controlNav ? "img" : "a";
					n.controlNav = $("." + i + "control-nav li " + e, n.controlsContainer ? n.controlsContainer : n)
				},
				active: function () {
					n.controlNav.removeClass(i + "active").eq(n.animatingTo).addClass(i + "active")
				},
				update: function (e, t) {
					n.pagingCount > 1 && "add" === e ? n.controlNavScaffold.append($('<li><a href="#">' + n.count + "</a></li>")) : 1 === n.pagingCount ? n.controlNavScaffold.find("li").remove() : n.controlNav.eq(t).closest("li").remove(), f.controlNav.set(), n.pagingCount > 1 && n.pagingCount !== n.controlNav.length ? n.update(t, e) : f.controlNav.active()
				}
			},
			directionNav: {
				setup: function () {
					var e = $('<ul class="' + i + 'direction-nav"><li class="' + i + 'nav-prev"><a class="' + i + 'prev" href="#">' + n.vars.prevText + '</a></li><li class="' + i + 'nav-next"><a class="' + i + 'next" href="#">' + n.vars.nextText + "</a></li></ul>");
					n.customDirectionNav ? n.directionNav = n.customDirectionNav : n.controlsContainer ? ($(n.controlsContainer).append(e), n.directionNav = $("." + i + "direction-nav li a", n.controlsContainer)) : (n.append(e), n.directionNav = $("." + i + "direction-nav li a", n)), f.directionNav.update(), n.directionNav.bind(o, function (e) {
						e.preventDefault();
						var t;
						"" !== l && l !== e.type || (t = $(this).hasClass(i + "next") ? n.getTarget("next") : n.getTarget("prev"), n.flexAnimate(t, n.vars.pauseOnAction)), "" === l && (l = e.type), f.setToClearWatchedEvent()
					})
				},
				update: function () {
					var e = i + "disabled";
					1 === n.pagingCount ? n.directionNav.addClass(e).attr("tabindex", "-1") : n.vars.animationLoop ? n.directionNav.removeClass(e).removeAttr("tabindex") : 0 === n.animatingTo ? n.directionNav.removeClass(e).filter("." + i + "prev").addClass(e).attr("tabindex", "-1") : n.animatingTo === n.last ? n.directionNav.removeClass(e).filter("." + i + "next").addClass(e).attr("tabindex", "-1") : n.directionNav.removeClass(e).removeAttr("tabindex")
				}
			},
			pausePlay: {
				setup: function () {
					var e = $('<div class="' + i + 'pauseplay"><a href="#"></a></div>');
					n.controlsContainer ? (n.controlsContainer.append(e), n.pausePlay = $("." + i + "pauseplay a", n.controlsContainer)) : (n.append(e), n.pausePlay = $("." + i + "pauseplay a", n)), f.pausePlay.update(n.vars.slideshow ? i + "pause" : i + "play"), n.pausePlay.bind(o, function (e) {
						e.preventDefault(), "" !== l && l !== e.type || ($(this).hasClass(i + "pause") ? (n.manualPause = !0, n.manualPlay = !1, n.pause()) : (n.manualPause = !1, n.manualPlay = !0, n.play())), "" === l && (l = e.type), f.setToClearWatchedEvent()
					})
				},
				update: function (e) {
					"play" === e ? n.pausePlay.removeClass(i + "pause").addClass(i + "play").html(n.vars.playText) : n.pausePlay.removeClass(i + "play").addClass(i + "pause").html(n.vars.pauseText)
				}
			},
			touch: function () {
				function e(e) {
					e.stopPropagation(), n.animating ? e.preventDefault() : (n.pause(), t._gesture.addPointer(e.pointerId), w = 0, c = d ? n.h : n.w, f = Number(new Date), l = v && u && n.animatingTo === n.last ? 0 : v && u ? n.limit - (n.itemW + n.vars.itemMargin) * n.move * n.animatingTo : v && n.currentSlide === n.last ? n.limit : v ? (n.itemW + n.vars.itemMargin) * n.move * n.currentSlide : u ? (n.last - n.currentSlide + n.cloneOffset) * c : (n.currentSlide + n.cloneOffset) * c)
				}

				function a(e) {
					e.stopPropagation();
					var a = e.target._slider;
					if (a) {
						var n = -e.translationX,
							i = -e.translationY;
						if (w += d ? i : n, m = (a.vars.rtl ? -1 : 1) * w, x = d ? Math.abs(w) < Math.abs(-n) : Math.abs(w) < Math.abs(-i), e.detail === e.MSGESTURE_FLAG_INERTIA) return void setImmediate(function () {
							t._gesture.stop()
						});
						(!x || Number(new Date) - f > 500) && (e.preventDefault(), !p && a.transitions && (a.vars.animationLoop || (m = w / (0 === a.currentSlide && w < 0 || a.currentSlide === a.last && w > 0 ? Math.abs(w) / c + 2 : 1)), a.setProps(l + m, "setTouch")))
					}
				}

				function i(e) {
					e.stopPropagation();
					var t = e.target._slider;
					if (t) {
						if (t.animatingTo === t.currentSlide && !x && null !== m) {
							var a = u ? -m : m,
								n = a > 0 ? t.getTarget("next") : t.getTarget("prev");
							t.canAdvance(n) && (Number(new Date) - f < 550 && Math.abs(a) > 50 || Math.abs(a) > c / 2) ? t.flexAnimate(n, t.vars.pauseOnAction) : p || t.flexAnimate(t.currentSlide, t.vars.pauseOnAction, !0)
						}
						s = null, o = null, m = null, l = null, w = 0
					}
				}
				var s, o, l, c, m, f, g, h, S, x = !1,
					y = 0,
					b = 0,
					w = 0;
				r ? (t.style.msTouchAction = "none", t._gesture = new MSGesture, t._gesture.target = t, t.addEventListener("MSPointerDown", e, !1), t._slider = n, t.addEventListener("MSGestureChange", a, !1), t.addEventListener("MSGestureEnd", i, !1)) : (g = function (e) {
					n.animating ? e.preventDefault() : (window.navigator.msPointerEnabled || 1 === e.touches.length) && (n.pause(), c = d ? n.h : n.w, f = Number(new Date), y = e.touches[0].pageX, b = e.touches[0].pageY, l = v && u && n.animatingTo === n.last ? 0 : v && u ? n.limit - (n.itemW + n.vars.itemMargin) * n.move * n.animatingTo : v && n.currentSlide === n.last ? n.limit : v ? (n.itemW + n.vars.itemMargin) * n.move * n.currentSlide : u ? (n.last - n.currentSlide + n.cloneOffset) * c : (n.currentSlide + n.cloneOffset) * c, s = d ? b : y, o = d ? y : b, t.addEventListener("touchmove", h, !1), t.addEventListener("touchend", S, !1))
				}, h = function (e) {
					y = e.touches[0].pageX, b = e.touches[0].pageY, m = d ? s - b : (n.vars.rtl ? -1 : 1) * (s - y), x = d ? Math.abs(m) < Math.abs(y - o) : Math.abs(m) < Math.abs(b - o);
					var t = 500;
					(!x || Number(new Date) - f > 500) && (e.preventDefault(), !p && n.transitions && (n.vars.animationLoop || (m /= 0 === n.currentSlide && m < 0 || n.currentSlide === n.last && m > 0 ? Math.abs(m) / c + 2 : 1), n.setProps(l + m, "setTouch")))
				}, S = function (e) {
					if (t.removeEventListener("touchmove", h, !1), n.animatingTo === n.currentSlide && !x && null !== m) {
						var a = u ? -m : m,
							i = a > 0 ? n.getTarget("next") : n.getTarget("prev");
						n.canAdvance(i) && (Number(new Date) - f < 550 && Math.abs(a) > 50 || Math.abs(a) > c / 2) ? n.flexAnimate(i, n.vars.pauseOnAction) : p || n.flexAnimate(n.currentSlide, n.vars.pauseOnAction, !0)
					}
					t.removeEventListener("touchend", S, !1), s = null, o = null, m = null, l = null
				}, t.addEventListener("touchstart", g, !1))
			},
			resize: function () {
				!n.animating && n.is(":visible") && (v || n.doMath(), p ? f.smoothHeight() : v ? (n.slides.width(n.computedW), n.update(n.pagingCount), n.setProps()) : d ? (n.viewport.height(n.h), n.setProps(n.h, "setTotal")) : (n.vars.smoothHeight && f.smoothHeight(), n.newSlides.width(n.computedW), n.setProps(n.computedW, "setTotal")))
			},
			smoothHeight: function (e) {
				if (!d || p) {
					var t = p ? n : n.viewport;
					e ? t.animate({
						height: n.slides.eq(n.animatingTo).innerHeight()
					}, e) : t.innerHeight(n.slides.eq(n.animatingTo).innerHeight())
				}
			},
			sync: function (e) {
				var t = $(n.vars.sync).data("flexslider"),
					a = n.animatingTo;
				switch (e) {
					case "animate":
						t.flexAnimate(a, n.vars.pauseOnAction, !1, !0);
						break;
					case "play":
						t.playing || t.asNav || t.play();
						break;
					case "pause":
						t.pause();
						break
				}
			},
			uniqueID: function (e) {
				return e.filter("[id]").add(e.find("[id]")).each(function () {
					var e = $(this);
					e.attr("id", e.attr("id") + "_clone")
				}), e
			},
			pauseInvisible: {
				visProp: null,
				init: function () {
					var e = f.pauseInvisible.getHiddenProp();
					if (e) {
						var t = e.replace(/[H|h]idden/, "") + "visibilitychange";
						document.addEventListener(t, function () {
							f.pauseInvisible.isHidden() ? n.startTimeout ? clearTimeout(n.startTimeout) : n.pause() : n.started ? n.play() : n.vars.initDelay > 0 ? setTimeout(n.play, n.vars.initDelay) : n.play()
						})
					}
				},
				isHidden: function () {
					var e = f.pauseInvisible.getHiddenProp();
					return !!e && document[e]
				},
				getHiddenProp: function () {
					var e = ["webkit", "moz", "ms", "o"];
					if ("hidden" in document) return "hidden";
					for (var t = 0; t < e.length; t++)
						if (e[t] + "Hidden" in document) return e[t] + "Hidden";
					return null
				}
			},
			setToClearWatchedEvent: function () {
				clearTimeout(c), c = setTimeout(function () {
					l = ""
				}, 3e3)
			}
		}, n.flexAnimate = function (e, t, a, r, o) {
			if (n.vars.animationLoop || e === n.currentSlide || (n.direction = e > n.currentSlide ? "next" : "prev"), m && 1 === n.pagingCount && (n.direction = n.currentItem < e ? "next" : "prev"), !n.animating && (n.canAdvance(e, o) || a) && n.is(":visible")) {
				if (m && r) {
					var l = $(n.vars.asNavFor).data("flexslider");
					if (n.atEnd = 0 === e || e === n.count - 1, l.flexAnimate(e, !0, !1, !0, o), n.direction = n.currentItem < e ? "next" : "prev", l.direction = n.direction, Math.ceil((e + 1) / n.visible) - 1 === n.currentSlide || 0 === e) return n.currentItem = e, n.slides.removeClass(i + "active-slide").eq(e).addClass(i + "active-slide"), !1;
					n.currentItem = e, n.slides.removeClass(i + "active-slide").eq(e).addClass(i + "active-slide"), e = Math.floor(e / n.visible)
				}
				if (n.animating = !0, n.animatingTo = e, t && n.pause(), n.vars.before(n), n.syncExists && !o && f.sync("animate"), n.vars.controlNav && f.controlNav.active(), v || n.slides.removeClass(i + "active-slide").eq(e).addClass(i + "active-slide"), n.atEnd = 0 === e || e === n.last, n.vars.directionNav && f.directionNav.update(), e === n.last && (n.vars.end(n), n.vars.animationLoop || n.pause()), p) s ? (n.slides.eq(n.currentSlide).css({
					opacity: 0,
					zIndex: 1
				}), n.slides.eq(e).css({
					opacity: 1,
					zIndex: 2
				}), n.wrapup(c)) : (n.slides.eq(n.currentSlide).css({
					zIndex: 1
				}).animate({
					opacity: 0
				}, n.vars.animationSpeed, n.vars.easing), n.slides.eq(e).css({
					zIndex: 2
				}).animate({
					opacity: 1
				}, n.vars.animationSpeed, n.vars.easing, n.wrapup));
				else {
					var c = d ? n.slides.filter(":first").height() : n.computedW,
						g, h, S;
					v ? (g = n.vars.itemMargin, S = (n.itemW + g) * n.move * n.animatingTo, h = S > n.limit && 1 !== n.visible ? n.limit : S) : h = 0 === n.currentSlide && e === n.count - 1 && n.vars.animationLoop && "next" !== n.direction ? u ? (n.count + n.cloneOffset) * c : 0 : n.currentSlide === n.last && 0 === e && n.vars.animationLoop && "prev" !== n.direction ? u ? 0 : (n.count + 1) * c : u ? (n.count - 1 - e + n.cloneOffset) * c : (e + n.cloneOffset) * c, n.setProps(h, "", n.vars.animationSpeed), n.transitions ? (n.vars.animationLoop && n.atEnd || (n.animating = !1, n.currentSlide = n.animatingTo), n.container.unbind("webkitTransitionEnd transitionend"), n.container.bind("webkitTransitionEnd transitionend", function () {
						clearTimeout(n.ensureAnimationEnd), n.wrapup(c)
					}), clearTimeout(n.ensureAnimationEnd), n.ensureAnimationEnd = setTimeout(function () {
						n.wrapup(c)
					}, n.vars.animationSpeed + 100)) : n.container.animate(n.args, n.vars.animationSpeed, n.vars.easing, function () {
						n.wrapup(c)
					})
				}
				n.vars.smoothHeight && f.smoothHeight(n.vars.animationSpeed)
			}
		}, n.wrapup = function (e) {
			p || v || (0 === n.currentSlide && n.animatingTo === n.last && n.vars.animationLoop ? n.setProps(e, "jumpEnd") : n.currentSlide === n.last && 0 === n.animatingTo && n.vars.animationLoop && n.setProps(e, "jumpStart")), n.animating = !1, n.currentSlide = n.animatingTo, n.vars.after(n)
		}, n.animateSlides = function () {
			!n.animating && e && n.flexAnimate(n.getTarget("next"))
		}, n.pause = function () {
			clearInterval(n.animatedSlides), n.animatedSlides = null, n.playing = !1, n.vars.pausePlay && f.pausePlay.update("play"), n.syncExists && f.sync("pause")
		}, n.play = function () {
			n.playing && clearInterval(n.animatedSlides), n.animatedSlides = n.animatedSlides || setInterval(n.animateSlides, n.vars.slideshowSpeed), n.started = n.playing = !0, n.vars.pausePlay && f.pausePlay.update("pause"), n.syncExists && f.sync("play")
		}, n.stop = function () {
			n.pause(), n.stopped = !0
		}, n.canAdvance = function (e, t) {
			var a = m ? n.pagingCount - 1 : n.last;
			return !!t || (!(!m || n.currentItem !== n.count - 1 || 0 !== e || "prev" !== n.direction) || (!m || 0 !== n.currentItem || e !== n.pagingCount - 1 || "next" === n.direction) && (!(e === n.currentSlide && !m) && (!!n.vars.animationLoop || (!n.atEnd || 0 !== n.currentSlide || e !== a || "next" === n.direction) && (!n.atEnd || n.currentSlide !== a || 0 !== e || "next" !== n.direction))))
		}, n.getTarget = function (e) {
			return n.direction = e, "next" === e ? n.currentSlide === n.last ? 0 : n.currentSlide + 1 : 0 === n.currentSlide ? n.last : n.currentSlide - 1
		}, n.setProps = function (e, t, a) {
			var i = function () {
				var a = e || (n.itemW + n.vars.itemMargin) * n.move * n.animatingTo;
				return function () {
					if (v) return "setTouch" === t ? e : u && n.animatingTo === n.last ? 0 : u ? n.limit - (n.itemW + n.vars.itemMargin) * n.move * n.animatingTo : n.animatingTo === n.last ? n.limit : a;
					switch (t) {
						case "setTotal":
							return u ? (n.count - 1 - n.currentSlide + n.cloneOffset) * e : (n.currentSlide + n.cloneOffset) * e;
						case "setTouch":
							return e;
						case "jumpEnd":
							return u ? e : n.count * e;
						case "jumpStart":
							return u ? n.count * e : e;
						default:
							return e
					}
				}() * (n.vars.rtl ? 1 : -1) + "px"
			}();
			n.transitions && (i = n.isFirefox ? d ? "translate3d(0," + i + ",0)" : "translate3d(" + parseInt(i) + "px,0,0)" : d ? "translate3d(0," + i + ",0)" : "translate3d(" + (n.vars.rtl ? -1 : 1) * parseInt(i) + "px,0,0)", a = void 0 !== a ? a / 1e3 + "s" : "0s", n.container.css("-" + n.pfx + "-transition-duration", a), n.container.css("transition-duration", a)), n.args[n.prop] = i, (n.transitions || void 0 === a) && n.container.css(n.args), n.container.css("transform", i)
		}, n.setup = function (e) {
			if (p) n.vars.rtl ? n.slides.css({
				width: "100%",
				float: "right",
				marginLeft: "-100%",
				position: "relative"
			}) : n.slides.css({
				width: "100%",
				float: "left",
				marginRight: "-100%",
				position: "relative"
			}), "init" === e && (s ? n.slides.css({
				opacity: 0,
				display: "block",
				webkitTransition: "opacity " + n.vars.animationSpeed / 1e3 + "s ease",
				zIndex: 1
			}).eq(n.currentSlide).css({
				opacity: 1,
				zIndex: 2
			}) : 0 == n.vars.fadeFirstSlide ? n.slides.css({
				opacity: 0,
				display: "block",
				zIndex: 1
			}).eq(n.currentSlide).css({
				zIndex: 2
			}).css({
				opacity: 1
			}) : n.slides.css({
				opacity: 0,
				display: "block",
				zIndex: 1
			}).eq(n.currentSlide).css({
				zIndex: 2
			}).animate({
				opacity: 1
			}, n.vars.animationSpeed, n.vars.easing)), n.vars.smoothHeight && f.smoothHeight();
			else {
				var t, a;
				"init" === e && (n.viewport = $('<div class="' + i + 'viewport"></div>').css({
					overflow: "hidden",
					position: "relative"
				}).appendTo(n).append(n.container), n.cloneCount = 0, n.cloneOffset = 0, u && (a = $.makeArray(n.slides).reverse(), n.slides = $(a), n.container.empty().append(n.slides))), n.vars.animationLoop && !v && (n.cloneCount = 2, n.cloneOffset = 1, "init" !== e && n.container.find(".clone").remove(), n.container.append(f.uniqueID(n.slides.first().clone().addClass("clone")).attr("aria-hidden", "true")).prepend(f.uniqueID(n.slides.last().clone().addClass("clone")).attr("aria-hidden", "true"))), n.newSlides = $(n.vars.selector, n), t = u ? n.count - 1 - n.currentSlide + n.cloneOffset : n.currentSlide + n.cloneOffset, d && !v ? (n.container.height(200 * (n.count + n.cloneCount) + "%").css("position", "absolute").width("100%"), setTimeout(function () {
					n.newSlides.css({
						display: "block"
					}), n.doMath(), n.viewport.height(n.h), n.setProps(t * n.h, "init")
				}, "init" === e ? 100 : 0)) : (n.container.width(200 * (n.count + n.cloneCount) + "%"), n.setProps(t * n.computedW, "init"), setTimeout(function () {
					n.doMath(), n.vars.rtl && n.isFirefox ? n.newSlides.css({
						width: n.computedW,
						marginRight: n.computedM,
						float: "right",
						display: "block"
					}) : n.newSlides.css({
						width: n.computedW,
						marginRight: n.computedM,
						float: "left",
						display: "block"
					}), n.vars.smoothHeight && f.smoothHeight()
				}, "init" === e ? 100 : 0))
			}
			v || n.slides.removeClass(i + "active-slide").eq(n.currentSlide).addClass(i + "active-slide"), n.vars.init(n)
		}, n.doMath = function () {
			var e = n.slides.first(),
				t = n.vars.itemMargin,
				a = n.vars.minItems,
				i = n.vars.maxItems;
			n.w = void 0 === n.viewport ? n.width() : n.viewport.width(), n.isFirefox && (n.w = n.width()), n.h = e.height(), n.boxPadding = e.outerWidth() - e.width(), v ? (n.itemT = n.vars.itemWidth + t, n.itemM = t, n.minW = a ? a * n.itemT : n.w, n.maxW = i ? i * n.itemT - t : n.w, n.itemW = n.minW > n.w ? (n.w - t * (a - 1)) / a : n.maxW < n.w ? (n.w - t * (i - 1)) / i : n.vars.itemWidth > n.w ? n.w : n.vars.itemWidth, n.visible = Math.floor(n.w / n.itemW), n.move = n.vars.move > 0 && n.vars.move < n.visible ? n.vars.move : n.visible, n.pagingCount = Math.ceil((n.count - n.visible) / n.move + 1), n.last = n.pagingCount - 1, n.limit = 1 === n.pagingCount ? 0 : n.vars.itemWidth > n.w ? n.itemW * (n.count - 1) + t * (n.count - 1) : (n.itemW + t) * n.count - n.w - t) : (n.itemW = n.w, n.itemM = t, n.pagingCount = n.count, n.last = n.count - 1), n.computedW = n.itemW - n.boxPadding, n.computedM = n.itemM
		}, n.update = function (e, t) {
			n.doMath(), v || (e < n.currentSlide ? n.currentSlide += 1 : e <= n.currentSlide && 0 !== e && (n.currentSlide -= 1), n.animatingTo = n.currentSlide), n.vars.controlNav && !n.manualControls && ("add" === t && !v || n.pagingCount > n.controlNav.length ? f.controlNav.update("add") : ("remove" === t && !v || n.pagingCount < n.controlNav.length) && (v && n.currentSlide > n.last && (n.currentSlide -= 1, n.animatingTo -= 1), f.controlNav.update("remove", n.last))), n.vars.directionNav && f.directionNav.update()
		}, n.addSlide = function (e, t) {
			var a = $(e);
			n.count += 1, n.last = n.count - 1, d && u ? void 0 !== t ? n.slides.eq(n.count - t).after(a) : n.container.prepend(a) : void 0 !== t ? n.slides.eq(t).before(a) : n.container.append(a), n.update(t, "add"), n.slides = $(n.vars.selector + ":not(.clone)", n), n.setup(), n.vars.added(n)
		}, n.removeSlide = function (e) {
			var t = isNaN(e) ? n.slides.index($(e)) : e;
			n.count -= 1, n.last = n.count - 1, isNaN(e) ? $(e, n.slides).remove() : d && u ? n.slides.eq(n.last).remove() : n.slides.eq(e).remove(), n.doMath(), n.update(t, "remove"), n.slides = $(n.vars.selector + ":not(.clone)", n), n.setup(), n.vars.removed(n)
		}, f.init()
	}, $(window).blur(function (t) {
		e = !1
	}).focus(function (t) {
		e = !0
	}), $.flexslider.defaults = {
		namespace: "flex-",
		selector: ".slides > li",
		animation: "fade",
		easing: "swing",
		direction: "horizontal",
		reverse: !1,
		animationLoop: !0,
		smoothHeight: !1,
		startAt: 0,
		slideshow: !0,
		slideshowSpeed: 7e3,
		animationSpeed: 600,
		initDelay: 0,
		randomize: !1,
		fadeFirstSlide: !0,
		thumbCaptions: !1,
		pauseOnAction: !0,
		pauseOnHover: !1,
		pauseInvisible: !0,
		useCSS: !0,
		touch: !0,
		video: !1,
		controlNav: !0,
		directionNav: !0,
		prevText: "Previous",
		nextText: "Next",
		keyboard: !0,
		multipleKeyboard: !1,
		mousewheel: !1,
		pausePlay: !1,
		pauseText: "Pause",
		playText: "Play",
		controlsContainer: "",
		manualControls: "",
		customDirectionNav: "",
		sync: "",
		asNavFor: "",
		itemWidth: 0,
		itemMargin: 0,
		minItems: 1,
		maxItems: 0,
		move: 0,
		allowOneSlide: !0,
		isFirefox: !1,
		start: function () {},
		before: function () {},
		after: function () {},
		end: function () {},
		added: function () {},
		removed: function () {},
		init: function () {},
		rtl: !1
	}, $.fn.flexslider = function (e) {
		if (void 0 === e && (e = {}), "object" == typeof e) return this.each(function () {
			var t = $(this),
				a = e.selector ? e.selector : ".slides > li",
				n = t.find(a);
			1 === n.length && !1 === e.allowOneSlide || 0 === n.length ? (n.fadeIn(400), e.start && e.start(t)) : void 0 === t.data("flexslider") && new $.flexslider(this, e)
		});
		var t = $(this).data("flexslider");
		switch (e) {
			case "play":
				t.play();
				break;
			case "pause":
				t.pause();
				break;
			case "stop":
				t.stop();
				break;
			case "next":
				t.flexAnimate(t.getTarget("next"), !0);
				break;
			case "prev":
			case "previous":
				t.flexAnimate(t.getTarget("prev"), !0);
				break;
			default:
				"number" == typeof e && t.flexAnimate(e, !0)
		}
	}
}(jQuery);
/*! Magnific Popup - v1.1.0 - 2016-02-20
 * http://dimsemenov.com/plugins/magnific-popup/
 * Copyright (c) 2016 Dmitry Semenov; */
! function (a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : a("object" == typeof exports ? require("jquery") : window.jQuery || window.Zepto)
}(function (a) {
	var b, c, d, e, f, g, h = "Close",
		i = "BeforeClose",
		j = "AfterClose",
		k = "BeforeAppend",
		l = "MarkupParse",
		m = "Open",
		n = "Change",
		o = "mfp",
		p = "." + o,
		q = "mfp-ready",
		r = "mfp-removing",
		s = "mfp-prevent-close",
		t = function () {},
		u = !!window.jQuery,
		v = a(window),
		w = function (a, c) {
			b.ev.on(o + a + p, c)
		},
		x = function (b, c, d, e) {
			var f = document.createElement("div");
			return f.className = "mfp-" + b, d && (f.innerHTML = d), e ? c && c.appendChild(f) : (f = a(f), c && f.appendTo(c)), f
		},
		y = function (c, d) {
			b.ev.triggerHandler(o + c, d), b.st.callbacks && (c = c.charAt(0).toLowerCase() + c.slice(1), b.st.callbacks[c] && b.st.callbacks[c].apply(b, a.isArray(d) ? d : [d]))
		},
		z = function (c) {
			return c === g && b.currTemplate.closeBtn || (b.currTemplate.closeBtn = a(b.st.closeMarkup.replace("%title%", b.st.tClose)), g = c), b.currTemplate.closeBtn
		},
		A = function () {
			a.magnificPopup.instance || (b = new t, b.init(), a.magnificPopup.instance = b)
		},
		B = function () {
			var a = document.createElement("p").style,
				b = ["ms", "O", "Moz", "Webkit"];
			if (void 0 !== a.transition) return !0;
			for (; b.length;)
				if (b.pop() + "Transition" in a) return !0;
			return !1
		};
	t.prototype = {
		constructor: t,
		init: function () {
			var c = navigator.appVersion;
			b.isLowIE = b.isIE8 = document.all && !document.addEventListener, b.isAndroid = /android/gi.test(c), b.isIOS = /iphone|ipad|ipod/gi.test(c), b.supportsTransition = B(), b.probablyMobile = b.isAndroid || b.isIOS || /(Opera Mini)|Kindle|webOS|BlackBerry|(Opera Mobi)|(Windows Phone)|IEMobile/i.test(navigator.userAgent), d = a(document), b.popupsCache = {}
		},
		open: function (c) {
			var e;
			if (c.isObj === !1) {
				b.items = c.items.toArray(), b.index = 0;
				var g, h = c.items;
				for (e = 0; e < h.length; e++)
					if (g = h[e], g.parsed && (g = g.el[0]), g === c.el[0]) {
						b.index = e;
						break
					}
			} else b.items = a.isArray(c.items) ? c.items : [c.items], b.index = c.index || 0;
			if (b.isOpen) return void b.updateItemHTML();
			b.types = [], f = "", c.mainEl && c.mainEl.length ? b.ev = c.mainEl.eq(0) : b.ev = d, c.key ? (b.popupsCache[c.key] || (b.popupsCache[c.key] = {}), b.currTemplate = b.popupsCache[c.key]) : b.currTemplate = {}, b.st = a.extend(!0, {}, a.magnificPopup.defaults, c), b.fixedContentPos = "auto" === b.st.fixedContentPos ? !b.probablyMobile : b.st.fixedContentPos, b.st.modal && (b.st.closeOnContentClick = !1, b.st.closeOnBgClick = !1, b.st.showCloseBtn = !1, b.st.enableEscapeKey = !1), b.bgOverlay || (b.bgOverlay = x("bg").on("click" + p, function () {
				b.close()
			}), b.wrap = x("wrap").attr("tabindex", -1).on("click" + p, function (a) {
				b._checkIfClose(a.target) && b.close()
			}), b.container = x("container", b.wrap)), b.contentContainer = x("content"), b.st.preloader && (b.preloader = x("preloader", b.container, b.st.tLoading));
			var i = a.magnificPopup.modules;
			for (e = 0; e < i.length; e++) {
				var j = i[e];
				j = j.charAt(0).toUpperCase() + j.slice(1), b["init" + j].call(b)
			}
			y("BeforeOpen"), b.st.showCloseBtn && (b.st.closeBtnInside ? (w(l, function (a, b, c, d) {
				c.close_replaceWith = z(d.type)
			}), f += " mfp-close-btn-in") : b.wrap.append(z())), b.st.alignTop && (f += " mfp-align-top"), b.fixedContentPos ? b.wrap.css({
				overflow: b.st.overflowY,
				overflowX: "hidden",
				overflowY: b.st.overflowY
			}) : b.wrap.css({
				top: v.scrollTop(),
				position: "absolute"
			}), (b.st.fixedBgPos === !1 || "auto" === b.st.fixedBgPos && !b.fixedContentPos) && b.bgOverlay.css({
				height: d.height(),
				position: "absolute"
			}), b.st.enableEscapeKey && d.on("keyup" + p, function (a) {
				27 === a.keyCode && b.close()
			}), v.on("resize" + p, function () {
				b.updateSize()
			}), b.st.closeOnContentClick || (f += " mfp-auto-cursor"), f && b.wrap.addClass(f);
			var k = b.wH = v.height(),
				n = {};
			if (b.fixedContentPos && b._hasScrollBar(k)) {
				var o = b._getScrollbarSize();
				o && (n.marginRight = o)
			}
			b.fixedContentPos && (b.isIE7 ? a("body, html").css("overflow", "hidden") : n.overflow = "hidden");
			var r = b.st.mainClass;
			return b.isIE7 && (r += " mfp-ie7"), r && b._addClassToMFP(r), b.updateItemHTML(), y("BuildControls"), a("html").css(n), b.bgOverlay.add(b.wrap).prependTo(b.st.prependTo || a(document.body)), b._lastFocusedEl = document.activeElement, setTimeout(function () {
				b.content ? (b._addClassToMFP(q), b._setFocus()) : b.bgOverlay.addClass(q), d.on("focusin" + p, b._onFocusIn)
			}, 16), b.isOpen = !0, b.updateSize(k), y(m), c
		},
		close: function () {
			b.isOpen && (y(i), b.isOpen = !1, b.st.removalDelay && !b.isLowIE && b.supportsTransition ? (b._addClassToMFP(r), setTimeout(function () {
				b._close()
			}, b.st.removalDelay)) : b._close())
		},
		_close: function () {
			y(h);
			var c = r + " " + q + " ";
			if (b.bgOverlay.detach(), b.wrap.detach(), b.container.empty(), b.st.mainClass && (c += b.st.mainClass + " "), b._removeClassFromMFP(c), b.fixedContentPos) {
				var e = {
					marginRight: ""
				};
				b.isIE7 ? a("body, html").css("overflow", "") : e.overflow = "", a("html").css(e)
			}
			d.off("keyup" + p + " focusin" + p), b.ev.off(p), b.wrap.attr("class", "mfp-wrap").removeAttr("style"), b.bgOverlay.attr("class", "mfp-bg"), b.container.attr("class", "mfp-container"), !b.st.showCloseBtn || b.st.closeBtnInside && b.currTemplate[b.currItem.type] !== !0 || b.currTemplate.closeBtn && b.currTemplate.closeBtn.detach(), b.st.autoFocusLast && b._lastFocusedEl && a(b._lastFocusedEl).focus(), b.currItem = null, b.content = null, b.currTemplate = null, b.prevHeight = 0, y(j)
		},
		updateSize: function (a) {
			if (b.isIOS) {
				var c = document.documentElement.clientWidth / window.innerWidth,
					d = window.innerHeight * c;
				b.wrap.css("height", d), b.wH = d
			} else b.wH = a || v.height();
			b.fixedContentPos || b.wrap.css("height", b.wH), y("Resize")
		},
		updateItemHTML: function () {
			var c = b.items[b.index];
			b.contentContainer.detach(), b.content && b.content.detach(), c.parsed || (c = b.parseEl(b.index));
			var d = c.type;
			if (y("BeforeChange", [b.currItem ? b.currItem.type : "", d]), b.currItem = c, !b.currTemplate[d]) {
				var f = b.st[d] ? b.st[d].markup : !1;
				y("FirstMarkupParse", f), f ? b.currTemplate[d] = a(f) : b.currTemplate[d] = !0
			}
			e && e !== c.type && b.container.removeClass("mfp-" + e + "-holder");
			var g = b["get" + d.charAt(0).toUpperCase() + d.slice(1)](c, b.currTemplate[d]);
			b.appendContent(g, d), c.preloaded = !0, y(n, c), e = c.type, b.container.prepend(b.contentContainer), y("AfterChange")
		},
		appendContent: function (a, c) {
			b.content = a, a ? b.st.showCloseBtn && b.st.closeBtnInside && b.currTemplate[c] === !0 ? b.content.find(".mfp-close").length || b.content.append(z()) : b.content = a : b.content = "", y(k), b.container.addClass("mfp-" + c + "-holder"), b.contentContainer.append(b.content)
		},
		parseEl: function (c) {
			var d, e = b.items[c];
			if (e.tagName ? e = {
					el: a(e)
				} : (d = e.type, e = {
					data: e,
					src: e.src
				}), e.el) {
				for (var f = b.types, g = 0; g < f.length; g++)
					if (e.el.hasClass("mfp-" + f[g])) {
						d = f[g];
						break
					}
				e.src = e.el.attr("data-mfp-src"), e.src || (e.src = e.el.attr("href"))
			}
			return e.type = d || b.st.type || "inline", e.index = c, e.parsed = !0, b.items[c] = e, y("ElementParse", e), b.items[c]
		},
		addGroup: function (a, c) {
			var d = function (d) {
				d.mfpEl = this, b._openClick(d, a, c)
			};
			c || (c = {});
			var e = "click.magnificPopup";
			c.mainEl = a, c.items ? (c.isObj = !0, a.off(e).on(e, d)) : (c.isObj = !1, c.delegate ? a.off(e).on(e, c.delegate, d) : (c.items = a, a.off(e).on(e, d)))
		},
		_openClick: function (c, d, e) {
			var f = void 0 !== e.midClick ? e.midClick : a.magnificPopup.defaults.midClick;
			if (f || !(2 === c.which || c.ctrlKey || c.metaKey || c.altKey || c.shiftKey)) {
				var g = void 0 !== e.disableOn ? e.disableOn : a.magnificPopup.defaults.disableOn;
				if (g)
					if (a.isFunction(g)) {
						if (!g.call(b)) return !0
					} else if (v.width() < g) return !0;
				c.type && (c.preventDefault(), b.isOpen && c.stopPropagation()), e.el = a(c.mfpEl), e.delegate && (e.items = d.find(e.delegate)), b.open(e)
			}
		},
		updateStatus: function (a, d) {
			if (b.preloader) {
				c !== a && b.container.removeClass("mfp-s-" + c), d || "loading" !== a || (d = b.st.tLoading);
				var e = {
					status: a,
					text: d
				};
				y("UpdateStatus", e), a = e.status, d = e.text, b.preloader.html(d), b.preloader.find("a").on("click", function (a) {
					a.stopImmediatePropagation()
				}), b.container.addClass("mfp-s-" + a), c = a
			}
		},
		_checkIfClose: function (c) {
			if (!a(c).hasClass(s)) {
				var d = b.st.closeOnContentClick,
					e = b.st.closeOnBgClick;
				if (d && e) return !0;
				if (!b.content || a(c).hasClass("mfp-close") || b.preloader && c === b.preloader[0]) return !0;
				if (c === b.content[0] || a.contains(b.content[0], c)) {
					if (d) return !0
				} else if (e && a.contains(document, c)) return !0;
				return !1
			}
		},
		_addClassToMFP: function (a) {
			b.bgOverlay.addClass(a), b.wrap.addClass(a)
		},
		_removeClassFromMFP: function (a) {
			this.bgOverlay.removeClass(a), b.wrap.removeClass(a)
		},
		_hasScrollBar: function (a) {
			return (b.isIE7 ? d.height() : document.body.scrollHeight) > (a || v.height())
		},
		_setFocus: function () {
			(b.st.focus ? b.content.find(b.st.focus).eq(0) : b.wrap).focus()
		},
		_onFocusIn: function (c) {
			return c.target === b.wrap[0] || a.contains(b.wrap[0], c.target) ? void 0 : (b._setFocus(), !1)
		},
		_parseMarkup: function (b, c, d) {
			var e;
			d.data && (c = a.extend(d.data, c)), y(l, [b, c, d]), a.each(c, function (c, d) {
				if (void 0 === d || d === !1) return !0;
				if (e = c.split("_"), e.length > 1) {
					var f = b.find(p + "-" + e[0]);
					if (f.length > 0) {
						var g = e[1];
						"replaceWith" === g ? f[0] !== d[0] && f.replaceWith(d) : "img" === g ? f.is("img") ? f.attr("src", d) : f.replaceWith(a("<img>").attr("src", d).attr("class", f.attr("class"))) : f.attr(e[1], d)
					}
				} else b.find(p + "-" + c).html(d)
			})
		},
		_getScrollbarSize: function () {
			if (void 0 === b.scrollbarSize) {
				var a = document.createElement("div");
				a.style.cssText = "width: 99px; height: 99px; overflow: scroll; position: absolute; top: -9999px;", document.body.appendChild(a), b.scrollbarSize = a.offsetWidth - a.clientWidth, document.body.removeChild(a)
			}
			return b.scrollbarSize
		}
	}, a.magnificPopup = {
		instance: null,
		proto: t.prototype,
		modules: [],
		open: function (b, c) {
			return A(), b = b ? a.extend(!0, {}, b) : {}, b.isObj = !0, b.index = c || 0, this.instance.open(b)
		},
		close: function () {
			return a.magnificPopup.instance && a.magnificPopup.instance.close()
		},
		registerModule: function (b, c) {
			c.options && (a.magnificPopup.defaults[b] = c.options), a.extend(this.proto, c.proto), this.modules.push(b)
		},
		defaults: {
			disableOn: 0,
			key: null,
			midClick: !1,
			mainClass: "",
			preloader: !0,
			focus: "",
			closeOnContentClick: !1,
			closeOnBgClick: !0,
			closeBtnInside: !0,
			showCloseBtn: !0,
			enableEscapeKey: !0,
			modal: !1,
			alignTop: !1,
			removalDelay: 0,
			prependTo: null,
			fixedContentPos: "auto",
			fixedBgPos: "auto",
			overflowY: "auto",
			closeMarkup: '<button title="%title%" type="button" class="mfp-close">&#215;</button>',
			tClose: "Close (Esc)",
			tLoading: "Loading...",
			autoFocusLast: !0
		}
	}, a.fn.magnificPopup = function (c) {
		A();
		var d = a(this);
		if ("string" == typeof c)
			if ("open" === c) {
				var e, f = u ? d.data("magnificPopup") : d[0].magnificPopup,
					g = parseInt(arguments[1], 10) || 0;
				f.items ? e = f.items[g] : (e = d, f.delegate && (e = e.find(f.delegate)), e = e.eq(g)), b._openClick({
					mfpEl: e
				}, d, f)
			} else b.isOpen && b[c].apply(b, Array.prototype.slice.call(arguments, 1));
		else c = a.extend(!0, {}, c), u ? d.data("magnificPopup", c) : d[0].magnificPopup = c, b.addGroup(d, c);
		return d
	};
	var C, D, E, F = "inline",
		G = function () {
			E && (D.after(E.addClass(C)).detach(), E = null)
		};
	a.magnificPopup.registerModule(F, {
		options: {
			hiddenClass: "hide",
			markup: "",
			tNotFound: "Content not found"
		},
		proto: {
			initInline: function () {
				b.types.push(F), w(h + "." + F, function () {
					G()
				})
			},
			getInline: function (c, d) {
				if (G(), c.src) {
					var e = b.st.inline,
						f = a(c.src);
					if (f.length) {
						var g = f[0].parentNode;
						g && g.tagName && (D || (C = e.hiddenClass, D = x(C), C = "mfp-" + C), E = f.after(D).detach().removeClass(C)), b.updateStatus("ready")
					} else b.updateStatus("error", e.tNotFound), f = a("<div>");
					return c.inlineElement = f, f
				}
				return b.updateStatus("ready"), b._parseMarkup(d, {}, c), d
			}
		}
	});
	var H, I = "ajax",
		J = function () {
			H && a(document.body).removeClass(H)
		},
		K = function () {
			J(), b.req && b.req.abort()
		};
	a.magnificPopup.registerModule(I, {
		options: {
			settings: null,
			cursor: "mfp-ajax-cur",
			tError: '<a href="%url%">The content</a> could not be loaded.'
		},
		proto: {
			initAjax: function () {
				b.types.push(I), H = b.st.ajax.cursor, w(h + "." + I, K), w("BeforeChange." + I, K)
			},
			getAjax: function (c) {
				H && a(document.body).addClass(H), b.updateStatus("loading");
				var d = a.extend({
					url: c.src,
					success: function (d, e, f) {
						var g = {
							data: d,
							xhr: f
						};
						y("ParseAjax", g), b.appendContent(a(g.data), I), c.finished = !0, J(), b._setFocus(), setTimeout(function () {
							b.wrap.addClass(q)
						}, 16), b.updateStatus("ready"), y("AjaxContentAdded")
					},
					error: function () {
						J(), c.finished = c.loadError = !0, b.updateStatus("error", b.st.ajax.tError.replace("%url%", c.src))
					}
				}, b.st.ajax.settings);
				return b.req = a.ajax(d), ""
			}
		}
	});
	var L, M = function (c) {
		if (c.data && void 0 !== c.data.title) return c.data.title;
		var d = b.st.image.titleSrc;
		if (d) {
			if (a.isFunction(d)) return d.call(b, c);
			if (c.el) return c.el.attr(d) || ""
		}
		return ""
	};
	a.magnificPopup.registerModule("image", {
		options: {
			markup: '<div class="mfp-figure"><div class="mfp-close"></div><figure><div class="mfp-img"></div><figcaption><div class="mfp-bottom-bar"><div class="mfp-title"></div><div class="mfp-counter"></div></div></figcaption></figure></div>',
			cursor: "mfp-zoom-out-cur",
			titleSrc: "title",
			verticalFit: !0,
			tError: '<a href="%url%">The image</a> could not be loaded.'
		},
		proto: {
			initImage: function () {
				var c = b.st.image,
					d = ".image";
				b.types.push("image"), w(m + d, function () {
					"image" === b.currItem.type && c.cursor && a(document.body).addClass(c.cursor)
				}), w(h + d, function () {
					c.cursor && a(document.body).removeClass(c.cursor), v.off("resize" + p)
				}), w("Resize" + d, b.resizeImage), b.isLowIE && w("AfterChange", b.resizeImage)
			},
			resizeImage: function () {
				var a = b.currItem;
				if (a && a.img && b.st.image.verticalFit) {
					var c = 0;
					b.isLowIE && (c = parseInt(a.img.css("padding-top"), 10) + parseInt(a.img.css("padding-bottom"), 10)), a.img.css("max-height", b.wH - c)
				}
			},
			_onImageHasSize: function (a) {
				a.img && (a.hasSize = !0, L && clearInterval(L), a.isCheckingImgSize = !1, y("ImageHasSize", a), a.imgHidden && (b.content && b.content.removeClass("mfp-loading"), a.imgHidden = !1))
			},
			findImageSize: function (a) {
				var c = 0,
					d = a.img[0],
					e = function (f) {
						L && clearInterval(L), L = setInterval(function () {
							return d.naturalWidth > 0 ? void b._onImageHasSize(a) : (c > 200 && clearInterval(L), c++, void(3 === c ? e(10) : 40 === c ? e(50) : 100 === c && e(500)))
						}, f)
					};
				e(1)
			},
			getImage: function (c, d) {
				var e = 0,
					f = function () {
						c && (c.img[0].complete ? (c.img.off(".mfploader"), c === b.currItem && (b._onImageHasSize(c), b.updateStatus("ready")), c.hasSize = !0, c.loaded = !0, y("ImageLoadComplete")) : (e++, 200 > e ? setTimeout(f, 100) : g()))
					},
					g = function () {
						c && (c.img.off(".mfploader"), c === b.currItem && (b._onImageHasSize(c), b.updateStatus("error", h.tError.replace("%url%", c.src))), c.hasSize = !0, c.loaded = !0, c.loadError = !0)
					},
					h = b.st.image,
					i = d.find(".mfp-img");
				if (i.length) {
					var j = document.createElement("img");
					j.className = "mfp-img", c.el && c.el.find("img").length && (j.alt = c.el.find("img").attr("alt")), c.img = a(j).on("load.mfploader", f).on("error.mfploader", g), j.src = c.src, i.is("img") && (c.img = c.img.clone()), j = c.img[0], j.naturalWidth > 0 ? c.hasSize = !0 : j.width || (c.hasSize = !1)
				}
				return b._parseMarkup(d, {
					title: M(c),
					img_replaceWith: c.img
				}, c), b.resizeImage(), c.hasSize ? (L && clearInterval(L), c.loadError ? (d.addClass("mfp-loading"), b.updateStatus("error", h.tError.replace("%url%", c.src))) : (d.removeClass("mfp-loading"), b.updateStatus("ready")), d) : (b.updateStatus("loading"), c.loading = !0, c.hasSize || (c.imgHidden = !0, d.addClass("mfp-loading"), b.findImageSize(c)), d)
			}
		}
	});
	var N, O = function () {
		return void 0 === N && (N = void 0 !== document.createElement("p").style.MozTransform), N
	};
	a.magnificPopup.registerModule("zoom", {
		options: {
			enabled: !1,
			easing: "ease-in-out",
			duration: 300,
			opener: function (a) {
				return a.is("img") ? a : a.find("img")
			}
		},
		proto: {
			initZoom: function () {
				var a, c = b.st.zoom,
					d = ".zoom";
				if (c.enabled && b.supportsTransition) {
					var e, f, g = c.duration,
						j = function (a) {
							var b = a.clone().removeAttr("style").removeAttr("class").addClass("mfp-animated-image"),
								d = "all " + c.duration / 1e3 + "s " + c.easing,
								e = {
									position: "fixed",
									zIndex: 9999,
									left: 0,
									top: 0,
									"-webkit-backface-visibility": "hidden"
								},
								f = "transition";
							return e["-webkit-" + f] = e["-moz-" + f] = e["-o-" + f] = e[f] = d, b.css(e), b
						},
						k = function () {
							b.content.css("visibility", "visible")
						};
					w("BuildControls" + d, function () {
						if (b._allowZoom()) {
							if (clearTimeout(e), b.content.css("visibility", "hidden"), a = b._getItemToZoom(), !a) return void k();
							f = j(a), f.css(b._getOffset()), b.wrap.append(f), e = setTimeout(function () {
								f.css(b._getOffset(!0)), e = setTimeout(function () {
									k(), setTimeout(function () {
										f.remove(), a = f = null, y("ZoomAnimationEnded")
									}, 16)
								}, g)
							}, 16)
						}
					}), w(i + d, function () {
						if (b._allowZoom()) {
							if (clearTimeout(e), b.st.removalDelay = g, !a) {
								if (a = b._getItemToZoom(), !a) return;
								f = j(a)
							}
							f.css(b._getOffset(!0)), b.wrap.append(f), b.content.css("visibility", "hidden"), setTimeout(function () {
								f.css(b._getOffset())
							}, 16)
						}
					}), w(h + d, function () {
						b._allowZoom() && (k(), f && f.remove(), a = null)
					})
				}
			},
			_allowZoom: function () {
				return "image" === b.currItem.type
			},
			_getItemToZoom: function () {
				return b.currItem.hasSize ? b.currItem.img : !1
			},
			_getOffset: function (c) {
				var d;
				d = c ? b.currItem.img : b.st.zoom.opener(b.currItem.el || b.currItem);
				var e = d.offset(),
					f = parseInt(d.css("padding-top"), 10),
					g = parseInt(d.css("padding-bottom"), 10);
				e.top -= a(window).scrollTop() - f;
				var h = {
					width: d.width(),
					height: (u ? d.innerHeight() : d[0].offsetHeight) - g - f
				};
				return O() ? h["-moz-transform"] = h.transform = "translate(" + e.left + "px," + e.top + "px)" : (h.left = e.left, h.top = e.top), h
			}
		}
	});
	var P = "iframe",
		Q = "//about:blank",
		R = function (a) {
			if (b.currTemplate[P]) {
				var c = b.currTemplate[P].find("iframe");
				c.length && (a || (c[0].src = Q), b.isIE8 && c.css("display", a ? "block" : "none"))
			}
		};
	a.magnificPopup.registerModule(P, {
		options: {
			markup: '<div class="mfp-iframe-scaler"><div class="mfp-close"></div><iframe class="mfp-iframe" src="//about:blank" frameborder="0" allowfullscreen></iframe></div>',
			srcAction: "iframe_src",
			patterns: {
				youtube: {
					index: "youtube.com",
					id: "v=",
					src: "//www.youtube.com/embed/%id%?autoplay=1"
				},
				vimeo: {
					index: "vimeo.com/",
					id: "/",
					src: "//player.vimeo.com/video/%id%?autoplay=1"
				},
				gmaps: {
					index: "//maps.google.",
					src: "%id%&output=embed"
				}
			}
		},
		proto: {
			initIframe: function () {
				b.types.push(P), w("BeforeChange", function (a, b, c) {
					b !== c && (b === P ? R() : c === P && R(!0))
				}), w(h + "." + P, function () {
					R()
				})
			},
			getIframe: function (c, d) {
				var e = c.src,
					f = b.st.iframe;
				a.each(f.patterns, function () {
					return e.indexOf(this.index) > -1 ? (this.id && (e = "string" == typeof this.id ? e.substr(e.lastIndexOf(this.id) + this.id.length, e.length) : this.id.call(this, e)), e = this.src.replace("%id%", e), !1) : void 0
				});
				var g = {};
				return f.srcAction && (g[f.srcAction] = e), b._parseMarkup(d, g, c), b.updateStatus("ready"), d
			}
		}
	});
	var S = function (a) {
			var c = b.items.length;
			return a > c - 1 ? a - c : 0 > a ? c + a : a
		},
		T = function (a, b, c) {
			return a.replace(/%curr%/gi, b + 1).replace(/%total%/gi, c)
		};
	a.magnificPopup.registerModule("gallery", {
		options: {
			enabled: !1,
			arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"></button>',
			preload: [0, 2],
			navigateByImgClick: !0,
			arrows: !0,
			tPrev: "Previous (Left arrow key)",
			tNext: "Next (Right arrow key)",
			tCounter: "%curr% of %total%"
		},
		proto: {
			initGallery: function () {
				var c = b.st.gallery,
					e = ".mfp-gallery";
				return b.direction = !0, c && c.enabled ? (f += " mfp-gallery", w(m + e, function () {
					c.navigateByImgClick && b.wrap.on("click" + e, ".mfp-img", function () {
						return b.items.length > 1 ? (b.next(), !1) : void 0
					}), d.on("keydown" + e, function (a) {
						37 === a.keyCode ? b.prev() : 39 === a.keyCode && b.next()
					})
				}), w("UpdateStatus" + e, function (a, c) {
					c.text && (c.text = T(c.text, b.currItem.index, b.items.length))
				}), w(l + e, function (a, d, e, f) {
					var g = b.items.length;
					e.counter = g > 1 ? T(c.tCounter, f.index, g) : ""
				}), w("BuildControls" + e, function () {
					if (b.items.length > 1 && c.arrows && !b.arrowLeft) {
						var d = c.arrowMarkup,
							e = b.arrowLeft = a(d.replace(/%title%/gi, c.tPrev).replace(/%dir%/gi, "left")).addClass(s),
							f = b.arrowRight = a(d.replace(/%title%/gi, c.tNext).replace(/%dir%/gi, "right")).addClass(s);
						e.click(function () {
							b.prev()
						}), f.click(function () {
							b.next()
						}), b.container.append(e.add(f))
					}
				}), w(n + e, function () {
					b._preloadTimeout && clearTimeout(b._preloadTimeout), b._preloadTimeout = setTimeout(function () {
						b.preloadNearbyImages(), b._preloadTimeout = null
					}, 16)
				}), void w(h + e, function () {
					d.off(e), b.wrap.off("click" + e), b.arrowRight = b.arrowLeft = null
				})) : !1
			},
			next: function () {
				b.direction = !0, b.index = S(b.index + 1), b.updateItemHTML()
			},
			prev: function () {
				b.direction = !1, b.index = S(b.index - 1), b.updateItemHTML()
			},
			goTo: function (a) {
				b.direction = a >= b.index, b.index = a, b.updateItemHTML()
			},
			preloadNearbyImages: function () {
				var a, c = b.st.gallery.preload,
					d = Math.min(c[0], b.items.length),
					e = Math.min(c[1], b.items.length);
				for (a = 1; a <= (b.direction ? e : d); a++) b._preloadItem(b.index + a);
				for (a = 1; a <= (b.direction ? d : e); a++) b._preloadItem(b.index - a)
			},
			_preloadItem: function (c) {
				if (c = S(c), !b.items[c].preloaded) {
					var d = b.items[c];
					d.parsed || (d = b.parseEl(c)), y("LazyLoad", d), "image" === d.type && (d.img = a('<img class="mfp-img" />').on("load.mfploader", function () {
						d.hasSize = !0
					}).on("error.mfploader", function () {
						d.hasSize = !0, d.loadError = !0, y("LazyLoadError", d)
					}).attr("src", d.src)), d.preloaded = !0
				}
			}
		}
	});
	var U = "retina";
	a.magnificPopup.registerModule(U, {
		options: {
			replaceSrc: function (a) {
				return a.src.replace(/\.\w+$/, function (a) {
					return "@2x" + a
				})
			},
			ratio: 1
		},
		proto: {
			initRetina: function () {
				if (window.devicePixelRatio > 1) {
					var a = b.st.retina,
						c = a.ratio;
					c = isNaN(c) ? c() : c, c > 1 && (w("ImageHasSize." + U, function (a, b) {
						b.img.css({
							"max-width": b.img[0].naturalWidth / c,
							width: "100%"
						})
					}), w("ElementParse." + U, function (b, d) {
						d.src = a.replaceSrc(d, c)
					}))
				}
			}
		}
	}), A()
});
eval(function (p, a, c, k, e, r) {
	e = function (c) {
		return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36))
	};
	if (!''.replace(/^/, String)) {
		while (c--) r[e(c)] = k[c] || e(c);
		k = [function (e) {
			return r[e]
		}];
		e = function () {
			return '\\w+'
		};
		c = 1
	};
	while (c--)
		if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]);
	return p
}('7(A 3c.3q!=="9"){3c.3q=9(e){9 t(){}t.5S=e;p 5R t}}(9(e,t,n){h r={1N:9(t,n){h r=c;r.$k=e(n);r.6=e.4M({},e.37.2B.6,r.$k.v(),t);r.2A=t;r.4L()},4L:9(){9 r(e){h n,r="";7(A t.6.33==="9"){t.6.33.R(c,[e])}l{1A(n 38 e.d){7(e.d.5M(n)){r+=e.d[n].1K}}t.$k.2y(r)}t.3t()}h t=c,n;7(A t.6.2H==="9"){t.6.2H.R(c,[t.$k])}7(A t.6.2O==="2Y"){n=t.6.2O;e.5K(n,r)}l{t.3t()}},3t:9(){h e=c;e.$k.v("d-4I",e.$k.2x("2w")).v("d-4F",e.$k.2x("H"));e.$k.z({2u:0});e.2t=e.6.q;e.4E();e.5v=0;e.1X=14;e.23()},23:9(){h e=c;7(e.$k.25().N===0){p b}e.1M();e.4C();e.$S=e.$k.25();e.E=e.$S.N;e.4B();e.$G=e.$k.17(".d-1K");e.$K=e.$k.17(".d-1p");e.3u="U";e.13=0;e.26=[0];e.m=0;e.4A();e.4z()},4z:9(){h e=c;e.2V();e.2W();e.4t();e.30();e.4r();e.4q();e.2p();e.4o();7(e.6.2o!==b){e.4n(e.6.2o)}7(e.6.O===j){e.6.O=4Q}e.19();e.$k.17(".d-1p").z("4i","4h");7(!e.$k.2m(":3n")){e.3o()}l{e.$k.z("2u",1)}e.5O=b;e.2l();7(A e.6.3s==="9"){e.6.3s.R(c,[e.$k])}},2l:9(){h e=c;7(e.6.1Z===j){e.1Z()}7(e.6.1B===j){e.1B()}e.4g();7(A e.6.3w==="9"){e.6.3w.R(c,[e.$k])}},3x:9(){h e=c;7(A e.6.3B==="9"){e.6.3B.R(c,[e.$k])}e.3o();e.2V();e.2W();e.4f();e.30();e.2l();7(A e.6.3D==="9"){e.6.3D.R(c,[e.$k])}},3F:9(){h e=c;t.1c(9(){e.3x()},0)},3o:9(){h e=c;7(e.$k.2m(":3n")===b){e.$k.z({2u:0});t.18(e.1C);t.18(e.1X)}l{p b}e.1X=t.4d(9(){7(e.$k.2m(":3n")){e.3F();e.$k.4b({2u:1},2M);t.18(e.1X)}},5x)},4B:9(){h e=c;e.$S.5n(\'<L H="d-1p">\').4a(\'<L H="d-1K"></L>\');e.$k.17(".d-1p").4a(\'<L H="d-1p-49">\');e.1H=e.$k.17(".d-1p-49");e.$k.z("4i","4h")},1M:9(){h e=c,t=e.$k.1I(e.6.1M),n=e.$k.1I(e.6.2i);7(!t){e.$k.I(e.6.1M)}7(!n){e.$k.I(e.6.2i)}},2V:9(){h t=c,n,r;7(t.6.2Z===b){p b}7(t.6.48===j){t.6.q=t.2t=1;t.6.1h=b;t.6.1s=b;t.6.1O=b;t.6.22=b;t.6.1Q=b;t.6.1R=b;p b}n=e(t.6.47).1f();7(n>(t.6.1s[0]||t.2t)){t.6.q=t.2t}7(t.6.1h!==b){t.6.1h.5g(9(e,t){p e[0]-t[0]});1A(r=0;r<t.6.1h.N;r+=1){7(t.6.1h[r][0]<=n){t.6.q=t.6.1h[r][1]}}}l{7(n<=t.6.1s[0]&&t.6.1s!==b){t.6.q=t.6.1s[1]}7(n<=t.6.1O[0]&&t.6.1O!==b){t.6.q=t.6.1O[1]}7(n<=t.6.22[0]&&t.6.22!==b){t.6.q=t.6.22[1]}7(n<=t.6.1Q[0]&&t.6.1Q!==b){t.6.q=t.6.1Q[1]}7(n<=t.6.1R[0]&&t.6.1R!==b){t.6.q=t.6.1R[1]}}7(t.6.q>t.E&&t.6.46===j){t.6.q=t.E}},4r:9(){h n=c,r,i;7(n.6.2Z!==j){p b}i=e(t).1f();n.3d=9(){7(e(t).1f()!==i){7(n.6.O!==b){t.18(n.1C)}t.5d(r);r=t.1c(9(){i=e(t).1f();n.3x()},n.6.45)}};e(t).44(n.3d)},4f:9(){h e=c;e.2g(e.m);7(e.6.O!==b){e.3j()}},43:9(){h t=c,n=0,r=t.E-t.6.q;t.$G.2f(9(i){h s=e(c);s.z({1f:t.M}).v("d-1K",3p(i));7(i%t.6.q===0||i===r){7(!(i>r)){n+=1}}s.v("d-24",n)})},42:9(){h e=c,t=e.$G.N*e.M;e.$K.z({1f:t*2,T:0});e.43()},2W:9(){h e=c;e.40();e.42();e.3Z();e.3v()},40:9(){h e=c;e.M=1F.4O(e.$k.1f()/e.6.q)},3v:9(){h e=c,t=(e.E*e.M-e.6.q*e.M)*-1;7(e.6.q>e.E){e.D=0;t=0;e.3z=0}l{e.D=e.E-e.6.q;e.3z=t}p t},3Y:9(){p 0},3Z:9(){h t=c,n=0,r=0,i,s,o;t.J=[0];t.3E=[];1A(i=0;i<t.E;i+=1){r+=t.M;t.J.2D(-r);7(t.6.12===j){s=e(t.$G[i]);o=s.v("d-24");7(o!==n){t.3E[n]=t.J[i];n=o}}}},4t:9(){h t=c;7(t.6.2a===j||t.6.1v===j){t.B=e(\'<L H="d-5A"/>\').5m("5l",!t.F.15).5c(t.$k)}7(t.6.1v===j){t.3T()}7(t.6.2a===j){t.3S()}},3S:9(){h t=c,n=e(\'<L H="d-4U"/>\');t.B.1o(n);t.1u=e("<L/>",{"H":"d-1n",2y:t.6.2U[0]||""});t.1q=e("<L/>",{"H":"d-U",2y:t.6.2U[1]||""});n.1o(t.1u).1o(t.1q);n.w("2X.B 21.B",\'L[H^="d"]\',9(e){e.1l()});n.w("2n.B 28.B",\'L[H^="d"]\',9(n){n.1l();7(e(c).1I("d-U")){t.U()}l{t.1n()}})},3T:9(){h t=c;t.1k=e(\'<L H="d-1v"/>\');t.B.1o(t.1k);t.1k.w("2n.B 28.B",".d-1j",9(n){n.1l();7(3p(e(c).v("d-1j"))!==t.m){t.1g(3p(e(c).v("d-1j")),j)}})},3P:9(){h t=c,n,r,i,s,o,u;7(t.6.1v===b){p b}t.1k.2y("");n=0;r=t.E-t.E%t.6.q;1A(s=0;s<t.E;s+=1){7(s%t.6.q===0){n+=1;7(r===s){i=t.E-t.6.q}o=e("<L/>",{"H":"d-1j"});u=e("<3N></3N>",{4R:t.6.39===j?n:"","H":t.6.39===j?"d-59":""});o.1o(u);o.v("d-1j",r===s?i:s);o.v("d-24",n);t.1k.1o(o)}}t.35()},35:9(){h t=c;7(t.6.1v===b){p b}t.1k.17(".d-1j").2f(9(){7(e(c).v("d-24")===e(t.$G[t.m]).v("d-24")){t.1k.17(".d-1j").Z("2d");e(c).I("2d")}})},3e:9(){h e=c;7(e.6.2a===b){p b}7(e.6.2e===b){7(e.m===0&&e.D===0){e.1u.I("1b");e.1q.I("1b")}l 7(e.m===0&&e.D!==0){e.1u.I("1b");e.1q.Z("1b")}l 7(e.m===e.D){e.1u.Z("1b");e.1q.I("1b")}l 7(e.m!==0&&e.m!==e.D){e.1u.Z("1b");e.1q.Z("1b")}}},30:9(){h e=c;e.3P();e.3e();7(e.B){7(e.6.q>=e.E){e.B.3K()}l{e.B.3J()}}},55:9(){h e=c;7(e.B){e.B.3k()}},U:9(e){h t=c;7(t.1E){p b}t.m+=t.6.12===j?t.6.q:1;7(t.m>t.D+(t.6.12===j?t.6.q-1:0)){7(t.6.2e===j){t.m=0;e="2k"}l{t.m=t.D;p b}}t.1g(t.m,e)},1n:9(e){h t=c;7(t.1E){p b}7(t.6.12===j&&t.m>0&&t.m<t.6.q){t.m=0}l{t.m-=t.6.12===j?t.6.q:1}7(t.m<0){7(t.6.2e===j){t.m=t.D;e="2k"}l{t.m=0;p b}}t.1g(t.m,e)},1g:9(e,n,r){h i=c,s;7(i.1E){p b}7(A i.6.1Y==="9"){i.6.1Y.R(c,[i.$k])}7(e>=i.D){e=i.D}l 7(e<=0){e=0}i.m=i.d.m=e;7(i.6.2o!==b&&r!=="4e"&&i.6.q===1&&i.F.1x===j){i.1t(0);7(i.F.1x===j){i.1L(i.J[e])}l{i.1r(i.J[e],1)}i.2r();i.4l();p b}s=i.J[e];7(i.F.1x===j){i.1T=b;7(n===j){i.1t("1w");t.1c(9(){i.1T=j},i.6.1w)}l 7(n==="2k"){i.1t(i.6.2v);t.1c(9(){i.1T=j},i.6.2v)}l{i.1t("1m");t.1c(9(){i.1T=j},i.6.1m)}i.1L(s)}l{7(n===j){i.1r(s,i.6.1w)}l 7(n==="2k"){i.1r(s,i.6.2v)}l{i.1r(s,i.6.1m)}}i.2r()},2g:9(e){h t=c;7(A t.6.1Y==="9"){t.6.1Y.R(c,[t.$k])}7(e>=t.D||e===-1){e=t.D}l 7(e<=0){e=0}t.1t(0);7(t.F.1x===j){t.1L(t.J[e])}l{t.1r(t.J[e],1)}t.m=t.d.m=e;t.2r()},2r:9(){h e=c;e.26.2D(e.m);e.13=e.d.13=e.26[e.26.N-2];e.26.5f(0);7(e.13!==e.m){e.35();e.3e();e.2l();7(e.6.O!==b){e.3j()}}7(A e.6.3y==="9"&&e.13!==e.m){e.6.3y.R(c,[e.$k])}},X:9(){h e=c;e.3A="X";t.18(e.1C)},3j:9(){h e=c;7(e.3A!=="X"){e.19()}},19:9(){h e=c;e.3A="19";7(e.6.O===b){p b}t.18(e.1C);e.1C=t.4d(9(){e.U(j)},e.6.O)},1t:9(e){h t=c;7(e==="1m"){t.$K.z(t.2z(t.6.1m))}l 7(e==="1w"){t.$K.z(t.2z(t.6.1w))}l 7(A e!=="2Y"){t.$K.z(t.2z(e))}},2z:9(e){p{"-1G-1a":"2C "+e+"1z 2s","-1W-1a":"2C "+e+"1z 2s","-o-1a":"2C "+e+"1z 2s",1a:"2C "+e+"1z 2s"}},3H:9(){p{"-1G-1a":"","-1W-1a":"","-o-1a":"",1a:""}},3I:9(e){p{"-1G-P":"1i("+e+"V, C, C)","-1W-P":"1i("+e+"V, C, C)","-o-P":"1i("+e+"V, C, C)","-1z-P":"1i("+e+"V, C, C)",P:"1i("+e+"V, C,C)"}},1L:9(e){h t=c;t.$K.z(t.3I(e))},3L:9(e){h t=c;t.$K.z({T:e})},1r:9(e,t){h n=c;n.29=b;n.$K.X(j,j).4b({T:e},{54:t||n.6.1m,3M:9(){n.29=j}})},4E:9(){h e=c,r="1i(C, C, C)",i=n.56("L"),s,o,u,a;i.2w.3O="  -1W-P:"+r+"; -1z-P:"+r+"; -o-P:"+r+"; -1G-P:"+r+"; P:"+r;s=/1i\\(C, C, C\\)/g;o=i.2w.3O.5i(s);u=o!==14&&o.N===1;a="5z"38 t||t.5Q.4P;e.F={1x:u,15:a}},4q:9(){h e=c;7(e.6.27!==b||e.6.1U!==b){e.3Q();e.3R()}},4C:9(){h e=c,t=["s","e","x"];e.16={};7(e.6.27===j&&e.6.1U===j){t=["2X.d 21.d","2N.d 3U.d","2n.d 3V.d 28.d"]}l 7(e.6.27===b&&e.6.1U===j){t=["2X.d","2N.d","2n.d 3V.d"]}l 7(e.6.27===j&&e.6.1U===b){t=["21.d","3U.d","28.d"]}e.16.3W=t[0];e.16.2K=t[1];e.16.2J=t[2]},3R:9(){h t=c;t.$k.w("5y.d",9(e){e.1l()});t.$k.w("21.3X",9(t){p e(t.1d).2m("5C, 5E, 5F, 5N")})},3Q:9(){9 s(e){7(e.2b!==W){p{x:e.2b[0].2c,y:e.2b[0].41}}7(e.2b===W){7(e.2c!==W){p{x:e.2c,y:e.41}}7(e.2c===W){p{x:e.52,y:e.53}}}}9 o(t){7(t==="w"){e(n).w(r.16.2K,a);e(n).w(r.16.2J,f)}l 7(t==="Q"){e(n).Q(r.16.2K);e(n).Q(r.16.2J)}}9 u(n){h u=n.3h||n||t.3g,a;7(u.5a===3){p b}7(r.E<=r.6.q){p}7(r.29===b&&!r.6.3f){p b}7(r.1T===b&&!r.6.3f){p b}7(r.6.O!==b){t.18(r.1C)}7(r.F.15!==j&&!r.$K.1I("3b")){r.$K.I("3b")}r.11=0;r.Y=0;e(c).z(r.3H());a=e(c).2h();i.2S=a.T;i.2R=s(u).x-a.T;i.2P=s(u).y-a.5o;o("w");i.2j=b;i.2L=u.1d||u.4c}9 a(o){h u=o.3h||o||t.3g,a,f;r.11=s(u).x-i.2R;r.2I=s(u).y-i.2P;r.Y=r.11-i.2S;7(A r.6.2E==="9"&&i.3C!==j&&r.Y!==0){i.3C=j;r.6.2E.R(r,[r.$k])}7((r.Y>8||r.Y<-8)&&r.F.15===j){7(u.1l!==W){u.1l()}l{u.5L=b}i.2j=j}7((r.2I>10||r.2I<-10)&&i.2j===b){e(n).Q("2N.d")}a=9(){p r.Y/5};f=9(){p r.3z+r.Y/5};r.11=1F.3v(1F.3Y(r.11,a()),f());7(r.F.1x===j){r.1L(r.11)}l{r.3L(r.11)}}9 f(n){h s=n.3h||n||t.3g,u,a,f;s.1d=s.1d||s.4c;i.3C=b;7(r.F.15!==j){r.$K.Z("3b")}7(r.Y<0){r.1y=r.d.1y="T"}l{r.1y=r.d.1y="3i"}7(r.Y!==0){u=r.4j();r.1g(u,b,"4e");7(i.2L===s.1d&&r.F.15!==j){e(s.1d).w("3a.4k",9(t){t.4S();t.4T();t.1l();e(t.1d).Q("3a.4k")});a=e.4N(s.1d,"4V").3a;f=a.4W();a.4X(0,0,f)}}o("Q")}h r=c,i={2R:0,2P:0,4Y:0,2S:0,2h:14,4Z:14,50:14,2j:14,51:14,2L:14};r.29=j;r.$k.w(r.16.3W,".d-1p",u)},4j:9(){h e=c,t=e.4m();7(t>e.D){e.m=e.D;t=e.D}l 7(e.11>=0){t=0;e.m=0}p t},4m:9(){h t=c,n=t.6.12===j?t.3E:t.J,r=t.11,i=14;e.2f(n,9(s,o){7(r-t.M/20>n[s+1]&&r-t.M/20<o&&t.34()==="T"){i=o;7(t.6.12===j){t.m=e.4p(i,t.J)}l{t.m=s}}l 7(r+t.M/20<o&&r+t.M/20>(n[s+1]||n[s]-t.M)&&t.34()==="3i"){7(t.6.12===j){i=n[s+1]||n[n.N-1];t.m=e.4p(i,t.J)}l{i=n[s+1];t.m=s+1}}});p t.m},34:9(){h e=c,t;7(e.Y<0){t="3i";e.3u="U"}l{t="T";e.3u="1n"}p t},4A:9(){h e=c;e.$k.w("d.U",9(){e.U()});e.$k.w("d.1n",9(){e.1n()});e.$k.w("d.19",9(t,n){e.6.O=n;e.19();e.32="19"});e.$k.w("d.X",9(){e.X();e.32="X"});e.$k.w("d.1g",9(t,n){e.1g(n)});e.$k.w("d.2g",9(t,n){e.2g(n)})},2p:9(){h e=c;7(e.6.2p===j&&e.F.15!==j&&e.6.O!==b){e.$k.w("57",9(){e.X()});e.$k.w("58",9(){7(e.32!=="X"){e.19()}})}},1Z:9(){h t=c,n,r,i,s,o;7(t.6.1Z===b){p b}1A(n=0;n<t.E;n+=1){r=e(t.$G[n]);7(r.v("d-1e")==="1e"){4s}i=r.v("d-1K");s=r.17(".5b");7(A s.v("1J")!=="2Y"){r.v("d-1e","1e");4s}7(r.v("d-1e")===W){s.3K();r.I("4u").v("d-1e","5e")}7(t.6.4v===j){o=i>=t.m}l{o=j}7(o&&i<t.m+t.6.q&&s.N){t.4w(r,s)}}},4w:9(e,n){9 o(){e.v("d-1e","1e").Z("4u");n.5h("v-1J");7(r.6.4x==="4y"){n.5j(5k)}l{n.3J()}7(A r.6.2T==="9"){r.6.2T.R(c,[r.$k])}}9 u(){i+=1;7(r.2Q(n.3l(0))||s===j){o()}l 7(i<=2q){t.1c(u,2q)}l{o()}}h r=c,i=0,s;7(n.5p("5q")==="5r"){n.z("5s-5t","5u("+n.v("1J")+")");s=j}l{n[0].1J=n.v("1J")}u()},1B:9(){9 s(){h r=e(n.$G[n.m]).2G();n.1H.z("2G",r+"V");7(!n.1H.1I("1B")){t.1c(9(){n.1H.I("1B")},0)}}9 o(){i+=1;7(n.2Q(r.3l(0))){s()}l 7(i<=2q){t.1c(o,2q)}l{n.1H.z("2G","")}}h n=c,r=e(n.$G[n.m]).17("5w"),i;7(r.3l(0)!==W){i=0;o()}l{s()}},2Q:9(e){h t;7(!e.3M){p b}t=A e.4D;7(t!=="W"&&e.4D===0){p b}p j},4g:9(){h t=c,n;7(t.6.2F===j){t.$G.Z("2d")}t.1D=[];1A(n=t.m;n<t.m+t.6.q;n+=1){t.1D.2D(n);7(t.6.2F===j){e(t.$G[n]).I("2d")}}t.d.1D=t.1D},4n:9(e){h t=c;t.4G="d-"+e+"-5B";t.4H="d-"+e+"-38"},4l:9(){9 a(e){p{2h:"5D",T:e+"V"}}h e=c,t=e.4G,n=e.4H,r=e.$G.1S(e.m),i=e.$G.1S(e.13),s=1F.4J(e.J[e.m])+e.J[e.13],o=1F.4J(e.J[e.m])+e.M/2,u="5G 5H 5I 5J";e.1E=j;e.$K.I("d-1P").z({"-1G-P-1P":o+"V","-1W-4K-1P":o+"V","4K-1P":o+"V"});i.z(a(s,10)).I(t).w(u,9(){e.3m=j;i.Q(u);e.31(i,t)});r.I(n).w(u,9(){e.36=j;r.Q(u);e.31(r,n)})},31:9(e,t){h n=c;e.z({2h:"",T:""}).Z(t);7(n.3m&&n.36){n.$K.Z("d-1P");n.3m=b;n.36=b;n.1E=b}},4o:9(){h e=c;e.d={2A:e.2A,5P:e.$k,S:e.$S,G:e.$G,m:e.m,13:e.13,1D:e.1D,15:e.F.15,F:e.F,1y:e.1y}},3G:9(){h r=c;r.$k.Q(".d d 21.3X");e(n).Q(".d d");e(t).Q("44",r.3d)},1V:9(){h e=c;7(e.$k.25().N!==0){e.$K.3r();e.$S.3r().3r();7(e.B){e.B.3k()}}e.3G();e.$k.2x("2w",e.$k.v("d-4I")||"").2x("H",e.$k.v("d-4F"))},5T:9(){h e=c;e.X();t.18(e.1X);e.1V();e.$k.5U()},5V:9(t){h n=c,r=e.4M({},n.2A,t);n.1V();n.1N(r,n.$k)},5W:9(e,t){h n=c,r;7(!e){p b}7(n.$k.25().N===0){n.$k.1o(e);n.23();p b}n.1V();7(t===W||t===-1){r=-1}l{r=t}7(r>=n.$S.N||r===-1){n.$S.1S(-1).5X(e)}l{n.$S.1S(r).5Y(e)}n.23()},5Z:9(e){h t=c,n;7(t.$k.25().N===0){p b}7(e===W||e===-1){n=-1}l{n=e}t.1V();t.$S.1S(n).3k();t.23()}};e.37.2B=9(t){p c.2f(9(){7(e(c).v("d-1N")===j){p b}e(c).v("d-1N",j);h n=3c.3q(r);n.1N(t,c);e.v(c,"2B",n)})};e.37.2B.6={q:5,1h:b,1s:[60,4],1O:[61,3],22:[62,2],1Q:b,1R:[63,1],48:b,46:b,1m:2M,1w:64,2v:65,O:b,2p:b,2a:b,2U:["1n","U"],2e:j,12:b,1v:j,39:b,2Z:j,45:2M,47:t,1M:"d-66",2i:"d-2i",1Z:b,4v:j,4x:"4y",1B:b,2O:b,33:b,3f:j,27:j,1U:j,2F:b,2o:b,3B:b,3D:b,2H:b,3s:b,1Y:b,3y:b,3w:b,2E:b,2T:b}})(67,68,69)', 62, 382, '||||||options|if||function||false|this|owl||||var||true|elem|else|currentItem|||return|items|||||data|on|||css|typeof|owlControls|0px|maximumItem|itemsAmount|browser|owlItems|class|addClass|positionsInArray|owlWrapper|div|itemWidth|length|autoPlay|transform|off|apply|userItems|left|next|px|undefined|stop|newRelativeX|removeClass||newPosX|scrollPerPage|prevItem|null|isTouch|ev_types|find|clearInterval|play|transition|disabled|setTimeout|target|loaded|width|goTo|itemsCustom|translate3d|page|paginationWrapper|preventDefault|slideSpeed|prev|append|wrapper|buttonNext|css2slide|itemsDesktop|swapSpeed|buttonPrev|pagination|paginationSpeed|support3d|dragDirection|ms|for|autoHeight|autoPlayInterval|visibleItems|isTransition|Math|webkit|wrapperOuter|hasClass|src|item|transition3d|baseClass|init|itemsDesktopSmall|origin|itemsTabletSmall|itemsMobile|eq|isCss3Finish|touchDrag|unWrap|moz|checkVisible|beforeMove|lazyLoad||mousedown|itemsTablet|setVars|roundPages|children|prevArr|mouseDrag|mouseup|isCssFinish|navigation|touches|pageX|active|rewindNav|each|jumpTo|position|theme|sliding|rewind|eachMoveUpdate|is|touchend|transitionStyle|stopOnHover|100|afterGo|ease|orignalItems|opacity|rewindSpeed|style|attr|html|addCssSpeed|userOptions|owlCarousel|all|push|startDragging|addClassActive|height|beforeInit|newPosY|end|move|targetElement|200|touchmove|jsonPath|offsetY|completeImg|offsetX|relativePos|afterLazyLoad|navigationText|updateItems|calculateAll|touchstart|string|responsive|updateControls|clearTransStyle|hoverStatus|jsonSuccess|moveDirection|checkPagination|endCurrent|fn|in|paginationNumbers|click|grabbing|Object|resizer|checkNavigation|dragBeforeAnimFinish|event|originalEvent|right|checkAp|remove|get|endPrev|visible|watchVisibility|Number|create|unwrap|afterInit|logIn|playDirection|max|afterAction|updateVars|afterMove|maximumPixels|apStatus|beforeUpdate|dragging|afterUpdate|pagesInArray|reload|clearEvents|removeTransition|doTranslate|show|hide|css2move|complete|span|cssText|updatePagination|gestures|disabledEvents|buildButtons|buildPagination|mousemove|touchcancel|start|disableTextSelect|min|loops|calculateWidth|pageY|appendWrapperSizes|appendItemsSizes|resize|responsiveRefreshRate|itemsScaleUp|responsiveBaseWidth|singleItem|outer|wrap|animate|srcElement|setInterval|drag|updatePosition|onVisibleItems|block|display|getNewPosition|disable|singleItemTransition|closestItem|transitionTypes|owlStatus|inArray|moveEvents|response|continue|buildControls|loading|lazyFollow|lazyPreload|lazyEffect|fade|onStartup|customEvents|wrapItems|eventTypes|naturalWidth|checkBrowser|originalClasses|outClass|inClass|originalStyles|abs|perspective|loadContent|extend|_data|round|msMaxTouchPoints|5e3|text|stopImmediatePropagation|stopPropagation|buttons|events|pop|splice|baseElWidth|minSwipe|maxSwipe|dargging|clientX|clientY|duration|destroyControls|createElement|mouseover|mouseout|numbers|which|lazyOwl|appendTo|clearTimeout|checked|shift|sort|removeAttr|match|fadeIn|400|clickable|toggleClass|wrapAll|top|prop|tagName|DIV|background|image|url|wrapperWidth|img|500|dragstart|ontouchstart|controls|out|input|relative|textarea|select|webkitAnimationEnd|oAnimationEnd|MSAnimationEnd|animationend|getJSON|returnValue|hasOwnProperty|option|onstartup|baseElement|navigator|new|prototype|destroy|removeData|reinit|addItem|after|before|removeItem|1199|979|768|479|800|1e3|carousel|jQuery|window|document'.split('|'), 0, {}));
/*!
jQuery Waypoints - v2.0.5
Copyright (c) 2011-2014 Caleb Troughton
Licensed under the MIT license.
https://github.com/imakewebthings/jquery-waypoints/blob/master/licenses.txt
*/
(function () {
	var t = [].indexOf || function (t) {
			for (var e = 0, n = this.length; e < n; e++) {
				if (e in this && this[e] === t) return e
			}
			return -1
		},
		e = [].slice;
	(function (t, e) {
		if (typeof define === "function" && define.amd) {
			return define("waypoints", ["jquery"], function (n) {
				return e(n, t)
			})
		} else {
			return e(t.jQuery, t)
		}
	})(window, function (n, r) {
		var i, o, l, s, f, u, c, a, h, d, p, y, v, w, g, m;
		i = n(r);
		a = t.call(r, "ontouchstart") >= 0;
		s = {
			horizontal: {},
			vertical: {}
		};
		f = 1;
		c = {};
		u = "waypoints-context-id";
		p = "resize.waypoints";
		y = "scroll.waypoints";
		v = 1;
		w = "waypoints-waypoint-ids";
		g = "waypoint";
		m = "waypoints";
		o = function () {
			function t(t) {
				var e = this;
				this.$element = t;
				this.element = t[0];
				this.didResize = false;
				this.didScroll = false;
				this.id = "context" + f++;
				this.oldScroll = {
					x: t.scrollLeft(),
					y: t.scrollTop()
				};
				this.waypoints = {
					horizontal: {},
					vertical: {}
				};
				this.element[u] = this.id;
				c[this.id] = this;
				t.bind(y, function () {
					var t;
					if (!(e.didScroll || a)) {
						e.didScroll = true;
						t = function () {
							e.doScroll();
							return e.didScroll = false
						};
						return r.setTimeout(t, n[m].settings.scrollThrottle)
					}
				});
				t.bind(p, function () {
					var t;
					if (!e.didResize) {
						e.didResize = true;
						t = function () {
							n[m]("refresh");
							return e.didResize = false
						};
						return r.setTimeout(t, n[m].settings.resizeThrottle)
					}
				})
			}
			t.prototype.doScroll = function () {
				var t, e = this;
				t = {
					horizontal: {
						newScroll: this.$element.scrollLeft(),
						oldScroll: this.oldScroll.x,
						forward: "right",
						backward: "left"
					},
					vertical: {
						newScroll: this.$element.scrollTop(),
						oldScroll: this.oldScroll.y,
						forward: "down",
						backward: "up"
					}
				};
				if (a && (!t.vertical.oldScroll || !t.vertical.newScroll)) {
					n[m]("refresh")
				}
				n.each(t, function (t, r) {
					var i, o, l;
					l = [];
					o = r.newScroll > r.oldScroll;
					i = o ? r.forward : r.backward;
					n.each(e.waypoints[t], function (t, e) {
						var n, i;
						if (r.oldScroll < (n = e.offset) && n <= r.newScroll) {
							return l.push(e)
						} else if (r.newScroll < (i = e.offset) && i <= r.oldScroll) {
							return l.push(e)
						}
					});
					l.sort(function (t, e) {
						return t.offset - e.offset
					});
					if (!o) {
						l.reverse()
					}
					return n.each(l, function (t, e) {
						if (e.options.continuous || t === l.length - 1) {
							return e.trigger([i])
						}
					})
				});
				return this.oldScroll = {
					x: t.horizontal.newScroll,
					y: t.vertical.newScroll
				}
			};
			t.prototype.refresh = function () {
				var t, e, r, i = this;
				r = n.isWindow(this.element);
				e = this.$element.offset();
				this.doScroll();
				t = {
					horizontal: {
						contextOffset: r ? 0 : e.left,
						contextScroll: r ? 0 : this.oldScroll.x,
						contextDimension: this.$element.width(),
						oldScroll: this.oldScroll.x,
						forward: "right",
						backward: "left",
						offsetProp: "left"
					},
					vertical: {
						contextOffset: r ? 0 : e.top,
						contextScroll: r ? 0 : this.oldScroll.y,
						contextDimension: r ? n[m]("viewportHeight") : this.$element.height(),
						oldScroll: this.oldScroll.y,
						forward: "down",
						backward: "up",
						offsetProp: "top"
					}
				};
				return n.each(t, function (t, e) {
					return n.each(i.waypoints[t], function (t, r) {
						var i, o, l, s, f;
						i = r.options.offset;
						l = r.offset;
						o = n.isWindow(r.element) ? 0 : r.$element.offset()[e.offsetProp];
						if (n.isFunction(i)) {
							i = i.apply(r.element)
						} else if (typeof i === "string") {
							i = parseFloat(i);
							if (r.options.offset.indexOf("%") > -1) {
								i = Math.ceil(e.contextDimension * i / 100)
							}
						}
						r.offset = o - e.contextOffset + e.contextScroll - i;
						if (r.options.onlyOnScroll && l != null || !r.enabled) {
							return
						}
						if (l !== null && l < (s = e.oldScroll) && s <= r.offset) {
							return r.trigger([e.backward])
						} else if (l !== null && l > (f = e.oldScroll) && f >= r.offset) {
							return r.trigger([e.forward])
						} else if (l === null && e.oldScroll >= r.offset) {
							return r.trigger([e.forward])
						}
					})
				})
			};
			t.prototype.checkEmpty = function () {
				if (n.isEmptyObject(this.waypoints.horizontal) && n.isEmptyObject(this.waypoints.vertical)) {
					this.$element.unbind([p, y].join(" "));
					return delete c[this.id]
				}
			};
			return t
		}();
		l = function () {
			function t(t, e, r) {
				var i, o;
				if (r.offset === "bottom-in-view") {
					r.offset = function () {
						var t;
						t = n[m]("viewportHeight");
						if (!n.isWindow(e.element)) {
							t = e.$element.height()
						}
						return t - n(this).outerHeight()
					}
				}
				this.$element = t;
				this.element = t[0];
				this.axis = r.horizontal ? "horizontal" : "vertical";
				this.callback = r.handler;
				this.context = e;
				this.enabled = r.enabled;
				this.id = "waypoints" + v++;
				this.offset = null;
				this.options = r;
				e.waypoints[this.axis][this.id] = this;
				s[this.axis][this.id] = this;
				i = (o = this.element[w]) != null ? o : [];
				i.push(this.id);
				this.element[w] = i
			}
			t.prototype.trigger = function (t) {
				if (!this.enabled) {
					return
				}
				if (this.callback != null) {
					this.callback.apply(this.element, t)
				}
				if (this.options.triggerOnce) {
					return this.destroy()
				}
			};
			t.prototype.disable = function () {
				return this.enabled = false
			};
			t.prototype.enable = function () {
				this.context.refresh();
				return this.enabled = true
			};
			t.prototype.destroy = function () {
				delete s[this.axis][this.id];
				delete this.context.waypoints[this.axis][this.id];
				return this.context.checkEmpty()
			};
			t.getWaypointsByElement = function (t) {
				var e, r;
				r = t[w];
				if (!r) {
					return []
				}
				e = n.extend({}, s.horizontal, s.vertical);
				return n.map(r, function (t) {
					return e[t]
				})
			};
			return t
		}();
		d = {
			init: function (t, e) {
				var r;
				e = n.extend({}, n.fn[g].defaults, e);
				if ((r = e.handler) == null) {
					e.handler = t
				}
				this.each(function () {
					var t, r, i, s;
					t = n(this);
					i = (s = e.context) != null ? s : n.fn[g].defaults.context;
					if (!n.isWindow(i)) {
						i = t.closest(i)
					}
					i = n(i);
					r = c[i[0][u]];
					if (!r) {
						r = new o(i)
					}
					return new l(t, r, e)
				});
				n[m]("refresh");
				return this
			},
			disable: function () {
				return d._invoke.call(this, "disable")
			},
			enable: function () {
				return d._invoke.call(this, "enable")
			},
			destroy: function () {
				return d._invoke.call(this, "destroy")
			},
			prev: function (t, e) {
				return d._traverse.call(this, t, e, function (t, e, n) {
					if (e > 0) {
						return t.push(n[e - 1])
					}
				})
			},
			next: function (t, e) {
				return d._traverse.call(this, t, e, function (t, e, n) {
					if (e < n.length - 1) {
						return t.push(n[e + 1])
					}
				})
			},
			_traverse: function (t, e, i) {
				var o, l;
				if (t == null) {
					t = "vertical"
				}
				if (e == null) {
					e = r
				}
				l = h.aggregate(e);
				o = [];
				this.each(function () {
					var e;
					e = n.inArray(this, l[t]);
					return i(o, e, l[t])
				});
				return this.pushStack(o)
			},
			_invoke: function (t) {
				this.each(function () {
					var e;
					e = l.getWaypointsByElement(this);
					return n.each(e, function (e, n) {
						n[t]();
						return true
					})
				});
				return this
			}
		};
		n.fn[g] = function () {
			var t, r;
			r = arguments[0], t = 2 <= arguments.length ? e.call(arguments, 1) : [];
			if (d[r]) {
				return d[r].apply(this, t)
			} else if (n.isFunction(r)) {
				return d.init.apply(this, arguments)
			} else if (n.isPlainObject(r)) {
				return d.init.apply(this, [null, r])
			} else if (!r) {
				return n.error("jQuery Waypoints needs a callback function or handler option.")
			} else {
				return n.error("The " + r + " method does not exist in jQuery Waypoints.")
			}
		};
		n.fn[g].defaults = {
			context: r,
			continuous: true,
			enabled: true,
			horizontal: false,
			offset: 0,
			triggerOnce: false
		};
		h = {
			refresh: function () {
				return n.each(c, function (t, e) {
					return e.refresh()
				})
			},
			viewportHeight: function () {
				var t;
				return (t = r.innerHeight) != null ? t : i.height()
			},
			aggregate: function (t) {
				var e, r, i;
				e = s;
				if (t) {
					e = (i = c[n(t)[0][u]]) != null ? i.waypoints : void 0
				}
				if (!e) {
					return []
				}
				r = {
					horizontal: [],
					vertical: []
				};
				n.each(r, function (t, i) {
					n.each(e[t], function (t, e) {
						return i.push(e)
					});
					i.sort(function (t, e) {
						return t.offset - e.offset
					});
					r[t] = n.map(i, function (t) {
						return t.element
					});
					return r[t] = n.unique(r[t])
				});
				return r
			},
			above: function (t) {
				if (t == null) {
					t = r
				}
				return h._filter(t, "vertical", function (t, e) {
					return e.offset <= t.oldScroll.y
				})
			},
			below: function (t) {
				if (t == null) {
					t = r
				}
				return h._filter(t, "vertical", function (t, e) {
					return e.offset > t.oldScroll.y
				})
			},
			left: function (t) {
				if (t == null) {
					t = r
				}
				return h._filter(t, "horizontal", function (t, e) {
					return e.offset <= t.oldScroll.x
				})
			},
			right: function (t) {
				if (t == null) {
					t = r
				}
				return h._filter(t, "horizontal", function (t, e) {
					return e.offset > t.oldScroll.x
				})
			},
			enable: function () {
				return h._invoke("enable")
			},
			disable: function () {
				return h._invoke("disable")
			},
			destroy: function () {
				return h._invoke("destroy")
			},
			extendFn: function (t, e) {
				return d[t] = e
			},
			_invoke: function (t) {
				var e;
				e = n.extend({}, s.vertical, s.horizontal);
				return n.each(e, function (e, n) {
					n[t]();
					return true
				})
			},
			_filter: function (t, e, r) {
				var i, o;
				i = c[n(t)[0][u]];
				if (!i) {
					return []
				}
				o = [];
				n.each(i.waypoints[e], function (t, e) {
					if (r(i, e)) {
						return o.push(e)
					}
				});
				o.sort(function (t, e) {
					return t.offset - e.offset
				});
				return n.map(o, function (t) {
					return t.element
				})
			}
		};
		n[m] = function () {
			var t, n;
			n = arguments[0], t = 2 <= arguments.length ? e.call(arguments, 1) : [];
			if (h[n]) {
				return h[n].apply(null, t)
			} else {
				return h.aggregate.call(null, n)
			}
		};
		n[m].settings = {
			resizeThrottle: 100,
			scrollThrottle: 30
		};
		return i.on("load.waypoints", function () {
			return n[m]("refresh")
		})
	})
}).call(this);
! function (e, t) {
	"object" == typeof exports && "undefined" != typeof module ? module.exports = t() : "function" == typeof define && define.amd ? define(t) : e.moment = t()
}(this, function () {
	"use strict";
	var e, i;

	function c() {
		return e.apply(null, arguments)
	}

	function o(e) {
		return e instanceof Array || "[object Array]" === Object.prototype.toString.call(e)
	}

	function u(e) {
		return null != e && "[object Object]" === Object.prototype.toString.call(e)
	}

	function l(e) {
		return void 0 === e
	}

	function d(e) {
		return "number" == typeof e || "[object Number]" === Object.prototype.toString.call(e)
	}

	function h(e) {
		return e instanceof Date || "[object Date]" === Object.prototype.toString.call(e)
	}

	function f(e, t) {
		var n, s = [];
		for (n = 0; n < e.length; ++n) s.push(t(e[n], n));
		return s
	}

	function m(e, t) {
		return Object.prototype.hasOwnProperty.call(e, t)
	}

	function _(e, t) {
		for (var n in t) m(t, n) && (e[n] = t[n]);
		return m(t, "toString") && (e.toString = t.toString), m(t, "valueOf") && (e.valueOf = t.valueOf), e
	}

	function y(e, t, n, s) {
		return Ot(e, t, n, s, !0).utc()
	}

	function g(e) {
		return null == e._pf && (e._pf = {
			empty: !1,
			unusedTokens: [],
			unusedInput: [],
			overflow: -2,
			charsLeftOver: 0,
			nullInput: !1,
			invalidMonth: null,
			invalidFormat: !1,
			userInvalidated: !1,
			iso: !1,
			parsedDateParts: [],
			meridiem: null,
			rfc2822: !1,
			weekdayMismatch: !1
		}), e._pf
	}

	function p(e) {
		if (null == e._isValid) {
			var t = g(e),
				n = i.call(t.parsedDateParts, function (e) {
					return null != e
				}),
				s = !isNaN(e._d.getTime()) && t.overflow < 0 && !t.empty && !t.invalidMonth && !t.invalidWeekday && !t.weekdayMismatch && !t.nullInput && !t.invalidFormat && !t.userInvalidated && (!t.meridiem || t.meridiem && n);
			if (e._strict && (s = s && 0 === t.charsLeftOver && 0 === t.unusedTokens.length && void 0 === t.bigHour), null != Object.isFrozen && Object.isFrozen(e)) return s;
			e._isValid = s
		}
		return e._isValid
	}

	function v(e) {
		var t = y(NaN);
		return null != e ? _(g(t), e) : g(t).userInvalidated = !0, t
	}
	i = Array.prototype.some ? Array.prototype.some : function (e) {
		for (var t = Object(this), n = t.length >>> 0, s = 0; s < n; s++)
			if (s in t && e.call(this, t[s], s, t)) return !0;
		return !1
	};
	var r = c.momentProperties = [];

	function w(e, t) {
		var n, s, i;
		if (l(t._isAMomentObject) || (e._isAMomentObject = t._isAMomentObject), l(t._i) || (e._i = t._i), l(t._f) || (e._f = t._f), l(t._l) || (e._l = t._l), l(t._strict) || (e._strict = t._strict), l(t._tzm) || (e._tzm = t._tzm), l(t._isUTC) || (e._isUTC = t._isUTC), l(t._offset) || (e._offset = t._offset), l(t._pf) || (e._pf = g(t)), l(t._locale) || (e._locale = t._locale), 0 < r.length)
			for (n = 0; n < r.length; n++) l(i = t[s = r[n]]) || (e[s] = i);
		return e
	}
	var t = !1;

	function M(e) {
		w(this, e), this._d = new Date(null != e._d ? e._d.getTime() : NaN), this.isValid() || (this._d = new Date(NaN)), !1 === t && (t = !0, c.updateOffset(this), t = !1)
	}

	function S(e) {
		return e instanceof M || null != e && null != e._isAMomentObject
	}

	function D(e) {
		return e < 0 ? Math.ceil(e) || 0 : Math.floor(e)
	}

	function k(e) {
		var t = +e,
			n = 0;
		return 0 !== t && isFinite(t) && (n = D(t)), n
	}

	function a(e, t, n) {
		var s, i = Math.min(e.length, t.length),
			r = Math.abs(e.length - t.length),
			a = 0;
		for (s = 0; s < i; s++)(n && e[s] !== t[s] || !n && k(e[s]) !== k(t[s])) && a++;
		return a + r
	}

	function Y(e) {
		!1 === c.suppressDeprecationWarnings && "undefined" != typeof console && console.warn && console.warn("Deprecation warning: " + e)
	}

	function n(i, r) {
		var a = !0;
		return _(function () {
			if (null != c.deprecationHandler && c.deprecationHandler(null, i), a) {
				for (var e, t = [], n = 0; n < arguments.length; n++) {
					if (e = "", "object" == typeof arguments[n]) {
						for (var s in e += "\n[" + n + "] ", arguments[0]) e += s + ": " + arguments[0][s] + ", ";
						e = e.slice(0, -2)
					} else e = arguments[n];
					t.push(e)
				}
				Y(i + "\nArguments: " + Array.prototype.slice.call(t).join("") + "\n" + (new Error).stack), a = !1
			}
			return r.apply(this, arguments)
		}, r)
	}
	var s, O = {};

	function T(e, t) {
		null != c.deprecationHandler && c.deprecationHandler(e, t), O[e] || (Y(t), O[e] = !0)
	}

	function x(e) {
		return e instanceof Function || "[object Function]" === Object.prototype.toString.call(e)
	}

	function b(e, t) {
		var n, s = _({}, e);
		for (n in t) m(t, n) && (u(e[n]) && u(t[n]) ? (s[n] = {}, _(s[n], e[n]), _(s[n], t[n])) : null != t[n] ? s[n] = t[n] : delete s[n]);
		for (n in e) m(e, n) && !m(t, n) && u(e[n]) && (s[n] = _({}, s[n]));
		return s
	}

	function P(e) {
		null != e && this.set(e)
	}
	c.suppressDeprecationWarnings = !1, c.deprecationHandler = null, s = Object.keys ? Object.keys : function (e) {
		var t, n = [];
		for (t in e) m(e, t) && n.push(t);
		return n
	};
	var W = {};

	function H(e, t) {
		var n = e.toLowerCase();
		W[n] = W[n + "s"] = W[t] = e
	}

	function R(e) {
		return "string" == typeof e ? W[e] || W[e.toLowerCase()] : void 0
	}

	function C(e) {
		var t, n, s = {};
		for (n in e) m(e, n) && (t = R(n)) && (s[t] = e[n]);
		return s
	}
	var F = {};

	function L(e, t) {
		F[e] = t
	}

	function U(e, t, n) {
		var s = "" + Math.abs(e),
			i = t - s.length;
		return (0 <= e ? n ? "+" : "" : "-") + Math.pow(10, Math.max(0, i)).toString().substr(1) + s
	}
	var N = /(\[[^\[]*\])|(\\)?([Hh]mm(ss)?|Mo|MM?M?M?|Do|DDDo|DD?D?D?|ddd?d?|do?|w[o|w]?|W[o|W]?|Qo?|YYYYYY|YYYYY|YYYY|YY|gg(ggg?)?|GG(GGG?)?|e|E|a|A|hh?|HH?|kk?|mm?|ss?|S{1,9}|x|X|zz?|ZZ?|.)/g,
		G = /(\[[^\[]*\])|(\\)?(LTS|LT|LL?L?L?|l{1,4})/g,
		V = {},
		E = {};

	function I(e, t, n, s) {
		var i = s;
		"string" == typeof s && (i = function () {
			return this[s]()
		}), e && (E[e] = i), t && (E[t[0]] = function () {
			return U(i.apply(this, arguments), t[1], t[2])
		}), n && (E[n] = function () {
			return this.localeData().ordinal(i.apply(this, arguments), e)
		})
	}

	function A(e, t) {
		return e.isValid() ? (t = j(t, e.localeData()), V[t] = V[t] || function (s) {
			var e, i, t, r = s.match(N);
			for (e = 0, i = r.length; e < i; e++) E[r[e]] ? r[e] = E[r[e]] : r[e] = (t = r[e]).match(/\[[\s\S]/) ? t.replace(/^\[|\]$/g, "") : t.replace(/\\/g, "");
			return function (e) {
				var t, n = "";
				for (t = 0; t < i; t++) n += x(r[t]) ? r[t].call(e, s) : r[t];
				return n
			}
		}(t), V[t](e)) : e.localeData().invalidDate()
	}

	function j(e, t) {
		var n = 5;

		function s(e) {
			return t.longDateFormat(e) || e
		}
		for (G.lastIndex = 0; 0 <= n && G.test(e);) e = e.replace(G, s), G.lastIndex = 0, n -= 1;
		return e
	}
	var Z = /\d/,
		z = /\d\d/,
		$ = /\d{3}/,
		q = /\d{4}/,
		J = /[+-]?\d{6}/,
		B = /\d\d?/,
		Q = /\d\d\d\d?/,
		X = /\d\d\d\d\d\d?/,
		K = /\d{1,3}/,
		ee = /\d{1,4}/,
		te = /[+-]?\d{1,6}/,
		ne = /\d+/,
		se = /[+-]?\d+/,
		ie = /Z|[+-]\d\d:?\d\d/gi,
		re = /Z|[+-]\d\d(?::?\d\d)?/gi,
		ae = /[0-9]{0,256}['a-z\u00A0-\u05FF\u0700-\uD7FF\uF900-\uFDCF\uFDF0-\uFF07\uFF10-\uFFEF]{1,256}|[\u0600-\u06FF\/]{1,256}(\s*?[\u0600-\u06FF]{1,256}){1,2}/i,
		oe = {};

	function ue(e, n, s) {
		oe[e] = x(n) ? n : function (e, t) {
			return e && s ? s : n
		}
	}

	function le(e, t) {
		return m(oe, e) ? oe[e](t._strict, t._locale) : new RegExp(de(e.replace("\\", "").replace(/\\(\[)|\\(\])|\[([^\]\[]*)\]|\\(.)/g, function (e, t, n, s, i) {
			return t || n || s || i
		})))
	}

	function de(e) {
		return e.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&")
	}
	var he = {};

	function ce(e, n) {
		var t, s = n;
		for ("string" == typeof e && (e = [e]), d(n) && (s = function (e, t) {
				t[n] = k(e)
			}), t = 0; t < e.length; t++) he[e[t]] = s
	}

	function fe(e, i) {
		ce(e, function (e, t, n, s) {
			n._w = n._w || {}, i(e, n._w, n, s)
		})
	}
	var me = 0,
		_e = 1,
		ye = 2,
		ge = 3,
		pe = 4,
		ve = 5,
		we = 6,
		Me = 7,
		Se = 8;

	function De(e) {
		return ke(e) ? 366 : 365
	}

	function ke(e) {
		return e % 4 == 0 && e % 100 != 0 || e % 400 == 0
	}
	I("Y", 0, 0, function () {
		var e = this.year();
		return e <= 9999 ? "" + e : "+" + e
	}), I(0, ["YY", 2], 0, function () {
		return this.year() % 100
	}), I(0, ["YYYY", 4], 0, "year"), I(0, ["YYYYY", 5], 0, "year"), I(0, ["YYYYYY", 6, !0], 0, "year"), H("year", "y"), L("year", 1), ue("Y", se), ue("YY", B, z), ue("YYYY", ee, q), ue("YYYYY", te, J), ue("YYYYYY", te, J), ce(["YYYYY", "YYYYYY"], me), ce("YYYY", function (e, t) {
		t[me] = 2 === e.length ? c.parseTwoDigitYear(e) : k(e)
	}), ce("YY", function (e, t) {
		t[me] = c.parseTwoDigitYear(e)
	}), ce("Y", function (e, t) {
		t[me] = parseInt(e, 10)
	}), c.parseTwoDigitYear = function (e) {
		return k(e) + (68 < k(e) ? 1900 : 2e3)
	};
	var Ye, Oe = Te("FullYear", !0);

	function Te(t, n) {
		return function (e) {
			return null != e ? (be(this, t, e), c.updateOffset(this, n), this) : xe(this, t)
		}
	}

	function xe(e, t) {
		return e.isValid() ? e._d["get" + (e._isUTC ? "UTC" : "") + t]() : NaN
	}

	function be(e, t, n) {
		e.isValid() && !isNaN(n) && ("FullYear" === t && ke(e.year()) && 1 === e.month() && 29 === e.date() ? e._d["set" + (e._isUTC ? "UTC" : "") + t](n, e.month(), Pe(n, e.month())) : e._d["set" + (e._isUTC ? "UTC" : "") + t](n))
	}

	function Pe(e, t) {
		if (isNaN(e) || isNaN(t)) return NaN;
		var n, s = (t % (n = 12) + n) % n;
		return e += (t - s) / 12, 1 === s ? ke(e) ? 29 : 28 : 31 - s % 7 % 2
	}
	Ye = Array.prototype.indexOf ? Array.prototype.indexOf : function (e) {
		var t;
		for (t = 0; t < this.length; ++t)
			if (this[t] === e) return t;
		return -1
	}, I("M", ["MM", 2], "Mo", function () {
		return this.month() + 1
	}), I("MMM", 0, 0, function (e) {
		return this.localeData().monthsShort(this, e)
	}), I("MMMM", 0, 0, function (e) {
		return this.localeData().months(this, e)
	}), H("month", "M"), L("month", 8), ue("M", B), ue("MM", B, z), ue("MMM", function (e, t) {
		return t.monthsShortRegex(e)
	}), ue("MMMM", function (e, t) {
		return t.monthsRegex(e)
	}), ce(["M", "MM"], function (e, t) {
		t[_e] = k(e) - 1
	}), ce(["MMM", "MMMM"], function (e, t, n, s) {
		var i = n._locale.monthsParse(e, s, n._strict);
		null != i ? t[_e] = i : g(n).invalidMonth = e
	});
	var We = /D[oD]?(\[[^\[\]]*\]|\s)+MMMM?/,
		He = "January_February_March_April_May_June_July_August_September_October_November_December".split("_");
	var Re = "Jan_Feb_Mar_Apr_May_Jun_Jul_Aug_Sep_Oct_Nov_Dec".split("_");

	function Ce(e, t) {
		var n;
		if (!e.isValid()) return e;
		if ("string" == typeof t)
			if (/^\d+$/.test(t)) t = k(t);
			else if (!d(t = e.localeData().monthsParse(t))) return e;
		return n = Math.min(e.date(), Pe(e.year(), t)), e._d["set" + (e._isUTC ? "UTC" : "") + "Month"](t, n), e
	}

	function Fe(e) {
		return null != e ? (Ce(this, e), c.updateOffset(this, !0), this) : xe(this, "Month")
	}
	var Le = ae;
	var Ue = ae;

	function Ne() {
		function e(e, t) {
			return t.length - e.length
		}
		var t, n, s = [],
			i = [],
			r = [];
		for (t = 0; t < 12; t++) n = y([2e3, t]), s.push(this.monthsShort(n, "")), i.push(this.months(n, "")), r.push(this.months(n, "")), r.push(this.monthsShort(n, ""));
		for (s.sort(e), i.sort(e), r.sort(e), t = 0; t < 12; t++) s[t] = de(s[t]), i[t] = de(i[t]);
		for (t = 0; t < 24; t++) r[t] = de(r[t]);
		this._monthsRegex = new RegExp("^(" + r.join("|") + ")", "i"), this._monthsShortRegex = this._monthsRegex, this._monthsStrictRegex = new RegExp("^(" + i.join("|") + ")", "i"), this._monthsShortStrictRegex = new RegExp("^(" + s.join("|") + ")", "i")
	}

	function Ge(e) {
		var t = new Date(Date.UTC.apply(null, arguments));
		return e < 100 && 0 <= e && isFinite(t.getUTCFullYear()) && t.setUTCFullYear(e), t
	}

	function Ve(e, t, n) {
		var s = 7 + t - n;
		return -((7 + Ge(e, 0, s).getUTCDay() - t) % 7) + s - 1
	}

	function Ee(e, t, n, s, i) {
		var r, a, o = 1 + 7 * (t - 1) + (7 + n - s) % 7 + Ve(e, s, i);
		return o <= 0 ? a = De(r = e - 1) + o : o > De(e) ? (r = e + 1, a = o - De(e)) : (r = e, a = o), {
			year: r,
			dayOfYear: a
		}
	}

	function Ie(e, t, n) {
		var s, i, r = Ve(e.year(), t, n),
			a = Math.floor((e.dayOfYear() - r - 1) / 7) + 1;
		return a < 1 ? s = a + Ae(i = e.year() - 1, t, n) : a > Ae(e.year(), t, n) ? (s = a - Ae(e.year(), t, n), i = e.year() + 1) : (i = e.year(), s = a), {
			week: s,
			year: i
		}
	}

	function Ae(e, t, n) {
		var s = Ve(e, t, n),
			i = Ve(e + 1, t, n);
		return (De(e) - s + i) / 7
	}
	I("w", ["ww", 2], "wo", "week"), I("W", ["WW", 2], "Wo", "isoWeek"), H("week", "w"), H("isoWeek", "W"), L("week", 5), L("isoWeek", 5), ue("w", B), ue("ww", B, z), ue("W", B), ue("WW", B, z), fe(["w", "ww", "W", "WW"], function (e, t, n, s) {
		t[s.substr(0, 1)] = k(e)
	});
	I("d", 0, "do", "day"), I("dd", 0, 0, function (e) {
		return this.localeData().weekdaysMin(this, e)
	}), I("ddd", 0, 0, function (e) {
		return this.localeData().weekdaysShort(this, e)
	}), I("dddd", 0, 0, function (e) {
		return this.localeData().weekdays(this, e)
	}), I("e", 0, 0, "weekday"), I("E", 0, 0, "isoWeekday"), H("day", "d"), H("weekday", "e"), H("isoWeekday", "E"), L("day", 11), L("weekday", 11), L("isoWeekday", 11), ue("d", B), ue("e", B), ue("E", B), ue("dd", function (e, t) {
		return t.weekdaysMinRegex(e)
	}), ue("ddd", function (e, t) {
		return t.weekdaysShortRegex(e)
	}), ue("dddd", function (e, t) {
		return t.weekdaysRegex(e)
	}), fe(["dd", "ddd", "dddd"], function (e, t, n, s) {
		var i = n._locale.weekdaysParse(e, s, n._strict);
		null != i ? t.d = i : g(n).invalidWeekday = e
	}), fe(["d", "e", "E"], function (e, t, n, s) {
		t[s] = k(e)
	});
	var je = "Sunday_Monday_Tuesday_Wednesday_Thursday_Friday_Saturday".split("_");
	var Ze = "Sun_Mon_Tue_Wed_Thu_Fri_Sat".split("_");
	var ze = "Su_Mo_Tu_We_Th_Fr_Sa".split("_");
	var $e = ae;
	var qe = ae;
	var Je = ae;

	function Be() {
		function e(e, t) {
			return t.length - e.length
		}
		var t, n, s, i, r, a = [],
			o = [],
			u = [],
			l = [];
		for (t = 0; t < 7; t++) n = y([2e3, 1]).day(t), s = this.weekdaysMin(n, ""), i = this.weekdaysShort(n, ""), r = this.weekdays(n, ""), a.push(s), o.push(i), u.push(r), l.push(s), l.push(i), l.push(r);
		for (a.sort(e), o.sort(e), u.sort(e), l.sort(e), t = 0; t < 7; t++) o[t] = de(o[t]), u[t] = de(u[t]), l[t] = de(l[t]);
		this._weekdaysRegex = new RegExp("^(" + l.join("|") + ")", "i"), this._weekdaysShortRegex = this._weekdaysRegex, this._weekdaysMinRegex = this._weekdaysRegex, this._weekdaysStrictRegex = new RegExp("^(" + u.join("|") + ")", "i"), this._weekdaysShortStrictRegex = new RegExp("^(" + o.join("|") + ")", "i"), this._weekdaysMinStrictRegex = new RegExp("^(" + a.join("|") + ")", "i")
	}

	function Qe() {
		return this.hours() % 12 || 12
	}

	function Xe(e, t) {
		I(e, 0, 0, function () {
			return this.localeData().meridiem(this.hours(), this.minutes(), t)
		})
	}

	function Ke(e, t) {
		return t._meridiemParse
	}
	I("H", ["HH", 2], 0, "hour"), I("h", ["hh", 2], 0, Qe), I("k", ["kk", 2], 0, function () {
		return this.hours() || 24
	}), I("hmm", 0, 0, function () {
		return "" + Qe.apply(this) + U(this.minutes(), 2)
	}), I("hmmss", 0, 0, function () {
		return "" + Qe.apply(this) + U(this.minutes(), 2) + U(this.seconds(), 2)
	}), I("Hmm", 0, 0, function () {
		return "" + this.hours() + U(this.minutes(), 2)
	}), I("Hmmss", 0, 0, function () {
		return "" + this.hours() + U(this.minutes(), 2) + U(this.seconds(), 2)
	}), Xe("a", !0), Xe("A", !1), H("hour", "h"), L("hour", 13), ue("a", Ke), ue("A", Ke), ue("H", B), ue("h", B), ue("k", B), ue("HH", B, z), ue("hh", B, z), ue("kk", B, z), ue("hmm", Q), ue("hmmss", X), ue("Hmm", Q), ue("Hmmss", X), ce(["H", "HH"], ge), ce(["k", "kk"], function (e, t, n) {
		var s = k(e);
		t[ge] = 24 === s ? 0 : s
	}), ce(["a", "A"], function (e, t, n) {
		n._isPm = n._locale.isPM(e), n._meridiem = e
	}), ce(["h", "hh"], function (e, t, n) {
		t[ge] = k(e), g(n).bigHour = !0
	}), ce("hmm", function (e, t, n) {
		var s = e.length - 2;
		t[ge] = k(e.substr(0, s)), t[pe] = k(e.substr(s)), g(n).bigHour = !0
	}), ce("hmmss", function (e, t, n) {
		var s = e.length - 4,
			i = e.length - 2;
		t[ge] = k(e.substr(0, s)), t[pe] = k(e.substr(s, 2)), t[ve] = k(e.substr(i)), g(n).bigHour = !0
	}), ce("Hmm", function (e, t, n) {
		var s = e.length - 2;
		t[ge] = k(e.substr(0, s)), t[pe] = k(e.substr(s))
	}), ce("Hmmss", function (e, t, n) {
		var s = e.length - 4,
			i = e.length - 2;
		t[ge] = k(e.substr(0, s)), t[pe] = k(e.substr(s, 2)), t[ve] = k(e.substr(i))
	});
	var et, tt = Te("Hours", !0),
		nt = {
			calendar: {
				sameDay: "[Today at] LT",
				nextDay: "[Tomorrow at] LT",
				nextWeek: "dddd [at] LT",
				lastDay: "[Yesterday at] LT",
				lastWeek: "[Last] dddd [at] LT",
				sameElse: "L"
			},
			longDateFormat: {
				LTS: "h:mm:ss A",
				LT: "h:mm A",
				L: "MM/DD/YYYY",
				LL: "MMMM D, YYYY",
				LLL: "MMMM D, YYYY h:mm A",
				LLLL: "dddd, MMMM D, YYYY h:mm A"
			},
			invalidDate: "Invalid date",
			ordinal: "%d",
			dayOfMonthOrdinalParse: /\d{1,2}/,
			relativeTime: {
				future: "in %s",
				past: "%s ago",
				s: "a few seconds",
				ss: "%d seconds",
				m: "a minute",
				mm: "%d minutes",
				h: "an hour",
				hh: "%d hours",
				d: "a day",
				dd: "%d days",
				M: "a month",
				MM: "%d months",
				y: "a year",
				yy: "%d years"
			},
			months: He,
			monthsShort: Re,
			week: {
				dow: 0,
				doy: 6
			},
			weekdays: je,
			weekdaysMin: ze,
			weekdaysShort: Ze,
			meridiemParse: /[ap]\.?m?\.?/i
		},
		st = {},
		it = {};

	function rt(e) {
		return e ? e.toLowerCase().replace("_", "-") : e
	}

	function at(e) {
		var t = null;
		if (!st[e] && "undefined" != typeof module && module && module.exports) try {
			t = et._abbr, require("./locale/" + e), ot(t)
		} catch (e) {}
		return st[e]
	}

	function ot(e, t) {
		var n;
		return e && ((n = l(t) ? lt(e) : ut(e, t)) ? et = n : "undefined" != typeof console && console.warn && console.warn("Locale " + e + " not found. Did you forget to load it?")), et._abbr
	}

	function ut(e, t) {
		if (null !== t) {
			var n, s = nt;
			if (t.abbr = e, null != st[e]) T("defineLocaleOverride", "use moment.updateLocale(localeName, config) to change an existing locale. moment.defineLocale(localeName, config) should only be used for creating a new locale See http://momentjs.com/guides/#/warnings/define-locale/ for more info."), s = st[e]._config;
			else if (null != t.parentLocale)
				if (null != st[t.parentLocale]) s = st[t.parentLocale]._config;
				else {
					if (null == (n = at(t.parentLocale))) return it[t.parentLocale] || (it[t.parentLocale] = []), it[t.parentLocale].push({
						name: e,
						config: t
					}), null;
					s = n._config
				}
			return st[e] = new P(b(s, t)), it[e] && it[e].forEach(function (e) {
				ut(e.name, e.config)
			}), ot(e), st[e]
		}
		return delete st[e], null
	}

	function lt(e) {
		var t;
		if (e && e._locale && e._locale._abbr && (e = e._locale._abbr), !e) return et;
		if (!o(e)) {
			if (t = at(e)) return t;
			e = [e]
		}
		return function (e) {
			for (var t, n, s, i, r = 0; r < e.length;) {
				for (t = (i = rt(e[r]).split("-")).length, n = (n = rt(e[r + 1])) ? n.split("-") : null; 0 < t;) {
					if (s = at(i.slice(0, t).join("-"))) return s;
					if (n && n.length >= t && a(i, n, !0) >= t - 1) break;
					t--
				}
				r++
			}
			return et
		}(e)
	}

	function dt(e) {
		var t, n = e._a;
		return n && -2 === g(e).overflow && (t = n[_e] < 0 || 11 < n[_e] ? _e : n[ye] < 1 || n[ye] > Pe(n[me], n[_e]) ? ye : n[ge] < 0 || 24 < n[ge] || 24 === n[ge] && (0 !== n[pe] || 0 !== n[ve] || 0 !== n[we]) ? ge : n[pe] < 0 || 59 < n[pe] ? pe : n[ve] < 0 || 59 < n[ve] ? ve : n[we] < 0 || 999 < n[we] ? we : -1, g(e)._overflowDayOfYear && (t < me || ye < t) && (t = ye), g(e)._overflowWeeks && -1 === t && (t = Me), g(e)._overflowWeekday && -1 === t && (t = Se), g(e).overflow = t), e
	}

	function ht(e, t, n) {
		return null != e ? e : null != t ? t : n
	}

	function ct(e) {
		var t, n, s, i, r, a = [];
		if (!e._d) {
			var o, u;
			for (o = e, u = new Date(c.now()), s = o._useUTC ? [u.getUTCFullYear(), u.getUTCMonth(), u.getUTCDate()] : [u.getFullYear(), u.getMonth(), u.getDate()], e._w && null == e._a[ye] && null == e._a[_e] && function (e) {
					var t, n, s, i, r, a, o, u;
					if (null != (t = e._w).GG || null != t.W || null != t.E) r = 1, a = 4, n = ht(t.GG, e._a[me], Ie(Tt(), 1, 4).year), s = ht(t.W, 1), ((i = ht(t.E, 1)) < 1 || 7 < i) && (u = !0);
					else {
						r = e._locale._week.dow, a = e._locale._week.doy;
						var l = Ie(Tt(), r, a);
						n = ht(t.gg, e._a[me], l.year), s = ht(t.w, l.week), null != t.d ? ((i = t.d) < 0 || 6 < i) && (u = !0) : null != t.e ? (i = t.e + r, (t.e < 0 || 6 < t.e) && (u = !0)) : i = r
					}
					s < 1 || s > Ae(n, r, a) ? g(e)._overflowWeeks = !0 : null != u ? g(e)._overflowWeekday = !0 : (o = Ee(n, s, i, r, a), e._a[me] = o.year, e._dayOfYear = o.dayOfYear)
				}(e), null != e._dayOfYear && (r = ht(e._a[me], s[me]), (e._dayOfYear > De(r) || 0 === e._dayOfYear) && (g(e)._overflowDayOfYear = !0), n = Ge(r, 0, e._dayOfYear), e._a[_e] = n.getUTCMonth(), e._a[ye] = n.getUTCDate()), t = 0; t < 3 && null == e._a[t]; ++t) e._a[t] = a[t] = s[t];
			for (; t < 7; t++) e._a[t] = a[t] = null == e._a[t] ? 2 === t ? 1 : 0 : e._a[t];
			24 === e._a[ge] && 0 === e._a[pe] && 0 === e._a[ve] && 0 === e._a[we] && (e._nextDay = !0, e._a[ge] = 0), e._d = (e._useUTC ? Ge : function (e, t, n, s, i, r, a) {
				var o = new Date(e, t, n, s, i, r, a);
				return e < 100 && 0 <= e && isFinite(o.getFullYear()) && o.setFullYear(e), o
			}).apply(null, a), i = e._useUTC ? e._d.getUTCDay() : e._d.getDay(), null != e._tzm && e._d.setUTCMinutes(e._d.getUTCMinutes() - e._tzm), e._nextDay && (e._a[ge] = 24), e._w && void 0 !== e._w.d && e._w.d !== i && (g(e).weekdayMismatch = !0)
		}
	}
	var ft = /^\s*((?:[+-]\d{6}|\d{4})-(?:\d\d-\d\d|W\d\d-\d|W\d\d|\d\d\d|\d\d))(?:(T| )(\d\d(?::\d\d(?::\d\d(?:[.,]\d+)?)?)?)([\+\-]\d\d(?::?\d\d)?|\s*Z)?)?$/,
		mt = /^\s*((?:[+-]\d{6}|\d{4})(?:\d\d\d\d|W\d\d\d|W\d\d|\d\d\d|\d\d))(?:(T| )(\d\d(?:\d\d(?:\d\d(?:[.,]\d+)?)?)?)([\+\-]\d\d(?::?\d\d)?|\s*Z)?)?$/,
		_t = /Z|[+-]\d\d(?::?\d\d)?/,
		yt = [
			["YYYYYY-MM-DD", /[+-]\d{6}-\d\d-\d\d/],
			["YYYY-MM-DD", /\d{4}-\d\d-\d\d/],
			["GGGG-[W]WW-E", /\d{4}-W\d\d-\d/],
			["GGGG-[W]WW", /\d{4}-W\d\d/, !1],
			["YYYY-DDD", /\d{4}-\d{3}/],
			["YYYY-MM", /\d{4}-\d\d/, !1],
			["YYYYYYMMDD", /[+-]\d{10}/],
			["YYYYMMDD", /\d{8}/],
			["GGGG[W]WWE", /\d{4}W\d{3}/],
			["GGGG[W]WW", /\d{4}W\d{2}/, !1],
			["YYYYDDD", /\d{7}/]
		],
		gt = [
			["HH:mm:ss.SSSS", /\d\d:\d\d:\d\d\.\d+/],
			["HH:mm:ss,SSSS", /\d\d:\d\d:\d\d,\d+/],
			["HH:mm:ss", /\d\d:\d\d:\d\d/],
			["HH:mm", /\d\d:\d\d/],
			["HHmmss.SSSS", /\d\d\d\d\d\d\.\d+/],
			["HHmmss,SSSS", /\d\d\d\d\d\d,\d+/],
			["HHmmss", /\d\d\d\d\d\d/],
			["HHmm", /\d\d\d\d/],
			["HH", /\d\d/]
		],
		pt = /^\/?Date\((\-?\d+)/i;

	function vt(e) {
		var t, n, s, i, r, a, o = e._i,
			u = ft.exec(o) || mt.exec(o);
		if (u) {
			for (g(e).iso = !0, t = 0, n = yt.length; t < n; t++)
				if (yt[t][1].exec(u[1])) {
					i = yt[t][0], s = !1 !== yt[t][2];
					break
				}
			if (null == i) return void(e._isValid = !1);
			if (u[3]) {
				for (t = 0, n = gt.length; t < n; t++)
					if (gt[t][1].exec(u[3])) {
						r = (u[2] || " ") + gt[t][0];
						break
					}
				if (null == r) return void(e._isValid = !1)
			}
			if (!s && null != r) return void(e._isValid = !1);
			if (u[4]) {
				if (!_t.exec(u[4])) return void(e._isValid = !1);
				a = "Z"
			}
			e._f = i + (r || "") + (a || ""), kt(e)
		} else e._isValid = !1
	}
	var wt = /^(?:(Mon|Tue|Wed|Thu|Fri|Sat|Sun),?\s)?(\d{1,2})\s(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s(\d{2,4})\s(\d\d):(\d\d)(?::(\d\d))?\s(?:(UT|GMT|[ECMP][SD]T)|([Zz])|([+-]\d{4}))$/;

	function Mt(e, t, n, s, i, r) {
		var a = [function (e) {
			var t = parseInt(e, 10); {
				if (t <= 49) return 2e3 + t;
				if (t <= 999) return 1900 + t
			}
			return t
		}(e), Re.indexOf(t), parseInt(n, 10), parseInt(s, 10), parseInt(i, 10)];
		return r && a.push(parseInt(r, 10)), a
	}
	var St = {
		UT: 0,
		GMT: 0,
		EDT: -240,
		EST: -300,
		CDT: -300,
		CST: -360,
		MDT: -360,
		MST: -420,
		PDT: -420,
		PST: -480
	};

	function Dt(e) {
		var t, n, s, i = wt.exec(e._i.replace(/\([^)]*\)|[\n\t]/g, " ").replace(/(\s\s+)/g, " ").replace(/^\s\s*/, "").replace(/\s\s*$/, ""));
		if (i) {
			var r = Mt(i[4], i[3], i[2], i[5], i[6], i[7]);
			if (t = i[1], n = r, s = e, t && Ze.indexOf(t) !== new Date(n[0], n[1], n[2]).getDay() && (g(s).weekdayMismatch = !0, !(s._isValid = !1))) return;
			e._a = r, e._tzm = function (e, t, n) {
				if (e) return St[e];
				if (t) return 0;
				var s = parseInt(n, 10),
					i = s % 100;
				return (s - i) / 100 * 60 + i
			}(i[8], i[9], i[10]), e._d = Ge.apply(null, e._a), e._d.setUTCMinutes(e._d.getUTCMinutes() - e._tzm), g(e).rfc2822 = !0
		} else e._isValid = !1
	}

	function kt(e) {
		if (e._f !== c.ISO_8601)
			if (e._f !== c.RFC_2822) {
				e._a = [], g(e).empty = !0;
				var t, n, s, i, r, a, o, u, l = "" + e._i,
					d = l.length,
					h = 0;
				for (s = j(e._f, e._locale).match(N) || [], t = 0; t < s.length; t++) i = s[t], (n = (l.match(le(i, e)) || [])[0]) && (0 < (r = l.substr(0, l.indexOf(n))).length && g(e).unusedInput.push(r), l = l.slice(l.indexOf(n) + n.length), h += n.length), E[i] ? (n ? g(e).empty = !1 : g(e).unusedTokens.push(i), a = i, u = e, null != (o = n) && m(he, a) && he[a](o, u._a, u, a)) : e._strict && !n && g(e).unusedTokens.push(i);
				g(e).charsLeftOver = d - h, 0 < l.length && g(e).unusedInput.push(l), e._a[ge] <= 12 && !0 === g(e).bigHour && 0 < e._a[ge] && (g(e).bigHour = void 0), g(e).parsedDateParts = e._a.slice(0), g(e).meridiem = e._meridiem, e._a[ge] = function (e, t, n) {
					var s;
					if (null == n) return t;
					return null != e.meridiemHour ? e.meridiemHour(t, n) : (null != e.isPM && ((s = e.isPM(n)) && t < 12 && (t += 12), s || 12 !== t || (t = 0)), t)
				}(e._locale, e._a[ge], e._meridiem), ct(e), dt(e)
			} else Dt(e);
		else vt(e)
	}

	function Yt(e) {
		var t, n, s, i, r = e._i,
			a = e._f;
		return e._locale = e._locale || lt(e._l), null === r || void 0 === a && "" === r ? v({
			nullInput: !0
		}) : ("string" == typeof r && (e._i = r = e._locale.preparse(r)), S(r) ? new M(dt(r)) : (h(r) ? e._d = r : o(a) ? function (e) {
			var t, n, s, i, r;
			if (0 === e._f.length) return g(e).invalidFormat = !0, e._d = new Date(NaN);
			for (i = 0; i < e._f.length; i++) r = 0, t = w({}, e), null != e._useUTC && (t._useUTC = e._useUTC), t._f = e._f[i], kt(t), p(t) && (r += g(t).charsLeftOver, r += 10 * g(t).unusedTokens.length, g(t).score = r, (null == s || r < s) && (s = r, n = t));
			_(e, n || t)
		}(e) : a ? kt(e) : l(n = (t = e)._i) ? t._d = new Date(c.now()) : h(n) ? t._d = new Date(n.valueOf()) : "string" == typeof n ? (s = t, null === (i = pt.exec(s._i)) ? (vt(s), !1 === s._isValid && (delete s._isValid, Dt(s), !1 === s._isValid && (delete s._isValid, c.createFromInputFallback(s)))) : s._d = new Date(+i[1])) : o(n) ? (t._a = f(n.slice(0), function (e) {
			return parseInt(e, 10)
		}), ct(t)) : u(n) ? function (e) {
			if (!e._d) {
				var t = C(e._i);
				e._a = f([t.year, t.month, t.day || t.date, t.hour, t.minute, t.second, t.millisecond], function (e) {
					return e && parseInt(e, 10)
				}), ct(e)
			}
		}(t) : d(n) ? t._d = new Date(n) : c.createFromInputFallback(t), p(e) || (e._d = null), e))
	}

	function Ot(e, t, n, s, i) {
		var r, a = {};
		return !0 !== n && !1 !== n || (s = n, n = void 0), (u(e) && function (e) {
			if (Object.getOwnPropertyNames) return 0 === Object.getOwnPropertyNames(e).length;
			var t;
			for (t in e)
				if (e.hasOwnProperty(t)) return !1;
			return !0
		}(e) || o(e) && 0 === e.length) && (e = void 0), a._isAMomentObject = !0, a._useUTC = a._isUTC = i, a._l = n, a._i = e, a._f = t, a._strict = s, (r = new M(dt(Yt(a))))._nextDay && (r.add(1, "d"), r._nextDay = void 0), r
	}

	function Tt(e, t, n, s) {
		return Ot(e, t, n, s, !1)
	}
	c.createFromInputFallback = n("value provided is not in a recognized RFC2822 or ISO format. moment construction falls back to js Date(), which is not reliable across all browsers and versions. Non RFC2822/ISO date formats are discouraged and will be removed in an upcoming major release. Please refer to http://momentjs.com/guides/#/warnings/js-date/ for more info.", function (e) {
		e._d = new Date(e._i + (e._useUTC ? " UTC" : ""))
	}), c.ISO_8601 = function () {}, c.RFC_2822 = function () {};
	var xt = n("moment().min is deprecated, use moment.max instead. http://momentjs.com/guides/#/warnings/min-max/", function () {
			var e = Tt.apply(null, arguments);
			return this.isValid() && e.isValid() ? e < this ? this : e : v()
		}),
		bt = n("moment().max is deprecated, use moment.min instead. http://momentjs.com/guides/#/warnings/min-max/", function () {
			var e = Tt.apply(null, arguments);
			return this.isValid() && e.isValid() ? this < e ? this : e : v()
		});

	function Pt(e, t) {
		var n, s;
		if (1 === t.length && o(t[0]) && (t = t[0]), !t.length) return Tt();
		for (n = t[0], s = 1; s < t.length; ++s) t[s].isValid() && !t[s][e](n) || (n = t[s]);
		return n
	}
	var Wt = ["year", "quarter", "month", "week", "day", "hour", "minute", "second", "millisecond"];

	function Ht(e) {
		var t = C(e),
			n = t.year || 0,
			s = t.quarter || 0,
			i = t.month || 0,
			r = t.week || 0,
			a = t.day || 0,
			o = t.hour || 0,
			u = t.minute || 0,
			l = t.second || 0,
			d = t.millisecond || 0;
		this._isValid = function (e) {
			for (var t in e)
				if (-1 === Ye.call(Wt, t) || null != e[t] && isNaN(e[t])) return !1;
			for (var n = !1, s = 0; s < Wt.length; ++s)
				if (e[Wt[s]]) {
					if (n) return !1;
					parseFloat(e[Wt[s]]) !== k(e[Wt[s]]) && (n = !0)
				}
			return !0
		}(t), this._milliseconds = +d + 1e3 * l + 6e4 * u + 1e3 * o * 60 * 60, this._days = +a + 7 * r, this._months = +i + 3 * s + 12 * n, this._data = {}, this._locale = lt(), this._bubble()
	}

	function Rt(e) {
		return e instanceof Ht
	}

	function Ct(e) {
		return e < 0 ? -1 * Math.round(-1 * e) : Math.round(e)
	}

	function Ft(e, n) {
		I(e, 0, 0, function () {
			var e = this.utcOffset(),
				t = "+";
			return e < 0 && (e = -e, t = "-"), t + U(~~(e / 60), 2) + n + U(~~e % 60, 2)
		})
	}
	Ft("Z", ":"), Ft("ZZ", ""), ue("Z", re), ue("ZZ", re), ce(["Z", "ZZ"], function (e, t, n) {
		n._useUTC = !0, n._tzm = Ut(re, e)
	});
	var Lt = /([\+\-]|\d\d)/gi;

	function Ut(e, t) {
		var n = (t || "").match(e);
		if (null === n) return null;
		var s = ((n[n.length - 1] || []) + "").match(Lt) || ["-", 0, 0],
			i = 60 * s[1] + k(s[2]);
		return 0 === i ? 0 : "+" === s[0] ? i : -i
	}

	function Nt(e, t) {
		var n, s;
		return t._isUTC ? (n = t.clone(), s = (S(e) || h(e) ? e.valueOf() : Tt(e).valueOf()) - n.valueOf(), n._d.setTime(n._d.valueOf() + s), c.updateOffset(n, !1), n) : Tt(e).local()
	}

	function Gt(e) {
		return 15 * -Math.round(e._d.getTimezoneOffset() / 15)
	}

	function Vt() {
		return !!this.isValid() && (this._isUTC && 0 === this._offset)
	}
	c.updateOffset = function () {};
	var Et = /^(\-|\+)?(?:(\d*)[. ])?(\d+)\:(\d+)(?:\:(\d+)(\.\d*)?)?$/,
		It = /^(-|\+)?P(?:([-+]?[0-9,.]*)Y)?(?:([-+]?[0-9,.]*)M)?(?:([-+]?[0-9,.]*)W)?(?:([-+]?[0-9,.]*)D)?(?:T(?:([-+]?[0-9,.]*)H)?(?:([-+]?[0-9,.]*)M)?(?:([-+]?[0-9,.]*)S)?)?$/;

	function At(e, t) {
		var n, s, i, r = e,
			a = null;
		return Rt(e) ? r = {
			ms: e._milliseconds,
			d: e._days,
			M: e._months
		} : d(e) ? (r = {}, t ? r[t] = e : r.milliseconds = e) : (a = Et.exec(e)) ? (n = "-" === a[1] ? -1 : 1, r = {
			y: 0,
			d: k(a[ye]) * n,
			h: k(a[ge]) * n,
			m: k(a[pe]) * n,
			s: k(a[ve]) * n,
			ms: k(Ct(1e3 * a[we])) * n
		}) : (a = It.exec(e)) ? (n = "-" === a[1] ? -1 : (a[1], 1), r = {
			y: jt(a[2], n),
			M: jt(a[3], n),
			w: jt(a[4], n),
			d: jt(a[5], n),
			h: jt(a[6], n),
			m: jt(a[7], n),
			s: jt(a[8], n)
		}) : null == r ? r = {} : "object" == typeof r && ("from" in r || "to" in r) && (i = function (e, t) {
			var n;
			if (!e.isValid() || !t.isValid()) return {
				milliseconds: 0,
				months: 0
			};
			t = Nt(t, e), e.isBefore(t) ? n = Zt(e, t) : ((n = Zt(t, e)).milliseconds = -n.milliseconds, n.months = -n.months);
			return n
		}(Tt(r.from), Tt(r.to)), (r = {}).ms = i.milliseconds, r.M = i.months), s = new Ht(r), Rt(e) && m(e, "_locale") && (s._locale = e._locale), s
	}

	function jt(e, t) {
		var n = e && parseFloat(e.replace(",", "."));
		return (isNaN(n) ? 0 : n) * t
	}

	function Zt(e, t) {
		var n = {
			milliseconds: 0,
			months: 0
		};
		return n.months = t.month() - e.month() + 12 * (t.year() - e.year()), e.clone().add(n.months, "M").isAfter(t) && --n.months, n.milliseconds = +t - +e.clone().add(n.months, "M"), n
	}

	function zt(s, i) {
		return function (e, t) {
			var n;
			return null === t || isNaN(+t) || (T(i, "moment()." + i + "(period, number) is deprecated. Please use moment()." + i + "(number, period). See http://momentjs.com/guides/#/warnings/add-inverted-param/ for more info."), n = e, e = t, t = n), $t(this, At(e = "string" == typeof e ? +e : e, t), s), this
		}
	}

	function $t(e, t, n, s) {
		var i = t._milliseconds,
			r = Ct(t._days),
			a = Ct(t._months);
		e.isValid() && (s = null == s || s, a && Ce(e, xe(e, "Month") + a * n), r && be(e, "Date", xe(e, "Date") + r * n), i && e._d.setTime(e._d.valueOf() + i * n), s && c.updateOffset(e, r || a))
	}
	At.fn = Ht.prototype, At.invalid = function () {
		return At(NaN)
	};
	var qt = zt(1, "add"),
		Jt = zt(-1, "subtract");

	function Bt(e, t) {
		var n = 12 * (t.year() - e.year()) + (t.month() - e.month()),
			s = e.clone().add(n, "months");
		return -(n + (t - s < 0 ? (t - s) / (s - e.clone().add(n - 1, "months")) : (t - s) / (e.clone().add(n + 1, "months") - s))) || 0
	}

	function Qt(e) {
		var t;
		return void 0 === e ? this._locale._abbr : (null != (t = lt(e)) && (this._locale = t), this)
	}
	c.defaultFormat = "YYYY-MM-DDTHH:mm:ssZ", c.defaultFormatUtc = "YYYY-MM-DDTHH:mm:ss[Z]";
	var Xt = n("moment().lang() is deprecated. Instead, use moment().localeData() to get the language configuration. Use moment().locale() to change languages.", function (e) {
		return void 0 === e ? this.localeData() : this.locale(e)
	});

	function Kt() {
		return this._locale
	}

	function en(e, t) {
		I(0, [e, e.length], 0, t)
	}

	function tn(e, t, n, s, i) {
		var r;
		return null == e ? Ie(this, s, i).year : ((r = Ae(e, s, i)) < t && (t = r), function (e, t, n, s, i) {
			var r = Ee(e, t, n, s, i),
				a = Ge(r.year, 0, r.dayOfYear);
			return this.year(a.getUTCFullYear()), this.month(a.getUTCMonth()), this.date(a.getUTCDate()), this
		}.call(this, e, t, n, s, i))
	}
	I(0, ["gg", 2], 0, function () {
		return this.weekYear() % 100
	}), I(0, ["GG", 2], 0, function () {
		return this.isoWeekYear() % 100
	}), en("gggg", "weekYear"), en("ggggg", "weekYear"), en("GGGG", "isoWeekYear"), en("GGGGG", "isoWeekYear"), H("weekYear", "gg"), H("isoWeekYear", "GG"), L("weekYear", 1), L("isoWeekYear", 1), ue("G", se), ue("g", se), ue("GG", B, z), ue("gg", B, z), ue("GGGG", ee, q), ue("gggg", ee, q), ue("GGGGG", te, J), ue("ggggg", te, J), fe(["gggg", "ggggg", "GGGG", "GGGGG"], function (e, t, n, s) {
		t[s.substr(0, 2)] = k(e)
	}), fe(["gg", "GG"], function (e, t, n, s) {
		t[s] = c.parseTwoDigitYear(e)
	}), I("Q", 0, "Qo", "quarter"), H("quarter", "Q"), L("quarter", 7), ue("Q", Z), ce("Q", function (e, t) {
		t[_e] = 3 * (k(e) - 1)
	}), I("D", ["DD", 2], "Do", "date"), H("date", "D"), L("date", 9), ue("D", B), ue("DD", B, z), ue("Do", function (e, t) {
		return e ? t._dayOfMonthOrdinalParse || t._ordinalParse : t._dayOfMonthOrdinalParseLenient
	}), ce(["D", "DD"], ye), ce("Do", function (e, t) {
		t[ye] = k(e.match(B)[0])
	});
	var nn = Te("Date", !0);
	I("DDD", ["DDDD", 3], "DDDo", "dayOfYear"), H("dayOfYear", "DDD"), L("dayOfYear", 4), ue("DDD", K), ue("DDDD", $), ce(["DDD", "DDDD"], function (e, t, n) {
		n._dayOfYear = k(e)
	}), I("m", ["mm", 2], 0, "minute"), H("minute", "m"), L("minute", 14), ue("m", B), ue("mm", B, z), ce(["m", "mm"], pe);
	var sn = Te("Minutes", !1);
	I("s", ["ss", 2], 0, "second"), H("second", "s"), L("second", 15), ue("s", B), ue("ss", B, z), ce(["s", "ss"], ve);
	var rn, an = Te("Seconds", !1);
	for (I("S", 0, 0, function () {
			return ~~(this.millisecond() / 100)
		}), I(0, ["SS", 2], 0, function () {
			return ~~(this.millisecond() / 10)
		}), I(0, ["SSS", 3], 0, "millisecond"), I(0, ["SSSS", 4], 0, function () {
			return 10 * this.millisecond()
		}), I(0, ["SSSSS", 5], 0, function () {
			return 100 * this.millisecond()
		}), I(0, ["SSSSSS", 6], 0, function () {
			return 1e3 * this.millisecond()
		}), I(0, ["SSSSSSS", 7], 0, function () {
			return 1e4 * this.millisecond()
		}), I(0, ["SSSSSSSS", 8], 0, function () {
			return 1e5 * this.millisecond()
		}), I(0, ["SSSSSSSSS", 9], 0, function () {
			return 1e6 * this.millisecond()
		}), H("millisecond", "ms"), L("millisecond", 16), ue("S", K, Z), ue("SS", K, z), ue("SSS", K, $), rn = "SSSS"; rn.length <= 9; rn += "S") ue(rn, ne);

	function on(e, t) {
		t[we] = k(1e3 * ("0." + e))
	}
	for (rn = "S"; rn.length <= 9; rn += "S") ce(rn, on);
	var un = Te("Milliseconds", !1);
	I("z", 0, 0, "zoneAbbr"), I("zz", 0, 0, "zoneName");
	var ln = M.prototype;

	function dn(e) {
		return e
	}
	ln.add = qt, ln.calendar = function (e, t) {
		var n = e || Tt(),
			s = Nt(n, this).startOf("day"),
			i = c.calendarFormat(this, s) || "sameElse",
			r = t && (x(t[i]) ? t[i].call(this, n) : t[i]);
		return this.format(r || this.localeData().calendar(i, this, Tt(n)))
	}, ln.clone = function () {
		return new M(this)
	}, ln.diff = function (e, t, n) {
		var s, i, r;
		if (!this.isValid()) return NaN;
		if (!(s = Nt(e, this)).isValid()) return NaN;
		switch (i = 6e4 * (s.utcOffset() - this.utcOffset()), t = R(t)) {
			case "year":
				r = Bt(this, s) / 12;
				break;
			case "month":
				r = Bt(this, s);
				break;
			case "quarter":
				r = Bt(this, s) / 3;
				break;
			case "second":
				r = (this - s) / 1e3;
				break;
			case "minute":
				r = (this - s) / 6e4;
				break;
			case "hour":
				r = (this - s) / 36e5;
				break;
			case "day":
				r = (this - s - i) / 864e5;
				break;
			case "week":
				r = (this - s - i) / 6048e5;
				break;
			default:
				r = this - s
		}
		return n ? r : D(r)
	}, ln.endOf = function (e) {
		return void 0 === (e = R(e)) || "millisecond" === e ? this : ("date" === e && (e = "day"), this.startOf(e).add(1, "isoWeek" === e ? "week" : e).subtract(1, "ms"))
	}, ln.format = function (e) {
		e || (e = this.isUtc() ? c.defaultFormatUtc : c.defaultFormat);
		var t = A(this, e);
		return this.localeData().postformat(t)
	}, ln.from = function (e, t) {
		return this.isValid() && (S(e) && e.isValid() || Tt(e).isValid()) ? At({
			to: this,
			from: e
		}).locale(this.locale()).humanize(!t) : this.localeData().invalidDate()
	}, ln.fromNow = function (e) {
		return this.from(Tt(), e)
	}, ln.to = function (e, t) {
		return this.isValid() && (S(e) && e.isValid() || Tt(e).isValid()) ? At({
			from: this,
			to: e
		}).locale(this.locale()).humanize(!t) : this.localeData().invalidDate()
	}, ln.toNow = function (e) {
		return this.to(Tt(), e)
	}, ln.get = function (e) {
		return x(this[e = R(e)]) ? this[e]() : this
	}, ln.invalidAt = function () {
		return g(this).overflow
	}, ln.isAfter = function (e, t) {
		var n = S(e) ? e : Tt(e);
		return !(!this.isValid() || !n.isValid()) && ("millisecond" === (t = R(l(t) ? "millisecond" : t)) ? this.valueOf() > n.valueOf() : n.valueOf() < this.clone().startOf(t).valueOf())
	}, ln.isBefore = function (e, t) {
		var n = S(e) ? e : Tt(e);
		return !(!this.isValid() || !n.isValid()) && ("millisecond" === (t = R(l(t) ? "millisecond" : t)) ? this.valueOf() < n.valueOf() : this.clone().endOf(t).valueOf() < n.valueOf())
	}, ln.isBetween = function (e, t, n, s) {
		return ("(" === (s = s || "()")[0] ? this.isAfter(e, n) : !this.isBefore(e, n)) && (")" === s[1] ? this.isBefore(t, n) : !this.isAfter(t, n))
	}, ln.isSame = function (e, t) {
		var n, s = S(e) ? e : Tt(e);
		return !(!this.isValid() || !s.isValid()) && ("millisecond" === (t = R(t || "millisecond")) ? this.valueOf() === s.valueOf() : (n = s.valueOf(), this.clone().startOf(t).valueOf() <= n && n <= this.clone().endOf(t).valueOf()))
	}, ln.isSameOrAfter = function (e, t) {
		return this.isSame(e, t) || this.isAfter(e, t)
	}, ln.isSameOrBefore = function (e, t) {
		return this.isSame(e, t) || this.isBefore(e, t)
	}, ln.isValid = function () {
		return p(this)
	}, ln.lang = Xt, ln.locale = Qt, ln.localeData = Kt, ln.max = bt, ln.min = xt, ln.parsingFlags = function () {
		return _({}, g(this))
	}, ln.set = function (e, t) {
		if ("object" == typeof e)
			for (var n = function (e) {
					var t = [];
					for (var n in e) t.push({
						unit: n,
						priority: F[n]
					});
					return t.sort(function (e, t) {
						return e.priority - t.priority
					}), t
				}(e = C(e)), s = 0; s < n.length; s++) this[n[s].unit](e[n[s].unit]);
		else if (x(this[e = R(e)])) return this[e](t);
		return this
	}, ln.startOf = function (e) {
		switch (e = R(e)) {
			case "year":
				this.month(0);
			case "quarter":
			case "month":
				this.date(1);
			case "week":
			case "isoWeek":
			case "day":
			case "date":
				this.hours(0);
			case "hour":
				this.minutes(0);
			case "minute":
				this.seconds(0);
			case "second":
				this.milliseconds(0)
		}
		return "week" === e && this.weekday(0), "isoWeek" === e && this.isoWeekday(1), "quarter" === e && this.month(3 * Math.floor(this.month() / 3)), this
	}, ln.subtract = Jt, ln.toArray = function () {
		var e = this;
		return [e.year(), e.month(), e.date(), e.hour(), e.minute(), e.second(), e.millisecond()]
	}, ln.toObject = function () {
		var e = this;
		return {
			years: e.year(),
			months: e.month(),
			date: e.date(),
			hours: e.hours(),
			minutes: e.minutes(),
			seconds: e.seconds(),
			milliseconds: e.milliseconds()
		}
	}, ln.toDate = function () {
		return new Date(this.valueOf())
	}, ln.toISOString = function (e) {
		if (!this.isValid()) return null;
		var t = !0 !== e,
			n = t ? this.clone().utc() : this;
		return n.year() < 0 || 9999 < n.year() ? A(n, t ? "YYYYYY-MM-DD[T]HH:mm:ss.SSS[Z]" : "YYYYYY-MM-DD[T]HH:mm:ss.SSSZ") : x(Date.prototype.toISOString) ? t ? this.toDate().toISOString() : new Date(this.valueOf() + 60 * this.utcOffset() * 1e3).toISOString().replace("Z", A(n, "Z")) : A(n, t ? "YYYY-MM-DD[T]HH:mm:ss.SSS[Z]" : "YYYY-MM-DD[T]HH:mm:ss.SSSZ")
	}, ln.inspect = function () {
		if (!this.isValid()) return "moment.invalid(/* " + this._i + " */)";
		var e = "moment",
			t = "";
		this.isLocal() || (e = 0 === this.utcOffset() ? "moment.utc" : "moment.parseZone", t = "Z");
		var n = "[" + e + '("]',
			s = 0 <= this.year() && this.year() <= 9999 ? "YYYY" : "YYYYYY",
			i = t + '[")]';
		return this.format(n + s + "-MM-DD[T]HH:mm:ss.SSS" + i)
	}, ln.toJSON = function () {
		return this.isValid() ? this.toISOString() : null
	}, ln.toString = function () {
		return this.clone().locale("en").format("ddd MMM DD YYYY HH:mm:ss [GMT]ZZ")
	}, ln.unix = function () {
		return Math.floor(this.valueOf() / 1e3)
	}, ln.valueOf = function () {
		return this._d.valueOf() - 6e4 * (this._offset || 0)
	}, ln.creationData = function () {
		return {
			input: this._i,
			format: this._f,
			locale: this._locale,
			isUTC: this._isUTC,
			strict: this._strict
		}
	}, ln.year = Oe, ln.isLeapYear = function () {
		return ke(this.year())
	}, ln.weekYear = function (e) {
		return tn.call(this, e, this.week(), this.weekday(), this.localeData()._week.dow, this.localeData()._week.doy)
	}, ln.isoWeekYear = function (e) {
		return tn.call(this, e, this.isoWeek(), this.isoWeekday(), 1, 4)
	}, ln.quarter = ln.quarters = function (e) {
		return null == e ? Math.ceil((this.month() + 1) / 3) : this.month(3 * (e - 1) + this.month() % 3)
	}, ln.month = Fe, ln.daysInMonth = function () {
		return Pe(this.year(), this.month())
	}, ln.week = ln.weeks = function (e) {
		var t = this.localeData().week(this);
		return null == e ? t : this.add(7 * (e - t), "d")
	}, ln.isoWeek = ln.isoWeeks = function (e) {
		var t = Ie(this, 1, 4).week;
		return null == e ? t : this.add(7 * (e - t), "d")
	}, ln.weeksInYear = function () {
		var e = this.localeData()._week;
		return Ae(this.year(), e.dow, e.doy)
	}, ln.isoWeeksInYear = function () {
		return Ae(this.year(), 1, 4)
	}, ln.date = nn, ln.day = ln.days = function (e) {
		if (!this.isValid()) return null != e ? this : NaN;
		var t, n, s = this._isUTC ? this._d.getUTCDay() : this._d.getDay();
		return null != e ? (t = e, n = this.localeData(), e = "string" != typeof t ? t : isNaN(t) ? "number" == typeof (t = n.weekdaysParse(t)) ? t : null : parseInt(t, 10), this.add(e - s, "d")) : s
	}, ln.weekday = function (e) {
		if (!this.isValid()) return null != e ? this : NaN;
		var t = (this.day() + 7 - this.localeData()._week.dow) % 7;
		return null == e ? t : this.add(e - t, "d")
	}, ln.isoWeekday = function (e) {
		if (!this.isValid()) return null != e ? this : NaN;
		if (null != e) {
			var t = (n = e, s = this.localeData(), "string" == typeof n ? s.weekdaysParse(n) % 7 || 7 : isNaN(n) ? null : n);
			return this.day(this.day() % 7 ? t : t - 7)
		}
		return this.day() || 7;
		var n, s
	}, ln.dayOfYear = function (e) {
		var t = Math.round((this.clone().startOf("day") - this.clone().startOf("year")) / 864e5) + 1;
		return null == e ? t : this.add(e - t, "d")
	}, ln.hour = ln.hours = tt, ln.minute = ln.minutes = sn, ln.second = ln.seconds = an, ln.millisecond = ln.milliseconds = un, ln.utcOffset = function (e, t, n) {
		var s, i = this._offset || 0;
		if (!this.isValid()) return null != e ? this : NaN;
		if (null != e) {
			if ("string" == typeof e) {
				if (null === (e = Ut(re, e))) return this
			} else Math.abs(e) < 16 && !n && (e *= 60);
			return !this._isUTC && t && (s = Gt(this)), this._offset = e, this._isUTC = !0, null != s && this.add(s, "m"), i !== e && (!t || this._changeInProgress ? $t(this, At(e - i, "m"), 1, !1) : this._changeInProgress || (this._changeInProgress = !0, c.updateOffset(this, !0), this._changeInProgress = null)), this
		}
		return this._isUTC ? i : Gt(this)
	}, ln.utc = function (e) {
		return this.utcOffset(0, e)
	}, ln.local = function (e) {
		return this._isUTC && (this.utcOffset(0, e), this._isUTC = !1, e && this.subtract(Gt(this), "m")), this
	}, ln.parseZone = function () {
		if (null != this._tzm) this.utcOffset(this._tzm, !1, !0);
		else if ("string" == typeof this._i) {
			var e = Ut(ie, this._i);
			null != e ? this.utcOffset(e) : this.utcOffset(0, !0)
		}
		return this
	}, ln.hasAlignedHourOffset = function (e) {
		return !!this.isValid() && (e = e ? Tt(e).utcOffset() : 0, (this.utcOffset() - e) % 60 == 0)
	}, ln.isDST = function () {
		return this.utcOffset() > this.clone().month(0).utcOffset() || this.utcOffset() > this.clone().month(5).utcOffset()
	}, ln.isLocal = function () {
		return !!this.isValid() && !this._isUTC
	}, ln.isUtcOffset = function () {
		return !!this.isValid() && this._isUTC
	}, ln.isUtc = Vt, ln.isUTC = Vt, ln.zoneAbbr = function () {
		return this._isUTC ? "UTC" : ""
	}, ln.zoneName = function () {
		return this._isUTC ? "Coordinated Universal Time" : ""
	}, ln.dates = n("dates accessor is deprecated. Use date instead.", nn), ln.months = n("months accessor is deprecated. Use month instead", Fe), ln.years = n("years accessor is deprecated. Use year instead", Oe), ln.zone = n("moment().zone is deprecated, use moment().utcOffset instead. http://momentjs.com/guides/#/warnings/zone/", function (e, t) {
		return null != e ? ("string" != typeof e && (e = -e), this.utcOffset(e, t), this) : -this.utcOffset()
	}), ln.isDSTShifted = n("isDSTShifted is deprecated. See http://momentjs.com/guides/#/warnings/dst-shifted/ for more information", function () {
		if (!l(this._isDSTShifted)) return this._isDSTShifted;
		var e = {};
		if (w(e, this), (e = Yt(e))._a) {
			var t = e._isUTC ? y(e._a) : Tt(e._a);
			this._isDSTShifted = this.isValid() && 0 < a(e._a, t.toArray())
		} else this._isDSTShifted = !1;
		return this._isDSTShifted
	});
	var hn = P.prototype;

	function cn(e, t, n, s) {
		var i = lt(),
			r = y().set(s, t);
		return i[n](r, e)
	}

	function fn(e, t, n) {
		if (d(e) && (t = e, e = void 0), e = e || "", null != t) return cn(e, t, n, "month");
		var s, i = [];
		for (s = 0; s < 12; s++) i[s] = cn(e, s, n, "month");
		return i
	}

	function mn(e, t, n, s) {
		"boolean" == typeof e ? d(t) && (n = t, t = void 0) : (t = e, e = !1, d(n = t) && (n = t, t = void 0)), t = t || "";
		var i, r = lt(),
			a = e ? r._week.dow : 0;
		if (null != n) return cn(t, (n + a) % 7, s, "day");
		var o = [];
		for (i = 0; i < 7; i++) o[i] = cn(t, (i + a) % 7, s, "day");
		return o
	}
	hn.calendar = function (e, t, n) {
		var s = this._calendar[e] || this._calendar.sameElse;
		return x(s) ? s.call(t, n) : s
	}, hn.longDateFormat = function (e) {
		var t = this._longDateFormat[e],
			n = this._longDateFormat[e.toUpperCase()];
		return t || !n ? t : (this._longDateFormat[e] = n.replace(/MMMM|MM|DD|dddd/g, function (e) {
			return e.slice(1)
		}), this._longDateFormat[e])
	}, hn.invalidDate = function () {
		return this._invalidDate
	}, hn.ordinal = function (e) {
		return this._ordinal.replace("%d", e)
	}, hn.preparse = dn, hn.postformat = dn, hn.relativeTime = function (e, t, n, s) {
		var i = this._relativeTime[n];
		return x(i) ? i(e, t, n, s) : i.replace(/%d/i, e)
	}, hn.pastFuture = function (e, t) {
		var n = this._relativeTime[0 < e ? "future" : "past"];
		return x(n) ? n(t) : n.replace(/%s/i, t)
	}, hn.set = function (e) {
		var t, n;
		for (n in e) x(t = e[n]) ? this[n] = t : this["_" + n] = t;
		this._config = e, this._dayOfMonthOrdinalParseLenient = new RegExp((this._dayOfMonthOrdinalParse.source || this._ordinalParse.source) + "|" + /\d{1,2}/.source)
	}, hn.months = function (e, t) {
		return e ? o(this._months) ? this._months[e.month()] : this._months[(this._months.isFormat || We).test(t) ? "format" : "standalone"][e.month()] : o(this._months) ? this._months : this._months.standalone
	}, hn.monthsShort = function (e, t) {
		return e ? o(this._monthsShort) ? this._monthsShort[e.month()] : this._monthsShort[We.test(t) ? "format" : "standalone"][e.month()] : o(this._monthsShort) ? this._monthsShort : this._monthsShort.standalone
	}, hn.monthsParse = function (e, t, n) {
		var s, i, r;
		if (this._monthsParseExact) return function (e, t, n) {
			var s, i, r, a = e.toLocaleLowerCase();
			if (!this._monthsParse)
				for (this._monthsParse = [], this._longMonthsParse = [], this._shortMonthsParse = [], s = 0; s < 12; ++s) r = y([2e3, s]), this._shortMonthsParse[s] = this.monthsShort(r, "").toLocaleLowerCase(), this._longMonthsParse[s] = this.months(r, "").toLocaleLowerCase();
			return n ? "MMM" === t ? -1 !== (i = Ye.call(this._shortMonthsParse, a)) ? i : null : -1 !== (i = Ye.call(this._longMonthsParse, a)) ? i : null : "MMM" === t ? -1 !== (i = Ye.call(this._shortMonthsParse, a)) ? i : -1 !== (i = Ye.call(this._longMonthsParse, a)) ? i : null : -1 !== (i = Ye.call(this._longMonthsParse, a)) ? i : -1 !== (i = Ye.call(this._shortMonthsParse, a)) ? i : null
		}.call(this, e, t, n);
		for (this._monthsParse || (this._monthsParse = [], this._longMonthsParse = [], this._shortMonthsParse = []), s = 0; s < 12; s++) {
			if (i = y([2e3, s]), n && !this._longMonthsParse[s] && (this._longMonthsParse[s] = new RegExp("^" + this.months(i, "").replace(".", "") + "$", "i"), this._shortMonthsParse[s] = new RegExp("^" + this.monthsShort(i, "").replace(".", "") + "$", "i")), n || this._monthsParse[s] || (r = "^" + this.months(i, "") + "|^" + this.monthsShort(i, ""), this._monthsParse[s] = new RegExp(r.replace(".", ""), "i")), n && "MMMM" === t && this._longMonthsParse[s].test(e)) return s;
			if (n && "MMM" === t && this._shortMonthsParse[s].test(e)) return s;
			if (!n && this._monthsParse[s].test(e)) return s
		}
	}, hn.monthsRegex = function (e) {
		return this._monthsParseExact ? (m(this, "_monthsRegex") || Ne.call(this), e ? this._monthsStrictRegex : this._monthsRegex) : (m(this, "_monthsRegex") || (this._monthsRegex = Ue), this._monthsStrictRegex && e ? this._monthsStrictRegex : this._monthsRegex)
	}, hn.monthsShortRegex = function (e) {
		return this._monthsParseExact ? (m(this, "_monthsRegex") || Ne.call(this), e ? this._monthsShortStrictRegex : this._monthsShortRegex) : (m(this, "_monthsShortRegex") || (this._monthsShortRegex = Le), this._monthsShortStrictRegex && e ? this._monthsShortStrictRegex : this._monthsShortRegex)
	}, hn.week = function (e) {
		return Ie(e, this._week.dow, this._week.doy).week
	}, hn.firstDayOfYear = function () {
		return this._week.doy
	}, hn.firstDayOfWeek = function () {
		return this._week.dow
	}, hn.weekdays = function (e, t) {
		return e ? o(this._weekdays) ? this._weekdays[e.day()] : this._weekdays[this._weekdays.isFormat.test(t) ? "format" : "standalone"][e.day()] : o(this._weekdays) ? this._weekdays : this._weekdays.standalone
	}, hn.weekdaysMin = function (e) {
		return e ? this._weekdaysMin[e.day()] : this._weekdaysMin
	}, hn.weekdaysShort = function (e) {
		return e ? this._weekdaysShort[e.day()] : this._weekdaysShort
	}, hn.weekdaysParse = function (e, t, n) {
		var s, i, r;
		if (this._weekdaysParseExact) return function (e, t, n) {
			var s, i, r, a = e.toLocaleLowerCase();
			if (!this._weekdaysParse)
				for (this._weekdaysParse = [], this._shortWeekdaysParse = [], this._minWeekdaysParse = [], s = 0; s < 7; ++s) r = y([2e3, 1]).day(s), this._minWeekdaysParse[s] = this.weekdaysMin(r, "").toLocaleLowerCase(), this._shortWeekdaysParse[s] = this.weekdaysShort(r, "").toLocaleLowerCase(), this._weekdaysParse[s] = this.weekdays(r, "").toLocaleLowerCase();
			return n ? "dddd" === t ? -1 !== (i = Ye.call(this._weekdaysParse, a)) ? i : null : "ddd" === t ? -1 !== (i = Ye.call(this._shortWeekdaysParse, a)) ? i : null : -1 !== (i = Ye.call(this._minWeekdaysParse, a)) ? i : null : "dddd" === t ? -1 !== (i = Ye.call(this._weekdaysParse, a)) ? i : -1 !== (i = Ye.call(this._shortWeekdaysParse, a)) ? i : -1 !== (i = Ye.call(this._minWeekdaysParse, a)) ? i : null : "ddd" === t ? -1 !== (i = Ye.call(this._shortWeekdaysParse, a)) ? i : -1 !== (i = Ye.call(this._weekdaysParse, a)) ? i : -1 !== (i = Ye.call(this._minWeekdaysParse, a)) ? i : null : -1 !== (i = Ye.call(this._minWeekdaysParse, a)) ? i : -1 !== (i = Ye.call(this._weekdaysParse, a)) ? i : -1 !== (i = Ye.call(this._shortWeekdaysParse, a)) ? i : null
		}.call(this, e, t, n);
		for (this._weekdaysParse || (this._weekdaysParse = [], this._minWeekdaysParse = [], this._shortWeekdaysParse = [], this._fullWeekdaysParse = []), s = 0; s < 7; s++) {
			if (i = y([2e3, 1]).day(s), n && !this._fullWeekdaysParse[s] && (this._fullWeekdaysParse[s] = new RegExp("^" + this.weekdays(i, "").replace(".", "\\.?") + "$", "i"), this._shortWeekdaysParse[s] = new RegExp("^" + this.weekdaysShort(i, "").replace(".", "\\.?") + "$", "i"), this._minWeekdaysParse[s] = new RegExp("^" + this.weekdaysMin(i, "").replace(".", "\\.?") + "$", "i")), this._weekdaysParse[s] || (r = "^" + this.weekdays(i, "") + "|^" + this.weekdaysShort(i, "") + "|^" + this.weekdaysMin(i, ""), this._weekdaysParse[s] = new RegExp(r.replace(".", ""), "i")), n && "dddd" === t && this._fullWeekdaysParse[s].test(e)) return s;
			if (n && "ddd" === t && this._shortWeekdaysParse[s].test(e)) return s;
			if (n && "dd" === t && this._minWeekdaysParse[s].test(e)) return s;
			if (!n && this._weekdaysParse[s].test(e)) return s
		}
	}, hn.weekdaysRegex = function (e) {
		return this._weekdaysParseExact ? (m(this, "_weekdaysRegex") || Be.call(this), e ? this._weekdaysStrictRegex : this._weekdaysRegex) : (m(this, "_weekdaysRegex") || (this._weekdaysRegex = $e), this._weekdaysStrictRegex && e ? this._weekdaysStrictRegex : this._weekdaysRegex)
	}, hn.weekdaysShortRegex = function (e) {
		return this._weekdaysParseExact ? (m(this, "_weekdaysRegex") || Be.call(this), e ? this._weekdaysShortStrictRegex : this._weekdaysShortRegex) : (m(this, "_weekdaysShortRegex") || (this._weekdaysShortRegex = qe), this._weekdaysShortStrictRegex && e ? this._weekdaysShortStrictRegex : this._weekdaysShortRegex)
	}, hn.weekdaysMinRegex = function (e) {
		return this._weekdaysParseExact ? (m(this, "_weekdaysRegex") || Be.call(this), e ? this._weekdaysMinStrictRegex : this._weekdaysMinRegex) : (m(this, "_weekdaysMinRegex") || (this._weekdaysMinRegex = Je), this._weekdaysMinStrictRegex && e ? this._weekdaysMinStrictRegex : this._weekdaysMinRegex)
	}, hn.isPM = function (e) {
		return "p" === (e + "").toLowerCase().charAt(0)
	}, hn.meridiem = function (e, t, n) {
		return 11 < e ? n ? "pm" : "PM" : n ? "am" : "AM"
	}, ot("en", {
		dayOfMonthOrdinalParse: /\d{1,2}(th|st|nd|rd)/,
		ordinal: function (e) {
			var t = e % 10;
			return e + (1 === k(e % 100 / 10) ? "th" : 1 === t ? "st" : 2 === t ? "nd" : 3 === t ? "rd" : "th")
		}
	}), c.lang = n("moment.lang is deprecated. Use moment.locale instead.", ot), c.langData = n("moment.langData is deprecated. Use moment.localeData instead.", lt);
	var _n = Math.abs;

	function yn(e, t, n, s) {
		var i = At(t, n);
		return e._milliseconds += s * i._milliseconds, e._days += s * i._days, e._months += s * i._months, e._bubble()
	}

	function gn(e) {
		return e < 0 ? Math.floor(e) : Math.ceil(e)
	}

	function pn(e) {
		return 4800 * e / 146097
	}

	function vn(e) {
		return 146097 * e / 4800
	}

	function wn(e) {
		return function () {
			return this.as(e)
		}
	}
	var Mn = wn("ms"),
		Sn = wn("s"),
		Dn = wn("m"),
		kn = wn("h"),
		Yn = wn("d"),
		On = wn("w"),
		Tn = wn("M"),
		xn = wn("y");

	function bn(e) {
		return function () {
			return this.isValid() ? this._data[e] : NaN
		}
	}
	var Pn = bn("milliseconds"),
		Wn = bn("seconds"),
		Hn = bn("minutes"),
		Rn = bn("hours"),
		Cn = bn("days"),
		Fn = bn("months"),
		Ln = bn("years");
	var Un = Math.round,
		Nn = {
			ss: 44,
			s: 45,
			m: 45,
			h: 22,
			d: 26,
			M: 11
		};
	var Gn = Math.abs;

	function Vn(e) {
		return (0 < e) - (e < 0) || +e
	}

	function En() {
		if (!this.isValid()) return this.localeData().invalidDate();
		var e, t, n = Gn(this._milliseconds) / 1e3,
			s = Gn(this._days),
			i = Gn(this._months);
		t = D((e = D(n / 60)) / 60), n %= 60, e %= 60;
		var r = D(i / 12),
			a = i %= 12,
			o = s,
			u = t,
			l = e,
			d = n ? n.toFixed(3).replace(/\.?0+$/, "") : "",
			h = this.asSeconds();
		if (!h) return "P0D";
		var c = h < 0 ? "-" : "",
			f = Vn(this._months) !== Vn(h) ? "-" : "",
			m = Vn(this._days) !== Vn(h) ? "-" : "",
			_ = Vn(this._milliseconds) !== Vn(h) ? "-" : "";
		return c + "P" + (r ? f + r + "Y" : "") + (a ? f + a + "M" : "") + (o ? m + o + "D" : "") + (u || l || d ? "T" : "") + (u ? _ + u + "H" : "") + (l ? _ + l + "M" : "") + (d ? _ + d + "S" : "")
	}
	var In = Ht.prototype;
	return In.isValid = function () {
		return this._isValid
	}, In.abs = function () {
		var e = this._data;
		return this._milliseconds = _n(this._milliseconds), this._days = _n(this._days), this._months = _n(this._months), e.milliseconds = _n(e.milliseconds), e.seconds = _n(e.seconds), e.minutes = _n(e.minutes), e.hours = _n(e.hours), e.months = _n(e.months), e.years = _n(e.years), this
	}, In.add = function (e, t) {
		return yn(this, e, t, 1)
	}, In.subtract = function (e, t) {
		return yn(this, e, t, -1)
	}, In.as = function (e) {
		if (!this.isValid()) return NaN;
		var t, n, s = this._milliseconds;
		if ("month" === (e = R(e)) || "year" === e) return t = this._days + s / 864e5, n = this._months + pn(t), "month" === e ? n : n / 12;
		switch (t = this._days + Math.round(vn(this._months)), e) {
			case "week":
				return t / 7 + s / 6048e5;
			case "day":
				return t + s / 864e5;
			case "hour":
				return 24 * t + s / 36e5;
			case "minute":
				return 1440 * t + s / 6e4;
			case "second":
				return 86400 * t + s / 1e3;
			case "millisecond":
				return Math.floor(864e5 * t) + s;
			default:
				throw new Error("Unknown unit " + e)
		}
	}, In.asMilliseconds = Mn, In.asSeconds = Sn, In.asMinutes = Dn, In.asHours = kn, In.asDays = Yn, In.asWeeks = On, In.asMonths = Tn, In.asYears = xn, In.valueOf = function () {
		return this.isValid() ? this._milliseconds + 864e5 * this._days + this._months % 12 * 2592e6 + 31536e6 * k(this._months / 12) : NaN
	}, In._bubble = function () {
		var e, t, n, s, i, r = this._milliseconds,
			a = this._days,
			o = this._months,
			u = this._data;
		return 0 <= r && 0 <= a && 0 <= o || r <= 0 && a <= 0 && o <= 0 || (r += 864e5 * gn(vn(o) + a), o = a = 0), u.milliseconds = r % 1e3, e = D(r / 1e3), u.seconds = e % 60, t = D(e / 60), u.minutes = t % 60, n = D(t / 60), u.hours = n % 24, o += i = D(pn(a += D(n / 24))), a -= gn(vn(i)), s = D(o / 12), o %= 12, u.days = a, u.months = o, u.years = s, this
	}, In.clone = function () {
		return At(this)
	}, In.get = function (e) {
		return e = R(e), this.isValid() ? this[e + "s"]() : NaN
	}, In.milliseconds = Pn, In.seconds = Wn, In.minutes = Hn, In.hours = Rn, In.days = Cn, In.weeks = function () {
		return D(this.days() / 7)
	}, In.months = Fn, In.years = Ln, In.humanize = function (e) {
		if (!this.isValid()) return this.localeData().invalidDate();
		var t, n, s, i, r, a, o, u, l, d, h, c = this.localeData(),
			f = (n = !e, s = c, i = At(t = this).abs(), r = Un(i.as("s")), a = Un(i.as("m")), o = Un(i.as("h")), u = Un(i.as("d")), l = Un(i.as("M")), d = Un(i.as("y")), (h = r <= Nn.ss && ["s", r] || r < Nn.s && ["ss", r] || a <= 1 && ["m"] || a < Nn.m && ["mm", a] || o <= 1 && ["h"] || o < Nn.h && ["hh", o] || u <= 1 && ["d"] || u < Nn.d && ["dd", u] || l <= 1 && ["M"] || l < Nn.M && ["MM", l] || d <= 1 && ["y"] || ["yy", d])[2] = n, h[3] = 0 < +t, h[4] = s, function (e, t, n, s, i) {
				return i.relativeTime(t || 1, !!n, e, s)
			}.apply(null, h));
		return e && (f = c.pastFuture(+this, f)), c.postformat(f)
	}, In.toISOString = En, In.toString = En, In.toJSON = En, In.locale = Qt, In.localeData = Kt, In.toIsoString = n("toIsoString() is deprecated. Please use toISOString() instead (notice the capitals)", En), In.lang = Xt, I("X", 0, 0, "unix"), I("x", 0, 0, "valueOf"), ue("x", se), ue("X", /[+-]?\d+(\.\d{1,3})?/), ce("X", function (e, t, n) {
		n._d = new Date(1e3 * parseFloat(e, 10))
	}), ce("x", function (e, t, n) {
		n._d = new Date(k(e))
	}), c.version = "2.22.2", e = Tt, c.fn = ln, c.min = function () {
		return Pt("isBefore", [].slice.call(arguments, 0))
	}, c.max = function () {
		return Pt("isAfter", [].slice.call(arguments, 0))
	}, c.now = function () {
		return Date.now ? Date.now() : +new Date
	}, c.utc = y, c.unix = function (e) {
		return Tt(1e3 * e)
	}, c.months = function (e, t) {
		return fn(e, t, "months")
	}, c.isDate = h, c.locale = ot, c.invalid = v, c.duration = At, c.isMoment = S, c.weekdays = function (e, t, n) {
		return mn(e, t, n, "weekdays")
	}, c.parseZone = function () {
		return Tt.apply(null, arguments).parseZone()
	}, c.localeData = lt, c.isDuration = Rt, c.monthsShort = function (e, t) {
		return fn(e, t, "monthsShort")
	}, c.weekdaysMin = function (e, t, n) {
		return mn(e, t, n, "weekdaysMin")
	}, c.defineLocale = ut, c.updateLocale = function (e, t) {
		if (null != t) {
			var n, s, i = nt;
			null != (s = at(e)) && (i = s._config), (n = new P(t = b(i, t))).parentLocale = st[e], st[e] = n, ot(e)
		} else null != st[e] && (null != st[e].parentLocale ? st[e] = st[e].parentLocale : null != st[e] && delete st[e]);
		return st[e]
	}, c.locales = function () {
		return s(st)
	}, c.weekdaysShort = function (e, t, n) {
		return mn(e, t, n, "weekdaysShort")
	}, c.normalizeUnits = R, c.relativeTimeRounding = function (e) {
		return void 0 === e ? Un : "function" == typeof e && (Un = e, !0)
	}, c.relativeTimeThreshold = function (e, t) {
		return void 0 !== Nn[e] && (void 0 === t ? Nn[e] : (Nn[e] = t, "s" === e && (Nn.ss = t - 1), !0))
	}, c.calendarFormat = function (e, t) {
		var n = e.diff(t, "days", !0);
		return n < -6 ? "sameElse" : n < -1 ? "lastWeek" : n < 0 ? "lastDay" : n < 1 ? "sameDay" : n < 2 ? "nextDay" : n < 7 ? "nextWeek" : "sameElse"
	}, c.prototype = ln, c.HTML5_FMT = {
		DATETIME_LOCAL: "YYYY-MM-DDTHH:mm",
		DATETIME_LOCAL_SECONDS: "YYYY-MM-DDTHH:mm:ss",
		DATETIME_LOCAL_MS: "YYYY-MM-DDTHH:mm:ss.SSS",
		DATE: "YYYY-MM-DD",
		TIME: "HH:mm",
		TIME_SECONDS: "HH:mm:ss",
		TIME_MS: "HH:mm:ss.SSS",
		WEEK: "YYYY-[W]WW",
		MONTH: "YYYY-MM"
	}, c
});
/*!
 * Pikaday
 *
 * Copyright Â© 2014 David Bushell | BSD & MIT license | https://github.com/dbushell/Pikaday
 */
(function (n, r) {
	var q;
	if ("object" === typeof exports) {
		try {
			q = require("moment")
		} catch (k) {}
		module.exports = r(q)
	} else "function" === typeof define && define.amd ? define(function (k) {
		q = k.defined && k.defined("moment") ? k("moment") : void 0;
		return r(q)
	}) : n.Pikaday = r(n.moment)
})(this, function (n) {
	var r = "function" === typeof n,
		q = !!window.addEventListener,
		k = window.document,
		x = window.setTimeout,
		t = function (a, b, c, d) {
			q ? a.addEventListener(b, c, !!d) : a.attachEvent("on" + b, c)
		},
		v = function (a, b, c, d) {
			q ? a.removeEventListener(b, c, !!d) : a.detachEvent("on" + b, c)
		},
		h = function (a, b) {
			return -1 !== (" " + a.className + " ").indexOf(" " + b + " ")
		},
		z = function (a) {
			return /Array/.test(Object.prototype.toString.call(a))
		},
		m = function (a) {
			return /Date/.test(Object.prototype.toString.call(a)) && !isNaN(a.getTime())
		},
		y = function (a) {
			m(a) && a.setHours(0, 0, 0, 0)
		},
		w = function (a, b, c) {
			var d, e;
			for (d in b)
				if ((e = void 0 !== a[d]) && "object" === typeof b[d] && void 0 === b[d].nodeName) m(b[d]) ? c && (a[d] = new Date(b[d].getTime())) : z(b[d]) ? c && (a[d] = b[d].slice(0)) : a[d] = w({}, b[d], c);
				else if (c || !e) a[d] = b[d];
			return a
		},
		A = function (a) {
			0 > a.month && (a.year -= Math.ceil(Math.abs(a.month) / 12), a.month += 12);
			11 < a.month && (a.year += Math.floor(Math.abs(a.month) / 12), a.month -= 12);
			return a
		},
		B = {
			field: null,
			bound: void 0,
			position: "bottom left",
			format: "YYYY-MM-DD",
			defaultDate: null,
			setDefaultDate: !1,
			firstDay: 0,
			minDate: null,
			maxDate: null,
			yearRange: 10,
			showWeekNumber: !1,
			minYear: 0,
			maxYear: 9999,
			minMonth: void 0,
			maxMonth: void 0,
			isRTL: !1,
			yearSuffix: "",
			showMonthAfterYear: !1,
			numberOfMonths: 1,
			mainCalendar: "left",
			container: void 0,
			i18n: {
				previousMonth: "Previous Month",
				nextMonth: "Next Month",
				months: "January February March April May June July August September October November December".split(" "),
				weekdays: "Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "),
				weekdaysShort: "Sun Mon Tue Wed Thu Fri Sat".split(" ")
			},
			onSelect: null,
			onOpen: null,
			onClose: null,
			onDraw: null
		},
		C = function (a, b, c) {
			for (b += a.firstDay; 7 <= b;) b -= 7;
			return c ? a.i18n.weekdaysShort[b] : a.i18n.weekdays[b]
		},
		E = function (a, b, c, d, e, f, u) {
			if (u) return '<td class="is-empty"></td>';
			u = [];
			f && u.push("is-disabled");
			e && u.push("is-today");
			d && u.push("is-selected");
			return '<td data-day="' + a + '" class="' + u.join(" ") + '"><button class="pika-button pika-day" type="button" data-pika-year="' + c + '" data-pika-month="' + b + '" data-pika-day="' + a + '">' + a + "</button></td>"
		},
		F = function (a, b, c) {
			var d = new Date(c, 0, 1);
			return '<td class="pika-week">' + Math.ceil(((new Date(c, b, a) - d) / 864E5 + d.getDay() + 1) / 7) + "</td>"
		},
		G = function (a, b) {
			return "<tr>" + (b ? a.reverse() : a).join("") + "</tr>"
		},
		H = function (a, b, c, d, e) {
			var f, u, l, g = a._o,
				k = c === g.minYear,
				m = c === g.maxYear,
				p = '<div class="pika-title">',
				h = !0,
				n = !0;
			l = [];
			for (f = 0; 12 > f; f++) l.push('<option value="' + (c === e ? f - b : 12 + f - b) + '"' + (f === d ? " selected" : "") + (k && f < g.minMonth || m && f > g.maxMonth ? "disabled" : "") + ">" + g.i18n.months[f] + "</option>");
			e = '<div class="pika-label">' + g.i18n.months[d] + '<select class="pika-select pika-select-month">' + l.join("") + "</select></div>";
			z(g.yearRange) ? (f = g.yearRange[0], u = g.yearRange[1] + 1) : (f = c - g.yearRange, u = 1 + c + g.yearRange);
			for (l = []; f < u && f <= g.maxYear; f++) f >= g.minYear && l.push('<option value="' + f + '"' + (f === c ? " selected" : "") + ">" + f + "</option>");
			c = '<div class="pika-label">' + c + g.yearSuffix + '<select class="pika-select pika-select-year">' + l.join("") + "</select></div>";
			p = g.showMonthAfterYear ? p + (c + e) : p + (e + c);
			k && (0 === d || g.minMonth >= d) && (h = !1);
			m && (11 === d || g.maxMonth <= d) && (n = !1);
			0 === b && (p += '<button class="pika-prev' + (h ? "" : " is-disabled") + '" type="button">' + g.i18n.previousMonth + "</button>");
			b === a._o.numberOfMonths - 1 && (p += '<button class="pika-next' + (n ? "" : " is-disabled") + '" type="button">' + g.i18n.nextMonth + "</button>");
			return p + "</div>"
		},
		D = function (a) {
			var b = this,
				c = b.config(a);
			b._onMouseDown = function (a) {
				if (b._v) {
					a = a || window.event;
					var e = a.target || a.srcElement;
					if (e) {
						if (!h(e, "is-disabled")) {
							if (h(e, "pika-button") && !h(e, "is-empty")) {
								b.setDate(new Date(e.getAttribute("data-pika-year"), e.getAttribute("data-pika-month"), e.getAttribute("data-pika-day")));
								c.bound && x(function () {
									b.hide();
									c.field && c.field.blur()
								}, 100);
								return
							}
							h(e, "pika-prev") ? b.prevMonth() : h(e, "pika-next") && b.nextMonth()
						}
						if (h(e, "pika-select")) b._c = !0;
						else if (a.preventDefault) a.preventDefault();
						else return a.returnValue = !1
					}
				}
			};
			b._onChange = function (a) {
				a = a || window.event;
				(a = a.target || a.srcElement) && (h(a, "pika-select-month") ? b.gotoMonth(a.value) : h(a, "pika-select-year") && b.gotoYear(a.value))
			};
			b._onInputChange = function (a) {
				a.firedBy !== b && (a = r ? (a = n(c.field.value, c.format)) && a.isValid() ? a.toDate() : null : new Date(Date.parse(c.field.value)), b.setDate(m(a) ? a : null), b._v || b.show())
			};
			b._onInputFocus = function () {
				b.show()
			};
			b._onInputClick = function () {
				b.show()
			};
			b._onInputBlur = function () {
				b._c || (b._b = x(function () {
					b.hide()
				}, 50));
				b._c = !1
			};
			b._onClick = function (a) {
				a = a || window.event;
				var e = a = a.target || a.srcElement;
				if (a) {
					q || !h(a, "pika-select") || a.onchange || (a.setAttribute("onchange", "return;"), t(a, "change", b._onChange));
					do
						if (h(e, "pika-single")) return; while (e = e.parentNode);
					b._v && a !== c.trigger && b.hide()
				}
			};
			b.el = k.createElement("div");
			b.el.className = "pika-single" + (c.isRTL ? " is-rtl" : "");
			t(b.el, "mousedown", b._onMouseDown, !0);
			t(b.el, "change", b._onChange);
			c.field && (c.container ? c.container.appendChild(b.el) : c.bound ? k.body.appendChild(b.el) : c.field.parentNode.insertBefore(b.el, c.field.nextSibling), t(c.field, "change", b._onInputChange), c.defaultDate || (c.defaultDate = r && c.field.value ? n(c.field.value, c.format).toDate() : new Date(Date.parse(c.field.value)), c.setDefaultDate = !0));
			a = c.defaultDate;
			m(a) ? c.setDefaultDate ? b.setDate(a, !0) : b.gotoDate(a) : b.gotoDate(new Date);
			c.bound ? (this.hide(), b.el.className += " is-bound", t(c.trigger, "click", b._onInputClick), t(c.trigger, "focus", b._onInputFocus), t(c.trigger, "blur", b._onInputBlur)) : this.show()
		};
	D.prototype = {
		config: function (a) {
			this._o || (this._o = w({}, B, !0));
			a = w(this._o, a, !0);
			a.isRTL = !!a.isRTL;
			a.field = a.field && a.field.nodeName ? a.field : null;
			a.bound = !!(void 0 !== a.bound ? a.field && a.bound : a.field);
			a.trigger = a.trigger && a.trigger.nodeName ? a.trigger : a.field;
			var b = parseInt(a.numberOfMonths, 10) || 1;
			a.numberOfMonths = 4 < b ? 4 : b;
			m(a.minDate) || (a.minDate = !1);
			m(a.maxDate) || (a.maxDate = !1);
			a.minDate && a.maxDate && a.maxDate < a.minDate && (a.maxDate = a.minDate = !1);
			a.minDate && (y(a.minDate), a.minYear = a.minDate.getFullYear(), a.minMonth = a.minDate.getMonth());
			a.maxDate && (y(a.maxDate), a.maxYear = a.maxDate.getFullYear(), a.maxMonth = a.maxDate.getMonth());
			z(a.yearRange) ? (b = (new Date).getFullYear() - 10, a.yearRange[0] = parseInt(a.yearRange[0], 10) || b, a.yearRange[1] = parseInt(a.yearRange[1], 10) || b) : (a.yearRange = Math.abs(parseInt(a.yearRange, 10)) || B.yearRange, 100 < a.yearRange && (a.yearRange = 100));
			return a
		},
		toString: function (a) {
			return m(this._d) ? r ? n(this._d).format(a || this._o.format) : this._d.toDateString() : ""
		},
		getMoment: function () {
			return r ? n(this._d) : null
		},
		setMoment: function (a, b) {
			r && n.isMoment(a) && this.setDate(a.toDate(), b)
		},
		getDate: function () {
			return m(this._d) ? new Date(this._d.getTime()) : null
		},
		setDate: function (a, b) {
			if (!a) return this._d = null, this.draw();
			"string" === typeof a && (a = new Date(Date.parse(a)));
			if (m(a)) {
				var c = this._o.minDate,
					d = this._o.maxDate;
				m(c) && a < c ? a = c : m(d) && a > d && (a = d);
				this._d = new Date(a.getTime());
				y(this._d);
				this.gotoDate(this._d);
				if (this._o.field) {
					this._o.field.value = this.toString();
					var c = this._o.field,
						d = {
							firedBy: this
						},
						e;
					k.createEvent ? (e = k.createEvent("HTMLEvents"), e.initEvent("change", !0, !1), e = w(e, d), c.dispatchEvent(e)) : k.createEventObject && (e = k.createEventObject(), e = w(e, d), c.fireEvent("onchange", e))
				}
				b || "function" !== typeof this._o.onSelect || this._o.onSelect.call(this, this.getDate())
			}
		},
		gotoDate: function (a) {
			var b = !0;
			if (m(a)) {
				if (this.calendars) {
					var b = new Date(this.calendars[0].year, this.calendars[0].month, 1),
						c = new Date(this.calendars[this.calendars.length - 1].year, this.calendars[this.calendars.length - 1].month, 1),
						d = a.getTime();
					c.setMonth(c.getMonth() + 1);
					c.setDate(c.getDate() - 1);
					b = d < b.getTime() || c.getTime() < d
				}
				b && (this.calendars = [{
					month: a.getMonth(),
					year: a.getFullYear()
				}], "right" === this._o.mainCalendar && (this.calendars[0].month += 1 - this._o.numberOfMonths));
				this.adjustCalendars()
			}
		},
		adjustCalendars: function () {
			this.calendars[0] = A(this.calendars[0]);
			for (var a = 1; a < this._o.numberOfMonths; a++) this.calendars[a] = A({
				month: this.calendars[0].month + a,
				year: this.calendars[0].year
			});
			this.draw()
		},
		gotoToday: function () {
			this.gotoDate(new Date)
		},
		gotoMonth: function (a) {
			isNaN(a) || (this.calendars[0].month = parseInt(a, 10), this.adjustCalendars())
		},
		nextMonth: function () {
			this.calendars[0].month++;
			this.adjustCalendars()
		},
		prevMonth: function () {
			this.calendars[0].month--;
			this.adjustCalendars()
		},
		gotoYear: function (a) {
			isNaN(a) || (this.calendars[0].year = parseInt(a, 10), this.adjustCalendars())
		},
		setMinDate: function (a) {
			this._o.minDate = a
		},
		setMaxDate: function (a) {
			this._o.maxDate = a
		},
		draw: function (a) {
			if (this._v || a) {
				var b = this._o,
					c = b.minYear,
					d = b.maxYear,
					e = b.minMonth,
					f = b.maxMonth;
				a = "";
				this._y <= c && (this._y = c, !isNaN(e) && this._m < e && (this._m = e));
				this._y >= d && (this._y = d, !isNaN(f) && this._m > f && (this._m = f));
				for (c = 0; c < b.numberOfMonths; c++) a += '<div class="pika-lendar">' + H(this, c, this.calendars[c].year, this.calendars[c].month, this.calendars[0].year) + this.render(this.calendars[c].year, this.calendars[c].month) + "</div>";
				this.el.innerHTML = a;
				b.bound && "hidden" !== b.field.type && x(function () {
					b.trigger.focus()
				}, 1);
				if ("function" === typeof this._o.onDraw) {
					var k = this;
					x(function () {
						k._o.onDraw.call(k)
					}, 0)
				}
			}
		},
		adjustPosition: function () {
			if (!this._o.container) {
				var a = this._o.trigger,
					b = a,
					c = this.el.offsetWidth,
					d = this.el.offsetHeight,
					e = window.innerWidth || k.documentElement.clientWidth,
					f = window.innerHeight || k.documentElement.clientHeight,
					m = window.pageYOffset || k.body.scrollTop || k.documentElement.scrollTop,
					l, g;
				if ("function" === typeof a.getBoundingClientRect) b = a.getBoundingClientRect(), l = b.left + window.pageXOffset, g = b.bottom + window.pageYOffset;
				else
					for (l = b.offsetLeft, g = b.offsetTop + b.offsetHeight; b = b.offsetParent;) l += b.offsetLeft, g += b.offsetTop;
				if (l + c > e || -1 < this._o.position.indexOf("right") && 0 < l - c + a.offsetWidth) l = l - c + a.offsetWidth;
				if (g + d > f + m || -1 < this._o.position.indexOf("top") && 0 < g - d - a.offsetHeight) g = g - d - a.offsetHeight;
				this.el.style.cssText = ["position: absolute", "left: " + l + "px", "top: " + g + "px"].join(";")
			}
		},
		render: function (a, b) {
			var c = this._o,
				d = new Date,
				e;
			e = [31, 0 === a % 4 && 0 !== a % 100 || 0 === a % 400 ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][b];
			var f = (new Date(a, b, 1)).getDay(),
				k = [],
				l = [];
			y(d);
			0 < c.firstDay && (f -= c.firstDay, 0 > f && (f += 7));
			for (var g = e + f, h = g; 7 < h;) h -= 7;
			for (var g = g + (7 - h), n = h = 0; h < g; h++) {
				var p = new Date(a, b, 1 + (h - f)),
					r = c.minDate && p < c.minDate || c.maxDate && p > c.maxDate,
					q;
				m(this._d) ? (q = this._d, q = p.getTime() === q.getTime()) : q = !1;
				var t = d,
					p = p.getTime() === t.getTime();
				l.push(E(1 + (h - f), b, a, q, p, r, h < f || h >= e + f));
				7 === ++n && (c.showWeekNumber && l.unshift(F(h - f, b, a)), k.push(G(l, c.isRTL)), l = [], n = 0)
			}
			e = [];
			c.showWeekNumber && e.push("<th></th>");
			for (d = 0; 7 > d; d++) e.push('<th scope="col"><abbr title="' + C(c, d) + '">' + C(c, d, !0) + "</abbr></th>");
			return '<table cellpadding="0" cellspacing="0" class="pika-table">' + ("<thead>" + (c.isRTL ? e.reverse() : e).join("") + "</thead>") + ("<tbody>" + k.join("") + "</tbody>") + "</table>"
		},
		isVisible: function () {
			return this._v
		},
		show: function () {
			if (!this._v) {
				var a = this.el,
					b;
				b = (" " + a.className + " ").replace(" is-hidden ", " ");
				b = b.trim ? b.trim() : b.replace(/^\s+|\s+$/g, "");
				a.className = b;
				this._v = !0;
				this.draw();
				this._o.bound && (t(k, "click", this._onClick), this.adjustPosition());
				"function" === typeof this._o.onOpen && this._o.onOpen.call(this)
			}
		},
		hide: function () {
			var a = this._v;
			if (!1 !== a) {
				this._o.bound && v(k, "click", this._onClick);
				this.el.style.cssText = "";
				var b = this.el;
				h(b, "is-hidden") || (b.className = "" === b.className ? "is-hidden" : b.className + " is-hidden");
				this._v = !1;
				void 0 !== a && "function" === typeof this._o.onClose && this._o.onClose.call(this)
			}
		},
		destroy: function () {
			this.hide();
			v(this.el, "mousedown", this._onMouseDown, !0);
			v(this.el, "change", this._onChange);
			this._o.field && (v(this._o.field, "change", this._onInputChange), this._o.bound && (v(this._o.trigger, "click", this._onInputClick), v(this._o.trigger, "focus", this._onInputFocus), v(this._o.trigger, "blur", this._onInputBlur)));
			this.el.parentNode && this.el.parentNode.removeChild(this.el)
		}
	};
	return D
});
(function (c) {
	var a = c(window),
		k = a.height();
	a.resize(function () {
		k = a.height()
	});
	c.fn.parallax = function (e, f, b) {
		function g() {
			var h = a.scrollTop();
			d.each(function () {
				var a = c(this),
					b = a.offset().top,
					a = l(a);
				b + a < h || b > h + k || d.css("backgroundPosition", e + " " + Math.round((m - h) * f) + "px")
			})
		}
		var d = c(this),
			l, m;
		d.each(function () {
			m = d.offset().top
		});
		l = b ? function (a) {
			return a.outerHeight(!0)
		} : function (a) {
			return a.height()
		};
		if (1 > arguments.length || null === e) e = "50%";
		if (2 > arguments.length || null === f) f = .1;
		if (3 > arguments.length || null === b) b = !0;
		a.bind("scroll", g).resize(g);
		g()
	}
})(jQuery);
/*!
 * parallax.js v1.3.1 (http://pixelcog.github.io/parallax.js/)
 * @copyright 2015 PixelCog, Inc.
 * @license MIT (https://github.com/pixelcog/parallax.js/blob/master/LICENSE)
 */
(function (d, g, h, k) {
	function b(a, c) {
		var f = this;
		"object" == typeof c && (delete c.refresh, delete c.render, d.extend(this, c));
		this.$element = d(a);
		!this.imageSrc && this.$element.is("img") && (this.imageSrc = this.$element.attr("src"));
		var e = (this.position + "").toLowerCase().match(/\S+/g) || [];
		1 > e.length && e.push("center");
		1 == e.length && e.push(e[0]);
		if ("top" == e[0] || "bottom" == e[0] || "left" == e[1] || "right" == e[1]) e = [e[1], e[0]];
		this.positionX != k && (e[0] = this.positionX.toLowerCase());
		this.positionY != k && (e[1] = this.positionY.toLowerCase());
		f.positionX = e[0];
		f.positionY = e[1];
		"left" != this.positionX && "right" != this.positionX && (isNaN(parseInt(this.positionX)) ? this.positionX = "center" : this.positionX = parseInt(this.positionX));
		"top" != this.positionY && "bottom" != this.positionY && (isNaN(parseInt(this.positionY)) ? this.positionY = "center" : this.positionY = parseInt(this.positionY));
		this.position = this.positionX + (isNaN(this.positionX) ? "" : "px") + " " + this.positionY + (isNaN(this.positionY) ? "" : "px");
		if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) return this.iosFix && !this.$element.is("img") && this.$element.css({
			backgroundImage: "url(" + this.imageSrc + ")",
			backgroundSize: "cover",
			backgroundPosition: this.position
		}), this;
		if (navigator.userAgent.match(/(Android)/)) return this.androidFix && !this.$element.is("img") && this.$element.css({
			backgroundImage: "url(" + this.imageSrc + ")",
			backgroundSize: "cover",
			backgroundPosition: this.position
		}), this;
		this.$mirror = d("<div />").prependTo("body");
		this.$slider = d("<img />").prependTo(this.$mirror);
		this.$mirror.addClass("parallax_images-mirror").css({
			visibility: "hidden",
			zIndex: this.zIndex,
			position: "fixed",
			top: 0,
			left: 0,
			overflow: "hidden"
		});
		this.$slider.addClass("parallax_images-slider").one("load", function () {
			f.naturalHeight && f.naturalWidth || (f.naturalHeight = this.naturalHeight || this.height || 1, f.naturalWidth = this.naturalWidth || this.width || 1);
			f.aspectRatio = f.naturalWidth / f.naturalHeight;
			b.isSetup || b.setup();
			b.sliders.push(f);
			b.isFresh = !1;
			b.requestRender()
		});
		this.$slider[0].src = this.imageSrc;
		(this.naturalHeight && this.naturalWidth || this.$slider[0].complete) && this.$slider.trigger("load")
	}(function () {
		for (var a = 0, c = ["ms", "moz", "webkit", "o"], b = 0; b < c.length && !g.requestAnimationFrame; ++b) g.requestAnimationFrame = g[c[b] + "RequestAnimationFrame"], g.cancelAnimationFrame = g[c[b] + "CancelAnimationFrame"] || g[c[b] + "CancelRequestAnimationFrame"];
		g.requestAnimationFrame || (g.requestAnimationFrame = function (b) {
			var c = (new Date).getTime(),
				d = Math.max(0, 16 - (c - a)),
				f = g.setTimeout(function () {
					b(c + d)
				}, d);
			a = c + d;
			return f
		});
		g.cancelAnimationFrame || (g.cancelAnimationFrame = function (a) {
			clearTimeout(a)
		})
	})();
	d.extend(b.prototype, {
		speed: .2,
		bleed: 0,
		zIndex: -100,
		iosFix: !0,
		androidFix: !0,
		position: "center",
		overScrollFix: !1,
		refresh: function () {
			this.boxWidth = this.$element.outerWidth();
			this.boxHeight = this.$element.outerHeight() + 2 * this.bleed;
			this.boxOffsetTop = this.$element.offset().top - this.bleed;
			this.boxOffsetLeft = this.$element.offset().left;
			this.boxOffsetBottom = this.boxOffsetTop + this.boxHeight;
			var a = b.winHeight,
				c = Math.min(this.boxOffsetTop, b.docHeight - a),
				a = Math.max(this.boxOffsetTop + this.boxHeight - a, 0),
				a = this.boxHeight + (c - a) * (1 - this.speed) | 0,
				c = (this.boxOffsetTop - c) * (1 - this.speed) | 0;
			a * this.aspectRatio >= this.boxWidth ? (this.imageWidth = a * this.aspectRatio | 0, this.imageHeight = a, this.offsetBaseTop = c, a = this.imageWidth - this.boxWidth, "left" == this.positionX ? this.offsetLeft = 0 : "right" == this.positionX ? this.offsetLeft = -a : isNaN(this.positionX) ? this.offsetLeft = -a / 2 | 0 : this.offsetLeft = Math.max(this.positionX, -a)) : (this.imageWidth = this.boxWidth, this.imageHeight = this.boxWidth / this.aspectRatio | 0, this.offsetLeft = 0, a = this.imageHeight - a, "top" == this.positionY ? this.offsetBaseTop = c : "bottom" == this.positionY ? this.offsetBaseTop = c - a : isNaN(this.positionY) ? this.offsetBaseTop = c - a / 2 | 0 : this.offsetBaseTop = c + Math.max(this.positionY, -a))
		},
		render: function () {
			var a = b.scrollTop,
				c = b.scrollLeft,
				d = this.overScrollFix ? b.overScroll : 0,
				e = a + b.winHeight;
			this.visibility = this.boxOffsetBottom > a && this.boxOffsetTop < e ? "visible" : "hidden";
			this.mirrorTop = this.boxOffsetTop - a;
			this.mirrorLeft = this.boxOffsetLeft - c;
			this.offsetTop = this.offsetBaseTop - this.mirrorTop * (1 - this.speed);
			this.$mirror.css({
				transform: "translate3d(0px, 0px, 0px)",
				visibility: this.visibility,
				top: this.mirrorTop - d,
				left: this.mirrorLeft,
				height: this.boxHeight,
				width: this.boxWidth
			});
			this.$slider.css({
				transform: "translate3d(0px, 0px, 0px)",
				position: "absolute",
				top: this.offsetTop,
				left: this.offsetLeft,
				height: this.imageHeight,
				width: this.imageWidth,
				maxWidth: "none"
			})
		}
	});
	d.extend(b, {
		scrollTop: 0,
		scrollLeft: 0,
		winHeight: 0,
		winWidth: 0,
		docHeight: 1073741824,
		docWidth: 1073741824,
		sliders: [],
		isReady: !1,
		isFresh: !1,
		isBusy: !1,
		setup: function () {
			if (!this.isReady) {
				var a = d(h),
					c = d(g);
				c.on("resize.px.parallax_images load.px.parallax_images", function () {
					b.winHeight = c.height();
					b.winWidth = c.width();
					b.docHeight = a.height();
					b.docWidth = a.width();
					b.isFresh = !1;
					b.requestRender()
				}).on("scroll.px.parallax_images load.px.parallax_images", function () {
					var a = b.docHeight - b.winHeight,
						d = b.docWidth - b.winWidth;
					b.scrollTop = Math.max(0, Math.min(a, c.scrollTop()));
					b.scrollLeft = Math.max(0, Math.min(d, c.scrollLeft()));
					b.overScroll = Math.max(c.scrollTop() - a, Math.min(c.scrollTop(), 0));
					b.requestRender()
				});
				this.isReady = !0
			}
		},
		configure: function (a) {
			"object" == typeof a && (delete a.refresh, delete a.render, d.extend(this.prototype, a))
		},
		refresh: function () {
			d.each(this.sliders, function () {
				this.refresh()
			});
			this.isFresh = !0
		},
		render: function () {
			this.isFresh || this.refresh();
			d.each(this.sliders, function () {
				this.render()
			})
		},
		requestRender: function () {
			var a = this;
			this.isBusy || (this.isBusy = !0, g.requestAnimationFrame(function () {
				a.render();
				a.isBusy = !1
			}))
		}
	});
	var l = d.fn.parallax_images;
	d.fn.parallax_images = function (a) {
		return this.each(function () {
			var c = d(this),
				f = "object" == typeof a && a;
			this == g || this == h || c.is("body") ? b.configure(f) : c.data("px.parallax_images") || (f = d.extend({}, c.data(), f), c.data("px.parallax_images", new b(this, f)));
			if ("string" == typeof a) b[a]()
		})
	};
	d.fn.parallax_images.Constructor = b;
	d.fn.parallax_images.noConflict = function () {
		d.fn.parallax_images = l;
		return this
	};
	d(h).on("ready.px.parallax_images.data-api", function () {
		d('[data-parallax_images="scroll"]').parallax_images()
	})
})(jQuery, window, document);;
window.Modernizr = function (a, b, c) {
		function D(a) {
			j.cssText = a
		}

		function E(a, b) {
			return D(n.join(a + ";") + (b || ""))
		}

		function F(a, b) {
			return typeof a === b
		}

		function G(a, b) {
			return !!~("" + a).indexOf(b)
		}

		function H(a, b) {
			for (var d in a) {
				var e = a[d];
				if (!G(e, "-") && j[e] !== c) return b == "pfx" ? e : !0
			}
			return !1
		}

		function I(a, b, d) {
			for (var e in a) {
				var f = b[a[e]];
				if (f !== c) return d === !1 ? a[e] : F(f, "function") ? f.bind(d || b) : f
			}
			return !1
		}

		function J(a, b, c) {
			var d = a.charAt(0).toUpperCase() + a.slice(1),
				e = (a + " " + p.join(d + " ") + d).split(" ");
			return F(b, "string") || F(b, "undefined") ? H(e, b) : (e = (a + " " + q.join(d + " ") + d).split(" "), I(e, b, c))
		}

		function K() {
			e.input = function (c) {
				for (var d = 0, e = c.length; d < e; d++) u[c[d]] = c[d] in k;
				return u.list && (u.list = !!b.createElement("datalist") && !!a.HTMLDataListElement), u
			}("autocomplete autofocus list placeholder max min multiple pattern required step".split(" ")), e.inputtypes = function (a) {
				for (var d = 0, e, f, h, i = a.length; d < i; d++) k.setAttribute("type", f = a[d]), e = k.type !== "text", e && (k.value = l, k.style.cssText = "position:absolute;visibility:hidden;", /^range$/.test(f) && k.style.WebkitAppearance !== c ? (g.appendChild(k), h = b.defaultView, e = h.getComputedStyle && h.getComputedStyle(k, null).WebkitAppearance !== "textfield" && k.offsetHeight !== 0, g.removeChild(k)) : /^(search|tel)$/.test(f) || (/^(url|email)$/.test(f) ? e = k.checkValidity && k.checkValidity() === !1 : e = k.value != l)), t[a[d]] = !!e;
				return t
			}("search tel url email datetime date month week time datetime-local number range color".split(" "))
		}
		var d = "2.8.2",
			e = {},
			f = !0,
			g = b.documentElement,
			h = "modernizr",
			i = b.createElement(h),
			j = i.style,
			k = b.createElement("input"),
			l = ":)",
			m = {}.toString,
			n = " -webkit- -moz- -o- -ms- ".split(" "),
			o = "Webkit Moz O ms",
			p = o.split(" "),
			q = o.toLowerCase().split(" "),
			r = {
				svg: "http://www.w3.org/2000/svg"
			},
			s = {},
			t = {},
			u = {},
			v = [],
			w = v.slice,
			x, y = function (a, c, d, e) {
				var f, i, j, k, l = b.createElement("div"),
					m = b.body,
					n = m || b.createElement("body");
				if (parseInt(d, 10))
					while (d--) j = b.createElement("div"), j.id = e ? e[d] : h + (d + 1), l.appendChild(j);
				return f = ["&#173;", '<style id="s', h, '">', a, "</style>"].join(""), l.id = h, (m ? l : n).innerHTML += f, n.appendChild(l), m || (n.style.background = "", n.style.overflow = "hidden", k = g.style.overflow, g.style.overflow = "hidden", g.appendChild(n)), i = c(l, a), m ? l.parentNode.removeChild(l) : (n.parentNode.removeChild(n), g.style.overflow = k), !!i
			},
			z = function (b) {
				var c = a.matchMedia || a.msMatchMedia;
				if (c) return c(b) && c(b).matches || !1;
				var d;
				return y("@media " + b + " { #" + h + " { position: absolute; } }", function (b) {
					d = (a.getComputedStyle ? getComputedStyle(b, null) : b.currentStyle)["position"] == "absolute"
				}), d
			},
			A = function () {
				function d(d, e) {
					e = e || b.createElement(a[d] || "div"), d = "on" + d;
					var f = d in e;
					return f || (e.setAttribute || (e = b.createElement("div")), e.setAttribute && e.removeAttribute && (e.setAttribute(d, ""), f = F(e[d], "function"), F(e[d], "undefined") || (e[d] = c), e.removeAttribute(d))), e = null, f
				}
				var a = {
					select: "input",
					change: "input",
					submit: "form",
					reset: "form",
					error: "img",
					load: "img",
					abort: "img"
				};
				return d
			}(),
			B = {}.hasOwnProperty,
			C;
		!F(B, "undefined") && !F(B.call, "undefined") ? C = function (a, b) {
			return B.call(a, b)
		} : C = function (a, b) {
			return b in a && F(a.constructor.prototype[b], "undefined")
		}, Function.prototype.bind || (Function.prototype.bind = function (b) {
			var c = this;
			if (typeof c != "function") throw new TypeError;
			var d = w.call(arguments, 1),
				e = function () {
					if (this instanceof e) {
						var a = function () {};
						a.prototype = c.prototype;
						var f = new a,
							g = c.apply(f, d.concat(w.call(arguments)));
						return Object(g) === g ? g : f
					}
					return c.apply(b, d.concat(w.call(arguments)))
				};
			return e
		}), s.flexbox = function () {
			return J("flexWrap")
		}, s.canvas = function () {
			var a = b.createElement("canvas");
			return !!a.getContext && !!a.getContext("2d")
		}, s.canvastext = function () {
			return !!e.canvas && !!F(b.createElement("canvas").getContext("2d").fillText, "function")
		}, s.webgl = function () {
			return !!a.WebGLRenderingContext
		}, s.touch = function () {
			var c;
			return "ontouchstart" in a || a.DocumentTouch && b instanceof DocumentTouch ? c = !0 : y(["@media (", n.join("touch-enabled),("), h, ")", "{#modernizr{top:9px;position:absolute}}"].join(""), function (a) {
				c = a.offsetTop === 9
			}), c
		}, s.geolocation = function () {
			return "geolocation" in navigator
		}, s.postmessage = function () {
			return !!a.postMessage
		}, s.websqldatabase = function () {
			return !!a.openDatabase
		}, s.indexedDB = function () {
			return !!J("indexedDB", a)
		}, s.hashchange = function () {
			return A("hashchange", a) && (b.documentMode === c || b.documentMode > 7)
		}, s.history = function () {
			return !!a.history && !!history.pushState
		}, s.draganddrop = function () {
			var a = b.createElement("div");
			return "draggable" in a || "ondragstart" in a && "ondrop" in a
		}, s.websockets = function () {
			return "WebSocket" in a || "MozWebSocket" in a
		}, s.rgba = function () {
			return D("background-color:rgba(150,255,150,.5)"), G(j.backgroundColor, "rgba")
		}, s.hsla = function () {
			return D("background-color:hsla(120,40%,100%,.5)"), G(j.backgroundColor, "rgba") || G(j.backgroundColor, "hsla")
		}, s.multiplebgs = function () {
			return D("background:url(https://),url(https://),red url(https://)"), /(url\s*\(.*?){3}/.test(j.background)
		}, s.backgroundsize = function () {
			return J("backgroundSize")
		}, s.borderimage = function () {
			return J("borderImage")
		}, s.borderradius = function () {
			return J("borderRadius")
		}, s.boxshadow = function () {
			return J("boxShadow")
		}, s.textshadow = function () {
			return b.createElement("div").style.textShadow === ""
		}, s.opacity = function () {
			return E("opacity:.55"), /^0.55$/.test(j.opacity)
		}, s.cssanimations = function () {
			return J("animationName")
		}, s.csscolumns = function () {
			return J("columnCount")
		}, s.cssgradients = function () {
			var a = "background-image:",
				b = "gradient(linear,left top,right bottom,from(#9f9),to(white));",
				c = "linear-gradient(left top,#9f9, white);";
			return D((a + "-webkit- ".split(" ").join(b + a) + n.join(c + a)).slice(0, -a.length)), G(j.backgroundImage, "gradient")
		}, s.cssreflections = function () {
			return J("boxReflect")
		}, s.csstransforms = function () {
			return !!J("transform")
		}, s.csstransforms3d = function () {
			var a = !!J("perspective");
			return a && "webkitPerspective" in g.style && y("@media (transform-3d),(-webkit-transform-3d){#modernizr{left:9px;position:absolute;height:3px;}}", function (b, c) {
				a = b.offsetLeft === 9 && b.offsetHeight === 3
			}), a
		}, s.csstransitions = function () {
			return J("transition")
		}, s.fontface = function () {
			var a;
			return y('@font-face {font-family:"font";src:url("https://")}', function (c, d) {
				var e = b.getElementById("smodernizr"),
					f = e.sheet || e.styleSheet,
					g = f ? f.cssRules && f.cssRules[0] ? f.cssRules[0].cssText : f.cssText || "" : "";
				a = /src/i.test(g) && g.indexOf(d.split(" ")[0]) === 0
			}), a
		}, s.generatedcontent = function () {
			var a;
			return y(["#", h, "{font:0/0 a}#", h, ':after{content:"', l, '";visibility:hidden;font:3px/1 a}'].join(""), function (b) {
				a = b.offsetHeight >= 3
			}), a
		}, s.video = function () {
			var a = b.createElement("video"),
				c = !1;
			try {
				if (c = !!a.canPlayType) c = new Boolean(c), c.ogg = a.canPlayType('video/ogg; codecs="theora"').replace(/^no$/, ""), c.h264 = a.canPlayType('video/mp4; codecs="avc1.42E01E"').replace(/^no$/, ""), c.webm = a.canPlayType('video/webm; codecs="vp8, vorbis"').replace(/^no$/, "")
			} catch (d) {}
			return c
		}, s.audio = function () {
			var a = b.createElement("audio"),
				c = !1;
			try {
				if (c = !!a.canPlayType) c = new Boolean(c), c.ogg = a.canPlayType('audio/ogg; codecs="vorbis"').replace(/^no$/, ""), c.mp3 = a.canPlayType("audio/mpeg;").replace(/^no$/, ""), c.wav = a.canPlayType('audio/wav; codecs="1"').replace(/^no$/, ""), c.m4a = (a.canPlayType("audio/x-m4a;") || a.canPlayType("audio/aac;")).replace(/^no$/, "")
			} catch (d) {}
			return c
		}, s.localstorage = function () {
			try {
				return localStorage.setItem(h, h), localStorage.removeItem(h), !0
			} catch (a) {
				return !1
			}
		}, s.sessionstorage = function () {
			try {
				return sessionStorage.setItem(h, h), sessionStorage.removeItem(h), !0
			} catch (a) {
				return !1
			}
		}, s.webworkers = function () {
			return !!a.Worker
		}, s.applicationcache = function () {
			return !!a.applicationCache
		}, s.svg = function () {
			return !!b.createElementNS && !!b.createElementNS(r.svg, "svg").createSVGRect
		}, s.inlinesvg = function () {
			var a = b.createElement("div");
			return a.innerHTML = "<svg/>", (a.firstChild && a.firstChild.namespaceURI) == r.svg
		}, s.smil = function () {
			return !!b.createElementNS && /SVGAnimate/.test(m.call(b.createElementNS(r.svg, "animate")))
		}, s.svgclippaths = function () {
			return !!b.createElementNS && /SVGClipPath/.test(m.call(b.createElementNS(r.svg, "clipPath")))
		};
		for (var L in s) C(s, L) && (x = L.toLowerCase(), e[x] = s[L](), v.push((e[x] ? "" : "no-") + x));
		return e.input || K(), e.addTest = function (a, b) {
				if (typeof a == "object")
					for (var d in a) C(a, d) && e.addTest(d, a[d]);
				else {
					a = a.toLowerCase();
					if (e[a] !== c) return e;
					b = typeof b == "function" ? b() : b, typeof f != "undefined" && f && (g.className += " " + (b ? "" : "no-") + a), e[a] = b
				}
				return e
			}, D(""), i = k = null,
			function (a, b) {
				function l(a, b) {
					var c = a.createElement("p"),
						d = a.getElementsByTagName("head")[0] || a.documentElement;
					return c.innerHTML = "x<style>" + b + "</style>", d.insertBefore(c.lastChild, d.firstChild)
				}

				function m() {
					var a = s.elements;
					return typeof a == "string" ? a.split(" ") : a
				}

				function n(a) {
					var b = j[a[h]];
					return b || (b = {}, i++, a[h] = i, j[i] = b), b
				}

				function o(a, c, d) {
					c || (c = b);
					if (k) return c.createElement(a);
					d || (d = n(c));
					var g;
					return d.cache[a] ? g = d.cache[a].cloneNode() : f.test(a) ? g = (d.cache[a] = d.createElem(a)).cloneNode() : g = d.createElem(a), g.canHaveChildren && !e.test(a) && !g.tagUrn ? d.frag.appendChild(g) : g
				}

				function p(a, c) {
					a || (a = b);
					if (k) return a.createDocumentFragment();
					c = c || n(a);
					var d = c.frag.cloneNode(),
						e = 0,
						f = m(),
						g = f.length;
					for (; e < g; e++) d.createElement(f[e]);
					return d
				}

				function q(a, b) {
					b.cache || (b.cache = {}, b.createElem = a.createElement, b.createFrag = a.createDocumentFragment, b.frag = b.createFrag()), a.createElement = function (c) {
						return s.shivMethods ? o(c, a, b) : b.createElem(c)
					}, a.createDocumentFragment = Function("h,f", "return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&&(" + m().join().replace(/[\w\-]+/g, function (a) {
						return b.createElem(a), b.frag.createElement(a), 'c("' + a + '")'
					}) + ");return n}")(s, b.frag)
				}

				function r(a) {
					a || (a = b);
					var c = n(a);
					return s.shivCSS && !g && !c.hasCSS && (c.hasCSS = !!l(a, "article,aside,dialog,figcaption,figure,footer,header,hgroup,main,nav,section{display:block}mark{background:#FF0;color:#000}template{display:none}")), k || q(a, c), a
				}
				var c = "3.7.0",
					d = a.html5 || {},
					e = /^<|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i,
					f = /^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i,
					g, h = "_html5shiv",
					i = 0,
					j = {},
					k;
				(function () {
					try {
						var a = b.createElement("a");
						a.innerHTML = "<xyz></xyz>", g = "hidden" in a, k = a.childNodes.length == 1 || function () {
							b.createElement("a");
							var a = b.createDocumentFragment();
							return typeof a.cloneNode == "undefined" || typeof a.createDocumentFragment == "undefined" || typeof a.createElement == "undefined"
						}()
					} catch (c) {
						g = !0, k = !0
					}
				})();
				var s = {
					elements: d.elements || "abbr article aside audio bdi canvas data datalist details dialog figcaption figure footer header hgroup main mark meter nav output progress section summary template time video",
					version: c,
					shivCSS: d.shivCSS !== !1,
					supportsUnknownElements: k,
					shivMethods: d.shivMethods !== !1,
					type: "default",
					shivDocument: r,
					createElement: o,
					createDocumentFragment: p
				};
				a.html5 = s, r(b)
			}(this, b), e._version = d, e._prefixes = n, e._domPrefixes = q, e._cssomPrefixes = p, e.mq = z, e.hasEvent = A, e.testProp = function (a) {
				return H([a])
			}, e.testAllProps = J, e.testStyles = y, e.prefixed = function (a, b, c) {
				return b ? J(a, b, c) : J(a, "pfx")
			}, g.className = g.className.replace(/(^|\s)no-js(\s|$)/, "$1$2") + (f ? " js " + v.join(" ") : ""), e
	}(this, this.document),
	function (a, b, c) {
		function d(a) {
			return "[object Function]" == o.call(a)
		}

		function e(a) {
			return "string" == typeof a
		}

		function f() {}

		function g(a) {
			return !a || "loaded" == a || "complete" == a || "uninitialized" == a
		}

		function h() {
			var a = p.shift();
			q = 1, a ? a.t ? m(function () {
				("c" == a.t ? B.injectCss : B.injectJs)(a.s, 0, a.a, a.x, a.e, 1)
			}, 0) : (a(), h()) : q = 0
		}

		function i(a, c, d, e, f, i, j) {
			function k(b) {
				if (!o && g(l.readyState) && (u.r = o = 1, !q && h(), l.onload = l.onreadystatechange = null, b)) {
					"img" != a && m(function () {
						t.removeChild(l)
					}, 50);
					for (var d in y[c]) y[c].hasOwnProperty(d) && y[c][d].onload()
				}
			}
			var j = j || B.errorTimeout,
				l = b.createElement(a),
				o = 0,
				r = 0,
				u = {
					t: d,
					s: c,
					e: f,
					a: i,
					x: j
				};
			1 === y[c] && (r = 1, y[c] = []), "object" == a ? l.data = c : (l.src = c, l.type = a), l.width = l.height = "0", l.onerror = l.onload = l.onreadystatechange = function () {
				k.call(this, r)
			}, p.splice(e, 0, u), "img" != a && (r || 2 === y[c] ? (t.insertBefore(l, s ? null : n), m(k, j)) : y[c].push(l))
		}

		function j(a, b, c, d, f) {
			return q = 0, b = b || "j", e(a) ? i("c" == b ? v : u, a, b, this.i++, c, d, f) : (p.splice(this.i++, 0, a), 1 == p.length && h()), this
		}

		function k() {
			var a = B;
			return a.loader = {
				load: j,
				i: 0
			}, a
		}
		var l = b.documentElement,
			m = a.setTimeout,
			n = b.getElementsByTagName("script")[0],
			o = {}.toString,
			p = [],
			q = 0,
			r = "MozAppearance" in l.style,
			s = r && !!b.createRange().compareNode,
			t = s ? l : n.parentNode,
			l = a.opera && "[object Opera]" == o.call(a.opera),
			l = !!b.attachEvent && !l,
			u = r ? "object" : l ? "script" : "img",
			v = l ? "script" : u,
			w = Array.isArray || function (a) {
				return "[object Array]" == o.call(a)
			},
			x = [],
			y = {},
			z = {
				timeout: function (a, b) {
					return b.length && (a.timeout = b[0]), a
				}
			},
			A, B;
		B = function (a) {
			function b(a) {
				var a = a.split("!"),
					b = x.length,
					c = a.pop(),
					d = a.length,
					c = {
						url: c,
						origUrl: c,
						prefixes: a
					},
					e, f, g;
				for (f = 0; f < d; f++) g = a[f].split("="), (e = z[g.shift()]) && (c = e(c, g));
				for (f = 0; f < b; f++) c = x[f](c);
				return c
			}

			function g(a, e, f, g, h) {
				var i = b(a),
					j = i.autoCallback;
				i.url.split(".").pop().split("?").shift(), i.bypass || (e && (e = d(e) ? e : e[a] || e[g] || e[a.split("/").pop().split("?")[0]]), i.instead ? i.instead(a, e, f, g, h) : (y[i.url] ? i.noexec = !0 : y[i.url] = 1, f.load(i.url, i.forceCSS || !i.forceJS && "css" == i.url.split(".").pop().split("?").shift() ? "c" : c, i.noexec, i.attrs, i.timeout), (d(e) || d(j)) && f.load(function () {
					k(), e && e(i.origUrl, h, g), j && j(i.origUrl, h, g), y[i.url] = 2
				})))
			}

			function h(a, b) {
				function c(a, c) {
					if (a) {
						if (e(a)) c || (j = function () {
							var a = [].slice.call(arguments);
							k.apply(this, a), l()
						}), g(a, j, b, 0, h);
						else if (Object(a) === a)
							for (n in m = function () {
									var b = 0,
										c;
									for (c in a) a.hasOwnProperty(c) && b++;
									return b
								}(), a) a.hasOwnProperty(n) && (!c && !--m && (d(j) ? j = function () {
								var a = [].slice.call(arguments);
								k.apply(this, a), l()
							} : j[n] = function (a) {
								return function () {
									var b = [].slice.call(arguments);
									a && a.apply(this, b), l()
								}
							}(k[n])), g(a[n], j, b, n, h))
					} else !c && l()
				}
				var h = !!a.test,
					i = a.load || a.both,
					j = a.callback || f,
					k = j,
					l = a.complete || f,
					m, n;
				c(h ? a.yep : a.nope, !!i), i && c(i)
			}
			var i, j, l = this.yepnope.loader;
			if (e(a)) g(a, 0, l, 0);
			else if (w(a))
				for (i = 0; i < a.length; i++) j = a[i], e(j) ? g(j, 0, l, 0) : w(j) ? B(j) : Object(j) === j && h(j, l);
			else Object(a) === a && h(a, l)
		}, B.addPrefix = function (a, b) {
			z[a] = b
		}, B.addFilter = function (a) {
			x.push(a)
		}, B.errorTimeout = 1e4, null == b.readyState && b.addEventListener && (b.readyState = "loading", b.addEventListener("DOMContentLoaded", A = function () {
			b.removeEventListener("DOMContentLoaded", A, 0), b.readyState = "complete"
		}, 0)), a.yepnope = k(), a.yepnope.executeStack = h, a.yepnope.injectJs = function (a, c, d, e, i, j) {
			var k = b.createElement("script"),
				l, o, e = e || B.errorTimeout;
			k.src = a;
			for (o in d) k.setAttribute(o, d[o]);
			c = j ? h : c || f, k.onreadystatechange = k.onload = function () {
				!l && g(k.readyState) && (l = 1, c(), k.onload = k.onreadystatechange = null)
			}, m(function () {
				l || (l = 1, c(1))
			}, e), i ? k.onload() : n.parentNode.insertBefore(k, n)
		}, a.yepnope.injectCss = function (a, c, d, e, g, i) {
			var e = b.createElement("link"),
				j, c = i ? h : c || f;
			e.href = a, e.rel = "stylesheet", e.type = "text/css";
			for (j in d) e.setAttribute(j, d[j]);
			g || (n.parentNode.insertBefore(e, n), m(c, 0))
		}
	}(this, document), Modernizr.load = function () {
		yepnope.apply(window, [].slice.call(arguments, 0))
	};
(window._gsQueue || (window._gsQueue = [])).push(function () {
	"use strict";
	window._gsDefine("easing.Back", ["easing.Ease"], function (t) {
		var e, i, s, r = window.GreenSockGlobals || window,
			n = r.com.greensock,
			a = 2 * Math.PI,
			o = Math.PI / 2,
			h = n._class,
			l = function (e, i) {
				var s = h("easing." + e, function () {}, !0),
					r = s.prototype = new t;
				return r.constructor = s, r.getRatio = i, s
			},
			_ = t.register || function () {},
			u = function (t, e, i, s) {
				var r = h("easing." + t, {
					easeOut: new e,
					easeIn: new i,
					easeInOut: new s
				}, !0);
				return _(r, t), r
			},
			c = function (t, e, i) {
				this.t = t, this.v = e, i && (this.next = i, i.prev = this, this.c = i.v - e, this.gap = i.t - t)
			},
			p = function (e, i) {
				var s = h("easing." + e, function (t) {
						this._p1 = t || 0 === t ? t : 1.70158, this._p2 = 1.525 * this._p1
					}, !0),
					r = s.prototype = new t;
				return r.constructor = s, r.getRatio = i, r.config = function (t) {
					return new s(t)
				}, s
			},
			f = u("Back", p("BackOut", function (t) {
				return (t -= 1) * t * ((this._p1 + 1) * t + this._p1) + 1
			}), p("BackIn", function (t) {
				return t * t * ((this._p1 + 1) * t - this._p1)
			}), p("BackInOut", function (t) {
				return 1 > (t *= 2) ? .5 * t * t * ((this._p2 + 1) * t - this._p2) : .5 * ((t -= 2) * t * ((this._p2 + 1) * t + this._p2) + 2)
			})),
			m = h("easing.SlowMo", function (t, e, i) {
				e = e || 0 === e ? e : .7, null == t ? t = .7 : t > 1 && (t = 1), this._p = 1 !== t ? e : 0, this._p1 = (1 - t) / 2, this._p2 = t, this._p3 = this._p1 + this._p2, this._calcEnd = i === !0
			}, !0),
			d = m.prototype = new t;
		return d.constructor = m, d.getRatio = function (t) {
			var e = t + (.5 - t) * this._p;
			return this._p1 > t ? this._calcEnd ? 1 - (t = 1 - t / this._p1) * t : e - (t = 1 - t / this._p1) * t * t * t * e : t > this._p3 ? this._calcEnd ? 1 - (t = (t - this._p3) / this._p1) * t : e + (t - e) * (t = (t - this._p3) / this._p1) * t * t * t : this._calcEnd ? 1 : e
		}, m.ease = new m(.7, .7), d.config = m.config = function (t, e, i) {
			return new m(t, e, i)
		}, e = h("easing.SteppedEase", function (t) {
			t = t || 1, this._p1 = 1 / t, this._p2 = t + 1
		}, !0), d = e.prototype = new t, d.constructor = e, d.getRatio = function (t) {
			return 0 > t ? t = 0 : t >= 1 && (t = .999999999), (this._p2 * t >> 0) * this._p1
		}, d.config = e.config = function (t) {
			return new e(t)
		}, i = h("easing.RoughEase", function (e) {
			e = e || {};
			for (var i, s, r, n, a, o, h = e.taper || "none", l = [], _ = 0, u = 0 | (e.points || 20), p = u, f = e.randomize !== !1, m = e.clamp === !0, d = e.template instanceof t ? e.template : null, g = "number" == typeof e.strength ? .4 * e.strength : .4; --p > -1;) i = f ? Math.random() : 1 / u * p, s = d ? d.getRatio(i) : i, "none" === h ? r = g : "out" === h ? (n = 1 - i, r = n * n * g) : "in" === h ? r = i * i * g : .5 > i ? (n = 2 * i, r = .5 * n * n * g) : (n = 2 * (1 - i), r = .5 * n * n * g), f ? s += Math.random() * r - .5 * r : p % 2 ? s += .5 * r : s -= .5 * r, m && (s > 1 ? s = 1 : 0 > s && (s = 0)), l[_++] = {
				x: i,
				y: s
			};
			for (l.sort(function (t, e) {
					return t.x - e.x
				}), o = new c(1, 1, null), p = u; --p > -1;) a = l[p], o = new c(a.x, a.y, o);
			this._prev = new c(0, 0, 0 !== o.t ? o : o.next)
		}, !0), d = i.prototype = new t, d.constructor = i, d.getRatio = function (t) {
			var e = this._prev;
			if (t > e.t) {
				for (; e.next && t >= e.t;) e = e.next;
				e = e.prev
			} else
				for (; e.prev && e.t >= t;) e = e.prev;
			return this._prev = e, e.v + (t - e.t) / e.gap * e.c
		}, d.config = function (t) {
			return new i(t)
		}, i.ease = new i, u("Bounce", l("BounceOut", function (t) {
			return 1 / 2.75 > t ? 7.5625 * t * t : 2 / 2.75 > t ? 7.5625 * (t -= 1.5 / 2.75) * t + .75 : 2.5 / 2.75 > t ? 7.5625 * (t -= 2.25 / 2.75) * t + .9375 : 7.5625 * (t -= 2.625 / 2.75) * t + .984375
		}), l("BounceIn", function (t) {
			return 1 / 2.75 > (t = 1 - t) ? 1 - 7.5625 * t * t : 2 / 2.75 > t ? 1 - (7.5625 * (t -= 1.5 / 2.75) * t + .75) : 2.5 / 2.75 > t ? 1 - (7.5625 * (t -= 2.25 / 2.75) * t + .9375) : 1 - (7.5625 * (t -= 2.625 / 2.75) * t + .984375)
		}), l("BounceInOut", function (t) {
			var e = .5 > t;
			return t = e ? 1 - 2 * t : 2 * t - 1, t = 1 / 2.75 > t ? 7.5625 * t * t : 2 / 2.75 > t ? 7.5625 * (t -= 1.5 / 2.75) * t + .75 : 2.5 / 2.75 > t ? 7.5625 * (t -= 2.25 / 2.75) * t + .9375 : 7.5625 * (t -= 2.625 / 2.75) * t + .984375, e ? .5 * (1 - t) : .5 * t + .5
		})), u("Circ", l("CircOut", function (t) {
			return Math.sqrt(1 - (t -= 1) * t)
		}), l("CircIn", function (t) {
			return -(Math.sqrt(1 - t * t) - 1)
		}), l("CircInOut", function (t) {
			return 1 > (t *= 2) ? -.5 * (Math.sqrt(1 - t * t) - 1) : .5 * (Math.sqrt(1 - (t -= 2) * t) + 1)
		})), s = function (e, i, s) {
			var r = h("easing." + e, function (t, e) {
					this._p1 = t || 1, this._p2 = e || s, this._p3 = this._p2 / a * (Math.asin(1 / this._p1) || 0)
				}, !0),
				n = r.prototype = new t;
			return n.constructor = r, n.getRatio = i, n.config = function (t, e) {
				return new r(t, e)
			}, r
		}, u("Elastic", s("ElasticOut", function (t) {
			return this._p1 * Math.pow(2, -10 * t) * Math.sin((t - this._p3) * a / this._p2) + 1
		}, .3), s("ElasticIn", function (t) {
			return -(this._p1 * Math.pow(2, 10 * (t -= 1)) * Math.sin((t - this._p3) * a / this._p2))
		}, .3), s("ElasticInOut", function (t) {
			return 1 > (t *= 2) ? -.5 * this._p1 * Math.pow(2, 10 * (t -= 1)) * Math.sin((t - this._p3) * a / this._p2) : .5 * this._p1 * Math.pow(2, -10 * (t -= 1)) * Math.sin((t - this._p3) * a / this._p2) + 1
		}, .45)), u("Expo", l("ExpoOut", function (t) {
			return 1 - Math.pow(2, -10 * t)
		}), l("ExpoIn", function (t) {
			return Math.pow(2, 10 * (t - 1)) - .001
		}), l("ExpoInOut", function (t) {
			return 1 > (t *= 2) ? .5 * Math.pow(2, 10 * (t - 1)) : .5 * (2 - Math.pow(2, -10 * (t - 1)))
		})), u("Sine", l("SineOut", function (t) {
			return Math.sin(t * o)
		}), l("SineIn", function (t) {
			return -Math.cos(t * o) + 1
		}), l("SineInOut", function (t) {
			return -.5 * (Math.cos(Math.PI * t) - 1)
		})), h("easing.EaseLookup", {
			find: function (e) {
				return t.map[e]
			}
		}, !0), _(r.SlowMo, "SlowMo", "ease,"), _(i, "RoughEase", "ease,"), _(e, "SteppedEase", "ease,"), f
	}, !0)
}), window._gsDefine && window._gsQueue.pop()();
var pixGS = window.GreenSockGlobals = {};
! function (t) {
	t.thimContentSlider = function (e, i) {
		function n(e) {
			var i = t(' 				<li> 					<div class="slide-content" style="margin: ' + g("itemPadding") + 'px;"> 						<img src="' + e.image + '" alt="' + e.alt + '" /> 					</div> 				</li> 			');
			return i
		}

		function s() {
			var e = typeof q.options.items,
				i = null;
			"string" == e ? i = t(q.options.items) : "object" == e && (i = t(q.options.items).children()), i && (q.options.items = [], i.each(function () {
				var e = t(this),
					i = e.find("img" + q.options.imageSelector + ":first"),
					n = i.parent();
				q.options.items.push({
					image: i.attr("src"),
					alt: i.attr("alt"),
					url: n.is("a") ? n.attr("href") : "",
					content: e.find(q.options.contentSelector)
				})
			}))
		}

		function o() {
			var e = t(' 					<div class="slides-wrapper"> 						<ul class="scrollable"></ul> 					</div> 					<a href="prev" class="control-nav prev"></a> 					<a href="next" class="control-nav next"></a> 					<div class="slides-content"></div>				'),
				i = q.options.items;
			q.$el.html(e), q.$slidesWrapper = q.$el.find(".slides-wrapper"), q.$scrollable = q.$el.find(".scrollable").css({
				marginTop: -g("itemPadding"),
				marginBottom: -g("itemPadding")
			}), q.$slideContent = q.$el.find(".slides-content");
			for (var s = 0, o = i.length; o > s; s++) {
				var d = n({
						image: i[s].image,
						alt: i[s].alt
					}),
					c = t('<div class="slide-content" />').append(i[s].content);
				q.$scrollable.append(d), q.$slideContent.append(c)
			}
			q.$items = q.$scrollable.children(), R = g("itemsVisible") <= g("items").length ? g("itemsVisible") : g("items").length, T = Math.floor(R / 2), k = T, D = q.$items.length, B = k, q.$el.on("click", ".control-nav", h).on("click", ".scrollable > li", h), q.options.mouseWheel && q.$el.on("mousewheel", function (t, e, i, n) {
				t.preventDefault(), -1 != e ? p() : v()
			}), q.options.autoPlay && b(), q.options.pauseOnHover && q.$el.hover(function () {
				w()
			}, function () {
				C()
			}), q.$scrollable.bind(E.start, a).bind(E.move, l).bind(E.end, r), m(), q.$slideContent.children().eq(k).css({
				opacity: 1
			}).addClass("current").siblings().removeClass("current")
		}

		function a() {}

		function l() {}

		function r() {}

		function m() {
			var t = q.$el.find(".control-nav");
			q.$el.hover(function () {
				q.$el.addClass("hover")
			}, function () {
				q.$el.removeClass("hover")
			}), q.$nav = t
		}

		function d() {
			var t = q.$nav.height(),
				e = {
					top: (O - 2 * g("itemPadding")) / 2,
					marginTop: -t / 2
				};
			"behind" == g("controlNav"), q.$nav.css(e)
		}

		function c(t) {
			if (t.hasClass("mid-item")) return 0;
			var e = q.$items.index(t),
				i = q.$items.index(q.$items.filter(".mid-item")),
				n = e - i;
			return n
		}

		function h(e) {
			e.preventDefault();
			var i = t(this).attr("href");
			switch (i) {
				case "prev":
					p();
					break;
				case "next":
					v();
					break;
				default:
					var n = t(e.target);
					n.is("li") || (n = n.closest("li")), u(c(n))
			}
		}

		function f(e, i) {
			"prev" == e ? q.$items.last().remove() : q.$items.first().remove(), $(), q.$items.eq(T).addClass("mid-item").siblings().removeClass("mid-item"), J && q.$slideContent.children().eq(B).stop().show().animate({
				opacity: 1
			}).siblings().hide(), z = !1, C(), q.$el.hasClass("hover"), t.isFunction(i) && i.apply(q)
		}

		function u(t) {
			if (0 == t) return G = "", void(J = !0);
			J = 1 == Math.abs(t) ? !0 : !1, G = 250;
			var e = 0 > t ? p : v;
			e.call(this, function () {
				u(0 > t ? t + 1 : t - 1)
			})
		}

		function p(t) {
			if (!z) {
				w(), z = !0, q.$slideContent.children().eq(B).stop().animate({
					opacity: 0
				}), B--, 0 > B && (B = D - 1);
				var e = (g("itemPadding"), parseInt((O - N) / 2)),
					i = 0,
					n = q.$items.length,
					s = 0,
					o = function () {
						s++, s == n && f("prev", t)
					},
					a = q.$items.last().clone();
				a.insertBefore(q.$items.first()).css({
					left: parseInt(q.$items.first().css("left")) - N
				}), $(), q.$el.find(".mid-item").removeClass("mid-item"), q.$items.eq(k + 1).addClass("mid-item");
				for (var l = i; n >= l; l++) {
					var r = {
						left: A - (k - l) * N,
						width: N,
						top: e
					};
					k > l ? r.left -= H : l == k ? (r.left = A, r.top = 0, r.width = O) : l == k + 1 ? (r.left = A + O + H, r.top = e, r.width = N) : r.left += O - N + H, q.$items.eq(l).stop().show().animate(r, G, o)
				}
			}
		}

		function v(t) {
			if (!z) {
				w(), z = !0, q.$slideContent.children().eq(B).stop().animate({
					opacity: 0
				}), B++, B >= D && (B = 0);
				var e = (g("itemPadding"), parseInt((O - N) / 2)),
					i = 0,
					n = q.$items.length,
					s = 0,
					o = function () {
						s++, s == n && f("next", t)
					},
					a = q.$items.first().clone();
				a.insertAfter(q.$items.last()).css({
					left: parseInt(q.$items.last().css("left")) + N
				}), $(), q.$el.find(".mid-item").removeClass("mid-item"), q.$items.eq(k + 1).addClass("mid-item");
				for (var l = i; n >= l; l++) {
					var r = {
						left: A - (k - l) * N,
						width: N,
						top: e
					};
					k > l ? r.left -= N + H : l == k ? (r.left -= N + H, r.top = e, r.width = N) : l == k + 1 ? (r.left = A, r.top = 0, r.width = O) : r.left = A + O + (l - k - 2) * N + H, q.$items.eq(l).stop().show().animate(r, G, o)
				}
			}
		}

		function $() {
			q.$items = q.$scrollable.children()
		}

		function g(t) {
			return q.options[t]
		}

		function b() {
			S && clearTimeout(S), S = setTimeout(function () {
				b(), v()
			}, g("pauseTime"))
		}

		function w() {
			S && clearTimeout(S)
		}

		function C() {
			g("autoPlay") && b()
		}

		function I(e) {
			if (q.$scrollable.css("width", ""), e = t.extend({
					itemPadding: g("itemPadding"),
					itemMaxWidth: g("itemMaxWidth"),
					itemsVisible: R,
					itemMinWidth: g("itemMinWidth")
				}, e || {}), V = q.$el.width(), O = parseInt(e.itemMaxWidth + 2 * e.itemPadding), N = parseInt(O / F), j = N * (e.itemsVisible - 1) + O + 2 * H, j > V) {
				var i = j - V,
					n = i / (e.itemsVisible + F - 1);
				if (O - n * F < e.itemMinWidth) {
					if (e.itemsVisible - 2 >= 1) return e.itemsVisible -= 2, void I({
						itemsVisible: e.itemsVisible
					})
				} else O -= n * F, N -= n;
				j = V
			} else q.$scrollable.width(j);
			A = parseInt((j - O) / 2)
		}

		function P() {
			I(), d();
			var t = g("itemPadding");
			q.$scrollable.height(O);
			var e = 0,
				i = parseInt((O - N) / 2),
				n = 0,
				s = q.$items.length - 1;
			q.$items.hide();
			for (var o = n; s >= o; o++) q.$items.eq(o).show(), o == k ? q.$items.eq(o).css({
				left: parseInt(A),
				width: parseInt(O)
			}).addClass("mid-item").find(".slide-content").css({
				margin: t
			}) : (e = A - (k - o) * N, o > k ? e += O - N + H : e -= H, q.$items.eq(o).css({
				width: parseInt(N),
				left: parseInt(e),
				top: parseInt(i)
			}).removeClass("mid-item"))
		}

		function x(t) {
			t ? P() : (W && clearTimeout(W), W = setTimeout(function () {
				P()
			}, 350))
		}

		function y() {
			s(), o(), M.on("resize.thim-content-slider", function () {
				x()
			}).trigger("resize.thim-content-slider"), P()
		}
		this.$el = t(e).addClass("thim-content-slider"), this.$items = [], this.options = t.extend({}, t.fn.thimContentSlider.defaults, i);
		var q = this,
			M = t(window),
			V = (t(document), t(document.body), 0),
			W = null,
			S = null,
			T = 0,
			k = 0,
			j = 0,
			z = !1,
			B = 0,
			D = 0,
			F = this.options.activeItemRatio || 2.5,
			H = this.options.activeItemPadding,
			N = 0,
			O = 0,
			R = this.options.itemsVisible || 7,
			A = 0,
			Q = "ontouchstart" in window || window.navigator.msMaxTouchPoints,
			E = {
				start: Q ? "touchstart" : "mousedown",
				move: Q ? "touchmove" : "mousemove",
				end: Q ? "touchend" : "mouseup"
			},
			G = "",
			J = !0;
		this.pause = w, this.restart = C, this.prev = p, this.next = v, this.update = P, this.move = u, y()
	}, t.fn.thimContentSlider = function (e) {
		var i = !1,
			n = [];
		if (arguments.length > 0 && "string" == typeof arguments[0]) {
			i = arguments[0];
			for (var s = 1; s < arguments.length; s++) n[s - 1] = arguments[s]
		}
		return t.each(this, function () {
			var s = t(this),
				o = s.data("thim-content-slider");
			if (o || (o = new t.thimContentSlider(this, e), s.data("thim-content-slider", o)), i) {
				if (t.isFunction(o[i])) return o[i].apply(o, n);
				throw "Method thimContentSlider." + i + "() does not exists"
			}
			return s
		})
	}, t.fn.thimContentSlider.defaults = {
		items: [{
			image: "",
			url: "",
			html: ""
		}],
		itemMaxWidth: 200,
		itemMinWidth: 150,
		itemsVisible: 7,
		itemPadding: 10,
		activeItemRatio: 2,
		activeItemPadding: 0,
		mouseWheel: !0,
		autoPlay: !0,
		pauseTime: 3e3,
		pauseOnHover: !0,
		imageSelector: "",
		contentSelector: ".content",
		controlNav: "behind"
	}
}(jQuery); /*!jquery.cookie v1.4.1 | MIT*/
! function (a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : "object" == typeof exports ? a(require("jquery")) : a(jQuery)
}(function (a) {
	function b(a) {
		return h.raw ? a : encodeURIComponent(a)
	}

	function c(a) {
		return h.raw ? a : decodeURIComponent(a)
	}

	function d(a) {
		return b(h.json ? JSON.stringify(a) : String(a))
	}

	function e(a) {
		0 === a.indexOf('"') && (a = a.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, "\\"));
		try {
			return a = decodeURIComponent(a.replace(g, " ")), h.json ? JSON.parse(a) : a
		} catch (b) {}
	}

	function f(b, c) {
		var d = h.raw ? b : e(b);
		return a.isFunction(c) ? c(d) : d
	}
	var g = /\+/g,
		h = a.cookie = function (e, g, i) {
			if (void 0 !== g && !a.isFunction(g)) {
				if (i = a.extend({}, h.defaults, i), "number" == typeof i.expires) {
					var j = i.expires,
						k = i.expires = new Date;
					k.setTime(+k + 864e5 * j)
				}
				return document.cookie = [b(e), "=", d(g), i.expires ? "; expires=" + i.expires.toUTCString() : "", i.path ? "; path=" + i.path : "", i.domain ? "; domain=" + i.domain : "", i.secure ? "; secure" : ""].join("")
			}
			for (var l = e ? void 0 : {}, m = document.cookie ? document.cookie.split("; ") : [], n = 0, o = m.length; o > n; n++) {
				var p = m[n].split("="),
					q = c(p.shift()),
					r = p.join("=");
				if (e && e === q) {
					l = f(r, g);
					break
				}
				e || void 0 === (r = f(r)) || (l[q] = r)
			}
			return l
		};
	h.defaults = {}, a.removeCookie = function (b, c) {
		return void 0 === a.cookie(b) ? !1 : (a.cookie(b, "", a.extend({}, c, {
			expires: -1
		})), !a.cookie(b))
	}
});
(function (n) {
	var t;
	t = function (t, i, r, u, f) {
		this.$el = n(i);
		this.end = t;
		this.active = !1;
		this.interval = 1e3;
		this.speed = u;
		this.callBack = jQuery.isFunction(f) ? f : null;
		this.localization = {
			days: "days",
			hours: "hours",
			minutes: "minutes",
			seconds: "seconds"
		};
		n.extend(this.localization, this.localization, r)
	};
	t.prototype = {
		getCounterNumbers: function () {
			var n = {
					days: {
						tens: 0,
						units: 0,
						hundreds: 0
					},
					hours: {
						tens: 0,
						units: 0
					},
					minutes: {
						tens: 0,
						units: 0
					},
					seconds: {
						tens: 0,
						units: 0
					}
				},
				s = 864e5,
				h = 36e5,
				c = 6e4,
				t = 0,
				l = new Date,
				i = this.end.getTime() - l.getTime(),
				r, u, f, e, o;
			return i <= 0 ? n : (r = Math.min(Math.floor(i / s), 999), t = i % s, n.days.hundreds = Math.floor(r / 100), u = r % 100, n.days.tens = Math.floor(u / 10), n.days.units = u % 10, f = Math.floor(t / h), t = t % h, n.hours.tens = Math.floor(f / 10), n.hours.units = f % 10, e = Math.floor(t / c), t = t % c, n.minutes.tens = Math.floor(e / 10), n.minutes.units = e % 10, o = Math.floor(t / 1e3), n.seconds.tens = Math.floor(o / 10), n.seconds.units = o % 10, n)
		},
		updatePart: function (t) {
			var r = this.getCounterNumbers(),
				i = n("." + t, this.$el),
				u, f;
			t == "days" && (this.setDayHundreds(r.days.hundreds > 0), i.find(".number.hundreds.show").html() != r[t].hundreds && (u = n(".n1.hundreds", i), f = n(".n2.hundreds", i), this.scrollNumber(u, f, r[t].hundreds)));
			i.find(".number.tens.show").html() != r[t].tens && (u = n(".n1.tens", i), f = n(".n2.tens", i), this.scrollNumber(u, f, r[t].tens));
			i.find(".number.units.show").html() != r[t].units && (u = n(".n1.units", i), f = n(".n2.units", i), this.scrollNumber(u, f, r[t].units))
		},
		timeOut: function () {
			var n = new Date,
				t = this.end.getTime() - n.getTime();
			return t <= 0 ? !0 : !1
		},
		setDayHundreds: function (t) {
			t ? n(".counter.days", this.$el).addClass("with-hundreds") : n(".counter.days", this.$el).removeClass("with-hundreds")
		},
		updateCounter: function () {
			this.updatePart("days");
			this.updatePart("hours");
			this.updatePart("minutes");
			this.updatePart("seconds");
			this.timeOut() && (this.active = !1, this.callBack && this.callBack(this))
		},
		localize: function (t) {
			n.isPlainObject(t) && n.extend(this.localization, this.localization, t);
			n(".days", this.$el).siblings(".counter-caption").text(this.localization.days);
			n(".hours", this.$el).siblings(".counter-caption").text(this.localization.hours);
			n(".minutes", this.$el).siblings(".counter-caption").text(this.localization.minutes);
			n(".seconds", this.$el).siblings(".counter-caption").text(this.localization.seconds)
		},
		start: function (n) {
			var i, t;
			n && (this.interval = n);
			i = this.interval;
			this.active = !0;
			t = this;
			setTimeout(function () {
				t.updateCounter();
				t.active && t.start()
			}, i)
		},
		stop: function () {
			this.active = !1
		},
		scrollNumber: function (n, t, i) {
			n.hasClass("show") ? (t.removeClass("hidden-down").css("top", "-100%").text(i).stop().animate({
				top: 0
			}, this.speed, function () {
				t.addClass("show")
			}), n.stop().animate({
				top: "100%"
			}, this.speed, function () {
				n.removeClass("show").addClass("hidden-down")
			})) : (n.removeClass("hidden-down").css("top", "-100%").text(i).stop().animate({
				top: 0
			}, this.speed, function () {
				n.addClass("show")
			}), t.stop().animate({
				top: "100%"
			}, this.speed, function () {
				t.removeClass("show").addClass("hidden-down")
			}))
		}
	};
	jQuery.fn.mbComingsoon = function (i) {
		var u = {
				interval: 1e3,
				callBack: null,
				localization: {
					days: "days",
					hours: "hours",
					minutes: "minutes",
					seconds: "seconds"
				},
				speed: 500
			},
			r = {},
			f = '   <div class="counter-group" id="myCounter">       <div class="counter-block">           <div class="counter days">               <div class="number show n1 hundreds">0<\/div>               <div class="number show n1 tens">0<\/div>               <div class="number show n1 units">0<\/div>               <div class="number hidden-up n2 hundreds">0<\/div>               <div class="number hidden-up n2 tens">0<\/div>               <div class="number hidden-up n2 units">0<\/div>           <\/div>           <div class="counter-caption">days<\/div>       <\/div>       <div class="counter-block">           <div class="counter hours">               <div class="number show n1 tens">0<\/div>               <div class="number show n1 units">0<\/div>               <div class="number hidden-up n2 tens">0<\/div>               <div class="number hidden-up n2 units">0<\/div>           <\/div>           <div class="counter-caption">hours<\/div>       <\/div>       <div class="counter-block">           <div class="counter minutes">               <div class="number show n1 tens">0<\/div>               <div class="number show n1 units">0<\/div>               <div class="number hidden-up n2 tens">0<\/div>               <div class="number hidden-up n2 units">0<\/div>           <\/div>           <div class="counter-caption">minutes<\/div>       <\/div>       <div class="counter-block">           <div class="counter seconds">               <div class="number show n1 tens">0<\/div>               <div class="number show n1 units">0<\/div>               <div class="number hidden-up n2 tens">0<\/div>               <div class="number hidden-up n2 units">0<\/div>           <\/div>           <div class="counter-caption">seconds<\/div>       <\/div>   <\/div>';
		return this.each(function () {
			var o = n(this),
				e = o.data("mbComingsoon");
			if (e) i == "start" ? e.start() : i == "stop" ? e.stop() : n.isPlainObject(i) && (i.expiryDate instanceof Date && (e.end = i.expiryDate), n.isNumeric(i.interval) && (e.interval = i.interval), n.isFunction(i.callBack) && (e.callBack = i.callBack), n.isPlainObject(i.localization) && this.localize(i.localization));
			else {
				if (i instanceof Date ? r.expiryDate = i : n.isPlainObject(i) ? n.extend(r, u, i) : typeof i == "string" && (r.expiryDate = new Date(i)), !r.expiryDate) throw new Error("Expiry date is required!");
				e = new t(r.expiryDate, o, r.localization, r.speed, r.callBack);
				o.html(f);
				e.localize();
				e.start()
			}
		})
	}
})(jQuery);
! function (i) {
	i.fn.theiaStickySidebar = function (t) {
		var o = {
			containerSelector: "",
			additionalMarginTop: 0,
			additionalMarginBottom: 0,
			updateSidebarHeight: !0,
			minWidth: 0,
			sidebarBehavior: "modern"
		};
		t = i.extend(o, t), t.additionalMarginTop = parseInt(t.additionalMarginTop) || 0, t.additionalMarginBottom = parseInt(t.additionalMarginBottom) || 0, i("head").append(i('<style>.theiaStickySidebar:after {content: ""; display: table; clear: both;}</style>')), this.each(function () {
			function o() {
				e.fixedScrollTop = 0, e.sidebar.css({
					"min-height": "1px"
				}), e.stickySidebar.css({
					position: "static",
					width: ""
				})
			}

			function a(t) {
				var o = t.height();
				return t.children().each(function () {
					o = Math.max(o, i(this).height())
				}), o
			}
			var e = {};
			e.sidebar = i(this), e.options = t || {}, e.container = i(e.options.containerSelector), 0 == e.container.size() && (e.container = e.sidebar.parent()), e.sidebar.parents().css("-webkit-transform", "none"), e.sidebar.css({
				position: "relative",
				overflow: "visible",
				"-webkit-box-sizing": "border-box",
				"-moz-box-sizing": "border-box",
				"box-sizing": "border-box"
			}), e.stickySidebar = e.sidebar.find(".theiaStickySidebar"), 0 == e.stickySidebar.length && (e.sidebar.find("script").remove(), e.stickySidebar = i("<div>").addClass("theiaStickySidebar").append(e.sidebar.children()), e.sidebar.append(e.stickySidebar)), e.marginTop = parseInt(e.sidebar.css("margin-top")), e.marginBottom = parseInt(e.sidebar.css("margin-bottom")), e.paddingTop = parseInt(e.sidebar.css("padding-top")), e.paddingBottom = parseInt(e.sidebar.css("padding-bottom"));
			var d = e.stickySidebar.offset().top,
				r = e.stickySidebar.outerHeight();
			e.stickySidebar.css("padding-top", 1), e.stickySidebar.css("padding-bottom", 1), d -= e.stickySidebar.offset().top, r = e.stickySidebar.outerHeight() - r - d, 0 == d ? (e.stickySidebar.css("padding-top", 0), e.stickySidebarPaddingTop = 0) : e.stickySidebarPaddingTop = 1, 0 == r ? (e.stickySidebar.css("padding-bottom", 0), e.stickySidebarPaddingBottom = 0) : e.stickySidebarPaddingBottom = 1, e.previousScrollTop = null, e.fixedScrollTop = 0, o(), e.onScroll = function (e) {
				if (e.stickySidebar.is(":visible")) {
					if (i("body").width() < e.options.minWidth) return void o();
					if (e.sidebar.outerWidth(!0) + 50 > e.container.width()) return void o();
					var d = i(document).scrollTop(),
						r = "static";
					if (d >= e.container.offset().top + (e.paddingTop + e.marginTop - e.options.additionalMarginTop)) {
						var s, n = e.paddingTop + e.marginTop + t.additionalMarginTop,
							c = e.paddingBottom + e.marginBottom + t.additionalMarginBottom,
							p = e.container.offset().top,
							b = e.container.offset().top + a(e.container),
							g = 0 + t.additionalMarginTop,
							l = e.stickySidebar.outerHeight() + n + c < i(window).height();
						s = l ? g + e.stickySidebar.outerHeight() : i(window).height() - e.marginBottom - e.paddingBottom - t.additionalMarginBottom;
						var h = p - d + e.paddingTop + e.marginTop,
							S = b - d - e.paddingBottom - e.marginBottom,
							f = e.stickySidebar.offset().top - d,
							m = e.previousScrollTop - d;
						"fixed" == e.stickySidebar.css("position") && "modern" == e.options.sidebarBehavior && (f += m), "legacy" == e.options.sidebarBehavior && (f = s - e.stickySidebar.outerHeight(), f = Math.max(f, s - e.stickySidebar.outerHeight())), f = m > 0 ? Math.min(f, g) : Math.max(f, s - e.stickySidebar.outerHeight()), f = Math.max(f, h), f = Math.min(f, S - e.stickySidebar.outerHeight());
						var y = e.container.height() == e.stickySidebar.outerHeight();
						r = (y || f != g) && (y || f != s - e.stickySidebar.outerHeight()) ? d + f - e.sidebar.offset().top - e.paddingTop <= t.additionalMarginTop ? "static" : "absolute" : "fixed"
					}
					if ("fixed" == r) e.stickySidebar.css({
						position: "fixed",
						width: e.sidebar.width(),
						top: f,
						left: e.sidebar.offset().left + parseInt(e.sidebar.css("padding-left"))
					});
					else if ("absolute" == r) {
						var k = {};
						"absolute" != e.stickySidebar.css("position") && (k.position = "absolute", k.top = d + f - e.sidebar.offset().top - e.stickySidebarPaddingTop - e.stickySidebarPaddingBottom), k.width = e.sidebar.width(), k.left = "", e.stickySidebar.css(k)
					} else "static" == r && o();
					"static" != r && 1 == e.options.updateSidebarHeight && e.sidebar.css({
						"min-height": e.stickySidebar.outerHeight() + e.stickySidebar.offset().top - e.sidebar.offset().top + e.paddingBottom
					}), e.previousScrollTop = d
				}
			}, e.onScroll(e), i(document).scroll(function (i) {
				return function () {
					i.onScroll(i)
				}
			}(e)), i(window).resize(function (i) {
				return function () {
					i.stickySidebar.css({
						position: "static"
					}), i.onScroll(i)
				}
			}(e))
		})
	}
}(jQuery);
/*!
 * Isotope PACKAGED v3.0.6
 *
 * Licensed GPLv3 for open source use
 * or Isotope Commercial License for commercial use
 *
 * https://isotope.metafizzy.co
 * Copyright 2010-2018 Metafizzy
 */
! function (t, e) {
	"function" == typeof define && define.amd ? define("jquery-bridget/jquery-bridget", ["jquery"], function (i) {
		return e(t, i)
	}) : "object" == typeof module && module.exports ? module.exports = e(t, require("jquery")) : t.jQueryBridget = e(t, t.jQuery)
}(window, function (t, e) {
	"use strict";

	function i(i, s, a) {
		function u(t, e, o) {
			var n, s = "$()." + i + '("' + e + '")';
			return t.each(function (t, u) {
				var h = a.data(u, i);
				if (!h) return void r(i + " not initialized. Cannot call methods, i.e. " + s);
				var d = h[e];
				if (!d || "_" == e.charAt(0)) return void r(s + " is not a valid method");
				var l = d.apply(h, o);
				n = void 0 === n ? l : n
			}), void 0 !== n ? n : t
		}

		function h(t, e) {
			t.each(function (t, o) {
				var n = a.data(o, i);
				n ? (n.option(e), n._init()) : (n = new s(o, e), a.data(o, i, n))
			})
		}
		a = a || e || t.jQuery, a && (s.prototype.option || (s.prototype.option = function (t) {
			a.isPlainObject(t) && (this.options = a.extend(!0, this.options, t))
		}), a.fn[i] = function (t) {
			if ("string" == typeof t) {
				var e = n.call(arguments, 1);
				return u(this, t, e)
			}
			return h(this, t), this
		}, o(a))
	}

	function o(t) {
		!t || t && t.bridget || (t.bridget = i)
	}
	var n = Array.prototype.slice,
		s = t.console,
		r = "undefined" == typeof s ? function () {} : function (t) {
			s.error(t)
		};
	return o(e || t.jQuery), i
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("ev-emitter/ev-emitter", e) : "object" == typeof module && module.exports ? module.exports = e() : t.EvEmitter = e()
}("undefined" != typeof window ? window : this, function () {
	function t() {}
	var e = t.prototype;
	return e.on = function (t, e) {
		if (t && e) {
			var i = this._events = this._events || {},
				o = i[t] = i[t] || [];
			return o.indexOf(e) == -1 && o.push(e), this
		}
	}, e.once = function (t, e) {
		if (t && e) {
			this.on(t, e);
			var i = this._onceEvents = this._onceEvents || {},
				o = i[t] = i[t] || {};
			return o[e] = !0, this
		}
	}, e.off = function (t, e) {
		var i = this._events && this._events[t];
		if (i && i.length) {
			var o = i.indexOf(e);
			return o != -1 && i.splice(o, 1), this
		}
	}, e.emitEvent = function (t, e) {
		var i = this._events && this._events[t];
		if (i && i.length) {
			i = i.slice(0), e = e || [];
			for (var o = this._onceEvents && this._onceEvents[t], n = 0; n < i.length; n++) {
				var s = i[n],
					r = o && o[s];
				r && (this.off(t, s), delete o[s]), s.apply(this, e)
			}
			return this
		}
	}, e.allOff = function () {
		delete this._events, delete this._onceEvents
	}, t
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("get-size/get-size", e) : "object" == typeof module && module.exports ? module.exports = e() : t.getSize = e()
}(window, function () {
	"use strict";

	function t(t) {
		var e = parseFloat(t),
			i = t.indexOf("%") == -1 && !isNaN(e);
		return i && e
	}

	function e() {}

	function i() {
		for (var t = {
				width: 0,
				height: 0,
				innerWidth: 0,
				innerHeight: 0,
				outerWidth: 0,
				outerHeight: 0
			}, e = 0; e < h; e++) {
			var i = u[e];
			t[i] = 0
		}
		return t
	}

	function o(t) {
		var e = getComputedStyle(t);
		return e || a("Style returned " + e + ". Are you running this code in a hidden iframe on Firefox? See https://bit.ly/getsizebug1"), e
	}

	function n() {
		if (!d) {
			d = !0;
			var e = document.createElement("div");
			e.style.width = "200px", e.style.padding = "1px 2px 3px 4px", e.style.borderStyle = "solid", e.style.borderWidth = "1px 2px 3px 4px", e.style.boxSizing = "border-box";
			var i = document.body || document.documentElement;
			i.appendChild(e);
			var n = o(e);
			r = 200 == Math.round(t(n.width)), s.isBoxSizeOuter = r, i.removeChild(e)
		}
	}

	function s(e) {
		if (n(), "string" == typeof e && (e = document.querySelector(e)), e && "object" == typeof e && e.nodeType) {
			var s = o(e);
			if ("none" == s.display) return i();
			var a = {};
			a.width = e.offsetWidth, a.height = e.offsetHeight;
			for (var d = a.isBorderBox = "border-box" == s.boxSizing, l = 0; l < h; l++) {
				var f = u[l],
					c = s[f],
					m = parseFloat(c);
				a[f] = isNaN(m) ? 0 : m
			}
			var p = a.paddingLeft + a.paddingRight,
				y = a.paddingTop + a.paddingBottom,
				g = a.marginLeft + a.marginRight,
				v = a.marginTop + a.marginBottom,
				_ = a.borderLeftWidth + a.borderRightWidth,
				z = a.borderTopWidth + a.borderBottomWidth,
				I = d && r,
				x = t(s.width);
			x !== !1 && (a.width = x + (I ? 0 : p + _));
			var S = t(s.height);
			return S !== !1 && (a.height = S + (I ? 0 : y + z)), a.innerWidth = a.width - (p + _), a.innerHeight = a.height - (y + z), a.outerWidth = a.width + g, a.outerHeight = a.height + v, a
		}
	}
	var r, a = "undefined" == typeof console ? e : function (t) {
			console.error(t)
		},
		u = ["paddingLeft", "paddingRight", "paddingTop", "paddingBottom", "marginLeft", "marginRight", "marginTop", "marginBottom", "borderLeftWidth", "borderRightWidth", "borderTopWidth", "borderBottomWidth"],
		h = u.length,
		d = !1;
	return s
}),
function (t, e) {
	"use strict";
	"function" == typeof define && define.amd ? define("desandro-matches-selector/matches-selector", e) : "object" == typeof module && module.exports ? module.exports = e() : t.matchesSelector = e()
}(window, function () {
	"use strict";
	var t = function () {
		var t = window.Element.prototype;
		if (t.matches) return "matches";
		if (t.matchesSelector) return "matchesSelector";
		for (var e = ["webkit", "moz", "ms", "o"], i = 0; i < e.length; i++) {
			var o = e[i],
				n = o + "MatchesSelector";
			if (t[n]) return n
		}
	}();
	return function (e, i) {
		return e[t](i)
	}
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("fizzy-ui-utils/utils", ["desandro-matches-selector/matches-selector"], function (i) {
		return e(t, i)
	}) : "object" == typeof module && module.exports ? module.exports = e(t, require("desandro-matches-selector")) : t.fizzyUIUtils = e(t, t.matchesSelector)
}(window, function (t, e) {
	var i = {};
	i.extend = function (t, e) {
		for (var i in e) t[i] = e[i];
		return t
	}, i.modulo = function (t, e) {
		return (t % e + e) % e
	};
	var o = Array.prototype.slice;
	i.makeArray = function (t) {
		if (Array.isArray(t)) return t;
		if (null === t || void 0 === t) return [];
		var e = "object" == typeof t && "number" == typeof t.length;
		return e ? o.call(t) : [t]
	}, i.removeFrom = function (t, e) {
		var i = t.indexOf(e);
		i != -1 && t.splice(i, 1)
	}, i.getParent = function (t, i) {
		for (; t.parentNode && t != document.body;)
			if (t = t.parentNode, e(t, i)) return t
	}, i.getQueryElement = function (t) {
		return "string" == typeof t ? document.querySelector(t) : t
	}, i.handleEvent = function (t) {
		var e = "on" + t.type;
		this[e] && this[e](t)
	}, i.filterFindElements = function (t, o) {
		t = i.makeArray(t);
		var n = [];
		return t.forEach(function (t) {
			if (t instanceof HTMLElement) {
				if (!o) return void n.push(t);
				e(t, o) && n.push(t);
				for (var i = t.querySelectorAll(o), s = 0; s < i.length; s++) n.push(i[s])
			}
		}), n
	}, i.debounceMethod = function (t, e, i) {
		i = i || 100;
		var o = t.prototype[e],
			n = e + "Timeout";
		t.prototype[e] = function () {
			var t = this[n];
			clearTimeout(t);
			var e = arguments,
				s = this;
			this[n] = setTimeout(function () {
				o.apply(s, e), delete s[n]
			}, i)
		}
	}, i.docReady = function (t) {
		var e = document.readyState;
		"complete" == e || "interactive" == e ? setTimeout(t) : document.addEventListener("DOMContentLoaded", t)
	}, i.toDashed = function (t) {
		return t.replace(/(.)([A-Z])/g, function (t, e, i) {
			return e + "-" + i
		}).toLowerCase()
	};
	var n = t.console;
	return i.htmlInit = function (e, o) {
		i.docReady(function () {
			var s = i.toDashed(o),
				r = "data-" + s,
				a = document.querySelectorAll("[" + r + "]"),
				u = document.querySelectorAll(".js-" + s),
				h = i.makeArray(a).concat(i.makeArray(u)),
				d = r + "-options",
				l = t.jQuery;
			h.forEach(function (t) {
				var i, s = t.getAttribute(r) || t.getAttribute(d);
				try {
					i = s && JSON.parse(s)
				} catch (a) {
					return void(n && n.error("Error parsing " + r + " on " + t.className + ": " + a))
				}
				var u = new e(t, i);
				l && l.data(t, o, u)
			})
		})
	}, i
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("outlayer/item", ["ev-emitter/ev-emitter", "get-size/get-size"], e) : "object" == typeof module && module.exports ? module.exports = e(require("ev-emitter"), require("get-size")) : (t.Outlayer = {}, t.Outlayer.Item = e(t.EvEmitter, t.getSize))
}(window, function (t, e) {
	"use strict";

	function i(t) {
		for (var e in t) return !1;
		return e = null, !0
	}

	function o(t, e) {
		t && (this.element = t, this.layout = e, this.position = {
			x: 0,
			y: 0
		}, this._create())
	}

	function n(t) {
		return t.replace(/([A-Z])/g, function (t) {
			return "-" + t.toLowerCase()
		})
	}
	var s = document.documentElement.style,
		r = "string" == typeof s.transition ? "transition" : "WebkitTransition",
		a = "string" == typeof s.transform ? "transform" : "WebkitTransform",
		u = {
			WebkitTransition: "webkitTransitionEnd",
			transition: "transitionend"
		}[r],
		h = {
			transform: a,
			transition: r,
			transitionDuration: r + "Duration",
			transitionProperty: r + "Property",
			transitionDelay: r + "Delay"
		},
		d = o.prototype = Object.create(t.prototype);
	d.constructor = o, d._create = function () {
		this._transn = {
			ingProperties: {},
			clean: {},
			onEnd: {}
		}, this.css({
			position: "absolute"
		})
	}, d.handleEvent = function (t) {
		var e = "on" + t.type;
		this[e] && this[e](t)
	}, d.getSize = function () {
		this.size = e(this.element)
	}, d.css = function (t) {
		var e = this.element.style;
		for (var i in t) {
			var o = h[i] || i;
			e[o] = t[i]
		}
	}, d.getPosition = function () {
		var t = getComputedStyle(this.element),
			e = this.layout._getOption("originLeft"),
			i = this.layout._getOption("originTop"),
			o = t[e ? "left" : "right"],
			n = t[i ? "top" : "bottom"],
			s = parseFloat(o),
			r = parseFloat(n),
			a = this.layout.size;
		o.indexOf("%") != -1 && (s = s / 100 * a.width), n.indexOf("%") != -1 && (r = r / 100 * a.height), s = isNaN(s) ? 0 : s, r = isNaN(r) ? 0 : r, s -= e ? a.paddingLeft : a.paddingRight, r -= i ? a.paddingTop : a.paddingBottom, this.position.x = s, this.position.y = r
	}, d.layoutPosition = function () {
		var t = this.layout.size,
			e = {},
			i = this.layout._getOption("originLeft"),
			o = this.layout._getOption("originTop"),
			n = i ? "paddingLeft" : "paddingRight",
			s = i ? "left" : "right",
			r = i ? "right" : "left",
			a = this.position.x + t[n];
		e[s] = this.getXValue(a), e[r] = "";
		var u = o ? "paddingTop" : "paddingBottom",
			h = o ? "top" : "bottom",
			d = o ? "bottom" : "top",
			l = this.position.y + t[u];
		e[h] = this.getYValue(l), e[d] = "", this.css(e), this.emitEvent("layout", [this])
	}, d.getXValue = function (t) {
		var e = this.layout._getOption("horizontal");
		return this.layout.options.percentPosition && !e ? t / this.layout.size.width * 100 + "%" : t + "px"
	}, d.getYValue = function (t) {
		var e = this.layout._getOption("horizontal");
		return this.layout.options.percentPosition && e ? t / this.layout.size.height * 100 + "%" : t + "px"
	}, d._transitionTo = function (t, e) {
		this.getPosition();
		var i = this.position.x,
			o = this.position.y,
			n = t == this.position.x && e == this.position.y;
		if (this.setPosition(t, e), n && !this.isTransitioning) return void this.layoutPosition();
		var s = t - i,
			r = e - o,
			a = {};
		a.transform = this.getTranslate(s, r), this.transition({
			to: a,
			onTransitionEnd: {
				transform: this.layoutPosition
			},
			isCleaning: !0
		})
	}, d.getTranslate = function (t, e) {
		var i = this.layout._getOption("originLeft"),
			o = this.layout._getOption("originTop");
		return t = i ? t : -t, e = o ? e : -e, "translate3d(" + t + "px, " + e + "px, 0)"
	}, d.goTo = function (t, e) {
		this.setPosition(t, e), this.layoutPosition()
	}, d.moveTo = d._transitionTo, d.setPosition = function (t, e) {
		this.position.x = parseFloat(t), this.position.y = parseFloat(e)
	}, d._nonTransition = function (t) {
		this.css(t.to), t.isCleaning && this._removeStyles(t.to);
		for (var e in t.onTransitionEnd) t.onTransitionEnd[e].call(this)
	}, d.transition = function (t) {
		if (!parseFloat(this.layout.options.transitionDuration)) return void this._nonTransition(t);
		var e = this._transn;
		for (var i in t.onTransitionEnd) e.onEnd[i] = t.onTransitionEnd[i];
		for (i in t.to) e.ingProperties[i] = !0, t.isCleaning && (e.clean[i] = !0);
		if (t.from) {
			this.css(t.from);
			var o = this.element.offsetHeight;
			o = null
		}
		this.enableTransition(t.to), this.css(t.to), this.isTransitioning = !0
	};
	var l = "opacity," + n(a);
	d.enableTransition = function () {
		if (!this.isTransitioning) {
			var t = this.layout.options.transitionDuration;
			t = "number" == typeof t ? t + "ms" : t, this.css({
				transitionProperty: l,
				transitionDuration: t,
				transitionDelay: this.staggerDelay || 0
			}), this.element.addEventListener(u, this, !1)
		}
	}, d.onwebkitTransitionEnd = function (t) {
		this.ontransitionend(t)
	}, d.onotransitionend = function (t) {
		this.ontransitionend(t)
	};
	var f = {
		"-webkit-transform": "transform"
	};
	d.ontransitionend = function (t) {
		if (t.target === this.element) {
			var e = this._transn,
				o = f[t.propertyName] || t.propertyName;
			if (delete e.ingProperties[o], i(e.ingProperties) && this.disableTransition(), o in e.clean && (this.element.style[t.propertyName] = "", delete e.clean[o]), o in e.onEnd) {
				var n = e.onEnd[o];
				n.call(this), delete e.onEnd[o]
			}
			this.emitEvent("transitionEnd", [this])
		}
	}, d.disableTransition = function () {
		this.removeTransitionStyles(), this.element.removeEventListener(u, this, !1), this.isTransitioning = !1
	}, d._removeStyles = function (t) {
		var e = {};
		for (var i in t) e[i] = "";
		this.css(e)
	};
	var c = {
		transitionProperty: "",
		transitionDuration: "",
		transitionDelay: ""
	};
	return d.removeTransitionStyles = function () {
		this.css(c)
	}, d.stagger = function (t) {
		t = isNaN(t) ? 0 : t, this.staggerDelay = t + "ms"
	}, d.removeElem = function () {
		this.element.parentNode.removeChild(this.element), this.css({
			display: ""
		}), this.emitEvent("remove", [this])
	}, d.remove = function () {
		return r && parseFloat(this.layout.options.transitionDuration) ? (this.once("transitionEnd", function () {
			this.removeElem()
		}), void this.hide()) : void this.removeElem()
	}, d.reveal = function () {
		delete this.isHidden, this.css({
			display: ""
		});
		var t = this.layout.options,
			e = {},
			i = this.getHideRevealTransitionEndProperty("visibleStyle");
		e[i] = this.onRevealTransitionEnd, this.transition({
			from: t.hiddenStyle,
			to: t.visibleStyle,
			isCleaning: !0,
			onTransitionEnd: e
		})
	}, d.onRevealTransitionEnd = function () {
		this.isHidden || this.emitEvent("reveal")
	}, d.getHideRevealTransitionEndProperty = function (t) {
		var e = this.layout.options[t];
		if (e.opacity) return "opacity";
		for (var i in e) return i
	}, d.hide = function () {
		this.isHidden = !0, this.css({
			display: ""
		});
		var t = this.layout.options,
			e = {},
			i = this.getHideRevealTransitionEndProperty("hiddenStyle");
		e[i] = this.onHideTransitionEnd, this.transition({
			from: t.visibleStyle,
			to: t.hiddenStyle,
			isCleaning: !0,
			onTransitionEnd: e
		})
	}, d.onHideTransitionEnd = function () {
		this.isHidden && (this.css({
			display: "none"
		}), this.emitEvent("hide"))
	}, d.destroy = function () {
		this.css({
			position: "",
			left: "",
			right: "",
			top: "",
			bottom: "",
			transition: "",
			transform: ""
		})
	}, o
}),
function (t, e) {
	"use strict";
	"function" == typeof define && define.amd ? define("outlayer/outlayer", ["ev-emitter/ev-emitter", "get-size/get-size", "fizzy-ui-utils/utils", "./item"], function (i, o, n, s) {
		return e(t, i, o, n, s)
	}) : "object" == typeof module && module.exports ? module.exports = e(t, require("ev-emitter"), require("get-size"), require("fizzy-ui-utils"), require("./item")) : t.Outlayer = e(t, t.EvEmitter, t.getSize, t.fizzyUIUtils, t.Outlayer.Item)
}(window, function (t, e, i, o, n) {
	"use strict";

	function s(t, e) {
		var i = o.getQueryElement(t);
		if (!i) return void(u && u.error("Bad element for " + this.constructor.namespace + ": " + (i || t)));
		this.element = i, h && (this.$element = h(this.element)), this.options = o.extend({}, this.constructor.defaults), this.option(e);
		var n = ++l;
		this.element.outlayerGUID = n, f[n] = this, this._create();
		var s = this._getOption("initLayout");
		s && this.layout()
	}

	function r(t) {
		function e() {
			t.apply(this, arguments)
		}
		return e.prototype = Object.create(t.prototype), e.prototype.constructor = e, e
	}

	function a(t) {
		if ("number" == typeof t) return t;
		var e = t.match(/(^\d*\.?\d*)(\w*)/),
			i = e && e[1],
			o = e && e[2];
		if (!i.length) return 0;
		i = parseFloat(i);
		var n = m[o] || 1;
		return i * n
	}
	var u = t.console,
		h = t.jQuery,
		d = function () {},
		l = 0,
		f = {};
	s.namespace = "outlayer", s.Item = n, s.defaults = {
		containerStyle: {
			position: "relative"
		},
		initLayout: !0,
		originLeft: !0,
		originTop: !0,
		resize: !0,
		resizeContainer: !0,
		transitionDuration: "0.4s",
		hiddenStyle: {
			opacity: 0,
			transform: "scale(0.001)"
		},
		visibleStyle: {
			opacity: 1,
			transform: "scale(1)"
		}
	};
	var c = s.prototype;
	o.extend(c, e.prototype), c.option = function (t) {
		o.extend(this.options, t)
	}, c._getOption = function (t) {
		var e = this.constructor.compatOptions[t];
		return e && void 0 !== this.options[e] ? this.options[e] : this.options[t]
	}, s.compatOptions = {
		initLayout: "isInitLayout",
		horizontal: "isHorizontal",
		layoutInstant: "isLayoutInstant",
		originLeft: "isOriginLeft",
		originTop: "isOriginTop",
		resize: "isResizeBound",
		resizeContainer: "isResizingContainer"
	}, c._create = function () {
		this.reloadItems(), this.stamps = [], this.stamp(this.options.stamp), o.extend(this.element.style, this.options.containerStyle);
		var t = this._getOption("resize");
		t && this.bindResize()
	}, c.reloadItems = function () {
		this.items = this._itemize(this.element.children)
	}, c._itemize = function (t) {
		for (var e = this._filterFindItemElements(t), i = this.constructor.Item, o = [], n = 0; n < e.length; n++) {
			var s = e[n],
				r = new i(s, this);
			o.push(r)
		}
		return o
	}, c._filterFindItemElements = function (t) {
		return o.filterFindElements(t, this.options.itemSelector)
	}, c.getItemElements = function () {
		return this.items.map(function (t) {
			return t.element
		})
	}, c.layout = function () {
		this._resetLayout(), this._manageStamps();
		var t = this._getOption("layoutInstant"),
			e = void 0 !== t ? t : !this._isLayoutInited;
		this.layoutItems(this.items, e), this._isLayoutInited = !0
	}, c._init = c.layout, c._resetLayout = function () {
		this.getSize()
	}, c.getSize = function () {
		this.size = i(this.element)
	}, c._getMeasurement = function (t, e) {
		var o, n = this.options[t];
		n ? ("string" == typeof n ? o = this.element.querySelector(n) : n instanceof HTMLElement && (o = n), this[t] = o ? i(o)[e] : n) : this[t] = 0
	}, c.layoutItems = function (t, e) {
		t = this._getItemsForLayout(t), this._layoutItems(t, e), this._postLayout()
	}, c._getItemsForLayout = function (t) {
		return t.filter(function (t) {
			return !t.isIgnored
		})
	}, c._layoutItems = function (t, e) {
		if (this._emitCompleteOnItems("layout", t), t && t.length) {
			var i = [];
			t.forEach(function (t) {
				var o = this._getItemLayoutPosition(t);
				o.item = t, o.isInstant = e || t.isLayoutInstant, i.push(o)
			}, this), this._processLayoutQueue(i)
		}
	}, c._getItemLayoutPosition = function () {
		return {
			x: 0,
			y: 0
		}
	}, c._processLayoutQueue = function (t) {
		this.updateStagger(), t.forEach(function (t, e) {
			this._positionItem(t.item, t.x, t.y, t.isInstant, e)
		}, this)
	}, c.updateStagger = function () {
		var t = this.options.stagger;
		return null === t || void 0 === t ? void(this.stagger = 0) : (this.stagger = a(t), this.stagger)
	}, c._positionItem = function (t, e, i, o, n) {
		o ? t.goTo(e, i) : (t.stagger(n * this.stagger), t.moveTo(e, i))
	}, c._postLayout = function () {
		this.resizeContainer()
	}, c.resizeContainer = function () {
		var t = this._getOption("resizeContainer");
		if (t) {
			var e = this._getContainerSize();
			e && (this._setContainerMeasure(e.width, !0), this._setContainerMeasure(e.height, !1))
		}
	}, c._getContainerSize = d, c._setContainerMeasure = function (t, e) {
		if (void 0 !== t) {
			var i = this.size;
			i.isBorderBox && (t += e ? i.paddingLeft + i.paddingRight + i.borderLeftWidth + i.borderRightWidth : i.paddingBottom + i.paddingTop + i.borderTopWidth + i.borderBottomWidth), t = Math.max(t, 0), this.element.style[e ? "width" : "height"] = t + "px"
		}
	}, c._emitCompleteOnItems = function (t, e) {
		function i() {
			n.dispatchEvent(t + "Complete", null, [e])
		}

		function o() {
			r++, r == s && i()
		}
		var n = this,
			s = e.length;
		if (!e || !s) return void i();
		var r = 0;
		e.forEach(function (e) {
			e.once(t, o)
		})
	}, c.dispatchEvent = function (t, e, i) {
		var o = e ? [e].concat(i) : i;
		if (this.emitEvent(t, o), h)
			if (this.$element = this.$element || h(this.element), e) {
				var n = h.Event(e);
				n.type = t, this.$element.trigger(n, i)
			} else this.$element.trigger(t, i)
	}, c.ignore = function (t) {
		var e = this.getItem(t);
		e && (e.isIgnored = !0)
	}, c.unignore = function (t) {
		var e = this.getItem(t);
		e && delete e.isIgnored
	}, c.stamp = function (t) {
		t = this._find(t), t && (this.stamps = this.stamps.concat(t), t.forEach(this.ignore, this))
	}, c.unstamp = function (t) {
		t = this._find(t), t && t.forEach(function (t) {
			o.removeFrom(this.stamps, t), this.unignore(t)
		}, this)
	}, c._find = function (t) {
		if (t) return "string" == typeof t && (t = this.element.querySelectorAll(t)), t = o.makeArray(t)
	}, c._manageStamps = function () {
		this.stamps && this.stamps.length && (this._getBoundingRect(), this.stamps.forEach(this._manageStamp, this))
	}, c._getBoundingRect = function () {
		var t = this.element.getBoundingClientRect(),
			e = this.size;
		this._boundingRect = {
			left: t.left + e.paddingLeft + e.borderLeftWidth,
			top: t.top + e.paddingTop + e.borderTopWidth,
			right: t.right - (e.paddingRight + e.borderRightWidth),
			bottom: t.bottom - (e.paddingBottom + e.borderBottomWidth)
		}
	}, c._manageStamp = d, c._getElementOffset = function (t) {
		var e = t.getBoundingClientRect(),
			o = this._boundingRect,
			n = i(t),
			s = {
				left: e.left - o.left - n.marginLeft,
				top: e.top - o.top - n.marginTop,
				right: o.right - e.right - n.marginRight,
				bottom: o.bottom - e.bottom - n.marginBottom
			};
		return s
	}, c.handleEvent = o.handleEvent, c.bindResize = function () {
		t.addEventListener("resize", this), this.isResizeBound = !0
	}, c.unbindResize = function () {
		t.removeEventListener("resize", this), this.isResizeBound = !1
	}, c.onresize = function () {
		this.resize()
	}, o.debounceMethod(s, "onresize", 100), c.resize = function () {
		this.isResizeBound && this.needsResizeLayout() && this.layout()
	}, c.needsResizeLayout = function () {
		var t = i(this.element),
			e = this.size && t;
		return e && t.innerWidth !== this.size.innerWidth
	}, c.addItems = function (t) {
		var e = this._itemize(t);
		return e.length && (this.items = this.items.concat(e)), e
	}, c.appended = function (t) {
		var e = this.addItems(t);
		e.length && (this.layoutItems(e, !0), this.reveal(e))
	}, c.prepended = function (t) {
		var e = this._itemize(t);
		if (e.length) {
			var i = this.items.slice(0);
			this.items = e.concat(i), this._resetLayout(), this._manageStamps(), this.layoutItems(e, !0), this.reveal(e), this.layoutItems(i)
		}
	}, c.reveal = function (t) {
		if (this._emitCompleteOnItems("reveal", t), t && t.length) {
			var e = this.updateStagger();
			t.forEach(function (t, i) {
				t.stagger(i * e), t.reveal()
			})
		}
	}, c.hide = function (t) {
		if (this._emitCompleteOnItems("hide", t), t && t.length) {
			var e = this.updateStagger();
			t.forEach(function (t, i) {
				t.stagger(i * e), t.hide()
			})
		}
	}, c.revealItemElements = function (t) {
		var e = this.getItems(t);
		this.reveal(e)
	}, c.hideItemElements = function (t) {
		var e = this.getItems(t);
		this.hide(e)
	}, c.getItem = function (t) {
		for (var e = 0; e < this.items.length; e++) {
			var i = this.items[e];
			if (i.element == t) return i
		}
	}, c.getItems = function (t) {
		t = o.makeArray(t);
		var e = [];
		return t.forEach(function (t) {
			var i = this.getItem(t);
			i && e.push(i)
		}, this), e
	}, c.remove = function (t) {
		var e = this.getItems(t);
		this._emitCompleteOnItems("remove", e), e && e.length && e.forEach(function (t) {
			t.remove(), o.removeFrom(this.items, t)
		}, this)
	}, c.destroy = function () {
		var t = this.element.style;
		t.height = "", t.position = "", t.width = "", this.items.forEach(function (t) {
			t.destroy()
		}), this.unbindResize();
		var e = this.element.outlayerGUID;
		delete f[e], delete this.element.outlayerGUID, h && h.removeData(this.element, this.constructor.namespace)
	}, s.data = function (t) {
		t = o.getQueryElement(t);
		var e = t && t.outlayerGUID;
		return e && f[e]
	}, s.create = function (t, e) {
		var i = r(s);
		return i.defaults = o.extend({}, s.defaults), o.extend(i.defaults, e), i.compatOptions = o.extend({}, s.compatOptions), i.namespace = t, i.data = s.data, i.Item = r(n), o.htmlInit(i, t), h && h.bridget && h.bridget(t, i), i
	};
	var m = {
		ms: 1,
		s: 1e3
	};
	return s.Item = n, s
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("isotope-layout/js/item", ["outlayer/outlayer"], e) : "object" == typeof module && module.exports ? module.exports = e(require("outlayer")) : (t.Isotope = t.Isotope || {}, t.Isotope.Item = e(t.Outlayer))
}(window, function (t) {
	"use strict";

	function e() {
		t.Item.apply(this, arguments)
	}
	var i = e.prototype = Object.create(t.Item.prototype),
		o = i._create;
	i._create = function () {
		this.id = this.layout.itemGUID++, o.call(this), this.sortData = {}
	}, i.updateSortData = function () {
		if (!this.isIgnored) {
			this.sortData.id = this.id, this.sortData["original-order"] = this.id, this.sortData.random = Math.random();
			var t = this.layout.options.getSortData,
				e = this.layout._sorters;
			for (var i in t) {
				var o = e[i];
				this.sortData[i] = o(this.element, this)
			}
		}
	};
	var n = i.destroy;
	return i.destroy = function () {
		n.apply(this, arguments), this.css({
			display: ""
		})
	}, e
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("isotope-layout/js/layout-mode", ["get-size/get-size", "outlayer/outlayer"], e) : "object" == typeof module && module.exports ? module.exports = e(require("get-size"), require("outlayer")) : (t.Isotope = t.Isotope || {}, t.Isotope.LayoutMode = e(t.getSize, t.Outlayer))
}(window, function (t, e) {
	"use strict";

	function i(t) {
		this.isotope = t, t && (this.options = t.options[this.namespace], this.element = t.element, this.items = t.filteredItems, this.size = t.size)
	}
	var o = i.prototype,
		n = ["_resetLayout", "_getItemLayoutPosition", "_manageStamp", "_getContainerSize", "_getElementOffset", "needsResizeLayout", "_getOption"];
	return n.forEach(function (t) {
		o[t] = function () {
			return e.prototype[t].apply(this.isotope, arguments)
		}
	}), o.needsVerticalResizeLayout = function () {
		var e = t(this.isotope.element),
			i = this.isotope.size && e;
		return i && e.innerHeight != this.isotope.size.innerHeight
	}, o._getMeasurement = function () {
		this.isotope._getMeasurement.apply(this, arguments)
	}, o.getColumnWidth = function () {
		this.getSegmentSize("column", "Width")
	}, o.getRowHeight = function () {
		this.getSegmentSize("row", "Height")
	}, o.getSegmentSize = function (t, e) {
		var i = t + e,
			o = "outer" + e;
		if (this._getMeasurement(i, o), !this[i]) {
			var n = this.getFirstItemSize();
			this[i] = n && n[o] || this.isotope.size["inner" + e]
		}
	}, o.getFirstItemSize = function () {
		var e = this.isotope.filteredItems[0];
		return e && e.element && t(e.element)
	}, o.layout = function () {
		this.isotope.layout.apply(this.isotope, arguments)
	}, o.getSize = function () {
		this.isotope.getSize(), this.size = this.isotope.size
	}, i.modes = {}, i.create = function (t, e) {
		function n() {
			i.apply(this, arguments)
		}
		return n.prototype = Object.create(o), n.prototype.constructor = n, e && (n.options = e), n.prototype.namespace = t, i.modes[t] = n, n
	}, i
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("masonry-layout/masonry", ["outlayer/outlayer", "get-size/get-size"], e) : "object" == typeof module && module.exports ? module.exports = e(require("outlayer"), require("get-size")) : t.Masonry = e(t.Outlayer, t.getSize)
}(window, function (t, e) {
	var i = t.create("masonry");
	i.compatOptions.fitWidth = "isFitWidth";
	var o = i.prototype;
	return o._resetLayout = function () {
		this.getSize(), this._getMeasurement("columnWidth", "outerWidth"), this._getMeasurement("gutter", "outerWidth"), this.measureColumns(), this.colYs = [];
		for (var t = 0; t < this.cols; t++) this.colYs.push(0);
		this.maxY = 0, this.horizontalColIndex = 0
	}, o.measureColumns = function () {
		if (this.getContainerWidth(), !this.columnWidth) {
			var t = this.items[0],
				i = t && t.element;
			this.columnWidth = i && e(i).outerWidth || this.containerWidth
		}
		var o = this.columnWidth += this.gutter,
			n = this.containerWidth + this.gutter,
			s = n / o,
			r = o - n % o,
			a = r && r < 1 ? "round" : "floor";
		s = Math[a](s), this.cols = Math.max(s, 1)
	}, o.getContainerWidth = function () {
		var t = this._getOption("fitWidth"),
			i = t ? this.element.parentNode : this.element,
			o = e(i);
		this.containerWidth = o && o.innerWidth
	}, o._getItemLayoutPosition = function (t) {
		t.getSize();
		var e = t.size.outerWidth % this.columnWidth,
			i = e && e < 1 ? "round" : "ceil",
			o = Math[i](t.size.outerWidth / this.columnWidth);
		o = Math.min(o, this.cols);
		for (var n = this.options.horizontalOrder ? "_getHorizontalColPosition" : "_getTopColPosition", s = this[n](o, t), r = {
				x: this.columnWidth * s.col,
				y: s.y
			}, a = s.y + t.size.outerHeight, u = o + s.col, h = s.col; h < u; h++) this.colYs[h] = a;
		return r
	}, o._getTopColPosition = function (t) {
		var e = this._getTopColGroup(t),
			i = Math.min.apply(Math, e);
		return {
			col: e.indexOf(i),
			y: i
		}
	}, o._getTopColGroup = function (t) {
		if (t < 2) return this.colYs;
		for (var e = [], i = this.cols + 1 - t, o = 0; o < i; o++) e[o] = this._getColGroupY(o, t);
		return e
	}, o._getColGroupY = function (t, e) {
		if (e < 2) return this.colYs[t];
		var i = this.colYs.slice(t, t + e);
		return Math.max.apply(Math, i)
	}, o._getHorizontalColPosition = function (t, e) {
		var i = this.horizontalColIndex % this.cols,
			o = t > 1 && i + t > this.cols;
		i = o ? 0 : i;
		var n = e.size.outerWidth && e.size.outerHeight;
		return this.horizontalColIndex = n ? i + t : this.horizontalColIndex, {
			col: i,
			y: this._getColGroupY(i, t)
		}
	}, o._manageStamp = function (t) {
		var i = e(t),
			o = this._getElementOffset(t),
			n = this._getOption("originLeft"),
			s = n ? o.left : o.right,
			r = s + i.outerWidth,
			a = Math.floor(s / this.columnWidth);
		a = Math.max(0, a);
		var u = Math.floor(r / this.columnWidth);
		u -= r % this.columnWidth ? 0 : 1, u = Math.min(this.cols - 1, u);
		for (var h = this._getOption("originTop"), d = (h ? o.top : o.bottom) + i.outerHeight, l = a; l <= u; l++) this.colYs[l] = Math.max(d, this.colYs[l])
	}, o._getContainerSize = function () {
		this.maxY = Math.max.apply(Math, this.colYs);
		var t = {
			height: this.maxY
		};
		return this._getOption("fitWidth") && (t.width = this._getContainerFitWidth()), t
	}, o._getContainerFitWidth = function () {
		for (var t = 0, e = this.cols; --e && 0 === this.colYs[e];) t++;
		return (this.cols - t) * this.columnWidth - this.gutter
	}, o.needsResizeLayout = function () {
		var t = this.containerWidth;
		return this.getContainerWidth(), t != this.containerWidth
	}, i
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("isotope-layout/js/layout-modes/masonry", ["../layout-mode", "masonry-layout/masonry"], e) : "object" == typeof module && module.exports ? module.exports = e(require("../layout-mode"), require("masonry-layout")) : e(t.Isotope.LayoutMode, t.Masonry)
}(window, function (t, e) {
	"use strict";
	var i = t.create("masonry"),
		o = i.prototype,
		n = {
			_getElementOffset: !0,
			layout: !0,
			_getMeasurement: !0
		};
	for (var s in e.prototype) n[s] || (o[s] = e.prototype[s]);
	var r = o.measureColumns;
	o.measureColumns = function () {
		this.items = this.isotope.filteredItems, r.call(this)
	};
	var a = o._getOption;
	return o._getOption = function (t) {
		return "fitWidth" == t ? void 0 !== this.options.isFitWidth ? this.options.isFitWidth : this.options.fitWidth : a.apply(this.isotope, arguments)
	}, i
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("isotope-layout/js/layout-modes/fit-rows", ["../layout-mode"], e) : "object" == typeof exports ? module.exports = e(require("../layout-mode")) : e(t.Isotope.LayoutMode)
}(window, function (t) {
	"use strict";
	var e = t.create("fitRows"),
		i = e.prototype;
	return i._resetLayout = function () {
		this.x = 0, this.y = 0, this.maxY = 0, this._getMeasurement("gutter", "outerWidth")
	}, i._getItemLayoutPosition = function (t) {
		t.getSize();
		var e = t.size.outerWidth + this.gutter,
			i = this.isotope.size.innerWidth + this.gutter;
		0 !== this.x && e + this.x > i && (this.x = 0, this.y = this.maxY);
		var o = {
			x: this.x,
			y: this.y
		};
		return this.maxY = Math.max(this.maxY, this.y + t.size.outerHeight), this.x += e, o
	}, i._getContainerSize = function () {
		return {
			height: this.maxY
		}
	}, e
}),
function (t, e) {
	"function" == typeof define && define.amd ? define("isotope-layout/js/layout-modes/vertical", ["../layout-mode"], e) : "object" == typeof module && module.exports ? module.exports = e(require("../layout-mode")) : e(t.Isotope.LayoutMode)
}(window, function (t) {
	"use strict";
	var e = t.create("vertical", {
			horizontalAlignment: 0
		}),
		i = e.prototype;
	return i._resetLayout = function () {
		this.y = 0
	}, i._getItemLayoutPosition = function (t) {
		t.getSize();
		var e = (this.isotope.size.innerWidth - t.size.outerWidth) * this.options.horizontalAlignment,
			i = this.y;
		return this.y += t.size.outerHeight, {
			x: e,
			y: i
		}
	}, i._getContainerSize = function () {
		return {
			height: this.y
		}
	}, e
}),
function (t, e) {
	"function" == typeof define && define.amd ? define(["outlayer/outlayer", "get-size/get-size", "desandro-matches-selector/matches-selector", "fizzy-ui-utils/utils", "isotope-layout/js/item", "isotope-layout/js/layout-mode", "isotope-layout/js/layout-modes/masonry", "isotope-layout/js/layout-modes/fit-rows", "isotope-layout/js/layout-modes/vertical"], function (i, o, n, s, r, a) {
		return e(t, i, o, n, s, r, a)
	}) : "object" == typeof module && module.exports ? module.exports = e(t, require("outlayer"), require("get-size"), require("desandro-matches-selector"), require("fizzy-ui-utils"), require("isotope-layout/js/item"), require("isotope-layout/js/layout-mode"), require("isotope-layout/js/layout-modes/masonry"), require("isotope-layout/js/layout-modes/fit-rows"), require("isotope-layout/js/layout-modes/vertical")) : t.Isotope = e(t, t.Outlayer, t.getSize, t.matchesSelector, t.fizzyUIUtils, t.Isotope.Item, t.Isotope.LayoutMode)
}(window, function (t, e, i, o, n, s, r) {
	function a(t, e) {
		return function (i, o) {
			for (var n = 0; n < t.length; n++) {
				var s = t[n],
					r = i.sortData[s],
					a = o.sortData[s];
				if (r > a || r < a) {
					var u = void 0 !== e[s] ? e[s] : e,
						h = u ? 1 : -1;
					return (r > a ? 1 : -1) * h
				}
			}
			return 0
		}
	}
	var u = t.jQuery,
		h = String.prototype.trim ? function (t) {
			return t.trim()
		} : function (t) {
			return t.replace(/^\s+|\s+$/g, "")
		},
		d = e.create("isotope", {
			layoutMode: "masonry",
			isJQueryFiltering: !0,
			sortAscending: !0
		});
	d.Item = s, d.LayoutMode = r;
	var l = d.prototype;
	l._create = function () {
		this.itemGUID = 0, this._sorters = {}, this._getSorters(), e.prototype._create.call(this), this.modes = {}, this.filteredItems = this.items, this.sortHistory = ["original-order"];
		for (var t in r.modes) this._initLayoutMode(t)
	}, l.reloadItems = function () {
		this.itemGUID = 0, e.prototype.reloadItems.call(this)
	}, l._itemize = function () {
		for (var t = e.prototype._itemize.apply(this, arguments), i = 0; i < t.length; i++) {
			var o = t[i];
			o.id = this.itemGUID++
		}
		return this._updateItemsSortData(t), t
	}, l._initLayoutMode = function (t) {
		var e = r.modes[t],
			i = this.options[t] || {};
		this.options[t] = e.options ? n.extend(e.options, i) : i, this.modes[t] = new e(this)
	}, l.layout = function () {
		return !this._isLayoutInited && this._getOption("initLayout") ? void this.arrange() : void this._layout()
	}, l._layout = function () {
		var t = this._getIsInstant();
		this._resetLayout(), this._manageStamps(), this.layoutItems(this.filteredItems, t), this._isLayoutInited = !0
	}, l.arrange = function (t) {
		this.option(t), this._getIsInstant();
		var e = this._filter(this.items);
		this.filteredItems = e.matches, this._bindArrangeComplete(), this._isInstant ? this._noTransition(this._hideReveal, [e]) : this._hideReveal(e), this._sort(), this._layout()
	}, l._init = l.arrange, l._hideReveal = function (t) {
		this.reveal(t.needReveal), this.hide(t.needHide)
	}, l._getIsInstant = function () {
		var t = this._getOption("layoutInstant"),
			e = void 0 !== t ? t : !this._isLayoutInited;
		return this._isInstant = e, e
	}, l._bindArrangeComplete = function () {
		function t() {
			e && i && o && n.dispatchEvent("arrangeComplete", null, [n.filteredItems])
		}
		var e, i, o, n = this;
		this.once("layoutComplete", function () {
			e = !0, t()
		}), this.once("hideComplete", function () {
			i = !0, t()
		}), this.once("revealComplete", function () {
			o = !0, t()
		})
	}, l._filter = function (t) {
		var e = this.options.filter;
		e = e || "*";
		for (var i = [], o = [], n = [], s = this._getFilterTest(e), r = 0; r < t.length; r++) {
			var a = t[r];
			if (!a.isIgnored) {
				var u = s(a);
				u && i.push(a), u && a.isHidden ? o.push(a) : u || a.isHidden || n.push(a)
			}
		}
		return {
			matches: i,
			needReveal: o,
			needHide: n
		}
	}, l._getFilterTest = function (t) {
		return u && this.options.isJQueryFiltering ? function (e) {
			return u(e.element).is(t);
		} : "function" == typeof t ? function (e) {
			return t(e.element)
		} : function (e) {
			return o(e.element, t)
		}
	}, l.updateSortData = function (t) {
		var e;
		t ? (t = n.makeArray(t), e = this.getItems(t)) : e = this.items, this._getSorters(), this._updateItemsSortData(e)
	}, l._getSorters = function () {
		var t = this.options.getSortData;
		for (var e in t) {
			var i = t[e];
			this._sorters[e] = f(i)
		}
	}, l._updateItemsSortData = function (t) {
		for (var e = t && t.length, i = 0; e && i < e; i++) {
			var o = t[i];
			o.updateSortData()
		}
	};
	var f = function () {
		function t(t) {
			if ("string" != typeof t) return t;
			var i = h(t).split(" "),
				o = i[0],
				n = o.match(/^\[(.+)\]$/),
				s = n && n[1],
				r = e(s, o),
				a = d.sortDataParsers[i[1]];
			return t = a ? function (t) {
				return t && a(r(t))
			} : function (t) {
				return t && r(t)
			}
		}

		function e(t, e) {
			return t ? function (e) {
				return e.getAttribute(t)
			} : function (t) {
				var i = t.querySelector(e);
				return i && i.textContent
			}
		}
		return t
	}();
	d.sortDataParsers = {
		parseInt: function (t) {
			return parseInt(t, 10)
		},
		parseFloat: function (t) {
			return parseFloat(t)
		}
	}, l._sort = function () {
		if (this.options.sortBy) {
			var t = n.makeArray(this.options.sortBy);
			this._getIsSameSortBy(t) || (this.sortHistory = t.concat(this.sortHistory));
			var e = a(this.sortHistory, this.options.sortAscending);
			this.filteredItems.sort(e)
		}
	}, l._getIsSameSortBy = function (t) {
		for (var e = 0; e < t.length; e++)
			if (t[e] != this.sortHistory[e]) return !1;
		return !0
	}, l._mode = function () {
		var t = this.options.layoutMode,
			e = this.modes[t];
		if (!e) throw new Error("No layout mode: " + t);
		return e.options = this.options[t], e
	}, l._resetLayout = function () {
		e.prototype._resetLayout.call(this), this._mode()._resetLayout()
	}, l._getItemLayoutPosition = function (t) {
		return this._mode()._getItemLayoutPosition(t)
	}, l._manageStamp = function (t) {
		this._mode()._manageStamp(t)
	}, l._getContainerSize = function () {
		return this._mode()._getContainerSize()
	}, l.needsResizeLayout = function () {
		return this._mode().needsResizeLayout()
	}, l.appended = function (t) {
		var e = this.addItems(t);
		if (e.length) {
			var i = this._filterRevealAdded(e);
			this.filteredItems = this.filteredItems.concat(i)
		}
	}, l.prepended = function (t) {
		var e = this._itemize(t);
		if (e.length) {
			this._resetLayout(), this._manageStamps();
			var i = this._filterRevealAdded(e);
			this.layoutItems(this.filteredItems), this.filteredItems = i.concat(this.filteredItems), this.items = e.concat(this.items)
		}
	}, l._filterRevealAdded = function (t) {
		var e = this._filter(t);
		return this.hide(e.needHide), this.reveal(e.matches), this.layoutItems(e.matches, !0), e.matches
	}, l.insert = function (t) {
		var e = this.addItems(t);
		if (e.length) {
			var i, o, n = e.length;
			for (i = 0; i < n; i++) o = e[i], this.element.appendChild(o.element);
			var s = this._filter(e).matches;
			for (i = 0; i < n; i++) e[i].isLayoutInstant = !0;
			for (this.arrange(), i = 0; i < n; i++) delete e[i].isLayoutInstant;
			this.reveal(s)
		}
	};
	var c = l.remove;
	return l.remove = function (t) {
		t = n.makeArray(t);
		var e = this.getItems(t);
		c.call(this, t);
		for (var i = e && e.length, o = 0; i && o < i; o++) {
			var s = e[o];
			n.removeFrom(this.filteredItems, s)
		}
	}, l.shuffle = function () {
		for (var t = 0; t < this.items.length; t++) {
			var e = this.items[t];
			e.sortData.random = Math.random()
		}
		this.options.sortBy = "random", this._sort(), this._layout()
	}, l._noTransition = function (t, e) {
		var i = this.options.transitionDuration;
		this.options.transitionDuration = 0;
		var o = t.apply(this, e);
		return this.options.transitionDuration = i, o
	}, l.getFilteredItemElements = function () {
		return this.filteredItems.map(function (t) {
			return t.element
		})
	}, d
});
/*!
 * Thim Simple Slider 1.0
 * Simple slider library
 * https://thimpress.com
 */
! function (a) {
	a.fn.extend({
		thim_simple_slider: function (b) {
			var c = {
					item: 3,
					itemActive: 1,
					itemSelector: ".item-event",
					align: "left",
					navigation: !0,
					pagination: !0,
					height: 400,
					activeWidth: 1170,
					itemWidth: 800,
					prev_text: "Prev",
					next_text: "Next"
				},
				b = a.extend(c, b);
			return this.each(function () {
				function n() {
					h = !0, c.navigation && (i.append('<div class="navigation"><div data-nav="prev" class="control-nav prev">' + c.prev_text + '</div><div data-nav="next" class="control-nav next">' + c.next_text + "</div></div>"), a(".control-nav", d).on("click", function () {
						var b = a(this).data("nav");
						"prev" == b ? r() : q()
					}))
				}

				function o() {
					if (c.pagination) {
						for (var b = '<div class="pagination">', e = 0; e < f; e++) b += e === g ? '<div class="item active"></div>' : '<div class="item"></div>';
						b += "</div>", i.append(b), a(".pagination .item", d).on("click", function (b) {
							a(this).hasClass("active") && b.preventDefault();
							var c = a(".pagination .item").index(this);
							a(".pagination .item", d).removeClass("active"), a(this).addClass("active"), g = c, p()
						})
					}
				}

				function p() {
					h && (m.removeClass("active-item").css({
						width: "",
						opacity: ""
					}), m.eq(g).addClass("active-item").css({
						width: k
					}).animate({
						opacity: .2
					}, 300, function () {
						m.eq(g).animate({
							opacity: 1
						}, 500)
					}));
					var b = a(".simple-item:not(.active-item)", d);
					"left" == c.align ? m.eq(g).css({
						width: k,
						left: ""
					}) : m.eq(g).css({
						width: k,
						right: ""
					}), b.css("width", l);
					for (var e = k, f = g; f < b.length; f++) {
						var i = b.eq(f);
						"left" == c.align ? i.css("left", e) : i.css("right", e), e += l
					}
					if (g > 0)
						for (var f = 0; f < g; f++) {
							var i = b.eq(f);
							"left" == c.align ? i.css("left", e) : i.css("right", e), e += l
						}
					c.pagination && a(".pagination .item", d).removeClass("active").eq(g).addClass("active")
				}

				function q() {
					g < f - 1 ? g += 1 : g = 0, p()
				}

				function r() {
					g > 0 ? g -= 1 : g = f - 1, p()
				}

				function s(b) {
					b.each(function (b, c) {
						b == g ? a(c).wrap('<div class="simple-item active-item"></div>') : a(c).wrap('<div class="simple-item"></div>')
					})
				}
				var c = b,
					d = a(this),
					e = a(c.itemSelector, d),
					f = e.length,
					g = c.itemActive - 1,
					h = !1;
				d.wrapInner('<div class="thim-simple-wrapper ' + c.align + '"><div class="wrapper"></div></div>');
				var i = a(".thim-simple-wrapper", d),
					k = (i.find(".wrapper"), c.activeWidth),
					l = c.itemWidth;
				s(e);
				var m = i.find(".simple-item");
				i.css("height", c.height), a(".simple-item", d).css("height", c.height), p(), n(), o()
			})
		}
	})
}(jQuery);
var thim_scroll = true;
var woof_js_after_ajax_done;
var can_escape = true;
(function ($) {
	'use strict';
	if (typeof LearnPress != 'undefined') {
		if (typeof LearnPress.load_lesson == 'undefined') {
			LearnPress.load_lesson = function (a, b) {
				LearnPress.$Course && LearnPress.$Course.loadLesson(a, b);
			};
		}
	}
	$.avia_utilities = $.avia_utilities || {};
	$.avia_utilities.supported = {};
	$.avia_utilities.supports = (function () {
		var div = document.createElement('div'),
			vendors = ['Khtml', 'Ms', 'Moz', 'Webkit', 'O'];
		return function (prop, vendor_overwrite) {
			if (div.style.prop !== undefined) {
				return '';
			}
			if (vendor_overwrite !== undefined) {
				vendors = vendor_overwrite;
			}
			prop = prop.replace(/^[a-z]/, function (val) {
				return val.toUpperCase();
			});
			var len = vendors.length;
			while (len--) {
				if (div.style[vendors[len] + prop] !== undefined) {
					return '-' + vendors[len].toLowerCase() + '-';
				}
			}
			return false;
		};
	}());
	(function ($, sr) {
		var debounce = function (func, threshold, execAsap) {
			var timeout;
			return function debounced() {
				var obj = this,
					args = arguments;

				function delayed() {
					if (!execAsap)
						func.apply(obj, args);
					timeout = null;
				}
				if (timeout)
					clearTimeout(timeout);
				else if (execAsap)
					func.apply(obj, args);
				timeout = setTimeout(delayed, threshold || 100);
			};
		};
		jQuery.fn[sr] = function (fn) {
			return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr);
		};
	})(jQuery, 'smartresize');
	var back_to_top = function () {
		jQuery(window).scroll(function () {
			if (jQuery(this).scrollTop() > 400) {
				jQuery('#back-to-top').addClass('active');
			} else {
				jQuery('#back-to-top').removeClass('active');
			}
		});
		jQuery('#back-to-top').on('click', function () {
			jQuery('html, body').animate({
				scrollTop: '0px'
			}, 800);
			return false;
		});
	};
	$(document).ready(function () {
		var $header = $('#masthead.header_default');
		var $content_pusher = $('#wrapper-container .content-pusher');
		$header.imagesLoaded(function () {
			var height_sticky_header = $header.outerHeight(true);
			$content_pusher.css({
				'padding-top': height_sticky_header + 'px'
			});
			$(window).resize(function () {
				var height_sticky_header = $header.outerHeight(true);
				$content_pusher.css({
					'padding-top': height_sticky_header + 'px'
				});
			});
		});
	});
	var thim_SwitchLayout = function () {
		var cookie_name = 'course_switch',
			archive = $('#thim-course-archive');
		if (archive.length > 0) {
			var data_cookie = archive.data('cookie') ? archive.data('cookie') : 'grid-layout';
			if ((!jQuery.cookie(cookie_name) && data_cookie != 'list-layout') || jQuery.cookie(cookie_name) == 'grid-layout') {
				if (archive.hasClass('thim-course-list')) {
					archive.removeClass('thim-course-list').addClass('thim-course-grid');
				}
				$('.thim-course-switch-layout > a.switchToGrid').addClass('switch-active');
			} else {
				if (archive.hasClass('thim-course-grid')) {
					archive.removeClass('thim-course-grid').addClass('thim-course-list');
				}
				$('.thim-course-switch-layout > a.switchToList').addClass('switch-active');
			}
			$(document).on('click', '.thim-course-switch-layout > a', function (event) {
				var elem = $(this),
					archive = $('#thim-course-archive');
				event.preventDefault();
				if (!elem.hasClass('switch-active')) {
					$('.thim-course-switch-layout > a').removeClass('switch-active');
					elem.addClass('switch-active');
					if (elem.hasClass('switchToGrid')) {
						archive.fadeOut(300, function () {
							archive.removeClass('thim-course-list').addClass(' thim-course-grid').fadeIn(300);
							jQuery.cookie(cookie_name, 'grid-layout', {
								expires: 3,
								path: '/'
							});
						});
					} else {
						archive.fadeOut(300, function () {
							archive.removeClass('thim-course-grid').addClass('thim-course-list').fadeIn(300);
							jQuery.cookie(cookie_name, 'list-layout', {
								expires: 3,
								path: '/'
							});
						});
					}
				}
			});
		}
	};
	var thim_Shop_SwitchLayout = function () {
		var cookie_name = 'product_list',
			archive = $('#thim-product-archive');
		if (archive.length > 0) {
			if (!jQuery.cookie(cookie_name) || jQuery.cookie(cookie_name) == 'grid-layout') {
				if (archive.hasClass('thim-product-list')) {
					archive.removeClass('thim-product-list').addClass('thim-product-grid');
				}
				$('.thim-product-switch-layout > a.switch-active').removeClass('switch-active');
				$('.thim-product-switch-layout > a.switchToGrid').addClass('switch-active');
			} else {
				if (archive.hasClass('thim-product-grid')) {
					archive.removeClass('thim-product-grid').addClass('thim-product-list');
				}
				$('.thim-product-switch-layout > a.switch-active').removeClass('switch-active');
				$('.thim-product-switch-layout > a.switchToList').addClass('switch-active');
			}
			$(document).on('click', '.thim-product-switch-layout > a', function (event) {
				var elem = $(this),
					archive = $('#thim-product-archive');
				event.preventDefault();
				if (!elem.hasClass('switch-active')) {
					$('.thim-product-switch-layout > a').removeClass('switch-active');
					elem.addClass('switch-active');
					if (elem.hasClass('switchToGrid')) {
						archive.fadeOut(300, function () {
							archive.removeClass('thim-product-list').addClass(' thim-product-grid').fadeIn(300);
							jQuery.cookie(cookie_name, 'grid-layout', {
								expires: 3,
								path: '/'
							});
						});
					} else {
						archive.fadeOut(300, function () {
							archive.removeClass('thim-product-grid').addClass('thim-product-list').fadeIn(300);
							jQuery.cookie(cookie_name, 'list-layout', {
								expires: 3,
								path: '/'
							});
						});
					}
				}
			});
		}
	};
	var thim_Blog_SwitchLayout = function () {
		var cookie_name = 'blog_layout',
			archive = $('#blog-archive'),
			switch_layout = archive.find('.switch-layout');
		if (archive.length > 0) {
			if (!jQuery.cookie(cookie_name) || jQuery.cookie(cookie_name) == 'grid-layout') {
				if (archive.hasClass('blog-list')) {
					archive.removeClass('blog-list').addClass('blog-grid');
				}
				switch_layout.find('> a.switch-active').removeClass('switch-active');
				switch_layout.find('> a.switchToGrid').addClass('switch-active');
			} else {
				if (archive.hasClass('blog-grid')) {
					archive.removeClass('blog-grid').addClass('blog-list');
				}
				switch_layout.find('> a.switch-active').removeClass('switch-active');
				switch_layout.find('> a.switchToList').addClass('switch-active');
			}
			$(document).on('click', '#blog-archive .switch-layout > a', function (event) {
				var elem = $(this),
					archive = $('#blog-archive');
				event.preventDefault();
				if (!elem.hasClass('switch-active')) {
					switch_layout.find('>a').removeClass('switch-active');
					elem.addClass('switch-active');
					if (elem.hasClass('switchToGrid')) {
						archive.fadeOut(300, function () {
							archive.removeClass('blog-list').addClass('blog-grid').fadeIn(300);
							jQuery.cookie(cookie_name, 'grid-layout', {
								expires: 3,
								path: '/'
							});
						});
					} else {
						archive.fadeOut(300, function () {
							archive.removeClass('blog-grid').addClass('blog-list').fadeIn(300);
							jQuery.cookie(cookie_name, 'list-layout', {
								expires: 3,
								path: '/'
							});
						});
					}
				}
			});
		}
	};
	var thim_post_audio = function () {
		$('.jp-jplayer').each(function () {
			var $this = $(this),
				url = $this.data('audio'),
				type = url.substr(url.lastIndexOf('.') + 1),
				player = '#' + $this.data('player'),
				audio = {};
			audio[type] = url;
			$this.jPlayer({
				ready: function () {
					$this.jPlayer('setMedia', audio);
				},
				swfPath: 'jplayer/',
				cssSelectorAncestor: player,
			});
		});
	};
	var thim_post_gallery = function () {
		$('article.format-gallery .flexslider').imagesLoaded(function () {
			$('.flexslider').flexslider({
				slideshow: true,
				animation: 'fade',
				pauseOnHover: true,
				animationSpeed: 400,
				smoothHeight: true,
				directionNav: true,
				controlNav: false,
			});
		});
	};
	var thim_quick_view = function () {
		$(document).on('click', '.quick-view', function (e) {
			$('.quick-view a').css('display', 'none');
			$(this).append('<a href="javascript:;" class="loading dark"></a>');
			var product_id = $(this).attr('data-prod');
			var data = {
				action: 'jck_quickview',
				product: product_id
			};
			$.post(ajaxurl, data, function (response) {
				$.magnificPopup.open({
					mainClass: 'my-mfp-zoom-in',
					items: {
						src: response,
						type: 'inline',
					},
					callbacks: {
						open: function () {
							$('body').addClass('thim-popup-active');
							$.magnificPopup.instance.close = function () {
								$('body').removeClass('thim-popup-active');
								$.magnificPopup.proto.close.call(this);
							};
						},
					},
				});
				$('.quick-view a').css('display', 'inline-block');
				$('.loading').remove();
				$('.product-card .wrapper').removeClass('animate');
				setTimeout(function () {
					if (typeof wc_add_to_cart_variation_params !== 'undefined') {
						$('.product-info .variations_form').each(function () {
							$(this).wc_variation_form().find('.variations select:eq(0)').change();
						});
					}
				}, 600);
			});
			e.preventDefault();
		});
	};
	var thim_miniCartHover = function () {
		jQuery(document).on('mouseenter', '.site-header .minicart_hover', function () {
			jQuery(this).next('.widget_shopping_cart_content').slideDown();
		}).on('mouseleave', '.site-header .minicart_hover', function () {
			jQuery(this).next('.widget_shopping_cart_content').delay(100).stop(true, false).slideUp();
		});
		jQuery(document).on('mouseenter', '.site-header .widget_shopping_cart_content', function () {
			jQuery(this).stop(true, false).show();
		}).on('mouseleave', '.site-header .widget_shopping_cart_content', function () {
			jQuery(this).delay(100).stop(true, false).slideUp();
		});
	};
	var thim_carousel = function () {
		if (jQuery().owlCarousel) {
			$('.thim-gallery-images').owlCarousel({
				autoPlay: false,
				singleItem: true,
				stopOnHover: true,
				pagination: true,
				autoHeight: false,
			});
			$('.thim-carousel-wrapper').each(function () {
				var item_visible = $(this).data('visible') ? parseInt($(this).data('visible')) : 4,
					item_desktopsmall = $(this).data('desktopsmall') ? parseInt($(this).data('desktopsmall')) : item_visible,
					itemsTablet = $(this).data('itemtablet') ? parseInt($(this).data('itemtablet')) : 2,
					itemsMobile = $(this).data('itemmobile') ? parseInt($(this).data('itemmobile')) : 1,
					pagination = $(this).data('pagination') ? true : false,
					navigation = $(this).data('navigation') ? true : false,
					autoplay = $(this).data('autoplay') ? parseInt($(this).data('autoplay')) : false,
					navigation_text = ($(this).data('navigation-text') && $(this).data('navigation-text') == '2') ? ['<i class=\'fa fa-long-arrow-left \'></i>', '<i class=\'fa fa-long-arrow-right \'></i>', ] : ['<i class=\'fa fa-chevron-left \'></i>', '<i class=\'fa fa-chevron-right \'></i>', ];
				$(this).owlCarousel({
					items: item_visible,
					itemsDesktop: [1200, item_visible],
					itemsDesktopSmall: [1024, item_desktopsmall],
					itemsTablet: [768, itemsTablet],
					itemsMobile: [480, itemsMobile],
					navigation: navigation,
					pagination: pagination,
					lazyLoad: true,
					autoPlay: autoplay,
					navigationText: navigation_text,
				});
			});
			$('.thim-carousel-course-categories .thim-course-slider, .thim-carousel-course-categories-tabs .thim-course-slider').each(function () {
				var item_visible = $(this).data('visible') ? parseInt($(this).data('visible')) : 7,
					item_desktop = $(this).data('desktop') ? parseInt($(this).data('desktop')) : 7,
					item_desktopsmall = $(this).data('desktopsmall') ? parseInt($(this).data('desktopsmall')) : 6,
					item_tablet = $(this).data('tablet') ? parseInt($(this).data('tablet')) : 4,
					item_mobile = $(this).data('mobile') ? parseInt($(this).data('mobile')) : 2,
					pagination = $(this).data('pagination') ? true : false,
					navigation = $(this).data('navigation') ? true : false,
					autoplay = $(this).data('autoplay') ? parseInt($(this).data('autoplay')) : false,
					is_rtl = $('body').hasClass('rtl');
				$(this).owlCarousel({
					items: item_visible,
					itemsDesktop: [1800, item_desktop],
					itemsDesktopSmall: [1024, item_desktopsmall],
					itemsTablet: [768, item_tablet],
					itemsMobile: [480, item_mobile],
					navigation: navigation,
					pagination: pagination,
					autoPlay: autoplay,
					navigationText: ['<i class=\'fa fa-chevron-left \'></i>', '<i class=\'fa fa-chevron-right \'></i>', ],
				});
			});
		}
	};
	var thim_contentslider = function () {
		$('.thim-testimonial-slider').each(function () {
			var elem = $(this),
				item_visible = parseInt(elem.data('visible')),
				item_time = parseInt(elem.data('time')),
				autoplay = elem.data('auto') ? true : false,
				item_ratio = elem.data('ratio') ? elem.data('ratio') : 1.18,
				item_padding = elem.data('padding') ? elem.data('padding') : 15,
				item_activepadding = elem.data('activepadding') ? elem.data('activepadding') : 0,
				item_width = elem.data('width') ? elem.data('width') : 100,
				mousewheel = elem.data('mousewheel') ? true : false;
			var testimonial_slider = $(this).thimContentSlider({
				items: elem,
				itemsVisible: item_visible,
				mouseWheel: mousewheel,
				autoPlay: autoplay,
				pauseTime: item_time,
				itemMaxWidth: item_width,
				itemMinWidth: item_width,
				activeItemRatio: item_ratio,
				activeItemPadding: item_activepadding,
				itemPadding: item_padding,
			});
		});
	};
	var thim_course_menu_landing = function () {
		if ($('.thim-course-menu-landing').length > 0) {
			var menu_landing = $('.thim-course-menu-landing'),
				tab_course = $('#course-landing .nav-tabs');
			var tab_active = tab_course.find('>li.active'),
				tab_item = tab_course.find('>li>a'),
				tab_landing = menu_landing.find('.thim-course-landing-tab'),
				tab_landing_item = tab_landing.find('>li>a'),
				landing_Top = ($('#course-landing').length) > 0 ? $('#course-landing').offset().top : 0,
				checkTop = ($(window).height() > landing_Top) ? $(window).height() : landing_Top;
			$('footer#colophon').addClass('has-thim-course-menu');
			if (tab_active.length > 0) {
				var active_href = tab_active.find('>a').attr('href'),
					landing_active = tab_landing.find('>li>a[href="' + active_href + '"]');
				if (landing_active.length > 0) {
					landing_active.parent().addClass('active');
				}
			}
			tab_landing_item.on('click', function (event) {
				event.preventDefault();
				var href = $(this).attr('href'),
					parent = $(this).parent();
				if (!parent.hasClass('active')) {
					tab_landing.find('li.active').removeClass('active');
					parent.addClass('active');
				}
				if (tab_course.length > 0) {
					tab_course.find('>li>a[href="' + href + '"]').trigger('click');
					$('body, html').animate({
						scrollTop: tab_course.offset().top - 50,
					}, 800);
				} else {
					$('body, html').animate({
						scrollTop: $($.attr(this, 'href')).offset().top,
					}, 500);
				}
			});
			tab_item.on('click', function () {
				var href = $(this).attr('href'),
					parent_landing = tab_landing.find('>li>a[href="' + href + '"]').parent();
				if (!parent_landing.hasClass('active')) {
					tab_landing.find('li.active').removeClass('active');
					parent_landing.addClass('active');
				}
			});
			$(window).scroll(function () {
				if ($(window).scrollTop() > checkTop) {
					$('body').addClass('course-landing-active');
				} else {
					$('body.course-landing-active').removeClass('course-landing-active');
				}
			});
		}
	};
	var thimImagepopup = function () {
		$('.thim-image-popup').magnificPopup({
			type: 'image',
			closeOnContentClick: true,
		});
	};
	$(document).on('click', '#course-curriculum-popup .popup-close', function (event) {
		event.preventDefault();
		$('#learn-press-block-content').remove();
	});
	$(function () {
		back_to_top();
		if (typeof jQuery.fn.waypoint !== 'undefined') {
			jQuery('.wpb_animate_when_almost_visible:not(.wpb_start_animation)').waypoint(function () {
				jQuery(this).addClass('wpb_start_animation');
			}, {
				offset: '85%'
			});
		}
	});

	function empty(data) {
		if (typeof (data) == 'number' || typeof (data) == 'boolean') {
			return false;
		}
		if (typeof (data) == 'undefined' || data === null) {
			return true;
		}
		if (typeof (data.length) != 'undefined') {
			return data.length === 0;
		}
		var count = 0;
		for (var i in data) {
			if (Object.prototype.hasOwnProperty.call(data, i)) {
				count++;
			}
		}
		return count === 0;
	}
	var windowWidth = window.innerWidth,
		windowHeight = window.innerHeight,
		$document = $(document),
		orientation = windowWidth > windowHeight ? 'landscape' : 'portrait';
	var TitleAnimation = {
		selector: '.article__parallax',
		initialized: false,
		animated: false,
		initialize: function () {},
		update: function () {},
	};
	$(window).on('debouncedresize', function (e) {
		windowWidth = $(window).width();
		windowHeight = $(window).height();
		TitleAnimation.initialize();
	});
	$(window).on('orientationchange', function (e) {
		setTimeout(function () {
			TitleAnimation.initialize();
		}, 300);
	});
	var latestScrollY = $('html').scrollTop() || $('body').scrollTop(),
		ticking = false;

	function updateAnimation() {
		ticking = false;
		TitleAnimation.update();
	}

	function requestScroll() {
		if (!ticking) {
			requestAnimationFrame(updateAnimation);
		}
		ticking = true;
	}
	$(window).on('scroll', function () {
		latestScrollY = $('html').scrollTop() || $('body').scrollTop();
		requestScroll();
	});
	jQuery(function ($) {
		var adminbar_height = jQuery('#wpadminbar').outerHeight();
		jQuery('.navbar-nav li a,.arrow-scroll > a').on('click', function (e) {
			if (parseInt(jQuery(window).scrollTop(), 10) < 2) {
				var height = 47;
			} else height = 0;
			var sticky_height = jQuery('#masthead').outerHeight();
			var menu_anchor = jQuery(this).attr('href');
			if (menu_anchor && menu_anchor.indexOf('#') == 0 && menu_anchor.length > 1) {
				e.preventDefault();
				$('html,body').animate({
					scrollTop: jQuery(menu_anchor).offset().top -
						adminbar_height - sticky_height + height,
				}, 850);
			}
		});
	});

	function mobilecheck() {
		var check = false;
		(function (a) {
			if (/(android|ipad|playbook|silk|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true;
		})(navigator.userAgent || navigator.vendor || window.opera);
		return check;
	}
	if (mobilecheck()) {
		window.addEventListener('load', function () {
			var main_content = document.getElementById('main-content');
			if (main_content) {
				main_content.addEventListener('touchstart', function (e) {
					jQuery('.wrapper-container').removeClass('mobile-menu-open');
				});
			}
		}, false);
	};
	if (jQuery(window).width() > 768) {
		jQuery('.navbar-nav>li.menu-item-has-children >a,.navbar-nav>li.menu-item-has-children >span,.navbar-nav>li.tc-menu-layout-builder >a,.navbar-nav>li.tc-menu-layout-builder >span').after('<span class="icon-toggle"><i class="fa fa-angle-down"></i></span>');
	} else {
		jQuery('.navbar-nav>li.menu-item-has-children:not(.current-menu-parent) >a,.navbar-nav>li.menu-item-has-children:not(.current-menu-parent) >span,.navbar-nav>li.tc-menu-layout-builder:not(.current-menu-parent) >a,.navbar-nav>li.tc-menu-layout-builder:not(.current-menu-parent) >span').after('<span class="icon-toggle"><i class="fa fa-angle-down"></i></span>');
		jQuery('.navbar-nav>li.menu-item-has-children.current-menu-parent >a,.navbar-nav>li.menu-item-has-children.current-menu-parent >span,.navbar-nav>li.tc-menu-layout-builder.current-menu-parent >a,.navbar-nav>li.tc-menu-layout-builder.current-menu-parent >span').after('<span class="icon-toggle"><i class="fa fa-angle-up"></i></span>');
	}
	jQuery('.navbar-nav>li.menu-item-has-children .icon-toggle, .navbar-nav>li.tc-menu-layout-builder .icon-toggle').on('click', function () {
		if (jQuery(this).next('.sub-menu').is(':hidden')) {
			jQuery(this).next('.sub-menu').slideDown(500, 'linear');
			jQuery(this).html('<i class="fa fa-angle-up"></i>');
		} else {
			jQuery(this).next('.sub-menu').slideUp(500, 'linear');
			jQuery(this).html('<i class="fa fa-angle-down"></i>');
		}
	});
	$(window).load(function () {
		thim_post_audio();
		thim_post_gallery();
		thim_quick_view();
		thim_miniCartHover();
		thim_carousel();
		thim_contentslider();
		thim_SwitchLayout();
		thim_Shop_SwitchLayout();
		thim_Blog_SwitchLayout();
		thimImagepopup();
		setTimeout(function () {
			TitleAnimation.initialize();
			thim_course_menu_landing();
		}, 400);
	});
})(jQuery);
(function ($) {
	var thim_quiz_index = function () {
		var question_index = $('.single-quiz .index-question'),
			quiz_total_text = $('.single-quiz .quiz-total .quiz-text');
		if (question_index.length > 0) {
			quiz_total_text.html(question_index.html());
		}
	};
	$(window).load(function () {
		$('.article__parallax').each(function (index, el) {
			$(el).parallax('50%', 0.4);
		});
		$('.images_parallax').parallax_images({
			speed: 0.5,
		});
		$(window).resize(function () {
			$('.images_parallax').each(function (index, el) {
				$(el).imagesLoaded(function () {
					var parallaxHeight = $(this).find('img').height();
					$(this).height(parallaxHeight);
				});
			});
		}).trigger('resize');
		thim_quiz_index();
		var $profile_list = $('.profile-tabs .nav-tabs>li ');
		if ($profile_list.length > 0) {
			$profile_list.addClass('thim-profile-list-' + $profile_list.length);
		}
	});
	$(document).ready(function () {
		$('.course-wishlist-box [class*=\'course-wishlist\']').on('click', function (event) {
			event.preventDefault();
			var $this = $(this);
			if ($this.hasClass('loading')) return;
			$this.addClass('loading');
			$this.toggleClass('course-wishlist');
			$this.toggleClass('course-wishlisted');
			$class = $this.attr('class');
			if ($this.hasClass('course-wishlisted')) {
				$.ajax({
					type: 'POST',
					url: window.location.href,
					dataType: 'html',
					data: {
						'lp-ajax': 'toggle_course_wishlist',
						course_id: $this.data('id'),
						nonce: $this.data('nonce'),
					},
					success: function () {
						$this.removeClass('loading');
					},
					error: function () {
						$this.removeClass('loading');
					},
				});
			}
			if ($this.hasClass('course-wishlist')) {
				$.ajax({
					type: 'POST',
					url: window.location.href,
					dataType: 'html',
					data: {
						'lp-ajax': 'toggle_course_wishlist',
						course_id: $this.data('id'),
						nonce: $this.data('nonce'),
					},
					success: function () {
						$this.removeClass('loading');
					},
					error: function () {
						$this.removeClass('loading');
					},
				});
			}
		});
		$('.video-container').on('click', '.beauty-intro .btns', function () {
			var iframe = '<iframe src="' + $(this).closest('.video-container').find('.yt-player').attr('data-video') + '" height= "' +
				$('.parallaxslider').height() + '"></iframe>';
			$(this).closest('.video-container').find('.yt-player').replaceWith(iframe);
			$(this).closest('.video-container').find('.hideClick:first').css('display', 'none');
		});
		if (!$('.add-review').length) {
			return;
		}
		var $star = $('.add-review .filled');
		var $review = $('#review-course-value');
		$star.find('li').on('mouseover', function () {
			$(this).nextAll().find('span').removeClass('fa-star').addClass('fa-star-o');
			$(this).prevAll().find('span').removeClass('fa-star-o').addClass('fa-star');
			$(this).find('span').removeClass('fa-star-o').addClass('fa-star');
			$review.val($(this).index() + 1);
		});
		$('.login-username [name="log"]').attr('placeholder', thim_js_translate.login);
		$('.login-password [name="pwd"]').attr('placeholder', thim_js_translate.password);
		$(window).scroll(function (event) {
			if (thim_scroll && thim_scroll === false) {
				event.preventDefault();
			}
		});
	});
	$(document).on('click', '#course-review-load-more', function () {
		var $button = $(this);
		if (!$button.is(':visible')) return;
		$button.addClass('loading');
		var paged = parseInt($(this).attr('data-paged')) + 1;
		$.ajax({
			type: 'POST',
			dataType: 'html',
			url: window.location.href,
			data: {
				action: 'learn_press_load_course_review',
				paged: paged,
			},
			success: function (response) {
				var $content = $(response),
					$new_review = $content.find('.course-reviews-list>li');
				$('#course-reviews .course-reviews-list').append($new_review);
				if ($content.find('#course-review-load-more').length) {
					$button.removeClass('loading').attr('data-paged', paged);
				} else {
					$button.remove();
				}
			},
		});
	});
	$(document).on('click', '.single-lp_course .course-meta .course-review .value', function () {
		var review_tab = $('.course-tabs a[href="#tab-course-review"]');
		if (review_tab.length > 0) {
			review_tab.trigger('click');
			$('body, html').animate({
				scrollTop: review_tab.offset().top - 50,
			}, 800);
		}
		var review_tab_v3 = $('.course-tabs a[href="#tab-reviews"]');
		if (review_tab_v3.length > 0) {
			review_tab_v3.trigger('click');
			$('body, html').animate({
				scrollTop: review_tab_v3.offset().top - 50,
			}, 800);
		}
	});
	var search_timer = false;

	function thimlivesearch(contain) {
		var input_search = contain.find('.courses-search-input'),
			list_search = contain.find('.courses-list-search'),
			keyword = input_search.val(),
			loading = contain.find('.fa-search,.fa-times');
		if (keyword) {
			if (keyword.length < 1) {
				return;
			}
			loading.addClass('fa-spinner fa-spin');
			$.ajax({
				type: 'POST',
				data: 'action=courses_searching&keyword=' + keyword + '&from=search',
				url: ajaxurl,
				success: function (html) {
					var data_li = '';
					var items = jQuery.parseJSON(html);
					if (!items.error) {
						$.each(items, function (index) {
							if (index == 0) {
								if (this['guid'] != '#') {
									data_li += '<li class="ui-menu-item' +
										this['id'] + ' ob-selected"><a class="ui-corner-all" href="' +
										this['guid'] + '">' + this['title'] + '</a></li>';
								} else {
									data_li += '<li class="ui-menu-item' +
										this['id'] + ' ob-selected">' +
										this['title'] + '</li>';
								}
							} else {
								data_li += '<li class="ui-menu-item' +
									this['id'] + '"><a class="ui-corner-all" href="' +
									this['guid'] + '">' + this['title'] + '</a></li>';
							}
						});
						list_search.addClass('search-visible').html('').append(data_li);
					}
					thimsearchHover();
					loading.removeClass('fa-spinner fa-spin');
				},
				error: function (html) {},
			});
		}
		list_search.html('');
	}

	function thimsearchHover() {
		$('.courses-list-search li').on('mouseenter', function () {
			$('.courses-list-search li').removeClass('ob-selected');
			$(this).addClass('ob-selected');
		});
	}
	$(document).ready(function () {
		$(document).on('click', '.thim-course-search-overlay .search-toggle', function (e) {
			e.stopPropagation();
			var parent = $(this).parent();
			$('body').addClass('thim-search-active');
			setTimeout(function () {
				parent.find('.thim-s').focus();
			}, 500);
		});
		$(document).on('click', '.search-popup-bg', function () {
			var parent = $(this).parent();
			window.clearTimeout(search_timer);
			parent.find('.courses-list-search').empty();
			parent.find('.thim-s').val('');
			$('body').removeClass('thim-search-active');
		});
		$(document).on('keyup', '.courses-search-input', function (event) {
			clearTimeout($.data(this, 'search_timer'));
			var contain = $(this).parents('.courses-searching'),
				list_search = contain.find('.courses-list-search'),
				item_search = list_search.find('>li');
			if (event.which == 13) {
				event.preventDefault();
				$(this).stop();
			} else if (event.which == 38) {
				if (navigator.userAgent.indexOf('Chrome') != -1 && parseFloat(navigator.userAgent.substring(navigator.userAgent.indexOf('Chrome') + 7).split(' ')[0]) >= 15) {
					var selected = item_search.filter('.ob-selected');
					if (item_search.length > 1) {
						item_search.removeClass('ob-selected');
						if (selected.prev().length == 0) {
							selected.siblings().last().addClass('ob-selected');
						} else {
							selected.prev().addClass('ob-selected');
						}
					}
				}
			} else if (event.which == 40) {
				if (navigator.userAgent.indexOf('Chrome') != -1 && parseFloat(navigator.userAgent.substring(navigator.userAgent.indexOf('Chrome') + 7).split(' ')[0]) >= 15) {
					var selected = item_search.filter('.ob-selected');
					if (item_search.length > 1) {
						item_search.removeClass('ob-selected');
						if (selected.next().length == 0) {
							selected.siblings().first().addClass('ob-selected');
						} else {
							selected.next().addClass('ob-selected');
						}
					}
				}
			} else if (event.which == 27) {
				if ($('body').hasClass('thim-search-active')) {
					$('body').removeClass('thim-search-active');
				}
				list_search.html('');
				$(this).val('');
				$(this).stop();
			} else {
				var search_timer = setTimeout(function () {
					thimlivesearch(contain);
				}, 500);
				$(this).data('search_timer', search_timer);
			}
		});
		$(document).on('keypress', '.courses-search-input', function (event) {
			var item_search = $(this).parents('.courses-searching').find('.courses-list-search>li');
			if (event.keyCode == 13) {
				var selected = $('.ob-selected');
				if (selected.length > 0) {
					var ob_href = selected.find('a').first().attr('href');
					window.location.href = ob_href;
				}
				event.preventDefault();
			}
			if (event.keyCode == 27) {
				if ($('body').hasClass('thim-search-active')) {
					$('body').removeClass('thim-search-active');
				}
				$('.courses-list-search').html('');
				$(this).val('');
				$(this).stop();
			}
			if (event.keyCode == 38) {
				var selected = item_search.filter('.ob-selected');
				if (item_search.length > 1) {
					item_search.removeClass('ob-selected');
					if (selected.prev().length == 0) {
						selected.siblings().last().addClass('ob-selected');
					} else {
						selected.prev().addClass('ob-selected');
					}
				}
			}
			if (event.keyCode == 40) {
				var selected = item_search.filter('.ob-selected');
				if (item_search.length > 1) {
					item_search.removeClass('ob-selected');
					if (selected.next().length == 0) {
						selected.siblings().first().addClass('ob-selected');
					} else {
						selected.next().addClass('ob-selected');
					}
				}
			}
		});
		$(document).on('click', '.courses-list-search, .courses-search-input', function (event) {
			event.stopPropagation();
		});
		$(document).on('click', 'body', function () {
			if (!$('body').hasClass('course-scroll-remove')) {
				$('body').addClass('course-scroll-remove');
				$('.courses-list-search').html('');
			}
		});
		$(window).scroll(function () {
			if ($('body').hasClass('course-scroll-remove') && $('.courses-list-search li').length > 0) {
				$('.courses-searching .courses-list-search').empty();
				$('.courses-searching .thim-s').val('');
			}
		});
		$(document).on('focus', '.courses-search-input', function () {
			if ($('body').hasClass('course-scroll-remove')) {
				$('body').removeClass('course-scroll-remove');
			}
		});
		$(document).on('click', '#popup-header .search-visible', function (e) {
			var href = $(e.target).attr('href');
			if (!href) {
				$('#popup-header .search-visible').removeClass('search-visible');
			}
		});
		$(document).on('click', '#popup-header button', function (e) {
			$('#popup-header .thim-s').trigger('focus');
		});
		$(document).on('focus', '#popup-header .thim-s', function () {
			var link = $('#popup-header .courses-list-search a');
			console.log($(this).val(), link.length);
			if ($(this).val() != '' && link.length > 0) {
				$('#popup-header .courses-list-search').addClass('search-visible');
			}
		});
		$('.wrapper-box-icon').each(function () {
			var $this = $(this);
			if ($this.attr('data-icon')) {
				var $color_icon = $('.boxes-icon i', $this).css('color');
				var $color_title = $('.heading__primary a', $this).css('color');
				var $color_icon_change = $this.attr('data-icon');
			}
			if ($this.attr('data-icon-border')) {
				var $color_icon_border = $('.boxes-icon', $this).css('border-color');
				var $color_icon_border_change = $this.attr('data-icon-border');
			}
			if ($this.attr('data-icon-bg')) {
				var $color_bg = $('.boxes-icon', $this).css('background-color');
				var $color_bg_change = $this.attr('data-icon-bg');
			}
			if ($this.attr('data-btn-bg')) {
				var $color_btn_bg = $('.smicon-read', $this).css('background-color');
				var $color_btn_border = $('.smicon-read', $this).css('border-color');
				var $color_btn_bg_text_color = $('.smicon-read', $this).css('color');
				var $color_btn_bg_change = $this.attr('data-btn-bg');
				if ($this.attr('data-text-readmore')) {
					var $color_btn_bg_text_color_change = $this.attr('data-text-readmore');
				} else {
					$color_btn_bg_text_color_change = $color_btn_bg_text_color;
				}
				$('.smicon-read', $this).on({
					'mouseenter': function () {
						if ($('#style_selector_container').length > 0) {
							if ($('.smicon-read', $this).css('background-color') != $color_btn_bg)
								$color_btn_bg = $('.smicon-read', $this).css('background-color');
						}
						$('.smicon-read', $this).css({
							'background-color': $color_btn_bg_change,
							'border-color': $color_btn_bg_change,
							'color': $color_btn_bg_text_color_change,
						});
					},
					'mouseleave': function () {
						$('.smicon-read', $this).css({
							'background-color': $color_btn_bg,
							'border-color': $color_btn_border,
							'color': $color_btn_bg_text_color,
						});
					},
				});
			}
			$($this).on({
				'mouseenter': function () {
					if ($this.attr('data-icon')) {
						$('.boxes-icon i', $this).css({
							'color': $color_icon_change
						});
						$('.heading__primary a', $this).css({
							'color': $color_icon_change
						});
					}
					if ($this.attr('data-icon-bg')) {
						if ($('#style_selector_container').length > 0) {
							if ($('.boxes-icon', $this).css('background-color') != $color_bg)
								$color_bg = $('.boxes-icon', $this).css('background-color');
						}
						$('.boxes-icon', $this).css({
							'background-color': $color_bg_change
						});
					}
					if ($this.attr('data-icon-border')) {
						$('.boxes-icon', $this).css({
							'border-color': $color_icon_border_change
						});
					}
				},
				'mouseleave': function () {
					if ($this.attr('data-icon')) {
						$('.boxes-icon i', $this).css({
							'color': $color_icon
						});
						$('.heading__primary a', $this).css({
							'color': $color_title
						});
					}
					if ($this.attr('data-icon-bg')) {
						$('.boxes-icon', $this).css({
							'background-color': $color_bg
						});
					}
					if ($this.attr('data-icon-border')) {
						$('.boxes-icon', $this).css({
							'border-color': $color_icon_border
						});
					}
				},
			});
		});
		$('.bg-video-play').on('click', function () {
			var elem = $(this),
				video = $(this).parents('.thim-widget-icon-box').find('.full-screen-video'),
				player = video.get(0);
			if (player.paused) {
				player.play();
				elem.addClass('bg-pause');
			} else {
				player.pause();
				elem.removeClass('bg-pause');
			}
		});
		$(document).on('click', '.wpcf7-form-control.wpcf7-submit', function () {
			var elem = $(this),
				form = elem.parents('.wpcf7-form');
			form.addClass('thim-sending');
			$(document).on('invalid.wpcf7', function (event) {
				form.removeClass('thim-sending');
			});
			$(document).on('spam.wpcf7', function (event) {
				form.removeClass('thim-sending');
				setTimeout(function () {
					if ($('.wpcf7-response-output').length > 0) {
						$('.wpcf7-response-output').hide();
					}
				}, 4000);
			});
			$(document).on('mailsent.wpcf7', function (event) {
				form.removeClass('thim-sending');
				setTimeout(function () {
					if ($('.wpcf7-response-output').length > 0) {
						$('.wpcf7-response-output').hide();
					}
				}, 4000);
			});
			$(document).on('mailfailed.wpcf7', function (event) {
				form.removeClass('thim-sending');
				setTimeout(function () {
					if ($('.wpcf7-response-output').length > 0) {
						$('.wpcf7-response-output').hide();
					}
				}, 4000);
			});
		});
	});
	jQuery(document).ready(function () {
		var carousels = $('.tp_event_owl_carousel');
		for (var i = 0; i < carousels.length; i++) {
			var data = $(carousels[i]).attr('data-countdown');
			var options = {
				navigation: true,
				slideSpeed: 300,
				paginationSpeed: 400,
				singleItem: true,
			};
			if (typeof data !== 'undefined') {
				data = JSON.parse(data);
				$.extend(options, data);
				$.each(options, function (k, v) {
					if (v === 'true') {
						options[k] = true;
					} else if (v === 'false') {
						options[k] = false;
					}
				});
			}
			if (typeof options.slide === 'undefined' || options.slide === true) {
				$(carousels[i]).owlCarousel(options);
			} else {
				$(carousels[i]).removeClass('owl-carousel');
			}
		}
	});
	jQuery(document).ready(function () {
		var offsetTop = 20;
		if ($('#wpadminbar').length) {
			offsetTop += $('#wpadminbar').outerHeight();
		}
		if ($('#masthead.sticky-header').length) {
			offsetTop += $('#masthead.sticky-header').outerHeight();
		}
		jQuery('#sidebar.sticky-sidebar').theiaStickySidebar({
			'containerSelector': '',
			'additionalMarginTop': offsetTop,
			'additionalMarginBottom': '0',
			'updateSidebarHeight': false,
			'minWidth': '768',
			'sidebarBehavior': 'modern',
		});
	});
	jQuery(document).ready(function () {
		$('.courses-searching form').submit(function () {
			var input_search = $(this).find('input[name=\'s\']');
			if ($.trim(input_search.val()) === '') {
				input_search.focus();
				return false;
			}
		});
		$('form#bbp-search-form1').submit(function () {
			if ($.trim($('#bbp_search').val()) === '') {
				$('#bbp_search').focus();
				return false;
			}
		});
		$('form.search-form1').submit(function () {
			var input_search = $(this).find('input[name=\'s\']');
			if ($.trim(input_search.val()) === '') {
				input_search.focus();
				return false;
			}
		});
		$('#customer_login .login').submit(function (event) {
			var elem = $(this),
				input_username = elem.find('#username'),
				input_pass = elem.find('#password');
			if (input_pass.length > 0 && input_pass.val() == '') {
				input_pass.addClass('invalid');
				event.preventDefault();
			}
			if (input_username.length > 0 && input_username.val() == '') {
				input_username.addClass('invalid');
				event.preventDefault();
			}
		});
		$('#customer_login .register').submit(function (event) {
			var elem = $(this),
				input_username = elem.find('#reg_username'),
				input_email = elem.find('#reg_email'),
				input_pass = elem.find('#reg_password'),
				input_captcha = $('#customer_login .register .captcha-result'),
				valid_email = /[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}/igm;
			if (input_captcha.length > 0) {
				var captcha_1 = parseInt(input_captcha.data('captcha1')),
					captcha_2 = parseInt(input_captcha.data('captcha2'));
				if (captcha_1 + captcha_2 != parseInt(input_captcha.val())) {
					input_captcha.addClass('invalid').val('');
					event.preventDefault();
				}
			}
			if (input_pass.length > 0 && input_pass.val() == '') {
				input_pass.addClass('invalid');
				event.preventDefault();
			}
			if (input_username.length > 0 && input_username.val() == '') {
				input_username.addClass('invalid');
				event.preventDefault();
			}
			if (input_email.length > 0 && (input_email.val() == '' || !valid_email.test(input_email.val()))) {
				input_email.addClass('invalid');
				event.preventDefault();
			}
		});
		$('form#commentform').submit(function (event) {
			var elem = $(this),
				comment = elem.find('#comment[aria-required="true"]'),
				author = elem.find('#author[aria-required="true"]'),
				url = elem.find('#url[aria-required="true"]'),
				email = elem.find('#email[aria-required="true"]'),
				valid_email = /[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}/igm;
			if (author.length > 0 && author.val() == '') {
				author.addClass('invalid');
				event.preventDefault();
			}
			if (comment.length > 0 && comment.val() == '') {
				comment.addClass('invalid');
				event.preventDefault();
			}
			if (url.length > 0 && url.val() == '') {
				url.addClass('invalid');
				event.preventDefault();
			}
			if (email.length > 0 && (email.val() == '' || !valid_email.test(email.val()))) {
				email.addClass('invalid');
				event.preventDefault();
			}
		});
		$('#customer_login .register, #reg_username, #reg_email, #reg_password').on('focus', function () {
			$(this).removeClass('invalid');
		});
		$('input.wpcf7-text, textarea.wpcf7-textarea').on('focus', function () {
			if ($(this).hasClass('wpcf7-not-valid')) {
				$(this).removeClass('wpcf7-not-valid');
			}
		});
		$('.thim-language').on({
			'mouseenter': function () {
				$(this).children('.list-lang').stop(true, false).fadeIn(250);
			},
			'mouseleave': function () {
				$(this).children('.list-lang').stop(true, false).fadeOut(250);
			},
		});
		$('#toolbar .menu li.menu-item-has-children').on({
			'mouseenter': function () {
				$(this).children('.sub-menu').stop(true, false).fadeIn(250);
			},
			'mouseleave': function () {
				$(this).children('.sub-menu').stop(true, false).fadeOut(250);
			},
		});

		function gallery_layout() {
			var $container = jQuery('.isotope-layout');
			$container.each(function () {
				var $this = jQuery(this),
					$width, $col, $width_unit, $height_unit;
				var $spacing = 10;
				$col = 6;
				if ($col != 1) {
					if (parseInt($container.width()) < 768) {
						$col = 4;
					}
					if (parseInt($container.width()) < 480) {
						$col = 2;
					}
				}
				$width_unit = Math.floor((parseInt($container.width(), 10) -
					($col - 1) * $spacing) / $col);
				$height_unit = Math.floor(parseInt($width_unit, 10));
				$this.find('.item_gallery').css({
					width: $width_unit,
				});
				if ($col == 1) {
					$height_unit = 'auto';
				}
				$this.find('.item_gallery .thim-gallery-popup').css({
					height: $height_unit,
				});
				if ($this.find('.item_gallery').hasClass('size32')) {
					if ($col > 1) {
						$this.find('.item_gallery.size32 .thim-gallery-popup').css({
							height: $height_unit * 2 + $spacing,
						});
					}
				}
				if ($this.find('.item_gallery').hasClass('size32')) {
					if ($col > 3) {
						$width = $width_unit * 4 + $spacing * 3;
						$this.find('.item_gallery.size32').css({
							width: $width,
						});
					} else {
						$width = $width_unit * 2 + $spacing * 1;
						$this.find('.item_gallery.size32').css({
							width: $width,
						});
					}
				}
				if ($this.find('.item_gallery').hasClass('size22') && $col != 1) {
					$this.find('.item_gallery.size22 .thim-gallery-popup').css({
						height: $height_unit * 2 + $spacing,
					});
				}
				if ($this.find('.item_gallery').hasClass('size22') && $col != 1) {
					$width = $width_unit * 2 + $spacing * 1;
					$this.find('.item_gallery.size22').css({
						width: $width,
					});
				}
				$this.isotope({
					itemSelector: '.item_gallery',
					masonry: {
						columnWidth: $width_unit,
						gutter: $spacing,
					},
				});
			});
		}
		gallery_layout();
		$(window).resize(function () {
			gallery_layout();
		});
		$(document).on('click', '.filter-controls .filter', function (e) {
			e.preventDefault();
			var filter = $(this).data('filter'),
				filter_wraper = $(this).parents('.thim-widget-gallery-posts').find('.wrapper-gallery-filter');
			$('.filter-controls .filter').removeClass('active');
			$(this).addClass('active');
			filter_wraper.isotope({
				filter: filter
			});
		});
		$(document).on('click', '.thim-gallery-popup', function (e) {
			e.preventDefault();
			var elem = $(this),
				post_id = elem.attr('data-id'),
				data = {
					action: 'thim_gallery_popup',
					post_id: post_id
				};
			elem.addClass('loading');
			$.post(ajaxurl, data, function (response) {
				elem.removeClass('loading');
				$('.thim-gallery-show').append(response);
				if ($('.thim-gallery-show img').length > 0) {
					$('.thim-gallery-show').magnificPopup({
						mainClass: 'my-mfp-zoom-in',
						type: 'image',
						delegate: 'a',
						showCloseBtn: false,
						gallery: {
							enabled: true,
						},
						callbacks: {
							open: function () {
								$('body').addClass('thim-popup-active');
								$.magnificPopup.instance.close = function () {
									$('.thim-gallery-show').empty();
									$('body').removeClass('thim-popup-active');
									$.magnificPopup.proto.close.call(this);
								};
							},
						},
					}).magnificPopup('open');
				} else {
					$.magnificPopup.open({
						mainClass: 'my-mfp-zoom-in',
						items: {
							src: $('.thim-gallery-show'),
							type: 'inline',
						},
						showCloseBtn: false,
						callbacks: {
							open: function () {
								$('body').addClass('thim-popup-active');
								$.magnificPopup.instance.close = function () {
									$('.thim-gallery-show').empty();
									$('body').removeClass('thim-popup-active');
									$.magnificPopup.proto.close.call(this);
								};
							},
						},
					});
				}
			});
		});
		$('.widget-button.custom_style').each(function () {
			var elem = $(this),
				old_style = elem.attr('style'),
				hover_style = elem.data('hover');
			elem.on({
				'mouseenter': function () {
					elem.attr('style', hover_style);
				},
				'mouseleave': function () {
					elem.attr('style', old_style);
				},
			});
		});
	});
	$(window).load(function () {
		$(window).resize(function () {
			thim_get_position_header_course_v2($('.content_course_2 .header_single_content .bg_header'));
			$('.thim-carousel-instructors .instructor-item').css('min-height', '');
			$('.thim-owl-carousel-post:not(.layout-3) .image').css('min-height', '');
			$('.thim-course-carousel .course-thumbnail').css('min-height', '');
			$('body.thim-demo-university-4 .thim-about-eduma, body.thim-demo-university-4 .thim-video-popup .video-info').css('min-height', '');
			if ($(window).width() < 767 || $(window).width() > 1200) {
				$('body.thim-demo-university-4 #sb_instagram .sbi_photo').css('min-height', '');
			}
			thim_get_position_header_course_v2($('.content_course_2 .header_single_content .bg_header'));
			thim_min_height_carousel($('.thim-carousel-instructors .instructor-item'));
			thim_min_height_carousel($('.thim-owl-carousel-post:not(.layout-3) .image'));
			thim_min_height_carousel($('.thim-course-carousel .course-thumbnail'));
			thim_min_height_carousel($('.thim-row-bg-border-top .thim-bg-border-top'));
			thim_min_height_carousel($('.thim-testimonial-carousel-kindergarten .item'));
			thim_min_height_carousel($('.thim-widget-carousel-categories .item .image, .thim-widget-carousel-categories .item .content-wrapper'));
			thim_min_height_content_area();
			if ($(window).width() > 767) {
				thim_min_height_carousel($('.thim-grid-posts .item-post .article-wrapper, .thim-grid-posts .item-post .article-image'));
				thim_min_height_carousel($('body.thim-demo-university-4 .thim-about-eduma, body.thim-demo-university-4 .thim-video-popup .video-info'));
			}
			if ($(window).width() > 767 && $(window).width() < 1200) {
				if ($('body.thim-demo-university-4 .thim-icon-our-programs').length) {
					var min_height = parseInt($('body.thim-demo-university-4 .thim-icon-our-programs').outerHeight() / 3);
					$('body.thim-demo-university-4 #sb_instagram .sbi_photo').css('min-height', min_height);
				}
			}
		});
	});

	function thim_get_position_header_course_v2($selector) {
		if ($(window).width() > 1025) {
			$selector.css('left', '-' +
				($(window).width() - $('.container').width()) / 2 + 'px');
			$selector.css('right', '-' +
				(($(window).width() - $('.container').width()) / 2 +
					(45 + $('.content_course_2 .course_right').width())) + 'px');
		} else {
			$selector.css('left', '-15px');
			$selector.css('right', '-' +
				(45 + $('.content_course_2 .course_right').width()) + 'px');
		}
	}

	function thim_min_height_carousel($selector) {
		var min_height = 0;
		$selector.each(function (index, val) {
			if ($(this).outerHeight() > min_height) {
				min_height = $(this).outerHeight();
			}
			if (index + 1 == $selector.length) {
				$selector.css('min-height', min_height);
			}
		});
	}

	function thim_min_height_content_area() {
		var content_area = $('#main-content .content-area'),
			footer = $('#main-content .site-footer'),
			winH = $(window).height();
		if (content_area.length > 0 && footer.length > 0) {
			content_area.css('min-height', winH - footer.height());
		}
	}
	(function (a) {
		a.fn.countTo = function (g) {
			g = g || {};
			return a(this).each(function () {
				function e(a) {
					a = b.formatter.call(h, a, b);
					f.html(a);
				}
				var b = a.extend({}, a.fn.countTo.defaults, {
						from: a(this).data('from'),
						to: a(this).data('to'),
						speed: a(this).data('speed'),
						refreshInterval: a(this).data('refresh-interval'),
						decimals: a(this).data('decimals'),
					}, g),
					j = Math.ceil(b.speed / b.refreshInterval),
					l = (b.to - b.from) / j,
					h = this,
					f = a(this),
					k = 0,
					c = b.from,
					d = f.data('countTo') || {};
				f.data('countTo', d);
				d.interval && clearInterval(d.interval);
				d.interval = setInterval(function () {
					c += l;
					k++;
					e(c);
					'function' == typeof b.onUpdate && b.onUpdate.call(h, c);
					k >= j && (f.removeData('countTo'), clearInterval(d.interval), c = b.to, 'function' == typeof b.onComplete && b.onComplete.call(h, c));
				}, b.refreshInterval);
				e(c);
			});
		};
		a.fn.countTo.defaults = {
			from: 0,
			to: 0,
			speed: 1E3,
			refreshInterval: 100,
			decimals: 0,
			formatter: function (a, e) {
				return a.toFixed(e.decimals);
			},
			onUpdate: null,
			onComplete: null,
		};
	})(jQuery);
	jQuery(window).load(function () {
		if (jQuery().waypoint) {
			jQuery('.counter-box').waypoint(function () {
				jQuery(this).find('.display-percentage').each(function () {
					var percentage = jQuery(this).data('percentage');
					jQuery(this).countTo({
						from: 0,
						to: percentage,
						refreshInterval: 40,
						speed: 2000,
					});
				});
			}, {
				triggerOnce: true,
				offset: '80%',
			});
		}
	});
	$(document).ready(function () {
		$('.thim-search-light-style').append('<a class="thim-button-down thim-click-to-bottom" href="#"><i class="fa fa-chevron-down"></i></a>');
		$(document).on('click', '.thim-button-down', function (e) {
			e.preventDefault();
			if ($('#wpadminbar').length > 0) {
				var height = parseInt($('#wpadminbar').outerHeight()) +
					parseInt($('.thim-search-light-style').outerHeight());
			} else {
				var height = parseInt($('.thim-search-light-style').outerHeight());
			}
			$('body, html').animate({
				'scrollTop': height,
			}, 600);
		});
		var html_scroll = '<div class="scroll_slider_tab"><div class="container">' + '<a href="" class="to_bottom">' + '<svg xmlns="http://www.w3.org/2000/svg"' + 'xmlns:xlink="http://www.w3.org/1999/xlink"' + 'width="18px" height="28px">' + '<path fill-rule="evenodd"  fill="rgb(255, 255, 255)"' + 'd="M16.169,2.687 C14.585,0.904 12.173,0.000 9.000,0.000 C5.827,0.000 3.415,0.904 1.831,2.687 C0.238,4.479 -0.000,6.580 -0.000,7.673 L-0.000,20.328 C-0.000,21.420 0.238,23.520 1.831,25.313 C3.415,27.096 5.827,28.000 9.000,28.000 C12.173,28.000 14.585,27.096 16.169,25.313 C17.762,23.520 18.000,21.420 18.000,20.328 L18.000,7.673 C18.000,6.580 17.762,4.479 16.169,2.687 ZM9.000,9.755 C8.342,9.755 7.808,9.242 7.808,8.611 L7.808,6.159 C7.808,5.528 8.342,5.015 9.000,5.015 C9.658,5.015 10.192,5.528 10.192,6.159 L10.192,8.611 C10.192,9.242 9.658,9.755 9.000,9.755 ZM17.059,20.328 C17.059,21.458 16.670,27.097 9.000,27.097 C1.330,27.097 0.941,21.458 0.941,20.328 L0.941,7.673 C0.941,6.566 1.315,1.138 8.529,0.911 L8.529,4.163 C7.578,4.369 6.866,5.185 6.866,6.159 L6.866,8.611 C6.866,9.585 7.578,10.401 8.529,10.607 L8.529,14.318 C8.529,14.568 8.740,14.770 9.000,14.770 C9.260,14.770 9.471,14.568 9.471,14.318 L9.471,10.607 C10.422,10.401 11.134,9.585 11.134,8.611 L11.134,6.159 C11.134,5.185 10.422,4.369 9.471,4.163 L9.471,0.911 C16.685,1.138 17.059,6.566 17.059,7.673 L17.059,20.328 Z"/>' + '</svg>' + '<i class="icon-chevron-down icon1"></i>' + '<i class="icon-chevron-down icon2"></i>' + '</a>' + '</div></div>';
		$('.have_scroll_bottom').append(html_scroll);
		$(document).on('click', '.have_scroll_bottom .scroll_slider_tab .to_bottom', function (e) {
			e.preventDefault();
			if ($('#wpadminbar').length > 0) {
				var height = parseInt($('#wpadminbar').outerHeight()) +
					parseInt($('.have_scroll_bottom').outerHeight());
			} else {
				var height = parseInt($('.have_scroll_bottom').outerHeight());
			}
			$('body, html').animate({
				'scrollTop': height,
			}, 600);
		});
		$(document).on('click', 'body.page-template-landing-page .current_page_item>a, .thim-top-landing .widget-button', function (e) {
			if ($('.thim-top-landing').length > 0) {
				e.preventDefault();
				if ($('#wpadminbar').length > 0) {
					var height = parseInt($('#wpadminbar').outerHeight()) +
						parseInt($('.thim-top-landing').outerHeight());
				} else {
					var height = parseInt($('.thim-top-landing').outerHeight());
				}
				$('body, html').animate({
					'scrollTop': height,
				}, 600);
			}
		});
	});
	$(document).ready(function () {
		$('.woof_list input[data-tax="pa_color"]').each(function () {
			$(this).css('background-color', $(this).attr('name'));
		});
		$('.woof_list input.woof_radio_term[name="pa_color"]').each(function () {
			$(this).css('background-color', $(this).data('slug'));
		});
	});
	woof_js_after_ajax_done = function () {
		$('.woof_list input[data-tax="pa_color"]').each(function () {
			$(this).css('background-color', $(this).attr('name'));
		});
		$('.woof_list input.woof_radio_term[name="pa_color"]').each(function () {
			$(this).css('background-color', $(this).data('slug'));
		});
		if ($('#thim-product-archive').hasClass('thim-product-list')) {
			$('.thim-product-switch-layout>a.switchToGrid.switch-active').removeClass('switch-active');
			$('.thim-product-switch-layout>a.switchToList').addClass('switch-active');
		} else {
			$('.thim-product-switch-layout>a.switchToList.switch-active').removeClass('switch-active');
			$('.thim-product-switch-layout>a.switchToGrid').addClass('switch-active');
		}
	};
	$(document).ready(function () {
		var tab_cat_course = $('.thim-carousel-course-categories-tabs');
		tab_cat_course.each(function () {
			tab_cat_course.find('.thim-course-slider .item').click(function (e) {
				e.preventDefault();
				tab_cat_course.find('.item_content.active').removeClass('active');
				tab_cat_course.find($(this).find('.title a').attr('href')).addClass('active');
				tab_cat_course.find('.thim-course-slider .item.active').removeClass('active');
				$(this).addClass('active');
			});
		});
		var item_input_new = $('.form_developer_course .content .yikes-easy-mc-form>label>input');
		item_input_new.focusin(function () {
			$(this).parent().find('span').css('font-size', '14px');
			$(this).parent().find('span').css('bottom', '36px');
		}).focusout(function () {
			if ($(this).val() == '') {
				$(this).parent().find('span').css('font-size', '0px');
				$(this).parent().find('span').css('bottom', '0px');
			}
		});
		var tab_course = $('.course-tabs .nav-tabs>li').length;
		if (tab_course > 0) {
			$('.course-tabs .nav-tabs>li').addClass('thim-col-' + tab_course);
		}
		$('.thim-widget-timetable .timetable-item ').each(function () {
			var elem = $(this),
				old_style = elem.attr('style'),
				hover_style = elem.data('hover');
			elem.on({
				'mouseenter': function () {
					elem.attr('style', hover_style);
				},
				'mouseleave': function () {
					elem.attr('style', old_style);
				},
			});
		});
		$('.profile-tabs').each(function () {
			var elem = $(this);
			elem.find('a[href^=#user_certificates]').on('click', function () {
				$(window).resize();
				if ($('.canvas-container').length > 0) {
					$('.canvas-container').trigger('click');
				}
			});
		});
		if (typeof LP != 'undefined') {
			LP.Hook.addAction('learn_press_receive_message', function () {
				var lesson_title = $('.course-item.item-current .course-item-title').text(),
					lesson_index = $('.course-item.item-current .index').text();
				$('#popup-header .popup-title').html('<span class="index">' + lesson_index + '</span>' +
					lesson_title);
			});
		}
		$('.thim-video-popup .button-popup').on('click', function (e) {
			var item = $(this);
			e.preventDefault();
			$.magnificPopup.open({
				items: {
					src: item.parent().parent().find('.video-content'),
					type: 'inline',
				},
				showCloseBtn: false,
				callbacks: {
					open: function () {
						$('body').addClass('thim-popup-active');
						$.magnificPopup.instance.close = function () {
							$('body').removeClass('thim-popup-active');
							$.magnificPopup.proto.close.call(this);
						};
					},
				},
			});
		});
		$('.mc4wp-form #mc4wp_email').on('focus', function () {
			$(this).parents('.mc4wp-form').addClass('focus-input');
		}).on('focusout', function () {
			$(this).parents('.mc4wp-form.focus-input').removeClass('focus-input');
		});
		$(document).on('click', '.button-retake-course, .button-finish-course', function () {
			$('.thim-box-loading-container.visible').removeClass('visible');
		});
		$(document).on('click', '.button-load-item', function () {
			$('#course-curriculum-popup').addClass('loading');
			$('.thim-box-loading-container').addClass('visible');
		});
		$('.thim-event-simple-slider').thim_simple_slider({
			item: 3,
			itemActive: 1,
			itemSelector: '.item-event',
			align: 'right',
			pagination: true,
			navigation: true,
			height: 392,
			activeWidth: 1170,
			itemWidth: 800,
			prev_text: '<i class="fa fa-long-arrow-left"></i>',
			next_text: '<i class="fa fa-long-arrow-right"></i>',
		});
		$('.width-navigation .menu-main-menu>li.menu-item').last().addClass('last-menu-item');
		if (navigator.userAgent.indexOf('Mac') > 0) {
			$('body').addClass('mac-os');
		}
		if (navigator.platform.match(/(iPhone|iPod|iPad)/i)) {
			$('body').addClass('i-os');
		}
		setTimeout(function () {
			$(window).trigger('resize');
		}, 1000);
		$(window).resize(function () {
			var get_padding1 = parseFloat($('body.rtl .vc_row-has-fill[data-vc-full-width="true"]').css('left')),
				get_padding2 = parseFloat($('body.rtl .vc_row-no-padding[data-vc-full-width="true"]').css('left'));
			if (get_padding1 != 'undefined') {
				$('body.rtl .vc_row-has-fill[data-vc-full-width="true"]').css({
					'right': get_padding1,
					'left': ''
				});
			}
			if (get_padding2 != 'undefined') {
				$('body.rtl .vc_row-no-padding[data-vc-full-width="true"]').css({
					'right': get_padding2,
					'left': ''
				});
			}
		});
		var search_time_out = null;
		$(document).on('keyup', 'body:not(.course-filter-active) .course-search-filter', function (event) {
			if (event.ctrlKey) {
				return;
			}
			if ((event.keyCode >= 48 && event.keyCode <= 90) || event.keyCode == 8 || event.keyCode == 32) {
				var elem = $(this),
					keyword = elem.val(),
					$body = $('body');
				if (search_time_out != null) clearTimeout(search_time_out);
				search_time_out = setTimeout(function () {
					elem.attr('disabled', 'disabled');
					search_time_out = null;
					$('#thim-course-archive').addClass('loading');
					var archive = elem.parents('#lp-archive-courses'),
						cateArr = [];
					if ($body.hasClass('category')) {
						var bodyClass = $body.attr('class'),
							cateClass = bodyClass.match(/category\-\d+/gi)[0],
							cateID = cateClass.split('-').pop();
						cateArr.push(cateID);
					}
					if ($('.list-cate-filter').length > 0) {
						$('.list-cate-filter input.filtered').each(function () {
							console.log(typeof $(this).val());
							if ($(this).val() !== cateID) {
								cateArr.push($(this).val());
							}
						});
					}
					$.ajax({
						url: $('#lp-archive-courses').data('allCoursesUrl'),
						type: 'POST',
						dataType: 'html',
						data: {
							s: keyword,
							ref: 'course',
							post_type: 'lp_course',
							course_orderby: $('.thim-course-order > select').val(),
							course_cate_filter: cateArr,
							course_paged: 1,
						},
						success: function (html) {
							var archive_html = $(html).find('#lp-archive-courses').html();
							archive.html(archive_html);
							$('.course-search-filter').val(keyword).trigger('focus');
							$body.removeClass('course-filter-active');
							$('.filter-loading').remove();
						},
						error: function () {
							$body.removeClass('course-filter-active');
							$('.filter-loading').remove();
						},
					});
				}, 1000);
			}
		});
		$(document).on('click', '.button-load-item', function () {
			can_escape = false;
		});
		$(document).on('keydown', function (event) {
			if (event.keyCode == 27) {
				if (typeof can_escape !== 'undefined') {
					if (can_escape === false) {
						event.preventDefault();
					}
				}
			}
		});
		$('.login-password').append('<span id="show_pass"><i class="fa fa-eye"></i></span>');
		$(document).on('click', '#show_pass', function () {
			var el = $(this),
				thim_pass = el.parents('.login-password').find('>input');
			if (el.hasClass('active')) {
				thim_pass.attr('type', 'password');
			} else {
				thim_pass.attr('type', 'text');
			}
			el.toggleClass('active');
		});
		$(document).on('click', '.content_course_2 .course_right .menu_course ul li a, .content_course_2 .thim-course-menu-landing .thim-course-landing-tab li a', function () {
			$('html, body').animate({
				scrollTop: $($(this).attr('href')).offset().top,
			}, 1000);
		});
		$(window).resize(function () {
			if ($(window).width() > 600) {
				$('footer#colophon.has-footer-bottom').css('margin-bottom', $('.footer-bottom').height());
			}
			if ($(window).width() < 768) {
				$('body.course-item-popup').addClass('full-screen-content-item');
				$('body.ltr.course-item-popup #learn-press-course-curriculum').css('left', '-300px');
				$('body.ltr.course-item-popup #learn-press-content-item').css('left', '0');
				$('body.rtl.course-item-popup #learn-press-course-curriculum').css('right', 'auto');
				$('body.rtl.course-item-popup #learn-press-content-item').css('right', 'auto');
			}
		});
	});
})(jQuery);
(function ($) {
	'use strict';

	function thim_get_url_parameters(sParam) {
		var sPageURL = window.location.search.substring(1);
		console.log(sPageURL);
		var sURLVariables = sPageURL.split('&');
		for (var i = 0; i < sURLVariables.length; i++) {
			var sParameterName = sURLVariables[i].split('=');
			if (sParameterName[0] === sParam) {
				return sParameterName[1];
			}
		}
	}
	var thim_eduma = {
		ready: function () {
			this.register_ajax();
			this.login_ajax();
			this.login_form_popup();
			this.form_submission_validate();
			this.thim_TopHeader();
			this.ctf7_input_effect();
			this.thim_course_filter();
			this.mobile_menu_toggle();
		},
		load: function () {
			this.thim_menu();
		},
		resize: function () {},
		validate_form: function (form) {
			var valid = true,
				email_valid = /[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}/igm;
			form.find('input.required').each(function () {
				if (!$(this).val()) {
					$(this).addClass('invalid');
					valid = false;
				}
				if ($(this).is(':checkbox') && !$(this).is(':checked')) {
					$(this).addClass('invalid');
					valid = false;
				}
				if ('email' === $(this).attr('type')) {
					if (!email_valid.test($(this).val())) {
						$(this).addClass('invalid');
						valid = false;
					}
				}
				if ($(this).hasClass('captcha-result')) {
					let captcha_1 = parseInt($(this).data('captcha1')),
						captcha_2 = parseInt($(this).data('captcha2'));
					if ((captcha_1 + captcha_2) !== parseInt($(this).val())) {
						$(this).addClass('invalid').val('');
						valid = false;
					}
				}
			});
			if (form.hasClass('auto_login')) {
				let $pw = form.find('input[name=password]'),
					$repeat_pw = form.find('input[name=repeat_password]');
				if ($pw.val() !== $repeat_pw.val()) {
					$pw.addClass('invalid');
					$repeat_pw.addClass('invalid');
					valid = false;
				}
			}
			$('form input.required').on('focus', function () {
				$(this).removeClass('invalid');
			});
			return valid;
		},
		login_form_popup: function () {
			$(document).on('click', 'body:not(".loggen-in") .thim-button-checkout', function (e) {
				if ($(window).width() > 767) {
					e.preventDefault();
					if ($('#thim-popup-login').length) {
						$('body').addClass('thim-popup-active');
						$('#thim-popup-login').addClass('active');
					} else {
						var redirect = $(this).data('redirect');
						window.location = redirect;
					}
				} else {
					e.preventDefault();
					var redirect = $(this).data('redirect');
					window.location = redirect;
				}
			});
			$(document).on('click', '#thim-popup-login .close-popup', function (event) {
				event.preventDefault();
				$('body').removeClass('thim-popup-active');
				$('#thim-popup-login').removeClass();
			});
			$('body .thim-login-popup a.js-show-popup').on('click', function (event) {
				event.preventDefault();
				let $popup = $('#thim-popup-login');
				$('body').addClass('thim-popup-active');
				$popup.addClass('active');
				if ($(this).hasClass('login')) {
					$popup.addClass('sign-in');
				} else {
					$popup.addClass('sign-up');
				}
			});
			$('#thim-popup-login .link-bottom a').on('click', function (e) {
				e.preventDefault();
				if ($(this).hasClass('login')) {
					$('#thim-popup-login').removeClass('sign-up').addClass('sign-in');
				} else {
					$('#thim-popup-login').removeClass('sign-in').addClass('sign-up');
				}
			});
			$('body:not(".logged-in") .enroll-course .button-enroll-course, body:not(".logged-in") form.purchase-course:not(".guest_checkout") .button:not(.button-add-to-cart)').on('click', function (e) {
				e.preventDefault();
				if ($('body').hasClass('thim-popup-feature')) {
					$('.thim-link-login.thim-login-popup .login').trigger('click');
				} else {
					window.location.href = $(this).parent().find('input[name=redirect_to]').val();
				}
			});
			$(document).on('click', '#thim-popup-login', function (e) {
				if ($(e.target).attr('id') === 'thim-popup-login') {
					$('body').removeClass('thim-popup-active');
					$('#thim-popup-login').removeClass();
				}
			});
		},
		register_ajax: function () {
			$('#thim-popup-login form[name=registerformpopup]').on('submit', function (e) {
				e.preventDefault();
				if (!thim_eduma.validate_form($(this))) {
					return false;
				}
				var $form = $(this),
					data = {
						action: 'thim_register_ajax',
						data: $form.serialize() + '&wp-submit=' +
							$form.find('input[type=submit]').val(),
						register_security: $form.find('#register_security').val(),
					},
					redirect_url = $form.find('input[name=redirect_to]').val(),
					$elem = $('#thim-popup-login .thim-login-container');
				$elem.addClass('loading');
				$elem.find('.message').slideDown().remove();
				$.ajax({
					type: 'POST',
					url: ajaxurl,
					data: data,
					success: function (response) {
						$elem.removeClass('loading');
						if (typeof response.data !== 'undefined') {
							$elem.find('.popup-message').html(response.data.message);
						}
						if (response.success === true) {
							if ($form.hasClass('auto_login')) {
								window.location.href = redirect_url;
							}
						} else {
							var $captchaIframe = $('#thim-popup-login .gglcptch iframe');
							if ($captchaIframe.length > 0) {
								$captchaIframe.attr('src', $captchaIframe.attr('src'));
							}
						}
					},
				});
			});
		},
		login_ajax: function () {
			$('#thim-popup-login form[name=loginpopopform]').submit(function (event) {
				event.preventDefault();
				if (!thim_eduma.validate_form($(this))) {
					return false;
				}
				var form = $(this),
					$elem = $('#thim-popup-login .thim-login-container'),
					wp_submit = $elem.find('input[type=submit]').val();
				$elem.addClass('loading');
				$elem.find('.message').slideDown().remove();
				var data = {
					action: 'thim_login_ajax',
					data: form.serialize() + '&wp-submit=' + wp_submit,
				};
				$.post(ajaxurl, data, function (response) {
					try {
						response = JSON.parse(response);
						$elem.find('.thim-login').append(response.message);
						if (response.code == '1') {
							if (response.redirect) {
								if (window.location.href == response.redirect) {
									location.reload();
								} else {
									window.location.href = response.redirect;
								}
							} else {
								location.reload();
							}
						} else {
							var $captchaIframe = $('#thim-popup-login .gglcptch iframe');
							if ($captchaIframe.length > 0) {
								$captchaIframe.attr('src', $captchaIframe.attr('src'));
							}
						}
					} catch (e) {
						return false;
					}
					$elem.removeClass('loading');
				});
				return false;
			});
		},
		form_submission_validate: function () {
			$('.form-submission-login form[name=loginform]').on('submit', function (e) {
				if (!thim_eduma.validate_form($(this))) {
					e.preventDefault();
					return false;
				}
			});
			$('.form-submission-register form[name=registerform]').on('submit', function (e) {
				if (!thim_eduma.validate_form($(this))) {
					e.preventDefault();
					return false;
				}
			});
			$('.form-submission-lost-password form[name=lostpasswordform]').on('submit', function (e) {
				if (!thim_eduma.validate_form($(this))) {
					e.preventDefault();
					return false;
				}
			});
		},
		thim_TopHeader: function () {
			var header = $('#masthead'),
				height_sticky_header = header.outerHeight(true),
				content_pusher = $('#wrapper-container .content-pusher'),
				top_site_main = $('#wrapper-container .top_site_main');
			if (header.hasClass('header_overlay')) {
				top_site_main.css({
					'padding-top': height_sticky_header + 'px'
				});
				$(window).resize(function () {
					let height_sticky_header = header.outerHeight(true);
					top_site_main.css({
						'padding-top': height_sticky_header + 'px'
					});
				});
			} else {
				content_pusher.css({
					'padding-top': height_sticky_header + 'px'
				});
				$(window).resize(function () {
					let height_sticky_header = header.outerHeight(true);
					content_pusher.css({
						'padding-top': height_sticky_header + 'px'
					});
				});
			}
		},
		ctf7_input_effect: function () {
			let $ctf7_edtech = $('.form_developer_course'),
				$item_input = $ctf7_edtech.find('.field_item input'),
				$submit_wrapper = $ctf7_edtech.find('.submit_row');
			$item_input.focus(function () {
				$(this).parent().addClass('focusing');
			}).blur(function () {
				$(this).parent().removeClass('focusing');
			});
			$submit_wrapper.on('click', function () {
				$(this).closest('form').submit();
			});
		},
		thim_course_filter: function () {
			let $body = $('body');
			if (!$body.hasClass('learnpress') || !$body.hasClass('archive')) {
				return;
			}
			let ajaxCall = function (data) {
				return $.ajax({
					url: $('#lp-archive-courses').data('allCoursesUrl'),
					type: 'POST',
					data: data,
					dataType: 'html',
					beforeSend: function () {
						$('#thim-course-archive').addClass('loading');
					},
				}).fail(function () {
					$('#thim-course-archive').removeClass('loading');
				}).done(function (data) {
					let $document = $($.parseHTML(data));
					$('#thim-course-archive').replaceWith($document.find('#thim-course-archive'));
					$('.learn-press-pagination ul.page-numbers').replaceWith($document.find('.learn-press-pagination ul.page-numbers'));
					$('.thim-course-top .course-index span').replaceWith($document.find('.thim-course-top .course-index span'));
				});
			};
			let sendData = {
				s: '',
				ref: 'course',
				post_type: 'lp_course',
				course_orderby: 'newly-published',
				course_paged: 1,
			};
			$(document).on('change', '.thim-course-order > select', function () {
				sendData.s = $('.courses-searching .course-search-filter').val();
				sendData.course_orderby = $(this).val();
				sendData.course_paged = 1;
				ajaxCall(sendData);
			});
			$(document).on('click', '#lp-archive-courses > .learn-press-pagination a.page-numbers', function (e) {
				e.preventDefault();
				$('html, body').animate({
					'scrollTop': $('.site-content').offset().top - 140,
				}, 1000);
				let url = $(this).attr('href'),
					arr = url.split('/'),
					pageNum = arr[arr.indexOf('page') + 1],
					paged = pageNum ? pageNum : 1,
					cateArr = [],
					instructorArr = [];
				$('form.thim-course-filter').find('input.filtered').each(function () {
					switch ($(this).attr('name')) {
						case 'course-cate-filter':
							cateArr.push($(this).val());
							break;
						case 'course-instructor-filter':
							instructorArr.push($(this).val());
							break;
						case 'course-price-filter':
							sendData.course_price_filter = $(this).val();
							break;
						default:
							break;
					}
				});
				if ($body.hasClass('category') && $('.list-cate-filter').length <= 0) {
					let bodyClass = $body.attr('class'),
						cateClass = bodyClass.match(/category\-\d+/gi)[0],
						cateID = cateClass.split('-').pop();
					cateArr.push(cateID);
				}
				sendData.course_cate_filter = cateArr;
				sendData.course_instructor_filter = instructorArr;
				sendData.s = $('.courses-searching .course-search-filter').val();
				sendData.course_orderby = $('.thim-course-order > select').val();
				sendData.course_paged = paged;
				ajaxCall(sendData);
			});
			$('form.thim-course-filter').on('submit', function (e) {
				e.preventDefault();
				let formData = $(this).serializeArray(),
					cateArr = [],
					instructorArr = [];
				if (!formData.length) {
					return;
				}
				$('html, body').animate({
					'scrollTop': $('.site-content').offset().top - 140,
				}, 1000);
				$(this).find('input').each(function () {
					let form_input = $(this);
					form_input.removeClass('filtered');
					if (form_input.is(':checked')) {
						form_input.addClass('filtered');
					}
				});
				$.each(formData, function (index, filter) {
					switch (filter.name) {
						case 'course-cate-filter':
							cateArr.push(filter.value);
							break;
						case 'course-instructor-filter':
							instructorArr.push(filter.value);
							break;
						case 'course-price-filter':
							sendData.course_price_filter = filter.value;
							break;
						default:
							break;
					}
				});
				if ($body.hasClass('category') && $('.list-cate-filter').length <= 0) {
					let bodyClass = $body.attr('class'),
						cateClass = bodyClass.match(/category\-\d+/gi)[0],
						cateID = cateClass.split('-').pop();
					cateArr.push(cateID);
				}
				sendData.course_cate_filter = cateArr;
				sendData.course_instructor_filter = instructorArr;
				sendData.course_paged = 1;
				ajaxCall(sendData);
			});
		},
		mobile_menu_toggle: function () {

		    $(document).click(function (e) {
		        if (!$(e.target).is('.mobile-menu-inner, .mobile-menu-wrapper')) {
		            $('body').removeClass('mobile-menu-open');
		        }
		    });

			$(document).on('click', '.menu-mobile-effect', function (e) {
				e.stopPropagation();
				$('body').toggleClass('mobile-menu-open');
			});

			$(document).on('click', '.mobile-menu-wrapper', function (e) {
			    e.stopPropagation();
				//$('body').removeClass('mobile-menu-open');
			});
			$(document).on('click', '.mobile-menu-inner', function (e) {
				e.stopPropagation();
			});

			$(document).on('click', '.mobile-menu-close', function (e) {
			    $('body').removeClass('mobile-menu-open');
			});
		},
		thim_menu: function () {
			var $header = $('#masthead.sticky-header'),
				off_Top = ($('.content-pusher').length > 0) ? $('.content-pusher').offset().top : 0,
				menuH = $header.outerHeight(),
				latestScroll = 0;
			if ($(window).scrollTop() > 2) {
				$header.removeClass('affix-top').addClass('affix');
			}
			$(window).scroll(function () {
				var current = $(this).scrollTop();
				if (current > 2) {
					$header.removeClass('affix-top').addClass('affix');
				} else {
					$header.removeClass('affix').addClass('affix-top');
				}
				if (current > latestScroll && current > menuH + off_Top) {
					if (!$header.hasClass('menu-hidden')) {
						$header.addClass('menu-hidden');
					}
				} else {
					if ($header.hasClass('menu-hidden')) {
						$header.removeClass('menu-hidden');
					}
				}
				latestScroll = current;
			});
			$('.wrapper-container:not(.mobile-menu-open) .site-header .navbar-nav >li,.wrapper-container:not(.mobile-menu-open) .site-header .navbar-nav li,.site-header .navbar-nav li ul li').on({
				'mouseenter': function () {
					$(this).children('.sub-menu').stop(true, false).fadeIn(250);
				},
				'mouseleave': function () {
					$(this).children('.sub-menu').stop(true, false).fadeOut(250);
				},
			});
			let $headerLayout = $('header#masthead');
			let magicLine = function () {
				if ($(window).width() > 768) {
					var menu_active = $('#masthead .navbar-nav>li.menu-item.current-menu-item,#masthead .navbar-nav>li.menu-item.current-menu-parent, #masthead .navbar-nav>li.menu-item.current-menu-ancestor');
					if (menu_active.length > 0) {
						menu_active.before('<span id="magic-line"></span>');
						var menu_active_child = menu_active.find('>a,>span.disable_link,>span.tc-menu-inner'),
							menu_left = menu_active.position().left,
							menu_child_left = parseInt(menu_active_child.css('padding-left')),
							magic = $('#magic-line');
						magic.width(menu_active_child.width()).css('left', Math.round(menu_child_left + menu_left)).data('magic-width', magic.width()).data('magic-left', magic.position().left);
					} else {
						var first_menu = $('#masthead .navbar-nav>li.menu-item:first-child');
						first_menu.before('<span id="magic-line"></span>');
						var magic = $('#magic-line');
						magic.data('magic-width', 0);
					}
					var nav_H = parseInt($('.site-header .navigation').outerHeight());
					magic.css('bottom', nav_H - (nav_H - 90) / 2 - 64);
					$('#masthead .navbar-nav>li.menu-item').on({
						'mouseenter': function () {
							var elem = $(this).find('>a,>span.disable_link,>span.tc-menu-inner'),
								new_width = elem.width(),
								parent_left = elem.parent().position().left,
								left = parseInt(elem.css('padding-left'));
							if (!magic.data('magic-left')) {
								magic.css('left', Math.round(parent_left + left));
								magic.data('magic-left', 'auto');
							}
							magic.stop().animate({
								left: Math.round(parent_left + left),
								width: new_width,
							});
						},
						'mouseleave': function () {
							magic.stop().animate({
								left: magic.data('magic-left'),
								width: magic.data('magic-width'),
							});
						},
					});
				}
			};
			if (!$headerLayout.hasClass('header_v4')) {
				magicLine();
			}
			var subMenuPosition = function (menuItem) {
				var $menuItem = menuItem,
					$container = $menuItem.closest('.container, .header_full'),
					$subMenu = $menuItem.find('>.sub-menu'),
					$menuItemWidth = $menuItem.width(),
					$containerWidth = $container.width(),
					$subMenuWidth = $subMenu.width(),
					$subMenuDistance = $subMenuWidth / 2,
					paddingContainer = 15;
				if ($('body').hasClass('rtl')) {
					var $menuItemDistance = $menuItem.offset().left - ($menuItemWidth / 2);
					if ($menuItemDistance < $subMenuDistance) {
						var leftPosition = $menuItem.offset().left - $container.offset().left - paddingContainer;
						$subMenu.css({
							'left': -leftPosition,
							'transform': 'translateX(0)',
						});
					}
				} else {
					var $menuItemDistance = $containerWidth - ($menuItem.offset().left - $container.offset().left) -
						($menuItemWidth / 2);
					console.log($subMenu.offset());
					if ($menuItemDistance < $subMenuDistance) {
						var rightPosition = $menuItemDistance - ($menuItemWidth / 2) + paddingContainer;
						$subMenu.css({
							'right': -rightPosition,
							'transform': 'translateX(0)',
						});
					}
				}
			};
			if ($headerLayout.hasClass('header_v1')) {
				var $menuItemRoot = $headerLayout.find('.menu-item.widget_area:not(.dropdown_full_width), .menu-item.multicolumn:not(.dropdown_full_width), .navbar-nav>li.tc-menu-layout-column, .navbar-nav>li.tc-menu-layout-builder');
				$menuItemRoot.each(function () {});
			}
		},
	};
	$(document).ready(function () {
		thim_eduma.ready();
	});
	$(window).load(function () {
		thim_eduma.load();
	});
	$(window).resize(function () {
		thim_eduma.resize();
	});
})(jQuery);
! function (a, b) {
	"use strict";

	function c() {
		if (!e) {
			e = !0;
			var a, c, d, f, g = -1 !== navigator.appVersion.indexOf("MSIE 10"),
				h = !!navigator.userAgent.match(/Trident.*rv:11\./),
				i = b.querySelectorAll("iframe.wp-embedded-content");
			for (c = 0; c < i.length; c++) {
				if (d = i[c], !d.getAttribute("data-secret")) f = Math.random().toString(36).substr(2, 10), d.src += "#?secret=" + f, d.setAttribute("data-secret", f);
				if (g || h) a = d.cloneNode(!0), a.removeAttribute("security"), d.parentNode.replaceChild(a, d)
			}
		}
	}
	var d = !1,
		e = !1;
	if (b.querySelector)
		if (a.addEventListener) d = !0;
	if (a.wp = a.wp || {}, !a.wp.receiveEmbedMessage)
		if (a.wp.receiveEmbedMessage = function (c) {
				var d = c.data;
				if (d)
					if (d.secret || d.message || d.value)
						if (!/[^a-zA-Z0-9]/.test(d.secret)) {
							var e, f, g, h, i, j = b.querySelectorAll('iframe[data-secret="' + d.secret + '"]'),
								k = b.querySelectorAll('blockquote[data-secret="' + d.secret + '"]');
							for (e = 0; e < k.length; e++) k[e].style.display = "none";
							for (e = 0; e < j.length; e++)
								if (f = j[e], c.source === f.contentWindow) {
									if (f.removeAttribute("style"), "height" === d.message) {
										if (g = parseInt(d.value, 10), g > 1e3) g = 1e3;
										else if (~~g < 200) g = 200;
										f.height = g
									}
									if ("link" === d.message)
										if (h = b.createElement("a"), i = b.createElement("a"), h.href = f.getAttribute("src"), i.href = d.value, i.host === h.host)
											if (b.activeElement === f) a.top.location.href = d.value
								} else;
						}
			}, d) a.addEventListener("message", a.wp.receiveEmbedMessage, !1), b.addEventListener("DOMContentLoaded", c, !1), a.addEventListener("load", c, !1)
}(window, document);
/*!
 * jQuery Cookie Plugin v1.4.1
 * https://github.com/carhartl/jquery-cookie
 *
 * Copyright 2013 Klaus Hartl
 * Released under the MIT license
 */
! function (a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : a("object" == typeof exports ? require("jquery") : jQuery)
}(function (a) {
	function b(a) {
		return h.raw ? a : encodeURIComponent(a)
	}

	function c(a) {
		return h.raw ? a : decodeURIComponent(a)
	}

	function d(a) {
		return b(h.json ? JSON.stringify(a) : String(a))
	}

	function e(a) {
		0 === a.indexOf('"') && (a = a.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, "\\"));
		try {
			return a = decodeURIComponent(a.replace(g, " ")), h.json ? JSON.parse(a) : a
		} catch (b) {}
	}

	function f(b, c) {
		var d = h.raw ? b : e(b);
		return a.isFunction(c) ? c(d) : d
	}
	var g = /\+/g,
		h = a.cookie = function (e, g, i) {
			if (void 0 !== g && !a.isFunction(g)) {
				if (i = a.extend({}, h.defaults, i), "number" == typeof i.expires) {
					var j = i.expires,
						k = i.expires = new Date;
					k.setTime(+k + 864e5 * j)
				}
				return document.cookie = [b(e), "=", d(g), i.expires ? "; expires=" + i.expires.toUTCString() : "", i.path ? "; path=" + i.path : "", i.domain ? "; domain=" + i.domain : "", i.secure ? "; secure" : ""].join("")
			}
			for (var l = e ? void 0 : {}, m = document.cookie ? document.cookie.split("; ") : [], n = 0, o = m.length; o > n; n++) {
				var p = m[n].split("="),
					q = c(p.shift()),
					r = p.join("=");
				if (e && e === q) {
					l = f(r, g);
					break
				}
				e || void 0 === (r = f(r)) || (l[q] = r)
			}
			return l
		};
	h.defaults = {}, a.removeCookie = function (b, c) {
		return void 0 === a.cookie(b) ? !1 : (a.cookie(b, "", a.extend({}, c, {
			expires: -1
		})), !a.cookie(b))
	}
});