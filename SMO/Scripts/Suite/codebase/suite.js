/*
@license

dhtmlxSuite v.7.2.5 Evaluation

This software is covered by DHTMLX Evaluation License and purposed only for evaluation.
Contact sales@dhtmlx.com to get Commercial or Enterprise license.
Usage without proper license is prohibited.

(c) XB Software.

*/
if (window.dhx && (window.dhx_legacy = dhx, delete window.dhx), function (t, e) {
	"object" == typeof exports && "object" == typeof module ? module.exports = e() : "function" == typeof define && define.amd ? define([], e) : "object" == typeof exports ? exports.dhx = e() : t.dhx = e()
}(window, function () {
	return n = {}, o.m = i = [function (t, o, n) {
		"use strict";
		(function (t) {
			Object.defineProperty(o, "__esModule", {
				value: !0
			});
			var e = n(143);

			function i(i) {
				function e(t) {
					var e = t.el.offsetHeight,
						t = t.el.offsetWidth;
					i(t, e)
				}
				var n = window.ResizeObserver;
				return n ? o.el("div.dhx-resize-observer", {
					_hooks: {
						didInsert: function (t) {
							new n(function () {
								return e(t)
							}).observe(t.el)
						}
					}
				}) : o.el("iframe.dhx-resize-observer", {
					_hooks: {
						didInsert: function (t) {
							t.el.contentWindow.onresize = function () {
								return e(t)
							}, e(t)
						}
					}
				})
			}
			o.el = e.defineElement, o.sv = e.defineSvgElement, o.view = e.defineView, o.create = e.createView, o.inject = e.injectView, o.KEYED_LIST = e.KEYED_LIST, o.disableHelp = function () {
				e.DEVMODE.mutations = !1, e.DEVMODE.warnings = !1, e.DEVMODE.verbose = !1, e.DEVMODE.UNKEYED_INPUT = !1
			}, o.resizer = i, o.resizeHandler = function (t, e) {
				return o.create({
					render: function () {
						return i(e)
					}
				}).mount(t)
			}, o.awaitRedraw = function () {
				return new t(function (t) {
					requestAnimationFrame(function () {
						t()
					})
				})
			}
		}).call(this, n(15))
	}, function (t, e, i) {
		"use strict";
		var o = this && this.__assign || function () {
			return (o = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(2),
			r = (new Date).valueOf();
		e.uid = function () {
			return "u" + r++
		}, e.extend = function t(e, i, n) {
			if (void 0 === n && (n = !0), i)
				for (var o in i) {
					var r = i[o],
						s = e[o];
					void 0 === r ? delete e[o] : !n || "object" != typeof s || s instanceof Date || s instanceof Array ? e[o] = r : t(s, r)
				}
			return e
		}, e.copy = function (t, e) {
			var i, n = {};
			for (i in t) e && i.startsWith("$") || (n[i] = t[i]);
			return n
		}, e.naturalSort = function (t) {
			return t.sort(function (t, e) {
				return "string" == typeof t ? t.localeCompare(e) : t - e
			})
		}, e.findIndex = function (t, e) {
			for (var i = t.length, n = 0; n < i; n++)
				if (e(t[n])) return n;
			return -1
		}, e.isEqualString = function (t, e) {
			if (t = t.toString(), e = e.toString(), t.length > e.length) return !1;
			for (var i = 0; i < t.length; i++)
				if (t[i].toLowerCase() !== e[i].toLowerCase()) return !1;
			return !0
		}, e.singleOuterClick = function (e) {
			var i = function (t) {
				e(t) && document.removeEventListener("click", i)
			};
			document.addEventListener("click", i)
		}, e.detectWidgetClick = function (e, i) {
			function t(t) {
				return i(n.locate(t, "dhx_widget_id") === e)
			}
			return document.addEventListener("click", t),
				function () {
					return document.removeEventListener("click", t)
				}
		}, e.unwrapBox = function (t) {
			return Array.isArray(t) ? t[0] : t
		}, e.wrapBox = function (t) {
			return Array.isArray(t) ? t : [t]
		}, e.isDefined = function (t) {
			return null != t
		}, e.range = function (t, e) {
			if (e < t) return [];
			for (var i = []; t <= e;) i.push(t++);
			return i
		}, e.isNumeric = function (t) {
			return !isNaN(t - parseFloat(t))
		}, e.downloadFile = function (t, e, i) {
			void 0 === i && (i = "text/plain");
			var n, o, i = new Blob([t], {
				type: i
			});
			window.navigator.msSaveOrOpenBlob ? window.navigator.msSaveOrOpenBlob(i, e) : (n = document.createElement("a"), o = URL.createObjectURL(i), n.href = o, n.download = e, document.body.appendChild(n), n.click(), setTimeout(function () {
				document.body.removeChild(n), window.URL.revokeObjectURL(o)
			}, 0))
		}, e.debounce = function (o, r, s) {
			var a;
			return function () {
				for (var t = this, e = [], i = 0; i < arguments.length; i++) e[i] = arguments[i];
				var n = s && !a;
				clearTimeout(a), a = setTimeout(function () {
					a = null, s || o.apply(t, e)
				}, r), n && o.apply(this, e)
			}
		}, e.compare = function t(e, i) {
			for (var n in e) {
				if (e.hasOwnProperty(n) !== i.hasOwnProperty(n)) return !1;
				switch (typeof e[n]) {
					case "object":
						if (!t(e[n], i[n])) return !1;
						break;
					case "function":
						if (void 0 === i[n] || "compare" !== n && e[n].toString() !== i[n].toString()) return !1;
						break;
					default:
						if (e[n] !== i[n]) return !1
				}
			}
			for (var n in i)
				if (void 0 === e[n]) return !1;
			return !0
		}, e.isType = function (t) {
			return ((Object.prototype.toString.call(t).match(/^\[object (\S+?)\]$/) || [])[1] || "undefined").toLowerCase()
		}, e.isEmptyObj = function (t) {
			for (var e in t) return !1;
			return !0
		}, e.getMaxArrayNymber = function (t) {
			if (t.length) {
				for (var e = -1 / 0, i = 0, n = t.length; i < n; i++) t[i] > e && (e = t[i]);
				return e
			}
		}, e.getMinArrayNymber = function (t) {
			if (t.length) {
				for (var e = 1 / 0, i = 0, n = t.length; i < n; i++) t[i] < e && (e = t[i]);
				return e
			}
		}, e.getStringWidth = function (t, e) {
			e = o({
				font: "normal 14px Roboto",
				lineHeight: 20
			}, e);
			var i = document.createElement("canvas"),
				n = i.getContext("2d");
			e.font && (n.font = e.font);
			t = n.measureText(t).width;
			return i.remove(), t
		}, e.rgbToHex = function (t) {
			if ("#" === t.substr(0, 1)) return t;
			t = /(.*?)rgb[a]?\((\d+), *(\d+), *(\d+),* *([\d+.]*)\)/.exec(t);
			return "#" + parseInt(t[2], 10).toString(16).padStart(2, "0") + parseInt(t[3], 10).toString(16).padStart(2, "0") + parseInt(t[4], 10).toString(16).padStart(2, "0")
		}
	}, function (t, e, i) {
		"use strict";
		var c = this && this.__assign || function () {
			return (c = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};

		function n(t, e, i) {
			for (void 0 === e && (e = "dhx_id"), void 0 === i && (i = "target"), t instanceof Event && (t = t[i]); t;) {
				if (t.getAttribute && t.getAttribute(e)) return t;
				t = t.parentNode
			}
		}
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.toNode = function (t) {
			var e;
			return "string" == typeof t ? document.getElementById(t) || document.querySelector(t) || (null === (e = document.querySelector("[dhx_root_id=" + t + "]")) || void 0 === e ? void 0 : e.parentElement) || document.body : t || document.body
		}, e.eventHandler = function (s, a, l) {
			var c = Object.keys(a);
			return function (t) {
				var e = s(t);
				if (void 0 !== e) {
					var i = t.target;
					t: for (; i;) {
						var n = i.getAttribute && i.getAttribute("class") || "";
						if (n.length)
							for (var o = n.split(" "), r = 0; r < c.length; r++)
								if (o.includes(c[r])) {
									if (!1 === a[c[r]](t, e)) return !1;
									break t
								} i = i.parentNode
					}
				}
				return l && l(t), !0
			}
		}, e.locateNode = n, e.locate = function (t, e) {
			return void 0 === e && (e = "dhx_id"), (t = n(t, e)) ? t.getAttribute(e) : ""
		}, e.locateNodeByClassName = function (t, e) {
			for (t instanceof Event && (t = t.target); t;) {
				if (e) {
					if (t.classList && t.classList.contains(e)) return t
				} else if (t.getAttribute && t.getAttribute("dhx_id")) return t;
				t = t.parentNode
			}
		}, e.getBox = function (t) {
			var e = t.getBoundingClientRect(),
				i = document.body,
				n = window.pageYOffset || i.scrollTop,
				t = window.pageXOffset || i.scrollLeft;
			return {
				top: e.top + n,
				left: e.left + t,
				right: i.offsetWidth - e.right,
				bottom: i.offsetHeight - e.bottom,
				width: e.right - e.left,
				height: e.bottom - e.top
			}
		};
		var o = -1;
		e.getScrollbarWidth = function () {
			if (-1 < o) return o;
			var t = document.createElement("div");
			return document.body.appendChild(t), t.style.cssText = "position: absolute;left: -99999px;overflow:scroll;width: 100px;height: 100px;", o = t.offsetWidth - t.clientWidth, document.body.removeChild(t), o
		};
		var r = -1;

		function s(t) {
			t = t.getBoundingClientRect();
			return {
				left: t.left + window.pageXOffset,
				right: t.right + window.pageXOffset,
				top: t.top + window.pageYOffset,
				bottom: t.bottom + window.pageYOffset
			}
		}

		function u() {
			return {
				rightBorder: window.pageXOffset + window.innerWidth,
				bottomBorder: window.pageYOffset + window.innerHeight
			}
		}

		function l(t, e) {
			var i, n, o, r = u(),
				s = r.rightBorder,
				a = r.bottomBorder - t.bottom - e.height,
				l = t.top - e.height;
			if ("bottom" === e.mode ? 0 <= a ? i = t.bottom : 0 <= l && (i = l) : 0 <= l ? i = l : 0 <= a && (i = t.bottom), a < 0 && l < 0) {
				if (e.auto) return d(t, c(c({}, e), {
					mode: "right",
					auto: !1
				}));
				i = l < a ? t.bottom : l
			}
			return {
				left: e.centering ? (n = t, o = e.width, r = s, a = (o - (n.right - n.left)) / 2, l = n.left - a, a = n.right + a, 0 <= l && a <= r ? l : l < 0 ? 0 : r - o) : (s = s - t.left - e.width, e = t.right - e.width, 0 <= s || !(0 <= e) && s < e ? t.left : e),
				top: i
			}
		}

		function d(t, e) {
			var i, n, o = u(),
				r = o.rightBorder,
				s = o.bottomBorder,
				a = r - t.right - e.width,
				o = t.left - e.width;
			if ("right" === e.mode ? 0 <= a ? n = t.right : 0 <= o && (n = o) : 0 <= o ? n = o : 0 <= a && (n = t.right), o < 0 && a < 0) {
				if (e.auto) return l(t, c(c({}, e), {
					mode: "bottom",
					auto: !1
				}));
				n = a < o ? o : t.right
			}
			return {
				left: n,
				top: e.centering ? (a = t, i = e.height, o = r, n = (i - (a.bottom - a.top)) / 2, r = a.top - n, n = a.bottom + n, 0 <= r && n <= o ? r : r < 0 ? 0 : o - i) : (i = t.bottom - e.height, !(0 <= (e = s - t.top - e.height)) && (0 < i || e < i) ? i : t.top)
			}
		}

		function a(t, e) {
			var i = ("bottom" === e.mode || "top" === e.mode ? l : d)(t, e),
				t = i.left,
				i = i.top;
			return {
				left: Math.round(t) + "px",
				top: Math.round(i) + "px",
				minWidth: Math.round(e.width) + "px",
				position: "absolute"
			}
		}
		e.getScrollbarHeight = function () {
			if (-1 < r) return r;
			var t = document.createElement("div");
			return document.body.appendChild(t), t.style.cssText = "position: absolute;left: -99999px;overflow:scroll;width: 100px;height: 100px;", r = t.offsetHeight - t.clientHeight, document.body.removeChild(t), r
		}, e.isIE = function () {
			var t = window.navigator.userAgent;
			return t.includes("MSIE ") || t.includes("Trident/")
		}, e.isSafari = function () {
			function t(t) {
				return t.test(window.navigator.userAgent)
			}
			var e = t(/Chrome/),
				i = t(/Firefox/);
			return !e && !i && t(/Safari/)
		}, e.isFirefox = function () {
			function t(t) {
				return t.test(window.navigator.userAgent)
			}
			var e = t(/Chrome/),
				i = t(/Safari/);
			return !e && !i && t(/Firefox/)
		}, e.getRealPosition = s, e.calculatePosition = a, e.fitPosition = function (t, e) {
			return a(s(t), e)
		}, e.getPageCss = function () {
			for (var t = [], e = 0; e < document.styleSheets.length; e++)
				for (var i = document.styleSheets[e], n = ("cssRules" in i ? i.cssRules : i.rules), o = 0; o < n.length; o++) {
					var r = n[o];
					"cssText" in r ? t.push(r.cssText) : t.push(r.selectorText + " {\n" + r.style.cssText + "\n}\n")
				}
			return t.join("\n")
		}, e.getLabelStyle = function (t) {
			var e = t.helpMessage,
				i = t.type,
				n = t.labelWidth,
				o = t.label,
				r = n && n.toString().startsWith("0"),
				t = "text" !== i && t.required;
			return !!(e || t || !(!o || o && r) || n && !r) && {
				style: (o || n) && !r && {
					width: n,
					"max-width": "100%"
				},
				label: o && r ? null : o
			}
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = (o.prototype.on = function (t, e, i) {
			t = t.toLowerCase();
			this.events[t] = this.events[t] || [], this.events[t].push({
				callback: e,
				context: i || this.context
			})
		}, o.prototype.detach = function (t, e) {
			var t = t.toLowerCase(),
				i = this.events[t];
			if (e && i && i.length)
				for (var n = i.length - 1; 0 <= n; n--) i[n].context === e && i.splice(n, 1);
			else this.events[t] = []
		}, o.prototype.fire = function (t, e) {
			void 0 === e && (e = []);
			t = t.toLowerCase();
			return !this.events[t] || !this.events[t].map(function (t) {
				return t.callback.apply(t.context, e)
			}).includes(!1)
		}, o.prototype.clear = function () {
			this.events = {}
		}, o);

		function o(t) {
			this.events = {}, this.context = t || this
		}
		e.EventSystem = n, e.EventsMixin = function (t) {
			var e = new n(t = t || {});
			t.detachEvent = e.detach.bind(e), t.attachEvent = e.on.bind(e), t.callEvent = e.fire.bind(e)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			o = i(2),
			i = (r.prototype.mount = function (t, e) {
				e && (this._view = e), t && this._view && this._view.mount && (this._container = o.toNode(t), this._container.tagName ? this._view.mount(this._container) : this._container.attach && this._container.attach(this))
			}, r.prototype.unmount = function () {
				var t = this.getRootView();
				t && t.node && (t.unmount(), this._view = null)
			}, r.prototype.getRootView = function () {
				return this._view
			}, r.prototype.getRootNode = function () {
				return this._view && this._view.node && this._view.node.el
			}, r.prototype.paint = function () {
				this._view && (this._view.node || this._container) && (this._doNotRepaint = !1, this._view.redraw())
			}, r);

		function r(t, e) {
			this.config = e || {}, this._uid = this.config.rootId || n.uid()
		}
		e.View = i, e.toViewLike = function (e) {
			return {
				getRootView: function () {
					return e
				},
				paint: function () {
					return e.node && e.redraw()
				},
				mount: function (t) {
					return e.mount(t)
				}
			}
		}
	}, function (t, e, i) {
		"use strict";
		var n;
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (n = e.FormEvents || (e.FormEvents = {})).change = "change", n.click = "click", n.focus = "focus", n.blur = "blur", n.keydown = "keydown", n.beforeShow = "beforeShow", n.afterShow = "afterShow", n.beforeHide = "beforeHide", n.afterHide = "afterHide", n.afterValidate = "afterValidate", n.beforeValidate = "beforeValidate", n.beforeChangeProperties = "beforeChangeProperties", n.afterChangeProperties = "afterChangeProperties", n.beforeSend = "beforesend", n.afterSend = "aftersend", n.buttonClick = "buttonClick", n.validationFail = "validationfail", (n = e.FileStatus || (e.FileStatus = {})).queue = "queue", n.uploaded = "uploaded", n.failed = "failed", n.inprogress = "inprogress", (n = e.UploaderEvents || (e.UploaderEvents = {})).uploadBegin = "uploadbegin", n.beforeUploadFile = "beforeuploadfile", n.uploadFile = "uploadfile", n.uploadFail = "uploadfail", n.uploadComplete = "uploadcomplete", n.uploadProgress = "uploadprogress", (n = e.ItemEvent || (e.ItemEvent = {})).click = "click", n.change = "change", n.input = "input", n.focus = "focus", n.blur = "blur", n.keydown = "keydown", n.changeOptions = "changeOptions", n.beforeShow = "beforeShow", n.afterShow = "afterShow", n.beforeHide = "beforeHide", n.afterHide = "afterHide", n.beforeValidate = "beforeValidate", n.afterValidate = "afterValidate", n.beforeUploadFile = "beforeUploadFile", n.uploadFile = "uploadfile", n.uploadBegin = "uploadBegin", n.uploadComplete = "uploadComplete", n.uploadFail = "uploadFail", n.uploadProgress = "uploadProgress", n.beforeChangeProperties = "beforeChangeProperties", n.afterChangeProperties = "afterChangeProperties", (n = e.ClearMethod || (e.ClearMethod = {})).value = "value", n.validation = "validation", (n = e.Validation || (e.Validation = {})).empty = "", n.validEmail = "email", n.validInteger = "integer", n.validNumeric = "numeric", n.validAlphaNumeric = "alphanumeric", n.validIPv4 = "IPv4", (e = e.ValidationStatus || (e.ValidationStatus = {}))[e.pre = 0] = "pre", e[e.error = 1] = "error", e[e.success = 2] = "success"
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(20)), n(e(63)), n(e(148)), n(e(149)), n(e(26)), n(e(180)), n(e(21)), n(e(66)), n(e(65)), n(e(181)), n(e(64)), n(e(41))
	}, function (t, n, e) {
		"use strict";
		var o = this && this.__assign || function () {
			return (o = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(n, "__esModule", {
			value: !0
		});
		var r, s, a = e(0),
			i = ["#394E79", "#5E83BA", "#C2D2E9", "#647B37", "#98A468", "#F0D0A9", "#EEB98E", "#9A8BA5", "#E3C5D5"];

		function l(e) {
			var i = {};
			return function (t) {
				return i[t] || (i[t] = e(t))
			}
		}
		n.getDefaultColor = function (t) {
			return void 0 === t && (t = 0), i[t]
		}, n.locator = function (e) {
			return e ? "string" == typeof e ? function (t) {
				return t[e]
			} : e : function () {
				return ""
			}
		}, n.log10 = function (t) {
			return Math.log(t) / Math.LN10
		}, n.getTextWidth = (r = function (t, e) {
			void 0 === e && (e = "");
			var i = document.createElement("canvas").getContext("2d");
			return e && (i.font = e), i.measureText(t).width
		}, s = {}, function () {
			for (var t = [], e = 0; e < arguments.length; e++) t[e] = arguments[e];
			for (var i = s, n = 0; n < t.length - 1; n++) i[t[n]] = i[t[n]] || {}, i = i[t[n]];
			var o = t.length - 1;
			return i[o] || (i[o] = r.apply(void 0, t))
		});
		var c = l(function (t) {
			var e = document.createElement("canvas").getContext("2d");
			return e.fillStyle = t, e.fillRect(0, 0, 2, 2), [(e = e.getImageData(1, 1, 1, 1).data)[0], e[1], e[2]]
		});

		function u(t, e) {
			return t.x < e.x + e.width && t.x + t.width > e.x && t.y < e.y + e.height && t.y + t.height > e.y
		}
		n.getColorShade = function (t, e) {
			return "rgb(" + (t = c(t).map(function (t) {
				return Math.floor(t * e + 255 * (1 - e))
			}))[0] + "," + t[1] + "," + t[2] + ")"
		}, n.getFontStyle = l(function (t) {
			var e = document.createElementNS("http://www.w3.org/2000/svg", "svg");
			e.setAttribute("class", "dhx_chart");
			var i = document.createElementNS("http://www.w3.org/2000/svg", "text");
			i.setAttribute("class", t), e.setAttribute("visibility", "hidden"), i.textContent = "test", e.appendChild(i), document.body.appendChild(e);
			i = getComputedStyle(i), i = i.fontSize + " " + i.fontFamily;
			return document.body.removeChild(e), i
		}), n.linearGradient = function (t, e) {
			return t = t.stops.map(function (t) {
				return a.sv("stop", {
					offset: 100 * t.offset + "%",
					"stop-color": t.color,
					"stop-opacity": t.opacity || 1
				})
			}), a.sv("linearGradient", {
				id: e,
				gradientTransform: "rotate(90)"
			}, t)
		}, n.getRadialGradient = function (t, e, i) {
			return e = e.map(function (t) {
				return a.sv("stop", {
					offset: t.offset,
					"stop-color": t.color,
					"stop-opacity": t.opacity || 1
				})
			}), a.sv("radialGradient", o({
				id: i,
				cx: 0,
				cy: 0,
				gradientUnits: "userSpaceOnUse"
			}, t), e)
		}, n.euclideanDistance = function (t, e, i, n) {
			return Math.sqrt((t - i) * (t - i) + (e - n) * (e - n))
		}, n.verticalCenteredText = function (t) {
			return a.sv("tspan", {
				dy: "0.5ex",
				style: {
					pointerEvents: "none"
				}
			}, t)
		}, n.verticalTopText = function (t) {
			return a.sv("tspan", {
				dy: "-0.5ex"
			}, t)
		}, n.verticalBottomText = function (t) {
			return a.sv("tspan", {
				dy: "1.5ex"
			}, t)
		}, n.calcPointRef = function (t, e) {
			return t + "_" + e
		}, n.getClassesForRotateScale = function (t, e) {
			var i = "",
				n = [];
			switch ("left" === t || "top" === t ? n.push("start-text", "end-text") : "right" !== t && "bottom" !== t || n.push("end-text", "start-text"), t) {
				case "left":
				case "right":
					0 === e ? i = n[1] : 0 < e ? 180 === e ? i = n[0] : 180 < e ? e < 270 ? i = n[0] : 270 < e && (i = n[1]) : e < 180 && (90 < e ? i = n[0] : e < 90 && (i = n[1])) : e < 0 && (-180 === e ? i = n[0] : e < -180 ? -270 < e ? i = n[0] : e < -270 && (i = n[1]) : -180 < e && (e < -90 ? i = n[0] : -90 < e && (i = n[1])));
					break;
				case "top":
				case "bottom":
					0 < e ? 180 < e ? i = n[0] : e < 180 && (i = n[1]) : e < 0 && (-180 < e ? i = n[0] : e < -180 && (i = n[1]))
			}
			return i
		}, n.getScales = function (t) {
			var e, i = [];
			for (e in t) {
				var n = t[e];
				(n.min || n.max || n.maxTicks || n.text || n.value) && i.push(e)
			}
			return i
		}, n.getSizesSVGText = function (t, e) {
			var i = [];
			return e = o({
				font: "normal 14px Roboto",
				lineHeight: 18
			}, e), i.push(n.getTextWidth(t, e.font)), i.push(e.lineHeight), i
		}, n.superposition = u, n.checkPositions = function (t, e, i, n, o) {
			u(t, e) && (e = o.right ? e.y - t.y + t.height : e.y - e.height - t.y, e = o.text1.y + e, Math.abs(e) + o.dy > n && (e = 0 < e ? n + o.dy : -n + o.dy, o.changeSector = !o.changeSector), n = Math.sqrt(Math.pow(i, 2) - Math.pow((e - o.dy) * i / n, 2)), n = o.right ? n : -n, o.changeSector && (n *= -1, o.line *= -1, t.class = o.right ? "pie-value end-text" : "pie-value start-text"), o.text1.x = n, o.text1.y = e, o.text2.x = n, o.text2.y = e + 16)
		}
	}, function (t, e, i) {
		"use strict";
		var n = this && this.__rest || function (t, e) {
			var i = {};
			for (o in t) Object.prototype.hasOwnProperty.call(t, o) && e.indexOf(o) < 0 && (i[o] = t[o]);
			if (null != t && "function" == typeof Object.getOwnPropertySymbols)
				for (var n = 0, o = Object.getOwnPropertySymbols(t); n < o.length; n++) e.indexOf(o[n]) < 0 && Object.prototype.propertyIsEnumerable.call(t, o[n]) && (i[o[n]] = t[o[n]]);
			return i
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var c = i(5);
		e.getFormItemCss = function (t, e, i) {
			void 0 === i && (i = !1);
			var n = t.labelPosition,
				o = t.required,
				r = t.disabled,
				s = t.hiddenLabel,
				a = void 0 === (l = t.css) ? "" : l,
				l = t.$validationStatus,
				l = ((t = {})[c.ValidationStatus.pre] = "", t[c.ValidationStatus.error] = " dhx_form-group--state_error", t[c.ValidationStatus.success] = " dhx_form-group--state_success", t[l] || ""),
				n = "left" === n ? " dhx_form-group--inline" : "",
				r = r ? " dhx_form-group--disabled" : "",
				s = s ? " dhx_form-group--label_sr" : "";
			return e ? n + (i ? "" : l) + (o ? " dhx_form-group--required" : "") + r + s + " " + a : n + r + s + " " + a
		};
		var o = ((i = {})[c.Validation.validAlphaNumeric] = /^[a-zA-Z0-9_]+$/, i[c.Validation.validEmail] = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/, i[c.Validation.validInteger] = /^-?\d+$/, i[c.Validation.validIPv4] = /\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b/, i[c.Validation.validNumeric] = /^-?\d+(\.\d+)?$/, i);
		e.getValidationMessage = function (t) {
			var e;
			return (e = {
				undefined: t.preMessage
			})[c.ValidationStatus.pre] = t.preMessage, e[c.ValidationStatus.error] = t.errorMessage, e[c.ValidationStatus.success] = t.successMessage, e[t.$validationStatus] || ""
		}, e.validateTemplate = function (t, e) {
			return !o[t] || o[t].test(e)
		}, e.isBlock = function (t) {
			return Boolean(t.rows) || Boolean(t.cols)
		}, e.validateInput = function (t, e) {
			var i = !0;
			return "function" == typeof e ? i = e(t) : (e = o[e]) && (i = e.test(t.toString())), i
		}, e.baseInputValidate = function (t, e) {
			var i = e.inputType,
				n = e.min,
				o = e.max,
				r = e.minlength,
				e = e.maxlength;
			return "number" === i ? void 0 !== n && void 0 !== o ? Number(n) <= Number(o) && Number(n) <= Number(t) && Number(o) >= Number(t) : void 0 !== n ? Number(n) <= Number(t) : void 0 !== o ? Number(o) >= Number(t) : 0 === t || "0" === t || !!t : void 0 !== r && void 0 !== e ? Number(r) <= String(t).length && Number(e) >= String(t).length : void 0 !== r ? Number(r) <= String(t).length : void 0 !== e ? Number(e) >= String(t).length : "string" == typeof t && !!t
		}, e.isTimeFormat = function (t, e) {
			return (12 === e ? /(^0?([1-9][0-2]?):[0-5][0-9]?([AP][M]?)$)/i : /(^(0[0-9]|1[0-9]|2[0-3]|[0-9]):[0-5][0-9]$)/i).test(t)
		}, e.isVerify = function (e) {
			return ["required", "validation", "minlength", "maxlength", "min", "max"].some(function (t) {
				switch (t) {
					case "required":
						return !!e[t];
					case "validation":
						return "function" == typeof e[t] || "email" === e[t] || "integer" === e[t] || "numeric" === e[t] || "alphanumeric" === e[t] || "IPv4" === e[t];
					case "minlength":
					case "maxlength":
						return "number" == typeof e[t] || "string" == typeof e[t];
					case "min":
					case "max":
						return ("number" == typeof e[t] || "string" == typeof e[t]) && "number" === e.inputType
				}
			})
		}, e.baseProps = ["width", "height", "padding", "css"], e.widgetConfig = function (t) {
			t.width, t.type, t.id, t.name, t.hidden, t.editable, t.valueFormat, t.css, t.required, t.helpMessage, t.preMessage, t.successMessage, t.errorMessage, t.label, t.labelWidth, t.labelPosition, t.hiddenLabel, t.validation, t.icon;
			return n(t, ["width", "type", "id", "name", "hidden", "editable", "valueFormat", "css", "required", "helpMessage", "preMessage", "successMessage", "errorMessage", "label", "labelWidth", "labelPosition", "hiddenLabel", "validation", "icon"])
		}
	}, function (t, e, i) {
		"use strict";
		var n;
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (n = e.GridEvents || (e.GridEvents = {})).scroll = "scroll", n.expand = "expand", n.filterChange = "filterChange", n.beforeResizeStart = "beforeResizeStart", n.resize = "resize", n.afterResizeEnd = "afterResizeEnd", n.cellClick = "cellClick", n.cellRightClick = "cellRightClick", n.cellMouseOver = "cellMouseOver", n.cellMouseDown = "cellMouseDown", n.cellDblClick = "cellDblClick", n.headerCellClick = "headerCellClick", n.footerCellClick = "footerCellClick", n.headerCellMouseOver = "headerCellMouseOver", n.footerCellMouseOver = "footerCellMouseOver", n.headerCellMouseDown = "headerCellMouseDown", n.footerCellMouseDown = "footerCellMouseDown", n.headerCellDblClick = "headerCellDblClick", n.footerCellDblClick = "footerCellDblClick", n.headerCellRightClick = "headerCellRightClick", n.footerCellRightClick = "footerCellRightClick", n.beforeEditStart = "beforeEditStart", n.afterEditStart = "afterEditStart", n.beforeEditEnd = "beforeEditEnd", n.afterEditEnd = "afterEditEnd", n.beforeKeyDown = "beforeKeyDown", n.afterKeyDown = "afterKeyDown", n.beforeColumnHide = "beforeColumnHide", n.afterColumnHide = "afterColumnHide", n.beforeColumnShow = "beforeColumnShow", n.afterColumnShow = "afterColumnShow", n.beforeRowHide = "beforeRowHide", n.afterRowHide = "afterRowHide", n.beforeRowShow = "beforeRowShow", n.afterRowShow = "afterRowShow", n.beforeRowDrag = "beforeRowDrag", n.dragRowStart = "dragRowStart", n.dragRowOut = "dragRowOut", n.dragRowIn = "dragRowIn", n.canRowDrop = "canRowDrop", n.cancelRowDrop = "cancelRowDrop", n.beforeRowDrop = "beforeRowDrop", n.afterRowDrop = "afterRowDrop", n.afterRowDrag = "afterRowDrag", n.beforeColumnDrag = "beforeColumnDrag", n.dragColumnStart = "dragColumnStart", n.dragColumnOut = "dragColumnOut", n.dragColumnIn = "dragColumnIn", n.canColumnDrop = "canColumnDrop", n.cancelColumnDrop = "cancelColumnDrop", n.beforeColumnDrop = "beforeColumnDrop", n.afterColumnDrop = "afterColumnDrop", n.afterColumnDrag = "afterColumnDrag", n.beforeRowResize = "beforeRowResize", n.afterRowResize = "afterRowResize", n.beforeSort = "beforeSort", n.afterSort = "afterSort", (n = e.GridSystemEvents || (e.GridSystemEvents = {})).cellTouchMove = "cellTouchMove", n.cellTouchEnd = "cellTouchEnd", n.headerCellTouchMove = "headerCellTouchMove", n.headerCellTouchEnd = "headerCellTouchEnd", (e = e.GridSelectionEvents || (e.GridSelectionEvents = {})).beforeUnSelect = "beforeUnSelect", e.afterUnSelect = "afterUnSelect", e.beforeSelect = "beforeSelect", e.afterSelect = "afterSelect"
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(131)), n(e(132)), n(e(136)), n(e(59)), n(e(38))
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(69)), n(e(159)), n(e(44))
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(162)), n(e(71))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(18),
			s = i(2);

		function a(t) {
			for (var e = t.toLowerCase().match(/\w+/g), i = 0, n = "", o = 0; o < e.length; o++) {
				var r = e[o];
				"ctrl" === r ? i += 4 : "shift" === r ? i += 2 : "alt" === r ? i += 1 : n = r
			}
			return i + n
		}
		var l = {
			Up: "arrowUp",
			Down: "arrowDown",
			Right: "arrowRight",
			Left: "arrowLeft",
			Esc: "escape",
			Spacebar: "space"
		},
			i = (n.prototype.destructor = function () {
				document.removeEventListener("keydown", this._initHandler), this.removeHotKey()
			}, n.prototype.addHotKey = function (t, e) {
				t = a(t);
				this._keysStorage[t] || (this._keysStorage[t] = []), this._keysStorage[t].push({
					handler: e
				})
			}, n.prototype.removeHotKey = function (t, i) {
				var n, o, r = this;
				t ? t && i ? (n = a(t), o = function (t) {
					return t.toString().replace(/\n/g, "").replace(/\s/g, "")
				}, this._keysStorage[n].forEach(function (t, e) {
					o(t.handler) === o(i) && (delete r._keysStorage[n][e], r._keysStorage[n] = r._keysStorage[n].filter(function (t) {
						return t
					}))
				})) : (t = a(t), delete this._keysStorage[t]) : this._keysStorage = {}
			}, n.prototype.exist = function (t) {
				t = a(t);
				return !!this._keysStorage[t]
			}, n);

		function n(t) {
			var o = this;
			this._keysStorage = {}, this._initHandler = function (t) {
				var e;
				e = 48 <= t.which && t.which <= 57 || 65 <= t.which && t.which <= 90 ? String.fromCharCode(t.which) : (e = 32 === t.which ? t.code : t.key, s.isIE() && l[e] || e);
				var i = o._keysStorage[(t.ctrlKey || t.metaKey ? 4 : 0) + (t.shiftKey ? 2 : 0) + (t.altKey ? 1 : 0) + (e && e.toLowerCase())];
				if (i)
					for (var n = 0; n < i.length; n++) {
						if (o._beforeCall && !1 === o._beforeCall(t, r.focusManager.getFocusId())) return;
						i[n].handler(t)
					}
			}, t && (this._beforeCall = t), document.addEventListener("keydown", this._initHandler)
		}
		e.KeyManager = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(2),
			c = i(4),
			u = i(12),
			o = (s = c.View, o(d, s), d.prototype._destructor = function () {
				this._helper && this._helper.destructor(), this.config = this._handlers = this._helper = null, this.unmount()
			}, d.prototype._getHandlers = function () {
				return {}
			}, d.prototype._init = function () { }, d.prototype._draw = function () {
				return this._drawLabel()
			}, d.prototype._drawLabel = function () {
				var t = this.config,
					e = t.id,
					i = t.helpMessage,
					n = t.required;
				this.config.helpMessage && (this._helper || (this._helper = new u.Popup({
					css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
				})), this._helper.attachHTML(this.config.helpMessage));
				t = l.getLabelStyle(this.config);
				return t && a.el((this.config.type.includes("group") ? "legend" : "label") + ".dhx_label", {
					for: e || this._uid,
					class: i ? "dhx_label--with-help" : "",
					style: t.style
				}, i ? [(t.label || n) && a.el("span.dhx_label__holder", t.label), a.el("span.dhx_label-help.dxi.dxi-help-circle-outline", {
					tabindex: "0",
					role: "button",
					onclick: this._handlers.showHelper,
					onfocus: this._handlers.showHelper,
					onblur: this._handlers.hideHelper,
					id: "dhx_label__help_" + (e || this._uid)
				})] : t.label)
			}, d);

		function d(t, e) {
			void 0 === e && (e = {});
			var i = s.call(this, t, e) || this;
			i._handlers = r({
				showHelper: function (t) {
					t.preventDefault(), t.stopPropagation(), i._helper.show(t.target, {
						mode: "left" === i.config.labelPosition ? "bottom" : "right"
					})
				},
				hideHelper: function (t) {
					t.preventDefault(), t.stopPropagation(), i._helper.hide()
				}
			}, i._getHandlers());
			return i.mount(t, a.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Label = o
	}, function (t, e, i) {
		(function (o, r) {
			! function () {
				var e = 1,
					i = {},
					n = !1;

				function u(t) {
					o.setImmediate ? r(t) : o.importScripts ? setTimeout(t) : (i[++e] = t, o.postMessage(e, "*"))
				}

				function d(t) {
					"use strict";
					if ("function" != typeof t && null != t) throw TypeError();
					if ("object" != typeof this || this && this.then) throw TypeError();
					var e, i, n = this,
						r = 0,
						s = 0,
						o = [];
					(n.promise = n).resolve = function (t) {
						return e = n.fn, i = n.er, r || (s = t, r = 1, u(c)), n
					}, n.reject = function (t) {
						return e = n.fn, i = n.er, r || (s = t, r = 2, u(c)), n
					}, n._d = 1, n.then = function (t, e) {
						if (1 != this._d) throw TypeError();
						var i = new d;
						return i.fn = t, i.er = e, 3 == r ? i.resolve(s) : 4 == r ? i.reject(s) : o.push(i), i
					}, n.catch = function (t) {
						return n.then(null, t)
					};
					var a = function (t) {
						r = t || 4, o.map(function (t) {
							3 == r && t.resolve(s) || t.reject(s)
						})
					};
					try {
						"function" == typeof t && t(n.resolve, n.reject)
					} catch (t) {
						n.reject(t)
					}
					return n;

					function l(t, e, i, n) {
						if (2 == r) return n();
						if ("object" != typeof s && "function" != typeof s || "function" != typeof t) n();
						else try {
							var o = 0;
							t.call(s, function (t) {
								o++ || (s = t, e())
							}, function (t) {
								o++ || (s = t, i())
							})
						} catch (t) {
							s = t, i()
						}
					}

					function c() {
						var t;
						try {
							t = s && s.then
						} catch (t) {
							return s = t, r = 2, c()
						}
						l(t, function () {
							r = 1, c()
						}, function () {
							r = 2, c()
						}, function () {
							try {
								1 == r && "function" == typeof e ? s = e(s) : 2 == r && "function" == typeof i && (s = i(s), r = 1)
							} catch (t) {
								return s = t, a()
							}
							s == n ? (s = TypeError(), a()) : l(t, function () {
								a(3)
							}, a, function () {
								a(1 == r && 3)
							})
						})
					}
				} (o = this).setImmediate || o.addEventListener("message", function (t) {
					if (t.source == o)
						if (n) u(i[t.data]);
						else {
							n = !0;
							try {
								i[t.data]()
							} catch (t) { }
							delete i[t.data], n = !1
						}
				}), d.resolve = function (e) {
					if (1 != this._d) throw TypeError();
					return e instanceof d ? e : new d(function (t) {
						t(e)
					})
				}, d.reject = function (i) {
					if (1 != this._d) throw TypeError();
					return new d(function (t, e) {
						e(i)
					})
				}, d.all = function (n) {
					if (1 != this._d) throw TypeError();
					if (!(n instanceof Array)) return d.reject(TypeError());
					var o = new d;
					return function i(t, e) {
						return e ? o.resolve(e) : t ? o.reject(t) : (0 == n.reduce(function (t, e) {
							return e && e.then ? t + 1 : t
						}, 0) && o.resolve(n), void n.map(function (t, e) {
							t && t.then && t.then(function (t) {
								return n[e] = t, i(), t
							}, i)
						}))
					}(), o
				}, d.race = function (n) {
					if (1 != this._d) throw TypeError();
					if (!(n instanceof Array)) return d.reject(TypeError());
					if (0 == n.length) return new d;
					var o = new d;
					return function i(t, e) {
						return e ? o.resolve(e) : t ? o.reject(t) : (0 == n.reduce(function (t, e) {
							return e && e.then ? t + 1 : t
						}, 0) && o.resolve(n), void n.map(function (t, e) {
							t && t.then && t.then(function (t) {
								i(null, t)
							}, i)
						}))
					}(), o
				}, d._d = 1, t.exports = d
			}()
		}).call(this, i(39), i(133).setImmediate)
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(1);
		e.transpose = function (t, e) {
			for (var i = [], n = 0; n < t.length; n++)
				for (var o = t[n], r = 0; r < o.length; r++) {
					i[r] = i[r] || [];
					var s = e ? e(o[r]) : o[r];
					i[r].push(s)
				}
			return i
		}, e.getStyleByClass = function (t, e, i, n) {
			return e = e.querySelector("." + i), i = e, i = "string" == typeof (t = '<div class="' + t + '"></div>') ? (i.insertAdjacentHTML("beforeend", t), i.lastChild) : (i.appendChild(t), t), t = {
				color: "rgb(0, 0, 0)" === (t = window.getComputedStyle(i)).color ? n.color : o.rgbToHex(t.color),
				background: "rgba(0, 0, 0, 0)" === t.backgroundColor ? n.background : o.rgbToHex(t.backgroundColor),
				fontSize: parseFloat(t.fontSize)
			}, e.removeChild(i), t.color === n.color && t.background === n.background && t.fontSize === n.fontSize ? null : t
		}, e.removeHTMLTags = function (t) {
			return "string" != typeof t && "number" != typeof t && "boolean" != typeof t ? "" : ("" + (null == t ? "" : t)).replace(/<[^>]*>/g, "").replace(/["]/g, "&quot;").trim()
		}, e.isCssSupport = function (e, i) {
			try {
				return CSS.supports(e, i)
			} catch (t) {
				var n = document.createElement("div");
				return n.style[e] = i, n.style[e] === i
			}
		}, e.isRowEmpty = function (i) {
			if (i) return Object.keys(i).reduce(function (t, e) {
				return "id" !== e && !e.startsWith("$") && t && void 0 !== i[e] && "" !== i[e] ? void 0 : t
			}, !0)
		}, e.isSortable = function (t, e) {
			return !1 !== e.sortable && t.sortable || e.sortable
		}, e.isAutoWidth = function (e, t) {
			if (t) return !1 !== t.autoWidth && e.autoWidth || t.autoWidth;
			var i = !1;
			return e.columns.map(function (t) {
				(!1 !== t.autoWidth && e.autoWidth || t.autoWidth) && (i = !0)
			}), i
		}, e.isTooltip = function (t, e) {
			return !1 !== e.tooltip && t.tooltip || e.tooltip
		}, e.isHtmlEnable = function (t, e) {
			return !1 !== e.htmlEnable && t.htmlEnable || e.htmlEnable
		}, e.getTotalWidth = function (t) {
			return t.reduce(function (t, e) {
				return t + (e.$width || 0)
			}, 0)
		}, e.getTotalHeight = function (t) {
			return t.reduce(function (t, e) {
				return t + (e.$height || 0)
			}, 0)
		}
	}, function (t, i, e) {
		"use strict";
		Object.defineProperty(i, "__esModule", {
			value: !0
		});
		var s = e(1),
			o = e(0),
			g = e(2);
		i.scrollViewConfig = {
			enable: !1,
			autoHide: !0,
			timeout: 1e3,
			scrollHandler: function () { }
		};
		n.prototype.enable = function () {
			this.config.enable = !0, this._getRootView().redraw()
		}, n.prototype.disable = function () {
			this.config.enable = !1, this._getRootView().redraw()
		}, n.prototype.render = function (t, e) {
			var i = this;
			if (void 0 === e && (e = ""), 0 === this._scrollWidth || !this.config.enable || !t.length) return t;
			e && (this._uid = e);
			var n = this.config.enable ? [o.el(".y-scroll", ((n = {})[this._wheelName] = this._handlers[this._wheelName], n._ref = e ? "scroll-y-area-" + e : "scroll-y-area", n.onmousedown = this._handlers.onmousedownArea, n.onmouseenter = this._handlers.onmouseenter, n.onmouseleave = this._handlers.onmouseleave, n.style = {
				width: "6px",
				height: "100%",
				right: 0,
				top: 0,
				position: "absolute"
			}, n), [o.el(".scroll-runner", {
				_ref: e ? "scroll-y-runner-" + e : "scroll-y-runner",
				onmousedown: this._handlers.onmousedownRunner,
				style: {
					height: this._runnerHeight + "px",
					top: this._runnerYTop
				}
			})]), o.el(".x-scroll", ((n = {})[this._wheelName] = this._handlers[this._wheelName], n._ref = e ? "scroll-x-area-" + e : "scroll-x-area", n.onmousedown = this._handlers.onmousedownArea, n.onmouseenter = this._handlers.onmouseenter, n.onmouseleave = this._handlers.onmouseleave, n.style = {
				width: "100%",
				height: "6px",
				left: 0,
				bottom: 0,
				position: "absolute"
			}, n), [o.el(".scroll-runner", {
				_ref: e ? "scroll-x-runner-" + e : "scroll-x-runner",
				onmousedown: this._handlers.onmousedownRunner,
				style: {
					width: this._runnerWidth + "px",
					left: this._runnerXLeft
				}
			})])] : null;
			return o.el(".scroll-view-wrapper", [o.el(".scroll-view", {
				onscroll: this._handlers.onscroll,
				_ref: e ? "scroll-view-" + e : "scroll-view",
				_hooks: {
					didInsert: function () {
						i.update()
					},
					didRecycle: function () {
						i.update()
					}
				},
				style: {
					width: "calc(100% + " + this._scrollWidth + "px)",
					height: "calc(100% + " + this._scrollHeight + "px)"
				}
			}, t)].concat(n))
		}, n.prototype.update = function () {
			var t, e, i, n, o, r = this._getRefs();
			r && (o = r.area, t = r.areaX, e = r.areaY, i = r.runnerY, n = r.runnerX, this._visibleYArea = o.clientHeight / o.scrollHeight, this._visibleXArea = o.clientWidth / o.scrollWidth, this._scrollYTop = o.scrollTop, this._scrollXLeft = o.scrollLeft, this._runnerYTop = this._scrollYTop * this._visibleYArea, this._runnerXLeft = this._scrollXLeft * this._visibleXArea, this._runnerHeight = this._visibleYArea < 1 ? o.clientHeight * this._visibleYArea : 0, this._runnerWidth = this._visibleXArea < 1 ? o.clientWidth * this._visibleXArea : 0, r = i.style.top, o = n.style.left, i.style.opacity = 1, i.style.top = this._runnerYTop + "px", i.style.height = this._runnerHeight + "px", n.style.opacity = 1, n.style.left = this._runnerXLeft + "px", n.style.width = this._runnerWidth + "px", r !== i.style.top && (e.style.opacity = .9, e.style.width = "10px"), o !== n.style.left && (t.style.opacity = .9, t.style.height = "10px"), this.config.autoHide ? this._autoHideFunc || (this._autoHideFunc = s.debounce(function () {
				i.style.opacity = 0, e.style.width = "6px", n.style.opacity = 0, t.style.height = "6px"
			}, this.config.timeout)) : this._autoHideFunc = s.debounce(function () {
				e.style.width = "6px", t.style.height = "6px"
			}, this.config.timeout), this._autoHideFunc())
		}, n.prototype._getRefs = function () {
			var t = this._getRootView(),
				e = !(!t.refs["scroll-view"] || !t.refs["scroll-x-runner"] && !t.refs["scroll-y-runner"]),
				i = !(!this._uid || !t.refs["scroll-view-" + this._uid] || !t.refs["scroll-x-runner-" + this._uid] && !t.refs["scroll-y-runner-" + this._uid]);
			if (t.refs) return e ? {
				area: t.refs["scroll-view"].el,
				areaY: t.refs["scroll-y-area"].el,
				areaX: t.refs["scroll-x-area"].el,
				runnerY: t.refs["scroll-y-runner"].el,
				runnerX: t.refs["scroll-x-runner"].el
			} : i ? {
				area: t.refs["scroll-view-" + this._uid].el,
				areaY: t.refs["scroll-y-area-" + this._uid].el,
				areaX: t.refs["scroll-x-area-" + this._uid].el,
				runnerY: t.refs["scroll-y-runner-" + this._uid].el,
				runnerX: t.refs["scroll-x-runner-" + this._uid].el
			} : void 0
		}, e = n;

		function n(t, e) {
			var v = this;
			void 0 === e && (e = {}), this.config = s.extend({
				enable: i.scrollViewConfig.enable,
				autoHide: i.scrollViewConfig.autoHide,
				timeout: i.scrollViewConfig.timeout,
				scrollHandler: i.scrollViewConfig.scrollHandler
			}, e), this._wheelName = g.isIE() ? "onmousewheel" : "onwheel", this._getRootView = t, this._scrollYTop = this._scrollXLeft = this._runnerYTop = this._runnerXLeft = this._runnerHeight = this._runnerWidth = 0, this._visibleYArea = this._visibleXArea = 1, this._scrollWidth = g.getScrollbarWidth(), this._scrollHeight = g.getScrollbarHeight(), this._handlers = ((t = {
				onscroll: function (t) {
					v.config.scrollHandler(t), v.update()
				}
			})[this._wheelName] = function (t) {
				var e = !!g.locateNodeByClassName(t.target, "y-scroll");
				t.preventDefault();
				var i, n = 40 * (0 < (t.deltaY || -t.wheelDelta) ? 1 : -1),
					t = v._getRefs().area;
				e ? (e = t.scrollHeight - v._runnerHeight, i = v._scrollYTop + n, t.scrollTop = i < 0 ? 0 : e < i ? e : i) : (i = t.scrollWidth - v._runnerWidth, n = v._scrollXLeft + n, t.scrollLeft = n < 0 ? 0 : i < n ? i : n), v.update()
			}, t.onmousedownRunner = function (t) {
				t.preventDefault();

				function e(t) {
					var e;
					i ? (e = t.pageY - u, o.scrollTop = e <= a ? 0 : l < e ? c : (e - a) / v._visibleYArea) : (t = t.pageX - p, o.scrollLeft = t <= d ? 0 : h < t ? f : (t - d) / v._visibleXArea), v.update()
				}
				var i = !!g.locateNodeByClassName(t.target, "y-scroll"),
					n = v._getRefs(),
					o = n.area,
					r = n.runnerY,
					s = n.runnerX,
					n = o.getBoundingClientRect(),
					a = n.top + window.pageYOffset,
					l = n.bottom + window.pageYOffset,
					c = o.scrollHeight - v._runnerHeight,
					u = t.pageY - r.getBoundingClientRect().top - window.pageYOffset,
					d = n.left + window.pageXOffset,
					h = n.right + window.pageXOffset,
					f = o.scrollWidth - v._runnerWidth,
					p = t.pageX - s.getBoundingClientRect().left - window.pageXOffset,
					_ = function () {
						document.removeEventListener("mousemove", e), document.removeEventListener("mouseup", _), document.body.classList.remove("dhx-no-select")
					};
				document.body.classList.add("dhx-no-select"), document.addEventListener("mousemove", e), document.addEventListener("mouseup", _)
			}, t.onmousedownArea = function (t) {
				var e, i, n, o;
				g.locateNodeByClassName(t, "scroll-runner") || (t.preventDefault(), e = !!g.locateNodeByClassName(t.target, "y-scroll"), i = (o = v._getRefs()).area, n = o.runnerY, o = o.runnerX, e ? i.scrollTop += (t.pageY - n.getBoundingClientRect().top) / v._visibleYArea : i.scrollLeft += (t.pageX - o.getBoundingClientRect().left) / v._visibleXArea, v.update())
			}, t.onmouseenter = function (t) {
				var e, i;
				g.locateNodeByClassName(t, "scroll-runner") || (i = v._getRefs()) && (e = !!g.locateNodeByClassName(t.target, "y-scroll"), t = i.areaX, i = i.areaY, e && 0 < v._runnerHeight ? i.style.background = "#eee" : !e && 0 < v._runnerWidth && (t.style.background = "#eee"))
			}, t.onmouseleave = function (t) {
				var e, i;
				g.locateNodeByClassName(t, "scroll-runner") || (i = v._getRefs()) && (e = !!g.locateNodeByClassName(t.target, "y-scroll"), t = i.areaX, i = i.areaY, e && 0 < v._runnerHeight ? i.style.background = "transparent" : !e && 0 < v._runnerWidth && (t.style.background = "transparent"))
			}, t)
		}
		i.ScrollView = e
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(2),
			i = (o.prototype.getFocusId = function () {
				return this._activeWidgetId
			}, o.prototype.setFocusId = function (t) {
				this._activeWidgetId = t
			}, o);

		function o() {
			var e = this;
			this._initHandler = function (t) {
				return e._activeWidgetId = n.locate(t, "dhx_widget_id")
			}, this._removeFocusClass = function (t) {
				var e = document.body.classList;
				e.contains("utilityfocus") && e.remove("utilityfocus")
			}, this._addFocusClass = function (t) {
				var e = document.body.classList;
				"Tab" === t.code ? e.contains("utilityfocus") || e.add("utilityfocus") : e.contains("utilityfocus") && e.remove("utilityfocus")
			}, document.addEventListener("focusin", this._initHandler), document.addEventListener("click", this._initHandler), document.addEventListener("mousedown", this._removeFocusClass), document.addEventListener("keydown", this._addFocusClass)
		}
		e.focusManager = new i
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(183)), n(e(184)), n(e(36))
	}, function (t, e, i) {
		"use strict";
		var n;
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (n = e.TreeFilterType || (e.TreeFilterType = {})).all = "all", n.level = "level", n.leafs = "leafs", (n = e.DataEvents || (e.DataEvents = {})).afterAdd = "afteradd", n.beforeAdd = "beforeadd", n.removeAll = "removeall", n.beforeRemove = "beforeremove", n.afterRemove = "afterremove", n.change = "change", n.load = "load", n.loadError = "loaderror", n.beforeLazyLoad = "beforelazyload", n.afterLazyLoad = "afterlazyload", (n = e.DragEvents || (e.DragEvents = {})).beforeDrag = "beforeDrag", n.dragStart = "dragStart", n.dragOut = "dragOut", n.dragIn = "dragIn", n.canDrop = "canDrop", n.cancelDrop = "cancelDrop", n.beforeDrop = "beforeDrop", n.afterDrop = "afterDrop", n.afterDrag = "afterDrag", (e = e.DataDriver || (e.DataDriver = {})).json = "json", e.csv = "csv", e.xml = "xml"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(26),
			o = i(64);
		e.isEqualObj = function (t, e) {
			for (var i in t)
				if (t[i] !== e[i] || Array.isArray(t[i])) return !1;
			return !0
		}, e.naturalCompare = function (t, e) {
			if (isNaN(t) || isNaN(e)) {
				var n = [],
					o = [];
				for (t.replace(/(\d+)|(\D+)/g, function (t, e, i) {
					n.push([e || 1 / 0, i || ""])
				}), e.replace(/(\d+)|(\D+)/g, function (t, e, i) {
					o.push([e || 1 / 0, i || ""])
				}); n.length && o.length;) {
					var i = n.shift(),
						r = o.shift(),
						r = i[0] - r[0] || i[1].localeCompare(r[1]);
					if (r) return r
				}
				return n.length - o.length
			}
			return t - e
		}, e.findByConf = function (t, e) {
			if ("function" == typeof e) {
				if (e.call(this, t)) return t
			} else if (e.by && e.match && t[e.by] === e.match) return t
		}, e.isDebug = function () {
			var t = window.dhx;
			if (void 0 !== t) return void 0 !== t.debug && t.debug
		}, e.dhxWarning = function (t) {
			console.warn(t)
		}, e.dhxError = function (t) {
			throw new Error(t)
		}, e.toProxy = function (t) {
			var e = typeof t;
			return "string" == e ? new n.DataProxy(t) : "object" == e ? t : void 0
		}, e.toDataDriver = function (t) {
			if ("string" == typeof t) {
				var e = window.dhx,
					e = e && e.dataDrivers || o.dataDrivers;
				if (e[t]) return new e[t];
				console.warn("Incorrect data driver type:", t), console.warn("Available types:", JSON.stringify(Object.keys(e)))
			} else if ("object" == typeof t) return t
		}, e.copyWithoutInner = function (t, e) {
			var i, n = {};
			for (i in t) i.startsWith("$") || e && e[i] || (n[i] = t[i]);
			return n
		}, e.isTreeCollection = function (t) {
			return Boolean(t.getRoot)
		}, e.hasJsonOrArrayStructure = function (t) {
			if ("object" == typeof t) return !0;
			if ("string" != typeof t) return !1;
			try {
				var e = JSON.parse(t);
				return "[object Object]" === Object.prototype.toString.call(e) || Array.isArray(e)
			} catch (t) {
				return !1
			}
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var g = i(1),
			b = i(16),
			w = i(23);

		function l(t, e) {
			t[e] && ("string" == typeof t[e] ? t[e] = [{
				text: "" + t[e]
			}] : t[e] = t[e].map(function (t) {
				return "string" == typeof t && (t = {
					text: t
				}), t
			}))
		}
		e.normalizeColumns = function (t, e) {
			var i = t.columns,
				n = t.htmlEnable;
			void 0 === e && (e = !1);
			for (var o = 0, r = i; o < r.length; o++) {
				var s = r[o];
				s.$htmlEnable = !(!s.htmlEnable && !n), s.$cellCss = s.$cellCss || {}, l(s, "header"), l(s, "footer"), s.header.reduce(function (t, e) {
					return t || !!e.content
				}, !1) && (s.$uniqueData = []), s.minWidth && s.minWidth < 20 && (s.minWidth = 20), s.maxWidth && s.maxWidth < 20 && (s.maxWidth = 20);
				var a = s.minWidth || 100;
				s.width && (a = (a = s.maxWidth && s.minWidth ? s.width >= s.minWidth && s.width <= s.maxWidth ? s.width : s.width >= s.maxWidth ? s.maxWidth : s.minWidth : s.maxWidth ? s.width <= s.maxWidth ? s.width : 100 < s.maxWidth ? s.maxWidth : 100 : !s.minWidth || s.width >= s.minWidth ? s.width : s.minWidth) < 20 ? 20 : a), s.$width = s.$width && !e ? s.$width : a, s.$width > s.maxWidth && (s.$width = s.maxWidth), s.$width < s.minWidth && (s.$width = s.minWidth)
			}
		}, e.countColumns = function (t, e) {
			var n = 0,
				o = 0,
				r = 0,
				s = !1,
				i = 0,
				a = !1;
			return e.forEach(function (t) {
				if (n = Math.max(n, t.header.length), r += t.$width, t.footer && (o = Math.max(o, t.footer.length), a = a || !0), !s)
					for (var e = 0, i = t.header; e < i.length; e++)
						if (i[e].colspan) return void (s = !0)
			}), e.forEach(function (t) {
				if (t.header.length < n)
					for (var e = 0; e < n; e++) t.header[e] = t.header[e] || {
						text: ""
					};
				if (a && (t.footer = t.footer || []), t.footer && t.footer.length < o)
					for (e = 0; e < o; e++) t.footer[e] = t.footer[e] || {
						text: ""
					};
				t.header = t.header.map(function (t) {
					return "object" != typeof t && (t = {
						text: t
					}), t.css = t.css || "", t.text || t.css.includes("dhx_cell-empty") || (t.css += " dhx_cell-empty"), t
				}), "" === t.header[0].text && i++
			}), t.$totalWidth = r, t.$headerLevel = n, t.$footerLevel = o, t.$colspans = s, t.$footer = a, i
		}, e.calculatePositions = function (t, e, i, n, o) {
			for (var r = n.columns || [], s = r.length, a = o || [], l = a.length, c = -1 / 0, u = 0; u < s; u++) r[u].$width > c && (c = r[u].$width);
			for (var d = 1 / 0, u = 0; u < s; u++) r[u].$width < d && (d = r[u].$width);
			for (var h = -1 / 0, u = 0; u < l; u++) a[u].$height > h && (h = a[u].$height);
			for (var f = n.rowHeight, o = Math.round(c / d), f = Math.round(h / f), p = n.$totalWidth / s, t = Math.round(t / p), _ = n.$totalHeight / l, e = Math.round(e / _), v = 0, g = i.left, m = 0; m < r.length; m++) {
				if (!(0 < (g -= r[m].$width) + p / 2)) break;
				v++
			}
			for (var y = 0, b = i.top, m = 0; m < a.length; m++) {
				if (!(0 < (b -= a[m].$height) + _ / 2)) break;
				y++
			}
			return {
				xStart: 0 <= v - o ? v - o : 0,
				xEnd: v + t + o,
				yStart: 0 <= y - f ? y - f : 0,
				yEnd: y + e + f
			}
		}, e.getUnique = function (t, e, i) {
			var n = t.map(function (t) {
				return t[e]
			});
			return i && n.forEach(function (t, e) {
				t.includes(", ") && (t.split(", ").forEach(function (t) {
					return n.push(t)
				}), delete n[e])
			}), n.filter(function (t, e, i) {
				return i.indexOf(t) === e && g.isDefined(t)
			}).sort()
		}, e.getMaxRowHeight = function (t, e, i) {
			void 0 === i && (i = {
				font: "20px Roboto",
				lineHeight: 20
			});
			var n = document.createElement("canvas"),
				o = n.getContext("2d", {
					alpha: !1
				});
			o.font = i.font;
			for (var r = {}, s = e.length, a = 0; a < s; a++) e[a].template ? r[e[a].id] = {
				width: e[a].$width || 0,
				htmlEnable: e[a].$htmlEnable,
				template: e[a].template,
				cols: e[a]
			} : r[e[a].id] = {
				width: e[a].$width || 0,
				htmlEnable: e[a].$htmlEnable
			};
			for (var l = [], c = [], u = 0, d = Object.entries(t); u < d.length; u++) {
				var h, f = d[u],
					p = f[0],
					_ = f[1];
				!r[p] || "id" === p || "height" === p || p.startsWith("$") || "string" != typeof _ && "number" != typeof _ || (h = "", h = null !== (f = r[p]) && void 0 !== f && f.template ? (f = r[p].template(_, t, r[p].cols), r[p].htmlEnable ? b.removeHTMLTags(f) : f) : "string" == typeof _ ? r[p].htmlEnable ? b.removeHTMLTags(_) : _ : _.toString(), l.push(h.split("\n").length), c.push(Math.round(o.measureText(h).width / (null === (p = r[p]) || void 0 === p ? void 0 : p.width))))
			}
			var v = Math.max(g.getMaxArrayNymber(l), g.getMaxArrayNymber(c));
			return n.remove(), v * i.lineHeight
		}, e.getCalculatedRowHeight = function (t, e) {
			void 0 === e && (e = {
				rowHeight: 40,
				padding: 8
			});
			var i = e.rowHeight < 40 ? t : t + 2 * e.padding;
			return t < e.rowHeight ? e.rowHeight : i
		}, e.getTreeCellWidthOffset = function (t) {
			return 20 + 20 * t.$level - (t.$items ? 20 : 0)
		}, e.getMaxColsWidth = function (t, e, i, n) {
			if (void 0 === i && (i = {
				font: "normal 14.4px Arial"
			}), t.length && e.length) {
				var o = document.createElement("canvas"),
					r = o.getContext("2d", {
						alpha: !1
					});
				r.font = i.font;
				for (var s = {}, a = e.length, l = 0; l < a; l++) e[l].template && "data" === n ? s[e[l].id] = {
					width: 20,
					htmlEnable: e[l].$htmlEnable,
					template: e[l].template,
					cols: e[l]
				} : s[e[l].id] = {
					width: 20,
					htmlEnable: e[l].$htmlEnable,
					format: e[l].format
				};
				for (var c = t.length, l = 0; l < c; l++)
					for (var u = 0, d = Object.entries(t[l]); u < d.length; u++) {
						var h, f = d[u],
							p = f[0],
							_ = f[1];
						s[p] && "id" !== p && "height" !== p && !p.startsWith("$") && ("string" == typeof _ || "number" == typeof _ || _ instanceof Date) && (h = void 0, h = "function" != typeof (null === (f = s[p]) || void 0 === f ? void 0 : f.template) || _ instanceof Date ? _ instanceof Date ? w.getFormattedDate(s[p].format || "%M %d %Y", _) : s[p].htmlEnable ? b.removeHTMLTags(_) : _.toString() : (f = s[p].template(_, t[l], s[p].cols), s[p].htmlEnable ? b.removeHTMLTags(f) : f), (h = r.measureText(h).width) > s[p].width && (s[p].width = h))
					}
				o.remove();
				for (var v = {}, g = 0, m = Object.entries(s); g < m.length; g++) {
					var y = m[g],
						p = y[0],
						_ = y[1];
					v[p] = Math.ceil(_.width)
				}
				return v
			}
		}, e.toFormat = function (r, t, s) {
			if (!r && "number" != typeof r) return r;
			switch (t) {
				case "number":
				case "percent":
					return s = s || "#", g.isDefined(r) && !isNaN(Number(r)) ? function (t) {
						r = r.toString();
						var e = s.replace(/#+/g, "#").split("#").filter(function (t) {
							return t
						});
						r = "percent" === t ? (100 * Number(r)).toString() : r;
						var i = Math.trunc(Number(r)).toString(),
							n = s.match(/0/g) && s.match(/0/g).length,
							o = e.find(function (t) {
								return !t.includes("0")
							}),
							o = o ? i.replace(/(\d)(?=(\d{3})+(\D|$))/g, "$1" + o) : i;
						return n && (i = e.find(function (t) {
							return t.includes("0")
						}).replace(/0+/g, ""), e = r.split(".")[1] || "0", e = Number("0." + e).toFixed(n), 1 <= Number(e) && (o++).toString(), o += i + (e = e.toString().split(".")[1].padEnd(n, "0"))), "percent" === t ? o + "%" : o
					}(t) : r;
				case "date":
					return s = s || "%M %d %Y", "string" == typeof r ? r = w.getFormattedDate(s, w.stringToDate(r, s)) : "object" == typeof r && (r = w.getFormattedDate(s, r)), r;
				default:
					return r
			}
		}
	}, function (t, n, e) {
		"use strict";
		Object.defineProperty(n, "__esModule", {
			value: !0
		});
		var i = e(1),
			o = e(1);
		n.locale = {
			monthsShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
			months: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
			daysShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
			days: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Monday"],
			cancel: "Cancel"
		};
		var g, r = {
			"%d": function (t) {
				t = t.getDate();
				return t < 10 ? "0" + t : t
			},
			"%j": function (t) {
				return t.getDate()
			},
			"%l": function (t) {
				return n.locale.days[t.getDay()]
			},
			"%D": function (t) {
				return n.locale.daysShort[t.getDay()]
			},
			"%m": function (t) {
				t = t.getMonth() + 1;
				return t < 10 ? "0" + t : t
			},
			"%n": function (t) {
				return t.getMonth() + 1
			},
			"%M": function (t) {
				return n.locale.monthsShort[t.getMonth()]
			},
			"%F": function (t) {
				return n.locale.months[t.getMonth()]
			},
			"%y": function (t) {
				return t.getFullYear().toString().slice(2)
			},
			"%Y": function (t) {
				return t.getFullYear()
			},
			"%h": function (t) {
				t = t.getHours() % 12;
				return 0 === t && (t = 12), t < 10 ? "0" + t : t
			},
			"%g": function (t) {
				t = t.getHours() % 12;
				return 0 === t && (t = 12), t
			},
			"%H": function (t) {
				t = t.getHours();
				return t < 10 ? "0" + t : t
			},
			"%G": function (t) {
				return t.getHours()
			},
			"%i": function (t) {
				t = t.getMinutes();
				return t < 10 ? "0" + t : t
			},
			"%s": function (t) {
				t = t.getSeconds();
				return t < 10 ? "0" + t : t
			},
			"%a": function (t) {
				return 12 <= t.getHours() ? "pm" : "am"
			},
			"%A": function (t) {
				return 12 <= t.getHours() ? "PM" : "AM"
			},
			"%u": function (t) {
				return t.getMilliseconds()
			}
		},
			m = {
				"%d": function (t, e) {
					/(^([0-9][0-9])$)/i.test(e) ? t.setDate(Number(e)) : t.setDate(Number(1))
				},
				"%j": function (t, e) {
					/(^([0-9]?[0-9])$)/i.test(e) ? t.setDate(Number(e)) : t.setDate(Number(1))
				},
				"%m": function (t, e) {
					var i = /(^([0-9][0-9])$)/i.test(e);
					i ? t.setMonth(Number(e) - 1) : t.setMonth(Number(0)), i && t.getMonth() !== Number(e) - 1 && t.setMonth(Number(e) - 1)
				},
				"%n": function (t, e) {
					var i = /(^([0-9]?[0-9])$)/i.test(e);
					i ? t.setMonth(Number(e) - 1) : t.setMonth(Number(0)), i && t.getMonth() !== Number(e) - 1 && t.setMonth(Number(e) - 1)
				},
				"%M": function (t, e) {
					var i = o.findIndex(n.locale.monthsShort, function (t) {
						return t === e
					}); - 1 === i ? t.setMonth(0) : t.setMonth(i), -1 !== i && t.getMonth() !== i && t.setMonth(i)
				},
				"%F": function (t, e) {
					var i = o.findIndex(n.locale.months, function (t) {
						return t === e
					}); - 1 === i ? t.setMonth(0) : t.setMonth(i), -1 !== i && t.getMonth() !== i && t.setMonth(i)
				},
				"%y": function (t, e) {
					/(^([0-9][0-9])$)/i.test(e) ? t.setFullYear(Number("20" + e)) : t.setFullYear(Number("2000"))
				},
				"%Y": function (t, e) {
					/(^([0-9][0-9][0-9][0-9])$)/i.test(e) ? t.setFullYear(Number(e)) : t.setFullYear(Number("2000"))
				},
				"%h": function (t, e, i) {
					/(^0[1-9]|1[0-2]$)/i.test(e) && "pm" === i || "PM" === i ? t.setHours(Number(e)) : t.setHours(Number(0))
				},
				"%g": function (t, e, i) {
					/(^[1-9]$)|(^0[1-9]|1[0-2]$)/i.test(e) && "pm" === i || "PM" === i ? t.setHours(Number(e)) : t.setHours(Number(0))
				},
				"%H": function (t, e) {
					/(^[0-2][0-9]$)/i.test(e) ? t.setHours(Number(e)) : t.setHours(Number(0))
				},
				"%G": function (t, e) {
					/(^[1-9][0-9]?$)/i.test(e) ? t.setHours(Number(e)) : t.setHours(Number(0))
				},
				"%i": function (t, e) {
					/(^([0-5][0-9])$)/i.test(e) ? t.setMinutes(Number(e)) : t.setMinutes(Number(0))
				},
				"%s": function (t, e) {
					/(^([0-5][0-9])$)/i.test(e) ? t.setSeconds(Number(e)) : t.setSeconds(Number(0))
				},
				"%a": function (t, e) {
					"pm" === e && t.setHours(t.getHours() + 12)
				},
				"%A": function (t, e) {
					"PM" === e && t.setHours(t.getHours() + 12)
				}
			};

		function y(t) {
			for (var e = [], i = "", n = 0; n < t.length; n++) "%" === t[n] ? (0 < i.length && (e.push({
				type: g.separator,
				value: i
			}), i = ""), e.push({
				type: g.datePart,
				value: t[n] + t[n + 1]
			}), n++) : i += t[n];
			return 0 < i.length && e.push({
				type: g.separator,
				value: i
			}), e
		}

		function s(t, e, i) {
			if ("string" == typeof t) {
				for (var n, o = [], r = 0, s = null, a = 0, l = y(e); a < l.length; a++) {
					var c = l[a];
					if (c.type === g.separator) {
						var u = t.indexOf(c.value, r);
						if (-1 === u) {
							if (i) return !1;
							throw new Error("Incorrect date, see docs: https://docs.dhtmlx.com/suite/calendar__api__calendar_dateformat_config.html")
						}
						s && (o.push({
							formatter: s,
							value: t.slice(r, u)
						}), s = null), r = u + c.value.length
					} else c.type === g.datePart && (s = c.value)
				}
				"%A" === s || "%a" === s ? o.unshift({
					formatter: s,
					value: t.slice(r)
				}) : s && o.push({
					formatter: s,
					value: t.slice(r)
				}), o.reverse();
				for (var d = 0, h = o; d < h.length; d++) "%A" !== (v = h[d]).formatter && "%a" !== v.formatter || (n = v.value);
				for (var f = new Date(0), p = 0, _ = o; p < _.length; p++) {
					var v = _[p];
					m[v.formatter] && m[v.formatter](f, v.value, n)
				}
				return !!i || f
			}
		} (e = g = g || {})[e.separator = 0] = "separator", e[e.datePart = 1] = "datePart", n.getFormattedDate = function (t, i) {
			return y(t).reduce(function (t, e) {
				return e.type === g.separator ? t + e.value : r[e.value] ? t + r[e.value](i) : t
			}, "")
		}, n.stringToDate = s;
		a.copy = function (t) {
			return new Date(t)
		}, a.fromYear = function (t) {
			return new Date(t, 0, 1)
		}, a.fromYearAndMonth = function (t, e) {
			return new Date(t, e, 1)
		}, a.weekStart = function (t, e) {
			e = (t.getDay() + 7 - e) % 7;
			return new Date(t.getFullYear(), t.getMonth(), t.getDate() - e)
		}, a.monthStart = function (t) {
			return new Date(t.getFullYear(), t.getMonth(), 1)
		}, a.yearStart = function (t) {
			return new Date(t.getFullYear(), 0, 1)
		}, a.dayStart = function (t) {
			return new Date(t.getFullYear(), t.getMonth(), t.getDate())
		}, a.addDay = function (t, e) {
			return void 0 === e && (e = 1), new Date(t.getFullYear(), t.getMonth(), t.getDate() + e)
		}, a.addMonth = function (t, e) {
			return void 0 === e && (e = 1), new Date(t.getFullYear(), t.getMonth() + e)
		}, a.addYear = function (t, e) {
			return void 0 === e && (e = 1), new Date(t.getFullYear() + e, t.getMonth())
		}, a.withHoursAndMinutes = function (t, e, i, n) {
			return void 0 === n || !n && 12 === e || n && 12 !== e ? new Date(t.getFullYear(), t.getMonth(), t.getDate(), e, i) : n && 12 === e ? new Date(t.getFullYear(), t.getMonth(), t.getDate(), 0, i) : new Date(t.getFullYear(), t.getMonth(), t.getDate(), e + 12, i)
		}, a.setMonth = function (t, e) {
			t.setMonth(e)
		}, a.setYear = function (t, e) {
			t.setFullYear(e)
		}, a.mergeHoursAndMinutes = function (t, e) {
			return new Date(t.getFullYear(), t.getMonth(), t.getDate(), e.getHours(), e.getMinutes())
		}, a.isWeekEnd = function (t) {
			return 0 === t.getDay() || 6 === t.getDay()
		}, a.getTwelweYears = function (t) {
			t = t.getFullYear(), t -= t % 12;
			return i.range(t, 11 + t)
		}, a.getWeekNumber = function (t) {
			6 !== t.getDay() && (t = a.addDay(t, 6 - t.getDay()));
			var e = (t.valueOf() - a.yearStart(t).valueOf()) / 864e5;
			return Math.floor((e - t.getDay() + 10) / 7)
		}, a.isSameDay = function (t, e) {
			return t.getFullYear() === e.getFullYear() && t.getMonth() === e.getMonth() && t.getDate() === e.getDate()
		}, a.toDateObject = function (t, e) {
			return "string" == typeof t ? s(t, e) : new Date(t)
		}, a.nullTimestampDate = new Date(0), e = a;

		function a() { }
		n.DateHelper = e
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(76)), n(e(172)), n(e(80))
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(82)), n(e(195))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(41),
			i = (o.prototype.updateUrl = function (t, e) {
				for (var i in void 0 === e && (e = {}), this._url = this.url = t || this._url, this.url += this.url.includes("?") ? "&" : "?", e) this.config[i] = e[i], this.url += i + "=" + encodeURIComponent(e[i]) + "&";
				this.url = this.url.slice(0, -1)
			}, o.prototype.load = function () {
				return n.ajax.get(this.url, null, {
					responseType: "text"
				})
			}, o.prototype.save = function (t, e) {
				switch (e) {
					case "delete":
						return n.ajax.delete(this.url, t);
					case "update":
						return n.ajax.put(this.url, t);
					case "insert":
					default:
						return n.ajax.post(this.url, t)
				}
			}, o);

		function o(t, e) {
			this.url = this._url = t, this.config = e
		}
		e.DataProxy = i
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(67)), n(e(178)), n(e(9)), n(e(33));
		var o = e(42);
		i.getTreeCell = o.getTreeCell, n(e(22)), n(e(16))
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(77)), n(e(170)), n(e(78)), n(e(46))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.SelectionEvents || (e.SelectionEvents = {})).beforeUnSelect = "beforeunselect", e.afterUnSelect = "afterunselect", e.beforeSelect = "beforeselect", e.afterSelect = "afterselect"
	}, function (t, e, i) {
		"use strict";
		var o = this && this.__assign || function () {
			return (o = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(0),
			d = i(1);
		e.getCount = function (t, e, i) {
			var n = {
				danger: " dhx_navbar-count--color_danger",
				secondary: " dhx_navbar-count--color_secondary",
				primary: " dhx_navbar-count--color_primary",
				success: " dhx_navbar-count--color_success"
			}[t.countColor] || " dhx_navbar-count--color_danger";
			return r.el(".dhx_navbar-count", {
				class: e + n + (!i && 99 < parseInt(t.count, 10) ? " dhx_navbar-count--overlimit" : "")
			}, i && 99 < parseInt(t.count, 10) ? "99+" : t.count)
		}, e.getIcon = function (t, e) {
			return void 0 === t && (t = ""), t.startsWith("dxi") && (t = "dxi " + t), r.el("span", {
				class: "dhx_" + e + "__icon " + t,
				"aria-hidden": "true"
			})
		};
		var s = function (t, e, i) {
			var n = "",
				o = "",
				o = (n = i ? "dhx_menu-item" : "dhx_" + t + "__item") + (e.css ? " " + e.css : "");
			return "spacer" !== e.type && "separator" !== e.type || (o += " " + n + "--" + e.type), "button" !== e.type || "sidebar" !== t || e.icon || (o += " dhx_navbar-item--colapse_hidden"), o
		};
		e.navbarComponentMixin = function (t, e, i, n) {
			var i = s(t, e, i),
				t = "ribbon" === t && ("navItem" === e.type || "imageButton" === e.type);
			return r.el("li", o({
				_key: e.id,
				class: i + (e.icon && !e.value && t ? " dhx_ribbon__item--icon" : "") + (e.src && !e.value && t ? " dhx_ribbon__item--icon" : "") + (e.size && t ? " dhx_ribbon__item--" + e.size : ""),
				".innerHTML": "customHTML" === e.type ? e.html : void 0,
				dhx_id: "customHTML" === e.type ? e.id : void 0
			}, (i = e.type, t = {
				role: "none"
			}, "separator" === i && (t.role = "separator", t["aria-orientation"] = "vertical"), t)), "customHTML" !== e.type ? [n] : void 0)
		}, e.getNavbarButtonCSS = function (t, e) {
			var i = t.color,
				n = t.size,
				o = t.view,
				r = t.full,
				s = t.icon,
				a = t.circle,
				l = t.loading,
				c = t.value,
				u = t.active,
				t = t.count;
			return ({
				danger: " dhx_button--color_danger",
				secondary: " dhx_button--color_secondary",
				primary: " dhx_button--color_primary",
				success: " dhx_button--color_success"
			}[i] || " dhx_button--color_primary") + ({
				small: " dhx_button--size_small",
				medium: " dhx_button--size_medium"
			}[n] || " dhx_button--size_medium") + ({
				flat: " dhx_button--view_flat",
				link: " dhx_button--view_link"
			}[o] || " dhx_button--view_flat") + (r ? " dhx_button--width_full" : "") + (a ? " dhx_button--circle" : "") + (l ? " dhx_button--loading" : "") + (u ? " dhx_button--active" : "") + (s && !c ? " dhx_button--icon" : "") + (d.isDefined(t) ? " dhx_button--count" : "")
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.ChartEvents || (e.ChartEvents = {})).toggleSeries = "toggleSeries", e.chartMouseMove = "chartMouseMove", e.chartMouseLeave = "chartMouseLeave", e.resize = "resize", e.serieClick = "serieClick", e.seriaMouseMove = "seriaMouseMove", e.seriaMouseLeave = "seriaMouseLeave"
	}, function (t, e, i) {
		"use strict";
		var v = this && this.__assign || function () {
			return (v = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var g = i(0),
			m = i(7);

		function y(t, e, i, n) {
			return n && (t += 2 * e * Math.asin(.5 * n / e) / (2 * Math.PI * e)), [Math.cos(2 * Math.PI * t) * e, Math.sin(2 * Math.PI * t) * i]
		}

		function r(t, e, i) {
			t.setAttribute("transform", "translate(" + e + ", " + i + ") scale(1.05)"), t.classList.add("dhx_pie-transform-delay")
		}

		function n(t) {
			t.setAttribute("transform", "translate(0, 0)"), t.classList.remove("dhx_pie-transform-delay")
		}

		function b(t, e) {
			return t - 1e-6 < e && e < t + 1e-6
		}

		function w(t, e) {
			return "M" + -t + ",0A" + t + "," + t + " 0 " + (e ? 0 : 1) + " 1 " + t + ",0A" + t + "," + t + " 0 " + (e ? 0 : 1) + " 1 " + -t + ",0"
		}
		e.getCoordinates = y, e.shiftCoordinates = function (t, e, i) {
			return [t[0] + e, t[1] + i]
		}, e.pieLikeHandlers = {
			onmouseover: function (e, i, t, n) {
				var o = n.parent.attrs.id;
				r(n.el, e, i), n.parent.body.forEach(function (t) {
					t.attrs.id !== o + "-text" && t.attrs.id !== o + "-connector" || r(t.el, e, i)
				})
			},
			onmouseout: function (t, e) {
				var i = e.parent.attrs.id;
				n(e.el), e.parent.body.forEach(function (t) {
					t.attrs.id !== i + "-text" && t.attrs.id !== i + "-connector" || n(t.el)
				})
			}
		}, e.radarScale = function (t, e, i) {
			var n, a = e < i ? e / 2 : i / 2,
				l = 1 / t.scales.length,
				c = .5 < l ? 1 : 0,
				u = [],
				o = (n = a, o = "#FAFBFD", g.sv("circle", {
					cx: 0,
					cy: 0,
					r: n,
					fill: o,
					stroke: "none",
					class: "background-circle"
				}));
			u.push(o);
			for (var d = -.25, r = [], s = t.axis, h = "radar-grid " + (t.zebra ? "zebra" : ""), f = 1; f < s.length; f += 2) {
				var p = a * s[f - 1],
					_ = a * s[f],
					_ = w(p, !0) + " " + w(_, !1),
					_ = g.sv("path", {
						d: _,
						fill: "none",
						stroke: "black",
						class: h
					});
				r.push(_)
			}
			return u.push(r), t.scales.forEach(function (t) {
				var e = y(d, a, a),
					i = e[0],
					n = e[1],
					o = d + l,
					r = y(o, a, a),
					e = r[0],
					r = r[1],
					e = "M " + i + " " + n + " A " + a + " " + a + " 0 " + c + " 1 " + e + " " + r + " L 0 0",
					r = g.sv("path", {
						d: e,
						stroke: "black",
						fill: "none",
						class: "radar-scale"
					});
				u.push(r);
				var s, e = [8, 8],
					r = e[0],
					e = e[1],
					r = b(d, 0) || b(d, .5) ? 0 : d < 0 || .5 < d ? -r : r,
					e = b(d, -.25) || b(d, .25) ? 0 : d < -.25 || .25 < d ? -e : e;
				t = b(d, -.25) || b(d, .25) ? (s = b(d, -.25) ? m.verticalTopText : m.verticalBottomText, g.sv("text", {
					x: i + e,
					y: n + r,
					class: "scale-text"
				}, [s(t)])) : (s = -.25 <= d && d <= .25 ? "start-text scale-text" : "end-text scale-text", g.sv("text", {
					x: i + e,
					y: n + r,
					class: s
				}, [m.verticalCenteredText(t)])), u.push(t), d = o
			}), d = -.25, t.realAxis && (o = t.realAxis.map(function (t, e) {
				var i = y(-.25, a * s[e], a * s[e]),
					e = i[0],
					i = i[1];
				return g.sv("text", {
					x: e,
					y: i,
					dx: -10,
					class: "radar-axis-text"
				}, [m.verticalCenteredText(t.toString())])
			}), u.push(o)), g.sv("g", v({
				transform: "translate(" + e / 2 + ", " + i / 2 + ")"
			}, {
				"aria-label": "x-axis" + ((t = t.attribute) ? ", " + t : "")
			}), u)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.getWidth = function (t, n, o) {
			return t = t.filter(function (t) {
				return !t.hidden
			}), n ? t.reduce(function (t, e, i) {
				e = e.$width;
				return t += o <= i && i < o + n ? e : 0
			}, 0) : t[o].$width
		}, e.getHeight = function (t, n, o) {
			return t = t.filter(function (t) {
				return !t.hidden
			}), n ? t.reduce(function (t, e, i) {
				e = e.$height;
				return t += o <= i && i < o + n ? e : 0
			}, 0) : t[o].$height
		}
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(156)), n(e(75))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		i(6);
		(i = e.FileStatus || (e.FileStatus = {})).queue = "queue", i.uploaded = "uploaded", i.failed = "failed", i.inprogress = "inprogress", (i = e.UploaderEvents || (e.UploaderEvents = {})).uploadBegin = "uploadbegin", i.beforeUploadFile = "beforeuploadfile", i.uploadFile = "uploadfile", i.uploadFail = "uploadfail", i.uploadComplete = "uploadcomplete", i.uploadProgress = "uploadprogress", (e.ProgressBarEvents || (e.ProgressBarEvents = {})).cancel = "cancel", (e = e.VaultMode || (e.VaultMode = {})).grid = "grid", e.list = "list"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		i = i(6);
		e.DataEvents = i.DataEvents, (e = e.NavigationBarEvents || (e.NavigationBarEvents = {})).inputCreated = "inputCreated", e.click = "click", e.openMenu = "openMenu", e.beforeHide = "beforeHide", e.afterHide = "afterHide", e.inputFocus = "inputFocus", e.inputBlur = "inputBlur", e.inputChange = "inputChange"
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, m = i(0),
			y = i(1),
			b = i(8),
			a = i(14),
			l = i(3),
			c = i(5),
			o = (s = a.Label, o(u, s), u.prototype.destructor = function () {
				this.events && this.events.clear(), this.events = this._uid = this._isValid = this._propsItem = this._propsItem = null, s.prototype._destructor.call(this), this.unmount()
			}, u.prototype.setProperties = function (t) {
				if (t && !y.isEmptyObj(t) && this.events.fire(c.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
					this.events.fire(c.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, u.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, u.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(c.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(c.ItemEvent.afterShow, [e]))
			}, u.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(c.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(c.ItemEvent.afterHide, [i, t]))
			}, u.prototype.isVisible = function () {
				return !this.config.hidden
			}, u.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, u.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, u.prototype.isDisabled = function () {
				return this.config.disabled
			}, u.prototype.validate = function (t, e) {
				void 0 === t && (t = !1);
				e = void 0 === e ? this.getValue() : e;
				if (t || this.events.fire(c.ItemEvent.beforeValidate, [e])) return this._isValid = this.config.validation ? b.validateInput(e, this.config.validation) : this.config.required ? !(!b.baseInputValidate(e, this.config) || !String(e).length) : b.baseInputValidate(e, this.config), t || (this.config.$validationStatus = this._isValid ? c.ValidationStatus.success : c.ValidationStatus.error, this.events.fire(c.ItemEvent.afterValidate, [e, this._isValid])), this._isValid
			}, u.prototype.clearValidate = function () {
				this.config.$validationStatus = c.ValidationStatus.pre, this.paint()
			}, u.prototype.clear = function () {
				"" !== this.config.value && (this.config.value = "", this.events.fire(c.ItemEvent.change, [this.getValue()]))
			}, u.prototype.setValue = function (t) {
				void 0 !== t && this.config.value !== t && (this.config.value = t, this.events.fire(c.ItemEvent.change, [this.getValue()]), b.isVerify(this.config) && this.validate())
			}, u.prototype.getValue = function () {
				var t = this.config,
					e = t.inputType,
					t = t.value;
				return "number" === e && "number" == typeof t ? t : "number" === e && "string" == typeof t ? t.length ? Number(t) : "" : "string" == typeof t ? t.length ? t : "" : void 0 === t ? "" : String(t)
			}, u.prototype.focus = function () {
				var t = this;
				m.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.focus()
				})
			}, u.prototype.blur = function () {
				var t = this;
				m.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.blur()
				})
			}, u.prototype._initView = function (t) {
				var e, i = this;
				if (y.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.config = {
					type: "input",
					id: t.id,
					name: t.name,
					disabled: !1,
					hidden: !1,
					inputType: "text",
					required: !1,
					validation: void 0,
					maxlength: void 0,
					minlength: void 0,
					min: void 0,
					max: void 0,
					icon: "",
					placeholder: "",
					autocomplete: !1,
					readOnly: !1,
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this.config.hidden && m.awaitRedraw().then(function () {
					i.hide(!0)
				}), this.paint()
			}, u.prototype._initHandlers = function () {
				var t = this;
				this.events.on(c.ItemEvent.change, function () {
					return t.paint()
				}), this.events.on(c.ItemEvent.afterValidate, function () {
					t.config.$validationStatus = t._isValid ? c.ValidationStatus.success : c.ValidationStatus.error, t.paint()
				})
			}, u.prototype._getHandlers = function () {
				var e = this;
				return {
					oninput: function (t) {
						t = t.target.value.trim();
						e.config.value = t, e.events.fire(c.ItemEvent.input, [t])
					},
					onchange: function (t) {
						t = t.target.value.trim();
						e.config.value = t, e.events.fire(c.ItemEvent.change, [e.getValue()]), b.isVerify(e.config) && e.validate(), e.paint()
					},
					onfocus: function () {
						e.events.fire(c.ItemEvent.focus, [e.getValue()]), e.paint()
					},
					onblur: function () {
						e.events.fire(c.ItemEvent.blur, [e.getValue()]), e.paint()
					},
					onkeydown: function (t) {
						e.events.fire(c.ItemEvent.keydown, [t])
					}
				}
			}, u.prototype._draw = function () {
				var t = this.config,
					e = t.id,
					i = t.value,
					n = t.disabled,
					o = t.name,
					r = t.icon,
					s = t.placeholder,
					a = t.required,
					l = t.inputType,
					c = t.hidden,
					u = t.autocomplete,
					d = t.readOnly,
					h = t.maxlength,
					f = t.minlength,
					p = t.max,
					_ = t.min,
					v = t.label,
					g = t.helpMessage,
					t = c ? " dhx_form-group--hidden" : "",
					c = (null === (c = null === (c = null === (c = this.getRootView()) || void 0 === c ? void 0 : c.refs) || void 0 === c ? void 0 : c.input) || void 0 === c ? void 0 : c.el) === document.activeElement;
				return m.el("div.dhx_form-group", {
					class: b.getFormItemCss(this.config, b.isVerify(this.config), c) + t
				}, [this._drawLabel(), m.el(".dhx_input__wrapper", {}, [m.el("div.dhx_input__container", {}, [this.config.icon ? m.el(".dhx_input__icon", {
					class: this.config.icon
				}) : null, m.el("input.dhx_input", {
					type: ["text", "number", "password"].includes(l) ? l : "text",
					dhx_id: o || e,
					id: e || this._uid,
					placeholder: s || "",
					value: y.isDefined(i) ? i : "",
					name: o || "",
					disabled: n,
					required: a,
					readOnly: d,
					maxlength: h,
					minlength: f,
					max: p,
					min: _,
					onblur: this._handlers.onblur,
					oninput: this._handlers.oninput,
					onchange: this._handlers.onchange,
					onfocus: this._handlers.onfocus,
					onkeydown: this._handlers.onkeydown,
					class: r ? "dhx_input--icon-padding" : "",
					autocomplete: u ? "on" : "off",
					_ref: "input",
					"aria-label": v || g || "type " + (o || l || "text"),
					"aria-describedby": g ? "dhx_label__help_" + (e || this._uid) : null
				})]), b.getValidationMessage(this.config) && m.el("span.dhx_input__caption", b.getValidationMessage(this.config))])])
			}, u);

		function u(t, e) {
			void 0 === e && (e = {});
			var i = s.call(this, null, e) || this;
			return i.events = new l.EventSystem, i._propsItem = ["inputType", "required", "validation", "icon", "placeholder", "autocomplete", "readOnly", "maxlength", "minlength", "min", "max", "step", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], i._props = r(b.baseProps, i._propsItem), i._isValid = !0, i._initView(e), i._initHandlers(), i
		}
		e.Input = o
	}, function (t, e, i) {
		"use strict";
		var n;
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (n = e.RealPosition || (e.RealPosition = {})).left = "left", n.right = "right", n.top = "top", n.bottom = "bottom", n.center = "center", (n = e.Position || (e.Position = {})).right = "right", n.bottom = "bottom", n.center = "center", (e = e.MessageContainerPosition || (e.MessageContainerPosition = {})).topLeft = "top-left", e.topRight = "top-right", e.bottomLeft = "bottom-left", e.bottomRight = "bottom-right"
	}, function (t, e) {
		"use strict";
		var i = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (t) {
			return typeof t
		} : function (t) {
			return t && "function" == typeof Symbol && t.constructor === Symbol && t !== Symbol.prototype ? "symbol" : typeof t
		},
			n = function () {
				return this
			}();
		try {
			n = n || new Function("return this")()
		} catch (t) {
			"object" === ("undefined" == typeof window ? "undefined" : i(window)) && (n = window)
		}
		t.exports = n
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		e.default = {
			apply: "apply",
			reject: "reject"
		}
	}, function (t, e, i) {
		"use strict";
		(function (c) {
			Object.defineProperty(e, "__esModule", {
				value: !0
			});
			var u = i(20),
				d = i(21);

			function h(t) {
				return t ? t.includes("json") ? "json" : t.includes("xml") ? "xml" : "text" : "text"
			}

			function n(o, r, s, t, a) {
				var n, l = t || {};
				return a && (l.Accept = "application/" + a), "GET" !== s && (l["Content-Type"] = l["Content-Type"] || "application/json"), "GET" === s && ((t = r && "object" == typeof r ? (n = r, Object.keys(n).reduce(function (t, e) {
					var i = "object" == typeof n[e] ? JSON.stringify(n[e]) : n[e];
					return t.push(e + "=" + encodeURIComponent(i)), t
				}, []).join("&")) : r && "string" == typeof r ? r : "") && (o += o.includes("?") ? "&" : "?", o += t), r = null), window.fetch ? window.fetch(o, {
					method: s,
					body: r ? JSON.stringify(r) : null,
					headers: l
				}).then(function (e) {
					if (!e.ok) return e.text().then(function (t) {
						return c.reject({
							status: e.status,
							statusText: e.statusText,
							message: t
						})
					});
					var t = a || h(e.headers.get("Content-Type"));
					if ("raw" === t) return {
						headers: Object.fromEntries(e.headers.entries()),
						url: e.url,
						body: e.body
					};
					if (204 !== e.status) switch (t) {
						case "json":
							return e.json();
						case "xml":
							var i = d.toDataDriver(u.DataDriver.xml);
							return i ? e.text().then(function (t) {
								return i.toJsonObject(t)
							}) : e.text();
						default:
							return e.text()
					}
				}) : new c(function (t, e) {
					var i, n = new XMLHttpRequest;
					for (i in n.onload = function () {
						200 <= n.status && n.status < 300 ? ("raw" === a && t({
							url: n.responseURL,
							headers: n.getAllResponseHeaders().trim().split(/[\r\n]+/).reduce(function (t, e) {
								e = e.split(": ");
								return t[e[0]] = e[1], t
							}, {}),
							body: n.response
						}), 204 === n.status ? t() : t(function (t, e) {
							switch (e) {
								case "json":
									return JSON.parse(t);
								case "text":
									return t;
								case "xml":
									var i = d.toDataDriver(u.DataDriver.xml);
									return i ? i.toJsonObject(t) : {
										parseError: "Incorrect data driver type: 'xml'"
									};
								default:
									return t
							}
						}(n.responseText, a || h(n.getResponseHeader("Content-Type"))))) : e({
							status: n.status,
							statusText: n.statusText
						})
					}, n.onerror = function () {
						e({
							status: n.status,
							statusText: n.statusText,
							message: n.responseText
						})
					}, n.open(s, o), l) n.setRequestHeader(i, l[i]);
					switch (s) {
						case "POST":
						case "DELETE":
						case "PUT":
							n.send(void 0 !== r ? JSON.stringify(r) : "");
							break;
						case "GET":
						default:
							n.send()
					}
				})
			}
			e.ajax = {
				get: function (t, e, i) {
					return n(t, e, "GET", i && i.headers, void 0 !== i ? i.responseType : void 0)
				},
				post: function (t, e, i) {
					return n(t, e, "POST", i && i.headers, void 0 !== i ? i.responseType : void 0)
				},
				put: function (t, e, i) {
					return n(t, e, "PUT", i && i.headers, void 0 !== i ? i.responseType : void 0)
				},
				delete: function (t, e, i) {
					return n(t, e, "DELETE", i && i.headers, void 0 !== i ? i.responseType : void 0)
				}
			}
		}).call(this, i(15))
	}, function (t, e, i) {
		"use strict";
		var k = this && this.__assign || function () {
			return (k = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var S = i(1),
			I = i(0),
			P = i(33),
			g = i(16),
			n = i(9),
			m = i(152),
			u = i(2),
			y = i(22);

		function o(t, e, i, n, o) {
			e = u.locateNodeByClassName(o.target, "dhx_grid-fixed-cols-wrap") ? 0 : e;
			var r, s, a, l = u.locateNodeByClassName(o.target, "dhx_grid-cell"),
				c = u.locateNodeByClassName(o.target, "dhx_span-cell");
			(l || c) && n && (r = (a = l ? l.parentNode : c).parentNode, s = l ? Array.prototype.indexOf.call(a.childNodes, l) : i.columns.findIndex(function (t) {
				return t.id === c.getAttribute("dhx_col_id")
			}), s = i.columns.filter(function (t) {
				return !t.hidden
			})[e + s], a = l ? Array.prototype.indexOf.call(r.childNodes, a) : Number(c.getAttribute("dhx_id")) - 1, a = i.data["" + ((l ? t : 0) + a)], (n.toLocaleLowerCase().includes("touch") ? i._events : i.events).fire(n, [a, s, o]))
		}

		function O(t, e, i) {
			return {
				onclick: [o, t, e, i, n.GridEvents.cellClick],
				onmouseover: [o, t, e, i, n.GridEvents.cellMouseOver],
				onmousedown: [o, t, e, i, n.GridEvents.cellMouseDown],
				ondblclick: [o, t, e, i, n.GridEvents.cellDblClick],
				oncontextmenu: [o, t, e, i, n.GridEvents.cellRightClick],
				ontouchstart: [o, t, e, i, n.GridEvents.cellMouseDown],
				ontouchmove: [o, t, e, i, n.GridSystemEvents.cellTouchMove],
				ontouchend: [o, t, e, i, n.GridSystemEvents.cellTouchEnd]
			}
		}

		function b(t, e, i, n) {
			var o = n.$editable && n.$editable.row === e.id && n.$editable.col === i.id,
				r = "",
				s = i.align ? " dhx_align-" + i.align : "dhx_align-left";
			n.dragMode && "row" === n.dragItem && (r += (e.$drophere && !o ? " dhx_grid-cell--drophere" : "") + (e.$dragtarget && !o ? " dhx_grid-cell--dragtarget" : "") + (o ? "" : " dhx_grid-cell--drag"));
			var a = y.getTreeCellWidthOffset(e);
			return I.el(".dhx_grid-cell", k({
				class: "dhx_tree-cell " + (i.$cellCss[e.id] || "") + " " + (e.$items ? "dhx_grid-expand-cell" : "") + " " + (o ? "dhx_tree-editing-cell" : "") + " " + r + s,
				style: {
					width: i.$width,
					height: e.$height,
					padding: e.$items ? 0 : "0 0 0 " + a + "px"
				},
				dhx_col_id: i.id
			}, {
				role: "gridcell",
				"aria-colindex": 1
			}), [e.$items ? I.el(".dhx_grid-expand-cell-icon", k(k({
				class: e.$opened ? "dxi dxi-chevron-up" : "dxi dxi-chevron-down",
				dhx_id: e.id
			}, {
				role: "button",
				"aria-label": e.$opened ? "Collapse group" : "Expand group"
			}), {
				style: {
					padding: e.$level ? "0 0 0 " + (4 + a) + "px" : "0 0 0 4px"
				}
			})) : null, I.el(".dhx_tree-cell", {
				class: s + (n.autoHeight ? " dhx_tree-cell_auto-height" : "")
			}, [t])])
		}
		e.getHandlers = O, e.getTreeCell = b, e.getCells = function (f) {
			if (!f.data || !f.columns) return [];
			var p = f.$positions,
				n = f.data ? f.data.slice(p.yStart, p.yEnd) : [],
				o = f.columns.slice(p.xStart, p.xEnd),
				_ = f.selection.getCell(),
				v = !0;
			return n.map(function (h, t) {
				var e = n.length - 1 === t,
					i = "";
				return f.rowCss && (i = f.rowCss(h)), h.$css && (i += h.$css), I.el(".dhx_grid-row", k({
					style: {
						height: e ? h.$height + 1 : h.$height
					},
					dhx_id: h.id,
					class: i,
					_key: h.id,
					_flags: I.KEYED_LIST
				}, {
					role: "row",
					"aria-rowindex": p.yStart + t + 1
				}), h.$customRender ? [h.$customRender(h, f)] : o.map(function (t, e) {
					var i, n, o, r;
					if (!t.hidden) {
						var s = y.toFormat(h[t.id], t.type, t.format),
							a = function (t, e, i, n) {
								return k({
									role: "gridcell",
									"aria-colindex": e,
									"aria-readonly": n ? "false" : "true"
								}, (e = t, n = h, t = {
									tabindex: -1
								}, _ ? _.row.id === n.id && _.column.id === e.id && (t.tabindex = 0) : v && (t.tabindex = 0, t.onfocus = function (t) {
									var e;
									f.selection && !_ && (e = t.target.parentNode.getAttribute("dhx_id"), (t = t.target.getAttribute("dhx_col_id")) && e && (f.selection.setCell(e, t), _ = f.selection.getCell()))
								}), v = !1, t))
							},
							l = t.template ? t.template(s, h, t) : "boolean" != typeof (r = s) && "boolean" !== t.type || "string" == typeof r ? r || 0 === r ? r : "" : "" + Boolean(r);
						"string" == typeof l && (l = g.isHtmlEnable(f, t) ? I.el("div.dhx_grid-cell__content", k({
							".innerHTML": l
						}, {
							role: "button",
							"aria-label": "Edit content"
						})) : l);
						var c = ((t.$cellCss && t.$cellCss[h.id] || "") + " dhx_" + t.type + "-cell").replace(/\s+/g, " "),
							u = t.$width,
							d = f.$editable && f.$editable.row === h.id && f.$editable.col === t.id;
						return ((d || "boolean" === t.type && (f.editable && (null === (o = t.editable) || void 0 === o || o) || !f.editable && t.editable)) && (f.leftSplit && f.columns.length !== f.leftSplit && f.columns.indexOf(t) < f.leftSplit || (i = h, n = t, o = f, l = m.getEditor(i, n, o).toHTML(), c += " dhx_grid-cell__editable", f.leftSplit === f.columns.indexOf(t) + 1 && --u)), "tree" === f.type && f.firstColId === t.id) ? b(l, h, t, f) : (f.dragMode && "row" === f.dragItem && (c += (h.$drophere && !d ? " dhx_grid-cell--drophere" : "") + (h.$dragtarget && !d ? " dhx_grid-cell--dragtarget" : "") + (d ? "" : " dhx_grid-cell--drag")), t.align && (c += " dhx_align-" + t.align), f.autoHeight && (c += " dhx_grid-cell__content_auto-height"), I.el(".dhx_grid-cell", k({
							class: c,
							style: {
								width: u,
								height: h.$height + "px"
							},
							_key: t.id,
							dhx_col_id: t.id
						}, a(t, p.xStart + e + 1, 0, f.editable)), [l]))
					}
				}))
			})
		}, e.getSpans = function (m, y) {
			var b = [],
				w = m.$positions,
				x = m.columns,
				E = m.data;
			if (!x.length || !m.spans) return null;
			for (var C = m.spans.sort(function (t, e) {
				return "string" == typeof t.row && "string" == typeof e.row ? t.row.localeCompare(e.row) : t.row - e.row
			}), t = 0; t < C.length; t++) ! function (t) {
				var e = C[t].row,
					i = C[t].column,
					n = C[t].rowspan,
					o = C[t].colspan,
					r = C[t].css;
				if (1 === n) return;
				var s = S.findIndex(x, function (t) {
					return "" + t.id == "" + i
				}),
					a = S.findIndex(E, function (t) {
						return "" + t.id == "" + e
					});
				if (s < 0 || a < 0) return;
				if (!0 === y && ((o || 1) + s > m.leftSplit || s + 1 > m.leftSplit)) return;
				var l = x[s],
					c = E[a];
				if (l.hidden) return;
				var u = C[t].text || (void 0 === c[i] ? "" : c[i]);
				u = "string" == typeof (u = (l.template || function (t, e, i) {
					return t || 0 === t ? t : ""
				})(u, c, l)) ? I.el("div.dhx_span-cell-content", {
					".innerHTML": u
				}) : u;
				for (var d = 0, h = 0; h < a; h++) d += E[h].$height;
				for (var f = d - 1, p = 0, _ = s - 1; 0 <= _; _--) p += x[_].$width;
				var v = s === x.length - 1,
					g = s + o === x.length,
					t = l.header[0].text ? " dhx_span-cell" : " dhx_span-cell dhx_span-cell--title";
				t += r ? " " + r : "", t += 0 === a ? " dhx_span-first-row" : "", t += 0 === s ? " dhx_span-first-col" : "", t += v || g ? " dhx_span-last-col" : "", t += o ? " dhx_span-string-cell" : " dhx_span-" + (l.type || "string") + "-cell", t += l.align ? " dhx_align-" + l.align : " dhx_align-left";
				l = 1 < o ? P.getWidth(x, o, s) : l.$width, c = 1 < n ? P.getHeight(E, n, a) : c.$height;
				b.push(I.el("div", k({
					class: t,
					style: {
						width: l,
						height: c,
						top: f,
						left: p
					},
					dhx_col_id: i,
					dhx_id: e,
					"aria-hidden": "true"
				}, O(w.yStart, w.xStart, m)), [u]))
			}(t);
			return b
		}, e.getShifts = function (t) {
			var e = t.columns.slice(0, t.$positions.xStart),
				t = t.data.slice(0, t.$positions.yStart);
			return {
				x: g.getTotalWidth(e),
				y: g.getTotalHeight(t)
			}
		}
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(157)), n(e(74))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.LayoutEvents || (e.LayoutEvents = {})).beforeShow = "beforeShow", e.afterShow = "afterShow", e.beforeHide = "beforeHide", e.afterHide = "afterHide", e.beforeResizeStart = "beforeResizeStart", e.resize = "resize", e.afterResizeEnd = "afterResizeEnd", e.beforeAdd = "beforeAdd", e.afterAdd = "afterAdd", e.beforeRemove = "beforeRemove", e.afterRemove = "afterRemove", e.beforeCollapse = "beforeCollapse", e.afterCollapse = "afterCollapse", e.beforeExpand = "beforeExpand", e.afterExpand = "afterExpand"
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(161)), n(e(72))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.ListEvents || (e.ListEvents = {})).click = "click", e.doubleClick = "doubleclick", e.focusChange = "focuschange", e.beforeEditStart = "beforeEditStart", e.afterEditStart = "afterEditStart", e.beforeEditEnd = "beforeEditEnd", e.afterEditEnd = "afterEditEnd", e.itemRightClick = "itemRightClick", e.itemMouseOver = "itemMouseOver", e.contextmenu = "contextmenu"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.default = {
			notFound: "Not Found",
			selectAll: "Select All",
			unselectAll: "Unselect All",
			selectedItems: "selected items"
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		e.default = {
			dragAndDrop: "Drag & drop",
			or: "or",
			browse: "Browse files",
			filesOrFoldersHere: "files or folders here",
			cancel: "Cancel",
			clearAll: "Clear all",
			clear: "Clear",
			add: "Add",
			upload: "Upload",
			download: "Download",
			error: "error",
			byte: "B",
			kilobyte: "KB",
			megabyte: "MB",
			gigabyte: "GB"
		}
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(200)), n(e(84)), n(e(83)), n(e(50));
		e = e(51);
		i.locale = e.default
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			t = t.replace(/^#?([a-f\d])([a-f\d])([a-f\d])$/i, function (t, e, i, n) {
				return e + e + i + i + n + n
			});
			t = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(t);
			return t ? {
				r: parseInt(t[1], 16),
				g: parseInt(t[2], 16),
				b: parseInt(t[3], 16)
			} : null
		}

		function o(t) {
			var e, i, n, o = t.r / 255,
				r = t.g / 255,
				s = t.b / 255,
				a = Math.max(o, r, s),
				l = a - Math.min(o, r, s),
				c = function (t) {
					return (a - t) / 6 / l + .5
				};
			return 0 == l ? e = i = 0 : (i = l / a, n = c(o), t = c(r), c = c(s), o === a ? e = c - t : r === a ? e = 1 / 3 + n - c : s === a && (e = 2 / 3 + t - n), e < 0 ? e += 1 : 1 < e && --e), {
				h: Math.floor(360 * e),
				s: i,
				v: a
			}
		}
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.HSVtoRGB = function (t) {
			var e, i = {
				r: 0,
				g: 0,
				b: 0
			},
				n = t.h / 60,
				o = t.s,
				r = t.v,
				t = Math.floor(n) % 6,
				n = n - Math.floor(n),
				s = 255 * r * (1 - o),
				a = 255 * r * (1 - o * n),
				l = 255 * r * (1 - o * (1 - n));
			switch (r *= 255, t) {
				case 0:
					i.r = r, i.g = l, i.b = s;
					break;
				case 1:
					i.r = a, i.g = r, i.b = s;
					break;
				case 2:
					i.r = s, i.g = r, i.b = l;
					break;
				case 3:
					i.r = s, i.g = a, i.b = r;
					break;
				case 4:
					i.r = l, i.g = s, i.b = r;
					break;
				case 5:
					i.r = r, i.g = s, i.b = a
			}
			for (e in i) i[e] = Math.round(i[e]);
			return i
		}, e.RGBToHex = function (i) {
			return Object.keys(i).reduce(function (t, e) {
				e = i[e].toString(16).toUpperCase();
				return t + (e = 1 === e.length ? "0" + e : e)
			}, "#")
		}, e.HexToRGB = n, e.RGBToHSV = o, e.HexToHSV = function (t) {
			return o(n(t))
		}, e.isHex = function (t) {
			return /(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test(t)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		e.default = {
			cancel: "Cancel",
			select: "Select",
			rightClickToDelete: "Right click to delete",
			customColors: "Custom colors",
			addNewColor: "Add new color"
		}
	}, function (t, e, i) {
		"use strict";
		var n = this && this.__assign || function () {
			return (n = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(211),
			r = i(85),
			s = i(7),
			a = {
				left: r.left,
				right: r.right,
				bottom: r.bottom,
				top: r.top
			},
			h = {
				left: r.leftGrid,
				right: r.rightGrid,
				bottom: r.bottomGrid,
				top: r.topGrid
			},
			r = (l.prototype.addPadding = function () {
				this._padding = !0
			}, l.prototype.getSize = function () {
				return this.config.size
			}, l.prototype.scaleReady = function (t) {
				var e = [];
				this._charts.forEach(function (t) {
					t.getPoints().forEach(function (t) {
						return e.push(t[1])
					})
				}), this._axis = new o.AxisCreator(e, this.config).getScale();
				var i = this._position;
				"radial" !== i && (t[i] += this.config.size)
			}, l.prototype.point = function (t) {
				return this.config.log ? this._logPoint(t) : this._isXDirection ? (t - this._axis.min) / (this._axis.max - this._axis.min) : 1 - (t - this._axis.min) / (this._axis.max - this._axis.min)
			}, l.prototype.add = function (t) {
				this._charts.push(t)
			}, l.prototype.paint = function (e, i) {
				var n = this;
				if (this.config.hidden) return null;
				var t = this._axis.steps.map(function (t) {
					return [n._isXDirection ? n.point(t) * e : n.point(t) * i, t]
				});
				return 0 === t.length && "left" === this._position && (t = [
					[0, 0]
				]), a[this._position](t, this.config, e, i)
			}, l.prototype.scaleGrid = function () {
				var a = this,
					l = this._position,
					c = this.config.grid,
					u = this.config.dashed,
					d = this.config.hidden;
				return {
					paint: function (t, e) {
						var i, n, o = a._axis.steps.indexOf(a.config.targetLine),
							r = (i = t, n = e, a._axis.steps.map(function (t) {
								return [a._isXDirection ? a.point(t) * i : a.point(t) * n, t]
							})),
							s = a.point(a.config.targetValue),
							s = {
								targetLine: o,
								dashed: u,
								grid: c,
								targetValue: s,
								hidden: d
							};
						return h[l](r, t, e, s)
					}
				}
			}, l.prototype._setDefaults = function (t) {
				t.locator && (this.locator = s.locator(t.locator)), this.config = n(n({}, {
					scalePadding: 20,
					textPadding: 11,
					grid: !0,
					targetLine: null,
					showText: !0
				}), t)
			}, l.prototype._logPoint = function (t) {
				var e = Math.abs(t) / t,
					i = this._axis.steps,
					n = i.length - 1,
					o = i.indexOf(t);
				return n = -1 !== o ? o / n : ((this._axis.min < 0 ? i.indexOf(0) : 0) + e * s.log10(Math.abs(t))) / n, this._isXDirection ? n : 1 - n
			}, l);

		function l(t, e, i) {
			this._data = t, this._padding = !1, this._charts = [], this._position = i, this._setDefaults(e), this._isXDirection = "bottom" === i || "top" === i, "radial" !== i && (this.config.size = "left" === i || "right" === i ? this.config.size || 40 + (this.config.title ? 40 : 0) : this.config.size || 20 + (this.config.title ? 40 : 0))
		}
		e.Scale = r
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(7),
			i = i(54),
			o = (r = i.default, o(a, r), a.prototype.addScale = function (t, e) {
				"bottom" === t || "top" === t ? (this.xScale = e, this._xLocator = e.locator) : (this.yScale = e, this._yLocator = s.locator(this.config.value))
			}, a.prototype.paint = function (t, e) {
				r.prototype.paint.call(this, t, e)
			}, a.prototype.dataReady = function (o) {
				var r = this;
				return this.config.active ? (this._points = this._data.map(function (t, e) {
					var i = r._xLocator(t),
						n = r._yLocator(t),
						n = [i, n, t.id, i, n];
					return o && (n[1] += o[e][1]), n
				}), this._points) : this._points = []
			}, a.prototype._calckFinalPoints = function (n, o) {
				var r = this;
				this._points.forEach(function (t, e) {
					t[0] = r.xScale.point(t[0]) * n || 0;
					var i = r.yScale.point(t[1]) * o;
					t[1] = isNaN(i) ? "xbar" === r.config.type ? 0 : o : i
				})
			}, a.prototype._defaultLocator = function (t) {
				return [this._yLocator(t), this._xLocator(t)]
			}, a);

		function a() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(1),
			r = i(31),
			s = i(7),
			n = i(87),
			i = (a.prototype.toggle = function () {
				this.config.active = !this.config.active
			}, a.prototype.getClosest = function (t, e) {
				for (var i = [1 / 0, null, null, null], n = 0, o = this._points; n < o.length; n++) {
					var r = o[n],
						s = this._getClosestDist(t, e, r[0], r[1]);
					i[0] > s && (i[0] = s, i[1] = r[0], i[2] = r[1], i[3] = r[2])
				}
				return i
			}, a.prototype.getClosestVertical = function (t) {
				for (var e = [1 / 0, null, null, null, null], i = 0, n = this._points; i < n.length; i++) {
					var o = n[i],
						r = Math.abs(o[0] - t);
					e[0] > r && (e[0] = r, e[1] = o[0], e[2] = o[1], e[3] = o[2], e[4] = o[4])
				}
				return e
			}, a.prototype.getTooltipType = function (t) {
				return "top"
			}, a.prototype.getTooltipText = function (t) {
				if (this._data.getItem(t) && this.config.tooltip) {
					t = this._defaultLocator(this._data.getItem(t));
					return this.config.tooltipTemplate ? this.config.tooltipTemplate(t) : t[0]
				}
			}, a.prototype.dataReady = function (t) {
				return this._points = []
			}, a.prototype.paint = function (t, e) {
				return this._calckFinalPoints(t, e)
			}, a.prototype.getPoints = function () {
				return this._points
			}, a.prototype.addScale = function (t, e) { }, a.prototype._getClosestDist = function (t, e, i, n) {
				return s.euclideanDistance(t, e, i, n)
			}, a.prototype._calckFinalPoints = function (t, e) { }, a.prototype._setDefaults = function (t) {
				this.config = t
			}, a.prototype._defaultLocator = function (t) {
				return [null, null]
			}, a.prototype._getPointType = function (t, e) {
				return n.getShadeHelper(t, e)
			}, a);

		function a(t, e, i) {
			var n = this;
			this._data = t, this._handlers = {
				onclick: function (t, e) {
					return n._events.fire(r.ChartEvents.serieClick, [t, e])
				},
				onmousemove: function (t, e, i) {
					return n._events.fire(r.ChartEvents.seriaMouseMove, [t, e, i])
				},
				onmouseleave: function (t, e) {
					return n._events.fire(r.ChartEvents.seriaMouseLeave, [t, e])
				}
			}, this.id = e.id = e.id || o.uid(), this._events = i, this._points = [], this._setDefaults(e)
		}
		e.default = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(7),
			i = i(54),
			o = (s = i.default, o(l, s), l.prototype.scaleReady = function (t) {
				for (var e in t) t[e] += this.config.paddings;
				return t
			}, l.prototype.dataReady = function () {
				var s = this,
					t = this._data;
				return this._sum = t.reduce(function (t, e) {
					return e.$hidden ? t : t + parseFloat(s._valueLocator(e))
				}, 0), this._points = t.reduce(function (t, e, i) {
					if (e.$hidden) return t;
					var n = s._textLocator(e),
						o = s._valueLocator(e),
						r = o / s._sum,
						i = s._colorLocator ? s._colorLocator(e) : a.getDefaultColor(i);
					return t.push([r, o, e.id, n, i]), t
				}, []), this._points
			}, l.prototype.toggle = function (t) {
				var e = this._data.getItem(t);
				e && this._data.update(t, {
					$hidden: !e.$hidden
				})
			}, l.prototype.getClosest = function (t, e) {
				for (var i = 1 - (Math.atan2(t - this._center[0], e - this._center[1]) + Math.PI) / Math.PI / 2, n = this._points, o = 0; o < n.length; o++) {
					if (n[o][0] >= i) return [0, this._tooltipData[o][0], this._tooltipData[o][1], n[o][2]];
					i -= n[o][0]
				}
				return [1 / 0, null, null, null]
			}, l.prototype.getTooltipText = function (t) {
				if (this.config.tooltip) {
					t = this._defaultLocator(this._data.getItem(t));
					return this.config.tooltipTemplate ? this.config.tooltipTemplate(t) : t[0]
				}
			}, l.prototype.getTooltipType = function (t) {
				return "simple"
			}, l.prototype._setDefaults = function (e) {
				var i = this,
					t = {
						subType: "basic",
						paddings: e.useLines ? 70 : 50
					};
				this.config = r(r({}, t), e), this._drawPointType = this._getPointType("empty", "none"), this._valueLocator = a.locator(e.value), this._textLocator = a.locator(e.text), e.color ? this._colorLocator = a.locator(e.color) : e.monochrome && (this._colorLocator = function (t) {
					return a.getColorShade(e.monochrome, 2 * i._getPercent(t))
				})
			}, l.prototype._defaultLocator = function (t) {
				return [this._valueLocator(t), this._textLocator(t)]
			}, l.prototype._getPercent = function (t) {
				return parseFloat(this._valueLocator(t)) / this._sum
			}, l);

		function l() {
			var t = null !== s && s.apply(this, arguments) || this;
			return t._center = [0, 0], t._tooltipData = [], t
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			a = this && this.__assign || function () {
				return (a = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var l, c = i(0),
			u = i(7),
			i = i(53),
			o = (l = i.default, o(r, l), r.prototype.paint = function (t, e) {
				var r = this;
				l.prototype.paint.call(this, t, e);
				var i, n = this.config.pointColor || this.config.color,
					o = "chart " + this.config.type + " " + (this.config.css || "") + " " + (this.config.dashed ? "dash-line" : ""),
					s = [];
				return this.config.strokeWidth && s.push(this._getForm(this._points, this.config, o, t, e)), this.config.pointType && (i = this._getPointType(this.config.pointType, n), s = s.concat(this._points.map(function (t) {
					return i(t[0], t[1], u.calcPointRef(t[2], r.id))
				}).map(function (t, e) {
					var i, n, o;
					return t && t.attrs && (t.attrs = a(a({}, t.attrs), (i = t.key, n = r._points, i && (o = n.find(function (t) {
						return i.includes(t[2])
					})), {
						role: "graphics-symbol",
						"aria-roledescription": "point",
						"aria-label": o ? "point x=" + o[3] + " y=" + o[4] : "",
						tabindex: 0
					})), r.config.tooltip && (t.attrs.onmousemove = [r._handlers.onmousemove, r._points[e][2], r.id], t.attrs.onmouseleave = [r._handlers.onmouseleave, r._points[e][2], r.id], t.attrs.onclick = [r._handlers.onmousemove, r._points[e][2], r.id])), t
				}))), c.sv("g", a(a({
					class: "seria",
					_key: this.id
				}, {
					"aria-label": "chart " + (this.config.value || "")
				}), {
					tabindex: 0
				}), s)
			}, r.prototype._getForm = function (t, e, i, n, o) {
				t = t.map(function (t, e) {
					return (e ? "L" : "M") + t[0] + " " + t[1]
				}).join(" ");
				return c.sv("path", {
					id: "seria" + e.id,
					d: t,
					stroke: e.color,
					class: i,
					"stroke-width": this.config.strokeWidth,
					fill: "none"
				})
			}, r.prototype._setDefaults = function (t) {
				this.config = a(a({}, {
					alpha: 1,
					strokeWidth: 2,
					active: !0,
					tooltip: !0
				}), t)
			}, r);

		function r() {
			return null !== l && l.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, d = i(0),
			a = i(3),
			h = i(8),
			l = i(12),
			c = i(5),
			u = i(14),
			f = i(1),
			o = (s = u.Label, o(p, s), p.prototype.setProperties = function (t, e) {
				if (void 0 === e && (e = !1), t && !f.isEmptyObj(t) && (e || this.events.fire(c.ItemEvent.beforeChangeProperties, [this.getProperties()]))) {
					for (var i in t) this._props.includes(i) && (this.config[i] = t[i]);
					e || this.events.fire(c.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, p.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, p.prototype.show = function () {
				this.config.hidden && this.events.fire(c.ItemEvent.beforeShow, [this.getValue()]) && (this.config.hidden = !1, this.events.fire(c.ItemEvent.afterShow, [this.getValue()]))
			}, p.prototype.hide = function (t) {
				this.config.hidden && !t || !this.events.fire(c.ItemEvent.beforeHide, [this.getValue(), t]) || (this.config.hidden = !0, this.events.fire(c.ItemEvent.afterHide, [this.getValue(), t]))
			}, p.prototype.isVisible = function () {
				return !this.config.hidden
			}, p.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, p.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, p.prototype.isDisabled = function () {
				return this.config.disabled
			}, p.prototype.validate = function (t) {
				if (void 0 === t && (t = !1), t || this.events.fire(c.ItemEvent.beforeValidate, [this.getValue()])) return (this.config.required || this.config.$required) && (this._isValid = !!this.config.checked), t || (this.config.$validationStatus = this._isValid ? c.ValidationStatus.success : c.ValidationStatus.error, this.events.fire(c.ItemEvent.afterValidate, [this.getValue(), this._isValid])), this._isValid
			}, p.prototype.clearValidate = function () {
				this.config.$validationStatus = c.ValidationStatus.pre, this.paint()
			}, p.prototype.setValue = function (t, e) {
				void 0 === e && (e = !1), void 0 !== t && t !== this.config.checked && (this.config.checked = !!t, e || (this.events.fire(c.ItemEvent.change, [this.getValue()]), h.isVerify(this.config) && this.validate()))
			}, p.prototype.getValue = function () {
				var t = this.config,
					e = t.value,
					t = t.checked;
				return e ? t ? e : "" : !!t
			}, p.prototype.clear = function (t) {
				this.config.checked && (this.config.checked = !1, t || this.events.fire(c.ItemEvent.change, [this.getValue()]))
			}, p.prototype.destructor = function () {
				this.events && this.events.clear(), this._inGroup = this._propsItem = this._props = this._isValid = this._uid = this.events = null, s.prototype._destructor.call(this), this.unmount()
			}, p.prototype.focus = function () {
				var t = this;
				d.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.focus()
				})
			}, p.prototype.blur = function () {
				var t = this;
				d.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.blur()
				})
			}, p.prototype.isChecked = function () {
				return !!this.config.checked
			}, p.prototype._initView = function (t) {
				var e = this;
				if (this._inGroup = this.config.$group, this._inGroup) {
					for (var i in this.config = {
						type: t.type,
						id: t.id,
						text: "",
						width: "content",
						height: "content",
						padding: 0
					}, t) "id" !== i && "type" !== i && "name" !== i && (this.config[i] = t[i]);
					this._handlers = {
						onchange: function (t) {
							e.config.checked = t.target.checked, e.events.fire(c.ItemEvent.change, [e.getValue()]), h.isVerify(e.config) && e.validate()
						},
						onfocus: function () {
							return e.events.fire(c.ItemEvent.focus, [e.getValue(), e.config.id])
						},
						onblur: function () {
							return e.events.fire(c.ItemEvent.blur, [e.getValue(), e.config.id])
						},
						onkeydown: function (t) {
							e.events.fire(c.ItemEvent.keydown, [t, e.config.id])
						}
					}
				} else {
					for (var i in this.config = {
						type: t.type,
						id: t.id,
						name: t.name,
						disabled: !1,
						required: !1,
						label: "",
						labelWidth: "",
						labelPosition: "top",
						hiddenLabel: !1,
						helpMessage: "",
						preMessage: "",
						successMessage: "",
						errorMessage: "",
						width: "content",
						height: "content",
						padding: 0
					}, t) "id" !== i && "type" !== i && "name" !== i && (this.config[i] = t[i]);
					this.config.helpMessage && (this._helper = new l.Popup({
						css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
					}), this._helper.attachHTML(this.config.helpMessage)), this._handlers = {
						showHelper: function (t) {
							t.preventDefault(), t.stopPropagation(), e._helper.show(t.target, {
								mode: "left" === e.config.labelPosition ? "bottom" : "right"
							})
						},
						hideHelper: function (t) {
							t.preventDefault(), t.stopPropagation(), e._helper.hide()
						},
						onchange: function (t) {
							e.config.checked = t.target.checked, e.events.fire(c.ItemEvent.change, [e.getValue()]), h.isVerify(e.config) && e.validate()
						},
						onfocus: function () {
							return e.events.fire(c.ItemEvent.focus, [e.getValue()])
						},
						onblur: function () {
							return e.events.fire(c.ItemEvent.blur, [e.getValue()])
						},
						onkeydown: function (t) {
							e.events.fire(c.ItemEvent.keydown, [t])
						}
					}
				}
				this.config.hidden && d.awaitRedraw().then(function () {
					e.hide(!0)
				})
			}, p.prototype._initHandlers = function () {
				var t = this;
				this.events.on(c.ItemEvent.change, function () {
					return t.paint()
				}), this.events.on(c.ItemEvent.afterValidate, function () {
					t.config.$validationStatus = t._isValid ? c.ValidationStatus.success : c.ValidationStatus.error, t.paint()
				})
			}, p.prototype._draw = function () {
				var t = this.config,
					e = t.id,
					i = t.value,
					n = t.checked,
					o = t.disabled,
					r = t.name,
					s = t.required,
					a = t.$required,
					l = t.text,
					c = t.label,
					u = t.labelWidth,
					t = t.helpMessage;
				return d.el("label.dhx_checkbox.dhx_form-group.dhx_form-group--checkbox", {
					class: h.getFormItemCss(this.config, h.isVerify(this.config) || !!a)
				}, this._inGroup ? [d.el(".dhx_checkbox__holder", [d.el("input.dhx_checkbox__input", {
					type: "checkbox",
					id: e,
					value: i || "",
					name: r || "",
					disabled: o,
					checked: n,
					onchange: this._handlers.onchange,
					onfocus: this._handlers.onfocus,
					onblur: this._handlers.onblur,
					onkeydown: this._handlers.onkeydown,
					required: s,
					_ref: "input"
				}), d.el("span.dhx_checkbox__visual-input"), d.el("span.dhx_text", [l])])] : [c || u || t || s ? this._drawLabel() : null, d.el(".dhx_checkbox__container", [d.el(".dhx_checkbox__holder", [d.el("input.dhx_checkbox__input", {
					type: "checkbox",
					dhx_id: r || e,
					id: e,
					value: i,
					name: r,
					disabled: o,
					checked: n,
					onchange: this._handlers.onchange,
					onfocus: this._handlers.onfocus,
					onblur: this._handlers.onblur,
					onkeydown: this._handlers.onkeydown,
					required: s,
					_ref: "input",
					"aria-label": c || "checkbox " + (l || ""),
					"aria-describedby": t ? "dhx_label__help_" + (e || this._uid) : null
				}), d.el("span.dhx_checkbox__visual-input"), d.el("span.dhx_text", [l])]), s && h.getValidationMessage(this.config) && d.el("span.dhx_input__caption", h.getValidationMessage(this.config))])])
			}, p);

		function p(t, e) {
			void 0 === e && (e = {});
			var i = s.call(this, t, e) || this;
			i.events = new a.EventSystem, i._isValid = !0, i._propsItem = ["required", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage", "text"], i._props = r(h.baseProps, i._propsItem), i._initView(e), i._initHandlers();
			return i.mount(t, d.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Checkbox = o
	}, function (t, e, i) {
		"use strict";

		function n(t) {
			var e = document.activeElement;
			e.classList.contains("dhx_alert__apply-button") && "Enter" === t.key || e.classList.contains("dhx_alert__confirm-reject") || e.classList.contains("dhx_alert__confirm-aply") || t.preventDefault()
		}
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.blockScreen = function (t) {
			var e = document.createElement("div");
			return e.className = "dhx_alert__overlay " + (t || ""), document.body.appendChild(e), document.addEventListener("keydown", n),
				function () {
					document.body.removeChild(e), document.removeEventListener("keydown", n)
				}
		}
	}, function (t, e, i) {
		"use strict";
		var n = this && this.__assign || function () {
			return (n = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(2),
			u = i(38),
			d = 750,
			h = 200;

		function c(t, e, i, n) {
			var o, r, s;
			switch (e) {
				case u.Position.center:
					return (r = t.left + window.pageXOffset + (t.width - i) / 2) + 8 < window.pageXOffset && (r = t.left + window.pageXOffset), {
						left: r,
						top: s = t.top + window.pageYOffset + (t.height - n) / 2,
						pos: o = u.RealPosition.center
					};
				case u.Position.right:
					return o = u.RealPosition.right, (r = t.right + window.pageXOffset) + i + 8 > window.innerWidth + window.pageXOffset && (r = window.pageXOffset + t.left - i, o = u.RealPosition.left), {
						left: r,
						top: s = window.pageYOffset + t.top + (t.height - n) / 2,
						pos: o
					};
				case u.Position.bottom:
				default:
					return (r = window.pageXOffset + t.left + (t.width - i) / 2) + i > window.innerWidth + window.pageXOffset ? r = window.innerWidth + window.pageXOffset - i : r < 0 && (r = 0), o = u.RealPosition.bottom, (s = window.pageYOffset + t.bottom) + n + 8 > window.innerHeight + window.pageYOffset && (s = window.pageYOffset + t.top - n, o = u.RealPosition.top), {
						left: r,
						top: s,
						pos: o
					}
			}
		}
		e.findPosition = c;
		var f = document.createElement("div"),
			p = document.createElement("span");
		p.className = "dhx_tooltip__text", f.appendChild(p), f.setAttribute("role", "tooltip"), f.style.position = "absolute";
		var _, v = null,
			g = !1,
			m = null,
			y = null;

		function b(t) {
			return t && t.classList.contains("dhx_popup--window") && t.classList.contains("dhx_popup--window_active") ? 2147483647 : null != t && t.classList.contains("dhx_popup--window") || null != t && t.classList.contains("dhx_popup--window_modal") ? 2147483646 : t && t.offsetParent ? b(t.offsetParent) : null
		}

		function w(t, e, i, n, o, r) {
			void 0 === o && (o = !1);
			var s = t.getBoundingClientRect();
			r ? p.innerHTML = e : p.textContent = e, document.body.appendChild(f), f.className = "dhx_widget dhx_tooltip" + (o ? " dhx_tooltip--forced" : "");
			var e = f.getBoundingClientRect(),
				e = c(s, i, e.width, e.height),
				a = e.left,
				l = e.top,
				e = e.pos,
				t = b(t);
			switch (t && (f.style.zIndex = t.toString()), e) {
				case u.RealPosition.bottom:
				case u.RealPosition.top:
				case u.RealPosition.left:
				case u.RealPosition.right:
				case u.RealPosition.center:
					f.style.left = a + "px", f.style.top = l + "px"
			}
			f.className += " dhx_tooltip--" + e + " " + (n || ""), g = !0, o || setTimeout(function () {
				f.className += " dhx_tooltip--animate"
			})
		}

		function r(e, t, i) {
			var n = i.force,
				o = i.showDelay,
				r = i.hideDelay,
				s = i.position,
				a = i.css,
				l = i.htmlEnable;
			n || (y = setTimeout(function () {
				w(e, t, s || u.Position.bottom, a, !1, l)
			}, o || d));
			var c = function () {
				var t;
				g && (t = r, v && (m = setTimeout(function () {
					document.body.removeChild(f), g = !1, m = null
				}, t || h))), clearTimeout(y), e.removeEventListener("mouseleave", c), e.removeEventListener("blur", c), document.removeEventListener("mousedown", c), _ = v = null
			};
			n && w(e, t, s, a, n, l), e.addEventListener("mouseleave", c), e.addEventListener("blur", c), document.addEventListener("mousedown", c), _ = c
		}

		function s(t, e) {
			var i = o.toNode(e.node);
			i !== v && (_ && (_(), _ = null), v = i, m ? (clearTimeout(m), m = null, r(i, t, n(n({}, e), {
				force: !0
			}))) : r(i, t, e))
		}

		function a(t) {
			t = o.locateNode(t, "dhx_tooltip_text");
			t && s(t.getAttribute("dhx_tooltip_text"), {
				position: t.getAttribute("dhx_tooltip_position") || u.Position.bottom,
				node: t
			})
		}
		e.getZIndex = b, e.tooltip = s, e.enableTooltip = function () {
			document.addEventListener("mousemove", a)
		}, e.disableTooltip = function () {
			document.removeEventListener("mousemove", a)
		}
	}, function (t, e, i) { }, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(62)), n(e(182)), n(e(35))
	}, function (t, i, a) {
		"use strict";
		(function (r) {
			Object.defineProperty(i, "__esModule", {
				value: !0
			});
			var n = a(1),
				o = a(2),
				s = a(6),
				c = a(35),
				t = (e.prototype.selectFile = function () {
					this._fileInput.click()
				}, e.prototype.linkDropArea = function (t) {
					function e(t) {
						return t.preventDefault()
					}
					var i = this,
						n = o.toNode(t),
						t = function (t) {
							t.preventDefault(), i.parseFiles(t.dataTransfer)
						};
					n.addEventListener("dragover", e), n.addEventListener("drop", t), this._dropAreas.set(n, {
						dragover: e,
						drop: t
					})
				}, e.prototype.unlinkDropArea = function (t) {
					var i = this;
					t ? (t = o.toNode(t), this._unlinkDropArea(t), this._dropAreas.delete(t)) : (this._dropAreas.forEach(function (t, e) {
						i._unlinkDropArea(e)
					}), this._dropAreas.clear())
				}, e.prototype.parseFiles = function (t) {
					if (t.items && t.items[0] && t.items[0].webkitGetAsEntry) this._parseAsWebkitEntry(t.items);
					else {
						for (var e = t.files, i = 0; i < e.length; i++) this._addFile(e[i]);
						this.config.autosend && this.send()
					}
				}, e.prototype.send = function (t) {
					var e = this;
					if (!this._uploadInfo || !this.isActive) {
						var i = this.data.findAll(function (t) {
							return t.status === c.FileStatus.queue || t.status === c.FileStatus.failed
						}).filter(function (t) {
							return e.events.fire(c.UploaderEvents.beforeUploadFile, [t])
						});
						if (i.length)
							if (this.isActive = !0, this._uploadInfo = {
								files: i,
								count: i.length,
								size: i.reduce(function (t, e) {
									return t + e.file.size
								}, 0)
							}, this.events.fire(c.UploaderEvents.uploadBegin, [i]), this.events.fire(c.UploaderEvents.uploadProgress, [0, 0, this._uploadInfo.size]), this.config.singleRequest) this._xhrSend(i, t);
							else
								for (var n = 0, o = i; n < o.length; n++) {
									var r = o[n];
									this._xhrSend([r], t)
								}
					}
				}, e.prototype.abort = function (t) {
					if (t) {
						t = this.data.getItem(t);
						t && t.request && 4 !== t.request.readyState && t.request.abort()
					} else if (this._uploadInfo && this._uploadInfo.files)
						for (var e = 0, i = this._uploadInfo.files; e < i.length; e++) {
							var n = i[e];
							this.abort(n.id)
						}
				}, e.prototype._unlinkDropArea = function (t) {
					var e, i = this._dropAreas.get(t);
					i && (e = i.dragover, i = i.drop, t.removeEventListener("dragover", e), t.removeEventListener("drop", i))
				}, e.prototype._initEvents = function () {
					var i = this;
					this._fileInput.addEventListener("change", function () {
						for (var t = i._fileInput.files, e = 0; e < t.length; e++) i._addFile(t[e]);
						i.config.autosend && i.send(), i._fileInput.value = null
					})
				}, e.prototype._xhrSend = function (a, t) {
					for (var l = this, t = this._createFormData(a, t), r = new XMLHttpRequest, e = 0, i = a; e < i.length; e++) {
						var n = i[e];
						this.data.update(n.id, {
							request: r,
							status: c.FileStatus.inprogress,
							progress: 0
						})
					}
					r.open("POST", this.config.target), r.upload.onprogress = function (t) {
						for (var e = 0, i = a; e < i.length; e++) {
							var n = i[e];
							l.data.update(n.id, {
								progress: t.loaded / t.total,
								status: c.FileStatus.inprogress
							})
						}
						var o = l._uploadInfo.files.reduce(function (t, e) {
							return t + e.size * e.progress
						}, 0) || 0,
							r = l._uploadInfo.size,
							s = o / l._uploadInfo.size * 100 || 0;
						l.events.fire(c.UploaderEvents.uploadProgress, [s, o, r])
					}, r.onloadend = function () {
						l._uploadInfo.count = l.config.singleRequest ? 0 : l._uploadInfo.count - 1;
						for (var t = 200 === r.status ? c.FileStatus.uploaded : c.FileStatus.failed, e = 200 === r.status && r.response ? JSON.parse(r.response) : null, i = 0, n = a; i < n.length; i++) {
							var o = n[i];
							l.data.update(o.id, {
								status: t
							}), t === c.FileStatus.uploaded ? (l.config.updateFromResponse && e && (l.config.singleRequest && e[o.id] ? l.data.update(o.id, e[o.id]) : l.config.singleRequest || l.data.update(o.id, e)), l.events.fire(c.UploaderEvents.uploadFile, [o, e])) : l.events.fire(c.UploaderEvents.uploadFail, [o])
						}
						0 === l._uploadInfo.count && (l.isActive = !1, l.events.fire(c.UploaderEvents.uploadComplete, [l._uploadInfo.files]))
					}, r.send(t)
				}, e.prototype._parseAsWebkitEntry = function (t) {
					for (var e = this, i = [], n = 0; n < t.length; n++) {
						var o = t[n].webkitGetAsEntry();
						i.push(this._traverseFileTree(o))
					}
					r.all(i).then(function () {
						e.config.autosend && e.send()
					})
				}, e.prototype._createFormData = function (t, e) {
					var i = this.config.fieldName,
						n = new FormData,
						o = this.config.params;
					if (e)
						for (var r in e) n.append(r, e[r]);
					if (o)
						for (var r in o) n.append(r, o[r]);
					for (var s = 1 < t.length ? "[]" : "", a = 0, l = t; a < l.length; a++) {
						var c = l[a];
						n.append(i + s, c.file, c.file.name), n.append(i + "_fullname" + s, c.path + c.file.name);
						c = "object" == typeof c.id ? c.id : c.id.toString();
						n.append(i + "_id" + s, c)
					}
					return n
				}, e.prototype._addFile = function (t, e) {
					void 0 === e && (e = "");
					e = {
						id: n.uid(),
						file: t,
						progress: 0,
						status: c.FileStatus.queue,
						src: null,
						path: e
					};
					this.data.add(e)
				}, e.prototype._traverseFileTree = function (t) {
					var n = this;
					return new r(function (r) {
						var s = 0,
							a = function (t, e) {
								var i, o;
								t.isFile ? (s++, t.file(function (t) {
									s--, n._addFile(t, e), 0 === s && r()
								})) : t.isDirectory && (i = t.createReader(), i = i, o = e + t.name + "/", s++, i.readEntries(function (t) {
									s--;
									for (var e = 0, i = t; e < i.length; e++) {
										var n = i[e];
										a(n, o)
									}
									0 === s && r()
								}))
							};
						a(t, "")
					})
				}, e);

			function e(t, e, i) {
				void 0 === t && (t = {}), this.config = n.extend({
					autosend: !0,
					updateFromResponse: !0,
					fieldName: "file"
				}, t), this.data = e || new s.DataCollection, this.events = i || this.data.events, this.isActive = !1, this._fileInput = document.createElement("input"), this._fileInput.type = "file", this._fileInput.multiple = !0, this._initEvents(), this._dropAreas = new Map
			}
			i.Uploader = t
		}).call(this, a(15))
	}, function (t, e, i) {
		"use strict";
		var a = this && this.__assign || function () {
			return (a = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		},
			n = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(3),
			r = i(144),
			s = i(147),
			l = i(26),
			c = i(21),
			u = i(20),
			p = i(1),
			i = (d.prototype._reset = function () {
				this._order = [], this._pull = {}, this._changes = {
					order: []
				}, this._initOrder = null, this._meta = new WeakMap, this._loaded = !1
			}, d.prototype.add = function (t, i) {
				var n = this;
				if (this.events.fire(u.DataEvents.beforeAdd, [t])) {
					t = Array.isArray(t) ? t.map(function (t, e) {
						return 0 !== e && (i += 1), n._add(t, i)
					}) : this._add(t, i);
					return this._applySmart(), t
				}
			}, d.prototype.remove = function (t) {
				var e = this;
				t && (t instanceof Array ? n(t).map(function (t) {
					e._remove(t)
				}) : this._remove(t))
			}, d.prototype.removeAll = function () {
				this._reset(), this.events.fire(u.DataEvents.removeAll), this.events.fire(u.DataEvents.change)
			}, d.prototype.exists = function (t) {
				return !!this._pull[t]
			}, d.prototype.getNearId = function (t) {
				if (!this._pull[t]) return this._order[0].id || ""
			}, d.prototype.getItem = function (t) {
				return this._pull[t]
			}, d.prototype.update = function (t, e, i) {
				var n = this.getItem(t);
				n ? c.isEqualObj(e, n) || (e.id && t !== e.id ? (c.dhxWarning("this method doesn't allow change id"), c.isDebug()) : (e.parent && n.parent && e.parent !== n.parent && this.move(t, -1, this, e.parent), p.extend(this._pull[t], e, !1), this.config.update && this.config.update(this._pull[t]), i || this._onChange("update", t, this._pull[t])), this._applySmart()) : c.dhxWarning("item not found")
			}, d.prototype.getIndex = function (e) {
				if (!e) return -1;
				var t = p.findIndex(this._order, function (t) {
					return t && t.id.toString() === e.toString()
				});
				return this._pull[e] && 0 <= t ? t : void 0
			}, d.prototype.getId = function (t) {
				if (this._order[t]) return this._order[t].id
			}, d.prototype.getLength = function () {
				return this._order.length
			}, d.prototype.isDataLoaded = function (t, e) {
				return void 0 === t && (t = 0), void 0 === e && (e = this._order.length), p.isNumeric(t) && p.isNumeric(e) ? 0 === this._order.slice(t, e).filter(function (t) {
					return t && t.$empty
				}).length : (this._loaded || (this._loaded = !this.find(function (t) {
					return t.$empty
				})), !!this._loaded)
			}, d.prototype.filter = function (t, e) {
				var i;
				this.isDataLoaded() ? (e && e.add || (this._order = this._initOrder || this._order, this._initOrder = null), !t || "function" == typeof t || void 0 !== (i = t).by && void 0 !== i.match && (t = i.compare ? function (t) {
					return i.compare(t[i.by], i.match, t, i.multi)
				} : function (t) {
					return t[i.by] == i.match
				}), this._filter = e && e.smartFilter ? t : null, this._applyFilters(t), this.events.fire(u.DataEvents.change)) : c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype.find = function (t) {
				for (var e in this._pull) {
					var i = c.findByConf(this._pull[e], t);
					if (i) return i
				}
				return null
			}, d.prototype.findAll = function (t) {
				var e, i = [];
				for (e in this._pull) {
					var n = c.findByConf(this._pull[e], t);
					n && i.push(n)
				}
				return i
			}, d.prototype.sort = function (t, e) {
				this.isDataLoaded() ? (e && e.smartSorting && (this._sorter = t), t && this._applySorters(t), this.events.fire(u.DataEvents.change)) : c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype.copy = function (t, i, n, o) {
				var r = this;
				return t instanceof Array ? t.map(function (t, e) {
					return r._copy(t, i, n, o, e)
				}) : this._copy(t, i, n, o)
			}, d.prototype.move = function (t, i, n, o, e) {
				var r = this;
				return t instanceof Array ? t.map(function (t, e) {
					return r._move(t, i, n, o, e)
				}) : this._move(t, i, n, o, 0, e)
			}, d.prototype.forEach = function (t) {
				for (var e = 0; e < this._order.length; e++) t.call(this, this._order[e], e, this._order)
			}, d.prototype.load = function (t, e) {
				return "string" == typeof t && (this.dataProxy = t = new l.DataProxy(t)), this.dataProxy = t, this._loader.load(t, e)
			}, d.prototype.parse = function (t, e) {
				return this._reset(), this._loader.parse(t, e)
			}, d.prototype.$parse = function (t) {
				var e = this.config.approximate;
				e && (t = this._approximate(t, e.value, e.maxNum)), this._parse_data(t), this._applySmart(), this.events.fire(u.DataEvents.change, ["load"]), this.events.fire(u.DataEvents.load)
			}, d.prototype.save = function (t) {
				"string" == typeof t && (t = new l.DataProxy(t)), this._loader.save(t)
			}, d.prototype.changeId = function (t, e, i) {
				var n;
				void 0 === e && (e = p.uid()), i || this.isDataLoaded() ? (n = this.getItem(t)) ? (n.id = e, p.extend(this._pull[t], n), this._pull[e] = this._pull[t], i || this._onChange("update", e, this._pull[e]), delete this._pull[t]) : c.dhxWarning("item not found") : c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype.isSaved = function () {
				return !this._changes.order.length
			}, d.prototype.map = function (t) {
				for (var e = [], i = 0; i < this._order.length; i++) e.push(t.call(this, this._order[i], i, this._order));
				return e
			}, d.prototype.mapRange = function (t, e, i) {
				t < 0 && (t = 0), e > this._order.length - 1 && (e = this._order.length - 1);
				for (var n = this._order.slice(t, e), o = [], r = t; r <= e; r++) o.push(i.call(this, this._order[r], r, n));
				return o
			}, d.prototype.reduce = function (t, e) {
				for (var i = 0; i < this._order.length; i++) e = t.call(this, e, this._order[i], i);
				return e
			}, d.prototype.serialize = function (t) {
				void 0 === t && (t = u.DataDriver.json);
				var e = this.map(function (t) {
					var e = a({}, t);
					return Object.keys(e).forEach(function (t) {
						t.startsWith("$") && delete e[t]
					}), e
				}),
					t = c.toDataDriver(t);
				if (t) return t.serialize(e)
			}, d.prototype.getInitialData = function () {
				return this._initOrder
			}, d.prototype.setMeta = function (t, e, i) {
				var n;
				t && ((n = this._meta.get(t)) || (n = {}, this._meta.set(t, n)), n[e] = i)
			}, d.prototype.getMeta = function (t, e) {
				t = this._meta.get(t);
				return t ? t[e] : null
			}, d.prototype.getMetaMap = function (t) {
				return this._meta.get(t)
			}, d.prototype.setRange = function (t, e) {
				this._range = e ? [t, e] : null
			}, d.prototype.getRawData = function (t, e, i, n) {
				if (i = i || this._order, 1 === n) return i;
				var o;
				if (this._range && (t = this._range[0] + t, e = -1 === e || t + (o = e - t) > this._range[1] ? this._range[1] : t + o), !e || 0 === t && (-1 === e || e === i.length)) return i;
				if (t >= i.length) return [];
				(-1 === e || e > i.length) && (e = i.length);
				i = i.slice(t, e);
				return 0 !== i.filter(function (t) {
					return t.$empty
				}).length && this.events.fire("dataRequest", [t, e]), i
			}, d.prototype._add = function (t, e) {
				if (this.isDataLoaded()) {
					e = this._addCore(t, e);
					return this._onChange("add", t.id, t), this.events.fire(u.DataEvents.afterAdd, [t]), e
				}
				c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype._remove = function (t) {
				if (this.isDataLoaded()) {
					var e = this._pull[t];
					if (e) {
						if (!this.events.fire(u.DataEvents.beforeRemove, [e])) return;
						this._removeCore(e.id), this._onChange("remove", t, e)
					}
					this.events.fire(u.DataEvents.afterRemove, [e])
				} else c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype._copy = function (t, e, i, n, o) {
				if (this.isDataLoaded()) {
					if (!this.exists(t)) return null;
					var r = p.uid();
					return (o && (e = -1 === e ? -1 : e + o), i) ? i instanceof d || !n ? i.exists(t) ? (i.add(a(a({}, c.copyWithoutInner(this.getItem(t))), {
						id: r
					}), e), r) : (i.add(c.copyWithoutInner(this.getItem(t)), e), t) : void i.add(c.copyWithoutInner(this.getItem(t)), e) : (this.add(a(a({}, c.copyWithoutInner(this.getItem(t))), {
						id: r
					}), e), r)
				}
				c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype._move = function (t, e, i, n, o, r) {
				if (this.isDataLoaded()) {
					if (o && (e = -1 === e ? -1 : e + o), i && i !== this && this.exists(t)) {
						var s = p.copy(this.getItem(t), !0);
						return r && (s.id = r), (!r && i.exists(t) || i.exists(r)) && (s.id = p.uid()), n && (s.parent = n), i.add(s, e), this.remove(t), s.id
					}
					if (this.getIndex(t) === e) return null;
					s = this._order.splice(this.getIndex(t), 1)[0];
					return -1 === e && (e = this._order.length), this._order.splice(e, 0, s), this.events.fire(u.DataEvents.change, [t, "update", this.getItem(t)]), t
				}
				c.dhxWarning("the method doesn't work with lazyLoad")
			}, d.prototype._addCore = function (t, e) {
				var i;
				return this.config.init && (t = this.config.init(t)), t.id = null !== (i = t.id) && void 0 !== i ? i : p.uid(), this._pull[t.id] && c.dhxError("Item " + t.id + " already exist"), this._initOrder && !c.isTreeCollection(this) && this._addToOrder(this._initOrder, t, e), this._addToOrder(this._order, t, e), t.id
			}, d.prototype._removeCore = function (e) {
				0 <= this.getIndex(e) && (this._order = this._order.filter(function (t) {
					return t.id !== e
				}), delete this._pull[e]), this._initOrder && this._initOrder.length && (this._initOrder = this._initOrder.filter(function (t) {
					return t.id !== e
				}), delete this._pull[e])
			}, d.prototype._parse_data = function (t) {
				var e = this._order.length;
				this.config.prep && (t = this.config.prep(t));
				for (var i = 0, n = t; i < n.length; i++) {
					var o = n[i];
					this._addCore(o, e++)
				}
			}, d.prototype._approximate = function (t, e, i) {
				for (var n = t.length, o = e.length, r = Math.floor(n / i), s = Array(Math.ceil(n / r)), a = 0, l = 0; l < n; l += r) {
					for (var c = p.copy(t[l]), u = Math.min(n, l + r), d = 0; d < o; d++) {
						for (var h = 0, f = l; f < u; f++) h += t[f][e[d]];
						c[e[d]] = h / (u - l)
					}
					s[a++] = c
				}
				return s
			}, d.prototype._onChange = function (t, e, i) {
				for (var n = 0, o = this._changes.order; n < o.length; n++)
					if ((s = o[n]).id === e && !s.saving) {
						s.error && (s.error = !1), "add" === s.status && (t = "add");
						var r = this._changes.order.indexOf(s),
							s = a(a({}, s), {
								obj: i,
								status: t
							});
						return (this._changes.order.splice(r, 1, s), this._loader.updateChanges(this._changes), "remove" === t && i.$emptyRow) ? void 0 : void this.events.fire(u.DataEvents.change, [e, t, i])
					} this._changes.order.push({
						id: e,
						status: t,
						obj: a({}, i),
						saving: !1
					}), this._loader.updateChanges(this._changes), this.events.fire(u.DataEvents.change, [e, t, i])
			}, d.prototype._addToOrder = function (t, e, i) {
				0 <= i && t[i] ? (this._pull[e.id] = e, t.splice(i, 0, e)) : (this._pull[e.id] = e, t.push(e))
			}, d.prototype._applySmart = function () {
				this._filter && this._applyFilters(), this._sorter && this._applySorters()
			}, d.prototype._applySorters = function (t) {
				this._sort.sort(this._order, t, this._sorter), this._initOrder && this._initOrder.length && this._sort.sort(this._initOrder, t, this._sorter)
			}, d.prototype._applyFilters = function (e) {
				var t, i = this._filter;
				e === i && (e = null), (e || i) && (t = this._order.filter(function (t) {
					return (!e || e(t)) && (!i || i(t))
				}), this._initOrder || (this._initOrder = this._order), this._order = t)
			}, d);

		function d(t, e) {
			var n = this;
			this._changes = {
				order: []
			}, this.config = t || {}, this._sort = new s.Sort, this._loader = new r.Loader(this, this._changes), this.events = e || new o.EventSystem(this), this.events.on("dataRequest", function (t, e) {
				var i = n.dataProxy;
				i && i.updateUrl && (i.updateUrl(null, {
					from: t,
					limit: i.config.limit || e - t
				}), n.load(i))
			}), this.events.on(u.DataEvents.loadError, function (t) {
				"string" != typeof t ? c.dhxError(t) : c.dhxWarning(t)
			}), this._reset()
		}
		e.DataCollection = i
	}, function (t, e, i) {
		"use strict";
		var n = this && this.__assign || function () {
			return (n = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(65),
			r = i(66),
			i = i(145);
		e.dataDrivers = {
			json: o.JsonDriver,
			csv: r.CsvDriver
		}, e.dataDriversPro = n(n({}, e.dataDrivers), {
			xml: i.XMLDriver
		})
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = (o.prototype.toJsonArray = function (t) {
			return this.getRows(t)
		}, o.prototype.serialize = function (t) {
			return t
		}, o.prototype.getFields = function (t) {
			return t
		}, o.prototype.getRows = function (t) {
			return "string" == typeof t ? JSON.parse(t) : t
		}, o);

		function o() { }
		e.JsonDriver = n
	}, function (t, e, i) {
		"use strict";
		var n = this && this.__assign || function () {
			return (n = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = (r.prototype.getFields = function (t, e) {
			for (var i = t.trim().split(this.config.columnDelimiter), n = {}, o = 0; o < i.length; o++) n[e ? e[o] : o + 1] = isNaN(Number(i[o])) ? i[o] : parseFloat(i[o]);
			return n
		}, r.prototype.getRows = function (t) {
			return t.trim().split(this.config.rowDelimiter)
		}, r.prototype.toJsonArray = function (t) {
			var e = this,
				i = this.getRows(t),
				n = this.config.names;
			return this.config.skipHeader && (t = i.splice(0, this.config.skipHeader), this.config.nameByHeader && (n = t[0].trim().split(this.config.columnDelimiter))), i.map(function (t) {
				return e.getFields(t, n)
			})
		}, r.prototype.serialize = function (t, e) {
			var i = t[0] ? Object.keys(t[0]).filter(function (t) {
				return !t.startsWith("$")
			}).join(this.config.columnDelimiter) + this.config.columnDelimiter + this.config.rowDelimiter : "",
				t = this._serialize(t);
			return e ? t : i + t
		}, r.prototype._serialize = function (t) {
			var o = this;
			return t.reduce(function (t, n) {
				var e = Object.keys(n).reduce(function (t, e, i) {
					return e.startsWith("$") || "items" === e ? t : "" + t + n[e] + (i === n.length - 1 ? "" : o.config.columnDelimiter)
				}, "");
				return n.items ? t + (t ? "\n" : "") + e + o._serialize(n.items) : "" + t + (t ? o.config.rowDelimiter : "") + e
			}, "")
		}, r);

		function r(t) {
			this.config = n(n({}, {
				skipHeader: 0,
				nameByHeader: !1,
				rowDelimiter: "\n",
				columnDelimiter: ","
			}), t), this.config.nameByHeader && (this.config.skipHeader = 1)
		}
		e.CsvDriver = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			_ = this && this.__assign || function () {
				return (_ = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			r = i(3),
			l = i(2),
			p = i(1),
			c = i(13),
			u = i(4),
			h = i(6),
			d = i(150),
			g = i(22),
			f = i(33),
			m = i(16),
			v = i(151),
			y = i(9),
			b = i(68),
			w = i(23),
			x = i(175),
			E = i(176),
			C = i(10),
			k = i(177),
			S = i(18),
			o = (s = u.View, o(I, s), I.prototype.destructor = function () {
				this._destroyContent(), this.keyManager && this.keyManager.destructor(), this.events.events = {}, this.events.context = null, this._activeFilters = this._filterData = this._scroll = this.content = null, this.unmount()
			}, I.prototype.setColumns = function (t) {
				var e = this;
				this.config.columns = t, this._parseColumns(!0), this._adjustColumns(), this._checkFilters(), this._checkMarks(), this.paint(), a.awaitRedraw().then(function () {
					e.config.keyNavigation && e._initHotKey(!0)
				})
			}, I.prototype.addRowCss = function (t, e) {
				var i = this.data.getItem(t),
					t = i.$css || "";
				t.match(new RegExp(e, "g")) || (i.$css = t + " " + e, this.paint())
			}, I.prototype.removeRowCss = function (t, e) {
				t = this.data.getItem(t), e = t.$css ? t.$css.replace(e, "") : "";
				t.$css = e, this.paint()
			}, I.prototype.addCellCss = function (t, e, i) {
				e = this._getColumn(e);
				e && (e.$cellCss[t] ? e.$cellCss[t] += e.$cellCss[t].match(new RegExp(i, "g")) ? "" : " " + i : this.data.getItem(t) && (e.$cellCss[t] = i + " "), this.paint())
			}, I.prototype.removeCellCss = function (t, e, i) {
				e = this._getColumn(e);
				e && (e.$cellCss[t] ? (e.$cellCss[t] = e.$cellCss[t].replace(i, ""), this.paint()) : this.data.getItem(t) && (e.$cellCss[t] = ""))
			}, I.prototype.showColumn = function (t) {
				var e = this._getColumn(t);
				e && e.hidden && this.events.fire(y.GridEvents.beforeColumnShow, [e]) && (e.hidden = !1, this.config.$totalWidth += e.$width, (t = this._hiddenFilters && this._hiddenFilters[e.id]) && (this._activeFilters[e.id] = t, delete this._hiddenFilters[e.id]), this.paint(), this._checkFilters(), this.events.fire(y.GridEvents.afterColumnShow, [e]))
			}, I.prototype.hideColumn = function (t) {
				var e = this._getColumn(t);
				e && !e.hidden && this.events.fire(y.GridEvents.beforeColumnHide, [e]) && (e.hidden = !0, this.config.$totalWidth -= e.$width, (t = this._activeFilters && this._activeFilters[e.id]) && (this._hiddenFilters || (this._hiddenFilters = {}), this._hiddenFilters[e.id] = t, delete this._activeFilters[e.id], this.data.filter()), this.paint(), this._checkFilters(), this.events.fire(y.GridEvents.afterColumnHide, [e]))
			}, I.prototype.isColumnHidden = function (t) {
				t = this._getColumn(t);
				if (t) return !!t.hidden
			}, I.prototype.showRow = function (t) {
				var e;
				p.isDefined(t) && (e = t.toString(), (t = this.data.getItem(e)) && t.hidden && this.events.fire(y.GridEvents.beforeRowShow, [t]) && (this.data.update(e, {
					hidden: !1
				}), this.data.filter(function (t) {
					return !t.hidden
				}), this.events.fire(y.GridEvents.afterRowShow, [t])))
			}, I.prototype.hideRow = function (t) {
				var e;
				p.isDefined(t) && (e = t.toString(), (t = this.data.getItem(e)) && this.events.fire(y.GridEvents.beforeRowHide, [t]) && (this.data.update(e, {
					hidden: !0
				}), this.data.filter(function (t) {
					return !t.hidden
				}), this.events.fire(y.GridEvents.afterRowHide, [t])))
			}, I.prototype.isRowHidden = function (t) {
				if (p.isDefined(t)) {
					t = this.data.getItem(t.toString());
					return t ? !!t.hidden : void 0
				}
			}, I.prototype.getScrollState = function () {
				return {
					x: this._scroll.left,
					y: this._scroll.top
				}
			}, I.prototype.scroll = function (t, e) {
				var i = this.getRootView().refs.grid_body.el;
				i.scrollLeft = "number" == typeof t ? t : i.scrollLeft, i.scrollTop = "number" == typeof e ? e : i.scrollTop, this.paint()
			}, I.prototype.scrollTo = function (e, i) {
				var t = this.selection.getCell(),
					n = this.config.columns.filter(function (t) {
						return !t.hidden
					}),
					o = p.findIndex(n, function (t) {
						return t.id == i
					}),
					r = t ? t.column : this.config.columns[0],
					s = p.findIndex(n, function (t) {
						return t.id == r.id
					}),
					a = this.config.leftSplit ? m.getTotalWidth(n.slice(0, this.config.leftSplit)) : 0,
					l = m.getTotalWidth(n.slice(0, o)) - (o - s < 0 ? a : 0),
					c = this.data.getRawData(0, -1),
					u = p.findIndex(c, function (t) {
						return t.id == e
					}),
					d = m.getTotalHeight(c.slice(0, u)),
					h = this.getScrollState(),
					f = this.config.width + h.x,
					t = this.config.height + h.y - this.config.headerRowHeight * this.config.$headerLevel,
					s = d - h.y - c[u].$height,
					a = l - h.x - n[o].$width,
					t = d + 2 * c[u].$height + 18 - t,
					f = l + 2 * n[o].$width + 18 - f,
					t = 0 < s && t < 0 ? 0 : s < 0 ? s : t,
					f = 0 < a && f < 0 ? 0 : a < 0 ? a : f;
				this.scroll(f + h.x, t + h.y)
			}, I.prototype.adjustColumnWidth = function (e, t) {
				void 0 === t && (t = !0);
				var i = this.config.columns.filter(function (t) {
					return !t.hidden
				}),
					n = i.filter(function (t) {
						return t.id === e
					}),
					r = this._adjustColumnsWidth(this.config.data, n, t);
				this.config.$totalWidth = i.reduce(function (t, e) {
					var i, n, o;
					return r[e.id] && (e.$fixed = !0, i = e.maxWidth, n = e.minWidth, o = r[e.id], e.$width = o, i && i < o && (e.$width = i), n && e.$width < n && (e.$width = n)), t + e.$width
				}, 0), this.paint()
			}, I.prototype.getCellRect = function (e, i) {
				var t = this.config.columns.filter(function (t) {
					return !t.hidden
				}),
					n = this.data.getRawData(0, -1),
					o = this.getSpan(e, i),
					r = p.findIndex(t, function (t) {
						return t.id == i
					}),
					s = p.findIndex(n, function (t) {
						return t.id == e
					});
				return {
					x: m.getTotalWidth(t.slice(0, r)),
					y: m.getTotalHeight(n.slice(0, s)),
					height: null != o && o.rowspan ? f.getHeight(n, o.rowspan, s) : n[s] ? n[s].$height : 0,
					width: null != o && o.colspan ? f.getWidth(t, o.colspan, r) : t[r] ? t[r].$width : 0
				}
			}, I.prototype.getColumn = function (e) {
				var t = p.findIndex(this.config.columns, function (t) {
					return t.id == e
				});
				if (0 <= t) return this.config.columns[t]
			}, I.prototype.addSpan = function (e) {
				this.config.spans = this.config.spans || [];
				var t = p.findIndex(this.config.spans, function (t) {
					return "" + t.row == "" + e.row && "" + t.column == "" + e.column
				});
				0 <= t ? this.config.spans[t] = e : (this.config.spans.push(e), this.paint())
			}, I.prototype.getSpan = function (e, i) {
				if (this.config.spans) {
					var t = p.findIndex(this.config.spans, function (t) {
						return "" + t.row == "" + e && "" + t.column == "" + i
					});
					return this.config.spans[t]
				}
			}, I.prototype.removeSpan = function (e, i) {
				var t;
				this.config.spans && (t = p.findIndex(this.config.spans, function (t) {
					return "" + t.row == "" + e && "" + t.column == "" + i
				}), this.config.spans.splice(t, 1), this.paint())
			}, I.prototype.editCell = function (t, e, i) {
				var n, o = this.data.getItem(t),
					r = this.getColumn(e);
				o && r ? (n = r.editorType, i || ("date" === r.type && (i = "datePicker"), "boolean" === r.type && (i = "checkbox"), n && (i = n)), this.events.fire(y.GridEvents.beforeEditStart, [o, r, i]) && (null !== (n = this.config.$editable) && void 0 !== n && n.editor || this.config.$editable && this.config.$editable.row === t && this.config.$editable.col === e && this.config.$editable.editorType === i || (this.config.$editable = {
					row: o.id,
					col: r.id,
					editorType: i
				}, this.selection.config.disabled || this.selection.setCell(t.toString(), e.toString()), this.paint(), this.events.fire(y.GridEvents.afterEditStart, [o, r, i])))) : h.dhxWarning("item not found")
			}, I.prototype.editEnd = function (t) {
				this.config.$editable && this.config.$editable.editor && this.config.$editable.editor.endEdit(t)
			}, I.prototype.getSortingState = function () {
				return {
					dir: this._sortDir,
					by: this._sortBy
				}
			}, I.prototype.getHeaderFilter = function (i) {
				var n = this,
					t = this.getColumn(i);
				if (t) {
					var o = null;
					return t.header.forEach(function (t) {
						var e;
						t.content && (e = n.content[t.content].element[i], o = "comboFilter" === t.content ? e : n.getRootView().refs[i + "_filter"].el)
					}), o
				}
			}, I.prototype.edit = function (t, e, i) {
				this.editCell(t, e, i)
			}, I.prototype._createView = function () {
				var i = this;
				return a.create({
					render: function (t, e) {
						return b.render(t, e, i._htmlEvents, i.selection, i._uid)
					}
				}, this)
			}, I.prototype._parseColumns = function (t) {
				void 0 === t && (t = !1), g.normalizeColumns(this.config, t);
				t = this.config.columns.filter(function (t) {
					return !t.hidden
				});
				g.countColumns(this.config, t)
			}, I.prototype._parseData = function () {
				this.config.data = this._prepareData(this.data), this._detectColsTypes(), this._checkFilters(), this._checkMarks(), this.data.filter(function (t) {
					return !t.hidden
				}), this._render()
			}, I.prototype._createCollection = function (t) {
				this.data = new h.DataCollection({
					prep: t
				}, this.events)
			}, I.prototype._getRowIndex = function (t) {
				return this.data.getIndex(t)
			}, I.prototype._setEventHandlers = function () {
				function i(t) {
					return 1
				}
				var d = this;
				this.data.events.on(h.DataEvents.load, function () {
					d.data.filter(function (t) {
						return t
					}), d._parseData(), d._checkFilters()
				}), this.data.events.on(h.DataEvents.change, function (t, e, i) {
					if ("setPage" !== e) {
						if ("add" !== e && "update" !== e && "remove" !== e || (d.config.data = d._prepareData(d.data)), t && (d._filterData = d.data.map(function (t) {
							return t
						}) || [], d._checkFilters()), d._detectColsTypes(), d._removeMarks(), d._checkMarks(), d.config.autoEmptyRow && (!d._activeFilters || p.isEmptyObj(d._activeFilters))) {
							e = d.data.find({
								by: "$emptyRow",
								match: !0
							});
							if (e) {
								if (e.id === t) return;
								d.data.move(e.id, d.data.getLength() - 1)
							} else d._addEmptyRow()
						}
						d._render()
					} else a.awaitRedraw().then(function () {
						d.scrollTo(d.data.getId(i[0]).toString(), d.config.columns[0].id.toString()), d._render()
					})
				}), this.data.events.on(h.DataEvents.removeAll, function () {
					d.config.columns.map(function (e) {
						e.header.map(function (t) {
							!t.content || "selectFilter" !== t.content && "comboFilter" !== t.content || (e.$uniqueData = [])
						})
					})
				}), this.events.on(h.DragEvents.beforeDrag, function (t, e) {
					return d.data.getItem(t.start) ? d.events.fire(y.GridEvents.beforeRowDrag, [t, e]) : "column" === d.config.dragItem || "both" === d.config.dragItem ? d.events.fire(y.GridEvents.beforeColumnDrag, [t, e]) : void 0
				}), this.events.on(h.DragEvents.dragStart, function (t, e) {
					i({
						$dragtarget: !0
					}), d.data.getItem(t.start) ? d.events.fire(y.GridEvents.dragRowStart, [t, e]) : "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.dragColumnStart, [t, e])
				}), this.events.on(h.DragEvents.dragIn, function (t, e) {
					d.data.getItem(t.start) ? d.events.fire(y.GridEvents.dragRowIn, [t, e]) : "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.dragColumnIn, [t, e])
				}), this.events.on(h.DragEvents.dragOut, function (t, e) {
					d.data.getItem(t.start) ? d.events.fire(y.GridEvents.dragRowOut, [t, e]) : "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.dragColumnOut, [t, e])
				}), this.events.on(h.DragEvents.canDrop, function (t, e) {
					i({
						$drophere: !0
					}), d.data.getItem(t.start) ? d.events.fire(y.GridEvents.canRowDrop, [t, e]) : "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.canColumnDrop, [t, e])
				}), this.events.on(h.DragEvents.cancelDrop, function (t, e) {
					i({
						$drophere: void 0
					}), d.data.getItem(t.start) ? d.events.fire(y.GridEvents.cancelRowDrop, [t, e]) : "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.cancelColumnDrop, [t, e])
				}), this.events.on(h.DragEvents.beforeDrop, function (t, e) {
					return "row" !== d.config.dragItem || "both" !== d.config.dragMode && "target" !== d.config.dragMode ? "column" === d.config.dragItem || "both" === d.config.dragItem ? d.events.fire(y.GridEvents.beforeColumnDrop, [t, e]) : void 0 : d.events.fire(y.GridEvents.beforeRowDrop, [t, e])
				}), this.events.on(h.DragEvents.afterDrop, function (e, t) {
					var i;
					if (d.data.getItem(e.start)) {
						d.events.fire(y.GridEvents.afterRowDrop, [e, t]);
						for (var n = d.data.getItem(e.start), o = 0, r = d.config.columns; o < r.length; o++) {
							var s = r[o];
							void 0 === n[s.id] && d.data.update(n.id, ((i = {})[s.id] = null, i), !0)
						}
						var a, l = d.data.getInitialData(),
							c = null == l ? void 0 : l.findIndex(function (t) {
								return t.id === e.start
							}),
							u = null == l ? void 0 : l.findIndex(function (t) {
								return t.id === e.target
							});
						for (a in c && -1 < u && null != l && l.splice(u, 0, null == l ? void 0 : l.splice(c, 1)[0]), d.config.data = d._prepareData(l || d.data.map(function (t) {
							return t
						})), d.data.parse(d.config.data), d._activeFilters) d.data.filter(d._activeFilters[a], {
							add: !0
						})
					} else "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.afterColumnDrop, [e, t])
				}), this.events.on(h.DragEvents.afterDrag, function (t, e) {
					i({
						$dragtarget: void 0
					}), d.data.getItem(t.start) ? d.events.fire(y.GridEvents.afterRowDrag, [t, e]) : "column" !== d.config.dragItem && "both" !== d.config.dragItem || d.events.fire(y.GridEvents.afterColumnDrag, [t, e]), d.config.data = d._prepareData(d.data instanceof Array ? d.data.map(function (t) {
						return t
					}) : d.data.getInitialData() || d.data), d.data.parse(d.config.data)
				}), this.events.on(y.GridEvents.cellMouseDown, function (t, e, i) {
					i.targetTouches ? (d._touch.timer = setTimeout(function () {
						d._dragStart(i)
					}, d._touch.duration), d._touch.timeStamp ? (d._touch.dblDuration >= d._touch.timeStamp - +i.timeStamp.toFixed() && ((!1 !== e.editable && d.config.editable || e.editable) && d.editCell(t.id, e.id, e.editorType), i.preventDefault(), d.events.fire(y.GridEvents.cellDblClick, [t, e, i])), d._touch.timeStamp = null) : d._touch.timeStamp = +i.timeStamp.toFixed(), setTimeout(function () {
						d._touch.timeStamp = null
					}, d._touch.dblDuration)) : d._dragStart(i)
				}), this._events.on(y.GridSystemEvents.cellTouchMove, function (t, e, i) {
					d._touch.start && i.preventDefault(), d._clearTouchTimer()
				}), this._events.on(y.GridSystemEvents.cellTouchEnd, function () {
					d._touch.start = !1, d._clearTouchTimer()
				}), this.events.on(y.GridEvents.filterChange, function (t, e, i) {
					t = null != t ? t : "", !d.config.autoEmptyRow || (o = d.data.find({
						by: "$emptyRow",
						match: !0
					})) && d.data.remove(o.id), d._activeFilters || (d._activeFilters = {});
					var n, o = d._getColumn(e);
					for (n in "" !== t ? d._activeFilters[e] = {
						by: e,
						match: t,
						compare: d.content[i].match,
						multi: "multiselect" === (null == o ? void 0 : o.editorType)
					} : delete d._activeFilters[e], d.data.filter(function (t) {
						return t
					}), d._activeFilters) d.data.filter(d._activeFilters[n], {
						add: !0
					})
				}), this.events.on(y.GridEvents.scroll, function (t) {
					d._scroll = {
						top: t.y,
						left: t.x
					}, d.editEnd(), d.paint()
				}), this.events.on(y.GridEvents.cellDblClick, function (t, e) {
					(!1 !== e.editable && d.config.editable || e.editable) && d.editCell(t.id, e.id, e.editorType)
				}), this.events.on(y.GridEvents.afterEditEnd, function (e, t, i) {
					var n, o, r;
					null !== (o = d.config.$editable) && void 0 !== o && o.editor && (d.config.$editable.col !== i.id || d.config.$editable.row !== t.id) || (o = d.config.$editable ? (n = d.config.$editable.row, d.config.$editable.col) : (n = t.id, i.id), delete (t = d.data.getItem(n)).$emptyRow, void 0 !== e && (r = "select" === i.editorType || "combobox" === i.editorType ? null === (r = i.options) || void 0 === r ? void 0 : r.find(function (t) {
						return t.id === e
					}) : null, d.data.update(n, _(_({}, t), ((t = {})[o] = (null == r ? void 0 : r.value) || e, t)))), d.config.$editable = null, d._checkFilters(), d.paint())
				}), this.events.on(y.GridEvents.headerCellMouseDown, function (t, e) {
					var i = e.target.getAttribute("dhx_resized");
					i && d.events.fire(y.GridEvents.beforeResizeStart, [t, e]) && E.startResize(d, i.toString(), e, function () {
						d.paint(), d.config.$resizing = null, d.events.fire(y.GridEvents.afterResizeEnd, [t, e])
					}), e.targetTouches && (d._touch.timeStamp ? (d._touch.dblDuration >= d._touch.timeStamp - +e.timeStamp.toFixed() && (e.preventDefault(), d.events.fire(y.GridEvents.headerCellDblClick, [t, e])), d._touch.timeStamp = null) : d._touch.timeStamp = +e.timeStamp.toFixed(), setTimeout(function () {
						d._touch.timeStamp = null
					}, d._touch.dblDuration))
				}), this.events.on(y.GridEvents.footerCellDblClick, function (t, e) {
					e.targetTouches && (d._touch.timeStamp ? (d._touch.dblDuration >= d._touch.timeStamp - +e.timeStamp.toFixed() && (e.preventDefault(), d.events.fire(y.GridEvents.footerCellDblClick, [t, e])), d._touch.timeStamp = null) : d._touch.timeStamp = +e.timeStamp.toFixed(), setTimeout(function () {
						d._touch.timeStamp = null
					}, d._touch.dblDuration))
				}), this.events.on(y.GridEvents.resize, function () {
					d._parseColumns(), d._checkFilters()
				})
			}, I.prototype._addEmptyRow = function () {
				var t = this.data.getId(this.data.getLength() - 1),
					e = this.data.getItem(t),
					t = this.config.columns.filter(function (t) {
						return !t.hidden
					});
				m.isRowEmpty(e) || this.data.add(t.reduce(function (t, e) {
					return t[e.id] = "", t
				}, {
					$emptyRow: !0
				}))
			}, I.prototype._sort = function (i, t, e) {
				var n = this;
				t ? this._sortDir = t : this._sortBy === i ? this._sortDir = "asc" === this._sortDir ? "desc" : "asc" : this._sortDir = "asc";
				this._sortBy = i, this.data.sort({
					by: i,
					dir: this._sortDir,
					as: null != e ? e : function (t) {
						var e = n.getColumn(i);
						return t && "date" === e.type ? ("string" == typeof t ? w.stringToDate(t, e.format) : t).getTime() : t ? "" + t : ""
					}
				}), this.events.fire(y.GridEvents.afterSort, [this.getColumn(i), this._sortDir])
			}, I.prototype._clearTouchTimer = function () {
				this._touch.timer && (clearTimeout(this._touch.timer), this._touch.timer = null)
			}, I.prototype._checkFilters = function () {
				var o = this,
					i = this._filterData;
				if (i) {
					this.config.columns.forEach(function (e) {
						e.header.forEach(function (t) {
							!t.content || "selectFilter" !== t.content && "comboFilter" !== t.content || (t = e.header.find(function (t) {
								return t.filterConfig
							}), t = g.getUnique(i, e.id, t ? t.filterConfig.multiselection : null), e.$uniqueData && e.$uniqueData.length > t.length ? t.forEach(function (t) {
								e.$uniqueData.includes(t) || e.$uniqueData.push(t)
							}) : e.$uniqueData = t)
						})
					});
					var t, r = this;
					for (t in this._activeFilters) ! function (e) {
						var i = r.config.columns.find(function (t) {
							return t.id === e
						}),
							t = i.header.find(function (t) {
								return !!t.content
							}),
							n = !1,
							n = Array.isArray(r._activeFilters[e].match) ? r._activeFilters[e].match.reduce(function (t, e) {
								if (i.$uniqueData.find(function (t) {
									return t.toString() === e
								})) return !0
							}, !1) : i.$uniqueData.find(function (t) {
								return t.toString() === o._activeFilters[e].match
							});
						!t || "selectFilter" !== t.content && "comboFilter" !== t.content || n ? r.data.filter(r._activeFilters[e], {
							add: !0
						}) : (delete r._activeFilters[e], r.data.filter())
					}(t)
				}
			}, I.prototype._adjustColumns = function () {
				var t, r, e, i = this;
				"boolean" == typeof this.config.adjust || "data" === this.config.adjust || "header" === this.config.adjust || "footer" === this.config.adjust ? (e = this.config.columns.filter(function (t) {
					return !t.hidden
				})).length && (t = this.config.data && this.config.data.length ? this.config.data : this.data.map(function (t) {
					return t
				}), r = this._adjustColumnsWidth(t, e), this.config.$totalWidth = e.reduce(function (t, e) {
					e.$fixed = !0;
					var i = e.maxWidth,
						n = e.minWidth,
						o = r[e.id];
					return e.$width = o, i && i < o && (e.$width = i), n && e.$width < n && (e.$width = n), t + e.$width
				}, 0)) : (e = this.config.columns.filter(function (t) {
					return !t.hidden && t.adjust
				})).length && e.forEach(function (t) {
					i.adjustColumnWidth(t.id, t.adjust)
				})
			}, I.prototype._prepareData = function (t) {
				var e = this;
				return this.config.autoHeight && (this.config.autoHeight = !1), this._adjustColumns(), t.map(function (t) {
					return t.$height = t.height || e.config.rowHeight, t
				})
			}, I.prototype._adjustColumnsWidth = function (t, e, i) {
				void 0 === i && (i = this.config.adjust);
				var n = {};
				if ("header" === i || !0 === i) {
					var o = e.filter(function (t) {
						return t.header
					});
					if (u = g.getMaxColsWidth(this._prepareColumnData(o, "header"), o, {
						font: "bold 14.4px Arial"
					}, "header"))
						for (var r = 0, s = Object.entries(u); r < s.length; r++) var a = s[r],
							l = a[0],
							c = a[1],
							n = Object.assign(n, ((a = {})[l] = +c + (m.isSortable(this.config, this.getColumn(l)) ? 36 : 16), a))
				}
				if ("footer" === i || !0 === i) {
					var u, o = e.filter(function (t) {
						return t.footer
					});
					if (u = g.getMaxColsWidth(this._prepareColumnData(o, "footer"), o, {
						font: "bold 14.4px Arial"
					}, "footer"))
						for (var d = 0, h = Object.entries(u); d < h.length; d++) {
							var f = h[d],
								l = f[0],
								c = f[1];
							(n[l] && n[l] < +c + 16 || !n[l]) && (n = Object.assign(n, ((f = {})[l] = +c + 16, f)))
						}
				}
				if (("data" === i || !0 === i) && (u = g.getMaxColsWidth(t, e, {
					font: "normal 14.4px Arial"
				}, "data")))
					for (var p = 0, _ = Object.entries(u); p < _.length; p++) {
						var v = _[p],
							l = v[0],
							c = v[1];
						(n[l] && n[l] < +c + 16 || !n[l]) && (n = Object.assign(n, ((v = {})[l] = +c + 16, v)))
					}
				return n
			}, I.prototype._prepareColumnData = function (t, e) {
				for (var i, n, o = [], r = 0; r < t.length; r++) {
					for (var s = [], a = 0; a < (null === (i = t[a]) || void 0 === i ? void 0 : i[e].length); a++) {
						var l = {};
						null !== (i = null === (i = t[r]) || void 0 === i ? void 0 : i[e][a]) && void 0 !== i && i.text ? (l[t[r].id] = (null === (n = null === (n = t[r]) || void 0 === n ? void 0 : n[e][a]) || void 0 === n ? void 0 : n.text) || "", s.push(l)) : null !== (n = null === (n = t[r]) || void 0 === n ? void 0 : n[e][a]) && void 0 !== n && n.content && (l[t[r].id] = this.content[null === (n = null === (n = t[r]) || void 0 === n ? void 0 : n[e][a]) || void 0 === n ? void 0 : n.content].toHtml(this.getColumn(t[r].id), this.config) || "", s.push(l))
					}
					for (var c = 0; c < s.length; c++)
						for (var u = 0, d = Object.entries(s[c]); u < d.length; u++) {
							var h = d[u],
								f = h[0],
								p = h[1];
							o[c] = _({}, o[c]) || {}, o[c] = Object.assign(o[c], ((h = {})[f] = p, h))
						}
				}
				return o
			}, I.prototype._dragStart = function (t) {
				if (this.config.dragMode && ("row" === this.config.dragItem || "both" === this.config.dragItem) && !this.config.$editable) {
					var e = this._getColumn(t.target.getAttribute("dhx_col_id"));
					if (!1 !== (null == e ? void 0 : e.draggable)) {
						e = l.locateNode(t, "dhx_id"), e = e && e.getAttribute("dhx_id");
						return t.targetTouches && (this._touch.start = !0), h.dragManager.onMouseDown(t, [e])
					}
				}
			}, I.prototype._getColumn = function (t) {
				for (var e = 0, i = this.config.columns; e < i.length; e++) {
					var n = i[e];
					if (n.id == t) return n
				}
			}, I.prototype._init = function () {
				this.events = new r.EventSystem(this), this._events = new r.EventSystem(this), this._attachDataCollection(), this.export = new d.Exporter(this), this._setEventHandlers()
			}, I.prototype._attachDataCollection = function () {
				var e = this;
				if (this.config.data instanceof h.DataCollection) return this.data = this.config.data, void (this.config.data = this.data.serialize());
				this._createCollection(function (t) {
					return t.spans && (e.config.spans = t.spans, t = t.data), t
				})
			}, I.prototype._setMarks = function (n, o) {
				for (var t = this.data.map(function (t) {
					return {
						id: t.id,
						data: t[n.id],
						row: t
					}
				}), r = this.data.map(function (t) {
					return t[n.id]
				}), e = 0, i = t; e < i.length; e++) ! function (t) {
					var e, i = o(t.data, r, t.row, n);
					i && (n.$cellCss = n.$cellCss || {}, e = (n.$cellCss[t.id] || "").split(" "), i.split(" ").map(function (t) {
						e.includes(t) || e.push(t)
					}), n.$cellCss[t.id] = e.join(" "))
				}(i[e])
			}, I.prototype._checkMarks = function () {
				var e = this;
				this.config.columns.map(function (t) {
					var o = t.mark;
					o && ("function" == typeof o ? e._setMarks(t, o) : e._setMarks(t, function (t, e) {
						var i = [];
						e.forEach(function (t) {
							null != t && "" !== t && i.push(parseFloat(t))
						});
						var n = p.getMinArrayNymber(i),
							e = p.getMaxArrayNymber(i);
						return o.max && e === parseFloat(t) ? o.max : !(!o.min || n !== parseFloat(t)) && o.min
					}))
				})
			}, I.prototype._removeMarks = function () {
				this.config.columns.forEach(function (t) {
					t.mark && (t.$cellCss = {})
				})
			}, I.prototype._detectColsTypes = function () {
				this.config.columns.forEach(function (t) {
					return t.type ? t : t.format ? (t.type = "number", t) : void (t.type || (t.type = "string"))
				})
			}, I.prototype._destroyContent = function () {
				for (var t in this.content) "comboFilter" === t && this.content[t].destroy()
			}, I.prototype._render = function () {
				this.paint()
			}, I.prototype._initHotKey = function (t) {
				void 0 === t && (t = !1);
				var e = k.getKeysHandlers(this);
				for (i in e) this.keyManager.exist(i) || this.keyManager.addHotKey(i, e[i]);
				if (!t)
					for (var i in this.config.hotkeys) this.keyManager.addHotKey(i, this.config.hotkeys[i])
			}, I);

		function I(t, e) {
			var r = s.call(this, t, e) || this;
			r._touch = {
				duration: 350,
				dblDuration: 300,
				timer: null,
				start: !1,
				timeStamp: null
			}, r.config = p.extend({
				rowHeight: 40,
				headerRowHeight: 40,
				footerRowHeight: 40,
				keyNavigation: !0,
				sortable: !0,
				columns: [],
				data: [],
				tooltip: !0,
				rootParent: "string" == typeof t && t || r._uid,
				headerSort: !0
			}, e), r.content = x.getContent(), r._scroll = {
				top: 0,
				left: 0
			}, r.config.autoWidth = r.config.autoWidth || r.config.fitToContainer, r.config.adjust = r.config.adjust || r.config.columnsAutoWidth, r.config.editable = r.config.editable || r.config.editing, r.config.leftSplit = r.config.leftSplit || r.config.splitAt, r.config.sortable && r.config.headerSort || (r.config.sortable = !1), r.config.columns.forEach(function (t) {
				t.format = t.format || t.dateFormat
			});

			function n(t, e, i) {
				var n;
				t && e && m.isTooltip(r.config, e) && (n = g.toFormat(t[e.id], e.type, e.format), e.tooltipTemplate ? n = e.tooltipTemplate(n, t, e) : n && e.template && (n = e.template(n, t, e)), n && C.tooltip(n, {
					css: "dhx_grid_tooltip",
					node: i,
					htmlEnable: m.isHtmlEnable(r.config, e)
				}))
			}

			function i(t, e) {
				var i;
				e && m.isTooltip(r.config, e) && ((i = t.target.querySelector(".dhx_grid-header-cell-text span") && t.target.querySelector(".dhx_grid-header-cell-text span").textContent || t.target.querySelector(".dhx_grid-footer-cell-text span") && t.target.querySelector(".dhx_grid-footer-cell-text span").textContent || "") && C.tooltip(i, {
					css: "dhx_grid_tooltip",
					node: t.target,
					htmlEnable: m.isHtmlEnable(r.config, e)
				}))
			}
			return r._htmlEvents = {
				onclick: l.eventHandler(function (t) {
					return l.locate(t)
				}, {
					"dhx_grid-header-cell--sortable": function (t, e) {
						var i, n = t.target.getAttribute("dhx_resized"),
							o = r._getColumn(e);
						o && m.isSortable(r.config, o) && !n && r.events.fire(y.GridEvents.beforeSort, [o, r._sortDir ? "asc" : "desc"]) && (t = (i = null === (t = l.locateNodeByClassName(t, "dhx_grid-header-cell")) || void 0 === t ? void 0 : t.querySelector(".dhx_grid-header-cell-text_content").innerHTML) ? o.header.find(function (t) {
							return t.text === i
						}) : null, o = "asc" === r._sortDir ? "desc" : "asc", r._sort(e, o, null == t ? void 0 : t.sortAs))
					},
					"dhx_grid-expand-cell": function (t, e) {
						t.target.classList.contains("dhx_grid-expand-cell-icon") && r.events.fire(y.GridEvents.expand, [e])
					}
				}),
				onscroll: function (t) {
					r.events.fire(y.GridEvents.scroll, [{
						y: t.target.scrollTop,
						x: t.target.scrollLeft
					}])
				},
				onmouseover: {
					".dhx_grid-cell": function (t) {
						var e = r.data.getItem(t.composedPath()[1].getAttribute("dhx_id")),
							i = r._getColumn(t.target.getAttribute("dhx_col_id"));
						n(e, i, t.target)
					},
					".dhx_grid-cell:not(.dhx_tree-cell) .dhx_grid-cell__content, .dhx_tree-cell :not(.dhx_grid-cell__content)": function (t) {
						var e = t.composedPath(),
							i = r.data.getItem(e[2].getAttribute("dhx_id")),
							e = r._getColumn(e[1].getAttribute("dhx_col_id"));
						n(i, e, t.target)
					},
					".dhx_grid-cell.dhx_tree-cell .dhx_grid-cell__content": function (t) {
						var e = t.composedPath(),
							i = r.data.getItem(e[3].getAttribute("dhx_id")),
							t = r._getColumn(e[2].getAttribute("dhx_col_id"));
						n(i, t, e[2])
					},
					".dhx_span-cell:not(.dhx_grid-header-cell)": function (t) {
						var e, i = r.data.getItem(t.target.getAttribute("dhx_id")),
							n = r._getColumn(t.target.getAttribute("dhx_col_id")),
							o = r.getSpan(i.id, n.id);
						i && o && m.isTooltip(r.config, o) && (e = o.text || g.toFormat(i[n.id], n.type, n.format), o.tooltipTemplate ? e = o.tooltipTemplate(e, o) : n.template && (e = n.template(e, i, n)), e && C.tooltip(e, {
							css: "dhx_grid_tooltip",
							node: t.target,
							htmlEnable: !0
						}))
					},
					".dhx_grid-header-cell:not(.dhx_span-cell)": function (t) {
						var e = r._getColumn(t.target.getAttribute("dhx_id"));
						i(t, e)
					},
					".dhx_grid-footer-cell:not(.dhx_span-cell)": function (t) {
						var e = r._getColumn(t.target.getAttribute("dhx_id"));
						i(t, e)
					},
					".dhx_grid-header-cell.dhx_span-cell": function (t) {
						var e = r._getColumn(t.target.getAttribute("dhx_id")),
							i = e && e.header.find(function (t) {
								return !(!t.rowspan && !t.colspan)
							});
						e && i && m.isTooltip(r.config, e) && ((i = i.text || "") && C.tooltip(i, {
							css: "dhx_grid_tooltip",
							node: t.target,
							htmlEnable: m.isHtmlEnable(r.config, e)
						}))
					},
					".dhx_grid-header-cell-text_content": function (t) {
						var e = t.composedPath(),
							i = r._getColumn(e[1].getAttribute("dhx_id"));
						i && m.isTooltip(r.config, i) && ((t = e[2].querySelector(".dhx_grid-header-cell-text_content") && e[2].querySelector(".dhx_grid-header-cell-text_content").textContent || "") && C.tooltip(t, {
							css: "dhx_grid_tooltip",
							node: e[1],
							htmlEnable: m.isHtmlEnable(r.config, i)
						}))
					}
				}
			}, (r.config.dragMode || r.config.dragItem) && (h.dragManager.setItem(r._uid, r), r.config.dragItem || (r.config.dragItem = "row"), r.config.dragMode || (r.config.dragMode = "both")), r._init(), r.config.columns && r._parseColumns(!0), r.config.data && r.config.data instanceof Array && r.config.data.length && r.config.columns && r.data.parse(r.config.data), r.selection = new v.Selection(r, {
				disabled: !r.config.selection
			}, r.events, r._uid), r.mount(t, r._createView()), r.config.autoWidth && r.config.autoHeight && r._prepareData(r.config.data), a.awaitRedraw().then(function () {
				r.config.keyNavigation && (r.keyManager = new c.KeyManager(function (t, e) {
					return !(e !== r._uid || !r.events.fire(y.GridEvents.beforeKeyDown, [t])) && (r.events.fire(y.GridEvents.afterKeyDown, [t]), !0)
				}), r._initHotKey(), S.focusManager.setFocusId(r._uid))
			}), e.autoEmptyRow && 0 === r.data.getLength() && (r._addEmptyRow(), r.paint()), r
		}
		e.Grid = o
	}, function (t, e, i) {
		"use strict";
		var y = this && this.__assign || function () {
			return (y = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var b = i(0),
			d = i(2),
			a = i(22),
			w = i(16),
			x = i(42),
			E = i(174),
			C = i(81),
			h = i(1),
			k = 2;

		function S(t, e, i) {
			var n = t.config,
				o = n.columns.filter(function (t) {
					return !t.hidden
				}),
				r = a.calculatePositions(i.width, i.height, t._scroll, n, e),
				s = o.slice(r.xStart, r.xEnd),
				i = e.slice(r.yStart, r.yEnd);
			return y(y({}, n), {
				data: e,
				columns: o,
				scroll: t._scroll,
				$positions: r,
				headerHeight: n.$headerLevel * n.headerRowHeight,
				footerHeight: n.$footerLevel * n.footerRowHeight,
				firstColId: o[0] && o[0].id,
				events: t.events,
				_events: t._events,
				currentColumns: s,
				currentRows: i,
				sortBy: t._sortBy,
				sortDir: t._sortDir,
				content: t.content,
				gridId: t._uid
			})
		}

		function I(t) {
			if (t && (t.tagName || (t = t._parent._container), t)) {
				var e = t.currentStyle || window.getComputedStyle(t),
					i = parseFloat(e.paddingLeft) + parseFloat(e.paddingRight) || 0,
					e = parseFloat(e.paddingTop) + parseFloat(e.paddingBottom) || 0;
				return {
					width: t.clientWidth - i,
					height: t.clientHeight - e
				}
			}
		}

		function P(o, t) {
			var e, i = x.getCells(o),
				n = x.getSpans(o),
				r = o.columns;
			o.$resizing && (a = h.findIndex(r, function (t) {
				return t.id === o.$resizing
			}), l = w.getTotalWidth(r.slice(0, a)) + r[a].$width, e = b.el(".dhx_grid-resize-line", {
				style: {
					top: 0,
					left: l,
					height: o.$totalHeight
				}
			}));
			var s, a = "string" == typeof (a = o.selection ? o.selection.toHTML() : null) ? b.el("div.dhx_selection", {
				".innerHTML": a
			}) : a,
				l = o.$positions,
				c = {};
			if (o.eventHandlers)
				for (var u in o.eventHandlers) o.eventHandlers.hasOwnProperty(u) && (s = o.eventHandlers[u], c[u] = d.eventHandler(function (t) {
					return e = t, i = d.locate(e, "dhx_id"), n = d.locate(e, "dhx_col_id"), t = o.currentRows.filter(function (t) {
						return t.id === i
					})[0], e = o.currentColumns.filter(function (t) {
						return t.id === n
					})[0], {
						row: i ? t : {},
						col: n ? e : {}
					};
					var e, i, n
				}, y({}, s)));
			return b.el(".dhx_data-wrap", y({
				style: {
					height: o.$totalHeight,
					width: o.$totalWidth,
					"padding-left": t.x,
					"padding-top": t.y
				},
				role: "presentation"
			}, c), [b.el(".dhx_grid_data", y(y({
				_flags: b.KEYED_LIST
			}, x.getHandlers(l.yStart, l.xStart, o)), {
				role: "rowgroup",
				"aria-rowcount": o.data.length
			}), i), b.el(".dhx_span-spans", {
				role: "presentation"
			}, n), b.el(".dhx_grid_selection", {
				_ref: "selection",
				"aria-hidden": "true"
			}, [a, e])])
		}

		function O(t, e, i) {
			i = i.height - k, i = e ? i : i - t.headerHeight;
			return !t.$footer || e ? i : i - t.footerHeight
		}

		function M(e, t, o, r, i) {
			void 0 === o && (o = !0), void 0 === r && (r = !1), void 0 === i && (i = !1);
			var s, n = !i && e.$totalHeight >= t.height - e.headerRowHeight ? d.getScrollbarWidth() : 0,
				a = t.width - k - n,
				l = e.columns.filter(function (t) {
					return !t.hidden
				}),
				n = l.filter(function (t) {
					return !t.width && !t.$fixed && w.isAutoWidth(e, t)
				}),
				c = w.getTotalWidth(l.filter(function (t) {
					return t.width || t.$fixed || !w.isAutoWidth(e, t)
				})),
				u = n.reduce(function (t, e) {
					return t + (e.gravity || 1)
				}, 0);
			a < e.$totalWidth ? (s = n.reduce(function (t, e) {
				return t + (e.maxWidth || e.$width)
			}, 0), 1 < n.length && n.forEach(function (t) {
				var e = 0,
					i = (e = o ? Math.abs(a - s) * ((t.gravity || 1) / u) : Math.abs(a - c) * ((t.gravity || 1) / u)) < t.minWidth,
					n = e > t.maxWidth;
				i || n ? i ? (c += t.$width - e, t.$fixed = !0) : n && (t.$width = t.maxWidth, t.$fixed = !0) : t.$width = e
			})) : n.forEach(function (t) {
				var e = Math.abs(a - c) * ((t.gravity || 1) / u),
					i = e < t.minWidth,
					n = e > t.maxWidth;
				i || n ? i ? (c += t.$width - e, r && (t.$fixed = !0)) : n && (t.$width = t.maxWidth, r && (t.$fixed = !0)) : t.$width = e, !o && t.$fixed && delete t.$fixed
			}), o && M(e, t, !1, r, i)
		}
		e.getRenderConfig = S, e.render = function (t, e, i, n, o) {
			e._container || (e.config.width = 1, e.config.height = 1), t && t.node && t.node.parent && t.node.parent.el && (m = I(t.node.parent.el), e.config.width = m.width, e.config.height = m.height);
			var r = e.config;
			if (!r) return b.el("div");
			if (!r.columns.length) return b.el(".dhx_grid", {
				dhx_root_id: r.rootParent
			});
			var s = e.data.getRawData(0, -1, null, 2);
			r.columns.reduce(function (t, e) {
				return e.hidden && t
			}, !0) ? r.$totalHeight = 0 : r.$totalHeight = s.reduce(function (t, e) {
				return t + (e.$height || 0)
			}, 0);
			var a = I(e._container),
				l = {
					width: r.width || a && a.width || 0,
					height: r.height || a && a.height || 0
				};
			w.isAutoWidth(r) && (M(r, l), r.$totalWidth = w.getTotalWidth(r.columns.filter(function (t) {
				return !t.hidden
			}))), r.width = l.width, r.height = l.height;
			var c = S(e, s, l);
			c.selection = n, c.datacollection = e.data;
			var u, d, h, f, p = x.getShifts(c),
				_ = w.isCssSupport("position", "sticky"),
				v = O(c, _, l),
				g = {
					wrapper: l,
					sticky: _,
					shifts: p,
					gridBodyHeight: v
				},
				m = C.getFixedRows(c, y(y({}, g), {
					name: "header",
					position: "top"
				})),
				a = c.$footer ? C.getFixedRows(c, y(y({}, g), {
					name: "footer",
					position: "bottom"
				})) : null,
				s = c.$totalWidth + k < l.width ? "dhx_grid-less-width" : "",
				n = c.$totalHeight + k < l.height ? "dhx_grid-less-height" : "";
			return t.node || (t = e.getScrollState(), u = t.x, d = t.y, b.awaitRedraw().then(function () {
				e.scroll(u, d)
			})), b.el(".dhx_grid.dhx_widget", y({
				class: (c.css || "") + (_ ? "" : " dhx_grid_border") + (r.multiselection ? " dhx_no-select--pointer" : ""),
				dhx_widget_id: o,
				dhx_root_id: r.rootParent
			}, (h = c.data, f = r.columns, t = c.editable, o = c.multiselection, {
				role: "grid",
				"aria-rowcount": h.length,
				"aria-colcount": f.filter(function (t) {
					return !t.hidden
				}).length,
				"aria-readonly": t ? "false" : "true",
				"aria-multiselectable": o ? "true" : "false"
			})), [b.resizer(function (t) {
				return w.isAutoWidth(e.config) && t && (r.$totalWidth = 0, M(r, l, !0, !0)), e.paint()
			}), b.el(".dhx_grid-content", {
				style: y({}, l),
				onclick: i.onclick,
				onmouseover: i.onmouseover,
				class: (s + " " + n).trim(),
				role: "presentation"
			}, [_ ? null : m, b.el(".dhx_grid-body", {
				style: {
					height: v,
					width: l.width - k
				},
				onscroll: i.onscroll,
				_ref: "grid_body",
				role: "presentation"
			}, [b.el("div", {}, [_ ? m : null, P(c, p), _ ? a : null])]), E.getFixedColsHeader(c, g), E.getFixedCols(c, g), _ ? null : a])])
		}, e.proRender = function (t, e, i, n, o) {
			e._container || (e.config.width = 1, e.config.height = 1), t && t.node && t.node.parent && t.node.parent.el && (v = I(t.node.parent.el), e.config.width = v.width, e.config.height = v.height);
			var r = e.config;
			if (!r) return b.el("div");
			if (!r.columns.length) return b.el(".dhx_grid", {
				dhx_root_id: r.rootParent
			});
			var s = e.data.getRawData(0, -1, null, 2);
			r.columns.reduce(function (t, e) {
				return e.hidden && t
			}, !0) ? r.$totalHeight = 0 : r.$totalHeight = s.reduce(function (t, e) {
				return t + (e.$height || 0)
			}, 0);
			var a = I(e._container),
				l = {
					width: r.width || a && a.width || 0,
					height: r.height || a && a.height || 0
				};
			w.isAutoWidth(r) && (M(r, l, !0, !1, e.scrollView && e.scrollView.config.enable), r.$totalWidth = w.getTotalWidth(r.columns.filter(function (t) {
				return !t.hidden
			}))), r.width = l.width, r.height = l.height;
			var c = S(e, s, l);
			c.selection = n, c.datacollection = e.data;
			var u, d, h = x.getShifts(c),
				f = w.isCssSupport("position", "sticky"),
				p = O(c, f, l),
				_ = {
					wrapper: l,
					sticky: f,
					shifts: h,
					gridBodyHeight: p
				},
				v = C.getFixedRows(c, y(y({}, _), {
					name: "header",
					position: "top"
				})),
				a = c.$footer ? C.getFixedRows(c, y(y({}, _), {
					name: "footer",
					position: "bottom"
				})) : null,
				s = c.$totalWidth + k < l.width ? "dhx_grid-less-width" : "",
				n = c.$totalHeight + k < l.height ? "dhx_grid-less-height" : "";
			return t.node || (t = e.getScrollState(), u = t.x, d = t.y, b.awaitRedraw().then(function () {
				e.scroll(u, d)
			})), h = b.el("div", {}, [f ? v : null, P(c, h), f ? a : null]), b.el(".dhx_grid.dhx_widget", {
				class: (c.css || "") + (f ? "" : " dhx_grid_border") + (r.multiselection ? " dhx_no-select--pointer" : ""),
				dhx_widget_id: o,
				dhx_root_id: r.rootParent,
				role: "grid",
				"aria-rowcount": c.data.length,
				"aria-colcount": r.columns.filter(function (t) {
					return !t.hidden
				}).length
			}, [b.resizer(function (t) {
				return w.isAutoWidth(e.config) && t && (r.$totalWidth = 0, M(r, l, !0, !0)), e.paint()
			}), b.el(".dhx_grid-content", {
				style: y({}, l),
				onclick: i.onclick,
				onmouseover: i.onmouseover,
				class: (s + " " + n).trim(),
				role: "presentation"
			}, [f ? null : v, b.el(".dhx_grid-body", {
				style: {
					height: p,
					width: l.width - k
				},
				onscroll: i.onscroll,
				_ref: "grid_body",
				role: "presentation"
			}, [e.scrollView && e.scrollView.config.enable ? e.scrollView.render([h]) : h]), E.getFixedColsHeader(c, _), E.getFixedCols(c, _), f ? null : a])])
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(44),
			a = i(70),
			l = i(0),
			o = (r = a.Cell, o(c, r), c.prototype.destructor = function () {
				for (var t in this._all) {
					var e;
					Object.prototype.hasOwnProperty.call(this._all, t) && ((e = this._all[t]).getWidget() && "function" == typeof e.getWidget().destructor && e.getWidget().destructor(), e.destructor())
				}
				this.config = this._cells = this._root = this._xLayout = this._isViewLayout = null, this._all = {}, this.unmount()
			}, c.prototype.toVDOM = function () {
				if (this._isViewLayout) {
					var t = [this.getCell(this.config.activeView).toVDOM()];
					return r.prototype.toVDOM.call(this, t)
				}
				var e = [];
				return this._inheritTypes(), this._cells.forEach(function (t) {
					t = t.toVDOM();
					Array.isArray(t) ? e = e.concat(t) : e.push(t)
				}), r.prototype.toVDOM.call(this, e)
			}, c.prototype.removeCell = function (e) {
				if (this.events.fire(s.LayoutEvents.beforeRemove, [e])) {
					var t = this.config.parent || this;
					if (t !== this) return t.removeCell(e);
					t = this.getCell(e);
					t && (t = t.getParent(), delete this._all[e], t._cells = t._cells.filter(function (t) {
						return t.id != e
					}), t.paint()), this.events.fire(s.LayoutEvents.afterRemove, [e])
				}
			}, c.prototype.addCell = function (t, e) {
				var i;
				void 0 === e && (e = -1), this.events.fire(s.LayoutEvents.beforeAdd, [t.id]) && (i = this._createCell(t), e < 0 && (e = this._cells.length + e + 1), this._cells.splice(e, 0, i), this.paint(), this.events.fire(s.LayoutEvents.afterAdd, [t.id]))
			}, c.prototype.getId = function (t) {
				return t < 0 && (t = this._cells.length + t), this._cells[t] ? this._cells[t].id : void 0
			}, c.prototype.getRefs = function (t) {
				return this._root.getRootView().refs[t]
			}, c.prototype.getCell = function (t) {
				return this._root._all[t]
			}, c.prototype.forEach = function (t, e, i) {
				if (void 0 === i && (i = 1 / 0), this._haveCells(e) && !(i < 1))
					for (var n = (e ? this._root._all[e] : this._root)._cells, o = 0; o < n.length; o++) {
						var r = n[o];
						t.call(this, r, o, n), this._haveCells(r.id) && r.forEach(t, r.id, --i)
					}
			}, c.prototype.cell = function (t) {
				return this.getCell(t)
			}, c.prototype._getCss = function (t) {
				var e = this._xLayout ? "dhx_layout-columns" : "dhx_layout-rows",
					i = this.config.align ? " " + e + "--" + this.config.align : "";
				if (t) return e + " dhx_layout-cell" + (this.config.align ? " dhx_layout-cell--" + this.config.align : "");
				var n = this.config.parent ? r.prototype._getCss.call(this) : "dhx_widget dhx_layout",
					t = this.config.parent ? "" : " dhx_layout-cell";
				return n + (this.config.full ? t : " " + e) + i
			}, c.prototype._parseConfig = function () {
				var e = this,
					t = this.config,
					i = t.rows || t.cols || t.views || [];
				this._xLayout = !t.rows, this._cells = i.map(function (t) {
					return e._createCell(t)
				})
			}, c.prototype._createCell = function (t) {
				var e = t.rows || t.cols || t.views ? (t.parent = this._root, new c(this, t)) : new a.Cell(this, t);
				return this._root._all[e.id] = e, t.init && t.init(e, t), e
			}, c.prototype._haveCells = function (t) {
				if (t) {
					t = this._root._all[t];
					return t._cells && 0 < t._cells.length
				}
				return 0 < Object.keys(this._all).length
			}, c.prototype._inheritTypes = function (t) {
				var e, i = this;
				void 0 === t && (t = this._cells), Array.isArray(t) ? t.forEach(function (t) {
					return i._inheritTypes(t)
				}) : ((e = t.config).rows || e.cols) && (t = t.getParent(), !e.type && t && (t.config.type ? e.type = t.config.type : this._inheritTypes(t)))
			}, c);

		function c(t, e) {
			var i = r.call(this, t, e) || this;
			return i._root = i.config.parent || i, i._all = {}, i._parseConfig(), i.config.activeTab && (i.config.activeView = i.config.activeTab), i.config.views && (i.config.activeView = i.config.activeView || i._cells[0].id, i._isViewLayout = !0), e.parent || (e = l.create({
				render: function () {
					return i.toVDOM()
				}
			}, i), i.mount(t, e)), i
		}
		e.Layout = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			l = this && this.__assign || function () {
				return (l = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, c = i(1),
			u = i(0),
			s = i(4),
			a = i(44),
			d = i(158),
			h = i(3),
			f = i(27),
			o = (r = s.View, o(p, r), p.prototype.paint = function () {
				var t;
				this.isVisible() && ((t = this.getRootView()) ? t.redraw() : this._parent.paint())
			}, p.prototype.isVisible = function () {
				if (!this._parent) return !(!this._container || !this._container.tagName) || Boolean(this.getRootNode());
				var t = this._parent.config.activeView;
				return (!t || t === this.id) && (!this.config.hidden && (!this._parent || this._parent.isVisible()))
			}, p.prototype.hide = function () {
				this.events.fire(a.LayoutEvents.beforeHide, [this.id]) && (this.config.hidden = !0, this._parent && this._parent.paint && this._parent.paint(), this.events.fire(a.LayoutEvents.afterHide, [this.id]))
			}, p.prototype.show = function () {
				var t = this;
				this.events.fire(a.LayoutEvents.beforeShow, [this.id]) && (this._parent && this._parent.config && void 0 !== this._parent.config.activeView ? this._parent.config.activeView = this.id : this.config.hidden = !1, this._parent && !this._parent.isVisible() && this._parent.show(), this.paint(), this._ui instanceof f.Grid && this._ui.config.keyNavigation && u.awaitRedraw().then(function () {
					t._ui.setColumns(t._ui.config.columns)
				}), this.events.fire(a.LayoutEvents.afterShow, [this.id]))
			}, p.prototype.expand = function () {
				this.events.fire(a.LayoutEvents.beforeExpand, [this.id]) && (this.config.collapsed = !1, this.events.fire(a.LayoutEvents.afterExpand, [this.id]), this.paint())
			}, p.prototype.collapse = function () {
				this.events.fire(a.LayoutEvents.beforeCollapse, [this.id]) && (this.config.collapsed = !0, this.events.fire(a.LayoutEvents.afterCollapse, [this.id]), this.paint())
			}, p.prototype.toggle = function () {
				this.config.collapsed ? this.expand() : this.collapse()
			}, p.prototype.getParent = function () {
				return this._parent
			}, p.prototype.destructor = function () {
				this.events && this.events.clear(), this.config = this.events = this.id = null, this._parent = this._handlers = this._uid = this._disabled = this._resizerHandlers = null, this.unmount()
			}, p.prototype.getWidget = function () {
				return this._ui
			}, p.prototype.getCellView = function () {
				return this._parent && this._parent.getRefs(this._uid)
			}, p.prototype.attach = function (t, e) {
				var i = this;
				return this.config.html = null, "object" == typeof t ? this._ui = t : "string" == typeof t ? this._ui = new window.dhx[t](null, e) : "function" == typeof t && (t.prototype instanceof s.View ? this._ui = new t(null, e) : this._ui = {
					getRootView: function () {
						return t(e)
					}
				}), this.paint(), this._ui instanceof f.Grid && this._ui.config.keyNavigation && u.awaitRedraw().then(function () {
					i._ui.setColumns(i._ui.config.columns)
				}), this._ui
			}, p.prototype.attachHTML = function (t) {
				this.config.html = t, this.paint()
			}, p.prototype.toVDOM = function (t) {
				if (null === this.config && (this.config = {}), !this.config.hidden) {
					var e, i = this._calculateStyle(),
						n = c.isDefined(this.config.padding) ? isNaN(Number(this.config.padding)) ? {
							padding: this.config.padding
						} : {
							padding: this.config.padding + "px"
						} : "",
						i = this.config.full || this.config.html ? i : l(l({}, i), n);
					this.config.html || (e = this._ui ? ((o = this._ui.getRootView()).render && (o = u.inject(o)), [o]) : t || null);
					var o = !this.config.resizable || this._isLastCell() || this.config.collapsed ? null : u.el(".dhx_layout-resizer." + (this._isXDirection() ? "dhx_layout-resizer--x" : "dhx_layout-resizer--y"), l(l({}, this._resizerHandlers), {
						_ref: "resizer_" + this._uid,
						tabindex: 0
					}), [u.el("span.dhx_layout-resizer__icon", {
						class: "dxi " + (this._isXDirection() ? "dxi-dots-vertical" : "dxi-dots-horizontal")
					})]),
						r = {};
					if (this.config.on)
						for (var s in this.config.on) r["on" + s] = this.config.on[s];
					var a = "",
						t = this.config.cols || this.config.rows;
					if (this.config.type && t) switch (this.config.type) {
						case "line":
							a = " dhx_layout-line";
							break;
						case "wide":
							a = " dhx_layout-wide";
							break;
						case "space":
							a = " dhx_layout-space"
					}
					n = u.el("div", l(l(((t = {
						_key: this._uid,
						_ref: this._uid
					})["aria-label"] = this.config.id ? "tab-content-" + this.config.id : null, t), r), {
						class: this._getCss(!1) + (this.config.css ? " " + this.config.css : "") + (this.config.collapsed ? " dhx_layout-cell--collapsed" : "") + (this.config.resizable ? " dhx_layout-cell--resizable" : "") + (this.config.type && !this.config.full ? a : ""),
						style: i
					}), this.config.full ? [u.el("div", {
						tabindex: this.config.collapsable ? "0" : "-1",
						role: this.config.collapsable ? "button" : null,
						"aria-label": this.config.collapsable ? "click to " + (this.config.collapsed ? "expand" : "collapse") : null,
						class: "dhx_layout-cell-header" + (this._isXDirection() ? " dhx_layout-cell-header--col" : " dhx_layout-cell-header--row") + (this.config.collapsable ? " dhx_layout-cell-header--collapseble" : "") + (this.config.collapsed ? " dhx_layout-cell-header--collapsed" : "") + (((this.getParent() || {}).config || {}).isAccordion ? " dhx_layout-cell-header--accordion" : ""),
						style: {
							height: this.config.headerHeight
						},
						onclick: this._handlers.toggle,
						onkeydown: this._handlers.enterCollapse
					}, [this.config.headerIcon && u.el("span.dhx_layout-cell-header__icon", {
						class: this.config.headerIcon
					}), this.config.headerImage && u.el(".dhx_layout-cell-header__image-wrapper", [u.el("img", {
						src: this.config.headerImage,
						class: "dhx_layout-cell-header__image"
					})]), this.config.header && u.el("h3.dhx_layout-cell-header__title", this.config.header), this.config.collapsable ? u.el("div.dhx_layout-cell-header__collapse-icon", {
						class: this._getCollapseIcon()
					}) : u.el("div.dhx_layout-cell-header__collapse-icon", {
						class: "dxi dxi-empty"
					})]), this.config.collapsed ? null : u.el("div", {
						style: l(l({}, n), {
							height: "calc(100% - " + (this.config.headerHeight || 37) + "px)"
						}),
						".innerHTML": this.config.html,
						class: this._getCss(!0) + " dhx_layout-cell-content" + (this.config.type ? a : "")
					}, e)] : !this.config.html || this.config.rows && this.config.cols && this.config.views ? e : [this.config.collapsed ? null : u.el(".dhx_layout-cell-content", {
						".innerHTML": this.config.html,
						style: n
					})]);
					return o ? [n, o] : n
				}
			}, p.prototype._getCss = function (t) {
				return "dhx_layout-cell"
			}, p.prototype._initHandlers = function () {
				function e(t) {
					var e;
					r.isActive && (e = (t.targetTouches ? t.targetTouches[0] : t).clientX, t = (t.targetTouches ? t.targetTouches[0] : t).clientY, e = r.xLayout ? e - r.range.min + window.pageXOffset : t - r.range.min + window.pageYOffset, t = r.xLayout ? "width" : "height", e < 0 ? e = r.resizerLength / 2 : e > r.size && (e = r.size - r.resizerLength), o.config[t] = e - r.resizerLength / 2 + "px", r.nextCell.config[t] = r.size - e - r.resizerLength / 2 + "px", o.paint(), o.events.fire(a.LayoutEvents.resize, [o.id]))
				}

				function i(t) {
					var e, i, n;
					t.targetTouches && t.preventDefault(), 3 !== t.which && (r.isActive && s(t), o.events.fire(a.LayoutEvents.beforeResizeStart, [o.id]) && (document.body.classList.add("dhx_no-select--resize"), i = o.getCellView(), n = (e = o._getNextCell()).getCellView(), t = o._getResizerView(), i = i.el.getBoundingClientRect(), t = t.el.getBoundingClientRect(), n = n.el.getBoundingClientRect(), r.xLayout = o._isXDirection(), r.left = i.left + window.pageXOffset, r.top = i.top + window.pageYOffset, r.margin = d.getMarginSize(o.getParent().config, r.xLayout), r.range = d.getBlockRange(i, n, r.xLayout), r.size = r.range.max - r.range.min - r.margin, r.isActive = !0, r.nextCell = e, r.resizerLength = r.xLayout ? t.width : t.height))
				}
				var o = this,
					r = {
						left: null,
						top: null,
						isActive: !(this._handlers = {
							enterCollapse: function (t) {
								13 === t.keyCode && o._handlers.toggle()
							},
							collapse: function () {
								o.config.collapsable && o.collapse()
							},
							expand: function () {
								o.config.collapsable && o.expand()
							},
							toggle: function () {
								o.config.collapsable && o.toggle()
							}
						}),
						range: null,
						xLayout: null,
						nextCell: null,
						size: null,
						resizerLength: null,
						margin: null
					},
					s = function (t) {
						r.isActive = !1, document.body.classList.remove("dhx_no-select--resize"), t.targetTouches ? (document.removeEventListener("touchend", s), document.removeEventListener("touchmove", e)) : (document.removeEventListener("mouseup", s), document.removeEventListener("mousemove", e)), o.events.fire(a.LayoutEvents.afterResizeEnd, [o.id])
					};
				this._resizerHandlers = {
					onmousedown: function (t) {
						i(t), document.addEventListener("mouseup", s), document.addEventListener("mousemove", e)
					},
					ontouchstart: function (t) {
						i(t), document.addEventListener("touchend", s), document.addEventListener("touchmove", e)
					},
					ondragstart: function (t) {
						return t.preventDefault()
					}
				}
			}, p.prototype._getCollapseIcon = function () {
				return this._isXDirection() && this.config.collapsed ? "dxi dxi-chevron-right" : this._isXDirection() && !this.config.collapsed ? "dxi dxi-chevron-left" : !this._isXDirection() && this.config.collapsed ? "dxi dxi-chevron-up" : this._isXDirection() || this.config.collapsed ? void 0 : "dxi dxi-chevron-down"
			}, p.prototype._isLastCell = function () {
				var t = this._parent;
				return t && t._cells.indexOf(this) === t._cells.length - 1
			}, p.prototype._getNextCell = function () {
				var t = this._parent,
					e = t._cells.indexOf(this);
				return t._cells[e + 1].config.hidden ? t._cells[e + 1]._getNextCell() : t._cells[e + 1]
			}, p.prototype._getResizerView = function () {
				return this._parent.getRefs("resizer_" + this._uid)
			}, p.prototype._isXDirection = function () {
				return this._parent && this._parent._xLayout
			}, p.prototype._calculateStyle = function () {
				var t = this.config;
				if (t) {
					var e = {},
						i = !1,
						n = !1;
					isNaN(Number(t.width)) || (t.width = t.width + "px"), isNaN(Number(t.height)) || (t.height = t.height + "px"), isNaN(Number(t.minWidth)) || (t.minWidth = t.minWidth + "px"), isNaN(Number(t.minHeight)) || (t.minHeight = t.minHeight + "px"), isNaN(Number(t.maxWidth)) || (t.maxWidth = t.maxWidth + "px"), isNaN(Number(t.maxHeight)) || (t.maxHeight = t.maxHeight + "px"), "content" === t.width && (i = !0), "content" === t.height && (n = !0);
					var o = t.width,
						r = t.height,
						s = t.cols,
						a = t.rows,
						l = t.minWidth,
						c = t.minHeight,
						u = t.maxWidth,
						d = t.maxHeight,
						h = t.gravity,
						f = t.collapsed,
						p = t.$fixed,
						_ = -1 === Math.sign(h) ? 0 : h;
					"boolean" == typeof h && (_ = h ? 1 : 0);
					var v = "boolean" == typeof h ? !h : -1 === Math.sign(h);
					this._isXDirection() ? (p || o || void 0 === h && (l || u)) && (v = !0) : (p || r || void 0 === h && (c || d)) && (v = !0);
					var g, v = v ? 0 : _ || 1,
						_ = this._isXDirection() ? "x" : "y";
					return void 0 !== l && (e.minWidth = l), void 0 !== c && (e.minHeight = c), void 0 !== u && (e.maxWidth = u), void 0 !== d && (e.maxHeight = d), void 0 === this._parent && (_ = !0), void 0 !== o && "content" !== o ? e.width = o : !0 === _ ? e.width = "100%" : "x" === _ && (i ? e.flex = "0 0 auto" : (g = this._isXDirection() ? "1px" : "auto", e.flex = v + " " + (s || a ? "0 " + g : "1 auto"))), void 0 !== r && "content" !== r ? e.height = r : !0 === _ ? e.height = "100%" : "y" === _ && (n ? e.flex = "0 0 auto" : (g = this._isXDirection() ? "auto" : "1px", e.flex = v + " " + (s || a ? "0 " + g : "1 auto"))), !0 === _ && void 0 === t.width && void 0 === t.height && (e.flex = v + " 1 auto"), f && (this._isXDirection() ? e.width = "auto" : e.height = "auto", e.flex = "0 0 auto"), e
				}
			}, p);

		function p(t, e) {
			e = r.call(this, t, e) || this;
			return e._disabled = [], t && t.isVisible && (e._parent = t), e._parent && e._parent.events ? e.events = e._parent.events : e.events = new h.EventSystem(e), e.config.full = void 0 === e.config.full ? Boolean(e.config.header || e.config.collapsable || e.config.headerHeight || e.config.headerIcon || e.config.headerImage) : e.config.full, e._initHandlers(), e.id = e.config.id || c.uid(), e
		}
		e.Cell = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.PopupEvents || (e.PopupEvents = {})).beforeHide = "beforeHide", e.beforeShow = "beforeShow", e.afterHide = "afterHide", e.afterShow = "afterShow", e.click = "click"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.SliderEvents || (e.SliderEvents = {})).change = "change", e.focus = "focus", e.blur = "blur", e.keydown = "keydown", e.mousedown = "mousedown", e.mouseup = "mouseup"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		e.default = {
			hours: "Hours",
			minutes: "Minutes",
			save: "Save"
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.TimepickerEvents || (e.TimepickerEvents = {})).change = "change", e.beforeApply = "beforeApply", e.afterApply = "afterApply", e.beforeClose = "beforeClose", e.afterClose = "afterClose", e.apply = "apply", e.close = "close", e.save = "save"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.CalendarEvents || (e.CalendarEvents = {})).change = "change", e.beforeChange = "beforechange", e.modeChange = "modeChange", e.monthSelected = "monthSelected", e.yearSelected = "yearSelected", e.cancelClick = "cancelClick", e.dateMouseOver = "dateMouseOver", e.dateHover = "dateHover"
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			d = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(1),
			h = i(0),
			s = i(3),
			f = i(2),
			a = i(167),
			l = i(4),
			c = i(6),
			u = i(11),
			p = i(28),
			_ = i(12),
			v = i(171),
			g = i(47),
			m = i(29),
			y = i(79),
			b = i(80);

		function w(t) {
			return t.icon ? '<span class="' + t.icon + ' dhx_combobox-options__icon"></span> <span class="dhx_combobox-options__value">' + t.value + "</span>" : t.src ? '<img src="' + t.src + '" class="dhx_combobox-options__image" alt=' + t.value + '></img> <span class="dhx_combobox-options__value">' + t.value + "</span>" : '<span class="dhx_combobox-options__value">' + t.value + "</span>"
		}
		var x, o = (x = l.View, o(E, x), E.prototype.focus = function () {
			if (this.config.disabled) return !1;
			this.getRootView().refs.input.el.focus()
		}, E.prototype.blur = function () {
			if (this.config.disabled) return !1;
			this.getRootView().refs.input.el.blur(), this.popup.hide()
		}, E.prototype.enable = function () {
			this.config.disabled = !1, this.paint()
		}, E.prototype.disable = function () {
			this.config.disabled = !0, this.paint()
		}, E.prototype.isDisabled = function () {
			return this.config.disabled
		}, E.prototype.clear = function () {
			if (this.config.disabled) return !1;
			this.list.selection.remove(), this._state.value = "", this._filter(), this.paint()
		}, E.prototype.getValue = function (t) {
			var e = this.list.selection.getId();
			return t ? r.wrapBox(e) : Array.isArray(e) ? e.join(",") : e
		}, E.prototype.setValue = function (t) {
			return this._setValue(t)
		}, E.prototype.destructor = function () {
			this.popup && this.popup.destructor(), this.events && this.events.clear(), this.list && this.list.destructor(), this._helper && this._helper.destructor(), this._layout && this._layout.destructor(), this.config = this.events = this.list = this.popup = null, this._helper = this._keyListener = this._handlers = this._state = this._uid = this._isPopupConfiqureted = null, this.unmount()
		}, E.prototype.setState = function (t) {
			switch (t) {
				case "success":
					this._state.currentState = b.ComboState.success;
					break;
				case "error":
					this._state.currentState = b.ComboState.error;
					break;
				default:
					this._state.currentState = b.ComboState.default
			}
			this.paint()
		}, E.prototype._setValue = function (t, e) {
			var i = this;
			if (void 0 === e && (e = !1), this.config.disabled || !this._exsistId(t)) return !1;
			this._filter(), this.config.multiselection || (this.list.selection.remove(), this._state.value = ""), this.config.multiselection ? ("string" == typeof t && (t = t.split(",")), "number" == typeof t && (t = [t]), t.forEach(function (t) {
				i.list.selection.add(t, !1, !1, e)
			})) : (t = r.unwrapBox(t), this.list.selection.add(t, !1, !1, e), (t = this.data.getItem(t)) && (this._state.value = this._getItemText(t))), this.paint()
		}, E.prototype._createLayout = function () {
			var t = this.list = new p.List(null, {
				template: this.config.template,
				virtual: this.config.virtual,
				keyNavigation: !1,
				multiselection: this.config.multiselection,
				itemHeight: this.config.itemHeight,
				height: this.config.listHeight,
				data: this.data
			}),
				e = this._layout = new u.Layout(this.popup.getContainer(), {
					css: "dhx_combobox-options dhx_combobox__options",
					rows: [{
						id: "select-unselect-all",
						hidden: !this.config.multiselection || !this.config.selectAllButton
					}, {
						id: "list",
						height: "content"
					}, {
						id: "not-found",
						hidden: !0
					}],
					on: {
						click: {
							".dhx_combobox__action-select-all": this._handlers.selectAll
						}
					}
				});
			e.getCell("list").attach(t), this.config.multiselection && this.config.selectAllButton && e.getCell("select-unselect-all").attach(y.selectAllView)
		}, E.prototype._initHandlers = function () {
			var i = this;
			this.config.helpMessage && (this._helper = new _.Popup({
				css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
			}), this._helper.attachHTML(this.config.helpMessage)), this._handlers = {
				showHelper: function (t) {
					t.preventDefault(), t.stopPropagation(), i._helper.show(t.target, {
						mode: "left" === i.config.labelPosition ? "bottom" : "right"
					})
				},
				selectAll: function () {
					i._state.unselectActive ? (i.list.selection.remove(), i.config.selectAllButton && (i._layout.getCell("select-unselect-all").attach(y.selectAllView), i._state.unselectActive = !1)) : (i.data.filter(), i.list.selection.add(), i.config.selectAllButton && (i._layout.getCell("select-unselect-all").attach(y.unselectAllView), i._state.unselectActive = !0)), i._changePopupPosition(), i.paint()
				},
				onkeydown: function (t) {
					"Tab" === t.key && i.popup.isVisible() ? i._hideOptions() : (i.popup.isVisible() || t.which !== a.KEY_CODES.DOWN_ARROW || i._showOptions(), i.popup.isVisible() && t.which === a.KEY_CODES.RIGHT_ARROW && i.config.readOnly && !i.config.multiselection && (i.list.moveFocus(p.MOVE_DOWN), t.preventDefault()), i.popup.isVisible() && t.which === a.KEY_CODES.LEFT_ARROW && i.config.readOnly && !i.config.multiselection && (i.list.moveFocus(p.MOVE_UP), t.preventDefault()), i.popup.isVisible() && t.which === a.KEY_CODES.DOWN_ARROW && (i.list.moveFocus(p.MOVE_DOWN), t.preventDefault()), i.popup.isVisible() && t.which === a.KEY_CODES.UP_ARROW && (i.list.moveFocus(p.MOVE_UP), t.preventDefault()), i.popup.isVisible() && t.which === a.KEY_CODES.ESC && i._hideOptions(), i.popup.isVisible() && t.which === a.KEY_CODES.ENTER && (i.setValue(i.list.getFocus()), i.config.multiselection || i._hideOptions())), i.events.fire(b.ComboboxEvents.keydown, [t, i.popup.isVisible() && i.list.getFocus()])
				},
				onkeyup: function (t) {
					i.config.multiselection && !i.config.itemsCount && (i._state.ignoreNext ? i._state.ignoreNext = !1 : t.which === a.KEY_CODES.BACKSPACE && !i._state.value && i.config.multiselection && i.list.selection.getId().length && (t = (t = i.list.selection.getId())[t.length - 1], i.list.selection.remove(t), i._changePopupPosition(), i.paint()))
				},
				oninput: function (t) {
					i.config.disabled || (t = t.target.value, i.events.fire(b.ComboboxEvents.input, [t]), i._state.value = t, i._filter(), t.length ? i._state.canDelete = !1 : (i._state.ignoreNext = !0, i._state.canDelete = !0), i.config.multiselection || (i.list.selection.remove(), i.paint()), i.popup.isVisible() || i._showOptions(), i._updatePopup())
				},
				oninputclick: function (t) {
					if (!i.config.disabled) {
						if (i.focus(), t.target.classList.contains("dhx_combobox__action-remove")) {
							var e = f.locate(t);
							return e ? (i.list.selection.remove(e), i._changePopupPosition(), void i.paint()) : void 0
						}
						if (t.target.classList.contains("dhx_combobox__action-clear-all")) return i.list.selection.getId().forEach(function (t) {
							return i.list.selection.remove(t)
						}), i.config.selectAllButton && i._state.unselectActive && (i._layout.getCell("select-unselect-all").attach(y.selectAllView), i._state.unselectActive = !1), void i.paint();
						t.preventDefault(), i.popup.isVisible() ? i.focus() : i._showOptions()
					}
				},
				toggleIcon: function () {
					i.focus(), i.popup.isVisible() ? i._hideOptions() : i._showOptions()
				},
				onfocus: function () {
					var t;
					return null === (t = i.events) || void 0 === t ? void 0 : t.fire(b.ComboboxEvents.focus, [])
				},
				onblur: function () {
					var t;
					return null === (t = i.events) || void 0 === t ? void 0 : t.fire(b.ComboboxEvents.blur, [])
				}
			}
		}, E.prototype._initEvents = function () {
			var i = this;
			this.data.events.on(c.DataEvents.load, function () {
				i.config.value && i._setValue(i.config.value, !0)
			}), this.list.events.on(p.ListEvents.click, function () {
				i.config.multiselection || i._hideOptions(), i._changePopupPosition()
			}), this.list.selection.events.on(m.SelectionEvents.afterSelect, function () {
				var t = i.getValue(i.config.multiselection);
				i.events.fire(b.ComboboxEvents.change, [t]), i._updateSelectedItem(t)
			}), this.list.selection.events.on(m.SelectionEvents.afterUnSelect, function () {
				var t, e = i.config.multiselection;
				i.config.readOnly && !e || (t = i.getValue(e), i.events.fire(b.ComboboxEvents.change, [t]), e && i._updateSelectedItem(t))
			}), this.popup.events.on(_.PopupEvents.beforeShow, function () {
				if (!i.popup.isVisible() && !i._isPopupConfiqureted) return i._configurePopup(), !1
			}), this.config.readOnly && this.popup.events.on(_.PopupEvents.afterShow, function () {
				var t;
				i._state.value ? (t = i.list.selection.getId(), i.list.setFocus(t)) : i.list.setFocus(i.data.getId(0)), i._keyListener.startNewListen(function (t) {
					return i._findBest(t)
				})
			})
		}, E.prototype._showOptions = function () {
			this.events.fire(b.ComboboxEvents.beforeOpen) && (this._state.value.length && (this._state.canDelete = !0), this._filter(), this._configurePopup() && (this.events.fire(b.ComboboxEvents.open), this.events.fire(b.ComboboxEvents.afterOpen)))
		}, E.prototype._configurePopup = function () {
			this._isPopupConfiqureted = !0;
			var t = this.getRootView();
			return !!(t && t.refs && t.refs.holder) && (this.popup.isVisible() || this._updatePopup(), !0)
		}, E.prototype._hideOptions = function () {
			var t = this;
			this.events.fire(b.ComboboxEvents.beforeClose) && (this.config.readOnly && this._keyListener.endListen(), this.list.setFocus(this.data.getId(0)), this.config.multiselection || this.config.readOnly || this.list.selection.contains() || (this._state.value = ""), h.awaitRedraw().then(function () {
				return t.popup.isVisible() && t.popup.hide()
			}), this.events.fire(b.ComboboxEvents.afterClose), this.events.fire(b.ComboboxEvents.close), this._filter(), this.paint())
		}, E.prototype._filter = function () {
			var t, e = this;
			this.config.readOnly || (this.data.filter(function (t) {
				return e.config.filter ? e.config.filter(t, e._state.value) : r.isEqualString(e._state.value, e._getItemText(t))
			}), this.config.multiselection ? this.list.setFocus(this.data.getId(0)) : (t = this.data.getIndex(this.list.selection.getId()), this.list.setFocus(this.data.getId(-1 < t ? t : 0))), 0 === this.data.getLength() ? (this.config.multiselection && this.config.selectAllButton && this._layout.getCell("select-unselect-all").hide(), this._layout.getCell("list").hide(), this._layout.getCell("not-found").attach(y.emptyListView), this._layout.getCell("not-found").show()) : (this.config.multiselection && this.config.selectAllButton && this._layout.getCell("select-unselect-all").show(), this._layout.getCell("not-found").isVisible() && (this._layout.getCell("list").show(), this._layout.getCell("not-found").hide())))
		}, E.prototype._findBest = function (e) {
			var i = this,
				t = this.data.find(function (t) {
					return r.isEqualString(e, i._getItemText(t))
				});
			t && this.list.selection.getId() !== t.id && (this.list.setFocus(t.id), this.paint())
		}, E.prototype._exsistId = function (t) {
			var e = this;
			return t instanceof Array ? t.every(function (t) {
				return e.data.exists(t)
			}) : this.data.exists(t)
		}, E.prototype._draw = function () {
			if (!this.config) return h.el("div");
			var t = this.config,
				e = t.multiselection,
				i = t.labelPosition,
				n = t.hiddenLabel,
				o = t.required,
				r = t.disabled,
				s = t.css,
				a = t.helpMessage,
				l = t.readOnly,
				c = t.placeholder,
				u = e ? null : this.data.getItem(this.list.selection.getId()),
				t = !this.list.selection.getId() || "object" == typeof this.list.selection.getId() && 0 === this.list.selection.getId().length,
				e = f.getLabelStyle(this.config);
			return h.el("div", {
				dhx_widget_id: this._uid,
				onkeydown: this._handlers.onkeydown,
				onkeyup: this._handlers.onkeyup,
				class: "dhx_widget dhx_combobox" + ("left" === i ? " dhx_combobox--label-inline" : "") + (n ? " dhx_combobox--sr_only" : "") + (o ? " dhx_combobox--required" : "") + (r ? " dhx_combobox--disabled" : "") + (s ? " " + s : "")
			}, [e ? h.el("label.dhx_label.dhx_combobox__label", {
				style: e.style,
				class: a ? "dhx_label--with-help" : "",
				onclick: this._handlers.oninputclick
			}, a ? [(e.label || o) && h.el("span.dhx_label__holder", e.label), h.el("span.dhx_label-help.dxi.dxi-help-circle-outline", {
				tabindex: "0",
				role: "button",
				onclick: this._handlers.showHelper,
				id: "dhx_label__help_" + this._uid
			})] : e.label) : null, h.el("div.dhx_combobox-input-box" + (r ? ".dhx_combobox-input-box--disabled" : "") + (l ? ".dhx_combobox-input-box--readonly" : "") + (this._state.currentState === b.ComboState.error ? ".dhx_combobox-input-box--state_error" : "") + (this._state.currentState === b.ComboState.success ? ".dhx_combobox-input-box--state_success" : ""), {
				_ref: "holder"
			}, [h.el("div.dhx_combobox-input__icon", {
				onclick: this._handlers.toggleIcon
			}, [h.el("span" + (this.popup.isVisible() ? ".dxi.dxi-menu-up" : ".dxi.dxi-menu-down"))]), h.el("div.dhx_combobox-input-list-wrapper", {
				onclick: this._handlers.oninputclick
			}, [h.el("ul.dhx_combobox-input-list", d(this._drawSelectedItems(), [h.el("li.dhx_combobox-input-list__item.dhx_combobox-input-list__item--input", [h.el("input.dhx_combobox-input", {
				oninput: this._handlers.oninput,
				onfocus: this._handlers.onfocus,
				onblur: this._handlers.onblur,
				_ref: "input",
				_key: this._uid,
				type: "text",
				placeHolder: t && c ? c : void 0,
				value: l && u ? this._getItemText(u) : this._state.value,
				readOnly: l || r,
				required: o,
				"aria-label": l ? "Select value" : "Type or select value",
				"aria-describedby": a ? "dhx_label__help_" + this._uid : null,
				"aria-expanded": !0
			})])]))])])])
		}, E.prototype._drawSelectedItems = function () {
			var t, i = this;
			if (!this.config.multiselection) return [];
			if (this.config.itemsCount) {
				var e = this.list.selection.getId().length;
				return e ? [h.el("li.dhx_combobox-input-list__item.dhx_combobox-tag", [h.el("span.dhx_combobox-tag__value", (t = e, "function" == typeof (e = this.config.itemsCount) ? e(t) : t + " " + g.default.selectedItems)), h.el("button.dhx_button.dhx_combobox-tag__action.dhx_combobox__action-clear-all", {
					"aria-label": "clear all"
				}, [h.el("span.dhx_button__icon.dxi.dxi-close-circle")])])] : []
			}
			return this.list.selection.getId().map(function (t) {
				var e = i.data.getItem(t);
				return e ? h.el("li.dhx_combobox-input-list__item.dhx_combobox-tag", {
					dhx_id: t
				}, [i._drawImageOrIcon(e), h.el("span.dhx_combobox-tag__value", i._getItemText(e)), h.el("button.dhx_button.dhx_button--icon.dhx_combobox-tag__action.dhx_combobox__action-remove", {
					type: "button",
					"aria-label": "remove"
				}, [h.el("span.dhx_button__icon.dxi.dxi-close-circle")])]) : null
			})
		}, E.prototype._drawImageOrIcon = function (t) {
			return t.src ? h.el("img.dhx_combobox-tag__image", {
				src: t.src,
				alt: ""
			}) : t.icon ? h.el("span.dhx_combobox-tag__icon", {
				class: t.icon
			}) : null
		}, E.prototype._getItemText = function (t) {
			return t ? t.value : null
		}, E.prototype._updateSelectedItem = function (t) {
			this.config.multiselection ? (this.config.selectAllButton && !this._state.unselectActive && this.data.getLength() === t.length ? (this._layout.getCell("select-unselect-all").attach(y.unselectAllView), this._state.unselectActive = !0) : this.config.selectAllButton && this._state.unselectActive && (this._layout.getCell("select-unselect-all").attach(y.selectAllView), this._state.unselectActive = !1), this._state.value && (this._state.value = "", this._state.canDelete = 0 === t.length, this._filter())) : this._state.value = null !== (t = this._getItemText(this.data.getItem(t))) && void 0 !== t ? t : "", this.paint()
		}, E.prototype._changePopupPosition = function () {
			var t = this;
			this.config.multiselection && h.awaitRedraw().then(function () {
				t._updatePopup()
			})
		}, E.prototype._updatePopup = function () {
			var t = this.getRootView().refs.holder.el;
			this.popup.getContainer().style.width = t.offsetWidth + "px";
			var e = this.data.getLength() * (this.config.itemHeight || 36);
			"string" == typeof this.config.listHeight && this.config.listHeight.includes("px") && (this.config.listHeight = this.config.listHeight.replace("px", ""));
			e = e < this.config.listHeight ? e : this.config.listHeight;
			this.popup.getContainer().style.height = (this.config.selectAllButton && this.config.multiselection ? e + 33 : e) + "px", this.popup.show(t, {
				mode: "bottom"
			})
		}, E);

		function E(t, e) {
			var i = x.call(this, t, r.extend({
				template: w,
				listHeight: 224,
				itemHeight: 36,
				disabled: !1,
				readOnly: !1
			}, e)) || this;
			i.config.itemsCount = i.config.itemsCount || i.config.showItemsCount, i.config.helpMessage = i.config.helpMessage || i.config.help, i.config.cellHeight && 36 === i.config.itemHeight && (i.config.itemHeight = i.config.cellHeight), i.config.labelInline && (i.config.labelPosition = "left"), Array.isArray(i.config.data) ? (i.events = new s.EventSystem(i), i.data = new c.DataCollection({}), i.data.parse(i.config.data)) : i.config.data ? (i.data = i.config.data, i.events = new s.EventSystem(i), i.events.context = i) : (i.events = new s.EventSystem(i), i.data = new c.DataCollection({})), i.popup = new _.Popup, i.popup.events.on(_.PopupEvents.afterShow, function () {
				i.paint()
			}), i.popup.events.on(_.PopupEvents.afterHide, function () {
				i.config.multiselection && (i._state.value = ""), i.paint()
			}), i.popup.events.on(_.PopupEvents.beforeHide, function (t) {
				t && i._hideOptions()
			}), (i.config.readonly || i.config.readOnly) && (i.config.readOnly = i.config.readOnly || i.config.readonly, i._keyListener = new v.KeyListener), i._state = {
				value: "",
				ignoreNext: !1,
				canDelete: !1,
				unselectActive: !1,
				currentState: b.ComboState.default
			}, i._initHandlers(), i._createLayout(), i.config.value && i._setValue(i.config.value, !0), i._initEvents();
			e = h.create({
				render: function () {
					return i._draw()
				},
				hooks: {
					didRedraw: function () {
						i.popup.isVisible() && (i.focus(), i._configurePopup())
					}
				}
			});
			return i.mount(t, e), i
		}
		e.Combobox = o
	}, function (t, r, e) {
		"use strict";
		var n, i = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			s = this && this.__assign || function () {
				return (s = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			a = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(r, "__esModule", {
			value: !0
		});
		var l = e(1),
			c = e(6),
			u = e(0),
			d = e(13),
			h = e(29),
			o = e(4),
			f = e(78),
			p = e(2),
			_ = e(46),
			v = e(168);
		r.MOVE_UP = 1, r.MOVE_DOWN = 2;
		var g, i = (g = o.View, i(m, g), m.prototype._didRedraw = function (t) { }, m.prototype._dblClick = function (t) {
			var e = p.locate(t);
			e && (e = this.data.getItem(e).id, this.config.editable && this.editItem(e), this.events.fire(_.ListEvents.doubleClick, [e, t]))
		}, m.prototype._clearTouchTimer = function () {
			this._touch.timer && (clearTimeout(this._touch.timer), this._touch.timer = null)
		}, m.prototype._dragStart = function (t) {
			var e = this;
			this._touch.start = !0;
			var i = [],
				n = p.locateNode(t, "dhx_id"),
				o = n && n.getAttribute("dhx_id"),
				n = this.selection.getId();
			return this.config.multiselection && n instanceof Array && (n.map(function (t) {
				t !== o && e.getRootView().refs[t] && i.push(e.getRootView().refs[t].el)
			}), n = a(n)), "string" == typeof n && (n = [n]), this.config.dragMode && !this._edited ? c.dragManager.onMouseDown(t, n || [o], i) : null
		}, m.prototype.disableSelection = function () {
			this.selection.disable()
		}, m.prototype.enableSelection = function () {
			this.selection.enable()
		}, m.prototype.editItem = function (t) {
			this._edited = t, this.data.getItem(this._edited) && this.events.fire(_.ListEvents.beforeEditStart, [t]) ? (this._getHotkeys(), this.paint(), this.events.fire(_.ListEvents.afterEditStart, [t])) : this._edited = null
		}, m.prototype.editEnd = function (t, e) {
			var i;
			this._edited && (null !== t && (i = this.data.getItem(e), this.data.update(e, s(s({}, i), {
				value: t
			}))), this._edited = null, this.paint())
		}, m.prototype.getFocusItem = function () {
			return this.data.getItem(this._focus)
		}, m.prototype.setFocus = function (t) {
			this._focus != t && this.data.exists(t) && (this._focus = t, this.showItem(t), this.events.fire(_.ListEvents.focusChange, [this.data.getIndex(this._focus), this._focus]), this.paint())
		}, m.prototype.getFocus = function () {
			return this._focus
		}, m.prototype.destructor = function () {
			this.events && this.events.clear(), this.keyManager && this.keyManager.destructor(), this.selection && this.selection.destructor(), this.config = this.events = this.selection = this.keyManager = null, this._handlers = this._focus = this._edited = this._events = this._topOffset = this._visibleHeight = this._touch = null, this.unmount()
		}, m.prototype.showItem = function (t) {
			var e, i, n, o = this.getRootView();
			o && o.node && o.node.el && void 0 !== t && ((e = this.getRootNode()) && (i = this.config.virtual, n = this.data.getIndex(t), o = Math.floor(n / e.children.length) || 0, t = e.children[n - e.children.length * o], (i || t) && (o = i ? parseInt(this.config.itemHeight) : t.clientHeight, (t = i ? n * o : t.offsetTop) >= e.scrollTop + e.clientHeight - o ? e.scrollTo(0, t - e.clientHeight + o) : t < e.scrollTop && e.scrollTo(0, t))))
		}, m.prototype._renderItem = function (t, e) {
			var i = this.config.itemHeight;
			if (t.$empty) return u.el("li", {
				class: "dhx_list-item",
				style: {
					height: i
				}
			});
			var n = this.config.template && this.config.template(t) || t.html,
				o = t.id == this._focus;
			if (t.id == this._edited) return v.getEditor(t, this).toHTML();
			var r = this.data.getMetaMap(t),
				o = s(s(s(s({}, this._events), {
					class: "dhx_list-item" + (r && r.selected ? " dhx_list-item--selected" : "") + (o ? " dhx_list-item--focus" : "") + (r && r.drop && !this._edited ? " dhx_list-item--drophere" : "") + (r && r.drag && !this._edited ? " dhx_list-item--dragtarget" : "") + (this.config.dragMode && !this._edited ? " dhx_list-item--drag" : "") + (t.css ? " " + t.css : ""),
					dhx_id: t.id,
					_ref: t.id.toString(),
					style: {
						height: i
					},
					_key: t.id,
					".innerHTML": n
				}), this.getItemAriaAttrs(this, t)), {
					tabindex: o ? 0 : -1
				});
			return n ? (o[".innerHTML"] = n, u.el("li", o)) : (o.class += " dhx_list-item--text", u.el("li", o, t.text || t.value))
		}, m.prototype._renderList = function () {
			var i = this,
				t = this._getRange(),
				e = this.data.getRawData(t[0], t[1]).map(function (t, e) {
					return i._renderItem(t, e)
				});
			return this.config.virtual && (e = a([u.el(".div", {
				style: {
					height: t[2] + "px"
				}
			})], e, [u.el(".div", {
				style: {
					height: t[3] + "px"
				}
			})])), u.el("ul.dhx_widget.dhx_list", s(s({
				style: {
					"max-height": this.config.height,
					position: "relative"
				},
				tabindex: 0,
				class: (this.config.css || "") + (this.config.multiselection && this.selection.getItem() ? " dhx_no-select--pointer" : ""),
				dhx_widget_id: this._uid
			}, this._handlers), this._getListAriaAttrs(this.config, this.data.getLength())), e)
		}, m.prototype.moveFocus = function (t, e) {
			var i, n, o = this.data.getLength();
			o && (n = (i = this._focus) ? this.data.getIndex(i) : -1, e = e || 1, t === r.MOVE_DOWN ? i = this.data.getId(Math.min(n + e, o - 1)) : t === r.MOVE_UP && (i = this.data.getId(Math.max(n - e, 0))), this.setFocus(i))
		}, m.prototype._getRange = function () {
			if (this.config.virtual) {
				var t = this._visibleHeight || parseInt(this.config.height),
					e = parseInt(this.config.itemHeight),
					i = this.data.getLength(),
					n = this.data.getLength() * e,
					o = this._topOffset,
					o = Math.max(0, Math.min(o, n - t)),
					r = Math.floor(o / e),
					t = Math.min(i - r, Math.floor(t / e) + 5);
				return this._topOffset = o, [r, t + r, r * e, n - e * (t + r)]
			}
			return [0, -1, 0, 0]
		}, m.prototype._getHotkeys = function () {
			var n = this;
			return {
				arrowDown: function (t) {
					n.moveFocus(r.MOVE_DOWN), t.preventDefault()
				},
				arrowUp: function (t) {
					n.moveFocus(r.MOVE_UP), t.preventDefault()
				},
				escape: function () {
					n.editEnd(null)
				},
				enter: function (t) {
					var e = n.selection.getItem(),
						i = e instanceof Array ? null === (i = e[0]) || void 0 === i ? void 0 : i.id : null == e ? void 0 : e.id;
					n.config.editable && (e && i === n._focus || !e) ? n.editItem(n._focus) : n.selection.add(n._focus), n.events.fire(_.ListEvents.click, [n._focus, t])
				},
				"shift+enter": function (t) {
					n.selection.add(n._focus, !1, !0), n.events.fire(_.ListEvents.click, [n._focus, t])
				},
				"ctrl+enter": function (t) {
					n.selection.add(n._focus, !0, !1), n.events.fire(_.ListEvents.click, [n._focus, t])
				},
				"ctrl+a": function (t) {
					n.config.multiselection && (t.preventDefault(), n.selection.remove(), n.data.map(function (t) {
						return t.id
					}).forEach(function (t) {
						"ctrlClick" === n.config.multiselection ? n.selection.add(t, !0) : n.selection.add(t)
					}))
				}
			}
		}, m.prototype._initHotKey = function () {
			var t, e = this._getHotkeys();
			for (t in e) this.keyManager.addHotKey(t, e[t]);
			for (t in this.config.hotkeys) this.keyManager.addHotKey(t, this.config.hotkeys[t])
		}, m.prototype.getItemAriaAttrs = function (t, e) {
			var i, n, o;
			return s(s({
				role: "option",
				"aria-selected": e.$selected ? "true" : "false"
			}, (o = e, (n = t).config.dragMode && !n._edited ? {
				"aria-grabbed": Boolean(o.$dragtarget && !n._edited).toString()
			} : {})), (i = t).config.editable ? {
				"aria-roledescription": i._edited ? "Press Enter to stop editing" : "Double click to edit content"
			} : {})
		}, m.prototype._getListAriaAttrs = function (t, e) {
			return {
				role: "listbox",
				"aria-label": "Listbox " + (t.title || "") + ", count of options = " + e + "." + (t.editable ? " Content is editable." : ""),
				"aria-multiselectable": t.selection && t.multiselection ? "true" : "false",
				"aria-readonly": t.editable ? "false" : "true"
			}
		}, m);

		function m(t, e) {
			void 0 === e && (e = {});
			var n = this,
				i = e.itemHeight || (e.virtual ? 37 : null);
			i && "number" == typeof i && (i = i.toString() + "px"), (n = g.call(this, t, l.extend({
				itemHeight: i,
				keyNavigation: !0,
				editable: !1,
				selection: !0
			}, e)) || this)._touch = {
				duration: 350,
				dblDuration: 300,
				timer: null,
				start: !1,
				timeStamp: null
			};
			e = n.config.data;
			e instanceof c.DataCollection ? (n.data = e, n.events = e.events) : (n.data = new c.DataCollection({}), n.events = n.data.events, e && n.data.parse(e)), n.selection = new f.Selection({
				disabled: !n.config.selection,
				multiselection: n.config.multiselection
			}, n.data, n.events), n.config.keyNavigation && (n.keyManager = new d.KeyManager(function (t, e) {
				return e == n._uid && (!n._edited || n._edited && "escape" !== t.key)
			}), n._initHotKey()), n.events.on(c.DataEvents.change, function (t, e, i) {
				"setPage" === e && n.showItem(n.data.getId(i[0])), n.paint()
			}), n.events.on(h.SelectionEvents.afterUnSelect, function () {
				return n.paint()
			}), n.events.on(h.SelectionEvents.afterSelect, function (t) {
				t && n.config.selection && (n._focus = t), n.paint()
			}), n.events.on(_.ListEvents.afterEditEnd, n.editEnd.bind(n));
			e = function (e) {
				return function (t) {
					n.data.setMeta(n.data.getItem(t.target), "drop", e), n.paint()
				}
			};
			n.events.on(c.DragEvents.canDrop, e(!0)), n.events.on(c.DragEvents.cancelDrop, e(!1));
			e = function (e) {
				return function (t) {
					t.source.map(function (t) {
						return n.data.setMeta(n.data.getItem(t), "drag", e)
					}), n.paint()
				}
			};
			n.events.on(c.DragEvents.dragStart, e(!0)), n.events.on(c.DragEvents.afterDrag, e(!1)), n._handlers = {
				onmousedown: function (t) {
					n._dragStart(t)
				},
				ontouchstart: function (t) {
					n._touch.timer = setTimeout(function () {
						n._dragStart(t)
					}, n._touch.duration), n._touch.timeStamp ? (n._touch.dblDuration >= n._touch.timeStamp - +t.timeStamp.toFixed() && (t.preventDefault(), n._dblClick(t)), n._touch.timeStamp = null) : n._touch.timeStamp = +t.timeStamp.toFixed(), setTimeout(function () {
						n._touch.timeStamp = null
					}, n._touch.dblDuration)
				},
				ontouchmove: function (t) {
					n._touch.start && t.preventDefault(), n._clearTouchTimer()
				},
				ontouchend: function () {
					n._touch.start = !1, n._clearTouchTimer()
				},
				ondragstart: function () {
					return !(n.config.dragMode && !n._edited) && null
				},
				oncontextmenu: function (t) {
					var e = p.locate(t);
					e && n.events.fire(_.ListEvents.itemRightClick, [e, t])
				},
				onclick: function (t) {
					var e = p.locate(t);
					e && (n.selection.add(e, t.ctrlKey || t.metaKey, t.shiftKey), n._focus = e, n.events.fire(_.ListEvents.click, [e, t]), n.paint())
				},
				ondblclick: function (t) {
					n._dblClick(t)
				},
				onscroll: function (t) {
					n.config.virtual && (n._topOffset = t.target.scrollTop, n._visibleHeight = t.target.offsetHeight, n.paint())
				},
				onmouseover: function (t) {
					var e = p.locate(t);
					e && e !== p.locate(t.relatedTarget) && n.events.fire(_.ListEvents.itemMouseOver, [e, t])
				}
			};
			e = n.config.eventHandlers;
			if (e)
				for (var o = 0, r = Object.entries(e); o < r.length; o++) {
					var s = r[o],
						a = s[0],
						s = s[1];
					n._handlers[a] = p.eventHandler(function (t) {
						return p.locate(t)
					}, s, n._handlers[a])
				}
			n.config.dragMode && c.dragManager.setItem(n._uid, n), n._topOffset = n._visibleHeight = 0;
			e = u.create({
				render: function () {
					return n._renderList()
				},
				hooks: {
					didMount: function (t) {
						n.config.virtual && (n._visibleHeight = t.node.el.offsetHeight)
					},
					didRedraw: function (t) {
						return n._didRedraw(t)
					}
				}
			});
			return n.mount(t, e), n
		}
		r.List = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(29),
			o = i(6),
			i = (r.prototype.enable = function () {
				this.config.disabled = !1
			}, r.prototype.disable = function () {
				this.remove(), this.config.disabled = !0
			}, r.prototype.getId = function () {
				return this.config.multiselection ? this._selected : this._selected[0]
			}, r.prototype.getItem = function () {
				var e = this;
				return this.config.multiselection ? this._selected.map(function (t) {
					return e._data.getItem(t)
				}) : this._selected.length ? this._data.getItem(this._selected[0]) : null
			}, r.prototype.contains = function (t) {
				return t ? this._selected.includes(t) : 0 < this._selected.length
			}, r.prototype.remove = function (t) {
				var e = this;
				t ? this._unselectItem(t) : (this._selected.forEach(function (t) {
					return e._unselectItem(t)
				}), this._selected = [])
			}, r.prototype.add = function (t, e, i, n) {
				var o, r = this;
				this.config.disabled || (void 0 !== t ? (o = this.config.multiselection, i && this._selected.length && o ? this._addMulti(t, n) : this._addSingle(t, o && ("ctrlClick" !== o || e), n)) : this._data.serialize().filter(function (t) {
					t = t.id;
					return !r._selected.includes(t)
				}).forEach(function (t) {
					t = t.id;
					r._addMulti(t, n)
				}))
			}, r.prototype.destructor = function () {
				var e = this;
				this._selected.forEach(function (t) {
					return e._unselectItem(t, !0)
				})
			}, r.prototype._addMulti = function (t, e) {
				var i = this._selected[this._selected.length - 1],
					n = this._data.getIndex(i),
					o = this._data.getIndex(t);
				for (o < n && (n = (t = [o, n])[0], o = t[1]); n <= o; n++) {
					var r = this._data.getId(n);
					this._selectItem(r, e)
				}
			}, r.prototype._addSingle = function (e, t, i) {
				var n = this;
				t || this._selected.forEach(function (t) {
					t != e && n._unselectItem(t)
				}), t && this._selected.includes(e) ? this._unselectItem(e, i) : this._selectItem(e, i)
			}, r.prototype._selectItem = function (t, e) {
				var i = this._data.getItem(t);
				i && !this._data.getMeta(i, "selected") && (e || this.events.fire(n.SelectionEvents.beforeSelect, [t])) && (this._selected.push(t), this._data.setMeta(i, "selected", !0), e || this.events.fire(n.SelectionEvents.afterSelect, [t]))
			}, r.prototype._unselectItem = function (e, t) {
				(t || this.events.fire(n.SelectionEvents.beforeUnSelect, [e])) && (this._selected = this._selected.filter(function (t) {
					return t !== e
				}), this._data.setMeta(this._data.getItem(e), "selected", !1), t || this.events.fire(n.SelectionEvents.afterUnSelect, [e]))
			}, r);

		function r(t, e, i) {
			var n = this;
			this.config = t, this.events = i, this._data = e, this._selected = [], this._data.events.on(o.DataEvents.removeAll, function () {
				n._selected = []
			}), "string" == typeof this.config.multiselection && (["click", "ctrlClick"].includes(this.config.multiselection) || (this.config.multiselection = !1)), this._data.events.on(o.DataEvents.beforeRemove, function (t) {
				var e;
				n._nextSelection = null, 1 === n._selected.length && (e = n._data.getIndex(t.id), 1 < (t = n._data.getLength()) && (e = t == e - 1 ? e - 1 : e + 1, n._nextSelection = n._data.getId(e)))
			}), this._data.events.on(o.DataEvents.afterRemove, function (t) {
				t = n._selected.indexOf(t.id); - 1 !== t && n._selected.splice(t, 1), n._nextSelection && (n.add(n._nextSelection), n._nextSelection = null)
			})
		}
		e.Selection = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			o = i(47);
		e.selectAllView = function () {
			return n.el(".dhx_list-item.dhx_combobox-options__item.dhx_combobox-options__item--select-all.dhx_combobox__action-select-all", o.default.selectAll)
		}, e.unselectAllView = function () {
			return n.el(".dhx_list-item.dhx_combobox-options__item.dhx_combobox-options__item--select-all.dhx_combobox__action-select-all", o.default.unselectAll)
		}, e.emptyListView = function () {
			return n.el("ul.dhx_list", [n.el("li.dhx_list-item.dhx_combobox-options__item", {}, o.default.notFound)])
		}
	}, function (t, e, i) {
		"use strict";
		var n;
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (n = e.ComboboxEvents || (e.ComboboxEvents = {})).change = "change", n.focus = "focus", n.blur = "blur", n.keydown = "keydown", n.input = "input", n.beforeOpen = "beforeOpen", n.afterOpen = "afterOpen", n.beforeClose = "beforeClose", n.afterClose = "afterClose", n.open = "open", n.close = "close", (e = e.ComboState || (e.ComboState = {}))[e.default = 0] = "default", e[e.error = 1] = "error", e[e.success = 2] = "success"
	}, function (t, e, i) {
		"use strict";
		var y = this && this.__assign || function () {
			return (y = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		},
			a = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var b = i(0),
			m = i(33),
			w = i(16),
			n = i(9),
			x = 2;

		function o(t, e, i, n) {
			i && (i.toLocaleLowerCase().includes("touch") ? e._events : e.events).fire(i, [t, n])
		}

		function E(t, e, i) {
			return {
				onclick: [o, t, i, n.GridEvents[e + "CellClick"]],
				onmouseover: [o, t, i, n.GridEvents[e + "CellMouseOver"]],
				onmousedown: [o, t, i, n.GridEvents[e + "CellMouseDown"]],
				ontouchstart: [o, t, i, n.GridEvents[e + "CellMouseDown"]],
				ondblclick: [o, t, i, n.GridEvents[e + "CellDblClick"]],
				oncontextmenu: [o, t, i, n.GridEvents[e + "CellRightClick"]],
				ontouchmove: [o, t, i, n.GridSystemEvents[e + "CellTouchMove"]],
				ontouchend: [o, t, i, n.GridSystemEvents[e + "CelltouchEnd"]]
			}
		}

		function C(t, e, i, n, o, r, s) {
			void 0 === o && (o = "");
			var a = e.type ? "dhx_" + e.type + "-cell" : "dhx_string-cell",
				t = i.content[t.content] && i.content[t.content].toHtml(e, i),
				l = {};
			return i.columns.forEach(function (t) {
				var e = !!i.content[t[n][s].content];
				l[t.id] = e && i.content[t[n][s].content].toHtml(t, i) || t[n][s].text
			}), b.el("." + a, {
				style: {
					class: o.trim(),
					padding: 0
				}
			}, [t && ("string" == typeof t || "number" == typeof t ? b.el("div", {
				class: "dhx_grid-footer-cell-text",
				role: "presentation",
				".innerHTML": e.template && "string" == typeof t ? e.template(t, l, e) : t
			}) : t)])
		}

		function l(f, t) {
			if (!f.data || !f.columns) return [];
			var e, p = f.$positions,
				_ = t.name,
				v = f.currentColumns,
				g = f[_ + "RowHeight"] || 40,
				m = (e = _, t = (t = v).map(function (t) {
					return t[e] || [{}]
				}), w.transpose(t));
			return m.map(function (t, h) {
				return b.el(".dhx_" + _ + "-row", y({
					style: {
						height: g
					}
				}, {
					role: "row",
					"aria-rowindex": h + 1
				}), t.map(function (n, t) {
					var e = n.css || "",
						i = v[t],
						o = p.xStart + t + 1,
						r = "dxi dxi-sort-variant dhx_grid-sort-icon",
						s = "none";
					f.sortBy && "" + i.id === f.sortBy && !n.content && (r += " dhx_grid-sort-icon--" + (u = f.sortDir || "asc"), e += " dhx_grid-" + _ + "-cell--sorted ", s = "asc" === u ? "ascending" : "descending");
					var a = w.isSortable(f, i) && n.text && "footer" !== _ && !1 !== n.headerSort;
					a && (e += " dhx_grid-header-cell--sortable");
					var l = 0 === t ? "dhx_first-column-cell" : "",
						c = t === v.length - 1 ? "dhx_last-column-cell" : "";
					n.content || (n.align ? e += " dhx_grid-header-cell--align_" + n.align + " " : e += " dhx_grid-header-cell--" + ("number" === i.type || "percent" === i.type || "date" === i.type ? "align_right" : "align_left") + " "), e += l + " " + c;
					var u = (void 0 !== i.resizable ? i : f).resizable;
					u && (u = b.el("div", {
						class: "dhx_resizer_grip_wrap",
						"aria-hidden": "true"
					}, [b.el("div", {
						class: "dhx_resizer_grip",
						dhx_resized: i.id,
						style: {
							height: 100 * m.length + "%"
						}
					}, [b.el("div", {
						class: "dhx_resizer_grip_line"
					})])]), ("footer" === _ || 0 < h) && (u = null)), n.align && (e += " dhx_align-" + n.align);
					l = function (t, e, i) {
						e = {
							"aria-colindex": e
						};
						return "footer" === t || n.content ? e.role = "gridcell" : (e.role = "columnheader", e["aria-sort"] = i), e
					};
					if (n.content) return b.el(".dhx_grid-" + _ + "-cell.dhx_grid-custom-content-cell", y(y({
						class: e.trim(),
						dhx_id: i.id,
						_key: t,
						style: {
							width: i.$width,
							height: "footer" === _ ? g + x / 2 + "px" : g + "px"
						}
					}, E(i, _, f)), l(_, o, s)), [C(n, i, f, _, "", 0, h), u || null]);
					var d, c = "dhx_grid-header-cell-text_content";
					return f.autoHeight && (c += " dhx_grid-header-cell-text_content-auto-height"), b.el(".dhx_grid-" + _ + "-cell", y(y({
						class: e.trim(),
						dhx_id: i.id,
						_key: t,
						style: {
							width: i.$width,
							height: "footer" === _ ? g + x / 2 + "px" : g + "px"
						}
					}, E(i, _, f)), l(_, o, s)), [b.el("div.dhx_grid-header-cell-text", {
						role: "presentation"
					}, [b.el("span", y(y({
						class: c
					}, (d = n.text, a ? {
						role: "button",
						"aria-label": "Sort by " + d
					} : {})), {
						".innerHTML": n.text
					})), u || null]), a && b.el("div", {
						class: r,
						"aria-hidden": "true"
					})])
				}))
			})
		}

		function c(d, h) {
			var f = d.columns,
				t = w.transpose(f.map(function (t) {
					return t[h.name] || []
				})),
				p = d[h.name + "RowHeight"] || 40,
				_ = h.name,
				v = d.$positions,
				g = 0;
			return t.map(function (t, u) {
				return g = 0, b.el(".dhx_span-row", {
					style: {
						top: p * u + "px",
						height: p
					},
					class: "dhx_header-row",
					"aria-hidden": "true"
				}, t.map(function (t, e) {
					var i = f[e];
					v.xStart;
					g += i.hidden ? 0 : i.$width;
					var n = 0 === e ? "dhx_first-column-cell" : "",
						o = e === f.length - 1 || (t.colspan || 0) + (e - 1) >= f.length - 1 ? "dhx_last-column-cell" : "",
						r = p;
					t.rowspan && (r = r * t.rowspan - 1);
					var s = w.isSortable(d, i) && t.rowspan && t.text && "footer" !== h.name,
						a = "dxi dxi-sort-variant dhx_grid-sort-icon";
					d.sortBy && "" + i.id === d.sortBy && !t.content && (a += " dhx_grid-sort-icon--" + (d.sortDir || "asc"));
					var l = i.align ? "dhx_align-" + i.align : "number" !== i.type && "percent" !== i.type && "date" !== i.type || t.colspan ? "dhx_align-left" : "dhx_align-right",
						c = "dhx_grid-header-cell " + n + " " + o + " " + (t.rowspan ? "dhx_span-cell__rowspan" : "") + " " + (t.align ? "dhx_align-" + t.align : l) + " " + (t.css || "");
					s && (c += " dhx_grid-header-cell--sortable"), t.content || (t.align ? c += " dhx_grid-header-cell--align_" + t.align + " " : c += " dhx_grid-header-cell--" + ("number" === i.type || "percent" === i.type || "date" === i.type ? "align_right" : "align_left") + " ");
					n = null;
					t.content && ((n = C(t, i, d, _, c, 0, u)).attrs.style = y(y({}, n.attrs.style), {
						width: "100%",
						borderRight: "0"
					}));
					o = "";
					0 < g - i.$width && (o = "1px solid #e4e4e4");
					l = "dhx_grid-header-cell-text_content";
					return d.autoHeight && (l += " dhx_grid-header-cell-text_content-auto-height"), t.colspan || t.rowspan ? b.el(".dhx_span-cell", y({
						style: {
							width: m.getWidth(f, t.colspan, e),
							height: "footer" === _ ? r + x / 2 : r,
							left: g - i.$width,
							borderLeft: o,
							top: p * u
						},
						class: c.trim(),
						dhx_id: i.id
					}, E(i, _, d)), [n || t.rowspan ? b.el("div.dhx_grid-header-cell-text", {
						role: "presentation"
					}, [b.el("span", {
						class: l,
						".innerHTML": t.text
					})]) : b.el("span", {
						class: l,
						".innerHTML": t.text
					}), s && b.el("div", {
						class: a
					})]) : null
				}).filter(function (t) {
					return t
				}))
			})
		}
		e.getRows = l, e.getFixedSpans = c, e.getFixedRows = function (t, e) {
			var i = l(t, e),
				n = c(t, e),
				o = null;
			"footer" !== e.name || e.sticky || (o = 0 <= t.leftSplit && l(y(y({}, t), {
				currentColumns: t.columns.slice(0, t.leftSplit),
				$positions: y(y({}, t.$positions), {
					xStart: 0,
					xEnd: t.leftSplit
				})
			}), e));
			var r, s = ((s = {
				position: "sticky"
			})[e.position] = 0, s);
			return e.sticky || (s.left = -t.scroll.left, r = -t.scroll.left, s.position = "relative"), b.el(".dhx_" + e.name + "-wrapper", {
				class: e.sticky ? "" : "dhx_compatible-" + e.name,
				style: y(y({}, s), {
					left: e.sticky ? r : 0,
					height: "footer" === e.name ? t[e.name + "Height"] + x / 2 : t[e.name + "Height"],
					width: e.sticky ? t.$totalWidth : e.wrapper.width - x
				}),
				role: "presentation"
			}, [b.el(".dhx_grid-" + e.name, {
				style: {
					height: "footer" === e.name ? t[e.name + "Height"] + x / 2 : t[e.name + "Height"],
					left: r,
					paddingLeft: e.shifts.x,
					width: t.$totalWidth
				},
				role: "presentation"
			}, [b.el(".dhx_" + e.name + "-rows", y({}, {
				role: "rowgroup",
				"aria-rowcount": a(i).length
			}), a(i)), b.el(".dhx_" + e.name + "-spans", {
				style: {
					marginLeft: -e.shifts.x
				},
				class: "dhx_" + e.name + "-rows",
				role: "presentation"
			}, n), o && b.el(".dhx_" + e.name + "-fixed-cols", {
				style: {
					position: "absolute",
					top: 0,
					left: t.scroll.left + "px",
					height: "100%"
				}
			}, o)]), b.el("div", {
				style: {
					width: t.$totalWidth
				},
				role: "presentation"
			})])
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(1),
			l = i(0),
			c = i(2),
			u = i(10),
			d = i(19),
			o = (s = d.Navbar, o(h, s), h.prototype.getState = function (t) {
				if (!a.isDefined(t) || this.data.getItem(t)) {
					var e, i = {};
					for (e in this.data.eachChild(this.data.getRoot(), function (t) {
						t.twoState && !t.group ? i[t.id] = t.active : "input" !== t.type && "selectButton" !== t.type || (i[t.id] = t.value)
					}, !1), this._groups) this._groups[e].active && (i[e] = this._groups[e].active);
					return t ? i[t] : i
				}
			}, h.prototype.setState = function (t) {
				for (var e in t) {
					var i;
					this._groups && this._groups[e] ? this._groups[e].active && (this.data.update(this._groups[e].active, {
						active: !1
					}), this._groups[e].active = t[e], this.data.update(t[e], {
						active: !0
					})) : "input" === (i = this.data.getItem(e)).type || "selectButton" === i.type ? this.data.update(e, {
						value: t[e]
					}) : this.data.update(e, {
						active: t[e]
					})
				}
			}, h.prototype._customHandlers = function () {
				var i = this;
				return {
					input: function (t) {
						var e = c.locate(t);
						i.data.update(e, {
							value: t.target.value
						})
					},
					tooltip: function (t) {
						var e = c.locateNode(t);
						e && (t = e.getAttribute("dhx_id"), (t = i.data.getItem(t)).tooltip && u.tooltip(t.tooltip, {
							node: e,
							position: u.Position.bottom
						}))
					}
				}
			}, h.prototype._getFactory = function () {
				return d.createFactory({
					widget: this,
					defaultType: "navItem",
					allowedTypes: ["button", "imageButton", "selectButton", "navItem", "menuItem", "separator", "spacer", "title", "input", "customHTML", "datePicker", "customHTMLButton"],
					widgetName: "toolbar"
				})
			}, h.prototype._draw = function (t) {
				var i = this,
					e = this.data.getLength() ? this.data.reduce(function (t, e) {
						switch (e.type) {
							case "title":
								return t || 20;
							case "button":
								return "small" === e.size && (!t || t <= 28) ? 28 : t || 32;
							default:
								return 32
						}
					}, 0) + 24 : null;
				return l.el("nav.dhx_widget.dhx_toolbar", {
					style: {
						height: e
					},
					class: this.config.css || ""
				}, [l.el("ul.dhx_navbar.dhx_navbar--horizontal", r(r({
					dhx_widget_id: this._uid,
					tabindex: 0
				}, {
					role: "toolbar",
					"aria-label": t || ""
				}), {
					onclick: this._handlers.onclick,
					onmousedown: this._handlers.onmousedown,
					oninput: this._handlers.input,
					onmouseover: this._handlers.tooltip,
					_hooks: {
						didInsert: function (t) {
							t.el.addEventListener("keyup", function (t) {
								var e;
								9 !== t.which || (e = c.locateNode(document.activeElement)) && (t = e.getAttribute("dhx_id"), (t = i.data.getItem(t)).tooltip && u.tooltip(t.tooltip, {
									node: e,
									position: u.Position.bottom,
									force: !0
								}))
							}, !0)
						}
					}
				}), this.data.map(function (t) {
					return i._factory(t)
				}, this.data.getRoot(), !1))])
			}, h.prototype._getMode = function (t, e) {
				return t.id === e ? "bottom" : "right"
			}, h.prototype._close = function (t) {
				this._activePosition = null, this._currentRoot = null, s.prototype._close.call(this, t)
			}, h.prototype._setRoot = function (t) {
				this.data.getParent(t) === this.data.getRoot() && (this._currentRoot = t)
			}, h);

		function h(t, e) {
			var i = s.call(this, t, a.extend({
				navigationType: "click"
			}, e)) || this;
			i._currentRoot = null;
			return i.mount(t, l.create({
				render: function () {
					return i._draw(t)
				}
			})), i
		}
		e.Toolbar = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.grayShades = ["#000000", "#4C4C4C", "#666666", "#808080", "#999999", "#B3B3B3", "#CCCCCC", "#E6E6E6", "#F2F2F2", "#FFFFFF"], e.palette = [
			["#D4DAE4", "#B0B8CD", "#949DB1", "#727A8C", "#5E6677", "#3F4757", "#1D2534"],
			["#FFCDD2", "#FE9998", "#F35C4E", "#E94633", "#D73C2D", "#CA3626", "#BB2B1A"],
			["#F9E6AD", "#F4D679", "#EDB90F", "#EAA100", "#EA8F00", "#EA7E00", "#EA5D00"],
			["#BCE4CE", "#90D2AF", "#33B579", "#36955F", "#247346", "#1D5B38", "#17492D"],
			["#BDF0E9", "#92E7DC", "#02D7C5", "#11B3A5", "#018B80", "#026B60", "#024F43"],
			["#B3E5FC", "#81D4FA", "#29B6F6", "#039BE5", "#0288D1", "#0277BD", "#01579B"],
			["#AEC1FF", "#88A3F9", "#5874CD", "#2349AE", "#163FA2", "#083596", "#002381"],
			["#C5C0DA", "#9F97C1", "#7E6BAD", "#584A8F", "#4F4083", "#473776", "#3A265F"],
			["#D6BDCC", "#C492AC", "#A9537C", "#963A64", "#81355A", "#6E3051", "#4C2640"],
			["#D2C5C1", "#B4A09A", "#826358", "#624339", "#5D4037", "#4E342E", "#3E2723"]
		]
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.ColorpickerEvents || (e.ColorpickerEvents = {})).change = "change", e.apply = "apply", e.cancelClick = "cancelClick", e.modeChange = "modeChange", e.selectClick = "selectClick", e.colorChange = "colorChange", e.viewChange = "viewChange"
	}, function (t, e, i) {
		"use strict";
		var v = this && this.__assign || function () {
			return (v = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var g = i(1),
			m = i(0),
			y = i(7),
			b = function (t) {
				return t.toString()
			},
			w = function (t, e) {
				return {
					role: "graphics-object",
					"aria-label": t + "-axis" + (e ? ", " + e : "")
				}
			};
		e.bottom = function (t, e, i, n) {
			var r, s, o = e.title,
				a = e.textPadding,
				l = e.scalePadding,
				c = e.textTemplate,
				u = e.showText,
				d = e.scaleRotate,
				h = c || b,
				f = [],
				c = 0;
			return u && (c = a, r = d && !isNaN(d), s = n + a, f = t.map(function (t) {
				var e, i = t[0],
					n = r ? "rotate(" + d + " " + i + " " + s + ")" : "",
					o = ["scale-text", "top-text"];
				return r && (e = d % 360, o.push(y.getClassesForRotateScale("bottom", e))), m.sv("text", {
					x: i,
					y: s,
					class: o.join(" "),
					transform: n
				}, [y.verticalCenteredText(h(t[1]))])
			})), a = g.uid(), t = null, n = m.sv("path", {
				class: "main-scale",
				d: "M0 " + n + " H" + (i - .5),
				id: a
			}), o && (t = m.sv("text", {
				dx: i / 2,
				dy: l + c
			}, [m.sv("textPath", {
				href: "#" + a,
				class: "scale-title "
			}, o)])), m.sv("g", v({}, w("x", o || e.text)), [n, t].concat(f))
		}, e.bottomGrid = function (t, e, i, n) {
			for (var o, r, s, a = n.dashed, l = n.grid, c = n.targetLine, u = n.targetValue, d = t.length, h = [], f = "grid-line " + (a ? "dash-line" : "") + " " + (l ? "" : "hidden-line"), p = 0; p < d; p++) 0 === p && 0 === t[p][0] && !n.hidden || (p !== c ? (r = "M" + t[p][0] + " 0 V " + i, s = m.sv("path", {
				d: r,
				class: f,
				_ref: "line" + Math.round(t[p][0])
			}), h.push(s), p === d - 1 && t[p][0] !== e && (o = "M" + e + " 0 V " + i, o = m.sv("path", {
				d: o,
				class: f
			}), h.push(o))) : (o = "M" + t[p][0] + " 0 V " + i, o = m.sv("path", {
				d: o,
				class: f + " spec-grid-line"
			}), h.push(o)));
			return u && (r = "M" + u * e + " 0 V " + i, s = m.sv("path", {
				d: r,
				class: f + " spec-grid-line"
			}), h.push(s)), m.sv("g", h)
		}, e.top = function (t, e, i, n) {
			var r, s, o = e.title,
				a = e.textPadding,
				l = e.scalePadding,
				c = e.textTemplate,
				u = e.showText,
				d = e.scaleRotate,
				h = c || b,
				f = [],
				c = 0;
			return u && (c = a, r = d && !isNaN(d), s = -a, f = t.map(function (t) {
				var e, i = ["scale-text"],
					n = t[0],
					o = r ? "rotate(" + d + " " + n + " " + s + ")" : "";
				return r && (e = d % 360, i.push(y.getClassesForRotateScale("top", e))), m.sv("text", {
					x: n,
					y: s,
					class: i.join(" "),
					transform: o
				}, [y.verticalCenteredText(h(t[1]))])
			})), u = g.uid(), a = m.sv("path", {
				d: "M0 0 H" + i,
				class: "main-scale",
				id: u
			}), t = null, o && (t = m.sv("text", {
				dx: i / 2,
				dy: -l - c
			}, [m.sv("textPath", {
				href: "#" + u,
				class: "scale-title"
			}, o)])), m.sv("g", v({}, w("x", o || e.text)), [a, t].concat(f))
		}, e.topGrid = function (t, e, i, n) {
			for (var o, r, s = n.dashed, a = n.grid, l = n.targetLine, c = t.length, u = [], d = "grid-line " + (s ? "dash-line" : "") + " " + (a ? "" : "hidden-line"), h = 0; h < c; h++) 0 === h && 0 === t[h][0] && !n.hidden || (h !== l ? (o = "M" + t[h][0] + " 0 V " + i, o = m.sv("path", {
				d: o,
				class: d,
				_ref: "line" + Math.round(t[h][0])
			}), u.push(o), h === c - 1 && 0 !== t[h][0] && (r = "M0 0 V " + i, r = m.sv("path", {
				d: r,
				class: d
			}), u.push(r))) : (r = "M" + t[h][0] + " 0 V " + i, r = m.sv("path", {
				d: r,
				class: d + " spec-grid-line"
			}), u.push(r)));
			return m.sv("g", u)
		}, e.left = function (t, e, i, n) {
			var s, a, l, c = e.title,
				u = e.textPadding,
				o = e.scalePadding,
				r = e.textTemplate,
				d = e.showText,
				h = e.scaleRotate,
				f = r || b,
				p = [],
				_ = 0;
			return d && (s = y.getFontStyle("scale-text"), a = 0, l = h && !isNaN(h), p = t.map(function (t) {
				var e, i = t[0],
					n = -u,
					o = l ? "rotate(" + h + " " + n + " " + i + ")" : "",
					r = ["scale-text"],
					t = f(t[1]);
				return c && (e = y.getTextWidth(t, s), a < e && (a = e)), l ? (e = h % 360, r.push(y.getClassesForRotateScale("left", e))) : r.push("end-text"), m.sv("text", {
					x: n,
					y: i,
					class: r.join(" "),
					transform: o
				}, [y.verticalCenteredText(t)])
			}), _ = a + u), r = g.uid(), d = m.sv("path", {
				class: "main-scale",
				d: "M0 " + n + " V 0.5",
				id: r,
				_ref: t.length ? "line0" : null
			}), t = null, c && (t = m.sv("text", {
				dx: n / 2,
				dy: -o - _
			}, [m.sv("textPath", {
				href: "#" + r,
				class: "scale-title"
			}, c)])), m.sv("g", v({}, w("y", c || e.text)), [d, t].concat(p))
		}, e.leftGrid = function (t, e, i, n) {
			for (var o, r, s, a = n.dashed, l = n.grid, c = n.targetLine, u = n.targetValue, d = t.length, h = [], f = "grid-line " + (a ? "dash-line" : ""), p = 0; p < d; p++) 0 === p && t[p][0] === i && !n.hidden || (c !== p ? l && (r = "M0 " + t[p][0] + " H " + e, s = m.sv("path", {
				d: r,
				class: f
			}), h.push(s), p === d - 1 && t[p][0] !== e && (o = "M0 0 H" + e, o = m.sv("path", {
				d: o,
				class: f
			}), h.push(o))) : (r = "M0 " + t[p][0] + " H " + e, s = m.sv("path", {
				d: r,
				class: f + " spec-grid-line"
			}), h.push(s)));
			return u && (r = "M0 " + u * i + " H " + e, s = m.sv("path", {
				d: r,
				class: f + " spec-grid-line"
			}), h.push(s)), m.sv("g", h)
		}, e.right = function (t, e, s, i) {
			var a, l, c, u = e.title,
				d = e.textPadding,
				n = e.scalePadding,
				o = e.textTemplate,
				r = e.showText,
				h = e.scaleRotate,
				f = o || b,
				p = [],
				_ = 0;
			return r && (a = y.getFontStyle("scale-text"), l = 0, c = h && !isNaN(h), p = t.map(function (t) {
				var e, i = f(t[1]),
					n = t[0],
					o = s + d,
					r = c ? "rotate(" + h + " " + o + " " + n + ")" : "",
					t = ["scale-text"];
				return u && (e = y.getTextWidth(i, a), l < e && (l = e)), c ? (e = h % 360, t.push(y.getClassesForRotateScale("right", e))) : t.push("start-text"), m.sv("text", {
					x: o,
					y: n,
					class: t.join(" "),
					transform: r
				}, [y.verticalCenteredText(i)])
			}), _ = d + l), o = g.uid(), r = m.sv("path", {
				d: "M" + s + " " + i + " V 0",
				class: "main-scale",
				id: o,
				_ref: t.length ? "line0" : null
			}), t = null, u && (t = m.sv("text", {
				dx: i / 2,
				dy: n + _
			}, [m.sv("textPath", {
				href: "#" + o,
				class: "scale-title"
			}, u)])), m.sv("g", v({}, w("y", u || e.text)), [r, t].concat(p))
		}, e.rightGrid = function (t, e, i, n) {
			for (var o, r, s, a = n.dashed, l = n.grid, c = n.targetLine, u = t.length, d = [], h = "grid-line " + (a ? "dash-line" : ""), f = 0; f < u; f++) 0 === f && t[f][0] === i && !n.hidden || (c !== f ? l && (r = "M0 " + t[f][0] + " H " + e, s = m.sv("path", {
				d: r,
				class: h
			}), d.push(s), f === u - 1 && t[f][0] !== e && (o = "M0 0 H" + e, o = m.sv("path", {
				d: o,
				class: h
			}), d.push(o))) : (r = "M0 " + t[f][0] + " H " + e, s = m.sv("path", {
				d: r,
				class: h + " spec-grid-line"
			}), d.push(s)));
			return m.sv("g", d)
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, g = i(0),
			a = i(7),
			i = i(53),
			o = (s = i.default, o(l, s), l.prototype.paint = function (t, e, i) {
				s.prototype.paint.call(this, t, e);
				var n = [];
				return this._form(t, e, n, i), this._markers(n), g.sv("g", r(r({
					class: "seria",
					_key: this.id
				}, {
					"aria-label": "chart " + (this.config.value || "")
				}), {
					tabindex: 0
				}), n)
			}, l.prototype.paintformAndMarkers = function (t, e, i) {
				s.prototype.paint.call(this, t, e);
				var n = [],
					o = [];
				return this._form(t, e, n, i), this._markers(o), [g.sv("g", {
					class: "seria",
					_key: this.id
				}, n), g.sv("g", {
					class: "seria_markers",
					_key: this.id + "_markers"
				}, o)]
			}, l.prototype._markers = function (t) {
				var e, i, n = this;
				this.config.pointType && (e = this.config.pointColor || this.config.color, i = this._getPointType(this.config.pointType, e), t.push.apply(t, this._points.map(function (t) {
					return i(t[0], t[1], a.calcPointRef(t[2], n.id))
				})))
			}, l.prototype._form = function (t, i, e, n) {
				var o, r, s = "chart " + this.config.type + " " + (this.config.css || "") + " " + (this.config.dashed ? "dash-line" : ""),
					a = this.config,
					l = a.id,
					c = a.fill,
					u = a.alpha,
					d = a.color,
					h = a.strokeWidth,
					f = this._points,
					a = f[f.length - 1],
					p = "";
				if (n) {
					for (var _ = n.length - 1; 0 <= _; _--) {
						var v = n[_];
						p += _ === f.length - 1 ? "M" + v[0] + " " + v[1] + " " : "L" + v[0] + " " + v[1] + " "
					}
					p += f.map(function (t, e) {
						return e ? "L " + t[0] + " " + t[1] : "V " + t[1]
					}).join(" ") + "Z"
				} else p += f.map(function (t, e) {
					return e ? "L" + t[0] + " " + t[1] : "M0 " + i + " L0 " + t[1] + " L" + t[0] + " " + t[1]
				}).join(" ") + ("L" + t + " " + a[1]) + " V " + i;
				h && (o = f.length - 1, r = function (t) {
					return t === o ? -.5 : t ? 0 : .5
				}, a = f.map(function (t, e) {
					return e ? "L" + (t[0] + r(e)) + " " + t[1] : "M0 " + t[1] + " L0 " + (t[1] + r(e)) + " L" + (t[0] + r(e)) + " " + t[1]
				}).join(" ") + ("L" + t + " " + a[1]), d = g.sv("path", {
					d: a,
					"stroke-width": h,
					stroke: d,
					fill: "none",
					class: s
				}), e.push(d));
				u = g.sv("path", {
					id: "seria" + l,
					d: p,
					class: s,
					fill: c,
					_ref: l,
					"fill-opacity": u,
					stroke: "none"
				});
				return e.push(u), e
			}, l.prototype._setDefaults = function (t) {
				var e = {
					alpha: .3,
					strokeWidth: 2,
					fill: t.color || "#5E83BA",
					color: "#5E83BA",
					active: !0,
					tooltip: !0,
					pointType: "empty"
				};
				this.config = r(r({}, e), t);
				e = this.config.pointType, t = this.config.pointColor || this.config.color;
				e && (this._drawPointType = this._getPointType(e, t))
			}, l);

		function l() {
			return null !== s && s.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s = i(0),
			a = i(7),
			n = {
				circle: function (t, e, i, n, o, r) {
					t = {
						_ref: r,
						cx: n,
						cy: o,
						r: 4,
						class: "figure point-circle",
						fill: e,
						stroke: t,
						"stroke-width": 2
					};
					return s.sv("circle", t)
				},
				rect: function (t, e, i, n, o, r) {
					t = {
						_ref: r,
						x: n - 4,
						y: o - 4,
						width: 8,
						height: 8,
						class: "figure point-rect",
						fill: e,
						stroke: t,
						"stroke-width": 2
					};
					return s.sv("rect", t)
				},
				rhombus: function (t, e, i, n, o, r) {
					t = {
						_ref: r,
						points: n - 5 + "," + o + " " + n + "," + (o + 5) + " " + (n + 5) + "," + o + " " + n + "," + (o - 5),
						class: "figure point-rhombus",
						fill: e,
						stroke: t,
						"stroke-width": 2
					};
					return s.sv("polygon", t)
				},
				triangle: function (t, e, i, n, o, r) {
					t = {
						_ref: r,
						points: n + "," + (o - 5) + " " + (n + 5) + "," + (o + 5) + " " + (n - 5) + "," + (o + 5),
						class: "figure point-triangle",
						fill: e,
						stroke: t,
						"stroke-width": 2
					};
					return s.sv("polygon", t)
				},
				simpleCircle: function (t, e, i, n, o, r) {
					t = {
						_ref: r,
						cx: n,
						cy: o,
						r: 3,
						class: "figure point-simple-circle",
						fill: t
					};
					return s.sv("circle", t)
				},
				simpleRect: function (t, e, i, n, o, r) {
					t = {
						_ref: r,
						x: n - 3,
						y: o - 3,
						width: 6,
						height: 6,
						class: "figure point-simple-rect",
						fill: t
					};
					return s.sv("rect", t)
				},
				empty: function () {
					return null
				}
			},
			o = {
				circle: function (t, e, i, n, o, r) {
					return '<circle class="figure point-circle" _ref="' + r + '" cx="' + n + '" cy="' + o + '" r="4" fill="' + e + '" stroke="' + t + '" stroke-width="2"/>'
				},
				rect: function (t, e, i, n, o, r) {
					return '<rect _ref="' + r + '" x="' + (n - 4) + '" y="' + (o - 4) + '" width="8" height="8" class="figure point-rect" fill="' + e + '" stroke="' + t + '" stroke-width="2"/>'
				},
				rhombus: function (t, e, i, n, o, r) {
					return '<polygon _ref="' + r + '" points="' + (n - 5) + "," + o + " " + n + "," + (o + 5) + " " + (n + 5) + "," + o + " " + n + "," + (o - 5) + '" class="figure point-rhombus" fill="' + e + '" stroke="' + t + '" stroke-width="2"/>'
				},
				triangle: function (t, e, i, n, o, r) {
					return '<polygon _ref="' + r + '" points="' + n + "," + (o - 5) + " " + (n + 5) + "," + (o + 5) + " " + (n - 5) + "," + (o + 5) + '" class="figure point-triangle" fill="' + e + '" stroke="' + t + '" stroke-width="2"/>'
				},
				simpleCircle: function (t, e, i, n, o, r) {
					return '<circle _ref="' + r + '" cx="' + n + '" cy="' + o + '" r="3" class="figure point-simple-circle" fill="' + t + '"/>'
				},
				simpleRect: function (t, e, i, n, o, r) {
					return '<rect _ref="id" x="' + (n - 3) + '" y="' + (o - 3) + '" width="6" height="6" class="figure point-simple-rect" fill="' + t + '"/>'
				},
				empty: function () {
					return null
				}
			};

		function l(t) {
			t = n[t.toString()];
			if (!t) throw new Error("unknown point type");
			return t
		}

		function c(t) {
			t = o[t.toString()];
			if (!t) throw new Error("unknown point type");
			return t
		}
		e.getHelper = l, e.getHTMLHelper = c, e.getShadeHelper = function (t, n) {
			var o = l(t);
			n = n || "none";
			var r = a.getColorShade(n, .2);
			return function (t, e, i) {
				return o(n, r, "", t, e, i)
			}
		}, e.getShadeHTMLHelper = function (t, n) {
			var o = c(t);
			n = n || "none";
			var r = a.getColorShade(n, .2);
			return function (t, e, i) {
				return o(n, r, "", t, e, i)
			}
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			_ = this && this.__assign || function () {
				return (_ = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(1),
			v = i(0),
			g = i(7),
			i = i(53),
			o = (r = i.default, o(a, r), a.prototype.addScale = function (t, e) {
				r.prototype.addScale.call(this, t, e), e.addPadding()
			}, a.prototype.seriesShift = function (t) {
				return this._shift = t, this.config.barWidth
			}, a.prototype.paint = function (t, e, i) {
				r.prototype.paint.call(this, t, e);
				if (!this.config.active) return null;
				var n = [];
				this._gradient && n.push(v.sv("defs", [this._gradient()]));
				var o = "chart " + this.config.type + " " + (this.config.css || "") + " " + (this.config.dashed ? "dash-line" : ""),
					i = this._getForm(this._points, o, t, e, i),
					n = n.concat(i);
				return v.sv("g", _(_({
					class: "seria",
					_key: this.id
				}, {
					"aria-label": "chart " + (this.config.value || "")
				}), {
					tabindex: 0
				}), n)
			}, a.prototype.getTooltipType = function (t, e, i) {
				return void 0 !== this.config.baseLine && this._baseLinePosition < i ? "bot" : "top"
			}, a.prototype._getClosestDist = function (t, e, i, n) {
				return this.config.stacked && e < n ? 1 / 0 : Math.abs(t - i)
			}, a.prototype._path = function (t, e) {
				return t[0] += this._shift, "\nM " + (t[0] - this.config.barWidth / 2) + " " + e + "\nV " + t[1] + "\nh " + this.config.barWidth + "\nV " + e
			}, a.prototype._base = function (t) {
				var e = this.config.baseLine;
				return this._baseLinePosition = void 0 !== e ? this.yScale.point(e) * t : t - 1
			}, a.prototype._text = function (t, e, i) {
				var n = t[0],
					t = (e + t[1]) / 2;
				return {
					x: n,
					y: t,
					class: "bar-text",
					transform: i && !isNaN(i) ? "rotate(" + i + " " + n + " " + t + ")" : ""
				}
			}, a.prototype._getForm = function (t, i, e, n, o) {
				function r(t) {
					return o ? o[t][1] : p
				}
				var s = this,
					a = this.config,
					l = a.baseLine,
					c = a.fill,
					u = a.alpha,
					d = a.showText,
					h = a.showTextTemplate,
					f = a.showTextRotate,
					a = [],
					p = this._base(n),
					n = t.map(function (t, e) {
						return v.sv("path", _(_({
							_key: "seria" + s.config.id + e,
							_ref: g.calcPointRef(t[2], s.config.id),
							d: s._path(t, r(e)),
							class: i,
							fill: c,
							onclick: [s._handlers.onclick, t[2], s.config.value],
							onmousemove: [s._handlers.onmousemove, t[2], s.config.id],
							onmouseleave: [s._handlers.onmouseleave, t[2], s.config.id],
							"fill-opacity": u
						}, {
							role: "graphics-symbol",
							"aria-roledescription": "bar",
							"aria-label": function (t, e, i) {
								void 0 === i && (i = 0);
								var n = e[3],
									o = i,
									e = e[4];
								return e < i && (o = e, e = i), "xbar" === t ? "bar y=" + n + ", x from " + o + " to " + e : "bar x=" + n + ", y from " + o + " to " + e
							}(s.config.type, t, l)
						}), {
							tabindex: 0
						}))
					});
				return a.push.apply(a, n), (d || h || f) && !1 !== d && (t = t.map(function (t, e) {
					var i, n, o = s._getText(t);
					return i = t, n = e, 16 < Math.abs(r(n) - i[1]) ? v.sv("text", _(_({}, s._text(t, r(e), f)), {
						"aria-hidden": "true"
					}), [h ? g.verticalCenteredText(h(o)) : g.verticalCenteredText(o)]) : null
				}), a.push.apply(a, t)), a
			}, a.prototype._getText = function (t) {
				return t[4].toString()
			}, a.prototype._setDefaults = function (t) {
				this.config = _(_({}, {
					barWidth: 30,
					alpha: 1,
					active: !0,
					tooltip: !0,
					pointType: "empty"
				}), t);
				var e, i, n = this.config.pointType,
					t = this.config.pointColor || this.config.color;
				n && (this.config.pointType = n, this._drawPointType = this._getPointType(n, t)), this.config.gradient && (e = "gradient" + s.uid(), i = this.config.gradient(this.config.fill), this._gradient = function () {
					return g.linearGradient(i, e)
				}, this.config.fill = "url(#" + e + ")")
			}, a);

		function a() {
			var t = null !== r && r.apply(this, arguments) || this;
			return t._shift = 0, t
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.default = function (t, e) {
			var i = t.length;
			if (i < 3) a = t;
			else
				for (var n = t[0], o = t[0], r = t[1], s = t[2], a = [t[0].slice(0, 2)], l = 1; l < i; l++) a.push([(-n[0] + 6 * o[0] + r[0]) / 6, (-n[1] + 6 * o[1] + r[1]) / 6, (o[0] + 6 * r[0] - s[0]) / 6, (o[1] + 6 * r[1] - s[1]) / 6, r[0], r[1]]), n = o, o = r, r = s, s = t[l + 2] || s;
			for (var c = "", l = 0; l < a.length; l++) {
				var u = a[l],
					d = u.length;
				l ? c += 5 < d ? "C" + u[0] + " " + u[1] + "\n\t\t\t\t" + u[2] + " " + u[3] + "\n\t\t\t\t" + u[4] + " " + u[5] : 5 === d ? "L" + u[0] + " " + u[1] : "S" + u[0] + " " + u[1] + "\n\t\t\t\t" + u[2] + " " + u[3] : (c += e ? "L" : "M", c += 5 === d ? u[0] + " " + u[1] : u[d - 2] + " " + u[d - 1])
			}
			return c
		}
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(91)), n(e(226)), n(e(92))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			d = this && this.__assign || function () {
				return (d = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(1),
			h = i(0),
			a = i(28),
			f = i(224),
			o = (r = a.List, o(l, r), l.prototype.showItem = function (t) {
				var e, i, n, o = this.getRootView();
				o && o.node && o.node.el && void 0 !== t && ((e = this.getRootNode()) && (i = this.config.virtual, n = this.data.getIndex(t), o = Math.floor(n / this.config.itemsInRow), t = Math.ceil(o / e.children.length) || 0, t = e.children[o - e.children.length * t], (i || t) && (t = t.children[n % this.config.itemsInRow], n = parseInt(this.config.gap.toString().replace("px", ""), null), t.offsetTop >= e.clientHeight + e.scrollTop - t.clientHeight ? e.scrollTop = t.offsetTop - e.clientHeight + t.clientHeight + n : t.offsetTop < e.scrollTop - n && (e.scrollTop = t.offsetTop - n))))
			}, l.prototype._didRedraw = function (t) {
				var e = t.node.el,
					i = e.scrollHeight > e.offsetHeight,
					e = t.node.attrs.class.replace(" dhx_dataview--has-scroll", ""),
					e = i ? e + " dhx_dataview--has-scroll" : e;
				t.node.patch({
					class: e
				})
			}, l.prototype._renderItem = function (t, e) {
				function i(t) {
					return parseFloat(t)
				}
				var n = this.config,
					o = n.itemsInRow,
					r = n.gap,
					s = n.template,
					a = n.itemHeight,
					l = s ? s(t) : t.htmlContent,
					c = t.id == this._focus,
					u = (e + 1) % this.config.itemsInRow == 0;
				if (t.id == this._edited) return f.getEditor(t, this).toHTML(u);
				n = t.id.toString(), e = this.data.getMetaMap(t);
				return h.el("div", d(d({
					class: "dhx_dataview-item" + (e && e.selected ? " dhx_dataview-item--selected" : "") + (c ? " dhx_dataview-item--focus" : "") + (e && e.drop && !this._edited ? " dhx_dataview-item--drophere" : "") + (e && e.drag && !this._edited ? " dhx_dataview-item--dragtarget" : "") + (this.config.dragMode && !this._edited ? " dhx_dataview-item--drag" : "") + (i(r) ? " dhx_dataview-item--with-gap" : "") + (t.css ? " " + t.css : "") + (u ? " dhx_dataview-item--last-item-in-row" : ""),
					style: {
						width: "calc(" + 100 / o + "% - " + i(r) + " * " + (o - 1) / o + "px)",
						"margin-right": u ? "" : r,
						height: s ? null : a
					},
					_key: n,
					dhx_id: n,
					_ref: n
				}, this.getDataViewItemAriaAttrs(this, t)), {
					tabindex: c ? 0 : -1
				}), l ? [h.el(".dhx_dataview-item__inner-html", {
					".innerHTML": l,
					role: "presentation"
				})] : t.value || t.text || t.value)
			}, l.prototype._renderList = function () {
				var n = this,
					t = this.data.getRawData(0, -1),
					e = this.config,
					o = e.itemsInRow,
					i = e.css,
					r = e.gap,
					s = 0,
					t = t.reduce(function (t, e, i) {
						return 0 === s && t.push([]), t[t.length - 1].push(n._renderItem(e, i)), s = (s + 1) % o, t
					}, []);
				return h.el("", d(d(d(d({}, this._handlers), {
					dhx_widget_id: this._uid,
					class: (i || "") + " dhx_widget dhx_dataview" + (this.config.multiselection && this.selection.getItem() ? " dhx_no-select--pointer" : ""),
					style: {
						height: this.config.height
					}
				}), this.getDataViewAriaAttrs(this.config, this.data.getLength(), t.length, o)), {
					tabindex: 0
				}), t.map(function (t, e) {
					return h.el(".dhx_dataview-row", {
						style: {
							margin: r
						},
						"aria-label": "Row " + (e + 1)
					}, t)
				}))
			}, l.prototype._getHotkeys = function () {
				var e = this,
					t = r.prototype._getHotkeys.call(this);
				return t.arrowUp = function (t) {
					e.moveFocus(a.MOVE_UP, e.config.itemsInRow), t.preventDefault()
				}, t.arrowDown = function (t) {
					e.moveFocus(a.MOVE_DOWN, e.config.itemsInRow), t.preventDefault()
				}, t.arrowLeft = function (t) {
					e.moveFocus(a.MOVE_UP), t.preventDefault()
				}, t.arrowRight = function (t) {
					e.moveFocus(a.MOVE_DOWN), t.preventDefault()
				}, t
			}, l.prototype.getDataViewItemAriaAttrs = function (t, e) {
				var i, n, o;
				return d(d({
					role: "option",
					"aria-selected": e.$selected ? "true" : "false"
				}, (o = e, (n = t).config.dragMode && !n._edited ? {
					"aria-grabbed": Boolean(o.$dragtarget && !n._edited).toString()
				} : {})), (i = t).config.editable ? {
					"aria-roledescription": i._edited ? "Press Enter to stop editing" : "Double click to edit content"
				} : {})
			}, l.prototype.getDataViewAriaAttrs = function (t, e, i, n) {
				return {
					role: "listbox",
					"aria-label": "Dataview, " + e + " options on " + i + " rows, " + n + " options per row." + (t.editable ? " Content is editable." : ""),
					"aria-multiselectable": t.selection && t.multiselection ? "true" : "false",
					"aria-readonly": t.editable ? "false" : "true"
				}
			}, l);

		function l(t, e) {
			return void 0 === e && (e = {}), r.call(this, t, s.extend({
				itemsInRow: 1,
				gap: "0px"
			}, e)) || this
		}
		e.DataView = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.DataViewEvents || (e.DataViewEvents = {})).click = "click", e.doubleClick = "doubleclick", e.focusChange = "focuschange", e.beforeEditStart = "beforeEditStart", e.afterEditStart = "afterEditStart", e.beforeEditEnd = "beforeEditEnd", e.afterEditEnd = "afterEditEnd", e.itemRightClick = "itemRightClick", e.itemMouseOver = "itemMouseOver", e.contextmenu = "contextmenu"
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(94)), n(e(228)), n(e(5));
		e = e(5);
		i.FormEvents = e.FormEvents
	}, function (t, d, h) {
		"use strict";
		(function (t) {
			var n, e = this && this.__extends || (n = function (t, e) {
				return (n = Object.setPrototypeOf || {
					__proto__: []
				}
					instanceof Array && function (t, e) {
						t.__proto__ = e
					} || function (t, e) {
						for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
					})(t, e)
			}, function (t, e) {
				function i() {
					this.constructor = t
				}
				n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
			}),
				i = this && this.__assign || function () {
					return (i = Object.assign || function (t) {
						for (var e, i = 1, n = arguments.length; i < n; i++)
							for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
						return t
					}).apply(this, arguments)
				},
				E = this && this.__rest || function (t, e) {
					var i = {};
					for (o in t) Object.prototype.hasOwnProperty.call(t, o) && e.indexOf(o) < 0 && (i[o] = t[o]);
					if (null != t && "function" == typeof Object.getOwnPropertySymbols)
						for (var n = 0, o = Object.getOwnPropertySymbols(t); n < o.length; n++) e.indexOf(o[n]) < 0 && Object.prototype.propertyIsEnumerable.call(t, o[n]) && (i[o[n]] = t[o[n]]);
					return i
				},
				C = this && this.__spreadArrays || function () {
					for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
					for (var n = Array(t), o = 0, e = 0; e < i; e++)
						for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
					return n
				};
			Object.defineProperty(d, "__esModule", {
				value: !0
			});
			var o, r = h(3),
				s = h(4),
				a = h(11),
				k = h(1),
				S = h(95),
				I = h(96),
				P = h(57),
				O = h(97),
				M = h(37),
				V = h(98),
				D = h(99),
				T = h(100),
				H = h(101),
				F = h(102),
				j = h(103),
				l = h(8),
				L = h(104),
				R = h(106),
				A = h(107),
				$ = h(108),
				N = h(5),
				c = h(0),
				e = (o = s.View, e(u, o), u.prototype.paint = function () {
					o.prototype.paint.call(this), this.layout.paint()
				}, u.prototype.send = function (n, o, r) {
					var s = this;
					if (void 0 === o && (o = "POST"), this.events.fire(N.FormEvents.beforeSend)) return new t(function (t, e) {
						var i = new XMLHttpRequest;
						switch (i.onload = function () {
							200 <= i.status && i.status < 300 ? t(i.response || i.responseText) : e({
								status: i.status,
								statusText: i.statusText
							})
						}, i.onloadend = function () {
							i.readyState === XMLHttpRequest.DONE && 200 === i.status && s.events.fire(N.FormEvents.afterSend)
						}, i.onerror = function () {
							e({
								status: i.status,
								statusText: i.statusText
							})
						}, "GET" === o && (n += "?" + encodeURIComponent(JSON.stringify(s.getValue()))), i.open(o, n), r || i.setRequestHeader("Content-Type", "application/json"), o) {
							case "POST":
								s._send(), i.send(r ? s.getValue(!0) : JSON.stringify(s.getValue()));
								break;
							case "DELETE":
							case "PUT":
								i.send(r ? s.getValue(!0) : JSON.stringify(s.getValue()));
								break;
							case "GET":
							default:
								i.send()
						}
					})
				}, u.prototype.clear = function (t) {
					switch (t) {
						case N.ClearMethod.value:
							this._clear();
							break;
						case N.ClearMethod.validation:
							this._clearValidate();
							break;
						default:
							this._clear(), this._clearValidate()
					}
					this.paint()
				}, u.prototype.setValue = function (t) {
					for (var e in t)
						for (var i in this._attachments) "function" == typeof this._attachments[i].setValue && this._attachments[i].config.name === e && this._attachments[i].setValue(t[e])
				}, u.prototype.getValue = function (t) {
					if (t) {
						var e, n = new FormData,
							o = this;
						for (e in this._state) ! function (i) {
							Array.isArray(o._state[i]) ? o._state[i].forEach(function (t, e) {
								return n.append(i + "[" + e + "]", t)
							}) : n.append(i, o._state[i])
						}(e);
						return n
					}
					return i({}, this._state)
				}, u.prototype.getItem = function (t) {
					for (var e in this._attachments)
						if (e == t) return this._attachments[e]
				}, u.prototype.validate = function (t) {
					for (var e in void 0 === t && (t = !1), this._isValid = !0, this._attachments) "function" == typeof this._attachments[e].validate && l.isVerify(this._attachments[e].config) && !this._attachments[e].validate(t) && (this._isValid = !1, t || this.events.fire(N.FormEvents.validationFail, [e, this._attachments[e]]));
					return this._isValid
				}, u.prototype.setProperties = function (t, e) {
					if ("string" == typeof t && e && !k.isEmptyObj(e))
						for (var i in this._attachments) "function" == typeof this._attachments[i].setProperties && i === t && this._attachments[i].setProperties(e);
					if ("object" == typeof t && !k.isEmptyObj(t))
						for (var i in this._attachments) "function" != typeof this._attachments[i].setProperties || k.isEmptyObj(t[i]) || this._attachments[i].setProperties(t[i])
				}, u.prototype.getProperties = function (t) {
					if (t)
						for (var e in this._attachments)
							if ("function" == typeof this._attachments[e].getProperties && e === t) return this._attachments[e].getProperties();
					var i = {};
					for (e in this._attachments) "function" == typeof this._attachments[e].getProperties && (i[e] = this._attachments[e].getProperties());
					return i
				}, u.prototype.show = function () {
					if (this.config.hidden || Object.values(this._attachments).some(function (t) {
						return !t.config.hidden
					}))
						for (var t in this._formContainerShow(), this._attachments) "function" == typeof this._attachments[t].show && this._attachments[t].show()
				}, u.prototype.hide = function (t) {
					if (!this.config.hidden || t)
						for (var e in this._formContainerHide(), this._attachments) "function" == typeof this._attachments[e].hide && this._attachments[e].hide(t)
				}, u.prototype.setFocus = function (t) {
					for (var e in this._attachments) {
						var i = this._attachments[e];
						"radiogroup" !== i.config.type && "checkboxgroup" !== i.config.type || i.focus(t), i.config.name === t && i.focus()
					}
				}, u.prototype.blur = function (t) {
					for (var e in t || this.forEach(function (t) {
						"function" == typeof t.blur && t.blur()
					}), this._attachments) {
						var i = this._attachments[e];
						"radiogroup" !== i.config.type && "checkboxgroup" !== i.config.type || i.blur(t), i.config.name === t && i.blur()
					}
				}, u.prototype.isVisible = function (t) {
					if (!t) return !this.config.hidden;
					for (var e in this._attachments)
						if (e === t) return !this._attachments[e].config.hidden
				}, u.prototype.disable = function () {
					for (var t in this.config.disabled = !0, this._attachments) "function" == typeof this._attachments[t].disable && this._attachments[t].disable()
				}, u.prototype.enable = function () {
					for (var t in this.config.disabled = !1, this._attachments) "function" == typeof this._attachments[t].enable && this._attachments[t].enable()
				}, u.prototype.isDisabled = function (t) {
					if (!t) return this.config.disabled;
					for (var e in this._attachments)
						if (e === t) return this._attachments[e].config.disabled
				}, u.prototype.forEach = function (t) {
					for (var e = Object.values(this._attachments), i = 0; i < e.length; i++) t.call(this, e[i], i, e)
				}, u.prototype.destructor = function () {
					this.events && this.events.clear(), this.layout && this.layout.destructor(), this.config = this._attachments = this._state = this._uid = this.container = this.events = this._isValid = null, this.unmount()
				}, u.prototype.getRootView = function () {
					return this.layout.getRootView()
				}, u.prototype._addLayoutItem = function (t) {
					var i = this,
						e = t.id = t.id || k.uid(),
						n = t.name = t.name || e.toString();
					t.type = t.type && t.type.toLowerCase();
					var o = t.width,
						r = t.height,
						s = t.css,
						e = t.padding,
						a = E(t, ["css", "padding"]),
						l = s ? s + " dhx_form-element" : "dhx_form-element",
						s = !("spacer" === t.type || void 0 === t.type);
					switch (s && !o && (o = "content"), s && !r && (r = "content"), a.type) {
						case "button":
							a.full && (l += " dhx_button--full-gravity");
							var c = this._attachments[n] = new I.Button(null, a);
							c.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), c.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), c.events.on(N.ItemEvent.click, function (t) {
								t.preventDefault(), i.events.fire(N.FormEvents.click, [n, t]), i.events.fire(N.FormEvents.buttonClick, [n, t]), c.config.submit && i.validate() && c.config.url && i.send(c.config.url)
							}), c.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), c.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), c.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), c.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), c.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), c.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), c.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "datepicker":
							var u = this._attachments[n] = new S.DatePicker(null, a);
							this._state[n] = u.getValue("Date" === a.valueFormat), u.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), u.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = u.getValue("Date" === a.valueFormat), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), u.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), u.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), u.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), u.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), u.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), u.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), u.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), u.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), u.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), u.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "checkbox":
							var d = this._attachments[n] = new P.Checkbox(null, a);
							this._state[n] = d.getValue(), d.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), d.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = d.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), d.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), d.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), d.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), d.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), d.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), d.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), d.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), d.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), d.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), d.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "checkboxgroup":
							var h = this._attachments[n] = new O.CheckboxGroup(null, a);
							this._state[n] = h.getValue(), h.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), h.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = h.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), h.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), h.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), h.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), h.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), h.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), h.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), h.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), h.events.on(N.ItemEvent.focus, function (t, e) {
								i.events.fire(N.FormEvents.focus, [n, t, e])
							}), h.events.on(N.ItemEvent.blur, function (t, e) {
								i.events.fire(N.FormEvents.blur, [n, t, e])
							}), h.events.on(N.ItemEvent.keydown, function (t, e) {
								i.events.fire(N.FormEvents.keydown, [t, n, e])
							});
							break;
						case "combo":
							var f = this._attachments[n] = new F.Combo(null, a);
							this._state[n] = f.getValue(), f.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), f.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = f.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), f.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), f.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), f.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), f.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), f.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), f.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), f.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), f.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), f.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), f.events.on(N.ItemEvent.keydown, function (t, e) {
								i.events.fire(N.FormEvents.keydown, [t, n, e])
							});
							break;
						case "input":
							var p = this._attachments[n] = new M.Input(null, a);
							this._state[n] = p.getValue(), p.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), p.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = p.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), p.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), p.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), p.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), p.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), p.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), p.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), p.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), p.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), p.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), p.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "radiogroup":
							var _ = this._attachments[n] = new V.RadioGroup(null, a);
							this._state[n] = _.getValue(), _.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), _.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = _.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), _.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), _.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), _.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), _.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), _.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), _.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), _.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), _.events.on(N.ItemEvent.focus, function (t, e) {
								i.events.fire(N.FormEvents.focus, [n, t, e])
							}), _.events.on(N.ItemEvent.blur, function (t, e) {
								i.events.fire(N.FormEvents.blur, [n, t, e])
							}), _.events.on(N.ItemEvent.keydown, function (t, e) {
								i.events.fire(N.FormEvents.keydown, [t, n, e])
							});
							break;
						case "select":
							var v = this._attachments[n] = new D.Select(null, a);
							this._state[n] = v.getValue(), v.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), v.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = v.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), v.events.on(N.ItemEvent.changeOptions, function (t) {
								i.layout.getCell(n).config.options = C(t)
							}), v.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), v.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), v.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), v.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), v.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), v.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), v.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), v.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), v.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), v.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "simplevault":
							a.$vaultHeight = r;
							var g = this._attachments[n] = new L.SimpleVault(null, a);
							this._state[n] = g.getValue(), g.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), g.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = g.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), g.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), g.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), g.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), g.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), g.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), g.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), g.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							});
							break;
						case "slider":
							var m = this._attachments[n] = new j.SliderForm(null, a);
							this._state[n] = m.getValue(), m.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), m.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), m.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), m.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), m.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), m.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), m.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), m.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "textarea":
							var y = this._attachments[n] = new T.Textarea(null, a);
							this._state[n] = y.getValue(), y.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), y.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = y.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), y.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), y.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), y.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), y.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), y.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), y.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), y.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), y.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), y.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), y.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "text":
							var b = this._attachments[n] = new H.Text(null, a);
							this._state[n] = b.getValue(), b.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), b.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = b.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), b.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), b.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), b.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), b.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), b.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), b.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), b.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							});
							break;
						case "timepicker":
							var w = this._attachments[n] = new R.TimePicker(null, a);
							this._state[n] = a.value && w.getValue("timeObject" === a.valueFormat) || "", w.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), w.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = w.getValue("timeObject" === a.valueFormat), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), w.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), w.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), w.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), w.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), w.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), w.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), w.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), w.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), w.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), w.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "colorpicker":
							var x = this._attachments[n] = new A.ColorPicker(null, a);
							this._state[n] = x.getValue(), x.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), x.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i._state[n] = x.getValue(), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), x.events.on(N.ItemEvent.change, function (t) {
								i._state[n] = t, i.events.fire(N.FormEvents.change, [n, t])
							}), x.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), x.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), x.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), x.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							}), x.events.on(N.ItemEvent.beforeValidate, function (t) {
								return i.events.fire(N.FormEvents.beforeValidate, [n, t])
							}), x.events.on(N.ItemEvent.afterValidate, function (t, e) {
								i.events.fire(N.FormEvents.afterValidate, [n, t, e])
							}), x.events.on(N.ItemEvent.focus, function (t) {
								i.events.fire(N.FormEvents.focus, [n, t])
							}), x.events.on(N.ItemEvent.blur, function (t) {
								i.events.fire(N.FormEvents.blur, [n, t])
							}), x.events.on(N.ItemEvent.keydown, function (t) {
								i.events.fire(N.FormEvents.keydown, [t, n])
							});
							break;
						case "spacer":
						default:
							m = this._attachments[n] = new $.Spacer(null, a);
							m.events.on(N.ItemEvent.beforeChangeProperties, function (t) {
								return i.events.fire(N.FormEvents.beforeChangeProperties, [n, t])
							}), m.events.on(N.ItemEvent.afterChangeProperties, function (t) {
								i._changeProps(n, t), i.events.fire(N.FormEvents.afterChangeProperties, [n, t]), i.paint()
							}), m.events.on(N.ItemEvent.beforeHide, function (t, e) {
								if (!e) return i.events.fire(N.FormEvents.beforeHide, [n, t])
							}), m.events.on(N.ItemEvent.beforeShow, function (t) {
								return i.events.fire(N.FormEvents.beforeShow, [n, t])
							}), m.events.on(N.ItemEvent.afterHide, function (t, e) {
								i.layout.getCell(n).hide(), e || i.events.fire(N.FormEvents.afterHide, [n, t])
							}), m.events.on(N.ItemEvent.afterShow, function (t) {
								i.layout.getCell(n).show(), i.events.fire(N.FormEvents.afterShow, [n, t])
							})
					}
					e = {
						id: n,
						width: o,
						height: r,
						padding: e,
						css: l
					};
					return "gravity" in t && (e.gravity = t.gravity), e
				}, u.prototype._changeProps = function (t, e) {
					var i, n = ["width", "height", "css", "padding"];
					for (i in e) n.includes(i) && (this.layout.getCell(t).config[i] = e[i])
				}, u.prototype._addLayoutItems = function (t) {
					var i = this;
					return t.map(function (t) {
						if (l.isBlock(t)) {
							var e = {
								width: "content",
								height: "content"
							};
							return i._createLayoutConfig(t, e), e
						}
						return i._addLayoutItem(t)
					})
				}, u.prototype._checkLayoutConfig = function (t, e) {
					return k.isDefined(t.css) && (e.css = t.css), k.isDefined(t.title) && (e.header = t.title), k.isDefined(t.padding) && (e.padding = t.padding), k.isDefined(t.gravity) && (e.gravity = t.gravity), k.isDefined(t.width) && (e.width = t.width), k.isDefined(t.height) && (e.height = t.height), k.isDefined(t.align) && (e.align = t.align), e
				}, u.prototype._createLayoutConfig = function (t, e) {
					e = this._checkLayoutConfig(t, e), k.isDefined(t.rows) ? e.rows = this._addLayoutItems(t.rows) : k.isDefined(t.cols) && (e.cols = this._addLayoutItems(t.cols))
				}, u.prototype._initUI = function (t) {
					var e = this._attachments = {},
						i = {
							padding: "8px"
						};
					this.config.css += " dhx_form", this._createLayoutConfig(this.config, i);
					var n, o = this.layout = new a.Layout(t, i);
					for (n in e) o.getCell(n).attach(e[n])
				}, u.prototype._initHandlers = function () {
					var t = this;
					this.events.on(N.FormEvents.afterShow, function () {
						t._formContainerShow()
					}), this.events.on(N.FormEvents.afterHide, function () {
						Object.values(t._attachments).some(function (t) {
							return !t.config.hidden
						}) || t._formContainerHide()
					})
				}, u.prototype._clear = function () {
					for (var t in this._state = {}, this._attachments) {
						var e = this._attachments[t].config.name;
						"function" == typeof this._attachments[t].clear && (this._attachments[t].clear(), e ? this._state[e] = this._attachments[t].getValue() : this._state[t] = this._attachments[t].getValue())
					}
				}, u.prototype._clearValidate = function () {
					for (var t in this._attachments) "function" == typeof this._attachments[t].clearValidate && this._attachments[t].clearValidate()
				}, u.prototype._formContainerShow = function () {
					this.config.hidden = !1, this.getRootView().node && (this.getRootView().node.el.style.display = "flex")
				}, u.prototype._formContainerHide = function () {
					this.config.hidden = !0, this.getRootView().node && (this.getRootView().node.el.style.display = "none")
				}, u.prototype._send = function () {
					for (var t in this._attachments) "function" == typeof this._attachments[t].send && this._attachments[t].send()
				}, u);

			function u(t, e) {
				var i = o.call(this, null, k.extend({
					disabled: !1,
					hidden: !1
				}, e)) || this;
				return i._isValid = !0, i._state = {}, i.events = new r.EventSystem(i), i.container = t, i._initUI(t), i.config.hidden && i.hide(!0), i.config.disabled && i.disable(), i.events.on(N.FormEvents.afterShow, function () {
					i._formContainerShow()
				}), i.events.on(N.FormEvents.afterHide, function () {
					Object.values(i._attachments).some(function (t) {
						return !t.config.hidden
					}) || i._formContainerHide()
				}), i.events.on(N.FormEvents.change, function () {
					return i.paint()
				}), c.awaitRedraw().then(function () {
					var t = i.layout.getRootNode();
					t && (t.setAttribute("role", "form"), i.config.title && t.setAttribute("aria-label", i.config.title))
				}), i
			}
			d.Form = e
		}).call(this, h(15))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(34),
			l = i(23),
			c = i(3),
			d = i(0),
			u = i(1),
			h = i(14),
			f = i(12),
			p = i(5),
			_ = i(8),
			v = i(13),
			o = (s = h.Label, o(g, s), g.prototype.destructor = function () {
				this.events && this.events.clear(), this.calendar && this.calendar.destructor(), this._popup && this._popup.destructor(), this.events = this._uid = this._propsCalendar = this._propsItem = this._props = this._isValid = null, s.prototype._destructor.call(this), this.unmount()
			}, g.prototype.setProperties = function (t) {
				if (t && !u.isEmptyObj(t) && this.events.fire(p.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e], this._propsCalendar.includes(e) && (this.calendar.config[e] = t[e]));
					this.events.fire(p.ItemEvent.afterChangeProperties, [this.getProperties()]), this.calendar.paint(), this.paint()
				}
			}, g.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, g.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(p.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(p.ItemEvent.afterShow, [e]))
			}, g.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(p.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(p.ItemEvent.afterHide, [i, t]))
			}, g.prototype.isVisible = function () {
				return !this.config.hidden
			}, g.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, g.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, g.prototype.isDisabled = function () {
				return this.config.disabled
			}, g.prototype.validate = function (t, e) {
				void 0 === t && (t = !1);
				var i = void 0 === e ? this.getValue() : e,
					e = "Date" === this.config.valueFormat ? i instanceof Date : l.stringToDate(i, this.calendar.config.dateFormat, !0);
				if (t || this.events.fire(p.ItemEvent.beforeValidate, [i])) return this._isValid = this.config.validation ? this.config.validation(i) : !!e, t || this.events.fire(p.ItemEvent.afterValidate, [i, this._isValid]), this._isValid
			}, g.prototype.clearValidate = function () {
				this.config.$validationStatus = p.ValidationStatus.pre, this.paint()
			}, g.prototype.setValue = function (t) {
				void 0 !== t && t !== this.config.value && (this.calendar.setValue(t), _.isVerify(this.config) && this.validate())
			}, g.prototype.getValue = function (t) {
				var e = this.config,
					i = e.value,
					e = e.valueFormat;
				return (i instanceof Date || !t || "" === i) && (i instanceof Date || "Date" !== e || "" === i) ? i || "" : l.stringToDate(i, this.calendar.config.dateFormat) || ""
			}, g.prototype.focus = function () {
				var t = this;
				d.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.focus()
				})
			}, g.prototype.blur = function () {
				var t = this;
				d.awaitRedraw().then(function () {
					t._popup.hide(), t.getRootView().refs.input.el.blur()
				})
			}, g.prototype.clear = function () {
				"" !== this.config.value && (this.config.value = "", this.calendar.clear(), this.events.fire(p.ItemEvent.change, [this.getValue()]))
			}, g.prototype.getWidget = function () {
				return this.calendar
			}, g.prototype._initView = function (t) {
				var e, i = this;
				if (u.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.calendar && this.calendar.destructor(), this._popup && this._popup.destructor(), this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					editable: !1,
					hidden: !1,
					value: "",
					mode: "calendar",
					mark: void 0,
					disabledDates: void 0,
					weekStart: "sunday",
					weekNumbers: !1,
					timePicker: !1,
					dateFormat: "%d/%m/%y",
					timeFormat: 24,
					thisMonthOnly: !1,
					valueFormat: "string",
					required: !1,
					validation: void 0,
					icon: "",
					placeholder: "",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this._popup = new f.Popup({
					css: "dhx_widget--border-shadow"
				}), this.calendar = new a.Calendar(null, _.widgetConfig(t)), this._popup.attach(this.calendar), this.config.hidden && d.awaitRedraw().then(function () {
					i.hide(!0)
				})
			}, g.prototype._initHandlers = function () {
				var n = this;
				this.calendar.events.on(a.CalendarEvents.change, function () {
					var t = n.calendar.getValue("Date" === n.config.valueFormat);
					n.config.value !== t && (n.config.value = t, n.events.fire(p.ItemEvent.change, [t]), n._popup.hide(), n.paint())
				}), this.events.on(p.ItemEvent.afterValidate, function () {
					n.config.$validationStatus = n._isValid ? p.ValidationStatus.success : p.ValidationStatus.error, n.paint()
				}), this.events.on(p.ItemEvent.blur, function () {
					n._popupIsFocus = !1, n.paint()
				}), this._popup.events.on(f.PopupEvents.afterHide, function () {
					document.activeElement !== n.getRootView().refs.input.el && n.events.fire(p.ItemEvent.blur, [n.getValue()]), n.paint()
				}), document.addEventListener("keydown", function (t) {
					var e, i = document.activeElement;
					(null === (e = null === (e = null === (e = n.getRootView()) || void 0 === e ? void 0 : e.refs) || void 0 === e ? void 0 : e.input) || void 0 === e ? void 0 : e.el) !== i && !n._popup.isVisible() || n.events.fire(p.ItemEvent.keydown, [t])
				}), document.addEventListener("mousedown", function () {
					n._popup.getContainer() !== document.activeElement && (n._popupIsFocus = !1)
				})
			}, g.prototype._getHandlers = function () {
				var e = this;
				return {
					onblur: function () {
						e._popup.isVisible() || e.events.fire(p.ItemEvent.blur, [e.getValue()]), e.paint()
					},
					onfocus: function () {
						var t;
						e._popup.isVisible() || (t = e.getRootView().refs.input.el, e._popup.show(t), e.events.fire(p.ItemEvent.focus, [e.getValue()]), e.paint())
					},
					oninput: function (t) {
						t = t.target.value;
						e.events.fire(p.ItemEvent.input, [t])
					},
					onchange: function (t) {
						t = t.target.value;
						e.config.editable && l.stringToDate(t, e.calendar.config.dateFormat, !0) ? e.setValue(t) : ("" === t && e.clear(), _.isVerify(e.config) && e.validate(), e.paint())
					}
				}
			}, g.prototype._initHotkeys = function () {
				var t, i = this,
					e = {
						"shift+tab": function () {
							i._applyTab()
						},
						tab: function () {
							i._applyTab()
						},
						escape: function () {
							i._popup.isVisible() && i.getRootView().refs.input.el.focus(), i._popup.hide()
						},
						enter: function () {
							var t;
							i._popup.isVisible() || (t = i.getRootView().refs.input.el, i._popup.show(t))
						},
						arrowRight: function (t) {
							var e = i._popup.getContainer();
							i._popup.isVisible() && !i._popupIsFocus && (t.preventDefault(), e.focus(), i._popupIsFocus = !0)
						}
					};
				for (t in e) this._keyManager.addHotKey(t, e[t])
			}, g.prototype._draw = function () {
				var t = this.config,
					e = t.icon,
					i = t.required,
					n = t.disabled,
					o = t.placeholder,
					r = t.name,
					s = t.id,
					a = t.editable,
					l = t.label,
					c = t.helpMessage,
					t = this.config.value instanceof Date ? this.calendar.getValue() : this.config.value,
					u = this._popup.isVisible() || (null === (u = null === (u = null === (u = this.getRootView()) || void 0 === u ? void 0 : u.refs) || void 0 === u ? void 0 : u.input) || void 0 === u ? void 0 : u.el) === document.activeElement;
				return d.el("div.dhx_form-group", {
					class: _.getFormItemCss(this.config, _.isVerify(this.config), u)
				}, [this._drawLabel(), d.el(".dhx_input__wrapper", [d.el("div.dhx_input__container", {}, [d.el(".dhx_input__icon", {
					class: e || "dxi dxi-calendar-today"
				}), d.el("input.dhx_input.dhx_input--icon-padding", {
					tabindex: 0,
					dhx_id: r || s,
					_key: this._uid,
					_ref: "input",
					value: t,
					type: "text",
					required: i,
					disabled: n,
					class: u && "dhx_input--focus",
					placeholder: o || "",
					name: r || "",
					id: s || this._uid,
					onfocus: this._handlers.onfocus,
					oninput: this._handlers.oninput,
					onchange: this._handlers.onchange,
					onblur: this._handlers.onblur,
					autocomplete: "off",
					readOnly: !a,
					"aria-label": l || (a ? "type or" : "") + " select date",
					"aria-describedby": c ? "dhx_label__help_" + (s || this._uid) : null
				})]), _.getValidationMessage(this.config) && d.el("span.dhx_input__caption", {}, _.getValidationMessage(this.config))])])
			}, g.prototype._applyTab = function () {
				var t;
				document.activeElement === (null === (t = null === (t = null === (t = this._popup.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.content) || void 0 === t ? void 0 : t.el) || this._popupIsFocus || this._popup.hide()
			}, g);

		function g(t, e) {
			var i = s.call(this, null, e) || this;
			i.events = new c.EventSystem, i._isValid = !0, i._popupIsFocus = !1, i._propsItem = ["required", "validation", "valueFormat", "icon", "placeholder", "editable", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], i._propsCalendar = ["mode", "mark", "disabledDates", "weekStart", "weekNumbers", "timePicker", "dateFormat", "timeFormat", "thisMonthOnly"], i._props = r(_.baseProps, i._propsItem, i._propsCalendar), i._keyManager = new v.KeyManager(function () {
				var t, e = document.activeElement;
				return (null === (t = null === (t = null === (t = i.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.input) || void 0 === t ? void 0 : t.el) === e || i._popup.isVisible()
			}), i._initView(e), i._initHandlers(), i._initHotkeys();
			return i.mount(t, d.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.DatePicker = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, p = i(0),
			l = i(4),
			c = i(5),
			u = i(3),
			d = i(1),
			h = i(8),
			o = (a = l.View, o(f, a), f.prototype.destructor = function () {
				this.events && this.events.clear(), this.config = this._propsItem = this._props = this.events = this._handlers = this._uid = null, this.unmount()
			}, f.prototype.setProperties = function (t) {
				if (t && !d.isEmptyObj(t) && this.events.fire(c.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
					this.config.text = this.config.text || this.config.value, this.events.fire(c.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, f.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, f.prototype.show = function () {
				var t = this.config,
					e = t.text;
				t.hidden && this.events.fire(c.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(c.ItemEvent.afterShow, [e]))
			}, f.prototype.hide = function (t) {
				var e = this.config,
					i = e.text;
				e.hidden && !t || !this.events.fire(c.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(c.ItemEvent.afterHide, [i, t]))
			}, f.prototype.isVisible = function () {
				return !this.config.hidden
			}, f.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, f.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, f.prototype.isDisabled = function () {
				return this.config.disabled
			}, f.prototype.focus = function () {
				var t = this;
				p.awaitRedraw().then(function () {
					t.getRootView().refs.button.el.focus()
				})
			}, f.prototype.blur = function () {
				var t = this;
				p.awaitRedraw().then(function () {
					t.getRootView().refs.button.el.blur()
				})
			}, f.prototype._draw = function () {
				var t = this.config,
					e = t.color,
					i = t.size,
					n = t.view,
					o = t.full,
					r = t.loading,
					s = t.circle,
					a = t.icon,
					l = t.text,
					c = t.disabled,
					u = t.submit,
					d = t.id,
					h = t.name,
					f = {
						danger: " dhx_button--color_danger",
						secondary: " dhx_button--color_secondary",
						primary: " dhx_button--color_primary",
						success: " dhx_button--color_success"
					}[e] || " dhx_button--color_primary",
					t = {
						small: " dhx_button--size_small",
						medium: " dhx_button--size_medium"
					}[i] || " dhx_button--size_medium",
					e = {
						flat: " dhx_button--view_flat",
						link: " dhx_button--view_link"
					}[n] || " dhx_button--view_flat",
					i = o ? " dhx_button--width_full" : "",
					n = s ? " dhx_button--circle" : "",
					o = r ? " dhx_button--loading" : "",
					s = a && !l ? " dhx_button--icon" : "";
				return p.el("button", {
					disabled: c,
					id: d,
					dhx_id: h || d,
					onclick: this._handlers.onclick,
					onfocus: this._handlers.onfocus,
					onblur: this._handlers.onblur,
					onkeydown: this._handlers.onkeydown,
					type: u ? "submit" : "button",
					class: "dhx_button" + f + t + e + i + n + o + s,
					_ref: "button"
				}, [a && p.el("span.dhx_button__icon", {
					class: a
				}), l && p.el("span.dhx_button__text", l), r && p.el("span.dhx_button__loading", [p.el("span.dhx_button__loading-icon.dxi.dxi-loading")])])
			}, f);

		function f(t, e) {
			var i = a.call(this, t, r({
				disabled: !1,
				hidden: !1,
				submit: !1,
				full: !1,
				circle: !1,
				loading: !1,
				view: "flat",
				size: "medium",
				color: "primary",
				width: "content",
				height: "content",
				padding: 0,
				url: "",
				text: "",
				icon: ""
			}, e)) || this;
			i._propsItem = ["submit", "url", "text", "icon", "view", "size", "color", "full", "circle", "loading"], i._props = s(h.baseProps, i._propsItem), i.config.text = i.config.text || i.config.value || "", i.events = new u.EventSystem, i._handlers = {
				onclick: function (t) {
					return i.events.fire(c.ItemEvent.click, [t])
				},
				onblur: function () {
					return i.events.fire(c.ItemEvent.blur, [i.config.text])
				},
				onfocus: function () {
					return i.events.fire(c.ItemEvent.focus, [i.config.text])
				},
				onkeydown: function (t) {
					return i.events.fire(c.ItemEvent.keydown, [t])
				}
			}, i.config.hidden && p.awaitRedraw().then(function () {
				i.hide(!0)
			});
			return i.mount(t, p.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Button = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(0),
			c = i(11),
			u = i(3),
			d = i(57),
			h = i(14),
			f = i(5),
			p = i(1),
			_ = i(8),
			v = i(12),
			o = (a = h.Label, o(g, a), g.prototype.destructor = function () {
				this.events && this.events.clear(), this.layout && this.layout.destructor(), this.events = this._uid = this._propsItem = this._props = this._isValid = this._buttons = null, a.prototype._destructor.call(this), this.unmount()
			}, g.prototype.setProperties = function (e, t) {
				if (void 0 !== e && this.events.fire(f.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					if ("object" == typeof e && !p.isEmptyObj(e)) {
						for (var i in e) this._props.includes(i) && (this.config[i] = e[i]);
						e.hasOwnProperty("options") && (this._initView(this.config), this._initHandlers())
					}
					var n;
					"string" != typeof e || !t || p.isEmptyObj(t) || (n = this._buttons.find(function (t) {
						return t.config.id === e
					})) && n.setProperties(t, !0), this.events.fire(f.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, g.prototype.getProperties = function (e) {
				if (void 0 !== e) return this._buttons.find(function (t) {
					return t.config.id === e
				}).getProperties();
				var t, i = {};
				for (t in this.config) this._props.includes(t) && (i[t] = this.config[t]);
				return i
			}, g.prototype.getValue = function (e) {
				if (void 0 === e) {
					var i = {};
					return this._buttons.forEach(function (t) {
						i[t.config.id] = t.getValue()
					}), i
				}
				var t = this._buttons.find(function (t) {
					return t.config.id === e
				});
				if (e && t) return t.getValue()
			}, g.prototype.setValue = function (t) {
				if (void 0 !== t && !p.isEmptyObj(t)) {
					for (var n = !1, o = this, e = 0, i = Object.entries(t); e < i.length; e++) {
						var r = i[e];
						! function (e, t) {
							var i = o._buttons.find(function (t) {
								return t.config.id === e
							});
							i && (i.setValue(!!t, !0), n = !0)
						}(r[0], r[1])
					}
					n && (this.events.fire(f.ItemEvent.change, [this.getValue()]), _.isVerify(this.config) && this.validate())
				}
			}, g.prototype.isChecked = function (e) {
				if (void 0 === e) {
					var i = {};
					return this._buttons.forEach(function (t) {
						i[t.config.id] = t.isChecked()
					}), i
				}
				var t = this._buttons.find(function (t) {
					return t.config.id === e
				});
				if (e && t) return t.isChecked()
			}, g.prototype.focus = function (e) {
				var i = this;
				l.awaitRedraw().then(function () {
					if (i._buttons.length) {
						if (!e) return i._buttons[0].focus();
						var t = i._buttons.find(function (t) {
							return t.config.id === e
						});
						t && t.focus()
					}
				})
			}, g.prototype.blur = function () {
				var t = this;
				l.awaitRedraw().then(function () {
					t._buttons.length && t._buttons.forEach(function (t) {
						t.blur()
					})
				})
			}, g.prototype.show = function () {
				this.config.hidden && this.events.fire(f.ItemEvent.beforeShow, [this.getValue()]) && (this.config.hidden = !1, this.events.fire(f.ItemEvent.afterShow, [this.getValue()]))
			}, g.prototype.hide = function (t) {
				this.config.hidden && !t || !this.events.fire(f.ItemEvent.beforeHide, [this.getValue(), t]) || (this.config.hidden = !0, this.events.fire(f.ItemEvent.afterHide, [this.getValue(), t]))
			}, g.prototype.isVisible = function () {
				return !this.config.hidden
			}, g.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, g.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, g.prototype.isDisabled = function () {
				return this.config.disabled
			}, g.prototype.clear = function () {
				this._buttons.some(function (t) {
					return t.isChecked()
				}) && (this._buttons.forEach(function (t) {
					t.clear(!0)
				}), this.events.fire(f.ItemEvent.change, [this.getValue()]))
			}, g.prototype.validate = function (t) {
				var e = this;
				if (void 0 === t && (t = !1), t || this.events.fire(f.ItemEvent.beforeValidate, [this.getValue()])) return this.config.required && (this._isValid = this._buttons.some(function (t) {
					return t.config.$required && !!t.config.checked
				})), t || (this._buttons.forEach(function (t) {
					t.config.$validationStatus = e._isValid ? f.ValidationStatus.success : f.ValidationStatus.error
				}), this.config.$validationStatus = this._isValid ? f.ValidationStatus.success : f.ValidationStatus.error, this.events.fire(f.ItemEvent.afterValidate, [this.getValue(), this._isValid])), this._isValid
			}, g.prototype.clearValidate = function () {
				this.config.$validationStatus = f.ValidationStatus.pre, this._buttons.forEach(function (t) {
					t.clearValidate()
				}), this.paint()
			}, g.prototype._initView = function (i) {
				var t, n = this;
				if (p.isEmptyObj(i) || !i.options || p.isEmptyObj(i.options)) throw new Error("Check the configuration is correct");
				for (t in this.layout && this.layout.destructor(), this._buttons.length && (this._buttons.forEach(function (t) {
					t.destructor()
				}), this._buttons = []), this.config = {
					type: i.type,
					id: i.id,
					name: i.name,
					disabled: !1,
					hidden: !1,
					options: {},
					required: !1,
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, i) "id" !== t && "type" !== t && "name" !== t && (this.config[t] = i[t]);
				this.config.helpMessage && (this._helper = new v.Popup({
					css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
				}), this._helper.attachHTML(this.config.helpMessage));
				var e = this.config.options.rows || this.config.options.cols;
				e.forEach(function (t) {
					t.id = t.id || p.uid(), t.$group = !0
				}), this.layout = new c.Layout(null, this.config.options), e.forEach(function (t) {
					var e = new d.Checkbox(null, r(r({}, t), {
						disabled: i.disabled,
						name: i.name,
						$required: i.required
					}));
					n._buttons.push(e), n.layout.getCell(t.id).attach(e), e.events.on(f.ItemEvent.change, function () {
						n.events.fire(f.ItemEvent.change, [n.getValue()]), _.isVerify(n.config) && n.validate()
					}), e.events.on(f.ItemEvent.focus, function (t, e) {
						n.events.fire(f.ItemEvent.focus, [n.getValue(), e])
					}), e.events.on(f.ItemEvent.blur, function (t, e) {
						n.events.fire(f.ItemEvent.blur, [n.getValue(), e])
					}), e.events.on(f.ItemEvent.keydown, function (t, e) {
						n.events.fire(f.ItemEvent.keydown, [t, e])
					})
				}), this.config.value && this.setValue(this.config.value), this.clearValidate(), this.config.hidden && l.awaitRedraw().then(function () {
					n.hide(!0)
				})
			}, g.prototype._initHandlers = function () {
				var t = this;
				this.events.on(f.ItemEvent.change, function () {
					t.config.value = t.getValue(), t.paint()
				}), this.events.on(f.ItemEvent.afterValidate, function () {
					t.config.$validationStatus = t._isValid ? f.ValidationStatus.success : f.ValidationStatus.error, t.paint()
				})
			}, g.prototype._draw = function () {
				var t = this.config.hidden ? " dhx_form-group--hidden" : "",
					e = this.config,
					i = e.label,
					n = e.labelWidth,
					o = e.helpMessage,
					r = e.required,
					s = e.name,
					e = e.id;
				return l.el("fieldset.dhx_form-group-fieldset", {}, [l.el("div.dhx_form-group.dhx_form-group--checkbox-group", {
					class: _.getFormItemCss(this.config, _.isVerify(this.config)) + t,
					dhx_id: s || e,
					role: "radiogroup"
				}, [i || n || o || r ? this._drawLabel() : null, l.el("div.dhx_checkbox-group--container", {}, [this.layout && l.inject(this.layout.getRootView()), r && _.getValidationMessage(this.config) && l.el("span.dhx_input__caption", _.getValidationMessage(this.config))])])])
			}, g);

		function g(t, e) {
			var i = a.call(this, null, e) || this;
			i.events = new u.EventSystem, i._buttons = [], i._isValid = !0, i._propsItem = ["required", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage", "options"], i._props = s(_.baseProps, i._propsItem), i._initView(e), i._initHandlers();
			return i.mount(t, l.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.CheckboxGroup = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(0),
			c = i(1),
			u = i(11),
			d = i(12),
			h = i(3),
			f = i(227),
			p = i(8),
			_ = i(14),
			v = i(5),
			o = (a = _.Label, o(g, a), g.prototype.destructor = function () {
				this.events && this.events.clear(), this.layout && this.layout.destructor(), this.events = this._uid = this._propsItem = this._props = this._isValid = this._buttons = null, a.prototype._destructor.call(this), this.unmount()
			}, g.prototype.setProperties = function (e, t) {
				if (void 0 !== e && this.events.fire(v.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					if ("object" == typeof e && !c.isEmptyObj(e)) {
						for (var i in e) this._props.includes(i) && (this.config[i] = e[i]);
						e.hasOwnProperty("options") && (this._initView(this.config), this._initHandlers())
					}
					var n;
					"string" != typeof e || !t || c.isEmptyObj(t) || (n = this._buttons.find(function (t) {
						return t.config.id === e
					})) && n.setProperties(t), this.events.fire(v.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, g.prototype.getProperties = function (e) {
				if (void 0 !== e) return this._buttons.find(function (t) {
					return t.config.id === e
				}).getProperties();
				var t, i = {};
				for (t in this.config) this._props.includes(t) && (i[t] = this.config[t]);
				return i
			}, g.prototype.getValue = function () {
				var e = this;
				return this._buttons.forEach(function (t) {
					t.getValue() && (e.config.value = t.getValue() || "")
				}), this.config.value || ""
			}, g.prototype.setValue = function (t) {
				t !== this.config.value && (this._setValue(t), this.events.fire(v.ItemEvent.change, [t]), p.isVerify(this.config) && this.validate())
			}, g.prototype.focus = function (e) {
				var i = this;
				l.awaitRedraw().then(function () {
					if (i._buttons.length) {
						if (!e) return i._buttons[0].focus();
						var t = i._buttons.find(function (t) {
							return t.config.id === e
						});
						return t ? t.focus() : void 0
					}
				})
			}, g.prototype.blur = function () {
				var t = this;
				l.awaitRedraw().then(function () {
					t._buttons.length && t._buttons.forEach(function (t) {
						return t.blur()
					})
				})
			}, g.prototype.show = function () {
				this.config.hidden && this.events.fire(v.ItemEvent.beforeShow, [this.getValue()]) && (this.config.hidden = !1, this.events.fire(v.ItemEvent.afterShow, [this.getValue()]))
			}, g.prototype.hide = function (t) {
				this.config.hidden && !t || !this.events.fire(v.ItemEvent.beforeHide, [this.getValue(), t]) || (this.config.hidden = !0, this.events.fire(v.ItemEvent.afterHide, [this.getValue(), t]))
			}, g.prototype.isVisible = function () {
				return !this.config.hidden
			}, g.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, g.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, g.prototype.isDisabled = function () {
				return this.config.disabled
			}, g.prototype.clear = function () {
				"" !== this.config.value && (this._buttons.forEach(function (t) {
					t.clear()
				}), this.config.value = "", this.events.fire(v.ItemEvent.change, [this.config.value]))
			}, g.prototype.validate = function (t) {
				var e = this;
				if (void 0 === t && (t = !1), t || this.events.fire(v.ItemEvent.beforeValidate, [this.getValue()])) return this.config.required && (this._isValid = this._buttons.some(function (t) {
					return e.config.required && !!t.config.checked
				})), t || (this._buttons.forEach(function (t) {
					t.config.$validationStatus = e._isValid ? v.ValidationStatus.success : v.ValidationStatus.error
				}), this.config.$validationStatus = this._isValid ? v.ValidationStatus.success : v.ValidationStatus.error, this.events.fire(v.ItemEvent.afterValidate, [this.getValue(), this._isValid])), this._isValid
			}, g.prototype.clearValidate = function () {
				this.config.$validationStatus = v.ValidationStatus.pre, this._buttons.map(function (t) {
					t.clearValidate()
				}), this.paint()
			}, g.prototype._initView = function (i) {
				var t, n = this;
				if (c.isEmptyObj(i) || !i.options || c.isEmptyObj(i.options)) throw new Error("Check the configuration is correct");
				for (t in this.layout && this.layout.destructor(), this._buttons.length && (this._buttons.forEach(function (t) {
					t.destructor()
				}), this._buttons = []), this.config = {
					type: i.type,
					id: i.id,
					name: i.name,
					disabled: !1,
					hidden: !1,
					options: {},
					required: !1,
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, i) "id" !== t && "type" !== t && "name" !== t && (this.config[t] = i[t]);
				this.config.hidden && l.awaitRedraw().then(function () {
					n.hide(!0)
				});
				var e = this.config.options.rows || this.config.options.cols;
				e.map(function (t) {
					t.id = t.id || c.uid()
				}), this.layout = new u.Layout(null, this.config.options), e.map(function (t) {
					var e = new f.RadioButton(null, r(r({}, t), {
						$disabled: i.disabled,
						$name: i.name,
						$required: i.required
					}));
					n._buttons.push(e), n.layout.getCell(t.id).attach(e), e.events.on(f.RadioButtonEvents.change, function () {
						n._buttons.map(function (t) {
							t.config.id !== e.config.id && t.setValue(!1)
						}), n.events.fire(v.ItemEvent.change, [n.getValue()]), p.isVerify(n.config) && n.validate()
					}), e.events.on(f.RadioButtonEvents.focus, function (t, e) {
						n.events.fire(v.ItemEvent.focus, [t, e])
					}), e.events.on(f.RadioButtonEvents.blur, function (t, e) {
						n.events.fire(v.ItemEvent.blur, [t, e])
					}), e.events.on(f.RadioButtonEvents.keydown, function (t, e) {
						n.events.fire(v.ItemEvent.keydown, [t, e])
					})
				})
			}, g.prototype._initHandlers = function () {
				var t = this;
				this.events.on(v.ItemEvent.change, function () {
					t.config.value = t.getValue(), t.paint()
				}), this.events.on(v.ItemEvent.afterValidate, function () {
					t.config.$validationStatus = t._isValid ? v.ValidationStatus.success : v.ValidationStatus.error, t.paint()
				})
			}, g.prototype._draw = function () {
				var t = this.config,
					e = t.label,
					i = t.labelWidth,
					n = t.helpMessage,
					o = t.required,
					r = t.hidden,
					s = t.name,
					t = t.id,
					r = r ? " dhx_form-group--hidden" : "";
				return l.el("fieldset.dhx_form-group-fieldset", {}, [l.el("div.dhx_form-group.dhx_form-group--radio-group", {
					class: p.getFormItemCss(this.config, p.isVerify(this.config)) + r,
					dhx_id: s || t
				}, [e || i || n || o ? [this._drawLabel()] : null, l.el("div.dhx_radio-group--container", {}, [this.layout && l.inject(this.layout.getRootView()), o && p.getValidationMessage(this.config) && l.el("span.dhx_input__caption", p.getValidationMessage(this.config))])])])
			}, g.prototype._setValue = function (e) {
				void 0 !== e && -1 !== this._buttons.findIndex(function (t) {
					return t.config.value === e
				}) && this._buttons.forEach(function (t) {
					e === t.config.value ? t.setValue(!0) : t.setValue(!1)
				})
			}, g);

		function g(t, e) {
			var i = a.call(this, null, e) || this;
			i.events = new h.EventSystem, i._buttons = [], i._isValid = !0, i._propsItem = ["required", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage", "options"], i._props = s(p.baseProps, i._propsItem), i._initView(e), i._initHandlers(), i.config.value && i._setValue(i.config.value), i.clearValidate(), i.config.helpMessage && (i._helper = new d.Popup({
				css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
			}), i._helper.attachHTML(i.config.helpMessage));
			return i.mount(t, l.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.RadioGroup = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, u = i(0),
			a = i(1),
			l = i(14),
			d = i(8),
			c = i(3),
			h = i(5),
			o = (s = l.Label, o(f, s), f.prototype.destructor = function () {
				this.events && this.events.clear(), this.events = this._uid = this._propsItem = this._props = this._isValid = null, s.prototype._destructor.call(this), this.unmount()
			}, f.prototype.setProperties = function (t) {
				if (t && !a.isEmptyObj(t) && this.events.fire(h.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
					this.events.fire(h.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, f.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, f.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(h.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(h.ItemEvent.afterShow, [e]))
			}, f.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(h.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(h.ItemEvent.afterHide, [i, t]))
			}, f.prototype.isVisible = function () {
				return !this.config.hidden
			}, f.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, f.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, f.prototype.isDisabled = function () {
				return this.config.disabled
			}, f.prototype.validate = function (t) {
				void 0 === t && (t = !1);
				var e = this.config,
					i = e.value,
					e = e.validation;
				if (t || this.events.fire(h.ItemEvent.beforeValidate, [i])) return this._isValid = e ? this.config.validation(i) : void 0 !== i && !!String(i).length, t || this.events.fire(h.ItemEvent.afterValidate, [i, this._isValid]), this._isValid
			}, f.prototype.clearValidate = function () {
				this.config.$validationStatus = h.ValidationStatus.pre, this.paint()
			}, f.prototype.clear = function () {
				this.config.value !== this.config.options[0].value && (this.config.value = this.config.options[0].value, this.events.fire(h.ItemEvent.change, [this.getValue()]))
			}, f.prototype.setValue = function (e) {
				void 0 !== e && e !== this.config.value && -1 !== this.config.options.findIndex(function (t) {
					return t.value === e
				}) && (this.config.value = e, this.events.fire(h.ItemEvent.change, [e]), d.isVerify(this.config) && this.validate())
			}, f.prototype.focus = function () {
				var t = this;
				u.awaitRedraw().then(function () {
					t.getRootView().refs.select.el.focus()
				})
			}, f.prototype.blur = function () {
				var t = this;
				u.awaitRedraw().then(function () {
					t.getRootView().refs.select.el.blur()
				})
			}, f.prototype.getValue = function () {
				return this.config.value
			}, f.prototype.setOptions = function (t) {
				if (!t || !t.length) throw new Error("Function argument cannot be empty, for more info check documentation https://docs.dhtmlx.com/suite/form__select.html#addingselect");
				this._checkOptions(t), this.config.options = r(t), this.config.value = this.config.options[0].value, this.events.fire(h.ItemEvent.changeOptions, [r(t)]), this.paint()
			}, f.prototype.getOptions = function () {
				return this.config.options
			}, f.prototype._initView = function (t) {
				var e, i = this;
				if (a.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this._checkOptions(t.options), this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					hidden: !1,
					required: !1,
					validation: void 0,
					icon: "",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0,
					options: t.options,
					value: t.options[0].value
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this.config.hidden && u.awaitRedraw().then(function () {
					i.hide(!0)
				}), this.paint()
			}, f.prototype._initHandlers = function () {
				var t = this;
				this.events.on(h.ItemEvent.afterValidate, function () {
					t.config.$validationStatus = t._isValid ? h.ValidationStatus.success : h.ValidationStatus.error, t.paint()
				}), this.events.on(h.ItemEvent.changeOptions, function () {
					t.events.fire(h.ItemEvent.change, [t.getValue()]), d.isVerify(t.config) && t.validate()
				}), this.events.on(h.ItemEvent.change, function () {
					return t.paint()
				})
			}, f.prototype._getHandlers = function () {
				var i = this;
				return {
					onchange: function (e) {
						var t = i.config.options.map(function (t) {
							return t.value
						}).find(function (t) {
							return t == e.target.value
						});
						i.config.value = t, i.events.fire(h.ItemEvent.change, [t]), d.isVerify(i.config) && i.validate()
					},
					onblur: function () {
						i.events.fire(h.ItemEvent.blur, [i.getValue()]), i.paint()
					},
					onfocus: function () {
						i.events.fire(h.ItemEvent.focus, [i.getValue()]), i.paint()
					}
				}
			}, f.prototype._draw = function () {
				var t = this.config,
					e = t.id,
					i = t.options,
					n = t.icon,
					o = t.value,
					r = t.label,
					s = t.labelWidth,
					a = t.helpMessage,
					l = t.required,
					c = t.name,
					t = (null === (t = null === (t = null === (t = this.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.select) || void 0 === t ? void 0 : t.el) === document.activeElement;
				return u.el(".dhx_form-group", {
					class: d.getFormItemCss(this.config, d.isVerify(this.config), t)
				}, [(r || s || a) && this._drawLabel(), u.el(".dhx_input__wrapper", {}, [u.el("div.dhx_input__container", {}, [u.el(".dhx_input__icon", {
					class: n || "dxi dxi-menu-down"
				}), u.el("select.dhx_select.dhx_input", {
					id: e,
					dhx_id: c || e,
					tabindex: 0,
					class: t && "dhx_input--focus",
					onchange: this._handlers.onchange,
					onfocus: this._handlers.onfocus,
					onblur: this._handlers.onblur,
					onkeydown: this._handlers.onkeydown,
					_ref: "select",
					required: l
				}, i.length && i.map(function (t) {
					return u.el("option", {
						value: t.value,
						disabled: t.disabled,
						selected: o === t.value
					}, t.content)
				}))]), d.getValidationMessage(this.config) && u.el("span.dhx_input__caption", d.getValidationMessage(this.config))])])
			}, f.prototype._checkOptions = function (t) {
				if (0 === t.length) throw new Error("Property options* cannot be empty, for more info check documentation https://docs.dhtmlx.com/suite/form__select.html#addingselect");
				t.forEach(function (t) {
					if (!t.hasOwnProperty("value") || !t.hasOwnProperty("content")) throw new Error("The object must contain two required properties value and content, for more info check documentation https://docs.dhtmlx.com/suite/form__select.html#addingselect")
				})
			}, f);

		function f(t, e) {
			var i = s.call(this, null, e) || this;
			return i.events = new c.EventSystem, i._isValid = !0, i._propsItem = ["required", "validation", "icon", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], i._props = r(d.baseProps, i._propsItem), i._initView(e), i._initHandlers(), i
		}
		e.Select = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, p = i(0),
			_ = i(8),
			a = i(37),
			l = i(5),
			v = i(1),
			o = (s = a.Input, o(c, s), c.prototype.setValue = function (t) {
				void 0 !== t && this.config.value !== t && (this.config.value = t, this.events.fire(l.ItemEvent.change, [this.getValue()]), _.isVerify(this.config) && this.validate())
			}, c.prototype.getValue = function () {
				return void 0 === this.config.value ? "" : String(this.config.value)
			}, c.prototype.focus = function () {
				var t = this;
				p.awaitRedraw().then(function () {
					t.getRootView().refs.textarea.el.focus()
				})
			}, c.prototype.blur = function () {
				var t = this;
				p.awaitRedraw().then(function () {
					t.getRootView().refs.textarea.el.blur()
				})
			}, c.prototype.setProperties = function (t) {
				if (t && !v.isEmptyObj(t) && this.events.fire(l.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
					this.events.fire(l.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, c.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, c.prototype._initView = function (t) {
				var e, i = this;
				if (v.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					hidden: !1,
					required: !1,
					validation: void 0,
					maxlength: void 0,
					minlength: void 0,
					placeholder: "",
					readOnly: !1,
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this.config.hidden && p.awaitRedraw().then(function () {
					i.hide(!0)
				}), this.paint()
			}, c.prototype._draw = function () {
				var t = this.config,
					e = t.id,
					i = t.value,
					n = t.disabled,
					o = t.name,
					r = t.placeholder,
					s = t.required,
					a = t.resizable,
					l = t.readOnly,
					c = t.maxlength,
					u = t.minlength,
					d = t.label,
					h = t.helpMessage,
					f = t.height,
					t = (null === (t = null === (t = null === (t = this.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.textarea) || void 0 === t ? void 0 : t.el) === document.activeElement;
				return p.el("div.dhx_form-group.dhx_form-group--textarea", {
					class: _.getFormItemCss(this.config, _.isVerify(this.config), t)
				}, [this._drawLabel(), p.el(".dhx_input__wrapper", [p.el("textarea.dhx_input.dhx_input--textarea", {
					type: "text",
					id: e,
					dhx_id: o || e,
					placeholder: r || "",
					value: v.isDefined(i) ? i : "",
					name: o || "",
					disabled: n,
					required: s,
					readOnly: l,
					maxlength: c,
					minlength: u,
					onblur: this._handlers.onblur,
					oninput: this._handlers.oninput,
					onchange: this._handlers.onchange,
					onfocus: this._handlers.onfocus,
					onkeydown: this._handlers.onkeydown,
					_hooks: {
						didInsert: function (t) {
							"content" === f && ((t = t.el).style.height = t.scrollHeight + "px")
						}
					},
					style: {
						resize: a ? "both" : "none"
					},
					_ref: "textarea",
					"aria-label": d || (l ? i : "") + " type text",
					"aria-describedby": h ? "dhx_label__help_" + (e || this._uid) : null
				}), _.getValidationMessage(this.config) && p.el("span.dhx_input__caption", {}, _.getValidationMessage(this.config))])])
			}, c);

		function c() {
			var t = null !== s && s.apply(this, arguments) || this;
			return t._propsItem = ["required", "validation", "placeholder", "readOnly", "maxlength", "minlength", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], t._props = r(_.baseProps, t._propsItem), t
		}
		e.Textarea = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(1),
			c = i(8),
			u = i(37),
			d = i(5),
			o = (s = u.Input, o(h, s), h.prototype.setProperties = function (t) {
				if (t && !l.isEmptyObj(t) && this.events.fire(d.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
					this.events.fire(d.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, h.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, h.prototype._initView = function (t) {
				var e, i = this;
				if (l.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					hidden: !1,
					value: "",
					inputType: "text",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this.config.hidden && a.awaitRedraw().then(function () {
					i.hide(!0)
				}), this.paint()
			}, h.prototype._draw = function () {
				var t = this.config,
					e = t.id,
					i = t.value,
					n = t.name,
					o = t.inputType,
					t = t.label;
				return a.el("div.dhx_form-group.dhx_form-group--textinput", {
					class: c.getFormItemCss(this.config)
				}, [this._drawLabel(), a.el(".dhx_input__wrapper", [a.el("input.dhx_input.dhx_input--textinput", {
					type: ["text", "number", "password"].includes(o) ? o : "text",
					readOnly: !0,
					id: e,
					dhx_id: n || e,
					value: l.isDefined(i) ? i : "",
					name: n,
					_ref: "input",
					tabindex: -1,
					role: "presentation",
					"aria-label": t || i
				})])])
			}, h);

		function h() {
			var t = null !== s && s.apply(this, arguments) || this;
			return t._propsItem = ["inputType", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage"], t._props = r(c.baseProps, t._propsItem), t
		}
		e.Text = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(1),
			c = i(8),
			u = i(24),
			d = i(3),
			h = i(14),
			f = i(5),
			o = (s = h.Label, o(p, s), p.prototype.destructor = function () {
				this.events && this.events.clear(), this.combobox && this.combobox.destructor(), this.events = this._uid = this._propsCombo = this._propsItem = this._props = this._isValid = null, s.prototype._destructor.call(this), this.unmount()
			}, p.prototype.setProperties = function (e) {
				var i = this;
				a.awaitRedraw().then(function () {
					if (e && !l.isEmptyObj(e) && i.events.fire(f.ItemEvent.beforeChangeProperties, [i.getProperties()])) {
						for (var t in e) i._props.includes(t) && (i.config[t] = e[t]);
						i._initView(i.config), i._initHandlers(), i.events.fire(f.ItemEvent.afterChangeProperties, [i.getProperties()]), i.combobox.paint(), i.paint()
					}
				})
			}, p.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, p.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(f.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(f.ItemEvent.afterShow, [e]))
			}, p.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(f.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(f.ItemEvent.afterHide, [i, t]))
			}, p.prototype.isVisible = function () {
				return !this.config.hidden
			}, p.prototype.disable = function () {
				this.config.disabled = !0, this.combobox.disable(), this.paint()
			}, p.prototype.enable = function () {
				this.config.disabled = !1, this.combobox.enable(), this.paint()
			}, p.prototype.isDisabled = function () {
				return this.config.disabled
			}, p.prototype.clear = function () {
				this.config.value && this.combobox.clear()
			}, p.prototype.getValue = function () {
				return this.config.multiselection ? this.combobox.getValue(!0) || [""] : this.combobox.getValue() || ""
			}, p.prototype.setValue = function (t) {
				void 0 !== t && t !== this.config.value && t && this.combobox.setValue(t)
			}, p.prototype.validate = function (t, e) {
				void 0 === t && (t = !1);
				e = void 0 === e ? this.getValue() : e;
				if (t || this.events.fire(f.ItemEvent.beforeValidate, [e])) return this._isValid = this.config.validation ? this.config.validation(e) : ("string" == typeof e || e instanceof Array) && this._exsistData(e), t || (this.config.$validationStatus = this._isValid ? f.ValidationStatus.success : f.ValidationStatus.error, this.config.required && this._validationStatus(), this.events.fire(f.ItemEvent.afterValidate, [e, this._isValid])), this._isValid
			}, p.prototype.clearValidate = function () {
				this.config.$validationStatus = f.ValidationStatus.pre, this._validationStatus(), this.paint()
			}, p.prototype.getWidget = function () {
				return this.combobox
			}, p.prototype.focus = function () {
				var t = this;
				a.awaitRedraw().then(function () {
					t.combobox.focus()
				})
			}, p.prototype.blur = function () {
				var t = this;
				a.awaitRedraw().then(function () {
					t.combobox.blur()
				})
			}, p.prototype._initView = function (t) {
				var e = this;
				if (l.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				this.combobox && this.combobox.destructor(), this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					hidden: !1,
					value: "",
					readOnly: !1,
					template: void 0,
					filter: void 0,
					multiselection: !1,
					selectAllButton: !1,
					itemsCount: void 0,
					itemHeight: 32,
					virtual: !1,
					listHeight: 224,
					required: !1,
					validation: void 0,
					placeholder: "",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				};
				var i, n = {};
				for (i in t) "id" !== i && "type" !== i && "name" !== i && (this.config[i] = t[i], "validation" !== i && (n[i] = t[i]));
				this.combobox = new u.Combobox(null, n), this.config.hidden && a.awaitRedraw().then(function () {
					e.hide(!0)
				}), this.paint()
			}, p.prototype._initHandlers = function () {
				var i = this;
				this.combobox.events.on(u.ComboboxEvents.change, function () {
					var t = i.config.value = i.getValue();
					i.events.fire(f.ItemEvent.change, [t]), c.isVerify(i.config) && i.validate(), i.paint()
				}), this.combobox.events.on(u.ComboboxEvents.focus, function () {
					i.events.fire(f.ItemEvent.focus, [i.getValue()]), i.paint()
				}), this.combobox.events.on(u.ComboboxEvents.blur, function () {
					i.events.fire(f.ItemEvent.blur, [i.getValue()]), i.paint()
				}), this.combobox.events.on(u.ComboboxEvents.keydown, function (t, e) {
					"Enter" === t.code && t.preventDefault(), i.events.fire(f.ItemEvent.keydown, [t, e])
				}), this.events.on(f.ItemEvent.afterValidate, function () {
					i.config.$validationStatus = i._isValid ? f.ValidationStatus.success : f.ValidationStatus.error, i.paint()
				})
			}, p.prototype._validationStatus = function () {
				var t;
				if (!(this.combobox.popup.isVisible() || (null === (t = null === (t = null === (t = this.combobox.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.input) || void 0 === t ? void 0 : t.el) === document.activeElement)) switch (this.config.$validationStatus) {
					case f.ValidationStatus.success:
						return this.combobox.config.css = (this.config.css || "") + "dhx_form-group--state_success";
					case f.ValidationStatus.error:
						return this.combobox.config.css = (this.config.css || "") + "dhx_form-group--state_error";
					case f.ValidationStatus.pre:
					default:
						return this.combobox.config.css = this.config.css || ""
				}
				return this.combobox.config.css = this.config.css || ""
			}, p.prototype._getRootView = function () {
				return this.combobox.getRootView()
			}, p.prototype._draw = function () {
				var t = this.config,
					e = t.labelWidth,
					i = t.labelPosition,
					n = t.name,
					t = t.id;
				return a.el(".dhx_form-group", {
					dhx_id: n || t
				}, [a.inject(this._getRootView()), a.el("div", {
					style: {
						"margin-left": e && "left" === i ? "calc(" + e + " + 16px)" : ""
					},
					class: this._validationStatus()
				}, [a.el("span.dhx_input__caption", c.getValidationMessage(this.config))])])
			}, p.prototype._exsistData = function (t) {
				var e = this;
				return t instanceof Array ? !!t.length && t.every(function (t) {
					return e.combobox.data.exists(t)
				}) : "string" == typeof t ? this.combobox.data.exists(t) : void 0
			}, p);

		function p(t, e) {
			var i = s.call(this, null, e) || this;
			return i.events = new d.EventSystem, i._isValid = !0, i._propsItem = ["required", "validation", "placeholder", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage", "readonly", "readOnly"], i._propsCombo = ["template", "filter", "multiselection", "selectAllButton", "itemsCount", "itemHeight", "virtual", "listHeight"], i._props = r(c.baseProps, i._propsItem, i._propsCombo), i._initView(e), i._initHandlers(), i
		}
		e.Combo = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(0),
			c = i(1),
			u = i(45),
			d = i(3),
			h = i(14),
			f = i(5),
			p = i(8),
			o = (a = h.Label, o(_, a), _.prototype.destructor = function () {
				this.events && this.events.clear(), this.slider && this.slider.destructor(), this.events = this._uid = this._propsSlider = this._propsItem = this._props = null, a.prototype._destructor.call(this), this.unmount()
			}, _.prototype.setProperties = function (t) {
				if (t && !c.isEmptyObj(t) && this.events.fire(f.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
					this._initView(this.config), this._initHandlers(), this.events.fire(f.ItemEvent.afterChangeProperties, [this.getProperties()]), this.slider.paint(), this.paint()
				}
			}, _.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, _.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(f.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(f.ItemEvent.afterShow, [e]))
			}, _.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(f.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(f.ItemEvent.afterHide, [i, t]))
			}, _.prototype.isVisible = function () {
				return !this.config.hidden
			}, _.prototype.disable = function () {
				this.config.disabled = !0, this.slider.disable(), this.paint()
			}, _.prototype.enable = function () {
				this.config.disabled = !1, this.slider.enable(), this.paint()
			}, _.prototype.isDisabled = function () {
				return this.config.disabled
			}, _.prototype.clear = function () {
				var t = this.config,
					e = t.value,
					t = t.min;
				e[0] !== t && this.slider.setValue(t)
			}, _.prototype.getValue = function () {
				return this.slider.getValue()
			}, _.prototype.setValue = function (t) {
				void 0 !== t && t !== this.config.value && this.slider.setValue(t)
			}, _.prototype.getWidget = function () {
				return this.slider
			}, _.prototype.focus = function (t) {
				var e = this;
				l.awaitRedraw().then(function () {
					e.slider.focus(t)
				})
			}, _.prototype.blur = function () {
				var t = this;
				l.awaitRedraw().then(function () {
					t.slider.blur()
				})
			}, _.prototype._initView = function (t) {
				var e = this;
				if (c.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				this.slider && this.slider.destructor();
				var i, n = {
					type: t.type,
					id: t.id,
					name: t.name,
					mode: "horizontal",
					min: 0,
					max: 100,
					step: 1,
					range: !1,
					inverse: !1,
					tick: void 0,
					tickTemplate: void 0,
					majorTick: void 0,
					tooltip: !0,
					disabled: !1,
					hidden: !1,
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					width: "content",
					height: "content",
					padding: 0
				};
				for (i in t) "id" !== i && "type" !== i && "name" !== i && (n[i] = t[i]);
				this.config = r({
					type: this.config.type
				}, n), this.slider = new u.Slider(null, n), this.config.disabled && this.slider.disable(), this.config.value = this.slider.getValue(), this.config.hidden && l.awaitRedraw().then(function () {
					e.hide(!0)
				})
			}, _.prototype._initHandlers = function () {
				var e = this;
				this.slider.events.on(u.SliderEvents.change, function () {
					var t = e.config.value = e.slider.getValue();
					e.events.fire(f.ItemEvent.change, [t]), e.paint()
				}), this.slider.events.on(u.SliderEvents.focus, function () {
					e.events.fire(f.ItemEvent.focus, [e.getValue()])
				}), this.slider.events.on(u.SliderEvents.blur, function () {
					e.events.fire(f.ItemEvent.blur, [e.getValue()])
				}), this.slider.events.on(u.SliderEvents.keydown, function (t) {
					e.events.fire(f.ItemEvent.keydown, [t])
				})
			}, _.prototype._getRootView = function () {
				return this.slider.paint(), this.slider.getRootView()
			}, _.prototype._drawSlider = function () {
				var t = this.config,
					e = t.name,
					t = t.id;
				return l.el("div.dhx_form-group", {
					dhx_id: e || t
				}, [l.inject(this._getRootView())])
			}, _);

		function _(t, e) {
			var i = a.call(this, null, e) || this;
			i.events = new d.EventSystem, i._propsItem = ["label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage"], i._propsSlider = ["min", "max", "step", "mode", "range", "inverse", "tooltip", "tick", "tickTemplate", "majorTick"], i._props = s(p.baseProps, i._propsItem, i._propsSlider), i._initView(e), i._initHandlers();
			return i.mount(t, l.create({
				render: function () {
					return i._drawSlider()
				}
			})), i
		}
		e.SliderForm = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(0),
			c = i(3),
			u = i(2),
			d = i(4),
			h = i(1),
			f = i(6),
			p = i(61),
			_ = i(12),
			v = i(8),
			g = i(105),
			m = i(5),
			o = (a = d.View, o(y, a), y.prototype.destructor = function () {
				this.events && this.events.clear(), this._helper && this._helper.destructor(), this.config = this.events = this._uid = this._helper = this._handlers = this._uploader = this.data = null, this._propsItem = this._propsSimpleVault = this._props = this._isValid = this._dragover = this._dragoverTimeout = null, this.unmount()
			}, y.prototype.setProperties = function (t) {
				if (t && !h.isEmptyObj(t) && this.events.fire(m.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e], this._propsSimpleVault.includes(e) && (this._uploader.config[e] = t[e]));
					this.events.fire(m.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, y.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, y.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(m.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(m.ItemEvent.afterShow, [e]))
			}, y.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(m.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(m.ItemEvent.afterHide, [i, t]))
			}, y.prototype.isVisible = function () {
				return !this.config.hidden
			}, y.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, y.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, y.prototype.isDisabled = function () {
				return this.config.disabled
			}, y.prototype.validate = function (t) {
				void 0 === t && (t = !1);
				var e = this.config,
					i = e.required,
					e = e.value;
				if (t || this.events.fire(m.ItemEvent.beforeValidate, [e])) return this._isValid = !i || 0 < this.data.getLength(), t || this.events.fire(m.ItemEvent.afterValidate, [e, this._isValid]), this._isValid
			}, y.prototype.clearValidate = function () {
				this.config.$validationStatus = m.ValidationStatus.pre, this.paint()
			}, y.prototype.clear = function () {
				0 !== this.getValue().length && (this.data.removeAll(), this.paint())
			}, y.prototype.getValue = function () {
				return this.data.serialize()
			}, y.prototype.selectFile = function () {
				this._uploader.selectFile()
			}, y.prototype.send = function (t) {
				v.isVerify(this.config) && !this.validate(!0) || this._uploader.send(t)
			}, y.prototype.setValue = function (t) {
				t.length && (this.data.parse(t), v.isVerify(this.config) && this.validate())
			}, y.prototype._initView = function (t) {
				var e, i = this;
				if (h.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					fieldName: t.fieldName || t.name || t.id,
					params: void 0,
					disabled: !1,
					hidden: !1,
					singleRequest: !1,
					target: "",
					value: [],
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this.config.hidden && l.awaitRedraw().then(function () {
					i.hide(!0)
				}), this.config.value.length && this.setValue(this.config.value), this.paint()
			}, y.prototype._initHandlers = function () {
				var n = this;
				this.data.events.on(m.ItemEvent.change, function (t, e) {
					var i = n.config.value = n.getValue();
					n.events.fire(m.ItemEvent.change, [i]), e && v.isVerify(n.config) && n.validate(), n.paint()
				}), this.events.on(m.ItemEvent.afterValidate, function () {
					n.config.$validationStatus = n._isValid ? m.ValidationStatus.success : m.ValidationStatus.error, n.paint()
				}), this._uploader.events.on("beforeUploadFile", function (t) {
					return n.events.fire(m.ItemEvent.beforeUploadFile, [t, n.config.value])
				}), this._uploader.events.on("uploadBegin", function (t) {
					n.events.fire(m.ItemEvent.uploadBegin, [t, n.config.value])
				}), this._uploader.events.on("uploadComplete", function (t) {
					n.events.fire(m.ItemEvent.uploadComplete, [t, n.config.value])
				}), this._uploader.events.on("uploadFail", function (t) {
					n.events.fire(m.ItemEvent.uploadFail, [t, n.config.value])
				}), this._uploader.events.on("uploadFile", function (t, e) {
					n.events.fire(m.ItemEvent.uploadFile, [t, n.config.value, e])
				}), this._uploader.events.on("uploadProgress", function (t) {
					n.events.fire(m.ItemEvent.uploadProgress, [t, n.config.value])
				})
			}, y.prototype._draw = function () {
				var e = this;
				this.config.helpMessage && (this._helper || (this._helper = new _.Popup({
					css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
				})), this._helper.attachHTML(this.config.helpMessage));
				var t = this.data.getLength() ? l.el("ul.dhx_simplevault__files.dhx_simplevault-files", {
					class: this.config.$vaultHeight ? "" : "dhx_simplevault-files__fixed"
				}, this.data.map(function (t) {
					return l.el("li.dhx_simplevault-files__item", [l.el("span.dhx_simplevault-files__item-name", t.file && t.file.name || t.name), l.el(".dhx_button.dhx_simplevault-files__delete.dhx_button--icon.dhx_button--view_link.dhx_button--size_small.dhx_button--color_secondary.dhx_button--circle", {
						dhx_id: t.id,
						onclick: e._handlers.remove
					}, [l.el("span.dxi.dxi-delete-forever")])])
				})) : null,
					i = this.config,
					n = i.id,
					o = i.helpMessage,
					r = i.disabled,
					s = i.required,
					a = i.label,
					i = u.getLabelStyle(this.config);
				return l.el(".dhx_form-group.dhx_form-group--simplevault", {
					class: v.getFormItemCss(this.config, v.isVerify(this.config))
				}, [i && l.el("legend.dhx_label", {
					class: o ? "dhx_label--with-help" : "",
					style: i.style,
					onclick: this._handlers.add,
					"aria-label": a || "select files"
				}, o ? [(i.label || s) && l.el("span.dhx_label__holder", i.label), l.el("span.dhx_label-help.dxi.dxi-help-circle-outline", {
					tabindex: "0",
					role: "button",
					onclick: this._handlers.showHelper,
					onfocus: this._handlers.showHelper,
					onblur: this._handlers.hideHelper
				})] : i.label), l.el(".dhx_input__wrapper", {
					id: this._uid
				}, [l.el("div", {
					_hooks: {
						didInsert: function (t) {
							e._uploader.linkDropArea(t.el)
						}
					},
					ondragover: this._handlers.ondragover,
					class: "dhx_simplevault" + (this._dragover ? " dhx_simplevault--on-drag" : "")
				}, [l.el("div.dhx_simplevault-loader", [l.el("span.dhx_simplevault__icon.dxi.dxi-vault")]), l.el(".dhx_simplevault__drop-area", [l.el("input.dhx_simplevault__input", {
					type: "file",
					id: n,
					disabled: r
				}), l.el("span.dhx_simplevault__icon.dxi.dxi-vault"), l.el("span.dhx_simplevault__title", [l.el("span", g.default.simpleVaultText), l.el("br"), l.el("label.dhx_simplevault__label", {
					onclick: this._handlers.add,
					for: n
				}, " " + g.default.simpleVaultLabel)])]), t]), v.getValidationMessage(this.config) && l.el("span.dhx_input__caption", {}, v.getValidationMessage(this.config))])])
			}, y);

		function y(t, e) {
			var o = a.call(this, t, e) || this;
			o._isValid = !0, o._propsItem = ["required", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], o._propsSimpleVault = ["target", "singleRequest", "fieldName", "params"], o._props = s(v.baseProps, o._propsItem, o._propsSimpleVault), o.events = new c.EventSystem(o), o.data = new f.DataCollection, o._uploader = new p.Uploader(r(r({}, e), {
				autosend: !1,
				fieldName: e.fieldName || e.name || e.id
			}), o.data, o.data.events), o._initView(e), o._initHandlers(), o._handlers = {
				add: function (t) {
					o.config.disabled || (t.preventDefault(), o._uploader.selectFile())
				},
				remove: function (t) {
					o.config.disabled || (t = u.locate(t)) && o.data.remove(t)
				},
				ondragover: function (t) {
					for (var e = 0, i = t.dataTransfer.types; e < i.length; e++) {
						var n = i[e];
						if ("Files" !== n && "application/x-moz-file" !== n) return
					}
					o._dragoverTimeout ? clearTimeout(o._dragoverTimeout) : o.paint(), o._dragover = !0, o._dragoverTimeout = setTimeout(function () {
						o._dragover = !1, o._dragoverTimeout = null, o.paint()
					}, 150)
				},
				showHelper: function (t) {
					t.stopPropagation(), t.preventDefault(), o._helper.show(t.target, {
						mode: "left" === o.config.labelPosition ? "bottom" : "right"
					})
				},
				hideHelper: function (t) {
					t.preventDefault(), t.stopPropagation(), o._helper.hide()
				}
			};
			return o.mount(t, l.create({
				render: function () {
					return o._draw()
				}
			})), o
		}
		e.SimpleVault = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.default = {
			simpleVaultText: "Drag & drop files or folders here or",
			simpleVaultLabel: "browse files"
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(43),
			u = i(0),
			l = i(1),
			c = i(14),
			d = i(12),
			h = i(3),
			f = i(5),
			p = i(8),
			_ = i(13),
			o = (s = c.Label, o(v, s), v.prototype.destructor = function () {
				this.events && this.events.clear(), this.timepicker && this.timepicker.destructor(), this._popup && this._popup.destructor(), this.events = this._uid = this._propsItem = this._propsTimepicker = this._props = this._isValid = null, s.prototype._destructor.call(this), this.unmount()
			}, v.prototype.setProperties = function (e) {
				var i = this;
				u.awaitRedraw().then(function () {
					if (e && !l.isEmptyObj(e) && i.events.fire(f.ItemEvent.beforeChangeProperties, [i.getProperties()])) {
						for (var t in e) i._props.includes(t) && (i.config[t] = e[t]);
						i._initView(i.config), i._initHandlers(), i.events.fire(f.ItemEvent.afterChangeProperties, [i.getProperties()]), i.paint()
					}
				})
			}, v.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, v.prototype.show = function () {
				this.config.hidden && this.events.fire(f.ItemEvent.beforeShow, [this.getValue("timeObject" === this.config.valueFormat)]) && (this.config.hidden = !1, this.events.fire(f.ItemEvent.afterShow, [this.getValue("timeObject" === this.config.valueFormat)]))
			}, v.prototype.hide = function (t) {
				this.config.hidden && !t || !this.events.fire(f.ItemEvent.beforeHide, [this.getValue("timeObject" === this.config.valueFormat), t]) || (this.config.hidden = !0, this.events.fire(f.ItemEvent.afterHide, [this.getValue("timeObject" === this.config.valueFormat), t]))
			}, v.prototype.isVisible = function () {
				return !this.config.hidden
			}, v.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, v.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, v.prototype.isDisabled = function () {
				return this.config.disabled
			}, v.prototype.validate = function (t, e) {
				void 0 === t && (t = !1);
				e = void 0 === e ? this.getValue("timeObject" === this.config.valueFormat) : e;
				if (t || this.events.fire(f.ItemEvent.beforeValidate, [e])) return this._isValid = this.config.validation ? this.config.validation(e) : p.isTimeFormat(this.getValue(), this.config.timeFormat), t || this.events.fire(f.ItemEvent.afterValidate, [e, this._isValid]), this._isValid
			}, v.prototype.clearValidate = function () {
				this.config.$validationStatus = f.ValidationStatus.pre, this.paint()
			}, v.prototype.setValue = function (t) {
				void 0 !== t && t !== this.config.value && (this.timepicker.setValue(t), this.config.controls && (this.config.value = this.timepicker.getValue(), this.events.fire(f.ItemEvent.change, [this.getValue("timeObject" === this.config.valueFormat)]), p.isVerify(this.config) && this.validate(), this.paint()))
			}, v.prototype.getValue = function (t) {
				return t ? this.timepicker.getValue(t) : this.config.value || ""
			}, v.prototype.focus = function () {
				var t = this;
				u.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.focus()
				})
			}, v.prototype.blur = function () {
				var t = this;
				u.awaitRedraw().then(function () {
					t._popup.hide(), t.getRootView().refs.input.el.blur()
				})
			}, v.prototype.clear = function () {
				"" !== this.config.value && this._clear()
			}, v.prototype.getWidget = function () {
				return this.timepicker
			}, v.prototype._initView = function (t) {
				var e, i = this;
				if (l.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.timepicker && this.timepicker.destructor(), this._popup && this._popup.destructor(), this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					editable: !1,
					hidden: !1,
					timeFormat: 24,
					controls: !1,
					valueFormat: "string",
					required: !1,
					validation: void 0,
					icon: "",
					placeholder: "",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this._popup = new d.Popup({
					css: "dhx_widget--border-shadow"
				}), this.timepicker = new a.Timepicker(null, p.widgetConfig(t)), this._popup.attach(this.timepicker), this.config.hasOwnProperty("value") && (this.config.value = this.timepicker.getValue()), this.config.hidden && u.awaitRedraw().then(function () {
					i.hide(!0)
				})
			}, v.prototype._initHandlers = function () {
				var n = this;
				this.events.on(f.ItemEvent.afterValidate, function () {
					n.config.$validationStatus = n._isValid ? f.ValidationStatus.success : f.ValidationStatus.error, n.paint()
				}), this.config.controls ? (this.timepicker.events.on(a.TimepickerEvents.afterClose, function () {
					n._popup.hide()
				}), this.timepicker.events.on(a.TimepickerEvents.afterApply, function () {
					n._afterApply()
				}), this._popup.events.on(d.PopupEvents.afterHide, function () {
					n.config.value && n.config.value !== n.timepicker.getValue() ? n.timepicker.setValue(n.config.value) : "" !== n.config.value && void 0 !== n.config.value || n._clear(!0)
				})) : this.timepicker.events.on(a.TimepickerEvents.change, function () {
					n.config.value = n.timepicker.getValue(), n.events.fire(f.ItemEvent.change, [n.getValue("timeObject" === n.config.valueFormat)]), n.paint()
				}), this._popup.events.on(d.PopupEvents.afterHide, function () {
					document.activeElement !== n.getRootView().refs.input.el && n.events.fire(f.ItemEvent.blur, [n.getValue()]), n.paint()
				}), this.events.on(f.ItemEvent.input, function (t) {
					var e = 12 === n.config.timeFormat ? 7 : 5;
					t.length >= e && p.isTimeFormat(t, n.config.timeFormat) ? n.timepicker.setValue(t) : "" !== t || n.config.controls || n.clear()
				}), this.events.on(f.ItemEvent.afterChangeProperties, function () {
					n.config.value = n.timepicker.getValue(), n.paint()
				}), this.events.on(f.ItemEvent.blur, function () {
					n._popupIsFocus = !1, n.paint()
				}), document.addEventListener("keydown", function (t) {
					var e, i = document.activeElement;
					(null === (e = null === (e = null === (e = n.getRootView()) || void 0 === e ? void 0 : e.refs) || void 0 === e ? void 0 : e.input) || void 0 === e ? void 0 : e.el) !== i && !n._popup.isVisible() || n.events.fire(f.ItemEvent.keydown, [t])
				}), document.addEventListener("mousedown", function () {
					n._popup.getContainer() !== document.activeElement && (n._popupIsFocus = !1)
				})
			}, v.prototype._getHandlers = function () {
				var e = this;
				return {
					onfocus: function () {
						var t;
						e._popup.isVisible() || (t = e.getRootView().refs.input.el, e._popup.show(t), e.events.fire(f.ItemEvent.focus, [e.getValue()]), e.paint())
					},
					onblur: function () {
						e._popup.isVisible() || e.events.fire(f.ItemEvent.blur, [e.getValue()]), e.paint()
					},
					oninput: function (t) {
						t = t.target.value;
						e.events.fire(f.ItemEvent.input, [t])
					}
				}
			}, v.prototype._initHotkeys = function () {
				var t, i = this,
					e = {
						"shift+tab": function () {
							i._applyTab()
						},
						tab: function () {
							i._applyTab()
						},
						escape: function () {
							i._popup.isVisible() && i.getRootView().refs.input.el.focus(), i._popup.hide()
						},
						enter: function () {
							var t = i.getRootView().refs.input.el;
							i._popup.isVisible() ? i._popupIsFocus && i.config.controls || (i.config.value && "" === t.value ? i.clear() : t.value !== i.config.value && (i.config.value = i.timepicker.getValue(), i.events.fire(f.ItemEvent.change, [i.getValue("timeObject" === i.config.valueFormat)])), p.isVerify(i.config) && i.validate(), i._popup.hide()) : i._popup.show(t)
						},
						arrowRight: function (t) {
							var e = i._popup.getContainer();
							i._popup.isVisible() && !i._popupIsFocus && (t.preventDefault(), e.focus(), i._popupIsFocus = !0)
						}
					};
				for (t in e) this._keyManager.addHotKey(t, e[t])
			}, v.prototype._draw = function () {
				var t = this.config,
					e = t.value,
					i = t.required,
					n = t.disabled,
					o = t.placeholder,
					r = t.name,
					s = t.id,
					a = t.editable,
					l = t.label,
					t = t.helpMessage,
					c = this._popup.isVisible() || (null === (c = null === (c = null === (c = this.getRootView()) || void 0 === c ? void 0 : c.refs) || void 0 === c ? void 0 : c.input) || void 0 === c ? void 0 : c.el) === document.activeElement;
				return u.el("div.dhx_form-group", {
					class: p.getFormItemCss(this.config, p.isVerify(this.config), c)
				}, [this._drawLabel(), u.el(".dhx_input__wrapper", [u.el("div.dhx_input__container", {}, [u.el(".dhx_input__icon.dxi.dxi-clock-outline"), u.el("input.dhx_input.dhx_input--icon-padding", {
					tabindex: 0,
					dhx_id: r || s,
					_key: this._uid,
					_ref: "input",
					value: e,
					type: "text",
					required: i,
					disabled: n,
					class: c && "dhx_input--focus",
					placeholder: o || "",
					name: r || "",
					id: s || this._uid,
					onfocus: this._handlers.onfocus,
					onblur: this._handlers.onblur,
					oninput: this._handlers.oninput,
					autocomplete: "off",
					readOnly: !a,
					"aria-label": l || (a ? "type or" : "") + " select date",
					"aria-describedby": t ? "dhx_label__help_" + (s || this._uid) : null
				})]), p.getValidationMessage(this.config) && u.el("span.dhx_input__caption", {}, p.getValidationMessage(this.config))])])
			}, v.prototype._clear = function (t) {
				this.timepicker.clear(), this.config.value = "", t || this.events.fire(f.ItemEvent.change, [this.config.value])
			}, v.prototype._afterApply = function () {
				var t = this.getRootView().refs.input.el.value;
				this.config.value && "" === t ? this.clear() : (this.config.value = this.timepicker.getValue(), this.events.fire(f.ItemEvent.change, [this.getValue("timeObject" === this.config.valueFormat)])), p.isVerify(this.config) && this.validate(), this._popup.hide(), this.paint()
			}, v.prototype._applyTab = function () {
				var t;
				document.activeElement === (null === (t = null === (t = null === (t = this._popup.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.content) || void 0 === t ? void 0 : t.el) || this._popupIsFocus || this._popup.hide()
			}, v);

		function v(t, e) {
			var i = s.call(this, null, e) || this;
			i.events = new h.EventSystem, i._isValid = !0, i._popupIsFocus = !1, i._propsItem = ["required", "validation", "icon", "placeholder", "editable", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], i._propsTimepicker = ["timeFormat", "controls", "valueFormat"], i._props = r(p.baseProps, i._propsItem, i._propsTimepicker), i._keyManager = new _.KeyManager(function () {
				var t, e = document.activeElement;
				return (null === (t = null === (t = null === (t = i.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.input) || void 0 === t ? void 0 : t.el) === e || i._popup.isVisible()
			}), i._initView(e), i._initHandlers(), i._initHotkeys();
			return i.mount(t, u.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.TimePicker = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(49),
			l = i(3),
			d = i(0),
			c = i(1),
			u = i(14),
			h = i(12),
			f = i(5),
			p = i(8),
			_ = i(13),
			o = (s = u.Label, o(v, s), v.prototype.destructor = function () {
				this.events && this.events.clear(), this._popup && this._popup.destructor(), this.colorpicker && this.colorpicker.destructor(), this.events = this._uid = this._propsColorpicker = this._propsItem = this._props = this._isValid = null, s.prototype._destructor.call(this), this.unmount()
			}, v.prototype.setProperties = function (t) {
				if (t && !c.isEmptyObj(t) && this.events.fire(f.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) this._props.includes(e) && (this.config[e] = t[e], this._propsColorpicker.includes(e) && (this.colorpicker.config[e] = t[e]));
					this.events.fire(f.ItemEvent.afterChangeProperties, [this.getProperties()]), this.colorpicker.paint(), this.paint()
				}
			}, v.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
				return e
			}, v.prototype.show = function () {
				var t = this.config,
					e = t.value;
				t.hidden && this.events.fire(f.ItemEvent.beforeShow, [e]) && (this.config.hidden = !1, this.events.fire(f.ItemEvent.afterShow, [e]))
			}, v.prototype.hide = function (t) {
				var e = this.config,
					i = e.value;
				e.hidden && !t || !this.events.fire(f.ItemEvent.beforeHide, [i, t]) || (this.config.hidden = !0, this.events.fire(f.ItemEvent.afterHide, [i, t]))
			}, v.prototype.isVisible = function () {
				return !this.config.hidden
			}, v.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, v.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, v.prototype.isDisabled = function () {
				return this.config.disabled
			}, v.prototype.validate = function (t, e) {
				void 0 === t && (t = !1);
				e = void 0 === e ? this.getValue() : e;
				if (t || this.events.fire(f.ItemEvent.beforeValidate, [e])) return this._isValid = this.config.validation ? this.config.validation(e) : a.isHex(e), t || this.events.fire(f.ItemEvent.afterValidate, [e, this._isValid]), this._isValid
			}, v.prototype.clearValidate = function () {
				this.config.$validationStatus = f.ValidationStatus.pre, this.paint()
			}, v.prototype.setValue = function (t) {
				void 0 !== t && t !== this.config.value && (this.colorpicker.setValue(t), p.isVerify(this.config) && this.validate())
			}, v.prototype.getValue = function () {
				return this.config.value || ""
			}, v.prototype.clear = function () {
				"" !== this.config.value && (this.config.value = "", this.colorpicker.clear())
			}, v.prototype.getWidget = function () {
				return this.colorpicker
			}, v.prototype.focus = function () {
				var t = this;
				d.awaitRedraw().then(function () {
					t.getRootView().refs.input.el.focus()
				})
			}, v.prototype.blur = function () {
				var t = this;
				d.awaitRedraw().then(function () {
					t._popup.hide(), t.getRootView().refs.input.el.blur()
				})
			}, v.prototype._initView = function (t) {
				var e, i = this;
				if (c.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				for (e in this.colorpicker && this.colorpicker.destructor(), this._popup && this._popup.destructor(), this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					editable: !1,
					hidden: !1,
					value: "",
					grayShades: !0,
					pickerOnly: !1,
					paletteOnly: !1,
					customColors: [],
					palette: a.palette,
					mode: "palette",
					required: !1,
					validation: void 0,
					icon: "",
					placeholder: "",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				}, t) "id" !== e && "type" !== e && "name" !== e && (this.config[e] = t[e]);
				this._popup = new h.Popup({
					css: "dhx_widget--border-shadow"
				}), this.colorpicker = new a.Colorpicker(null, p.widgetConfig(t)), this._popup.attach(this.colorpicker), this.config.hidden && d.awaitRedraw().then(function () {
					i.hide(!0)
				})
			}, v.prototype._initHandlers = function () {
				var n = this;
				this.colorpicker.events.on(a.ColorpickerEvents.change, function () {
					var t = n.config.value = n.colorpicker.getValue();
					n.events.fire(f.ItemEvent.change, [t]), n._popup.hide(), n.paint()
				}), this.events.on(f.ItemEvent.afterValidate, function () {
					n.config.$validationStatus = n._isValid ? f.ValidationStatus.success : f.ValidationStatus.error, n.paint()
				}), this._popup.events.on(h.PopupEvents.afterHide, function () {
					document.activeElement !== n.getRootView().refs.input.el && n.events.fire(f.ItemEvent.blur, [n.getValue()]), n.paint()
				}), this.events.on(f.ItemEvent.blur, function () {
					n._popupIsFocus = !1, n.paint()
				}), document.addEventListener("keydown", function (t) {
					var e, i = document.activeElement;
					(null === (e = null === (e = null === (e = n.getRootView()) || void 0 === e ? void 0 : e.refs) || void 0 === e ? void 0 : e.input) || void 0 === e ? void 0 : e.el) !== i && !n._popup.isVisible() || n.events.fire(f.ItemEvent.keydown, [t])
				}), document.addEventListener("mousedown", function () {
					n._popup.getContainer() !== document.activeElement && (n._popupIsFocus = !1)
				})
			}, v.prototype._getHandlers = function () {
				var e = this;
				return {
					onblur: function () {
						e._popup.isVisible() || e.events.fire(f.ItemEvent.blur, [e.getValue()]), e.paint()
					},
					onfocus: function () {
						var t;
						e._popup.isVisible() || (t = e.getRootView().refs.input.el, e._popup.show(t), e.events.fire(f.ItemEvent.focus, [e.getValue()]), e.paint())
					},
					oninput: function (t) {
						t = t.target.value;
						e.events.fire(f.ItemEvent.input, [t])
					},
					onchange: function (t) {
						t = t.target.value;
						e.config.editable && a.isHex(t) ? e.setValue(t) : ("" === t && e.clear(), p.isVerify(e.config) && e.validate(), e.paint())
					}
				}
			}, v.prototype._initHotkeys = function () {
				var t, i = this,
					e = {
						"shift+tab": function () {
							i._applyTab()
						},
						tab: function () {
							i._applyTab()
						},
						escape: function () {
							i._popup.isVisible() && i.getRootView().refs.input.el.focus(), i._popup.hide()
						},
						enter: function () {
							var t;
							i._popup.isVisible() || (t = i.getRootView().refs.input.el, i._popup.show(t))
						},
						arrowRight: function (t) {
							var e = i._popup.getContainer();
							i._popup.isVisible() && !i._popupIsFocus && (t.preventDefault(), e.focus(), i._popupIsFocus = !0)
						}
					};
				for (t in e) this._keyManager.addHotKey(t, e[t])
			}, v.prototype._draw = function () {
				var t = this.config,
					e = t.required,
					i = t.value,
					n = t.icon,
					o = t.disabled,
					r = t.placeholder,
					s = t.name,
					a = t.id,
					l = t.editable,
					c = t.label,
					t = t.helpMessage,
					u = this._popup.isVisible() || (null === (u = null === (u = null === (u = this.getRootView()) || void 0 === u ? void 0 : u.refs) || void 0 === u ? void 0 : u.input) || void 0 === u ? void 0 : u.el) === document.activeElement;
				return d.el("div.dhx_form-group", {
					class: p.getFormItemCss(this.config, p.isVerify(this.config), u)
				}, [this._drawLabel(), d.el(".dhx_input__wrapper", [d.el("div.dhx_input__container", {}, [d.el(".dhx_input__icon", {
					class: n || "dxi dxi-eyedropper-variant" + (i ? " dhx_input__icon--color-selected" : ""),
					style: {
						"background-color": i || "transparent"
					}
				}), d.el("input.dhx_input.dhx_input--icon-padding", {
					tabindex: 0,
					dhx_id: s || a,
					_key: this._uid,
					_ref: "input",
					value: i,
					type: "text",
					required: e,
					disabled: o,
					class: u && "dhx_input--focus",
					placeholder: r || "",
					name: s || "",
					id: a || this._uid,
					onfocus: this._handlers.onfocus,
					oninput: this._handlers.oninput,
					onchange: this._handlers.onchange,
					onblur: this._handlers.onblur,
					autocomplete: "off",
					readOnly: !l,
					"aria-label": c || (l ? "type or" : "") + " select color",
					"aria-describedby": t ? "dhx_label__help_" + (a || this._uid) : null
				})]), p.getValidationMessage(this.config) && d.el("span.dhx_input__caption", {}, p.getValidationMessage(this.config))])])
			}, v.prototype._applyTab = function () {
				var t;
				document.activeElement === (null === (t = null === (t = null === (t = this._popup.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.content) || void 0 === t ? void 0 : t.el) || this._popupIsFocus || this._popup.hide()
			}, v);

		function v(t, e) {
			var i = s.call(this, null, e) || this;
			i.events = new l.EventSystem, i._isValid = !0, i._popupIsFocus = !1, i._propsItem = ["required", "validation", "icon", "placeholder", "editable", "label", "labelWidth", "labelPosition", "hiddenLabel", "helpMessage", "preMessage", "successMessage", "errorMessage"], i._propsColorpicker = ["mode", "grayShades", "customColors", "palette"], i._props = r(p.baseProps, i._propsItem, i._propsColorpicker), i._keyManager = new _.KeyManager(function () {
				var t, e = document.activeElement;
				return (null === (t = null === (t = null === (t = i.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.input) || void 0 === t ? void 0 : t.el) === e || i._popup.isVisible()
			}), i._initView(e), i._initHandlers(), i._initHotkeys();
			return i.mount(t, d.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.ColorPicker = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(4),
			c = i(5),
			u = i(3),
			d = i(1),
			h = i(8),
			o = (s = l.View, o(f, s), f.prototype.destructor = function () {
				this.events && this.events.clear(), this.config = this.events = null, this.unmount()
			}, f.prototype.setProperties = function (t) {
				if (t && !d.isEmptyObj(t) && this.events.fire(c.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) h.baseProps.includes(e) && (this.config[e] = t[e]);
					this.events.fire(c.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, f.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) h.baseProps.includes(t) && (e[t] = this.config[t]);
				return e
			}, f.prototype.show = function () {
				this.config.hidden && this.events.fire(c.ItemEvent.beforeShow, [void 0]) && (this.config.hidden = !1, this.events.fire(c.ItemEvent.afterShow, [void 0]))
			}, f.prototype.hide = function (t) {
				this.config.hidden && !t || !this.events.fire(c.ItemEvent.beforeHide, [void 0, t]) || (this.config.hidden = !0, this.events.fire(c.ItemEvent.afterHide, [void 0, t]))
			}, f.prototype.isVisible = function () {
				return !this.config.hidden
			}, f.prototype._draw = function () {
				return a.el("div")
			}, f);

		function f(t, e) {
			var i = s.call(this, t, r({
				disabled: !1,
				hidden: !1,
				width: "content",
				height: "content",
				padding: 0
			}, e)) || this;
			i.events = new u.EventSystem, i.config.hidden && a.awaitRedraw().then(function () {
				i.hide(!0)
			});
			return i.mount(t, a.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Spacer = o
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(110)), n(e(234))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(0),
			a = i(19),
			l = i(1),
			c = i(2),
			u = i(10),
			o = (r = a.Navbar, o(d, r), d.prototype.getState = function () {
				var t, e = {};
				for (t in this.data.eachChild(this.data.getRoot(), function (t) {
					t.twoState && !t.group ? e[t.id] = t.active : "input" !== t.type && "selectButton" !== t.type || (e[t.id] = t.value)
				}, !0), this._groups) this._groups[t].active && (e[t] = this._groups[t].active);
				return e
			}, d.prototype.setState = function (t) {
				for (var e in t) {
					var i;
					this._groups && this._groups[e] ? this._groups[e].active && (this.data.update(this._groups[e].active, {
						active: !1
					}), this._groups[e].active = t[e], this.data.update(t[e], {
						active: !0
					})) : "input" === (i = this.data.getItem(e)).type || "selectButton" === i.type ? this.data.update(e, {
						value: t[e]
					}) : this.data.update(e, {
						active: t[e]
					})
				}
			}, d.prototype._getFactory = function () {
				return a.createFactory({
					widget: this,
					defaultType: "navItem",
					allowedTypes: ["navItem", "button", "imageButton", "selectButton", "customHTML", "input", "separator", "spacer", "title", "block", "customHTMLButton"],
					widgetName: "ribbon"
				})
			}, d.prototype._getMode = function (t, e) {
				return t.id === e ? "bottom" : "right"
			}, d.prototype._close = function (t) {
				this._activePosition = null, this._currentRoot = null, r.prototype._close.call(this, t)
			}, d.prototype._draw = function () {
				var i = this;
				this._heightCalculate();
				var t = Math.max.apply(Math, this._widgetHeight);
				return s.el("ul.dhx_ribbon.dhx_widget", {
					dhx_widget_id: this._uid,
					class: this.config.css || "",
					tabindex: 0,
					onclick: this._handlers.onclick,
					onmousedown: this._handlers.onmousedown,
					oninput: this._listeners.input,
					onmouseover: this._listeners.tooltip,
					_hooks: {
						didInsert: function (t) {
							t.el.addEventListener("keyup", function (t) {
								var e;
								9 !== t.which || (e = c.locateNode(document.activeElement)) && (t = e.getAttribute("dhx_id"), (t = i.data.getItem(t)).tooltip && u.tooltip(t.tooltip, {
									node: e,
									position: u.Position.bottom,
									force: !0
								}))
							}, !0)
						}
					}
				}, [s.el("li", {
					class: "dhx_ribbon-block dhx_ribbon-block--root",
					style: {
						height: this._haveTitle ? t + 24 : t
					}
				}, [s.el("ul.dhx_ribbon-content.dhx_ribbon-content--full-width", {
					style: {
						height: t
					}
				}, this.data.map(function (t) {
					return "block" === t.type ? i._drawBlock(t, !0) : i._factory(t)
				}, this.data.getRoot(), !1))])])
			}, d.prototype._setRoot = function (t) {
				var e = this.data.getParent(t);
				"block" === this.data.getItem(e).type && (this._currentRoot = t)
			}, d.prototype._drawBlock = function (t, e) {
				var i = this;
				if (!t || t.hidden) return null;
				var n = "dhx_ribbon-block dhx_ribbon-block" + ("col" === t.direction ? "--col" : "--row") + (t.title ? " dhx_ribbon-block--title" : "") + (t.css ? " " + t.css : "") + (e ? " dhx_ribbon-block--indented" : ""),
					e = t.items.map(function (t) {
						return "block" === t.type ? i._drawBlock(t) : "separator" !== t.type && "spacer" !== t.type ? i._factory(t) : null
					}, t.id, !1);
				return s.el("li", {
					class: n
				}, [s.el("ul.dhx_ribbon-content", e), t.title ? s.el("span.dhx_ribbon-content-label-wrapper", [s.el("span.dhx_ribbon-content-label", t.title)]) : null])
			}, d.prototype._getBlockHeight = function (t) {
				return "medium" === t.size && "button" !== t.type ? 72 : 36
			}, d.prototype._heightCalculate = function (t) {
				var i = this;
				void 0 === t && (t = this.data), t.forEach(function (t) {
					var e;
					"block" === t.type && "col" === t.direction ? (e = t.items.reduce(function (t, e) {
						return t + i._getBlockHeight(e)
					}, 0), i._heightCalculate(t.items)) : e = i._getBlockHeight(t), t.title && (i._haveTitle = !0), i._widgetHeight.push(e)
				})
			}, d);

		function d(t, e) {
			var i = r.call(this, t, l.extend({
				navigationType: "click"
			}, e)) || this;
			i._widgetHeight = [], i._listeners = {
				input: function (t) {
					var e = c.locate(t);
					i.data.update(e, {
						value: t.target.value
					})
				},
				tooltip: function (t) {
					var e = c.locateNode(t);
					e && (t = e.getAttribute("dhx_id"), (t = i.data.getItem(t)).tooltip && u.tooltip(t.tooltip, {
						node: e,
						position: u.Position.bottom
					}))
				}
			}, i._currentRoot = null;
			return i.mount(t, s.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Ribbon = o
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(112)), n(e(235)), n(e(113))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(0),
			a = i(2),
			l = i(113),
			c = i(10),
			u = i(19),
			d = i(1),
			o = (r = u.Navbar, o(h, r), h.prototype.toggle = function () {
				this.config.collapsed ? this.expand() : this.collapse(), this.events.fire(l.SidebarEvents.toggle, [this.config.collapsed]), this.paint()
			}, h.prototype.collapse = function () {
				this.events.fire(l.SidebarEvents.beforeCollapse, []) && (this.config.collapsed = !0, this.events.fire(l.SidebarEvents.afterCollapse, []), this.paint())
			}, h.prototype.expand = function () {
				this.events.fire(l.SidebarEvents.beforeExpand, []) && (this.config.collapsed = !1, this.events.fire(l.SidebarEvents.afterExpand, []), this.paint())
			}, h.prototype.isCollapsed = function () {
				return this.config.collapsed
			}, h.prototype._getFactory = function () {
				return u.createFactory({
					widget: this,
					defaultType: "navItem",
					allowedTypes: ["navItem", "menuItem", "customHTML", "separator", "spacer", "title", "customHTMLButton"],
					widgetName: "sidebar"
				})
			}, h.prototype._close = function (t) {
				this._activePosition = null, this._currentRoot = null, r.prototype._close.call(this, t)
			}, h.prototype._setRoot = function (t) {
				this.data.getParent(t) === this.data.getRoot() && (this._currentRoot = t)
			}, h.prototype._customHandlers = function () {
				var i = this;
				return {
					tooltip: function (t) {
						var e = a.locateNode(t);
						e && (t = e.getAttribute("dhx_id"), ((t = i.data.getItem(t)).tooltip || i.config.collapsed && t.value) && c.tooltip(t.tooltip || t.value, {
							node: e,
							position: c.Position.right
						}))
					}
				}
			}, h.prototype._draw = function () {
				var i = this,
					t = this.config,
					e = t.width,
					t = t.minWidth,
					e = this.config.collapsed ? t : e;
				return s.el("nav.dhx_widget.dhx_sidebar", {
					class: (this.config.css || "") + (this.config.collapsed ? " dhx_sidebar--minimized" : ""),
					style: {
						width: e + "px"
					}
				}, [s.el("ul.dhx_navbar.dhx_navbar--vertical", {
					dhx_widget_id: this._uid,
					tabindex: 0,
					onclick: this._handlers.onclick,
					onmousedown: this._handlers.onmousedown,
					oninput: this._handlers.input,
					onmouseover: this._handlers.tooltip,
					_hooks: {
						didInsert: function (t) {
							t.el.addEventListener("keyup", function (t) {
								var e;
								9 !== t.which || (e = a.locateNode(document.activeElement)) && (t = e.getAttribute("dhx_id"), ((t = i.data.getItem(t)).tooltip || i.config.collapsed && t.value) && c.tooltip(t.tooltip || t.value, {
									node: e,
									position: c.Position.right,
									force: !0
								}))
							}, !0)
						}
					}
				}, this.data.map(function (t) {
					return i._factory(t)
				}, this.data.getRoot(), !1))])
			}, h.prototype._getMode = function () {
				return "right"
			}, h.prototype._customInitEvents = function () {
				var t = this;
				this.events.on(u.NavigationBarEvents.inputBlur, function () {
					t._waitRestore && (t.toggle(), t._waitRestore = !1)
				}), this.events.on(u.NavigationBarEvents.inputFocus, function () {
					t.config.collapsed && (t._waitRestore = !0, t.toggle())
				})
			}, h);

		function h(t, e) {
			var i = r.call(this, t, d.extend({
				navigationType: "click",
				width: "200",
				minWidth: "44",
				collapsed: !1
			}, e)) || this;
			i._currentRoot = null;
			return i.mount(t, s.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Sidebar = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.SidebarEvents || (e.SidebarEvents = {})).beforeCollapse = "beforeCollapse", e.afterCollapse = "afterCollapse", e.beforeExpand = "beforeExpand", e.afterExpand = "afterExpand", e.toggle = "toggle"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.TabbarEvents || (e.TabbarEvents = {})).change = "change", e.beforeClose = "beforeClose", e.afterClose = "afterClose", e.close = "close"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n, o, r = i(0),
			s = i(3),
			a = i(2),
			l = i(13);
		(i = n = e.EditorMode || (e.EditorMode = {})).editText = "text", i.selectItem = "select", (i = o = e.EditorEvents || (e.EditorEvents = {})).begin = "begin", i.end = "end";
		c.prototype.edit = function (t, e) {
			return this._active && this._item !== e.item && this._finishEdit(), this._active = !0, this._targetId = t, this.config = e, this._item = e.item, this._currentValue = this._item.value, this.events.fire(o.begin, [t]), this._initOuterClick(), this._addHotkeys(), this._draw()
		}, c.prototype.isEditable = function () {
			return this._active
		}, c.prototype._draw = function () {
			var e = this;
			if (this.config.mode !== n.selectItem) return r.el("div.dhx_tree-editor", {
				_hooks: {
					didInsert: this._handlers.didInsert
				},
				id: "input_" + this._item.id,
				oninput: this._handlers.editText,
				contentEditable: !0
			}, this._currentValue);
			var t = this.config.options;
			return r.el("select", {
				id: "input_" + this._item.id,
				dhx_id: this._item.id,
				onchange: this._handlers.itemSelected
			}, t.map(function (t) {
				return r.el("option", {
					class: "editor-select",
					value: t,
					selected: e._currentValue === t,
					style: {
						border: "1px solid"
					}
				}, t)
			}))
		}, c.prototype._addHotkeys = function () {
			var t = this;
			this._keyManager.addHotKey("escape", function () {
				t._finishEdit()
			}), this._keyManager.addHotKey("enter", function () {
				t._finishEdit()
			})
		}, c.prototype._removeHotkeys = function () {
			this._keyManager.removeHotKey()
		}, c.prototype._finishEdit = function () {
			this.events.fire(o.end, [this._targetId, this._item.id, this._currentValue]) && this._clear()
		}, c.prototype._clear = function () {
			this._active = !1, this._removeClickListener(), this._removeHotkeys()
		}, c.prototype._initOuterClick = function () {
			document.addEventListener("click", this._documentClick)
		}, c.prototype._removeClickListener = function () {
			document.removeEventListener("click", this._documentClick)
		}, i = c;

		function c() {
			var e = this;
			this.events = new s.EventSystem, this._keyManager = new l.KeyManager, this._documentClick = function (t) {
				a.locate(t, "id") !== "input_" + e._item.id && (e._removeClickListener(), e._finishEdit())
			}, this._handlers = {
				editText: function (t) {
					e._currentValue = t.target.innerText
				},
				itemSelected: function (t) {
					e._currentValue = t.target.value, e._finishEdit()
				},
				didInsert: function (t) {
					var e, i = null == t ? void 0 : t.el;
					i && (e = document.createRange(), t = window.getSelection(), e.setStart(i.childNodes[0], i.innerText.length), e.collapse(!0), t.removeAllRanges(), t.addRange(e), i.focus())
				}
			}
		}
		e.Editor = i
	}, function (t, e, i) {
		"use strict";
		var n;
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (n = e.SelectStatus || (e.SelectStatus = {}))[n.unselected = 0] = "unselected", n[n.selected = 1] = "selected", n[n.indeterminate = 2] = "indeterminate", (e = e.TreeEvents || (e.TreeEvents = {})).itemClick = "itemclick", e.itemDblClick = "itemdblclick", e.itemRightClick = "itemrightclick", e.beforeCollapse = "beforeCollapse", e.afterCollapse = "afterCollapse", e.beforeExpand = "beforeExpand", e.afterExpand = "afterExpand", e.beforeEditStart = "beforeEditStart", e.afterEditStart = "afterEditStart", e.beforeEditEnd = "beforeEditEnd", e.afterEditEnd = "afterEditEnd", e.focusChange = "focusChange", e.beforeCheck = "beforeCheck", e.afterCheck = "afterCheck", e.itemContextMenu = "itemcontextmenu"
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(118)), n(e(242))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var l = i(1),
			n = i(0),
			o = i(3),
			r = i(13),
			c = i(18),
			s = i(11),
			a = i(25),
			u = i(240),
			f = i(119),
			d = i(241),
			h = i(19),
			i = (p.prototype.paint = function () {
				this._layout.paint()
			}, p.prototype.isFullScreen = function () {
				return this._fullScreen
			}, p.prototype.setFullScreen = function () {
				this._fullScreen || (this._fullScreen = !0, this.setSize(window.innerWidth, window.innerHeight), this.setPosition(window.pageXOffset, window.pageYOffset))
			}, p.prototype.unsetFullScreen = function () {
				this._fullScreen && (this._fullScreen = !1, this.setSize(this._oldSizes.width, this._oldSizes.height), this.setPosition(this._oldPosition.left, this._oldPosition.top))
			}, p.prototype.setSize = function (t, e) {
				this._oldSizes = {
					width: this._popup.offsetWidth,
					height: this._popup.offsetHeight
				};
				var i = {
					width: this._oldSizes.width,
					height: this._oldSizes.height
				};
				l.isDefined(t) && (this.config.width = i.width = t), l.isDefined(e) && (this.config.height = i.height = e), this._popup.style.width = i.width + "px", this._popup.style.height = i.height + "px", this.events.fire(f.WindowEvents.resize, [i, this._oldSizes, {
					left: !0,
					top: !0,
					bottom: !0,
					right: !0
				}])
			}, p.prototype.getSize = function () {
				return {
					width: this._popup.offsetWidth,
					height: this._popup.offsetHeight
				}
			}, p.prototype.setPosition = function (t, e) {
				this._oldPosition = {
					left: this._popup.offsetLeft,
					top: this._popup.offsetTop
				};
				var i = {
					left: this._oldPosition.left,
					top: this._oldPosition.top
				};
				l.isDefined(t) && (this.config.left = i.left = t), l.isDefined(e) && (this.config.top = i.top = e), this._popup.style.left = i.left + "px", this._popup.style.top = i.top + "px", this.events.fire(f.WindowEvents.resize, [i, this._oldPosition, {
					left: !0,
					top: !0,
					bottom: !0,
					right: !0
				}])
			}, p.prototype.getPosition = function () {
				return {
					left: this._popup.offsetLeft,
					top: this._popup.offsetTop
				}
			}, p.prototype.show = function (t, e) {
				if (void 0 === t && (t = this.config.left), void 0 === e && (e = this.config.top), this.events.fire(f.WindowEvents.beforeShow, [{
					left: t,
					top: e
				}])) {
					this.isVisible() && this.hide(), d.default.setActive(this._uid), this._popup.className += " dhx_popup dhx_widget" + (this.config.modal ? " dhx_popup--window_modal" : " dhx_popup--window") + (this.config.css ? " " + this.config.css : ""), this._popup.style.position = this.config.modal ? "fixed" : "absolute", this._popup.setAttribute("dhx_widget_id", this._uid);
					var i = this._getContainerParams(),
						n = i.containerInnerWidth,
						o = i.containerInnerHeight,
						r = i.containerXOffset,
						s = i.containerYOffset,
						a = this.config.width = this.config.width || this.config.minWidth || n / 2,
						i = this.config.height = this.config.height || this.config.minHeight || o / 2;
					if (this.config.left = t = l.isDefined(t) ? t : (n - a) / (this.config.modal ? 2 : 2 + r), this.config.top = e = l.isDefined(e) ? e : (o - i) / (this.config.modal ? 2 : 2 + s), this._isActive) return this._popup.style.left = t + "px", void (this._popup.style.top = e + "px");
					this.config.viewportOverflow && d.default.openFreeWindow(this.config.node), this.config.modal && this._blockScreen(), this._popup.style.width = a + "px", this._popup.style.height = i + "px", this._popup.style.left = t + "px", this._popup.style.top = e + "px", this.config.node.appendChild(this._popup), this._popup.focus(), this._isActive = !0, this.events.fire(f.WindowEvents.afterShow, [{
						left: t,
						top: e
					}]), c.focusManager.setFocusId(this._uid)
				}
			}, p.prototype.hide = function () {
				this._hide()
			}, p.prototype._hide = function (t) {
				this._isActive && this.events.fire(f.WindowEvents.beforeHide, [{
					left: this.config.left,
					top: this.config.top
				}, t]) && (this.config.viewportOverflow && d.default.closeFreeWindow(this.config.node), this._blocker && (this.config.node.removeChild(this._blocker), this.config.closable && this._keyManager.removeHotKey(null, this), this._blocker = null), this.config.node.removeChild(this._popup), this._isActive = !1, this.events.fire(f.WindowEvents.afterHide, [{
					left: this.config.left,
					top: this.config.top
				}, t]))
			}, p.prototype.isVisible = function () {
				return this._isActive
			}, p.prototype.getWidget = function () {
				return this._layout.getCell("content").getWidget()
			}, p.prototype.getContainer = function () {
				return this.getRootView().data._container
			}, p.prototype.attach = function (t, e) {
				this._layout.getCell("content").attach(t, e)
			}, p.prototype.attachHTML = function (t) {
				this._layout.getCell("content").attachHTML(t)
			}, p.prototype.getRootView = function () {
				return this._layout.getRootView()
			}, p.prototype.destructor = function () {
				this._isActive && this.hide(), this.header && this.header.destructor(), this.footer && this.footer.destructor(), this.events && this.events.clear(), this._keyManager && this._keyManager.destructor(), this._layout && this._layout.destructor(), this.config = this.header = this.footer = this.events = null, this._popup = this._uid = this._handlers = this._isActive = this._keyManager = this._fullScreen = this._oldSizes = this._oldPosition = null
			}, p.prototype.fullScreen = function () {
				this.setFullScreen()
			}, p.prototype._initHandlers = function () {
				var i = this;
				this._handlers = {
					headerDblClick: function (t) {
						return i.events.fire(f.WindowEvents.headerDoubleClick, [t])
					},
					move: function (t) {
						3 !== t.which && (t.preventDefault(), d.default.setActive(i._uid), u.detectDrag(t).then(function (t) {
							t && i._startDrag(t.x, t.y)
						}))
					},
					resize: {
						".dhx_window-resizer": function (e) {
							3 !== e.which && (e.preventDefault(), d.default.setActive(i._uid), u.detectDrag(e).then(function (t) {
								t && ((t = e.target.classList).contains("dhx_window-resizer--left") ? i._startResize({
									left: !0
								}) : t.contains("dhx_window-resizer--right") ? i._startResize({
									right: !0
								}) : t.contains("dhx_window-resizer--top") ? i._startResize({
									top: !0
								}) : t.contains("dhx_window-resizer--bottom") ? i._startResize({
									bottom: !0
								}) : t.contains("dhx_window-resizer--bottom_left") ? i._startResize({
									left: !0,
									bottom: !0
								}) : t.contains("dhx_window-resizer--bottom_right") ? i._startResize({
									bottom: !0,
									right: !0
								}) : t.contains("dhx_window-resizer--top_left") ? i._startResize({
									top: !0,
									left: !0
								}) : t.contains("dhx_window-resizer--top_right") && i._startResize({
									top: !0,
									right: !0
								}))
							}))
						}
					},
					setActive: function () {
						d.default.setActive(i._uid)
					}
				}
			}, p.prototype._initUI = function () {
				var i = this,
					t = [],
					e = (this.config.header || this.config.title || this.config.closable || this.config.movable) && !1 !== this.config.header;
				e && t.push({
					id: "header",
					height: "content",
					css: "dhx_window-header " + (this.config.movable ? "dhx_window-header--movable" : ""),
					on: {
						mousedown: this.config.movable && this._handlers.move,
						dblclick: this._handlers.headerDblClick
					}
				}), t.push({
					id: "content",
					css: e ? "dhx_window-content" : "dhx_window-content-without-header"
				}), this.config.footer && t.push({
					id: "footer",
					height: "content",
					css: "dhx_window-footer"
				}), this.config.resizable && t.push({
					id: "resizers",
					height: "content",
					css: "resizers"
				});
				var n, t = this._layout = new s.Layout(this._popup, {
					css: "dhx_window" + (this.config.modal ? " dhx_window--modal" : ""),
					rows: t,
					on: {
						click: this._handlers.setActive
					},
					id: this._uid
				});
				e && (n = this.header = new a.Toolbar, this.config.title && (this.header.data.add({
					type: "title",
					value: this.config.title,
					id: "title",
					css: "title_max"
				}), this._popup.setAttribute("aria-label", this.config.title)), this.config.closable && (this.header.data.add({
					type: "spacer"
				}), this.header.data.add({
					id: "close",
					type: "button",
					view: "link",
					size: "medium",
					color: "secondary",
					circle: !0,
					icon: "dxi dxi-close"
				}), n.events.on(h.NavigationBarEvents.click, function (t, e) {
					"close" === t && i._hide(e)
				})), t.getCell("header").attach(n)), this.config.footer && (n = this.footer = new a.Toolbar, t.getCell("footer").attach(n)), this.config.resizable && t.getCell("resizers").attach(function () {
					return i._drawResizers()
				})
			}, p.prototype._drawResizers = function () {
				return n.el(".dhx-resizers", {
					onmousedown: this._handlers.resize
				}, [n.el(".dhx_window-resizer.dhx_window-resizer--left", {
					_ref: "left"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--right", {
					_ref: "right"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--bottom", {
					_ref: "bottom"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--top", {
					_ref: "top"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--bottom_right", {
					_ref: "bottomRight"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--bottom_left", {
					_ref: "bottomLeft"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--top_right", {
					_ref: "topRight"
				}), n.el(".dhx_window-resizer.dhx_window-resizer--top_left", {
					_ref: "topLeft"
				})])
			}, p.prototype._startDrag = function (t, e) {
				var a = this;
				this.config.node.classList.add("dhx_window--stop_selection");

				function i(t) {
					var e, i, n, o = {
						left: a._popup.offsetLeft,
						top: a._popup.offsetTop
					},
						r = t.pageX - l,
						s = t.pageY - c;
					a.config.viewportOverflow || (e = (n = a._getContainerParams()).containerXOffset, i = n.containerYOffset, t = n.containerInnerWidth, n = n.containerInnerHeight, r < e ? r = e : e + t - u < r && (r = e + t - u), s < i ? s = i : i + n - d < s && (s = i + n - d)), a.config.left = r, a.config.top = s, a._popup.style.left = r + "px", a._popup.style.top = s + "px", s = {
						left: r,
						top: s
					}, a.events.fire(f.WindowEvents.move, [s, o, {
						left: !0,
						top: !0,
						bottom: !0,
						right: !0
					}])
				}
				var l = t - this._popup.offsetLeft,
					c = e - this._popup.offsetTop,
					u = this._popup.offsetWidth,
					d = this._popup.offsetHeight,
					n = function () {
						document.removeEventListener("mouseup", n), document.removeEventListener("mousemove", i), a.config.node.classList.remove("dhx_window--stop_selection")
					};
				document.addEventListener("mouseup", n), document.addEventListener("mousemove", i)
			}, p.prototype._startResize = function (r) {
				var t, e, s = this,
					a = 100 | this.config.minWidth,
					l = 100 | this.config.minHeight,
					c = this._popup.offsetLeft,
					u = this._popup.offsetTop,
					d = this._popup.offsetWidth,
					h = this._popup.offsetHeight,
					i = this.getRootView().refs;
				switch (!0) {
					case r.bottom && r.left:
						e = "dhx_window-body-pointer--bottom_left", t = i.bottomLeft;
						break;
					case r.bottom && r.right:
						e = "dhx_window-body-pointer--bottom_right", t = i.bottomLeft;
						break;
					case r.top && r.left:
						e = "dhx_window-body-pointer--top_left", t = i.bottomLeft;
						break;
					case r.top && r.right:
						e = "dhx_window-body-pointer--top-right", t = i.right;
						break;
					case r.top:
						e = "dhx_window-body-pointer--top", t = i.bottomLeft;
						break;
					case r.bottom:
						e = "dhx_window-body-pointer--bottom", t = i.bottomLeft;
						break;
					case r.left:
						e = "dhx_window-body-pointer--left", t = i.bottomLeft;
						break;
					case r.right:
						e = "dhx_window-body-pointer--right", t = i.right
				}
				t.el.classList.add("dhx_window-resizer--active"), this.config.node.classList.add("dhx_window--stop_selection"), this.config.node.classList.add(e);

				function n(t) {
					var e = (o = s._getContainerParams()).containerInnerWidth,
						i = o.containerInnerHeight,
						n = o.containerXOffset,
						o = o.containerYOffset,
						t = {
							width: s._notInNode() ? t.pageX - c : t.pageX - s.config.node.offsetLeft - c,
							height: s._notInNode() ? t.pageY - u : t.pageY - s.config.node.offsetTop - u,
							left: s._notInNode() ? t.pageX : t.pageX - s.config.node.offsetLeft,
							top: s._notInNode() ? t.pageY : t.pageY - s.config.node.offsetTop
						};
					r.right && (t.width < a ? t.width = a : t.width > n + e - c && (t.width = n + e - c), s._popup.style.width = t.width + "px"), r.bottom && (t.height < l ? t.height = l : t.height > o + i - u && (t.height = o + i - u), s._popup.style.height = t.height + "px"), r.left && (c + d - t.left < a && (t.left = c + d - a), t.width = c + d - t.left, s.config.left = t.left, s._popup.style.left = t.left + "px", s._popup.style.width = t.width + "px"), r.top && (t.top < o ? t.top = o : u + h - t.top < l && (t.top = u + h - l), t.height = u + h - t.top, s.config.top = t.top, s._popup.style.top = t.top + "px", s._popup.style.height = t.height + "px"), s.config.width = s._popup.offsetWidth, s.config.height = s._popup.offsetHeight, s.events.fire(f.WindowEvents.resize, [t, {
						left: c,
						top: u,
						height: h,
						width: d
					}, r])
				}
				var o = function () {
					document.removeEventListener("mouseup", o), document.removeEventListener("mousemove", n), s.config.node.classList.remove("dhx_window--stop_selection"), s.config.node.classList.remove(e), t.el.classList.remove("dhx_window-resizer--active")
				};
				document.addEventListener("mouseup", o), document.addEventListener("mousemove", n)
			}, p.prototype._blockScreen = function () {
				var n = this,
					t = document.createElement("div");
				t.className = "dhx_window__overlay", this.config.node.appendChild(t), this._blocker = t, this.config.closable && (t.addEventListener("click", function (t) {
					return n._hide(t)
				}), this._keyManager.addHotKey("escape", function (t) {
					var e = Array.prototype.slice.call(document.querySelectorAll(".dhx_popup--window_modal")),
						i = Array.prototype.slice.call(document.querySelectorAll(".dhx_popup--window")),
						i = e.concat(i);
					1 !== i.length ? (i.sort(function (t, e) {
						return +window.getComputedStyle(e).zIndex - +window.getComputedStyle(t).zIndex
					}), i[i.length - 1] === n._popup && n._hide(t)) : n._hide(t)
				}))
			}, p.prototype._notInNode = function () {
				return this.config.node === document.body || this.config.modal
			}, p.prototype._getContainerParams = function () {
				var t = this._notInNode();
				return {
					containerInnerWidth: t ? window.innerWidth : this.config.node.offsetWidth,
					containerInnerHeight: t ? window.innerHeight : this.config.node.offsetHeight,
					containerXOffset: t ? window.pageXOffset : this.config.node.scrollLeft,
					containerYOffset: t ? window.pageYOffset : this.config.node.scrollTop
				}
			}, p);

		function p(t) {
			var i = this;
			this.config = l.extend({
				movable: !1,
				resizable: !1,
				closable: t.modal
			}, t), this.config.node && "string" == typeof this.config.node ? this.config.node = document.getElementById(this.config.node) : this.config.node || (this.config.node = document.body), this._uid = l.uid(), this.events = new o.EventSystem(this);
			t = this._popup = document.createElement("div");
			t.tabIndex = 1, t.setAttribute("role", "dialog"), t.setAttribute("aria-modal", "" + (this.config.modal || !1)), this.config.modal || d.default.add(this._uid, this._popup), this._fullScreen = !1, this._isActive = !1, this._keyManager = new r.KeyManager(function (t, e) {
				return e === i._uid
			}), this._initHandlers(), this._initUI(), this.config.html && this.attachHTML(this.config.html)
		}
		e.Window = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.WindowEvents || (e.WindowEvents = {})).resize = "resize", e.headerDoubleClick = "headerdoubleclick", e.move = "move", e.afterShow = "aftershow", e.afterHide = "afterhide", e.beforeShow = "beforeshow", e.beforeHide = "beforehide"
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(243)), n(e(122)), n(e(121))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			u = this && this.__assign || function () {
				return (u = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, d = i(1),
			h = i(6),
			o = (r = h.TreeCollection, o(s, r), s.prototype.eachChild = function (t, e, i, n) {
				void 0 === i && (i = !0), n = n || function (t) {
					return !1 !== t.$opened
				}, r.prototype.eachChild.call(this, t, e, i, n)
			}, s.prototype.getMaxLevel = function () {
				var e = this,
					i = 1;
				return this.map(function (t) {
					t = e.getLevel(t.id);
					i = Math.max(t, i)
				}), i
			}, s.prototype.getLevel = function (t) {
				var e = 0;
				return this.eachParent(t, function () {
					e++
				}), e
			}, s.prototype.serialize = function (t) {
				void 0 === t && (t = h.DataDriver.json);
				var i = [];
				r.prototype.eachChild.call(this, this.getRoot(), function (t) {
					var e;
					t && (e = u({}, t), Object.keys(e).forEach(function (t) {
						t.startsWith("$") && delete e[t]
					}), i.push(e))
				});
				t = h.toDataDriver(t);
				if (t) return t.serialize(i)
			}, s.prototype.getPlainIndex = function (t) {
				return Object.keys(this._pull).indexOf("" + t)
			}, s.prototype.map = function (t, e, i) {
				void 0 === e && (e = this._root), void 0 === i && (i = !0);
				var n = [];
				if (!this.haveItems(e)) return n;
				for (var o, r = 0; r < this._childs[e].length; r++) n.push(t.call(this, this._childs[e][r], r)), i && this._childs[e][r].hasOwnProperty("$opened") && (o = this.map(t, this._childs[e][r].id, i), n = n.concat(o));
				return n
			}, s.prototype.mapVisible = function (t, e, i) {
				void 0 === e && (e = this._root), void 0 === i && (i = !0);
				var n = [];
				if (!this.haveItems(e)) return n;
				for (var o, r = 0; r < this._childs[e].length; r++) this._childs[e][r].hidden || (n.push(t.call(this, this._childs[e][r], r)), i && this._childs[e][r].$opened && (o = this.mapVisible(t, this._childs[e][r].id, i), n = n.concat(o)));
				return n
			}, s.prototype.getId = function (t) {
				return Object.keys(this._pull)[t]
			}, s.prototype._parse_data = function (t, e) {
				var i;
				void 0 === e && (e = this._root);
				for (var n = this._order.length, o = 0, r = t; o < r.length; o++) {
					var s = r[o];
					this.config.init && (s = this.config.init(s));
					for (var a = 0, l = Object.keys(s); a < l.length; a++) {
						var c = l[a];
						c.includes("$items") && delete s[c]
					}
					s.id = null !== (i = s.id) && void 0 !== i ? i : d.uid(), s.parent = void 0 === s.parent || s.parent && s.$items ? e : s.parent, this._pull[s.id] && h.dhxError("Item " + s.id + " already exist"), this._pull[s.id] = s, this._order[n++] = s, this._childs[s.parent] || (this._childs[s.parent] = []), this._childs[s.parent].push(s), s.$level = this.getLevel(s.id), s.items && s.items instanceof Object && (s.$opened = !0, this._parse_data(s.items, s.id))
				}
				this._checkItems()
			}, s.prototype._copy = function (t, e, i, n, o) {
				if (void 0 === i && (i = this), void 0 === n && (n = this._root), !this.exists(t)) return null;
				var r = this._childs[t];
				if (o && (e = -1 === e ? -1 : e + o), i === this && !this.canCopy(t, n)) return null;
				o = u({}, this.getItem(t));
				if (i.exists(t) && (o.id = d.uid()), this.exists(t) && (o.parent = n, i !== this && n === this._root && (o.parent = i.getRoot()), i.add(o, e), t = o.id), r)
					for (var s = 0, a = r; s < a.length; s++) {
						var l = a[s].id,
							c = this.getIndex(l);
						"string" == typeof t && this.copy(l, c, i, t)
					}
				return t
			}, s.prototype._addToOrder = function (t, e, i) {
				r.prototype._addToOrder.call(this, t, e, i), e.$level = this.getLevel(e.id), this._checkItems()
			}, s.prototype._removeCore = function (t) {
				r.prototype._removeCore.call(this, t), this._checkItems()
			}, s.prototype._checkItems = function () {
				var i = this;
				this.eachChild(this._root, function (t) {
					var e = i.haveItems(t.id);
					t.$items = e, t.$opened = d.isDefined(t.$opened) ? t.$opened : e
				}, !0, function () {
					return !0
				})
			}, s);

		function s(t, e) {
			return r.call(this, t, e) || this
		}
		e.TreeGridCollection = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e = e.TreeGridEvents || (e.TreeGridEvents = {})).beforeCollapse = "beforeCollapse", e.afterCollapse = "afterCollapse", e.beforeExpand = "beforeExpand", e.afterExpand = "afterExpand"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), (e.PaginationEvents || (e.PaginationEvents = {})).change = "change"
	}, function (t, e, i) {
		i(125), i(126), i(127), i(128), i(129), i(130), i(137), i(138), t.exports = i(139)
	}, function (t, e) {
		Object.values = Object.values || function (e) {
			var t = Object.prototype.toString.call(e);
			if (null == e) throw new TypeError("Cannot convert undefined or null to object");
			if (~["[object String]", "[object Object]", "[object Array]", "[object Function]"].indexOf(t)) {
				if (Object.keys) return Object.keys(e).map(function (t) {
					return e[t]
				});
				var i, n = [];
				for (i in e) e.hasOwnProperty(i) && n.push(e[i]);
				return n
			}
			return []
		}, Object.assign || Object.defineProperty(Object, "assign", {
			enumerable: !1,
			configurable: !0,
			writable: !0,
			value: function (t) {
				"use strict";
				for (var e = [], i = 1; i < arguments.length; i++) e[i - 1] = arguments[i];
				if (null == t) throw new TypeError("Cannot convert first argument to object");
				for (var n = Object(t), o = 0; o < e.length; o++) {
					var r = e[o];
					if (null != r)
						for (var s = Object.keys(Object(r)), a = 0, l = s.length; a < l; a++) {
							var c = s[a],
								u = Object.getOwnPropertyDescriptor(r, c);
							void 0 !== u && u.enumerable && (n[c] = r[c])
						}
				}
				return n
			}
		})
	}, function (t, e) {
		Array.prototype.includes || Object.defineProperty(Array.prototype, "includes", {
			value: function (t, e) {
				if (null == this) throw new TypeError('"this" is null or not defined');
				var i = Object(this),
					n = i.length >>> 0;
				if (0 == n) return !1;
				var o, r, e = 0 | e,
					s = Math.max(0 <= e ? e : n - Math.abs(e), 0);
				for (; s < n;) {
					if ((o = i[s]) === (r = t) || "number" == typeof o && "number" == typeof r && isNaN(o) && isNaN(r)) return !0;
					s++
				}
				return !1
			},
			configurable: !0,
			writable: !0
		}), Array.prototype.find || Object.defineProperty(Array.prototype, "find", {
			value: function (t) {
				if (null == this) throw new TypeError('"this" is null or not defined');
				var e = Object(this),
					i = e.length >>> 0;
				if ("function" != typeof t) throw new TypeError("predicate must be a function");
				for (var n = arguments[1], o = 0; o < i;) {
					var r = e[o];
					if (t.call(n, r, o, e)) return r;
					o++
				}
			},
			configurable: !0,
			writable: !0
		}), Array.prototype.findIndex || (Array.prototype.findIndex = function (t) {
			if (null == this) throw new TypeError("Array.prototype.findIndex called on null or undefined");
			if ("function" != typeof t) throw new TypeError("predicate must be a function");
			for (var e, i = Object(this), n = i.length >>> 0, o = arguments[1], r = 0; r < n; r++)
				if (e = i[r], t.call(o, e, r, i)) return r;
			return -1
		})
	}, function (t, e) {
		String.prototype.includes || (String.prototype.includes = function (t, e) {
			"use strict";
			return "number" != typeof e && (e = 0), !(e + t.length > this.length) && -1 !== this.indexOf(t, e)
		}), String.prototype.startsWith || Object.defineProperty(String.prototype, "startsWith", {
			enumerable: !1,
			configurable: !0,
			writable: !0,
			value: function (t, e) {
				return e = e || 0, this.indexOf(t, e) === e
			}
		}), String.prototype.padStart || (String.prototype.padStart = function (t, e) {
			return t >>= 0, e = String(e || " "), this.length > t ? String(this) : ((t -= this.length) > e.length && (e += e.repeat(t / e.length)), e.slice(0, t) + String(this))
		}), String.prototype.padEnd || (String.prototype.padEnd = function (t, e) {
			return t >>= 0, e = String(e || " "), this.length > t ? String(this) : ((t -= this.length) > e.length && (e += e.repeat(t / e.length)), String(this) + e.slice(0, t))
		})
	}, function (t, e) {
		var i, n, o, r;
		Element && !Element.prototype.matches && ((i = Element.prototype).matches = i.matchesSelector || i.mozMatchesSelector || i.msMatchesSelector || i.oMatchesSelector || i.webkitMatchesSelector), "classList" in SVGElement.prototype || Object.defineProperty(SVGElement.prototype, "classList", {
			get: function () {
				var i = this;
				return {
					contains: function (t) {
						return -1 !== i.className.baseVal.split(" ").indexOf(t)
					},
					add: function (t) {
						return i.setAttribute("class", i.getAttribute("class") + " " + t)
					},
					remove: function (t) {
						var e = i.getAttribute("class").replace(new RegExp("(\\s|^)".concat(t, "(\\s|$)"), "g"), "$2");
						i.classList.contains(t) && i.setAttribute("class", e)
					},
					toggle: function (t) {
						this.contains(t) ? this.remove(t) : this.add(t)
					}
				}
			},
			configurable: !0
		}), Object.entries || (n = Function.bind.call(Function.call, Array.prototype.reduce), o = Function.bind.call(Function.call, Object.prototype.propertyIsEnumerable), r = Function.bind.call(Function.call, Array.prototype.concat), Object.entries = function (i) {
			return n(Object.keys(i), function (t, e) {
				return r(t, "string" == typeof e && o(i, e) ? [
					[e, i[e]]
				] : [])
			}, [])
		}), Event.prototype.composedPath || (Event.prototype.composedPath = function () {
			if (this.path) return this.path;
			var t = this.target;
			for (this.path = []; null !== t.parentNode;) this.path.push(t), t = t.parentNode;
			return this.path.push(document, window), this.path
		})
	}, function (t, e) {
		Math.sign = Math.sign || function (t) {
			return 0 === (t = +t) || isNaN(t) ? t : 0 < t ? 1 : -1
		}
	}, function (t, e, i) {
		//"use strict";
		//i.r(e);
		//var n, o = i(10),
		//	e = i(60),
		//	r = ["dhx_638523928_message", "undefined", "Please contact us at <a style='color: white;' href='mailto:contact@dhtmlx.com?subject=DHTMLX Licensing - Trial End' target='_blank'>contact@dhtmlx.com</a> or visit ", " target='_blank'>dhtmlx.com</a> in order to obtain a proper license.", "Your evaluation period for DHTMLX has expired.", "now", "1638427507000", "join", "<a style='color: white;' href='https://dhtmlx.com/docs/products/licenses.shtml?expand=1&utm_source=trial&utm_medium=popup&utm_campaign=second_month#suite'"];
		//n = r,
		//	function (t) {
		//		for (; --t;) n.push(n.shift())
		//	}(150);

		//function s(t, e) {
		//	return r[t -= 374]
		//}
		//setTimeout(function () {
		//	var t, e, i = s,
		//		n = [i(382), i(380), i(377) + i(381)][i(376)]("<br>");
		//	typeof i(375) !== i(379) && setInterval(function () {
		//		var t, e = i;
		//		t = i, 2592e6 < Date[t(374)]() - t(375) && Object(o.message)({
		//			text: n,
		//			icon: "dxi-close",
		//			css: e(378)
		//		})
		//	}, (t = 6e4, e = 12e4, Math.floor(Math.random() * (e - t + 1)) + t))
		//}, 1)
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(1),
			s = i(2),
			a = i(38),
			l = new WeakMap,
			c = new Map;

		function u(t, e) {
			var i = document.createElement("div");
			return i.setAttribute("data-position", e), i.className = "dhx_message-container dhx_message-container--" + e + (t === document.body ? " dhx_message-container--in-body" : ""), i
		}

		function d(t, e) {
			e && clearTimeout(l.get(t));
			var i = t.parentNode,
				n = i.getAttribute("data-position"),
				o = i.parentNode,
				e = c.get(o);
			e && (!(e = e[n]) || -1 !== (e = (n = e.stack).indexOf(t)) && (i.removeChild(t), n.splice(e, 1), 0 === n.length && o.removeChild(i)))
		}
		e.message = function (t) {
			"string" == typeof t && (t = {
				text: t
			}), t.position = t.position || a.MessageContainerPosition.topRight;
			var e = document.createElement("div");
			e.className = "dhx_widget dhx_message " + (t.css || ""), e.setAttribute("role", "alert"), (o = t.text && r.uid()) && e.setAttribute("aria-describedby", o), t.html ? e.innerHTML = t.html : e.innerHTML = '<span class="dhx_message__text" id=' + o + ">" + t.text + "</span>\n\t\t" + (t.icon ? '<span class="dhx_message__icon dxi ' + t.icon + '"></span>' : "");
			var i = t.node ? s.toNode(t.node) : document.body;
			"static" === getComputedStyle(i).position && (i.style.position = "relative"), (o = c.get(i)) ? o[t.position] || (o[t.position] = {
				stack: [],
				container: u(i, t.position)
			}) : c.set(i, ((n = {})[t.position] = {
				stack: [],
				container: u(i, t.position)
			}, n));
			var n = (o = c.get(i)[t.position]).stack,
				o = o.container;
			0 === n.length && i.appendChild(o), n.push(e), o.appendChild(e), t.expire && (t = setTimeout(function () {
				return d(e)
			}, t.expire), l.set(e, t)), e.onclick = function () {
				return d(e, !0)
			}
		}
	}, function (t, n, o) {
		"use strict";
		(function (t) {
			Object.defineProperty(n, "__esModule", {
				value: !0
			});
			var e = o(40),
				i = o(58),
				c = o(1);
			n.alert = function (s) {
				var a = s.buttons && s.buttons[0] ? s.buttons[0] : e.default.apply,
					l = i.blockScreen(s.blockerCss);
				return new t(function (e) {
					var t = "dhx_alert__" + c.uid() + "_content",
						i = "dhx_alert__" + c.uid() + "_header",
						n = document.createElement("div");
					n.setAttribute("role", "alert"), n.setAttribute("aria-modal", "true"), s.text && n.setAttribute("aria-describedby", t), s.header && n.setAttribute("aria-labelledby", i), n.className = "dhx_widget dhx_alert " + (s.css || "");
					var o = function (t) {
						"Escape" !== t.key && "Esc" !== t.key || (r(t), e(!1))
					};

					function r(t) {
						t.preventDefault(), l(), document.body.removeChild(n), document.removeEventListener("keydown", o)
					}
					n.innerHTML = "\n\t\t\t" + (s.header ? "<div id=" + i + ' class="dhx_alert__header"> ' + s.header + " </div>" : "") + "\n\t\t\t" + (s.text ? "<div id=" + t + ' class="dhx_alert__content">' + s.text + "</div>" : "") + '\n\t\t\t<div class="dhx_alert__footer ' + (s.buttonsAlignment ? "dhx_alert__footer--" + s.buttonsAlignment : "") + '">\n\t\t\t\t<button type="button" aria-label="confirm" class="dhx_alert__apply-button dhx_button dhx_button--view_flat dhx_button--color_primary dhx_button--size_medium">' + a + "</button>\n\t\t\t</div>", document.body.appendChild(n), n.querySelector(".dhx_alert__apply-button").focus(), n.querySelector("button").addEventListener("click", function (t) {
						r(t), e(!0)
					}), document.addEventListener("keydown", o)
				})
			}
		}).call(this, o(15))
	}, function (t, o, r) {
		(function (t) {
			var e = void 0 !== t && t || "undefined" != typeof self && self || window,
				i = Function.prototype.apply;

			function n(t, e) {
				this._id = t, this._clearFn = e
			}
			o.setTimeout = function () {
				return new n(i.call(setTimeout, e, arguments), clearTimeout)
			}, o.setInterval = function () {
				return new n(i.call(setInterval, e, arguments), clearInterval)
			}, o.clearTimeout = o.clearInterval = function (t) {
				t && t.close()
			}, n.prototype.unref = n.prototype.ref = function () { }, n.prototype.close = function () {
				this._clearFn.call(e, this._id)
			}, o.enroll = function (t, e) {
				clearTimeout(t._idleTimeoutId), t._idleTimeout = e
			}, o.unenroll = function (t) {
				clearTimeout(t._idleTimeoutId), t._idleTimeout = -1
			}, o._unrefActive = o.active = function (t) {
				clearTimeout(t._idleTimeoutId);
				var e = t._idleTimeout;
				0 <= e && (t._idleTimeoutId = setTimeout(function () {
					t._onTimeout && t._onTimeout()
				}, e))
			}, r(134), o.setImmediate = "undefined" != typeof self && self.setImmediate || void 0 !== t && t.setImmediate || this && this.setImmediate, o.clearImmediate = "undefined" != typeof self && self.clearImmediate || void 0 !== t && t.clearImmediate || this && this.clearImmediate
		}).call(this, r(39))
	}, function (t, e, i) {
		(function (t, p) {
			! function (i, n) {
				"use strict";
				var o, r, s, a, l, c, e, u, t;

				function d(t) {
					delete r[t]
				}

				function h(t) {
					if (s) setTimeout(h, 0, t);
					else {
						var e = r[t];
						if (e) {
							s = !0;
							try {
								! function (t) {
									var e = t.callback,
										i = t.args;
									switch (i.length) {
										case 0:
											e();
											break;
										case 1:
											e(i[0]);
											break;
										case 2:
											e(i[0], i[1]);
											break;
										case 3:
											e(i[0], i[1], i[2]);
											break;
										default:
											e.apply(n, i)
									}
								}(e)
							} finally {
								d(t), s = !1
							}
						}
					}
				}

				function f(t) {
					t.source === i && "string" == typeof t.data && 0 === t.data.indexOf(u) && h(+t.data.slice(u.length))
				}
				i.setImmediate || (o = 1, s = !(r = {}), a = i.document, t = (t = Object.getPrototypeOf && Object.getPrototypeOf(i)) && t.setTimeout ? t : i, l = "[object process]" === {}.toString.call(i.process) ? function (t) {
					p.nextTick(function () {
						h(t)
					})
				} : function () {
					if (i.postMessage && !i.importScripts) {
						var t = !0,
							e = i.onmessage;
						return i.onmessage = function () {
							t = !1
						}, i.postMessage("", "*"), i.onmessage = e, t
					}
				}() ? (u = "setImmediate$" + Math.random() + "$", i.addEventListener ? i.addEventListener("message", f, !1) : i.attachEvent("onmessage", f), function (t) {
					i.postMessage(u + t, "*")
				}) : i.MessageChannel ? ((e = new MessageChannel).port1.onmessage = function (t) {
					h(t.data)
				}, function (t) {
					e.port2.postMessage(t)
				}) : a && "onreadystatechange" in a.createElement("script") ? (c = a.documentElement, function (t) {
					var e = a.createElement("script");
					e.onreadystatechange = function () {
						h(t), e.onreadystatechange = null, c.removeChild(e), e = null
					}, c.appendChild(e)
				}) : function (t) {
					setTimeout(h, 0, t)
				}, t.setImmediate = function (t) {
					"function" != typeof t && (t = new Function("" + t));
					for (var e = new Array(arguments.length - 1), i = 0; i < e.length; i++) e[i] = arguments[i + 1];
					return t = {
						callback: t,
						args: e
					}, r[o] = t, l(o), o++
				}, t.clearImmediate = d)
			}("undefined" == typeof self ? void 0 === t ? this : t : self)
		}).call(this, i(39), i(135))
	}, function (t, e) {
		var i, n, t = t.exports = {};

		function o() {
			throw new Error("setTimeout has not been defined")
		}

		function r() {
			throw new Error("clearTimeout has not been defined")
		}

		function s(e) {
			if (i === setTimeout) return setTimeout(e, 0);
			if ((i === o || !i) && setTimeout) return i = setTimeout, setTimeout(e, 0);
			try {
				return i(e, 0)
			} catch (t) {
				try {
					return i.call(null, e, 0)
				} catch (t) {
					return i.call(this, e, 0)
				}
			}
		} ! function () {
			try {
				i = "function" == typeof setTimeout ? setTimeout : o
			} catch (t) {
				i = o
			}
			try {
				n = "function" == typeof clearTimeout ? clearTimeout : r
			} catch (t) {
				n = r
			}
		}();
		var a, l = [],
			c = !1,
			u = -1;

		function d() {
			c && a && (c = !1, a.length ? l = a.concat(l) : u = -1, l.length && h())
		}

		function h() {
			if (!c) {
				var t = s(d);
				c = !0;
				for (var e = l.length; e;) {
					for (a = l, l = []; ++u < e;) a && a[u].run();
					u = -1, e = l.length
				}
				a = null, c = !1,
					function (e) {
						if (n === clearTimeout) return clearTimeout(e);
						if ((n === r || !n) && clearTimeout) return n = clearTimeout, clearTimeout(e);
						try {
							n(e)
						} catch (t) {
							try {
								return n.call(null, e)
							} catch (t) {
								return n.call(this, e)
							}
						}
					}(t)
			}
		}

		function f(t, e) {
			this.fun = t, this.array = e
		}

		function p() { }
		t.nextTick = function (t) {
			var e = new Array(arguments.length - 1);
			if (1 < arguments.length)
				for (var i = 1; i < arguments.length; i++) e[i - 1] = arguments[i];
			l.push(new f(t, e)), 1 !== l.length || c || s(h)
		}, f.prototype.run = function () {
			this.fun.apply(null, this.array)
		}, t.title = "browser", t.browser = !0, t.env = {}, t.argv = [], t.version = "", t.versions = {}, t.on = p, t.addListener = p, t.once = p, t.off = p, t.removeListener = p, t.removeAllListeners = p, t.emit = p, t.prependListener = p, t.prependOnceListener = p, t.listeners = function (t) {
			return []
		}, t.binding = function (t) {
			throw new Error("process.binding is not supported")
		}, t.cwd = function () {
			return "/"
		}, t.chdir = function (t) {
			throw new Error("process.chdir is not supported")
		}, t.umask = function () {
			return 0
		}
	}, function (t, n, o) {
		"use strict";
		(function (t) {
			Object.defineProperty(n, "__esModule", {
				value: !0
			});
			var e = o(40),
				i = o(58),
				h = o(1);
			n.confirm = function (l) {
				l.buttonsAlignment = l.buttonsAlignment || "right";
				var c = l.buttons && l.buttons[1] ? l.buttons[1] : e.default.apply,
					u = l.buttons && l.buttons[0] ? l.buttons[0] : e.default.reject,
					d = i.blockScreen(l.blockerCss);
				return new t(function (e) {
					var i = document.createElement("div");
					i.setAttribute("role", "alertdialog"), i.setAttribute("aria-modal", "true");
					var n, t = l.header && h.uid(),
						o = l.header && h.uid();
					o && i.setAttribute("aria-describedby", o), t && i.setAttribute("aria-labelledby", t);

					function r(t) {
						d(), i.removeEventListener("click", s), document.removeEventListener("keydown", a), document.body.removeChild(i), e(t)
					}
					var s = function (t) {
						"BUTTON" === t.target.tagName && r(t.target.classList.contains("dhx_alert__confirm-aply"))
					},
						a = function (t) {
							"Escape" === t.key || "Esc" === t.key ? (i.querySelector(".dhx_alert__confirm-aply").focus(), r(t.target.classList.contains("dhx_alert__confirm-reject"))) : "Tab" === t.key && ("aply" === n ? (n = "reject", i.querySelector(".dhx_alert__confirm-reject").focus()) : (n = "aply", i.querySelector(".dhx_alert__confirm-aply").focus()), t.preventDefault())
						};
					i.className = "dhx_widget dhx_alert dhx_alert--confirm" + (l.css ? " " + l.css : ""), i.innerHTML = "\n\t\t" + (l.header ? '<div class="dhx_alert__header" id=' + t + "> " + l.header + " </div>" : "") + "\n\t\t" + (l.text ? '<div class="dhx_alert__content" id=' + o + ">" + l.text + "</div>" : "") + '\n\t\t\t<div class="dhx_alert__footer ' + (l.buttonsAlignment ? "dhx_alert__footer--" + l.buttonsAlignment : "") + '">\n\t\t\t\t<button type="button" aria-label="reject" class="dhx_alert__confirm-reject dhx_button dhx_button--view_link dhx_button--color_primary dhx_button--size_medium">' + u + '</button>\n\t\t\t\t<button type="button"  aria-label="aply"class="dhx_alert__confirm-aply dhx_button dhx_button--view_flat dhx_button--color_primary dhx_button--size_medium">' + c + "</button>\n\t\t\t</div>", document.body.appendChild(i), n = "aply", i.querySelector(".dhx_alert__confirm-aply").focus(), i.addEventListener("click", s), document.addEventListener("keydown", a)
				})
			}
		}).call(this, o(15))
	}, function (t, e, i) {
		//"use strict";
		//i.r(e);
		//var n, s = i(10),
		//	e = i(60),
		//	o = ["contact@dhtmlx.com</a> or visit <a style='color: #0288d1;text-decoration: unset;' href='https://dhtmlx.com/docs/products/licenses.shtml?expand=1&utm_source=trial&utm_medium=", "1638427507000", "https://dhtmlx.com/docs/products/licenses.shtml?expand=1&utm_source=trial&utm_medium=popup&utm_campaign=second_month#suite", "random", "popup&utm_campaign=second_month#suite' target='_blank'>dhtmlx.com</a> in order to obtain a proper license.", "<svg class='dhx_alert__header--icon' xmlns='http://www.w3.org/2000/sv' xmlns:xlink='http://www.w3.org/1999/xlink' version='1.1' viewBox='0 0 24 24'><path d='M11,15H13V17H", "now", "right", "floor", "open", "_blank", "Please contact us at <a style='color: #0288d1;text-decoration: unset;' href='mailto:contact@dhtmlx.com?subject=DHTMLX Licensing - Trial End' target='_blank'>", "dhx_547239261_alert", "16.42 16.42,20 12,20Z'></path></svg><div class='dhx_alert__header--text'>Your evaluation period for DHTMLX has expired</div>"];
		//n = o,
		//	function (t) {
		//		for (; --t;) n.push(n.shift())
		//	}(148);

		//function a(t, e) {
		//	return o[t -= 257]
		//}
		//setTimeout(function () {
		//	var t, e, i, n = a,
		//		o = n(269) + "11V15M11,7H13V13H11V7M12,2C6.47,2 2,6.5 2,12C2,17.52 6.48,22 12,22C17.52,22 22,17.52 22,12C22,6.48 17.52,2 12,2M12,20C7.58,20 4,16.42 4,12C4,7.58 7.58,4 12,4C16.42,4 20,7.58 20,12C20," + n(263),
		//		r = n(261) + n(264) + n(268);
		//	void 0 !== n(265) && setInterval(function () {
		//		var t, e = n;
		//		t = n, 5184e6 < Date[t(270)]() - t(265) && Object(s.alert)({
		//			header: o,
		//			text: r,
		//			buttonsAlignment: e(257),
		//			buttons: ["check licensing"],
		//			css: e(262)
		//		}).then(function () {
		//			var t = e;
		//			window[t(259)](t(266), t(260))
		//		})
		//	}, (t = 6e4, e = 12e4, i = n, Math[i(258)](Math[i(267)]() * (e - t + 1)) + t))
		//}, 1)
	}, function (t, e) {
		//var i, n = ["1638427507000", "random", "undefined", "floor", "now"];
		//i = n,
		//	function (t) {
		//		for (; --t;) i.push(i.shift())
		//	}(179);

		//function o(t, e) {
		//	return n[t -= 168]
		//}
		//setTimeout(function () {
		//	var t, e, i, n = o;
		//	typeof n(170) !== n(172) && setInterval(function () {
		//		var t;
		//		t = n, 7776e6 < Date[t(169)]() - t(170) && alert("Your evaluation period for DHTMLX has expired.\nPlease contact us at contact@dhtmlx.com or visit dhtmlx.com in order to obtain a proper license.")
		//	}, (t = 6e4, e = 12e4, i = n, Math[i(168)](Math[i(171)]() * (e - t + 1)) + t))
		//}, 1)
	}, function (t, i, e) {
		"use strict";
		Object.defineProperty(i, "__esModule", {
			value: !0
		}),
			function (t) {
				for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
			}(e(140));
		var n = e(6);
		i.dataDrivers = n.dataDriversPro, i.LazyDataProxy = n.LazyDataProxy;
		n = e(120);
		i.TreeGridCollection = n.TreeGridCollection;
		n = e(17);
		i.ScrollView = n.ScrollView, i.scrollViewConfig = n.scrollViewConfig;
		n = e(120);
		i.TreeGrid = n.TreeGrid;
		n = e(27);
		i.Grid = n.ProGrid;
		n = e(28);
		i.List = n.ProList;
		n = e(24);
		i.Combobox = n.ProCombobox;
		n = e(90);
		i.DataView = n.ProDataView;
		n = e(11);
		i.Layout = n.ProLayout;
		n = e(25);
		i.Toolbar = n.ProToolbar;
		n = e(111);
		i.Sidebar = n.ProSidebar;
		n = e(109);
		i.Ribbon = n.ProRibbon;
		n = e(244);
		i.Pagination = n.Pagination;
		n = e(117);
		i.Window = n.ProWindow;
		e = e(93);
		i.Form = e.ProForm
	}, function (t, o, e) {
		"use strict";
		Object.defineProperty(o, "__esModule", {
			value: !0
		}), e(141);
		var i = e(142);
		o.cssManager = i.cssManager;
		var n = e(3);
		o.EventSystem = n.EventSystem;
		var r = e(0);
		o.awaitRedraw = r.awaitRedraw, o.resizeHandler = r.resizeHandler;
		var s = e(61);
		o.Uploader = s.Uploader;
		var a = e(6);
		o.DataCollection = a.DataCollection, o.TreeCollection = a.TreeCollection, o.DataProxy = a.DataProxy, o.dataDrivers = a.dataDrivers, o.ajax = a.ajax;
		var l = e(11);
		o.Layout = l.Layout;
		i = e(28);
		o.List = i.List;
		n = e(34);
		o.Calendar = n.Calendar;
		r = e(49);
		o.Colorpicker = r.Colorpicker;
		s = e(203);
		o.Chart = s.Chart;
		a = e(24);
		o.Combobox = a.Combobox;
		l = e(90);
		o.DataView = l.DataView;
		i = e(93);
		o.Form = i.Form;
		n = e(27);
		o.Grid = n.Grid;
		r = e(10);
		o.message = r.message, o.alert = r.alert, o.confirm = r.confirm, o.enableTooltip = r.enableTooltip, o.disableTooltip = r.disableTooltip, o.tooltip = r.tooltip;
		s = e(231);
		o.Menu = s.Menu, o.ContextMenu = s.ContextMenu;
		a = e(12);
		o.Popup = a.Popup;
		l = e(109);
		o.Ribbon = l.Ribbon;
		i = e(111);
		o.Sidebar = i.Sidebar;
		n = e(45);
		o.Slider = n.Slider;
		r = e(236);
		o.Tabbar = r.Tabbar;
		s = e(43);
		o.Timepicker = s.Timepicker;
		a = e(25);
		o.Toolbar = a.Toolbar;
		l = e(238);
		o.Tree = l.Tree;
		i = e(117);
		o.Window = i.Window;
		n = e(49), r = e(40), s = e(23), a = e(47), l = e(105), i = e(73), e = window;
		o.i18n = e.dhx && e.dhx.i18n ? e.dhx.i18 : {}, o.i18n.setLocale = function (t, e) {
			var i, n = o.i18n[t];
			for (i in e) n[i] = e[i]
		}, o.i18n.colorpicker = o.i18n.colorpicker || n.locale, o.i18n.message = o.i18n.message || r.default, o.i18n.calendar = o.i18n.calendar || s.locale, o.i18n.combobox = o.i18n.combobox || a.default, o.i18n.form = o.i18n.form || l.default, o.i18n.timepicker = o.i18n.timepicker || i.default
	}, function (t, e, i) { }, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			i = (o.prototype.update = function () {
				this._styleCont.innerHTML !== this._generateCss() && (document.head.appendChild(this._styleCont), this._styleCont.innerHTML = this._generateCss())
			}, o.prototype.remove = function (t) {
				delete this._classes[t], this.update()
			}, o.prototype.add = function (t, e, i) {
				void 0 === i && (i = !1);
				var n = this._toCssString(t),
					t = this._findSameClassId(n);
				return t && e && e !== t ? (this._classes[e] = this._classes[t], e) : t || this._addNewClass(n, e, i)
			}, o.prototype.get = function (t) {
				if (this._classes[t]) {
					for (var e = {}, i = 0, n = this._classes[t].split(";"); i < n.length; i++) {
						var o = n[i];
						o && (e[(o = o.split(":"))[0]] = o[1])
					}
					return e
				}
				return null
			}, o.prototype.destructor = function () {
				this._classes = this._styleCont = null
			}, o.prototype._findSameClassId = function (t) {
				for (var e in this._classes)
					if (t === this._classes[e]) return e;
				return null
			}, o.prototype._addNewClass = function (t, e, i) {
				e = e || "dhx_generated_class_" + n.uid();
				return this._classes[e] = t, i || this.update(), e
			}, o.prototype._toCssString = function (t) {
				var e, i = "";
				for (e in t) {
					var n = t[e];
					i += e.replace(/[A-Z]{1}/g, function (t) {
						return "-" + t.toLowerCase()
					}) + ":" + n + ";"
				}
				return i
			}, o.prototype._generateCss = function () {
				var t, e = "";
				for (t in this._classes) e += "." + t + "{" + this._classes[t] + "}\n";
				return e
			}, o);

		function o() {
			this._classes = {};
			var t = document.createElement("style");
			t.id = "dhx_generated_styles", this._styleCont = document.head.appendChild(t)
		}
		e.CssManager = i, e.cssManager = new i
	}, function (t, e, i) {
		/**
		 * Copyright (c) 2017, Leon Sorokin
		 * All rights reserved. (MIT Licensed)
		 *
		 * domvm.js (DOM ViewModel)
		 * A thin, fast, dependency-free vdom view layer
		 * @preserve https://github.com/leeoniya/domvm (v3.2.6, micro build)
		 */
		t.exports = function () {
			"use strict";
			var I = 1,
				l = 2,
				P = 3,
				O = 4,
				M = 5,
				t = typeof window !== "undefined",
				e, r = (t ? window : {}).requestAnimationFrame,
				c = {};

			function i() { }
			var p = Array.isArray;

			function u(t) {
				return t != null
			}

			function s(t) {
				return t != null && t.constructor === Object
			}

			function o(t, e, i, n) {
				t.splice.apply(t, [i, n].concat(e))
			}

			function a(t) {
				var e = typeof t;
				return e === "string" || e === "number"
			}

			function d(t) {
				return typeof t === "function"
			}

			function h(t) {
				return typeof t === "object" && d(t.then)
			}

			function f(t) {
				var e = arguments;
				for (var i = 1; i < e.length; i++)
					for (var n in e[i]) t[n] = e[i][n];
				return t
			}

			function _(t, e, i) {
				var n;
				while (n = e.shift())
					if (e.length === 0) t[n] = i;
					else t[n] = t = t[n] || {}
			}

			function v(t, e) {
				var i = [];
				for (var n = e; n < t.length; n++) i.push(t[n]);
				return i
			}

			function g(t, e) {
				for (var i in t)
					if (t[i] !== e[i]) return false;
				return true
			}

			function m(t, e) {
				var i = t.length;
				if (e.length !== i) return false;
				for (var n = 0; n < i; n++)
					if (t[n] !== e[n]) return false;
				return true
			}

			function y(t) {
				if (!r) return t;
				var e, i, n;

				function o() {
					e = 0;
					t.apply(i, n)
				}
				return function () {
					i = this;
					n = arguments;
					if (!e) e = r(o)
				}
			}

			function b(t, e, i) {
				return function () {
					return t.apply(i, e)
				}
			}

			function w(t) {
				var e = t.slice();
				var i = [];
				i.push(0);
				var n;
				var o;
				for (var r = 0, s = t.length; r < s; ++r) {
					var a = i[i.length - 1];
					if (t[a] < t[r]) {
						e[r] = a;
						i.push(r);
						continue
					}
					n = 0;
					o = i.length - 1;
					while (n < o) {
						var l = (n + o) / 2 | 0;
						if (t[i[l]] < t[r]) n = l + 1;
						else o = l
					}
					if (t[r] < t[i[n]]) {
						if (n > 0) e[r] = i[n - 1];
						i[n] = r
					}
				}
				n = i.length;
				o = i[n - 1];
				while (n-- > 0) {
					i[n] = o;
					o = e[o]
				}
				return i
			}

			function x(t, e) {
				var i = 0;
				var n = e.length - 1;
				var o;
				var r = n <= 2147483647 ? true : false;
				if (r)
					while (i <= n) {
						o = i + n >> 1;
						if (e[o] === t) return o;
						else if (e[o] < t) i = o + 1;
						else n = o - 1
					} else
					while (i <= n) {
						o = Math.floor((i + n) / 2);
						if (e[o] === t) return o;
						else if (e[o] < t) i = o + 1;
						else n = o - 1
					}
				return i == e.length ? null : i
			}

			function E(t) {
				return t[0] === "o" && t[1] === "n"
			}

			function C(t) {
				return t[0] === "_"
			}

			function k(t) {
				return t === "style"
			}

			function S(t) {
				t && t.el && t.el.offsetHeight
			}

			function V(t) {
				return t.node != null && t.node.el != null
			}

			function D(t, e) {
				switch (e) {
					case "value":
					case "checked":
					case "selected":
						return true
				}
				return false
			}

			function T(t) {
				t = t || c;
				while (t.vm == null && t.parent) t = t.parent;
				return t.vm
			}

			function H() { }
			var n = H.prototype = {
				constructor: H,
				type: null,
				vm: null,
				key: null,
				ref: null,
				data: null,
				hooks: null,
				ns: null,
				el: null,
				tag: null,
				attrs: null,
				body: null,
				flags: 0,
				_class: null,
				_diff: null,
				_dead: false,
				_lis: false,
				idx: null,
				parent: null
			};

			function F(t) {
				var e = new H;
				e.type = l;
				e.body = t;
				return e
			}
			var j = {},
				L = /\[(\w+)(?:=(\w+))?\]/g;

			function R(t) {
				{
					var e = j[t];
					if (e == null) {
						var i, n, o, r;
						j[t] = e = {
							tag: (i = t.match(/^[-\w]+/)) ? i[0] : "div",
							id: (n = t.match(/#([-\w]+)/)) ? n[1] : null,
							class: (o = t.match(/\.([-\w.]+)/)) ? o[1].replace(/\./g, " ") : null,
							attrs: null
						};
						while (r = L.exec(t)) {
							if (e.attrs == null) e.attrs = {};
							e.attrs[r[1]] = r[2] || ""
						}
					}
					return e
				}
			}
			var A = 1,
				$ = 2,
				N = 4,
				z = 8;

			function W(t, e, i, n) {
				var o = new H;
				o.type = I;
				if (u(n)) o.flags = n;
				o.attrs = e;
				var r = R(t);
				o.tag = r.tag;
				if (r.id || r.class || r.attrs) {
					var s = o.attrs || {};
					if (r.id && !u(s.id)) s.id = r.id;
					if (r.class) {
						o._class = r.class;
						s.class = r.class + (u(s.class) ? " " + s.class : "")
					}
					if (r.attrs)
						for (var a in r.attrs)
							if (!u(s[a])) s[a] = r.attrs[a];
					o.attrs = s
				}
				var l = o.attrs;
				if (u(l)) {
					if (u(l._key)) o.key = l._key;
					if (u(l._ref)) o.ref = l._ref;
					if (u(l._hooks)) o.hooks = l._hooks;
					if (u(l._data)) o.data = l._data;
					if (u(l._flags)) o.flags = l._flags;
					if (!u(o.key))
						if (u(o.ref)) o.key = o.ref;
						else if (u(l.id)) o.key = l.id;
						else if (u(l.name)) o.key = l.name + (l.type === "radio" || l.type === "checkbox" ? l.value : "")
				}
				if (i != null) o.body = i;
				return o
			}

			function B(t, e, i) {
				var n = ["refs"].concat(e.split("."));
				_(t, n, i)
			}

			function G(t) {
				while (t = t.parent) t.flags |= A
			}

			function q(t, e, i, n) {
				if (t.type === M || t.type === O) return;
				t.parent = e;
				t.idx = i;
				t.vm = n;
				if (t.ref != null) B(T(t), t.ref, t);
				var o = t.hooks,
					r = n && n.hooks;
				if (o && (o.willRemove || o.didRemove) || r && (r.willUnmount || r.didUnmount)) G(t);
				if (p(t.body)) Y(t);
				else;
			}

			function Y(t) {
				var e = t.body;
				for (var i = 0; i < e.length; i++) {
					var n = e[i];
					if (n === false || n == null) e.splice(i--, 1);
					else if (p(n)) o(e, n, i--, 1);
					else {
						if (n.type == null) e[i] = n = F("" + n);
						if (n.type === l)
							if (n.body == null || n.body === "") e.splice(i--, 1);
							else if (i > 0 && e[i - 1].type === l) {
								e[i - 1].body += n.body;
								e.splice(i--, 1)
							} else q(n, t, i, null);
						else q(n, t, i, null)
					}
				}
			}
			var U = {
				animationIterationCount: true,
				boxFlex: true,
				boxFlexGroup: true,
				boxOrdinalGroup: true,
				columnCount: true,
				flex: true,
				flexGrow: true,
				flexPositive: true,
				flexShrink: true,
				flexNegative: true,
				flexOrder: true,
				gridRow: true,
				gridColumn: true,
				order: true,
				lineClamp: true,
				borderImageOutset: true,
				borderImageSlice: true,
				borderImageWidth: true,
				fontWeight: true,
				lineHeight: true,
				opacity: true,
				orphans: true,
				tabSize: true,
				widows: true,
				zIndex: true,
				zoom: true,
				fillOpacity: true,
				floodOpacity: true,
				stopOpacity: true,
				strokeDasharray: true,
				strokeDashoffset: true,
				strokeMiterlimit: true,
				strokeOpacity: true,
				strokeWidth: true
			};

			function X(t, e) {
				return !isNaN(e) && !U[t] ? e + "px" : e
			}

			function K(t, e) {
				var i = (t.attrs || c).style;
				var n = e ? (e.attrs || c).style : null;
				if (i == null || a(i)) t.el.style.cssText = i;
				else {
					for (var o in i) {
						var r = i[o];
						if (n == null || r != null && r !== n[o]) t.el.style[o] = X(o, r)
					}
					if (n)
						for (var s in n)
							if (i[s] == null) t.el.style[s] = ""
				}
			}
			var J = [];

			function Z(t, e, i, n, o) {
				if (t != null) {
					var r = i.hooks[e];
					if (r)
						if (e[0] === "d" && e[1] === "i" && e[2] === "d") o ? S(i.parent) && r(i, n) : J.push([r, i, n]);
						else return r(i, n)
				}
			}

			function Q(t) {
				if (J.length) {
					S(t.node);
					var e;
					while (e = J.shift()) e[0](e[1], e[2])
				}
			}
			var tt = t ? document : null;

			function et(t) {
				while (t._node == null) t = t.parentNode;
				return t._node
			}

			function it(t, e) {
				if (e != null) return tt.createElementNS(e, t);
				return tt.createElement(t)
			}

			function nt(t) {
				return tt.createTextNode(t)
			}

			function ot(t) {
				return tt.createComment(t)
			}

			function rt(t) {
				return t.nextSibling
			}

			function st(t) {
				return t.previousSibling
			}

			function at(t) {
				var e = t.vm;
				var i = e != null && Z(e.hooks, "willUnmount", e, e.data);
				var n = Z(t.hooks, "willRemove", t);
				if ((t.flags & A) === A && p(t.body))
					for (var o = 0; o < t.body.length; o++) at(t.body[o]);
				return i || n
			}

			function lt(t, e, i) {
				var n = e._node,
					o = n.vm;
				if (p(n.body))
					if ((n.flags & A) === A)
						for (var r = 0; r < n.body.length; r++) lt(e, n.body[r].el);
					else ut(n);
				delete e._node;
				t.removeChild(e);
				Z(n.hooks, "didRemove", n, null, i);
				if (o != null) {
					Z(o.hooks, "didUnmount", o, o.data, i);
					o.node = null
				}
			}

			function ct(t, e) {
				var i = e._node;
				if (i._dead) return;
				var n = at(i);
				if (n != null && h(n)) {
					i._dead = true;
					n.then(b(lt, [t, e, true]))
				} else lt(t, e)
			}

			function ut(t) {
				var e = t.body;
				for (var i = 0; i < e.length; i++) {
					var n = e[i];
					delete n.el._node;
					if (n.vm != null) n.vm.node = null;
					if (p(n.body)) ut(n)
				}
			}

			function dt(t) {
				var e = t.el;
				if ((t.flags & A) === 0) {
					p(t.body) && ut(t);
					e.textContent = null
				} else {
					var i = e.firstChild;
					do {
						var n = rt(i);
						ct(e, i)
					} while (i = n)
				}
			}

			function ht(t, e, i) {
				var n = e._node,
					o = e.parentNode != null;
				var r = e === i || !o ? n.vm : null;
				if (r != null) Z(r.hooks, "willMount", r, r.data);
				Z(n.hooks, o ? "willReinsert" : "willInsert", n);
				t.insertBefore(e, i);
				Z(n.hooks, o ? "didReinsert" : "didInsert", n);
				if (r != null) Z(r.hooks, "didMount", r, r.data)
			}

			function ft(t, e, i) {
				ht(t, e, i ? rt(i) : null)
			}
			var pt = {};

			function _t(t) {
				f(pt, t)
			}

			function vt(t) {
				var e = this,
					i = e,
					n = v(arguments, 1).concat(i, i.data);
				do {
					var o = e.onemit,
						o = o ? o[t] : null;
					if (o) {
						o.apply(e, n);
						break
					}
				} while (e = e.parent());
				if (pt[t]) pt[t].apply(e, n)
			}
			var gt = i;

			function mt(t) {
				gt = t.onevent || gt;
				if (t.onemit) _t(t.onemit)
			}

			function yt(t, e, i) {
				t[e] = i
			}

			function bt(t, e, i, n, o) {
				var r = t.apply(o, e.concat([i, n, o, o.data]));
				o.onevent(i, n, o, o.data, e);
				gt.call(null, i, n, o, o.data, e);
				if (r === false) {
					i.preventDefault();
					i.stopPropagation()
				}
			}

			function wt(t) {
				var e = et(t.target);
				var i = T(e);
				var n = t.currentTarget._node.attrs["on" + t.type],
					o, r;
				if (p(n)) {
					o = n[0];
					r = n.slice(1);
					bt(o, r, t, e, i)
				} else
					for (var s in n)
						if (t.target.matches(s)) {
							var a = n[s];
							if (p(a)) {
								o = a[0];
								r = a.slice(1)
							} else {
								o = a;
								r = []
							}
							bt(o, r, t, e, i)
						}
			}

			function xt(t, e, i, n) {
				if (i === n) return;
				var o = t.el;
				if (i == null || d(i)) yt(o, e, i);
				else if (n == null) yt(o, e, wt)
			}

			function Et(t, e, i) {
				if (e[0] === ".") {
					e = e.substr(1);
					i = true
				}
				if (i) t.el[e] = "";
				else t.el.removeAttribute(e)
			}

			function Ct(t, e, i, n, o) {
				var r = t.el;
				if (i == null) !o && Et(t, e, false);
				else if (t.ns != null) r.setAttribute(e, i);
				else if (e === "class") r.className = i;
				else if (e === "id" || typeof i === "boolean" || n) r[e] = i;
				else if (e[0] === ".") r[e.substr(1)] = i;
				else r.setAttribute(e, i)
			}

			function kt(t, e, i) {
				var n = t.attrs || c;
				var o = e.attrs || c;
				if (n === o);
				else {
					for (var r in n) {
						var s = n[r];
						var a = D(t.tag, r);
						var l = a ? t.el[r] : o[r];
						if (s === l);
						else if (k(r)) K(t, e);
						else if (C(r));
						else if (E(r)) xt(t, r, s, l);
						else Ct(t, r, s, a, i)
					}
					for (var r in o) !(r in n) && !C(r) && Et(t, r, D(t.tag, r) || E(r))
				}
			}

			function St(t, e, i, n) {
				if (t.type === O) {
					e = t.data;
					i = t.key;
					n = t.opts;
					t = t.view
				}
				return new qt(t, e, i, n)
			}

			function It(t) {
				for (var e = 0; e < t.body.length; e++) {
					var i = t.body[e];
					var n = i.type;
					if (n <= P) ht(t.el, Pt(i));
					else if (n === O) {
						var o = St(i.view, i.data, i.key, i.opts)._redraw(t, e, false);
						n = o.node.type;
						ht(t.el, Pt(o.node))
					} else if (n === M) {
						var o = i.vm;
						o._redraw(t, e);
						n = o.node.type;
						ht(t.el, o.node.el)
					}
				}
			}

			function Pt(t, e) {
				if (t.el == null)
					if (t.type === I) {
						t.el = e || it(t.tag, t.ns);
						if (t.attrs != null) kt(t, c, true);
						if ((t.flags & z) === z) t.body.body(t);
						if (p(t.body)) It(t);
						else if (t.body != null && t.body !== "") t.el.textContent = t.body
					} else if (t.type === l) t.el = e || nt(t.body);
					else if (t.type === P) t.el = e || ot(t.body);
				t.el._node = t;
				return t.el
			}

			function Ot(t, e) {
				return e[t.idx + 1]
			}

			function Mt(t, e) {
				return e[t.idx - 1]
			}

			function Vt(t) {
				return t.parent
			}
			window.lisMove = Ft;
			var Dt = 1,
				Tt = 2;

			function Ht(l, c, u, d, h, f, p, _) {
				return function (t, e, i, n, o, r) {
					var s, a;
					if (n[d] != null) {
						if ((s = n[d]._node) == null) {
							n[d] = l(n[d]);
							return
						}
						if (Vt(s) !== t) {
							a = l(n[d]);
							s.vm != null ? s.vm.unmount(true) : ct(e, n[d]);
							n[d] = a;
							return
						}
					}
					if (n[h] == o) return Tt;
					else if (n[h].el == null) {
						u(e, Pt(n[h]), n[d]);
						n[h] = c(n[h], i)
					} else if (n[h].el === n[d]) {
						n[h] = c(n[h], i);
						n[d] = l(n[d])
					} else if (!r && s === n[p]) {
						a = n[d];
						n[d] = l(a);
						_(e, a, n[f]);
						n[f] = a
					} else {
						if (r && n[d] != null) return Ft(l, c, u, d, h, e, i, s, n);
						return Dt
					}
				}
			}

			function Ft(t, e, i, n, o, r, s, a, l) {
				if (a._lis) {
					i(r, l[o].el, l[n]);
					l[o] = e(l[o], s)
				} else {
					var c = x(a.idx, l.tombs);
					a._lis = true;
					var u = t(l[n]);
					i(r, l[n], c != null ? s[l.tombs[c]].el : c);
					if (c == null) l.tombs.push(a.idx);
					else l.tombs.splice(c, 0, a.idx);
					l[n] = u
				}
			}
			var jt = Ht(rt, Ot, ht, "lftSib", "lftNode", "rgtSib", "rgtNode", ft),
				Lt = Ht(st, Mt, ft, "rgtSib", "rgtNode", "lftSib", "lftNode", ht);

			function Rt(t, e) {
				var i = e.body,
					n = t.el,
					o = t.body,
					r = {
						lftNode: o[0],
						rgtNode: o[o.length - 1],
						lftSib: (i[0] || c).el,
						rgtSib: (i[i.length - 1] || c).el
					};
				t: while (1) {
					while (1) {
						var s = jt(t, n, o, r, null, false);
						if (s === Dt) break;
						if (s === Tt) break t
					}
					while (1) {
						var a = Lt(t, n, o, r, r.lftNode, false);
						if (a === Dt) break;
						if (a === Tt) break t
					}
					At(t, n, o, r);
					break
				}
			}

			function At(t, e, i, n) {
				var o = Array.prototype.slice.call(e.childNodes);
				var r = [];
				for (var s = 0; s < o.length; s++) {
					var a = o[s]._node;
					if (a.parent === t) r.push(a.idx)
				}
				var l = w(r).map(function (t) {
					return r[t]
				});
				for (var c = 0; c < l.length; c++) i[l[c]]._lis = true;
				n.tombs = l;
				while (1) {
					var u = jt(t, e, i, n, null, true);
					if (u === Tt) break
				}
			}

			function $t(t) {
				return t.el._node.parent !== t.parent
			}

			function Nt(t, e, i) {
				return e[i]
			}

			function zt(t, e, i) {
				for (; i < e.length; i++) {
					var n = e[i];
					if (n.vm != null) {
						if (t.type === O && n.vm.view === t.view && n.vm.key === t.key || t.type === M && n.vm === t.vm) return n
					} else if (!$t(n) && t.tag === n.tag && t.type === n.type && t.key === n.key && (t.flags & ~A) === (n.flags & ~A)) return n
				}
				return null
			}

			function Wt(t, e, i) {
				return e[e._keys[t.key]]
			}

			function Bt(t, e) {
				Z(e.hooks, "willRecycle", e, t);
				var i = t.el = e.el;
				var n = e.body;
				var o = t.body;
				i._node = t;
				if (t.type === l && o !== n) {
					i.nodeValue = o;
					return
				}
				if (t.attrs != null || e.attrs != null) kt(t, e, false);
				var r = p(n);
				var s = p(o);
				var a = (t.flags & z) === z;
				if (r) {
					if (s || a) Gt(t, e);
					else if (o !== n)
						if (o != null) i.textContent = o;
						else dt(e)
				} else if (s) {
					dt(e);
					It(t)
				} else if (o !== n)
					if (i.firstChild) i.firstChild.nodeValue = o;
					else i.textContent = o;
				Z(e.hooks, "didRecycle", e, t)
			}

			function Gt(t, e) {
				var i = t.body,
					n = i.length,
					o = e.body,
					r = o.length,
					s = (t.flags & z) === z,
					a = (t.flags & $) === $,
					l = (t.flags & N) === N,
					c = !a && t.type === I,
					u = true,
					d = l ? Wt : a || s ? Nt : zt;
				if (l) {
					var h = {};
					for (var f = 0; f < o.length; f++) h[o[f].key] = f;
					o._keys = h
				}
				if (c && n === 0) {
					dt(e);
					if (s) t.body = [];
					return
				}
				var p, _, v, g = 0,
					m = false,
					y = 0;
				if (s) {
					var b = {
						key: null
					};
					var w = Array(n)
				}
				for (var f = 0; f < n; f++) {
					if (s) {
						var x = false;
						var E = null;
						if (u) {
							if (l) b.key = i.key(f);
							p = d(b, o, y)
						}
						if (p != null) {
							v = p.idx;
							E = i.diff(f, p);
							if (E === true) {
								_ = p;
								_.parent = t;
								_.idx = f;
								_._lis = false
							} else x = true
						} else x = true;
						if (x) {
							_ = i.tpl(f);
							q(_, t, f);
							_._diff = E != null ? E : i.diff(f);
							if (p != null) Bt(_, p)
						} else;
						w[f] = _
					} else {
						var _ = i[f];
						var C = _.type;
						if (C <= P) {
							if (p = u && d(_, o, y)) {
								Bt(_, p);
								v = p.idx
							}
						} else if (C === O) {
							if (p = u && d(_, o, y)) {
								v = p.idx;
								var k = p.vm._update(_.data, t, f)
							} else var k = St(_.view, _.data, _.key, _.opts)._redraw(t, f, false);
							C = k.node.type
						} else if (C === M) {
							var S = V(_.vm);
							var k = _.vm._update(_.data, t, f, S);
							C = k.node.type
						}
					}
					if (!l && p != null) {
						if (v === y) {
							y++;
							if (y === r && n > r) {
								p = null;
								u = false
							}
						} else m = true;
						if (r > 100 && m && ++g % 10 === 0)
							while (y < r && $t(o[y])) y++
					}
				}
				if (s) t.body = w;
				c && Rt(t, e)
			}

			function qt(t, e, i, n) {
				var o = this;
				o.view = t;
				o.data = e;
				o.key = i;
				if (n) {
					o.opts = n;
					o.config(n)
				}
				var r = s(t) ? t : t.call(o, o, e, i, n);
				if (d(r)) o.render = r;
				else {
					o.render = r.render;
					o.config(r)
				}
				o._redrawAsync = y(function (t) {
					return o.redraw(true)
				});
				o._updateAsync = y(function (t) {
					return o.update(t, true)
				});
				o.init && o.init.call(o, o, o.data, o.key, n)
			}
			var Yt = qt.prototype = {
				constructor: qt,
				_diff: null,
				init: null,
				view: null,
				key: null,
				data: null,
				state: null,
				api: null,
				opts: null,
				node: null,
				hooks: null,
				onevent: i,
				refs: null,
				render: null,
				mount: Ut,
				unmount: Xt,
				config: function (t) {
					var e = this;
					if (t.init) e.init = t.init;
					if (t.diff) e.diff = t.diff;
					if (t.onevent) e.onevent = t.onevent;
					if (t.hooks) e.hooks = f(e.hooks || {}, t.hooks);
					if (t.onemit) e.onemit = f(e.onemit || {}, t.onemit)
				},
				parent: function () {
					return T(this.node.parent)
				},
				root: function () {
					var t = this.node;
					while (t.parent) t = t.parent;
					return t.vm
				},
				redraw: function (t) {
					var e = this;
					t ? e._redraw(null, null, V(e)) : e._redrawAsync();
					return e
				},
				update: function (t, e) {
					var i = this;
					e ? i._update(t, null, null, V(i)) : i._updateAsync(t);
					return i
				},
				_update: Zt,
				_redraw: Jt,
				_redrawAsync: null,
				_updateAsync: null
			};

			function Ut(t, e) {
				var i = this;
				if (e) {
					dt({
						el: t,
						flags: 0
					});
					i._redraw(null, null, false);
					if (t.nodeName.toLowerCase() !== i.node.tag) {
						Pt(i.node);
						ht(t.parentNode, i.node.el, t);
						t.parentNode.removeChild(t)
					} else ht(t.parentNode, Pt(i.node, t), t)
				} else {
					i._redraw(null, null);
					if (t) ht(t, i.node.el)
				}
				if (t) Q(i);
				return i
			}

			function Xt(t) {
				var e = this;
				var i = e.node;
				var n = i.el.parentNode;
				ct(n, i.el);
				if (!t) Q(e)
			}

			function Kt(t, e, i, n) {
				if (i != null) {
					i.body[n] = e;
					e.idx = n;
					e.parent = i;
					e._lis = false
				}
				return t
			}

			function Jt(t, e, i) {
				var n = t == null;
				var o = this;
				var r = o.node && o.node.el && o.node.el.parentNode;
				var s = o.node,
					a, l;
				if (o.diff != null) {
					a = o._diff;
					o._diff = l = o.diff(o, o.data);
					if (s != null) {
						var c = p(a) ? m : g;
						var u = a === l || c(a, l);
						if (u) return Kt(o, s, t, e)
					}
				}
				r && Z(o.hooks, "willRedraw", o, o.data);
				var d = o.render.call(o, o, o.data, a, l);
				if (d === s) return Kt(o, s, t, e);
				o.refs = null;
				if (o.key != null && d.key !== o.key) d.key = o.key;
				o.node = d;
				if (t) {
					q(d, t, e, o);
					t.body[e] = d
				} else if (s && s.parent) {
					q(d, s.parent, s.idx, o);
					s.parent.body[s.idx] = d
				} else q(d, null, null, o);
				if (i !== false)
					if (s)
						if (s.tag !== d.tag || s.key !== d.key) {
							s.vm = d.vm = null;
							var h = s.el.parentNode;
							var f = rt(s.el);
							ct(h, s.el);
							ht(h, Pt(d), f);
							s.el = d.el;
							d.vm = o
						} else Bt(d, s);
					else Pt(d);
				r && Z(o.hooks, "didRedraw", o, o.data);
				if (n && r) Q(o);
				return o
			}

			function Zt(t, e, i, n) {
				var o = this;
				if (t != null)
					if (o.data !== t) {
						Z(o.hooks, "willUpdate", o, t);
						o.data = t
					} return o._redraw(e, i, n)
			}

			function Qt(t, e, i, n) {
				var o, r;
				if (i == null)
					if (s(e)) o = e;
					else r = e;
				else {
					o = e;
					r = i
				}
				return W(t, o, r, n)
			}
			var te = "http://www.w3.org/2000/svg";

			function ee(t, e, i, n) {
				var o = Qt(t, e, i, n);
				o.ns = te;
				return o
			}

			function ie(t) {
				var e = new H;
				e.type = P;
				e.body = t;
				return e
			}

			function ne(t, e, i, n) {
				this.view = t;
				this.data = e;
				this.key = i;
				this.opts = n
			}

			function oe(t, e, i, n) {
				return new ne(t, e, i, n)
			}

			function re(t) {
				this.vm = t
			}

			function se(t) {
				return new re(t)
			}

			function ae(t) {
				var e = new H;
				e.type = I;
				e.el = e.key = t;
				return e
			}

			function le(r, s) {
				var o = r.length;
				var a = {
					items: r,
					length: o,
					key: function (t) {
						return s.key(r[t], t)
					},
					diff: function (t, e) {
						var i = s.diff(r[t], t);
						if (e == null) return i;
						var n = e._diff;
						var o = i === n || p(n) ? m(i, n) : g(i, n);
						return o || i
					},
					tpl: function (t) {
						return s.tpl(r[t], t)
					},
					map: function (t) {
						s.tpl = t;
						return a
					},
					body: function (t) {
						var e = Array(o);
						for (var i = 0; i < o; i++) {
							var n = a.tpl(i);
							n._diff = a.diff(i);
							e[i] = n;
							q(n, t, i)
						}
						t.body = e
					}
				};
				return a
			}
			ne.prototype = {
				constructor: ne,
				type: O,
				view: null,
				data: null,
				key: null,
				opts: null
			}, re.prototype = {
				constructor: re,
				type: M,
				vm: null
			};
			var ce = {
				config: mt,
				ViewModel: qt,
				VNode: H,
				createView: St,
				defineElement: Qt,
				defineSvgElement: ee,
				defineText: F,
				defineComment: ie,
				defineView: oe,
				injectView: se,
				injectElement: ae,
				lazyList: le,
				FIXED_BODY: $,
				DEEP_REMOVE: A,
				KEYED_LIST: N,
				LAZY_LIST: z
			};

			function ue(t, e) {
				! function (t, e, i) {
					{
						var n, o;
						null != e.type ? null == t.vm && (q(e, t.parent, t.idx, null), Bt(t.parent.body[t.idx] = e, t), i && S(e), Q(T(e))) : ((n = Object.create(t)).attrs = f({}, t.attrs), o = f(t.attrs, e), null != t._class && (e = o.class, o.class = null != e && "" !== e ? t._class + " " + e : t._class), kt(t, n), i && S(t))
					}
				}(this, t, e)
			}

			function de(t, e, i) {
				if (null != e.type) null == t.vm && (q(e, t.parent, t.idx, null), Bt(t.parent.body[t.idx] = e, t), i && S(e), Q(T(e)));
				else {
					var n = Object.create(t);
					(n = Object.create(t)).attrs = f({}, t.attrs);
					var o = f(t.attrs, e),
						s;
					null != t._class && (e = o.class, o.class = null != e && "" !== e ? t._class + " " + e : t._class), kt(t, n), i && S(t)
				}
			}

			function he(t, e) {
				var i = t.body;
				if (p(i))
					for (var n = 0; n < i.length; n++) {
						var o = i[n];
						if (o.vm != null) e.push(o.vm);
						else he(o, e)
					}
				return e
			}

			function fe(t) {
				var e = arguments;
				var i = e.length;
				var n, o;
				if (i > 1) {
					var r = 1;
					if (s(e[1])) {
						o = e[1];
						r = 2
					}
					if (i === r + 1 && (a(e[r]) || p(e[r]) || o && (o._flags & z) === z)) n = e[r];
					else n = v(e, r)
				}
				return W(t, o, n)
			}

			function pe() {
				var t = fe.apply(null, arguments);
				return t.ns = te, t
			}
			return n.patch = function (t, e) {
				! function (t, e, i) {
					{
						var n, o;
						null != e.type ? null == t.vm && (q(e, t.parent, t.idx, null), Bt(t.parent.body[t.idx] = e, t), i && S(e), Q(T(e))) : ((n = Object.create(t)).attrs = f({}, t.attrs), o = f(t.attrs, e), null != t._class && (e = o.class, o.class = null != e && "" !== e ? t._class + " " + e : t._class), kt(t, n), i && S(t))
					}
				}(this, t, e)
			}, Yt.emit = function (t) {
				var e = this,
					i = e,
					n = v(arguments, 1).concat(i, i.data);
				do {
					var o = e.onemit,
						o = o ? o[t] : null;
					if (o) {
						o.apply(e, n);
						break
					}
				} while (e = e.parent());
				pt[t] && pt[t].apply(e, n)
			}, Yt.onemit = null, Yt.body = function () {
				return function t(e, i) {
					var n = e.body;
					if (p(n))
						for (var o = 0; o < n.length; o++) {
							var r = n[o];
							null != r.vm ? i.push(r.vm) : t(r, i)
						}
					return i
				}(this.node, [])
			}, ce.defineElementSpread = fe, ce.defineSvgElementSpread = function () {
				var t = fe.apply(null, arguments);
				return t.ns = te, t
			}, ce
		}()
	}, function (t, i, n) {
		"use strict";
		(function (s) {
			var l = this && this.__assign || function () {
				return (l = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
			Object.defineProperty(i, "__esModule", {
				value: !0
			});
			var c = n(21),
				u = n(20),
				t = (e.prototype.load = function (t, e) {
					var i = this;
					if (!t.config || this._parent.events.fire(u.DataEvents.beforeLazyLoad, [])) return this._parent.loadData = t.load().then(function (t) {
						return t ? i.parse(t, e) : []
					}).catch(function (t) {
						i._parent.events.fire(u.DataEvents.loadError, [t])
					})
				}, e.prototype.parse = function (t, e) {
					var n = this;
					if (void 0 === e && (e = u.DataDriver.json), "json" !== e || c.hasJsonOrArrayStructure(t) || this._parent.events.fire(u.DataEvents.loadError, ["Uncaught SyntaxError: Unexpected end of input"]), !((t = (e = c.toDataDriver(e)).toJsonArray(t)) instanceof Array)) {
						var i = t.total_count - 1,
							o = t.from;
						if (t = t.data, 0 !== this._parent.getLength()) return t.forEach(function (t, e) {
							var i = o + e,
								e = n._parent.getId(i);
							e ? (i = n._parent.getItem(e)) && i.$empty && (n._parent.changeId(e, t.id, !0), n._parent.update(t.id, l(l({}, t), {
								$empty: void 0
							}), !0)) : c.dhxWarning("item not found")
						}), this._parent.events.fire(u.DataEvents.afterLazyLoad, [o, t.length]), this._parent.events.fire(u.DataEvents.change), t;
						for (var r = [], s = 0, a = 0; s <= i; s++) o <= s && s <= o + t.length - 1 ? (r.push(t[a]), a++) : r.push({
							$empty: !0
						});
						t = r
					}
					return this._parent.getInitialData() && this._parent.removeAll(), this._parent.$parse(t), t
				}, e.prototype.save = function (o) {
					for (var r = this, e = this, t = 0, i = this._changes.order; t < i.length; t++) ! function (i) {
						var n, t;
						i.saving || i.pending ? c.dhxWarning("item is saving") : (n = e._findPrevState(i.id)) && n.saving ? (t = new s(function (t, e) {
							n.promise.then(function () {
								i.pending = !1, t(r._setPromise(i, o))
							}).catch(function (t) {
								r._removeFromOrder(n), r._setPromise(i, o), c.dhxWarning(t), e(t)
							})
						}), e._addToChain(t), i.pending = !0) : e._setPromise(i, o)
					}(i[t]);
					this._changes.order.length && this._parent.saveData.then(function () {
						r._saving = !1
					})
				}, e.prototype.updateChanges = function (t) {
					this._changes = t
				}, e.prototype._setPromise = function (e, t) {
					var i, n = this;
					switch (e.status) {
						case "remove":
							i = "delete";
							break;
						case "add":
							i = "insert";
							break;
						default:
							i = e.status
					}
					return e.promise = t.save(e.obj, i), e.promise.then(function () {
						n._removeFromOrder(e)
					}).catch(function (t) {
						e.saving = !1, e.error = !0, c.dhxError(t)
					}), e.saving = !0, this._saving = !0, this._addToChain(e.promise), e.promise
				}, e.prototype._addToChain = function (t) {
					this._parent.saveData && this._saving ? this._parent.saveData = this._parent.saveData.then(function () {
						return t
					}) : this._parent.saveData = t
				}, e.prototype._findPrevState = function (t) {
					for (var e = 0, i = this._changes.order; e < i.length; e++) {
						var n = i[e];
						if (n.id === t) return n
					}
					return null
				}, e.prototype._removeFromOrder = function (e) {
					this._changes.order = this._changes.order.filter(function (t) {
						return !c.isEqualObj(t, e)
					})
				}, e);

			function e(t, e) {
				this._parent = t, this._changes = e
			}
			i.Loader = t
		}).call(this, n(15))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(146);
		o.prototype.toJsonArray = function (t) {
			return this.getRows(t)
		}, o.prototype.toJsonObject = function (t) {
			var e;
			return "string" == typeof t && (e = this._fromString(t)),
				function t(e, i) {
					i = i || {};
					var n = e.attributes;
					if (n && n.length)
						for (var o = 0; o < n.length; o++) i[n[o].name] = n[o].value;
					for (var r, s = e.childNodes, o = 0; o < s.length; o++) 1 === s[o].nodeType && (i[r = s[o].tagName] ? ("function" != typeof i[r].push && (i[r] = [i[r]]), i[r].push(t(s[o], {}))) : i[r] = t(s[o], {}));
					return i
				}(e)
		}, o.prototype.serialize = function (t) {
			return n.jsonToXML(t)
		}, o.prototype.getFields = function (t) {
			return t
		}, o.prototype.getRows = function (t) {
			if ("string" == typeof t && (t = this._fromString(t)), t) {
				t = t.childNodes && t.childNodes[0] && t.childNodes[0].childNodes;
				return t && t.length ? this._getRows(t) : null
			}
			return []
		}, o.prototype._getRows = function (t) {
			for (var e = [], i = 0; i < t.length; i++) "item" === t[i].tagName && e.push(this._nodeToJS(t[i]));
			return e
		}, o.prototype._fromString = function (t) {
			try {
				return (new DOMParser).parseFromString(t, "text/xml")
			} catch (t) {
				return null
			}
		}, o.prototype._nodeToJS = function (t) {
			var e = {};
			if (this._haveAttrs(t))
				for (var i = t.attributes, n = 0; n < i.length; n++) {
					var o = i[n],
						r = o.name,
						o = o.value;
					e[r] = this._toType(o)
				}
			if (3 === t.nodeType) return e.value = e.value || this._toType(t.textContent), e;
			var s = t.childNodes;
			if (s)
				for (n = 0; n < s.length; n++) {
					var a = s[n],
						l = a.tagName;
					l && ("items" === l && a.childNodes ? e[l] = this._getRows(a.childNodes) : this._haveAttrs(a) ? e[l] = this._nodeToJS(a) : e[l] = this._toType(a.textContent))
				}
			return e
		}, o.prototype._toType = function (t) {
			return "false" === t || "true" === t ? "true" === t : isNaN(t) ? t : Number(t)
		}, o.prototype._haveAttrs = function (t) {
			return t.attributes && t.attributes.length
		}, i = o;

		function o() { }
		e.XMLDriver = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = 4;

		function s(t) {
			return " ".repeat(t)
		}
		e.jsonToXML = function (t, e) {
			void 0 === e && (e = "root");
			for (var i = '<?xml version="1.0" encoding="iso-8859-1"?>\n<' + e + ">", n = 0; n < t.length; n++) i += "\n" + function e(t, i) {
				void 0 === i && (i = r);
				var n, o = s(i) + "<item>\n";
				for (n in t) Array.isArray(t[n]) ? (o += s(i + r) + "<" + n + ">\n", o += t[n].map(function (t) {
					return e(t, i + 2 * r)
				}).join("\n") + "\n", o += s(i + r) + "</" + n + ">\n") : o += s(i + r) + ("<" + n + ">" + t[n]) + "</" + n + ">\n";
				return o += s(i) + "</item>"
			}(t[n]);
			return i + "\n</" + e + ">"
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(21),
			i = (n.prototype.sort = function (t, e, i) {
				this._createSorter(e), i === e && (e = null), (i || e) && this._sort(t, i, e)
			}, n.prototype._createSorter = function (i) {
				var n = this;
				i && !i.rule && (i.rule = function (t, e) {
					t = n._checkVal(i.as, t[i.by]), e = n._checkVal(i.as, e[i.by]);
					return o.naturalCompare(t.toString(), e.toString())
				})
			}, n.prototype._checkVal = function (t, e) {
				return t ? t.call(this, e) : e
			}, n.prototype._sort = function (t, n, o) {
				var r = this,
					s = {
						asc: 1,
						desc: -1
					};
				return t.sort(function (t, e) {
					var i = 0;
					return n && (i = n.rule.call(r, t, e) * (s[n.dir] || s.asc)), 0 === i && o && (i = o.rule.call(r, t, e) * (s[o.dir] || s.asc)), i
				})
			}, n);

		function n() { }
		e.Sort = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var u = i(1),
			s = i(63),
			a = i(26),
			d = i(21),
			h = i(20);

		function l(t, e, i, n) {
			void 0 !== n && -1 !== n && t[i] && t[i][n] ? t[i].splice(n, 0, e) : (t[i] || (t[i] = []), t[i].push(e))
		}
		var c, o = (c = s.DataCollection, o(f, c), f.prototype.add = function (t, i, n) {
			var o = this;
			if (void 0 === i && (i = -1), void 0 === n && (n = this._root), this.events.fire(h.DataEvents.beforeAdd, [t])) return "object" != typeof t && (t = {
				value: t
			}), Array.isArray(t) ? t.map(function (t, e) {
				return o._add(t, i, n, e)
			}) : this._add(t, i, n)
		}, f.prototype.getRoot = function () {
			return this._root
		}, f.prototype.getParent = function (t, e) {
			if (void 0 === e && (e = !1), !this._pull[t]) return null;
			t = this._pull[t].parent;
			return e ? this._pull[t] : t
		}, f.prototype.getItems = function (t) {
			return this._childs && this._childs[t] ? this._childs[t] : []
		}, f.prototype.getLength = function (t) {
			return void 0 === t && (t = this._root), this._childs[t] ? this._childs[t].length : null
		}, f.prototype.removeAll = function (t) {
			if (t) {
				if (this._childs[t])
					for (var e = 0, i = r(this._childs[t]); e < i.length; e++) {
						var n = i[e];
						this.remove(n.id)
					}
			} else {
				c.prototype.removeAll.call(this);
				var o = this._root;
				this._initChilds = null, this._childs = ((t = {})[o] = [], t)
			}
		}, f.prototype.getIndex = function (e) {
			var t = this.getParent(e);
			return t && this._childs[t] ? u.findIndex(this._childs[t], function (t) {
				return t.id === e
			}) : -1
		}, f.prototype.sort = function (t) {
			var e = this;
			if (t) {
				for (var i in this._childs) this._sort.sort(this._childs[i], t);
				if (this._initChilds && Object.keys(this._initChilds).length)
					for (var i in this._initChilds) this._sort.sort(this._initChilds[i], t)
			} else this._childs = {}, this._parse_data(Object.keys(this._pull).map(function (t) {
				return e._pull[t]
			})), this._filters && this.filter(this._filters.filters, this._filters.config);
			this.events.fire(h.DataEvents.change)
		}, f.prototype.filter = function (t, e) {
			var n = this;
			if (void 0 === e && (e = {}), t) {
				if (this._initOrder || (this._initOrder = this._order), this._initChilds || (this._initChilds = this._childs), e.type = e.type || h.TreeFilterType.all, this._filters = {
					filters: {},
					config: e
				}, "function" != typeof t)
					if (t.by) this._filters.filters[t.by] = t;
					else
						for (var i in t) this._filters.filters[i] = t[i];
				else this._filters.filters = t;
				var o = {};
				this._recursiveFilter(this._filters.filters, e, this._root, 0, o), Object.keys(o).forEach(function (t) {
					for (var e = n.getParent(t), i = n.getItem(t); e;) o[e] || (o[e] = []), i && !o[e].find(function (t) {
						return t.id === i.id
					}) && o[e].push(i), i = n.getItem(e), e = n.getParent(e)
				}), this._childs = o, this.events.fire(h.DataEvents.change)
			} else this.restoreOrder()
		}, f.prototype.restoreOrder = function () {
			this._initChilds && (this._childs = this._initChilds, this._initChilds = null), this.events.fire(h.DataEvents.change)
		}, f.prototype.copy = function (t, i, n, o) {
			var r = this;
			return void 0 === n && (n = this), void 0 === o && (o = this._root), t instanceof Array ? t.map(function (t, e) {
				return r._copy(t, i, n, o, e)
			}) : this._copy(t, i, n, o)
		}, f.prototype.move = function (t, i, n, o) {
			var r = this;
			return void 0 === n && (n = this), void 0 === o && (o = this._root), t instanceof Array ? t.map(function (t, e) {
				return r._move(t, i, n, o, e)
			}) : this._move(t, i, n, o)
		}, f.prototype.forEach = function (t, e, i) {
			if (void 0 === e && (e = this._root), void 0 === i && (i = 1 / 0), this.haveItems(e) && !(i < 1))
				for (var n = this._childs[e], o = 0; o < n.length; o++) t.call(this, n[o], o, n), this.haveItems(n[o].id) && this.forEach(t, n[o].id, --i)
		}, f.prototype.eachChild = function (t, e, i, n) {
			if (void 0 === i && (i = !0), void 0 === n && (n = function () {
				return !0
			}), this.haveItems(t))
				for (var o = 0; o < this._childs[t].length; o++) e.call(this, this._childs[t][o], o), i && n(this._childs[t][o]) && this.eachChild(this._childs[t][o].id, e, i, n)
		}, f.prototype.getNearId = function (t) {
			return t
		}, f.prototype.loadItems = function (e, i) {
			var n = this;
			void 0 === i && (i = h.DataDriver.json);
			var t = this.config.autoload + "?id=" + e;
			new a.DataProxy(t).load().then(function (t) {
				t = (i = d.toDataDriver(i)).toJsonArray(t), n._parse_data(t, e), n.events.fire(h.DataEvents.change)
			})
		}, f.prototype.refreshItems = function (t, e) {
			void 0 === e && (e = h.DataDriver.json), this.removeAll(t), this.loadItems(t, e)
		}, f.prototype.eachParent = function (t, e, i) {
			void 0 === i && (i = !1);
			t = this.getItem(t);
			t && (i && e.call(this, t), t.parent !== this._root && (i = this.getItem(t.parent), e.call(this, i), this.eachParent(t.parent, e)))
		}, f.prototype.haveItems = function (t) {
			return t in this._childs
		}, f.prototype.canCopy = function (e, t) {
			if (e === t) return !1;
			var i = !0;
			return this.eachParent(t, function (t) {
				return t.id === e ? i = !1 : null
			}), i
		}, f.prototype.serialize = function (t, e) {
			void 0 === t && (t = h.DataDriver.json);
			e = this._serialize(this._root, e), t = d.toDataDriver(t);
			if (t) return t.serialize(e)
		}, f.prototype.getId = function (t, e) {
			if (void 0 === e && (e = this._root), this._childs[e] && this._childs[e][t]) return this._childs[e][t].id
		}, f.prototype.map = function (t, e, i) {
			void 0 === e && (e = this._root), void 0 === i && (i = !0);
			var n = [];
			if (!this.haveItems(e)) return n;
			for (var o, r = 0; r < this._childs[e].length; r++) n.push(t.call(this, this._childs[e][r], r, this._childs)), i && (o = this.map(t, this._childs[e][r].id, i), n = n.concat(o));
			return n
		}, f.prototype.getRawData = function (t, e, i, n, o) {
			return o = o || this._root, this._childs[o] ? (o = o === this._root ? c.prototype.getRawData.call(this, t, e, this._childs[o]) : this._childs[o], 2 === n ? this.flatten(o) : o) : []
		}, f.prototype.flatten = function (t) {
			var i = this,
				n = [];
			return t.forEach(function (t) {
				n.push(t);
				var e = i._childs[t.id];
				e && t.$opened && (n = n.concat(i.flatten(e)))
			}), n
		}, f.prototype._add = function (t, e, i, n) {
			void 0 === e && (e = -1), void 0 === i && (i = this._root), t.parent = t.parent ? t.parent.toString() : i, 0 < n && -1 !== e && (e += 1);
			e = c.prototype._add.call(this, t, e);
			if (Array.isArray(t.items))
				for (var o = 0, r = t.items; o < r.length; o++) {
					var s = r[o];
					this.add(s, -1, t.id)
				}
			return e
		}, f.prototype._copy = function (t, e, i, n, o) {
			if (void 0 === i && (i = this), void 0 === n && (n = this._root), !this.exists(t)) return null;
			var r = this._childs[t];
			if (o && (e = -1 === e ? -1 : e + o), i === this && !this.canCopy(t, n)) return null;
			o = d.copyWithoutInner(this.getItem(t), {
				items: !0
			});
			if (i.exists(t) && (o.id = u.uid()), d.isTreeCollection(i)) {
				if (this.exists(t) && (o.parent = n, i !== this && n === this._root && (o.parent = i.getRoot()), i.add(o, e), t = o.id), r)
					for (var s = 0, a = r; s < a.length; s++) {
						var l = a[s].id,
							c = this.getIndex(l);
						"string" == typeof t && this.copy(l, c, i, t)
					}
				return t
			}
			i.add(o, e)
		}, f.prototype._move = function (t, e, i, n, o) {
			if (void 0 === i && (i = this), void 0 === n && (n = this._root), !this.exists(t)) return null;
			if (o && (e = -1 === e ? -1 : e + o), i !== this) {
				if (!d.isTreeCollection(i)) return i.add(d.copyWithoutInner(this.getItem(t)), e), void this.remove(t);
				var r = this.copy(t, e, i, n);
				return this.remove(t), r
			}
			if (!this.canCopy(t, n)) return null;
			i = this.getParent(t), r = this.getIndex(t), r = this._childs[i].splice(r, 1)[0];
			return r.parent = n, this._childs[i].length || delete this._childs[i], this.haveItems(n) || (this._childs[n] = []), -1 === e ? e = this._childs[n].push(r) : this._childs[n].splice(e, 0, r), this.events.fire(h.DataEvents.change, [t, "update", this.getItem(t)]), t
		}, f.prototype._reset = function (t) {
			if (t)
				for (var e = 0, i = r(this._childs[t]); e < i.length; e++) {
					var n = i[e];
					this.remove(n.id)
				} else {
				c.prototype._reset.call(this);
				var o = this._root;
				this._initChilds = null, this._childs = ((t = {})[o] = [], t)
			}
		}, f.prototype._removeCore = function (e) {
			var t;
			this._pull[e] && (t = this.getParent(e), this._childs[t] = this._childs[t].filter(function (t) {
				return t.id !== e
			}), t === this._root || this._childs[t].length || delete this._childs[t], this._initChilds && this._initChilds[t] && (this._initChilds[t] = this._initChilds[t].filter(function (t) {
				return t.id !== e
			}), t === this._root || this._initChilds[t].length || delete this._initChilds[t]), this._initOrder && this._initOrder.length && (this._initOrder = this._initOrder.filter(function (t) {
				return t.id !== e
			})), this._fastDeleteChilds(this._childs, e), this._initChilds && this._fastDeleteChilds(this._initChilds, e))
		}, f.prototype._addToOrder = function (t, e, i) {
			var n = this._childs,
				o = this._initChilds,
				r = e.parent;
			(this._pull[e.id] = e).parent && this._pull[e.parent] && this._pull[e.parent].items && !this._pull[e.parent].items.find(function (t) {
				return t.id === e.id
			}) && this._pull[e.parent].items.push(e), c.prototype._addToOrder.call(this, t, e, i), l(n, e, r, i), o && l(o, e, r, i)
		}, f.prototype._parse_data = function (t, e) {
			var i;
			void 0 === e && (e = this._root);
			for (var n = this._order.length, o = 0, r = t; o < r.length; o++) {
				var s = r[o];
				this.config.init && (s = this.config.init(s)), s && "object" != typeof s && (s = {
					value: s
				}), s.id = null !== (i = s.id) && void 0 !== i ? i : u.uid(), s.parent = void 0 === s.parent || null === s.parent || s.parent && s.$items ? e : s.parent, this._pull[s.id] && d.dhxError("Item " + s.id + " already exist"), this._pull[s.id] = s, this._order[n++] = s, this._childs[s.parent] || (this._childs[s.parent] = []), this._childs[s.parent].push(s), s.items && s.items instanceof Object && this._parse_data(s.items, s.id)
			}
		}, f.prototype._fastDeleteChilds = function (t, e) {
			if (this._pull[e] && delete this._pull[e], t[e]) {
				for (var i = 0; i < t[e].length; i++) this._fastDeleteChilds(t, t[e][i].id);
				delete t[e]
			}
		}, f.prototype._recursiveFilter = function (n, e, t, i, o) {
			var r = this,
				s = this._childs[t];
			if (s) {
				var a, l = function (t) {
					switch (e.type) {
						case h.TreeFilterType.all:
							return !0;
						case h.TreeFilterType.level:
							return i === e.level;
						case h.TreeFilterType.leafs:
							return !r.haveItems(t.id)
					}
				};
				a = "function" == typeof n ? function (t) {
					return l(t) && n(t)
				} : function (t) {
					var e, i = !0;
					for (e in n)
						if (n[e].by && "" !== n[e].match && (i = t[n[e].by] && -1 !== t[n[e].by].toString().toLocaleLowerCase().indexOf(n[e].match.toString().toLowerCase())), !i) break;
					return l(t) && i
				}, (a = s.filter(a)).length && (o[t] = a);
				for (var c = 0, u = s; c < u.length; c++) {
					var d = u[c];
					this._recursiveFilter(n, e, d.id, i + 1, o)
				}
			}
		}, f.prototype._serialize = function (t, n) {
			var o = this;
			return void 0 === t && (t = this._root), this.map(function (t) {
				var e, i = {};
				for (e in t) "parent" === e || "items" === e || e.startsWith("$") || (i[e] = t[e]);
				return n && (i = n(i)), o.haveItems(t.id) && (i.items = o._serialize(t.id, n)), i
			}, t, !1)
		}, f);

		function f(t, e) {
			var i = c.call(this, t, e) || this;
			i._childs = {};
			e = i._root = t && t.rootId || "_ROOT_" + u.uid();
			return i._childs = ((t = {})[e] = [], t), i._initChilds = null, i
		}
		e.TreeCollection = o
	}, function (t, e, i) {
		"use strict";
		var g = this && this.__assign || function () {
			return (g = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		},
			a = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var h = i(2),
			m = i(27),
			f = i(179),
			p = i(20),
			y = i(21),
			b = i(1);
		var n = (o.prototype.setItem = function (t, e) {
			f.collectionStore.setItem(t, e)
		}, o.prototype.onMouseDown = function (t, e, i) {
			var n, o, r, s;
			1 !== t.which && !t.targetTouches || (t.targetTouches ? (document.addEventListener("touchmove", this._onMouseMove, !1), document.addEventListener("touchend", this._onMouseUp, !1)) : (document.addEventListener("mousemove", this._onMouseMove), document.addEventListener("mouseup", this._onMouseUp)), o = (n = h.locateNode(t, "dhx_id")) && n.getAttribute("dhx_id"), r = h.locate(t, "dhx_widget_id"), Array.isArray(e) && e.includes(o) ? (this._transferData.source = a(e), this._itemsForGhost = i) : (this._transferData.source = [o], this._itemsForGhost = null), o && r && (e = (s = h.getBox(n)).left, i = s.top, s = (t.targetTouches ? t.targetTouches[0] : t).pageX, t = (t.targetTouches ? t.targetTouches[0] : t).pageY, this._transferData.initXOffset = s - e, this._transferData.initYOffset = t - i, this._transferData.x = s, this._transferData.y = t, this._transferData.componentId = r, this._transferData.start = o, this._transferData.item = n))
		}, o.prototype.isDrag = function () {
			return this._isDrag
		}, o.prototype.cancelCanDrop = function (t) {
			this._canMove = !1, this._isDrag = !1;
			var e = this._transferData,
				i = e.start,
				n = e.source,
				o = e.target,
				e = e.dropComponentId,
				n = {
					start: i,
					source: n,
					target: o
				},
				e = f.collectionStore.getItem(e);
			e && o && e.events.fire(p.DragEvents.cancelDrop, [n, t]), this._transferData.dropComponentId = null, this._transferData.target = null
		}, o.prototype._moveGhost = function (t, e) {
			this._transferData.ghost && (this._transferData.ghost.style.left = t - this._transferData.initXOffset + "px", this._transferData.ghost.style.top = e - this._transferData.initYOffset + "px")
		}, o.prototype._removeGhost = function () {
			document.body.removeChild(this._transferData.ghost)
		}, o.prototype._onDrop = function (t) {
			var e, i, n, o, r;
			this._canMove && (r = (o = this._transferData).start, i = o.source, e = o.target, n = o.dropComponentId, i = {
				start: r,
				source: i,
				target: e,
				dropPosition: o.dropPosition
			}, n = (o = f.collectionStore.getItem(n)) && o.config, o && "source" !== n.dragMode && o.events.fire(p.DragEvents.beforeDrop, [i, t]) && (o = {
				id: e,
				component: o
			}, r = {
				id: r,
				component: this._transferData.component,
				newId: null
			}, this._move(r, o), r.newId && r.component !== o.component && (i.start = r.newId), o.component.events.fire(p.DragEvents.afterDrop, [i, t]))), this._endDrop(t)
		}, o.prototype._onDragStart = function (t, e, i) {
			var n = f.collectionStore.getItem(e),
				o = n.config,
				r = this._transferData,
				e = {
					start: r.start,
					source: r.source,
					target: r.target
				};
			if ("target" === o.dragMode || n._pregroupData) return null;
			r = function (t, e, i) {
				void 0 === i && (i = !1);
				var n = t.getBoundingClientRect(),
					o = document.createElement("div"),
					r = t.cloneNode(!0);
				return r.style.width = n.width + "px", r.style.height = n.height + "px", r.style.maxHeight = n.height + "px", r.style.fontSize = window.getComputedStyle(t.parentElement).fontSize, r.style.opacity = "0.8", r.style.fontSize = window.getComputedStyle(t.parentElement).fontSize, i && e && e.length || o.appendChild(r), e && e.length && e.forEach(function (t, e) {
					t = t.cloneNode(!0);
					t.style.width = n.width + "px", t.style.height = n.height + "px", t.style.maxHeight = n.height + "px", t.style.top = 12 * (e + 1) - n.height - n.height * e + "px", t.style.left = 12 * (e + 1) + "px", t.style.opacity = "0.6", t.style.zIndex = "" + (-e - 1), o.appendChild(t)
				}), o.className = "dhx_drag-ghost", o
			}(this._transferData.item, this._itemsForGhost, "column" === o.dragItem || "both" === o.dragItem);
			return n.events.fire(p.DragEvents.beforeDrag, [e, i, r]) && t ? (n.events.fire(p.DragEvents.dragStart, [e, i]), this._isDrag = !0, this._toggleTextSelection(!0), this._transferData.component = n, this._transferData.dragConfig = o, r) : null
		}, o.prototype._onDrag = function (t) {
			var e = (t.targetTouches ? t.targetTouches[0] : t).clientX,
				i = (t.targetTouches ? t.targetTouches[0] : t).clientY,
				n = document.elementFromPoint(e, i),
				o = h.locate(n, "dhx_widget_id");
			if (o) {
				var r = f.collectionStore.getItem(o),
					s = !!h.locateNodeByClassName(n, "dhx_grid-header") || !!h.locateNodeByClassName(n, "dhx_grid-footer"),
					a = r && r.config.columns ? r.config : void 0,
					l = a && ("both" === a.dragItem || "column" === a.dragItem);
				if (!s || l) {
					var c = h.locate(n, "dhx_id"),
						u = h.locate(n, "dhx_root_id");
					if (!c && !u) return this.cancelCanDrop(t), this._transferData.dropComponentId = o, this._transferData.target = null, void this._canDrop(t);
					var d = this._transferData,
						e = d.dropComponentId,
						i = d.start,
						a = d.source,
						s = d.target,
						l = d.componentId,
						n = d.dropPosition;
					if ("complex" === r.config.dropBehaviour) {
						d = function (t) {
							var e = t.clientY;
							if (!(t = h.locateNode(t))) return null;
							if (t = t.childNodes[0]) {
								t = t.getBoundingClientRect();
								return (e - t.top) / t.height
							}
						}(t);
						this._transferData.dropPosition = d <= .25 ? "top" : .75 <= d ? "bottom" : "in"
					} else if ((s === c || s === u) && e === o) return;
					e = {
						id: i,
						component: this._transferData.component
					};
					"source" !== r.config.dragMode && (e.component.events.fire(p.DragEvents.dragOut, [{
						start: i,
						source: a,
						target: s
					}, t]), o !== l || !y.isTreeCollection(e.component.data) || y.isTreeCollection(e.component.data) && e.component.data.canCopy(e.id, c) ? (this.cancelCanDrop(t), this._transferData.target = c || u, this._transferData.dropComponentId = o, e.component.events.fire(p.DragEvents.dragIn, [{
						start: i,
						source: a,
						target: s,
						dropPosition: n
					}, t]) && this._canDrop(t)) : this.cancelCanDrop(t))
				} else this._canMove && this.cancelCanDrop(t)
			} else this._canMove && this.cancelCanDrop(t)
		}, o.prototype._move = function (i, e) {
			var n = i.component.data,
				o = e.component.data,
				r = 0,
				s = e.id,
				t = y.isTreeCollection(o) ? e.component.config.dropBehaviour : void 0,
				a = i.component.config.columns ? i.component.config : void 0,
				a = a && ("both" === a.dragItem || "column" === a.dragItem) && a.columns.map(function (t) {
					return t.id
				}).filter(function (t) {
					return t === i.id || t === e.id
				}).length;
			if (a && i.component === e.component) {
				if (i.id === e.id) return;
				var l = (c = (u = i.component).config.columns.map(function (t) {
					return g({}, t)
				})).findIndex(function (t) {
					return t.id === i.id
				});
				return -1 === (h = c.findIndex(function (t) {
					return t.id === e.id
				})) ? void 0 : (c.splice(h, 0, c.splice(l, 1)[0]), u.setColumns(c), void u.paint())
			}
			if (a && i.component instanceof m.ProGrid && e.component instanceof m.ProGrid) {
				var c, u = i.component,
					d = e.component,
					l = (c = u.config.columns.map(function (t) {
						return g({}, t)
					})).findIndex(function (t) {
						return t.id === i.id
					}),
					a = d.config.columns.map(function (t) {
						return g({}, t)
					}),
					h = a.findIndex(function (t) {
						return t.id === e.id
					}),
					f = 0 <= a.findIndex(function (t) {
						return t.id === i.id
					}) ? i.id + "_copy" : i.id,
					p = [];
				u.data.forEach(function (t) {
					var e;
					p.push(((e = {})[f] = t[i.id], e))
				}), d.data.forEach(function (t, e) {
					d.data.update(t.id, g(g({}, t), p[e]))
				});
				l = c.splice(l, 1)[0];
				return l.id = f, a.splice(h, 0, l), d.setColumns(a), d.paint(), u.setColumns(c), void u.paint()
			}
			var _ = e.id === e.component.config.rootParent;
			switch (t) {
				case "child":
					break;
				case "sibling":
					s = o.getParent(s), r = o.getIndex(e.id) + 1;
					break;
				case "complex":
					var v = this._transferData.dropPosition;
					_ ? (s = e.id, r = o.getLength()) : "top" === v ? (s = o.getParent(s), r = o.getIndex(e.id)) : "bottom" === v && (s = o.getParent(s), r = o.getIndex(e.id) + 1);
					break;
				default:
					r = e.id ? i.component === e.component && o.getIndex(i.id) < o.getIndex(e.id) ? o.getIndex(e.id) - 1 : (-1 < o.getIndex(i.id) && (i.newId = b.uid()), o.getIndex(e.id)) : -1
			}
			this._transferData.dragConfig.dragCopy ? this._transferData.source instanceof Array && 1 < this._transferData.source.length ? this._transferData.source.map(function (t) {
				n.copy(t, r, o, s), -1 < r && r++
			}) : n.copy(i.id, r, o, s) : this._transferData.source instanceof Array && 1 < this._transferData.source.length ? this._transferData.source.map(function (t) {
				n.move(t, r, o, s), -1 < r && r++
			}) : n.move(i.id, r, o, s, i.newId)
		}, o.prototype._endDrop = function (t) {
			var e;
			this._toggleTextSelection(!1), this._transferData.component && (e = {
				start: (e = this._transferData).start,
				source: e.source,
				target: e.target
			}, this._transferData.component.events.fire(p.DragEvents.afterDrag, [e, t])), this.cancelCanDrop(t), this._canMove = !0, this._transferData = {}, this._transferData.target = null, this._transferData.dropComponentId = null
		}, o.prototype._canDrop = function (t) {
			this._canMove = !0;
			var e = this._transferData,
				i = {
					start: e.start,
					source: e.source,
					target: e.target,
					dropPosition: e.dropPosition
				},
				e = f.collectionStore.getItem(this._transferData.dropComponentId);
			e && this._transferData.target && e.events.fire(p.DragEvents.canDrop, [i, t])
		}, o.prototype._toggleTextSelection = function (t) {
			t ? document.body.classList.add("dhx_no-select") : document.body.classList.remove("dhx_no-select")
		}, o);

		function o() {
			var a = this;
			this._transferData = {}, this._canMove = !0, this._isDrag = !1, this._onMouseMove = function (t) {
				if (a._transferData.start) {
					var e = (t.targetTouches ? t.targetTouches[0] : t).pageX,
						i = (t.targetTouches ? t.targetTouches[0] : t).pageY,
						n = a._transferData,
						o = n.x,
						r = n.y,
						s = n.start,
						n = n.componentId;
					if (!a._transferData.ghost) {
						if (Math.abs(o - e) < 3 && Math.abs(r - i) < 3) return;
						n = a._onDragStart(s, n, t);
						if (!n) return void a._endDrop(t);
						a._transferData.ghost = n, document.body.appendChild(a._transferData.ghost)
					}
					a._moveGhost(e, i), a._onDrag(t)
				}
			}, this._onMouseUp = function (t) {
				a._transferData.x && (a._transferData.ghost ? (a._removeGhost(), a._onDrop(t)) : a._endDrop(t), t.targetTouches ? (document.removeEventListener("touchmove", a._onMouseMove), document.removeEventListener("touchend", a._onMouseUp)) : (document.removeEventListener("mousemove", a._onMouseMove), document.removeEventListener("mouseup", a._onMouseUp)))
			}
		}
		i = window.dhxHelpers = window.dhxHelpers || {};
		i.dragManager = i.dragManager || new n, e.dragManager = i.dragManager
	}, function (t, e, i) {
		"use strict";
		var n = this && this.__assign || function () {
			return (n = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		},
			a = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var g = i(16),
			l = i(6),
			o = i(1);
		r.prototype.xlsx = function (t) {
			return this._export(t)
		}, r.prototype.csv = function (t) {
			var e;
			void 0 === t && (t = {}), t = n({
				asFile: !0,
				rowDelimiter: "\n",
				columnDelimiter: ",",
				skipHeader: 0
			}, t), e = "getRoot" in this._view.data && t.flat ? this.getFlatCSV(t) : this._getCSV(t);
			var i = t.name || "grid_export";
			return t.asFile && o.downloadFile(e, i + ".csv", "text/csv"), e
		}, r.prototype._export = function (t) {
			void 0 === t && (t = {});
			for (var c = this._view.config.columns.filter(function (t) {
				return !t.hidden
			}), u = {}, e = g.transpose(c.map(function (t) {
				return t.header.map(function (t) {
					return t.text || " "
				})
			})), d = [], h = {
				default: {
					color: "#000000",
					background: "#FFFFFF",
					fontSize: 14
				}
			}, f = [], n = {}, i = this._view.data.serialize().map(function (i, t) {
				return u[i.id] = t, c.map(function (t, e) {
					return n[t.id] = e, g.removeHTMLTags(i[t.id])
				})
			}), p = [], _ = this._view.content, v = this, o = 0, r = c; o < r.length; o++) ! function (t) {
				var n, e, o, i, r;
				for (r in t.footer && (n = t.id, i = e = v._view.data.serialize().reduce(function (t, e) {
					return void 0 === e[n] || "" === e[n] || isNaN(e[n]) || t.push(parseFloat(e[n])), t
				}, []), "tree" === v._view.config.type && (i = (o = v._view.data).serialize().reduce(function (t, e) {
					var i;
					return 0 === e.$level && (void 0 === e[n] || "" === e[n] || isNaN(e[n]) ? (i = 0, o.eachChild(e.id, function (t) {
						o.haveItems(t.id) || (i += parseFloat(t[n]))
					}), t.push(i)) : t.push(parseFloat(e[n]) || 0)), t
				}, [])), t.footer[0].content ? (i = _[t.footer[0].content].calculate(e, i), p.push(i)) : p.push(t.footer[0].css || t.footer[0].text || " ")), d.push({
					width: t.width
				}), t.$cellCss) {
					var s, a = t.$cellCss[r],
						l = a.split("").reduce(function (t, e) {
							e = (t << 5) - t + e.charCodeAt(0);
							return Math.abs(e & e)
						}, 0).toString();
					h[l] || (s = document.body, (s = g.getStyleByClass(a, s, "dhx_grid-row", h.default)) && (h[l] = s)), h[l] && f.push([u[r], c.indexOf(t), l])
				}
			}(r[o]);
			p.length && i.push(p);
			var s, i = {
				name: t.name || "data",
				columns: d,
				header: e,
				data: i,
				styles: {
					cells: f,
					css: h
				}
			};
			return t.url && ((s = document.createElement("form")).setAttribute("target", "_blank"), s.setAttribute("action", t.url), s.setAttribute("method", "POST"), s.style.visibility = "hidden", (t = document.createElement("textarea")).setAttribute("name", "data"), t.value = JSON.stringify(i), s.appendChild(t), document.body.appendChild(s), s.submit(), setTimeout(function () {
				s.parentNode.removeChild(s)
			}, 100)), i
		}, r.prototype.getFlatCSV = function (o) {
			var e = this._view.data,
				t = e.getRoot(),
				r = this._view.config.columns[0],
				s = e.getMaxLevel(),
				i = "";
			e.eachChild(t, function (n) {
				var t = function (t, e) {
					for (var i, n = [], o = 0; o <= s; o++) t && t[r.id] ? (n[t.$level] = t[r.id], t = (i = e.getParent(t.id, !0)) && i.id ? i : null) : n[o] = "";
					return n
				}(n, e).join(o.columnDelimiter);
				i += t + Object.keys(n).reduce(function (t, e, i) {
					return "id" === e || "parent" === e || e.startsWith("$") || 0 === i ? t : t + o.columnDelimiter + (null === n[e] ? "" : n[e])
				}, ""), i += o.rowDelimiter
			});
			var t = this._export(o),
				n = function (t, e) {
					for (var i = 0; i < t.length; i++) t[i] = e;
					return t
				}(new Array(s + 1), ""),
				t = t.header.map(function (t) {
					return t.splice.apply(t, a([0, 1], n)), t
				});
			return new l.CsvDriver(o).serialize(t, !0) + o.rowDelimiter + i
		}, r.prototype._getCSV = function (t) {
			var e = this._export(t),
				i = e.header,
				t = new l.CsvDriver(t);
			return t.serialize(i, !0) + "\n" + t.serialize(e.data, !0)
		}, i = r;

		function r(t) {
			this._view = t
		}
		e.Exporter = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var h = i(0),
			u = i(1),
			o = i(6),
			r = i(3),
			f = i(9),
			p = i(18),
			i = (n.prototype.setCell = function (e, i, t, n) {
				var o, r, s, a, l, c, u, d = this;
				void 0 === t && (t = !1), void 0 === n && (n = !1), this._gridId && p.focusManager.getFocusId() !== this._gridId && p.focusManager.setFocusId(this._gridId), this.config.disabled || this._grid.config.$editable || !this._multiselection && this._oldSelectedCell && this._oldSelectedCell.row.id === (e && e.id || e) && this._oldSelectedCell.column.id === (i && i.id || i) || this._multiselection && 1 === this._selectedCells.length && this._selectedCells[0].row.id === (e && e.id || e) && this._selectedCells[0].column.id === (i && i.id || i) || ((!this._multiselection || t || n) && this._multiselection || this._selectedCells.length && this._removeCells(), this._multiselection && "cell" === this._type && this._selectedCells.find(function (t) {
					return t.row.id === (e && e.id || e) && t.column.id === (i && i.id || i)
				}) ? this.removeCell(e && e.id || e, i && i.id || i) : (l = this._oldSelectedCell || void 0, e = this._grid.data.getItem(e && e.id || e), o = this._grid.config.columns.filter(function (t) {
					return !t.hidden
				}), i = i || o[0], (i = this._grid.getColumn(i.id || i)) && e && (i = i.id ? i : this._grid.getColumn(i), this.events.fire(f.GridSelectionEvents.beforeSelect, [e, i]) && (this._selectedCell = {
					row: e,
					column: i
				}, this.events.fire(f.GridSelectionEvents.afterSelect, [e, i]), this._multiselection && n && l ? this._oldSelectedCell = l : this._oldSelectedCell = this._selectedCell, this._multiselection ? n && !t && 0 < this._selectedCells.length ? (r = this._grid.data.getIndex(this._oldSelectedCell.row.id), (s = this._grid.data.getIndex(e.id)) < r && (a = r, r = s, s = a), this._selectedCells = [this._oldSelectedCell], "cell" === this._type ? (l = (c = o.map(function (t) {
					return t.id
				})).indexOf(l.column.id), c = c.indexOf(i.id), -1 !== l && -1 !== c && (c < l && (a = l, l = c, c = a), u = o.slice(l, c + 1), this._grid.data.mapRange(r, s, function (e) {
					u.forEach(function (t) {
						t = {
							row: e,
							column: t
						}; - 1 === d._findIndex(t) && d._selectedCells.push(t)
					})
				}))) : this._grid.data.mapRange(r, s, function (t) {
					t = {
						row: t,
						column: i
					}; - 1 === d._findIndex(t) && d._selectedCells.push(t)
				})) : t && !n ? -1 === (n = this._findIndex()) ? this._selectedCells.push({
					row: this._selectedCell.row,
					column: this._selectedCell.column
				}) : 1 < this._selectedCells.length && this._selectedCells.splice(n, 1) : this._selectedCells = [this._selectedCell] : this._selectedCells = [this._selectedCell], h.awaitRedraw().then(function () {
					d._grid.paint(), d._setBrowserFocus()
				})))))
			}, n.prototype.getCell = function () {
				return this._selectedCell
			}, n.prototype.getCells = function () {
				return this._selectedCells
			}, n.prototype.toHTML = function () {
				var n = this;
				if (!this._isUnselected()) {
					if (this._multiselection) {
						var o = [];
						return this._selectedCells.forEach(function (t, e, i) {
							o.push(n._toHTML(t.row, t.column, e === i.length - 1 || "cell" === n._type))
						}), o
					}
					return this._toHTML(this._selectedCell.row, this._selectedCell.column, !0)
				}
			}, n.prototype.disable = function () {
				this.removeCell(), this.config.disabled = !0, this._grid.paint()
			}, n.prototype.enable = function () {
				this.config.disabled = !1, this._grid.paint()
			}, n.prototype.removeCell = function (i, n) {
				var t, o = this;
				i && n && "cell" === this._type ? (t = this._selectedCells.find(function (t) {
					var e = t.row,
						t = t.column;
					return e.id == i && t.id == n
				})) && this._removeCell(t.row, t.column) : i ? this._selectedCells.filter(function (t) {
					return t.row.id == i
				}).forEach(function (t) {
					var e = t.row,
						t = t.column;
					o._removeCell(e, t)
				}) : this._removeCells(), h.awaitRedraw().then(function () {
					o._grid.paint()
				})
			}, n.prototype._removeCell = function (e, i) {
				var t;
				e && i && e.id && i.id && this.events.fire(f.GridSelectionEvents.beforeUnSelect, [e, i]) && (t = this._selectedCells.findIndex(function (t) {
					return t.row.id === e.id && t.column.id === i.id
				}), this._selectedCells.splice(t, 1), this._selectedCell && i.id === this._selectedCell.column.id && e.id === this._selectedCell.row.id && (this._selectedCell = this._selectedCells[this._selectedCells.length - 1] || void 0), this.events.fire(f.GridSelectionEvents.afterUnSelect, [e, i]))
			}, n.prototype._removeCells = function () {
				var e = this;
				this._selectedCells.forEach(function (t) {
					e._removeCell(t && t.row, t && t.column)
				}), this._selectedCells.length && this._removeCells()
			}, n.prototype._init = function () {
				var n = this;
				this._grid.events.on(f.GridEvents.cellClick, function (t, e, i) {
					n.setCell(t, e, i.ctrlKey || i.metaKey, i.shiftKey)
				}), this._grid.data.events.on(o.DataEvents.beforeRemove, function (t) {
					var e;
					t && n._selectedCell && n._selectedCell.row && (e = n._grid.data.getIndex(String(n._selectedCell.row.id)), (t = n._grid.data.getId(e + 1)) ? n.setCell(t) : (e = n._grid.data.getId(e - 1)) && n.setCell(e), n._grid.paint())
				})
			}, n.prototype._toHTML = function (e, t, i) {
				void 0 === i && (i = !1);
				var n = this._grid.data.getRawData(0, -1, null, 2);
				if (-1 === u.findIndex(n, function (t) {
					return t.id == e.id
				})) return null;
				var o, r = this._grid.config.columns.filter(function (t) {
					return !t.hidden
				}),
					s = this._grid.config.leftSplit ? r.slice(0, this._grid.config.leftSplit) : [],
					a = s.map(function (t) {
						return t.id
					}),
					l = s.reduce(function (t, e) {
						return t + e.$width
					}, 0),
					c = this._grid.getCellRect(e.id, t.id),
					n = this._grid.getScrollState();
				a.includes(t.id) && i && (o = h.el(".dhx_grid-selected-cell", {
					style: {
						width: this._grid.config.leftSplit === a.indexOf(t.id) + 1 ? c.width - 1 : c.width,
						height: c.height,
						top: c.y,
						left: c.x + n.x,
						position: "absolute",
						zIndex: 10
					}
				}));
				r = s.length && l > c.x - n.x, a = c.width;
				r && (a -= l - (c.x - n.x));
				t = this._grid.config.$totalWidth;
				return h.el(".dhx_grid-selection", {
					style: {
						zIndex: o || "row" === this._grid.config.selection || "complex" === this._grid.config.selection ? 20 : 10
					}
				}, [("row" === this._type || "complex" === this._type) && h.el(".dhx_grid-selected-row", {
					style: {
						width: s.length ? t - n.x : t,
						height: c.height - 1,
						top: c.y,
						left: s.length ? n.x : 0,
						position: "absolute"
					}
				}), ("cell" === this._type || "complex" === this._type) && o || ("cell" === this._type || "complex" === this._type) && i && h.el(".dhx_grid-selected-cell", {
					style: {
						width: a,
						height: c.height - 1,
						top: c.y,
						left: r ? l + n.x : c.x,
						position: "absolute",
						display: 0 < a ? "flex" : "none",
						borderLeft: r ? "none" : null
					}
				})])
			}, n.prototype._isUnselected = function () {
				return !this._selectedCell || !this._selectedCell.row || !this._selectedCell.column || 0 === this._selectedCells.length
			}, n.prototype._findIndex = function (i) {
				var n = this;
				void 0 === i && (i = this._selectedCell);
				var o = -1;
				return this._selectedCells.some(function (t, e) {
					if ("cell" === n._type) {
						if (u.compare(t.row, i.row) && u.compare(t.column, i.column)) return o = e, !0
					} else if ("row" === n._type && u.compare(t.row, i.row)) return o = e, !0
				}), o
			}, n.prototype._setBrowserFocus = function () {
				var t, e, i = this._grid.getRootView().data.getRootNode();
				!i || (t = i.querySelector(".dhx_grid_data")) && this._selectedCell && this._selectedCell.row && this._selectedCell.column && (e = t.querySelector('[dhx_id="' + this._selectedCell.row.id + '"]'), t = this._grid.getSpan(this._selectedCell.row.id, this._selectedCell.column.id), !e || (e = (i = t ? i.querySelector(".dhx_span-spans") : null) ? i.querySelector('[dhx_col_id="' + t.column + '"][dhx_id="' + t.row + '"]') : e.querySelector('[dhx_col_id="' + this._selectedCell.column.id + '"]')) && (e.tabIndex = 0, e.focus({
					preventScroll: !0
				})))
			}, n);

		function n(t, e, i, n) {
			this._grid = t, this.config = e, this._gridId = n, this._selectedCell = void 0, this._oldSelectedCell = void 0, this._selectedCells = [], this._type = ["cell", "row", "complex"].includes(this._grid.config.selection) ? this._grid.config.selection : "complex", this._multiselection = t.config.multiselection && "complex" !== this._type, this.events = i || new r.EventSystem(this), this._init()
		}
		e.Selection = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o, r = i(9),
			s = i(153),
			a = i(154),
			l = i(155),
			c = i(165),
			u = i(166),
			d = i(173),
			h = {
				cell: {
					row: null,
					col: null
				},
				editor: null,
				gridId: null
			};
		e.getEditor = function (t, e, i) {
			var n = "boolean" === e.type ? "checkbox" : i.$editable.editorType;
			if (void 0 === n && (n = i.autoHeight ? "textarea" : "input"), h.cell.row === t.id && h.cell.col === e.id && h.gridId === i.gridId && i.$editable.editor) return h.editor;
			switch ("checkbox" !== n && (h = {
				cell: {
					row: t.id,
					col: e.id
				},
				editor: h.editor,
				gridId: i.gridId
			}), o || (o = function () {
				h = {
					cell: {
						row: null,
						col: null
					},
					editor: null,
					gridId: null
				}
			}, i.events.on(r.GridEvents.afterEditEnd, o)), n) {
				case "input":
					return h.editor = new s.InputEditor(t, e, i);
				case "textarea":
					return h.editor = new d.TextAreaEditor(t, e, i);
				case "select":
					return h.editor = new a.SelectEditor(t, e, i);
				case "datePicker":
					return h.editor = new l.DateEditor(t, e, i);
				case "checkbox":
					return new c.CheckboxEditor(t, e, i);
				case "multiselect":
				case "combobox":
					return h.editor = new u.ComboboxEditor(t, e, i);
				default:
					return h.editor = new s.InputEditor(t, e, i)
			}
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			o = i(9),
			r = i(1),
			i = (s.prototype.endEdit = function (t) {
				var e;
				t || (e = this._input.value), this._config.events.fire(o.GridEvents.beforeEditEnd, [e, this._cell.row, this._cell.col]) ? (this._input.removeEventListener("blur", this._handlers.onBlur), this._input.removeEventListener("change", this._handlers.onChange), "string" !== this._cell.col.type && r.isNumeric(e) && (e = parseFloat(e)), this._cell.row = this._config.datacollection.getItem(this._cell.row.id), this._config.events.fire(o.GridEvents.afterEditEnd, [e, this._cell.row, this._cell.col])) : this._input.focus()
			}, s.prototype.toHTML = function () {
				var t = this._cell.row[this._cell.col.id];
				return this._input && (t = this._input.value), this._config.$editable.editor = this, n.el("input.dhx_cell-editor.dhx_cell-editor__input", {
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_key: "cell_editor",
					dhx_id: "cell_editor",
					value: t
				})
			}, s.prototype._initHandlers = function () {
				var e = this;
				this._handlers = {
					onBlur: function () {
						e.endEdit()
					},
					onChange: function () {
						e.endEdit()
					},
					didInsert: function (t) {
						t = t.el;
						(e._input = t).focus(), t.setSelectionRange(0, t.value.length), t.addEventListener("change", e._handlers.onChange), t.addEventListener("blur", e._handlers.onBlur)
					}
				}
			}, s);

		function s(t, e, i) {
			this._config = i, this._cell = {
				row: t,
				col: e
			}, this._initHandlers()
		}
		e.InputEditor = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			o = i(9),
			i = (r.prototype.endEdit = function (t) {
				var e;
				t || (e = this._input.value), this._config.events.fire(o.GridEvents.beforeEditEnd, [e, this._cell.row, this._cell.col]) ? (this._input.removeEventListener("blur", this._handlers.onBlur), this._input.removeEventListener("keydown", this._handlers.onkeydown), this._cell.row = this._config.datacollection.getItem(this._cell.row.id), this._config.events.fire(o.GridEvents.afterEditEnd, [e, this._cell.row, this._cell.col])) : this._input.focus()
			}, r.prototype.toHTML = function () {
				var t = this._cell.col.options.map(function (t) {
					return "string" == typeof t ? {
						id: "" + t,
						value: t
					} : t
				}) || [],
					e = this._cell.row[this._cell.col.id];
				this._input && (e = this._input.options[this._input.selectedIndex].value);
				t = t.map(function (t) {
					return n.el("option", {
						selected: t === e,
						value: t.id
					}, t.value)
				});
				return this._config.$editable.editor = this, n.el("select.dhx_cell-editor.dhx_cell-editor__select", {
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_key: "cell_editor",
					dhx_id: "cell_editor"
				}, t)
			}, r.prototype._initHandlers = function () {
				var e = this;
				this._handlers = {
					onBlur: function () {
						e.endEdit()
					},
					onkeydown: function (t) {
						"Escape" === t.key && e.endEdit()
					},
					didInsert: function (t) {
						t = t.el;
						(e._input = t).focus(), t.addEventListener("blur", e._handlers.onBlur), t.addEventListener("keydown", e._handlers.onkeydown)
					}
				}
			}, r);

		function r(t, e, i) {
			this._config = i, this._cell = {
				row: t,
				col: e
			}, this._initHandlers()
		}
		e.SelectEditor = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(0),
			s = i(9),
			o = i(34),
			a = i(23),
			l = i(12),
			i = (n.prototype.endEdit = function (t, e) {
				var i = this;
				if (this._handlers) {
					var n = this._calendar.config.dateFormat,
						o = this._cell.row[this._cell.col.id];
					if (!t) {
						if (o instanceof Date || e) return this._value = this._calendar.getValue(), this._input.value = this._value, void this._popup.hide();
						a.stringToDate(this._input.value, n, !0) && (o && this._input.value.length === o.length || !o) && (this._value = this._input.value)
					}
					this._config.events.fire(s.GridEvents.beforeEditEnd, [this._value, this._cell.row, this._cell.col]) ? (this._input.removeEventListener("focus", this._handlers.onFocus), this._input.removeEventListener("change", this._handlers.onChange), document.removeEventListener("mousedown", this._handlers.onOuterClick), r.awaitRedraw().then(function () {
						i._popup.destructor(), i._calendar.destructor()
					}), this._cell.row = this._config.datacollection.getItem(this._cell.row.id), this._config.events.fire(s.GridEvents.afterEditEnd, [this._value, this._cell.row, this._cell.col])) : this._input.focus()
				}
			}, n.prototype.toHTML = function () {
				var t = this._cell.row[this._cell.col.id];
				return this._config.$editable.editor = this, document.addEventListener("mousedown", this._handlers.onOuterClick), r.el("input.dhx_cell-editor.dhx_cell-editor__input.dhx_cell-editor__datepicker", {
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_key: "cell_editor",
					dhx_id: "cell_editor",
					value: t
				})
			}, n.prototype._initHandlers = function () {
				var i = this;
				this._handlers = {
					onFocus: function () {
						r.awaitRedraw().then(function () {
							i._popup.show(i._input, {
								centering: !0,
								mode: "bottom"
							})
						})
					},
					onChange: function () {
						i.endEdit()
					},
					onOuterClick: function (t) {
						var e;
						t.target instanceof Node && (e = i._input && i._input.contains(t.target), t = i._popup && i._popup.getRootNode() && i._popup.getRootNode().contains(t.target), e || t || i._popup.hide())
					},
					didInsert: function (t) {
						t = t.el;
						i._input = t, i._input.addEventListener("focus", i._handlers.onFocus), i._input.addEventListener("change", i._handlers.onChange), t.focus(), t.setSelectionRange(t.value.length, t.value.length)
					}
				}
			}, n);

		function n(t, e, i) {
			var n = this;
			this._config = i, this._cell = {
				row: t,
				col: e
			}, this._calendar = new o.Calendar(null, {
				dateFormat: e.format
			});
			t = this._cell.row[this._cell.col.id], e = this._calendar.config.dateFormat;
			(a.stringToDate(t, e, !0) || t instanceof Date) && (this._calendar.setValue(t), this._value = this._calendar.getValue(), this._cell.row[this._cell.col.id] = this._value), this._popup = new l.Popup({
				css: "dhx_widget--bordered"
			}), this._popup.attach(this._calendar), this._calendar.events.on(o.CalendarEvents.change, function () {
				n.endEdit(!1, !0)
			}), this._popup.events.on(l.PopupEvents.afterHide, function () {
				n.endEdit()
			}), this._initHandlers()
		}
		e.DateEditor = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			m = this && this.__assign || function () {
				return (m = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			y = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(1),
			b = i(0),
			a = i(3),
			l = i(4),
			c = i(43),
			u = i(164),
			w = i(23),
			d = i(75),
			h = i(2),
			o = (r = l.View, o(f, r), f.prototype.setValue = function (t) {
				if (!t || t instanceof Array && 0 === t.length) return !1;
				this._selected = [];
				var e = t instanceof Array ? t[0] : t,
					i = w.DateHelper.toDateObject(e, this.config.dateFormat),
					e = w.DateHelper.copy(this._getSelected());
				return !!this.events.fire(d.CalendarEvents.beforeChange, [i, e, !1]) && (this._setSelected(t), this._timepicker && (this._timepicker.setValue(i), this._time = this._timepicker.getValue()), this.showDate(this._getSelected()), this.events.fire(d.CalendarEvents.change, [i, e, !1]), this.paint(), !0)
			}, f.prototype.getValue = function (t) {
				var e = this;
				return void 0 === t && (t = !1), this._selected[0] ? this.config.range ? t ? this._selected.map(function (t) {
					return w.DateHelper.copy(t)
				}) : this._selected.map(function (t) {
					return w.getFormattedDate(e.config.dateFormat, t)
				}) : t ? w.DateHelper.copy(this._selected[0]) : w.getFormattedDate(this.config.dateFormat, this._selected[0]) : ""
			}, f.prototype.getCurrentMode = function () {
				return this._currentViewMode
			}, f.prototype.showDate = function (t, e) {
				t && (this._currentDate = w.DateHelper.copy(t)), e && (this._currentViewMode = e), this.paint()
			}, f.prototype.destructor = function () {
				this._linkedCalendar && this._unlink(), this._timepicker && this._timepicker.destructor(), this.events && this.events.clear(), this.config = this.events = null, this._uid = this._selected = this._currentDate = this._currentViewMode = this._handlers = this._timepicker = this._time = null, this.unmount()
			}, f.prototype.clear = function () {
				var t = this.getValue(!0);
				this.config.timePicker && (this._timepicker.clear(), this._time = this._timepicker.getValue()), this._selected = [], this.showDate(null, this.config.mode), this.events.fire(d.CalendarEvents.change, [this.getValue(!0), t, !1])
			}, f.prototype.link = function (t) {
				var e = this;
				this._linkedCalendar && this._unlink(), this._linkedCalendar = t;
				var i = this.getValue(!0),
					n = t.getValue(!0),
					o = i && w.DateHelper.dayStart(i),
					r = n && w.DateHelper.dayStart(n);
				this.config.$rangeMark && this._linkedCalendar.config.$rangeMark || (this.config.$rangeMark = this._linkedCalendar.config.$rangeMark = function (t) {
					if (o && r) return o <= t && t <= r && function (t) {
						if (w.DateHelper.isSameDay(r, o)) return null;
						var e = "dhx_calendar-day--in-range";
						return w.DateHelper.isSameDay(t, o) && (e += " dhx_calendar-day--first-date"), w.DateHelper.isSameDay(t, r) && (e += " dhx_calendar-day--last-date"), e
					}(t)
				}), this.config.disabledDates && this._linkedCalendar.config.disabledDates || (this.config.disabledDates = function (t) {
					if (r) return r < t
				}, this._linkedCalendar.config.disabledDates = function (t) {
					if (o) return t < o
				}), this.config.thisMonthOnly = !0, t.config.thisMonthOnly = !0, this.events.on(d.CalendarEvents.change, function (t) {
					o = w.DateHelper.dayStart(t), e._linkedCalendar.paint()
				}, "link"), this._linkedCalendar.events.on(d.CalendarEvents.change, function (t) {
					r = w.DateHelper.dayStart(t), e.paint()
				}, "link"), this._linkedCalendar.paint(), this.paint()
			}, f.prototype._unlink = function () {
				this._linkedCalendar && (this.config.$rangeMark = this._linkedCalendar.config.$rangeMark = null, this.config.disabledDates = this._linkedCalendar.config.disabledDates = null, this.events.detach(d.CalendarEvents.change, "link"), this._linkedCalendar.events.detach(d.CalendarEvents.change, "link"), this._linkedCalendar.paint(), this._linkedCalendar = null)
			}, f.prototype._setSelected = function (t) {
				var i, n = this,
					e = t instanceof Array ? t[0] : t,
					e = w.DateHelper.toDateObject(e, this.config.dateFormat);
				t instanceof Array && this.config.range ? (i = [], t.forEach(function (t, e) {
					e < 2 && i.push(w.DateHelper.toDateObject(t, n.config.dateFormat))
				}), 2 === i.length && i[0] < i[1] ? i.forEach(function (t) {
					return n._selected.push(t)
				}) : this._selected[0] = i[0]) : this._selected[0] = e
			}, f.prototype._getSelected = function () {
				return this._selected[this._selected.length - 1]
			}, f.prototype._draw = function () {
				switch (this._currentViewMode) {
					case "calendar":
						return this.events.fire(d.CalendarEvents.modeChange, ["calendar"]), this._drawCalendar();
					case "month":
						return this.events.fire(d.CalendarEvents.modeChange, ["month"]), this._drawMonthSelector();
					case "year":
						return this.events.fire(d.CalendarEvents.modeChange, ["year"]), this._drawYearSelector();
					case "timepicker":
						return this.events.fire(d.CalendarEvents.modeChange, ["timepicker"]), this._drawTimepicker()
				}
			}, f.prototype._initHandlers = function () {
				function n(t) {
					void 0 === t && (t = !1);
					var e = 0;
					switch (o._currentViewMode) {
						case "calendar":
							e = t ? -7 : 7;
							break;
						case "month":
							e = t ? -4 : 4;
							break;
						case "year":
							e = t ? -4 : 4
					}
					return e
				}
				var o = this,
					r = {
						Up: "ArrowUp",
						Down: "ArrowDown",
						Right: "ArrowRight",
						Left: "ArrowLeft",
						Esc: "Escape",
						Spacebar: "Space"
					};
				this._handlers = {
					onkeydown: {
						".dhx_calendar-year, .dhx_calendar-month, .dhx_calendar-day": function (t, e) {
							switch (48 <= (i = t).which && i.which <= 57 || 65 <= i.which && i.which <= 90 ? String.fromCharCode(i.which) : (i = 32 === i.which ? i.code : i.key, h.isIE() && r[i] || i)) {
								case "Enter":
									o._selectDate(t, e);
									break;
								case "ArrowLeft":
									o._moveBrowseFocus(t, e, -1);
									break;
								case "ArrowRight":
									o._moveBrowseFocus(t, e, 1);
									break;
								case "ArrowUp":
									o._moveBrowseFocus(t, e, n(!0));
									break;
								case "ArrowDown":
									o._moveBrowseFocus(t, e, n())
							}
							var i
						}
					},
					onclick: {
						".dhx_calendar-year, .dhx_calendar-month, .dhx_calendar-day": function (t, e) {
							o._selectDate(t, e)
						},
						".dhx_calendar-action__cancel": function () {
							o.showDate(o._getSelected(), "calendar"), o.events.fire(d.CalendarEvents.cancelClick, [])
						},
						".dhx_calendar-action__show-month": function () {
							return o.showDate(null, "month")
						},
						".dhx_calendar-action__show-year": function () {
							return o.showDate(null, "year")
						},
						".dhx_calendar-action__next": function () {
							var t;
							switch (o._currentViewMode) {
								case "calendar":
									t = w.DateHelper.addMonth(o._currentDate, 1);
									break;
								case "month":
									t = w.DateHelper.addYear(o._currentDate, 1);
									break;
								case "year":
									t = w.DateHelper.addYear(o._currentDate, 12)
							}
							o.showDate(t)
						},
						".dhx_calendar-action__prev": function () {
							var t;
							switch (o._currentViewMode) {
								case "calendar":
									t = w.DateHelper.addMonth(o._currentDate, -1);
									break;
								case "month":
									t = w.DateHelper.addYear(o._currentDate, -1);
									break;
								case "year":
									t = w.DateHelper.addYear(o._currentDate, -12)
							}
							o.showDate(t)
						},
						".dhx_calendar-action__show-timepicker": function () {
							o._currentViewMode = "timepicker", o.paint()
						}
					},
					onmouseover: {
						".dhx_calendar-day": function (t, e) {
							o.events.fire(d.CalendarEvents.dateMouseOver, [new Date(e.attrs._date), t]), o.events.fire(d.CalendarEvents.dateHover, [new Date(e.attrs._date), t])
						}
					}
				}
			}, f.prototype._getData = function (r) {
				var s = this;
				this._isSelectedInCurrentRange = !1;
				for (var t = "monday" === this.config.weekStart ? 1 : 0, e = [], i = 6, a = w.DateHelper.weekStart(w.DateHelper.monthStart(r), t); i--;) {
					for (var n = w.DateHelper.getWeekNumber(a), l = 0, o = 7, c = [], u = function () {
						var t, e = w.DateHelper.isWeekEnd(a),
							i = r.getMonth() === a.getMonth(),
							n = d.config.disabledDates && d.config.disabledDates(a),
							o = [];
						d.config.range && d._selected[0] && d._selected[1] && (t = function () {
							if (s._selected[0] && s._selected[1]) {
								var t = w.DateHelper.dayStart(s._selected[0]),
									e = w.DateHelper.dayStart(s._selected[1]);
								return t <= a && a <= e && (w.DateHelper.isSameDay(s._selected[0], s._selected[1]) ? null : "dhx_calendar-day--in-range")
							}
						}, d.config.$rangeMark = t), e && i && o.push("dhx_calendar-day--weekend"), i || (d.config.thisMonthOnly ? (l++, o.push("dhx_calendar-day--hidden")) : o.push("dhx_calendar-day--muffled")), !d.config.mark || (i = d.config.mark(a)) && o.push(i), d.config.$rangeMark && (t = d.config.$rangeMark(a)) && o.push(t), n && (e ? o.push("dhx_calendar-day--weekend-disabled") : o.push("dhx_calendar-day--disabled")), d._selected.forEach(function (t, e) {
							t && w.DateHelper.isSameDay(t, a) && (s._isSelectedInCurrentRange = !0, t = "dhx_calendar-day--selected", s.config.range && (t += " dhx_calendar-day--selected-" + (0 === e ? "first " : "last")), o.push(t))
						}), c.push({
							date: a,
							day: a.getDate(),
							css: o.join(" ")
						}), a = w.DateHelper.addDay(a)
					}, d = this; o--;) u();
					e.push({
						weekNumber: n,
						days: c,
						disabledWeekNumber: 7 === l
					})
				}
				return e
			}, f.prototype._drawCalendar = function () {
				for (var t, i = this, e = this._currentDate, n = this.config, o = n.weekStart, r = n.thisMonthOnly, s = n.css, a = n.timePicker, l = n.width, n = ("monday" === o ? y(w.locale.daysShort.slice(1), [w.locale.daysShort[0]]) : w.locale.daysShort).map(function (t) {
					return b.el(".dhx_calendar-weekday", t)
				}), o = this._getData(e), c = !0, u = this._getSelected(), d = function (t) {
					var e = {
						role: "button",
						tabindex: -1,
						"aria-pressed": "false"
					};
					return t && (i._isSelectedInCurrentRange ? (t = t.date) && u && t.getTime() === u.getTime() && (e.tabindex = 0, e["aria-pressed"] = "true") : c && (e.tabindex = 0), c = !1), e
				}, h = [], f = [], p = 0, _ = o; p < _.length; p++) {
					var v = _[p],
						g = v.days.map(function (t) {
							return b.el("div.dhx_calendar-day", m({
								class: t.css,
								_date: t.date
							}, d(t)), t.day)
						});
					!this.config.weekNumbers || v.disabledWeekNumber && r || f.push(b.el("div", {
						class: "dhx_calendar-week-number"
					}, v.weekNumber)), h = h.concat(g)
				}
				this.config.weekNumbers && (t = b.el(".dhx_calendar__week-numbers", f));
				s = "dhx_calendar dhx_widget" + (s ? " " + s : "") + (a ? " dhx_calendar--with_timepicker" : "") + (this.config.weekNumbers ? " dhx_calendar--with_week-numbers" : "");
				return b.el("div", m({
					class: s,
					style: {
						width: this.config.weekNumbers ? "calc(" + l + " + 48px )" : l
					}
				}, this._handlers), [b.el(".dhx_calendar__wrapper", [this._drawHeader(b.el("button.dhx_calendar-action__show-month.dhx_button.dhx_button--view_link.dhx_button--size_small.dhx_button--color_secondary.dhx_button--circle", {
					"aria-live": "polite",
					type: "button"
				}, w.locale.months[e.getMonth()] + " " + e.getFullYear())), this.config.weekNumbers && b.el(".dhx_calendar__dates-wrapper", [b.el(".dhx_calendar__weekdays", n), b.el(".dhx_calendar__days", h), t]), !this.config.weekNumbers && b.el(".dhx_calendar__weekdays", n), !this.config.weekNumbers && b.el(".dhx_calendar__days", h), a ? b.el(".dhx_timepicker__actions", [b.el("button.dhx_calendar__timepicker-button.dhx_button.dhx_button--view_link.dhx_button--size_small.dhx_button--color_secondary.dhx_button--width_full.dhx_button--circle.dhx_calendar-action__show-timepicker", {
					type: "button"
				}, [b.el("span.dhx_button__icon.dxi.dxi-clock-outline"), b.el("span.dhx_button__text", this._time)])]) : null])])
			}, f.prototype._drawMonthSelector = function () {
				function o(t) {
					return u && e === t
				}
				var t = this._currentDate,
					e = t.getMonth(),
					i = this._getSelected() ? this._getSelected().getFullYear() : null,
					n = this.config,
					r = n.css,
					s = n.timePicker,
					a = n.weekNumbers,
					l = n.width,
					n = n.mode,
					s = "dhx_calendar dhx_widget" + (r ? " " + r : "") + (s ? " dhx_calendar--with_timepicker" : "") + (a ? " dhx_calendar--with_week-numbers" : ""),
					c = !0,
					u = i === t.getFullYear();
				return b.el("div", m({
					class: s,
					style: {
						width: a ? "calc(" + l + " + 48px)" : l
					}
				}, this._handlers), [b.el(".dhx_calendar__wrapper", [this._drawHeader(b.el("button.dhx_calendar-action__show-year.dhx_button.dhx_button--view_link.dhx_button--size_small.dhx_button--color_secondary.dhx_button--circle", {
					"aria-live": "polite",
					type: "button"
				}, t.getFullYear())), b.el(".dhx_calendar__months", w.locale.monthsShort.map(function (t, e) {
					return b.el("div", m(m({
						class: "dhx_calendar-month" + (o(e) ? " dhx_calendar-month--selected" : "")
					}, (i = e, n = {
						role: "button",
						tabindex: -1,
						"aria-pressed": "false"
					}, t && (u ? o(i) && (n.tabindex = 0, n["aria-pressed"] = "true") : c && (n.tabindex = 0), c = !1), n)), {
						_date: e
					}), t);
					var i, n
				})), "month" !== n ? b.el(".dhx_calendar__actions", [b.el("button.dhx_button.dhx_button--color_primary.dhx_button--view_link.dhx_button--size_small.dhx_button--width_full.dhx_button--circle.dhx_calendar-action__cancel", {
					type: "button"
				}, w.locale.cancel)]) : null])])
			}, f.prototype._drawYearSelector = function () {
				function n(t) {
					return e._getSelected() && t === e._getSelected().getFullYear()
				}
				var e = this,
					t = this._currentDate,
					i = w.DateHelper.getTwelweYears(t),
					o = this.config,
					r = o.css,
					s = o.timePicker,
					a = o.weekNumbers,
					t = o.width,
					o = o.mode,
					s = "dhx_calendar dhx_widget" + (r ? " " + r : "") + (s ? " dhx_calendar--with_timepicker" : "") + (a ? " dhx_calendar--with_week-numbers" : ""),
					l = !0,
					c = this._getSelected() && i.includes(this._getSelected().getFullYear());
				return b.el("div", m({
					class: s,
					style: {
						width: a ? "calc(" + t + " + 48px)" : t
					}
				}, this._handlers), [b.el(".dhx_calendar__wrapper", [this._drawHeader(b.el("button.dhx_button.dhx_button--view_link.dhx_button--size_small.dhx_button--color_secondary.dhx_button--circle", {
					"aria-live": "polite",
					type: "button"
				}, i[0] + "-" + i[i.length - 1])), b.el(".dhx_calendar__years", i.map(function (t) {
					return b.el("div", m({
						class: "dhx_calendar-year" + (n(t) ? " dhx_calendar-year--selected" : ""),
						_date: t
					}, (i = {
						role: "button",
						tabindex: -1,
						"aria-pressed": "false"
					}, (e = t) && (c ? n(e) && (i.tabindex = 0, i["aria-pressed"] = "true") : l && (i.tabindex = 0), l = !1), i)), t);
					var e, i
				})), "year" !== o && "month" !== o ? b.el(".dhx_calendar__actions", [b.el("button.dhx_button.dhx_button--color_primary.dhx_button--view_link.dhx_button--size_small.dhx_button--width_full.dhx_button--circle.dhx_calendar-action__cancel", {
					type: "button"
				}, w.locale.cancel)]) : null])])
			}, f.prototype._drawHeader = function (t) {
				return b.el(".dhx_calendar__navigation", [b.el("button.dhx_calendar-navigation__button.dhx_calendar-action__prev" + u.linkButtonClasses + ".dhx_button--icon.dhx_button--circle", {
					"aria-label": "prev",
					type: "button"
				}, [b.el(".dhx_button__icon.dxi.dxi-chevron-left")]), t, b.el("button.dhx_calendar-navigation__button.dhx_calendar-action__next" + u.linkButtonClasses + ".dhx_button--icon.dhx_button--circle", {
					"aria-label": "next",
					type: "button"
				}, [b.el(".dhx_button__icon.dxi.dxi-chevron-right")])])
			}, f.prototype._drawTimepicker = function () {
				var t = this.config,
					e = t.css,
					i = t.weekNumbers,
					t = t.width;
				return b.el(".dhx_widget.dhx-calendar", {
					class: e ? " " + e : "",
					style: {
						width: i ? "calc(" + t + " + 48px)" : t
					}
				}, [b.inject(this._timepicker.getRootView())])
			}, f.prototype._selectDate = function (t, e) {
				var i = e.attrs._date,
					n = w.DateHelper.copy(this._getSelected());
				switch (this._currentViewMode) {
					case "calendar":
						var o = this.config.timePicker ? w.DateHelper.mergeHoursAndMinutes(i, this._getSelected() || this._currentDate) : i;
						if (!this.events.fire(d.CalendarEvents.beforeChange, [o, n, !0])) return;
						this.config.range && 1 === this._selected.length && this._selected[0] < o ? this._selected.push(o) : (this._selected = [], this._selected[0] = o), e.el.blur(), this.showDate(this._getSelected()), this.events.fire(d.CalendarEvents.change, [i, n, !0]);
						break;
					case "month":
						if ("month" !== this.config.mode) w.DateHelper.setMonth(this._currentDate, i), this.showDate(null, "calendar"), this.events.fire(d.CalendarEvents.monthSelected, [i]);
						else {
							var r = w.DateHelper.fromYearAndMonth(this._currentDate.getFullYear() || this._getSelected().getFullYear(), i);
							if (!this.events.fire(d.CalendarEvents.beforeChange, [r, n, !0])) return;
							this._currentDate = r, this._selected[0] = r, this.events.fire(d.CalendarEvents.change, [this._getSelected(), n, !0]), this.events.fire(d.CalendarEvents.monthSelected, [i]), this.paint()
						}
						break;
					case "year":
						if ("year" !== this.config.mode) w.DateHelper.setYear(this._currentDate, i), this.showDate(null, "month"), this.events.fire(d.CalendarEvents.yearSelected, [i]);
						else {
							r = w.DateHelper.fromYear(i);
							if (!this.events.fire(d.CalendarEvents.beforeChange, [r, n, !0])) return;
							this._currentDate = r, this._selected[0] = r, this.events.fire(d.CalendarEvents.change, [this._getSelected(), n, !0]), this.events.fire(d.CalendarEvents.yearSelected, [i]), this.paint()
						}
				}
			}, f.prototype._moveBrowseFocus = function (t, e, i) {
				e && (!(i = e.parent.body[e.idx + i]) || (i = i.el) && (t.target.tabIndex = -1, i.tabIndex = 0, i.focus({
					preventScroll: !0
				})))
			}, f);

		function f(t, e) {
			void 0 === e && (e = {});
			var o = r.call(this, t, s.extend({
				weekStart: "sunday",
				thisMonthOnly: !1,
				dateFormat: window && window.dhx && window.dhx.dateFormat,
				width: "250px"
			}, e)) || this;
			switch (o._selected = [], o.events = new a.EventSystem, o.config.disabledDates = o.config.disabledDates || o.config.block, o.config.mode = o.config.mode || o.config.view, o.config.dateFormat || (o.config.timePicker ? 12 === o.config.timeFormat ? o.config.dateFormat = "%d/%m/%y %h:%i %A" : o.config.dateFormat = "%d/%m/%y %H:%i" : o.config.dateFormat = "%d/%m/%y"), o.config.value && o._setSelected(o.config.value), o.config.date ? o._currentDate = w.DateHelper.toDateObject(o.config.date, o.config.dateFormat) : o._getSelected() ? o._currentDate = w.DateHelper.copy(o._getSelected()) : o._currentDate = new Date, o.config.mode) {
				case "month":
					o._currentViewMode = "month";
					break;
				case "year":
					o._currentViewMode = "year";
					break;
				default:
					o._currentViewMode = "calendar"
			}
			o._initHandlers(), o.config.timePicker && (o._timepicker = new c.Timepicker(null, {
				timeFormat: o.config.timeFormat,
				controls: !0
			}), e = o._getSelected() || new Date, o._timepicker.setValue(e), o._time = o._timepicker.getValue(), o._timepicker.events.on(c.TimepickerEvents.afterClose, function () {
				o._timepicker.setValue(o._time), o.showDate(null, "calendar")
			}), o._timepicker.events.on(c.TimepickerEvents.afterApply, function () {
				var t = o._timepicker.getValue(!0),
					e = t.hour,
					i = t.minute,
					n = t.AM,
					t = o._getSelected(),
					n = w.DateHelper.withHoursAndMinutes(o._getSelected() || new Date, e, i, n);
				o.events.fire(d.CalendarEvents.beforeChange, [n, t, !0]) && (o._selected[o._selected.length - 1] = n, o.events.fire(d.CalendarEvents.change, [n, t, !0])), o._time = o._timepicker.getValue(), o.showDate(null, "calendar")
			}));
			return o.mount(t, b.create({
				render: function () {
					return o._draw()
				}
			})), o
		}
		e.Calendar = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s = i(1),
			a = i(0),
			l = i(3),
			c = i(4),
			u = i(11),
			d = i(45),
			h = i(73),
			f = i(163),
			p = i(74),
			_ = i(2);

		function v(t, e) {
			return isNaN(t) ? 0 : Math.min(e, Math.max(0, t))
		}
		var g, o = (g = c.View, o(m, g), m.prototype.getValue = function (t) {
			12 === this.config.timeFormat && (this._time.hour = this._time.hour % 12 || 12);
			var e = this._time,
				i = e.hour,
				n = e.minute,
				e = e.AM;
			if (t) {
				t = {
					hour: i,
					minute: n
				};
				return 12 === this.config.timeFormat && (t.AM = e), t
			}
			return (i < 10 ? "0" + i : i) + ":" + (n < 10 ? "0" + n : n) + (12 === this.config.timeFormat ? e ? "AM" : "PM" : "")
		}, m.prototype.setValue = function (t) {
			this._setValue(t), this._hoursSlider.setValue(this._time.hour), this._minutesSlider.setValue(this._time.minute), this._inputsView.paint()
		}, m.prototype.clear = function () {
			24 === this.config.timeFormat ? this.setValue("00:00") : this.setValue("12:00AM")
		}, m.prototype.destructor = function () {
			this._minutesSlider && this._minutesSlider.destructor(), this._hoursSlider && this._hoursSlider.destructor(), this.events && this.events.clear(), this.layout && this.layout.destructor(), this.config = this.events = null, this._handlers = this._time = this._inputsView = this._minutesSlider = this._hoursSlider = null, this.unmount()
		}, m.prototype.getRootView = function () {
			return this.layout.getRootView()
		}, m.prototype._setValue = function (t) {
			var e, i, n = 0,
				o = 0;
			return "number" == typeof t && (t = new Date(t)), t instanceof Date ? (n = t.getMinutes(), o = t.getHours()) : Array.isArray(t) ? (o = v(t[0], 23), n = v(t[1], 59), t[2] && "pm" === t[2].toLowerCase() && (e = !0)) : "string" == typeof t ? (o = v(+(i = t.match(/\d+/g))[0], 23), n = v(+i[1], 59), t.toLowerCase().includes("pm") && (e = !0)) : "object" == typeof t && t.hasOwnProperty("hour") && t.hasOwnProperty("minute") && (o = t.hour, n = t.minute, e = !t.AM), e && o < 12 && (o += 12), 12 === this.config.timeFormat && !f.isTimeCheck(t) && 12 <= o && (e = !0), this._time = {
				hour: o,
				minute: n,
				AM: !e
			}
		}, m.prototype._initUI = function (t) {
			var e = this,
				i = {
					gravity: !1,
					css: "dhx_widget dhx_timepicker " + (this.config.css || "") + (this.config.controls ? " dhx_timepicker--with-controls" : ""),
					rows: [{
						id: "timepicker",
						css: "dhx_timepicker__inputs"
					}, {
						id: "hour-slider",
						css: "dhx_timepicker__hour"
					}, {
						id: "minute-slider",
						css: "dhx_timepicker__minute"
					}]
				};
			this.config.controls && (i.rows.unshift({
				id: "close-action",
				css: "dhx_timepicker__close"
			}), i.rows.push({
				id: "save-action",
				css: "dhx_timepicker__save"
			}));
			var n = this.layout = new u.Layout(t, i),
				o = a.create({
					render: function () {
						return e._draw()
					}
				}),
				t = this._inputsView = c.toViewLike(o),
				i = this._minutesSlider = new d.Slider(null, {
					min: 0,
					max: 59,
					step: 1,
					tooltip: !1,
					labelPosition: "top",
					label: h.default.minutes,
					value: this.config.value ? this._time.minute : 0
				}),
				o = this._hoursSlider = new d.Slider(null, {
					min: 0,
					max: 23,
					step: 1,
					tooltip: !1,
					labelPosition: "top",
					label: h.default.hours,
					value: !this.config.value || 12 === this._time.hour && this._time.AM ? 0 : this._time.hour
				});
			n.getCell("timepicker").attach(t), n.getCell("hour-slider").attach(o), n.getCell("minute-slider").attach(i), this.config.controls && (n.getCell("save-action").attach(function () {
				return a.el("button.dhx_timepicker__button-save.dhx_button.dhx_button--view_flat.dhx_button--color_primary.dhx_button--size_small.dhx_button--circle.dhx_button--width_full", {
					onclick: e._outerHandlers.save,
					type: "button"
				}, h.default.save)
			}), n.getCell("close-action").attach(function () {
				return a.el("button.dhx_timepicker__button-close.dhx_button.dhx_button--view_link.dhx_button--size_medium.dhx_button--view_link.dhx_button--color_secondary.dhx_button--icon.dhx_button--circle", {
					_ref: "close",
					onclick: e._outerHandlers.close,
					type: "button",
					"aria-label": "close timepicker"
				}, [a.el("span.dhx_button__icon.dxi.dxi-close")])
			}))
		}, m.prototype._initHandlers = function () {
			function e(t) {
				var e = v(parseInt(t.value, 10), 59);
				t.value = e.toString(), n._minutesSlider.setValue(e)
			}

			function i(t) {
				var e = v(parseInt(t.value, 10), 23);
				t.value = e.toString(), n._hoursSlider.setValue(e)
			}
			var n = this;
			this._handlers = {
				onchange: {
					".dhx_timepicker-input--hour": function (t) {
						return i(t.target)
					},
					".dhx_timepicker-input--minutes": function (t) {
						return e(t.target)
					}
				},
				oninput: {
					".dhx_timepicker-input--hour": function (t) {
						(_.isSafari() || _.isFirefox()) && i(t.target)
					},
					".dhx_timepicker-input--minutes": function (t) {
						(_.isSafari() || _.isFirefox()) && e(t.target)
					}
				}
			}, this._outerHandlers = {
				close: function () {
					n.events.fire(p.TimepickerEvents.beforeClose, [n.getValue("timeObject" === n.config.valueFormat)]) && (n.events.fire(p.TimepickerEvents.afterClose, [n.getValue("timeObject" === n.config.valueFormat)]), n.events.fire(p.TimepickerEvents.close, []))
				},
				save: function () {
					n.events.fire(p.TimepickerEvents.beforeApply, [n.getValue("timeObject" === n.config.valueFormat)]) && (n.events.fire(p.TimepickerEvents.afterApply, [n.getValue("timeObject" === n.config.valueFormat)]), n.events.fire(p.TimepickerEvents.apply, [n.getValue()]), n.events.fire(p.TimepickerEvents.save, [n._time]))
				}
			}
		}, m.prototype._initEvents = function () {
			var e = this;
			this._hoursSlider.events.on(d.SliderEvents.change, function (t) {
				t < e._hoursSlider.config.min || t > e._hoursSlider.config.max || (12 === e.config.timeFormat ? (e._time.AM = t < 12, e._time.hour = t % 12 || 12) : e._time.hour = t, e.events.fire(p.TimepickerEvents.change, [e.getValue("timeObject" === e.config.valueFormat)]), e._inputsView.paint())
			}), this._minutesSlider.events.on(d.SliderEvents.change, function (t) {
				t < e._minutesSlider.config.min || t > e._minutesSlider.config.max || (e._time.minute = t, e.events.fire(p.TimepickerEvents.change, [e.getValue("timeObject" === e.config.valueFormat)]), e._inputsView.paint())
			})
		}, m.prototype._draw = function () {
			return a.el(".dhx_timepicker-inputs", r({}, this._handlers), [a.el("input.dhx_timepicker-input.dhx_timepicker-input--hour", {
				_key: "hour",
				_ref: "hour",
				value: 1 < this.getValue(!0).hour.toString().length ? this.getValue(!0).hour : "0" + this.getValue(!0).hour,
				"aria-label": "hours"
			}), a.el("span.dhx_timepicker-delimer", ":"), a.el("input.dhx_timepicker-input.dhx_timepicker-input--minutes", {
				_key: "minute",
				value: 1 < this.getValue(!0).minute.toString().length ? this.getValue(!0).minute : "0" + this.getValue(!0).minute,
				"aria-label": "minutes"
			}), 12 === this.config.timeFormat ? a.el(".dhx_timepicker-ampm", this._time.AM ? "AM" : "PM") : null])
		}, m);

		function m(t, e) {
			void 0 === e && (e = {});
			e = g.call(this, t, s.extend({
				timeFormat: 24,
				controls: !1,
				valueFormat: "string",
				actions: !1
			}, e)) || this;
			return e.events = new l.EventSystem(e), e._time = {
				hour: 0,
				minute: 0,
				AM: !0
			}, 12 === e.config.timeFormat && (e._time.hour = 12), e.config.controls = e.config.controls || e.config.actions, e.config.value && e._setValue(e.config.value), e._initUI(t), e._initHandlers(), e._initEvents(), e
		}
		e.Timepicker = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.getBlockRange = function (t, e, i) {
			return void 0 === i && (i = !0), i ? {
				min: t.left + window.pageXOffset,
				max: e.right + window.pageXOffset
			} : {
				min: t.top + window.pageYOffset,
				max: e.bottom + window.pageYOffset
			}
		}, e.getMarginSize = function (t, e) {
			return t && ("space" === t.type || "wide" === t.type && e) ? 10 : 0
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(69),
			a = i(160),
			o = (r = s.Layout, o(l, r), l.prototype._createCell = function (t) {
				var e = t.rows || t.cols || t.views ? (t.parent = this._root, new l(this, t)) : new a.ProCell(this, t);
				return this._root._all[e.id] = e, t.init && t.init(e, t), e
			}, l);

		function l(t, e) {
			return r.call(this, t, e) || this
		}
		e.ProLayout = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			c = this && this.__assign || function () {
				return (c = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, u = i(1),
			d = i(0),
			s = i(17),
			i = i(70),
			o = (r = i.Cell, o(a, r), a.prototype._getFirstRootView = function (t) {
				return void 0 === t && (t = this), t.getParent() && t.getParent().getRootView() ? t.getParent().getRootView() : this._getFirstRootView(t.getParent())
			}, a.prototype.toVDOM = function (t) {
				if (null === this.config && (this.config = {}), !this.config.hidden) {
					var e, i = this._calculateStyle(),
						n = u.isDefined(this.config.padding) ? isNaN(Number(this.config.padding)) ? {
							padding: this.config.padding
						} : {
							padding: this.config.padding + "px"
						} : "",
						o = this.config.full || this.config.html ? i : c(c({}, i), n);
					this.config.html || (e = this._ui ? ((l = this._ui.getRootView()).render && (l = d.inject(l)), l ? [this.scrollView.render(l)] : l || null) : t ? this.scrollView.render([t]) : t || null);
					var i = !this.config.resizable || this._isLastCell() || this.config.collapsed ? null : d.el(".dhx_layout-resizer." + (this._isXDirection() ? "dhx_layout-resizer--x" : "dhx_layout-resizer--y"), c(c({}, this._resizerHandlers), {
						_ref: "resizer_" + this._uid
					}), [d.el("span.dhx_layout-resizer__icon", {
						class: "dxi " + (this._isXDirection() ? "dxi-dots-vertical" : "dxi-dots-horizontal")
					})]),
						r = {};
					if (this.config.on)
						for (var s in this.config.on) r["on" + s] = this.config.on[s];
					var a = "",
						l = this.config.cols || this.config.rows;
					if (this.config.type && l) switch (this.config.type) {
						case "line":
							a = " dhx_layout-line";
							break;
						case "wide":
							a = " dhx_layout-wide";
							break;
						case "space":
							a = " dhx_layout-space"
					}
					t = d.el(".dhx_layout-cell-content", {
						".innerHTML": this.config.html,
						_key: this._uid + "_html",
						style: n
					}), t = d.el("div", c(c(((l = {
						_key: this._uid,
						_ref: this._uid
					})["aria-label"] = this.config.id ? "tab-content-" + this.config.id : null, l), r), {
						class: this._getCss(!1) + (this.config.css ? " " + this.config.css : "") + (this.config.collapsed ? " dhx_layout-cell--collapsed" : "") + (this.config.resizable ? " dhx_layout-cell--resizable" : "") + (this.config.type && !this.config.full ? a : ""),
						style: o
					}), this.config.full ? [d.el("div", {
						tabindex: this.config.collapsable ? "0" : "-1",
						class: "dhx_layout-cell-header" + (this._isXDirection() ? " dhx_layout-cell-header--col" : " dhx_layout-cell-header--row") + (this.config.collapsable ? " dhx_layout-cell-header--collapseble" : "") + (this.config.collapsed ? " dhx_layout-cell-header--collapsed" : "") + (((this.getParent() || {}).config || {}).isAccordion ? " dhx_layout-cell-header--accordion" : ""),
						style: {
							height: this.config.headerHeight
						},
						onclick: this._handlers.toggle,
						onkeydown: this._handlers.enterCollapse
					}, [this.config.headerIcon && d.el("span.dhx_layout-cell-header__icon", {
						class: this.config.headerIcon
					}), this.config.headerImage && d.el(".dhx_layout-cell-header__image-wrapper", [d.el("img", {
						src: this.config.headerImage,
						class: "dhx_layout-cell-header__image"
					})]), this.config.header && d.el("h3.dhx_layout-cell-header__title", this.config.header), this.config.collapsable ? d.el("div.dhx_layout-cell-header__collapse-icon", {
						class: this._getCollapseIcon()
					}) : d.el("div.dhx_layout-cell-header__collapse-icon", {
						class: "dxi dxi-empty"
					})]), this.config.collapsed ? null : d.el("div", {
						style: c(c({}, n), {
							height: "calc(100% - " + (this.config.headerHeight || 37) + "px)"
						}),
						".innerHTML": this.config.html,
						class: this._getCss(!0) + " dhx_layout-cell-content" + (this.config.type ? a : "")
					}, e)] : !this.config.html || this.config.rows && this.config.cols && this.config.views ? e : [this.config.collapsed ? null : this.scrollView && this.scrollView.config.enable ? this.scrollView.render([t], this._uid) : t]);
					return i ? [t, i] : t
				}
			}, a);

		function a(t, e) {
			var i = r.call(this, t, e) || this;
			return i.scrollView = new s.ScrollView(function () {
				return i._getFirstRootView()
			}), i
		}
		e.ProCell = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			l = this && this.__assign || function () {
				return (l = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(1),
			c = i(0),
			s = i(3),
			a = i(13),
			u = i(4),
			d = i(12),
			h = i(72),
			f = i(2);

		function p(t, e, i) {
			return t < e ? e : i < t ? i : t
		}
		var _, o = (_ = u.View, o(v, _), v.prototype.disable = function () {
			this._disabled = !0, this.paint()
		}, v.prototype.enable = function () {
			this._disabled = !1, this.paint()
		}, v.prototype.isDisabled = function () {
			return this._disabled
		}, v.prototype.focus = function (t) {
			this.getRootView().refs[t ? "extraRunner" : "runner"].el.focus()
		}, v.prototype.blur = function () {
			this.getRootView().refs[this._isExtraActive ? "extraRunner" : "runner"].el.blur()
		}, v.prototype.getValue = function () {
			var t, e;
			return this.config.range ? (t = this._getValue(this._currentPosition)) < (e = this._getValue(this._extraCurrentPosition)) ? [t, e] : [e, t] : [this._getValue(this._currentPosition)]
		}, v.prototype.setValue = function (t) {
			var e = this._getValue(this._currentPosition);
			if (Array.isArray(t) && 1 < t.length) {
				var i = this._getValue(this._extraCurrentPosition);
				this._setValue(t[0], !1), this.events.fire(h.SliderEvents.change, [t[0], e, !1]), this._setValue(t[1], !0), this.events.fire(h.SliderEvents.change, [t[1], i, !0])
			} else {
				if (t = parseFloat(t), isNaN(t)) throw new Error("Wrong value type, for more info check documentation https://docs.dhtmlx.com/suite/slider__api__slider_setvalue_method.html");
				this._setValue(t), this.events.fire(h.SliderEvents.change, [t, e, !1])
			}
			this.paint()
		}, v.prototype.destructor = function () {
			this._keyManager && this._keyManager.destructor(), document.body.contains(this._tooltip) && document.body.removeChild(this._tooltip), this._tooltip = null, this.unmount()
		}, v.prototype._calcSliderPosition = function () {
			var t = this.getRootView();
			t && (t = t.refs.track.el.getBoundingClientRect(), this._offsets = {
				left: t.left + window.pageXOffset,
				top: t.top + window.pageYOffset
			}, this._length = "horizontal" === this.config.mode ? t.width : t.height)
		}, v.prototype._initHotkeys = function () {
			var t, e = this,
				i = {
					arrowLeft: function (t) {
						t.preventDefault(), e._move(-e.config.step, t.target.classList.contains("dhx_slider__thumb--extra"))
					},
					arrowRight: function (t) {
						t.preventDefault(), e._move(e.config.step, t.target.classList.contains("dhx_slider__thumb--extra"))
					},
					arrowUp: function (t) {
						t.preventDefault(), e._move(e.config.step, t.target.classList.contains("dhx_slider__thumb--extra"))
					},
					arrowDown: function (t) {
						t.preventDefault(), e._move(-e.config.step, t.target.classList.contains("dhx_slider__thumb--extra"))
					}
				};
			for (t in i) this._keyManager.addHotKey(t, i[t])
		}, v.prototype._move = function (t, e) {
			this.config.inverse && (t = -t);
			var i = this.config,
				n = i.max,
				o = i.min,
				r = e ? this._getValue(this._extraCurrentPosition) : this._getValue(this._currentPosition),
				i = r + t;
			this._setValue(r + t, e), (n < i || i < o) && (i = r), this.events.fire(h.SliderEvents.change, [i, r, e]), this.paint()
		}, v.prototype._initStartPosition = function () {
			var t, e, i = this.config,
				n = i.max,
				o = i.min,
				r = i.range,
				i = (t = this.config.value, e = this.config.min, i = this.config.max, (t = void 0 === t ? [] : Array.isArray(t) ? t : "string" == typeof t ? t.split(",").map(function (t) {
					return parseInt(t, 10)
				}) : [t])[0] = void 0 === t[0] ? e : p(t[0], e, i), t[1] = void 0 === t[1] ? i : p(t[1], e, i), t),
				t = i[0],
				i = i[1];
			this._currentPosition = (t - o) / (n - o) * 100, r && (this._extraCurrentPosition = (n - i) / (n - o) * 100), this._currentPosition = (t - o) / (n - o) * 100, r && (this._extraCurrentPosition = (i - o) / (n - o) * 100), this._isInverse() && (this._currentPosition = 100 - this._currentPosition, r && (this._extraCurrentPosition = 100 - this._extraCurrentPosition))
		}, v.prototype._getValue = function (t) {
			this._isInverse() && (t = 100 - t);
			var e = this.config,
				i = e.min,
				n = e.max,
				e = e.step;
			if (100 === t) return n;
			if (0 === t) return i;
			t = t * (n - i) / 100, n = t % e, e = e / 2 <= n ? e : 0;
			return +(Number(i) + Number(t) - n + e).toFixed(5)
		}, v.prototype._setValue = function (t, e) {
			void 0 === e && (e = !1);
			var i = this.config,
				n = i.max,
				i = i.min;
			if (n < t || t < i) return !1;
			i = (t - i) / (n - i) * 100, i = this._isInverse() ? 100 - i : i;
			e ? this._extraCurrentPosition = i : this._currentPosition = i
		}, v.prototype._initHandlers = function () {
			function e(t) {
				if (t.targetTouches || t.preventDefault(), t = ((t.targetTouches ? t.targetTouches[0] : t)[n._axis] - n._getBegining()) / n._length * 100, n._findNewDirection) {
					if (Math.abs(n._currentPosition - t) < 1) return;
					t > n._currentPosition ? n._possibleRange = [n._currentPosition, 100] : n._possibleRange = [0, n._currentPosition], n._findNewDirection = null
				}
				n._inSide(t) && n._updatePosition(t, n._isExtraActive), n.paint()
			}

			function i(t) {
				var e, i;
				n._disabled || 3 === t.which || (n.events.fire(h.SliderEvents.mousedown, [t]), n._isMouseMoving = !0, e = t.target.classList.contains("dhx_slider__thumb--extra") ? (n._isExtraActive = !0, n._extraCurrentPosition) : (n._isExtraActive = !1, n._currentPosition), n._findNewDirection = null, n.config.range ? (t = (i = n._currentPosition > n._extraCurrentPosition ? [n._currentPosition, n._extraCurrentPosition] : [n._extraCurrentPosition, n._currentPosition])[0], i = i[1], n._currentPosition === n._extraCurrentPosition ? (n._findNewDirection = e, n._possibleRange = [0, 100]) : n._possibleRange = e < t ? [0, t] : [i, 100]) : n._possibleRange = [0, 100])
			}
			var n = this,
				o = function (t) {
					n.events.fire(h.SliderEvents.mouseup, [t]), setTimeout(function () {
						n._isMouseMoving = !1, n.paint()
					}, 4), t.targetTouches ? (document.removeEventListener("touchend", o), document.removeEventListener("touchmove", e)) : (document.removeEventListener("mouseup", o), document.removeEventListener("mousemove", e))
				};
			this.config.helpMessage && (this._helper = new d.Popup({
				css: "dhx_tooltip dhx_tooltip--forced dhx_tooltip--light"
			}), this._helper.attachHTML(this.config.helpMessage)), this._handlers = {
				showHelper: function (t) {
					t.preventDefault(), t.stopPropagation(), n._helper.show(t.target)
				},
				onmousedown: function (t) {
					i(t), document.addEventListener("mousemove", e), document.addEventListener("mouseup", o)
				},
				ontouchstart: function (t) {
					n._setTooltip(t), n._mouseIn = !1, i(t), document.addEventListener("touchmove", e), document.addEventListener("touchend", o), n.paint()
				},
				ontouchend: function (t) {
					n._setTooltip(t), n._mouseIn = !1, n.paint()
				},
				onlabelClick: function () {
					n.getRootView().refs.runner.el.focus()
				},
				onclick: function (t) {
					var e;
					n._disabled || n._isMouseMoving || 3 === t.which || (e = (t[n._axis] - n._getBegining()) / n._length * 100, t = n.getRootView().refs, !n.config.range || Math.abs(n._currentPosition - e) < Math.abs(n._extraCurrentPosition - e) ? (n._updatePosition(e, !1), t.runner.el.focus()) : (n._updatePosition(e, !0), t.extraRunner.el.focus()), n.paint())
				},
				onmouseover: function (t) {
					n._setTooltip(t), n._mouseIn = !0, n.paint()
				},
				onmouseout: function (t) {
					n._setTooltip(t), n._mouseIn = !1, n.paint()
				},
				onfocus: function (t) {
					n._setTooltip(t), n._focusIn = !0, n.events.fire(h.SliderEvents.focus, []), n.paint()
				},
				onblur: function (t) {
					n._setTooltip(t), n._focusIn = !1, n.events.fire(h.SliderEvents.blur, []), n.paint()
				},
				onkeydown: function (t) {
					n.events.fire(h.SliderEvents.keydown, [t])
				}
			}
		}, v.prototype._getBegining = function () {
			return "horizontal" === this.config.mode ? this._offsets.left - window.pageXOffset : this._offsets.top - window.pageYOffset
		}, v.prototype._inSide = function (t) {
			var e = this._possibleRange;
			return t < e[0] ? (this._updatePosition(e[0], this._isExtraActive), !1) : !(t > e[1]) || (this._updatePosition(e[1], this._isExtraActive), !1)
		}, v.prototype._updatePosition = function (t, e) {
			void 0 === e && (e = !1), 100 < t && (t = 100), t < 0 && (t = 0);
			var i = this.config,
				n = i.max,
				o = i.min,
				i = e ? this._extraCurrentPosition : this._currentPosition,
				i = this._getValue(i),
				t = this._getValue(t);
			i !== t && (o = (t - o) / (n - o) * 100, o = this._isInverse() ? 100 - o : o, e ? this._extraCurrentPosition = o : this._currentPosition = o, this.events.fire(h.SliderEvents.change, [t, i, e]))
		}, v.prototype._getRunnerStyle = function (t) {
			void 0 === t && (t = !1);
			var e = "horizontal" === this.config.mode ? "left" : "top",
				i = t ? this._extraCurrentPosition : this._currentPosition;
			return (t = {})[e] = i + "%", t
		}, v.prototype._isInverse = function () {
			return this.config.inverse && "horizontal" === this.config.mode || !this.config.inverse && "vertical" === this.config.mode
		}, v.prototype._getRunnerCss = function (t) {
			return void 0 === t && (t = !1), "dhx_slider__thumb" + (t ? " dhx_slider__thumb--extra" : "") + (this._isMouseMoving && (t && this._isExtraActive || !t && !this._isExtraActive) ? " dhx_slider__thumb--active" : "") + (this._disabled ? " dhx_slider__thumb--disabled" : "") + (this._isNullable(t ? this._extraCurrentPosition : this._currentPosition) && !this.config.range ? " dhx_slider__thumb--nullable" : "")
		}, v.prototype._draw = function () {
			var t = this.config,
				e = t.labelPosition,
				i = t.mode,
				n = t.hiddenLabel,
				o = t.tick,
				r = t.majorTick,
				s = t.css,
				a = t.helpMessage,
				t = f.getLabelStyle(l(l({}, this.config), {
					required: !1
				}));
			return !this._tooltip || this._mouseIn && this._focusIn && this._isMouseMoving || document.body.contains(this._tooltip) && document.body.removeChild(this._tooltip), c.el("div", {
				class: "dhx_slider dhx_slider--mode_" + i + ("left" === e ? " dhx_slider--label-inline" : "") + (n ? " dhx_slider--label_sr" : "") + (o ? " dhx_slider--ticks" : "") + (r ? " dhx_slider--major-ticks" : "") + (s ? " " + s : "") + (this._disabled ? " dhx_slider--disabled" : "")
			}, [t ? c.el("label.dhx_label.dhx_slider__label", {
				style: t.style,
				class: a ? "dhx_label--with-help" : "",
				onclick: this._handlers.onlabelClick
			}, a ? [t.label && c.el("span.dhx_label__holder", t.label), c.el("span.dhx_label-help.dxi.dxi-help-circle-outline", {
				tabindex: "0",
				role: "button",
				onclick: this._handlers.showHelper
			})] : t.label) : null, this._drawSlider()])
		}, v.prototype._drawSlider = function () {
			return c.el(".dhx_widget.dhx_slider__track-holder", {
				dhx_widget_id: this._uid
			}, [c.el(".dhx_slider__track", {
				_ref: "track",
				onmouseover: this._handlers.onmouseover,
				onmouseout: this._handlers.onmouseout,
				onclick: this._handlers.onclick
			}, [this._getDetector(), c.el("div", {
				_ref: "runner",
				class: this._getRunnerCss(),
				ontouchstart: this._handlers.ontouchstart,
				ontouchend: this._handlers.ontouchend,
				onmousedown: this._handlers.onmousedown,
				onfocus: this._handlers.onfocus,
				onblur: this._handlers.onblur,
				onkeydown: this._handlers.onkeydown,
				style: this._getRunnerStyle(),
				tabindex: 0
			}), this.config.tooltip && (this._mouseIn || this._focusIn || this._isMouseMoving) ? this._drawTooltip() : null, this.config.tooltip && this.config.range && (this._mouseIn || this._focusIn || this._isMouseMoving) ? this._drawTooltip(!0) : null, this.config.range ? c.el("div", {
				_ref: "extraRunner",
				class: this._getRunnerCss(!0),
				ontouchstart: this._handlers.ontouchstart,
				ontouchend: this._handlers.ontouchend,
				onmousedown: this._handlers.onmousedown,
				onfocus: this._handlers.onfocus,
				onblur: this._handlers.onblur,
				onkeydown: this._handlers.onkeydown,
				style: this._getRunnerStyle(!0),
				tabindex: 0
			}) : null]), this.config.tick ? this._drawTicks() : null])
		}, v.prototype._getDetector = function () {
			var t;
			if (this._disabled) return c.el(".dhx_slider__range");
			var e = "horizontal" === this.config.mode ? "left" : "top",
				i = "horizontal" === this.config.mode ? "width" : "height";
			if (this.config.range) {
				var n = this._currentPosition > this._extraCurrentPosition ? [this._currentPosition, this._extraCurrentPosition] : [this._extraCurrentPosition, this._currentPosition],
					o = n[0],
					r = n[1];
				return c.el(".dhx_slider__range", {
					style: ((n = {})[e] = r + "%", n[i] = o - r + "%", n)
				})
			}
			return this._isInverse() ? c.el(".dhx_slider__range", {
				style: ((t = {})[e] = this._currentPosition + "%", t[i] = 100 - this._currentPosition + "%", t)
			}) : c.el(".dhx_slider__range", {
				style: ((t = {})[e] = 0, t[i] = this._currentPosition + "%", t)
			})
		}, v.prototype._drawTooltip = function (t) {
			var e, i, n;
			void 0 === t && (t = !1), "none" !== this._activeTooltip && this.getRootView() && (e = "extraTooltip" === this._activeTooltip ? this._extraCurrentPosition : this._currentPosition, i = "horizontal" === this.config.mode ? "left" : "top", n = "", (t && this._isExtraActive || !t && !this._isExtraActive) && (n += " dhx_slider__thumb-label--active"), this._tooltip || (this._tooltip = document.createElement("div")), t = ("tooltip" === this._activeTooltip ? this.getRootView().refs.runner : this.getRootView().refs.extraRunner).el.getBoundingClientRect(), this._tooltip.className = "dhx_slider__thumb-label" + n, this._tooltip.style.left = t.x + ("left" == i ? 6 : -30) + window.pageXOffset + "px", this._tooltip.style.top = t.y + ("left" == i ? -30 : 6) + window.pageYOffset + "px", this._tooltip.innerText = this._getValue(e).toString(), document.body.appendChild(this._tooltip))
		}, v.prototype._getTicks = function () {
			for (var t = this.config, e = t.max, i = t.min, n = t.step, o = t.tick, r = t.majorTick, s = e - i, a = n * o / s, l = [], c = 0, u = 0; c < 1;) {
				var d = +(Number(i) + c * s).toFixed(5),
					h = u % r == 0;
				l.push({
					position: (this._isInverse() ? 100 * (1 - c) : 100 * c) + "%",
					isMultiple: h,
					label: h && "function" == typeof this.config.tickTemplate ? this.config.tickTemplate(d) : null
				}), c += a, u++
			}
			return l.push({
				position: (this._isInverse() ? 0 : 100) + "%",
				isMultiple: !0,
				label: "function" == typeof this.config.tickTemplate ? this.config.tickTemplate(e) : null
			}), l
		}, v.prototype._drawTicks = function () {
			var i = "horizontal" === this.config.mode ? "left" : "top";
			return c.el(".dhx_slider__ticks-holder", this._getTicks().map(function (t) {
				var e;
				return c.el("div", {
					class: "dhx_slider__tick" + (t.isMultiple ? " dhx_slider__tick--major" : ""),
					style: ((e = {})[i] = t.position, e)
				}, void 0 !== t.label ? [c.el(".dhx_slider__tick-label", t.label)] : null)
			}))
		}, v.prototype._isNullable = function (t) {
			return this._isInverse() ? 100 === t : 0 === t
		}, v.prototype._setTooltip = function (t) {
			t.target.classList.contains("dhx_slider__thumb--extra") ? this._activeTooltip = "extraTooltip" : t.target.classList.contains("dhx_slider__thumb") ? this._activeTooltip = "tooltip" : this._activeTooltip = "none"
		}, v);

		function v(t, e) {
			var i = _.call(this, t, r.extend({
				mode: "horizontal",
				min: 0,
				max: 100,
				step: 1,
				tooltip: !0
			}, e)) || this;
			i._disabled = !1, i.config.helpMessage = i.config.helpMessage || i.config.help, void 0 !== i.config.thumbLabel && (i.config.tooltip = i.config.thumbLabel), i.config.labelInline && (i.config.labelPosition = "left"), i.events = new s.EventSystem(i), i._axis = "horizontal" === i.config.mode ? "clientX" : "clientY", i._initStartPosition(), i._keyManager = new a.KeyManager(function () {
				var t;
				return document.activeElement === (null === (t = i.getRootView().refs[i._isExtraActive ? "extraRunner" : "runner"]) || void 0 === t ? void 0 : t.el)
			}), i._initHotkeys();
			e = c.create({
				render: function () {
					return i._draw()
				},
				hooks: {
					didMount: function () {
						return i._calcSliderPosition()
					},
					didRedraw: function () {
						return i._calcSliderPosition()
					}
				}
			});
			return i._initHandlers(), i.mount(t, e), i
		}
		e.Slider = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			s = this && this.__assign || function () {
				return (s = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, a = i(1),
			l = i(0),
			c = i(3),
			u = i(2),
			d = i(4),
			h = i(71),
			o = (r = d.View, o(f, r), f.prototype.show = function (t, e, i) {
				var n = this;
				void 0 === e && (e = {}), this.events.fire(h.PopupEvents.beforeShow, [t]) && (t = u.toNode(t), this._isActive ? this._setPopupSize(t, e) : (i && this.attach(i), this._popup.style.left = "0", this._popup.style.top = "0", l.awaitRedraw().then(function () {
					n._setPopupSize(t, e), n._popup.style.position = "fixed", document.body.appendChild(n._popup), n._isActive = !0
				}).then(function () {
					n._popup.style.position = "absolute", n.events.fire(h.PopupEvents.afterShow, [t]), n._outerClickDestructor = n._detectOuterClick(t)
				})))
			}, f.prototype.hide = function () {
				this._hide(!1, null)
			}, f.prototype.isVisible = function () {
				return this._isActive
			}, f.prototype.attach = function (t, e) {
				return this._html = null, "object" == typeof t ? this._ui = t : "string" == typeof t ? this._ui = new window.dhx[t](null, e) : "function" == typeof t && (t.prototype instanceof d.View ? this._ui = new t(null, e) : this._ui = {
					getRootView: function () {
						return t(e)
					}
				}), this.paint(), this._ui
			}, f.prototype.attachHTML = function (t) {
				this._html = t, this.paint()
			}, f.prototype.getWidget = function () {
				return this._ui
			}, f.prototype.getContainer = function () {
				return this.getRootView().refs.content.el
			}, f.prototype.toVDOM = function () {
				var t;
				return this._html ? t = l.el(".dhx_popup__inner-html-content", {
					".innerHTML": this._html
				}) : (t = this._ui ? this._ui.getRootView() : null) && t.render && (t = l.inject(t)), l.el("div", {
					class: "dhx_popup-content",
					tabindex: 0,
					onclick: this._clickEvent,
					_key: this._uid,
					_ref: "content"
				}, [t])
			}, f.prototype.destructor = function () {
				this.hide(), this._outerClickDestructor && this._outerClickDestructor(), this._popup = null
			}, f.prototype._setPopupSize = function (t, e, i) {
				var n = this;
				void 0 === i && (i = 3);
				var o = this._popup.getBoundingClientRect(),
					r = o.width,
					o = o.height;
				if (this._timeout && (clearTimeout(this._timeout), this._timeout = null), !i || 0 !== r && 0 !== o) {
					r = u.fitPosition(t, s(s({
						centering: !0,
						mode: "bottom"
					}, e), {
						width: r,
						height: o
					})), o = r.left, r = r.top;
					if (this._popup.style.left = o, this._popup.style.top = r, e.indent && 0 !== e.indent) switch (e.mode) {
						case "top":
							this._popup.style.top = parseInt(this._popup.style.top.slice(0, -2), null) - parseInt(e.indent.toString(), null) + "px";
							break;
						case "bottom":
							this._popup.style.top = parseInt(this._popup.style.top.slice(0, -2), null) + parseInt(e.indent.toString(), null) + "px";
							break;
						case "left":
							this._popup.style.left = parseInt(this._popup.style.left.slice(0, -2), null) - parseInt(e.indent.toString(), null) + "px";
							break;
						case "right":
							this._popup.style.left = parseInt(this._popup.style.left.slice(0, -2), null) + parseInt(e.indent.toString(), null) + "px";
							break;
						default:
							this._popup.style.top = parseInt(this._popup.style.top.slice(0, -2), null) + parseInt(e.indent.toString(), null) + "px"
					}
				} else this._timeout = setTimeout(function () {
					n._isActive && (n._setPopupSize(t, e, i - 1), n._timeout = null)
				})
			}, f.prototype._detectOuterClick = function (i) {
				var n = this,
					o = function (t) {
						for (var e = t.target; e;) {
							if (e === i || e === n._popup) return;
							e = e.parentNode
						}
						n._hide(!0, t) && document.removeEventListener("mousedown", o)
					};
				return document.addEventListener("mousedown", o),
					function () {
						return document.removeEventListener("mousedown", o)
					}
			}, f.prototype._hide = function (t, e) {
				if (this._isActive) return !!this.events.fire(h.PopupEvents.beforeHide, [t, e]) && (document.body.removeChild(this._popup), this._isActive = !1, this._outerClickDestructor && (this._outerClickDestructor(), this._outerClickDestructor = null), this.events.fire(h.PopupEvents.afterHide, [e]), !0)
			}, f);

		function f(t) {
			void 0 === t && (t = {});
			var e = r.call(this, null, a.extend({}, t)) || this,
				i = e._popup = document.createElement("div");
			return i.className = "dhx_widget dhx_popup" + (e.config.css ? " " + e.config.css : ""), i.style.position = "absolute", i.setAttribute("role", "dialog"), i.setAttribute("aria-modal", "true"), i.setAttribute("aria-live", "polite"), e.mount(i, l.create({
				render: function () {
					return e.toVDOM()
				}
			})), e._clickEvent = function (t) {
				return e.events.fire(h.PopupEvents.click, [t])
			}, e.events = t.events || new c.EventSystem(e), e._isActive = !1, e
		}
		e.Popup = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.isTimeCheck = function (t) {
			return /(^12:[0-5][0-9]?AM$)/i.test(t)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.linkButtonClasses = ".dhx_button.dhx_button--view_link.dhx_button--icon.dhx_button--size_medium.dhx_button--color_secondary"
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			o = i(0),
			r = i(9),
			i = (s.prototype.endEdit = function () {
				var t = this._checked;
				this._config.events.fire(r.GridEvents.beforeEditEnd, [t, this._cell.row, this._cell.col]) ? (this._cell.row = this._config.datacollection.getItem(this._cell.row.id), this._config.events.fire(r.GridEvents.afterEditEnd, [t, this._cell.row, this._cell.col])) : this._input.checked = !t
			}, s.prototype.toHTML = function () {
				void 0 === this._checked && (this._checked = this._cell.row[this._cell.col.id]);
				var t = n.uid();
				return o.el("div.dhx_checkbox.dhx_cell-editor__checkbox", [o.el("label", {
					style: {
						display: "none"
					},
					for: t
				}, this._checked || "false"), o.el("input.dhx_checkbox__input", {
					type: "checkbox",
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_key: "cell_editor",
					dhx_id: "cell_editor",
					checked: this._checked,
					id: t,
					style: {
						userSelect: "none"
					}
				}), o.el("span.dhx_checkbox__visual-input")])
			}, s.prototype._initHandlers = function () {
				var e = this;
				this._handlers = {
					onClick: function () {
						var t = !e._input.checked;
						e._config.events.fire(r.GridEvents.beforeEditStart, [e._cell.row, e._cell.col, "checkbox"]) ? (e._checked = t, e._config.events.fire(r.GridEvents.afterEditStart, [e._cell.row, e._cell.col, "checkbox"]), e.endEdit()) : e._input.checked = !t
					},
					didInsert: function (t) {
						e._checkbox = t.el.parentNode.lastChild, e._input = t.el.parentNode.querySelector("input"), t.el.parentNode.addEventListener("click", e._handlers.onClick)
					}
				}
			}, s);

		function s(t, e, i) {
			this._config = i, this._cell = {
				row: t,
				col: e
			}, this._initHandlers()
		}
		e.CheckboxEditor = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			o = i(9),
			r = i(24),
			s = i(18),
			i = (a.prototype.endEdit = function (t) {
				var e;
				t || (e = this._input.getValue(), e = "multiselect" === this._cell.col.editorType ? e.split(",").join(", ") : e), this._config.events.fire(o.GridEvents.beforeEditEnd, [e, this._cell.row, this._cell.col]) ? (this._input.popup.hide(), document.removeEventListener("mousedown", this._handlers.onOuterClick), this._cell.row = this._config.datacollection.getItem(this._cell.row.id), s.focusManager.setFocusId(this._config.gridId), this._config.events.fire(o.GridEvents.afterEditEnd, [e, this._cell.row, this._cell.col])) : this._input.focus()
			}, a.prototype.toHTML = function () {
				var e = this,
					t = this._cell.col.options.map(function (t) {
						return "string" == typeof t ? {
							id: "" + t,
							value: t
						} : t
					}) || [];
				return this._input || (this._input = new r.Combobox(null, {
					cellHeight: 37,
					css: "dhx_cell-editor__combobox",
					multiselection: "multiselect" === this._cell.col.editorType
				}), this._input.data.parse(t), t = this._cell.row[this._cell.col.id], t = "multiselect" === this._cell.col.editorType ? (t || "").split(", ") : t, this._input.setValue(t), this._input.events.on("keydown", this._handlers.onkeydown)), document.addEventListener("mousedown", this._handlers.onOuterClick), this._config.$editable.editor = this, n.awaitRedraw().then(function () {
					var t = e._input.getRootView().refs.holder.el;
					e._input.popup.getContainer().style.width = t.offsetWidth + "px", e._input.popup.show(t)
				}), s.focusManager.setFocusId(this._input._uid), n.inject(this._input.getRootView())
			}, a.prototype._initHandlers = function () {
				var i = this;
				this._handlers = {
					onOuterClick: function (t) {
						var e;
						t.target instanceof Node && (e = i._input && i._input.getRootNode() && i._input.getRootNode().contains(t.target), t = i._input.popup && i._input.popup.getRootNode() && i._input.popup.getRootNode().contains(t.target), e || t || i.endEdit())
					},
					onkeydown: function (t) {
						"Escape" !== t.key && "Tab" !== t.key || i.endEdit()
					}
				}
			}, a);

		function a(t, e, i) {
			this._config = i, this._cell = {
				row: t,
				col: e
			}, this._initHandlers()
		}
		e.ComboboxEditor = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.KEY_CODES = {
			BACKSPACE: 8,
			ENTER: 13,
			ESC: 27,
			DOWN_ARROW: 40,
			UP_ARROW: 38,
			LEFT_ARROW: 37,
			RIGHT_ARROW: 39
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(169);
		e.getEditor = function (t, e) {
			return new n.InputEditor(t, e)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			o = i(46),
			i = (r.prototype.endEdit = function () {
				var t;
				this._input && (t = this._input.value, this._list.events.fire(o.ListEvents.beforeEditEnd, [t, this._item.id]) ? (this._input.removeEventListener("blur", this._handlers.onBlur), this._input.removeEventListener("change", this._handlers.onChange), this._handlers = {}, this._mode = !1, this._list.events.fire(o.ListEvents.afterEditEnd, [t, this._item.id])) : this._input.focus())
			}, r.prototype.toHTML = function () {
				this._mode = !0;
				var t = this._config.itemHeight;
				return n.el(".dhx_input__wrapper", {
					role: "presentation"
				}, [n.el("div.dhx_input__container", {
					role: "presentation"
				}, [n.el("input.dhx_input", {
					class: this._item.css ? " " + this._item.css : "",
					style: {
						height: t,
						width: "100%",
						padding: "8px 12px"
					},
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_key: this._item.id,
					dhx_id: this._item.id
				})])])
			}, r.prototype._initHandlers = function () {
				var e = this;
				this._handlers = {
					onBlur: function () {
						e.endEdit()
					},
					onChange: function () {
						e.endEdit()
					},
					didInsert: function (t) {
						t = t.el;
						(e._input = t).focus(), t.value = e._item.value, t.setSelectionRange(0, t.value.length), t.addEventListener("change", e._handlers.onChange), t.addEventListener("blur", e._handlers.onBlur)
					}
				}
			}, r);

		function r(t, e) {
			var i = this;
			this._list = e, this._config = e.config, this._item = t, this._list.events.on(o.ListEvents.focusChange, function (t, e) {
				i._mode && e !== i._item.id && i.endEdit()
			}), this._initHandlers()
		}
		e.InputEditor = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(0),
			c = i(17),
			i = i(77),
			o = (a = i.List, o(u, a), u.prototype.destructor = function () {
				a.prototype.destructor.call(this), this.scrollView = null
			}, u.prototype.showItem = function (t) {
				var e, i, n, o = this.getRootView();
				o && o.node && o.node.el && void 0 !== t && ((e = this.getRootNode()) && (i = this.config.virtual, n = this.data.getIndex(t), o = Math.floor(n / e.children.length) || 0, t = e.children[n - e.children.length * o], (i || t) && (o = i ? parseInt(this.config.itemHeight) : t.clientHeight, (t = i ? n * o : t.offsetTop) >= e.scrollTop + e.clientHeight - o ? e.scrollTo(0, t - e.clientHeight + o) : t < e.scrollTop && e.scrollTo(0, t))))
			}, u.prototype._renderList = function () {
				var i = this,
					t = this._getRange(),
					e = this.data.getRawData(t[0], t[1]).map(function (t, e) {
						return i._renderItem(t, e)
					});
				this.config.virtual && (e = s([l.el(".div", {
					style: {
						height: t[2] + "px"
					}
				})], e, [l.el(".div", {
					style: {
						height: t[3] + "px"
					}
				})]));
				var n = this.scrollView && this.scrollView.config.enable,
					t = (this.config.css || "") + (this.config.multiselection && this.selection.getItem() ? " dhx_no-select--pointer" : "") + (n ? " dhx_list--scroll-view" : "");
				return l.el("ul.dhx_widget.dhx_list", r(r({
					style: {
						"max-height": this.config.height,
						position: "relative"
					},
					class: t,
					dhx_widget_id: this._uid
				}, this._handlers), this._getListAriaAttrs(this.config, this.data.getLength())), n ? [].concat(this.scrollView.render(e)) : e)
			}, u);

		function u(t, e) {
			var i = a.call(this, t, e) || this;
			return i.scrollView = new c.ScrollView(function () {
				return i.getRootView()
			}), i.paint(), i
		}
		e.ProList = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = (o.prototype.startNewListen = function (t) {
			this._isActive = !0, this._sequence = "", this._currentAction = t
		}, o.prototype.endListen = function () {
			this._currentAction = null, this.reset(), this._isActive = !1
		}, o.prototype.reset = function () {
			this._sequence = ""
		}, o.prototype._change = function () {
			this._currentAction(this._sequence), this._addClearTimeout()
		}, o.prototype._addClearTimeout = function () {
			var t = this;
			this._clearTimeout && clearTimeout(this._clearTimeout), this._clearTimeout = setTimeout(function () {
				t.reset(), t._clearTimeout = null
			}, 2e3)
		}, o);

		function o() {
			var e = this;
			this._sequence = "", document.addEventListener("keydown", function (t) {
				e._isActive && ("Backspace" === (t = t.key) && 0 < e._sequence.length && (e._sequence = e._sequence.slice(0, e._sequence.length - 1), e._change()), t.length < 2 && (e._sequence += t, e._change()))
			})
		}
		e.KeyListener = n
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(76),
			a = i(28),
			l = i(11),
			c = i(79),
			o = (r = s.Combobox, o(u, r), u.prototype._createLayout = function () {
				var t = this.list = new a.ProList(null, {
					template: this.config.template,
					virtual: this.config.virtual,
					keyNavigation: !1,
					multiselection: this.config.multiselection,
					itemHeight: this.config.itemHeight,
					height: this.config.listHeight,
					data: this.data
				}),
					e = this._layout = new l.ProLayout(this.popup.getContainer(), {
						css: "dhx_combobox-options dhx_combobox__options",
						rows: [{
							id: "select-unselect-all",
							hidden: !this.config.multiselection || !this.config.selectAllButton
						}, {
							id: "list",
							height: "content"
						}, {
							id: "not-found",
							hidden: !0
						}],
						on: {
							click: {
								".dhx_combobox__action-select-all": this._handlers.selectAll
							}
						}
					});
				e.getCell("list").attach(t), this.config.multiselection && this.config.selectAllButton && e.getCell("select-unselect-all").attach(c.selectAllView)
			}, u);

		function u(t, e) {
			return r.call(this, t, e) || this
		}
		e.ProCombobox = o
	}, function (t, e, i) {
		"use strict";
		var r = this && this.__assign || function () {
			return (r = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			s = i(9),
			o = i(1),
			a = i(22),
			i = (l.prototype.endEdit = function (t) {
				var e;
				t || (e = this._editor.value), this._config.events.fire(s.GridEvents.beforeEditEnd, [e, this._cell.row, this._cell.col]) ? (this._editor.removeEventListener("blur", this._handlers.onBlur), this._editor.removeEventListener("change", this._handlers.onChange), this._editor.removeEventListener("input", this._handlers.onInput), "string" !== this._cell.col.type && o.isNumeric(e) && (e = parseFloat(e)), this._cell.row = this._config.datacollection.getItem(this._cell.row.id), this._config.events.fire(s.GridEvents.afterEditEnd, [e, this._cell.row, this._cell.col])) : this._editor.focus()
			}, l.prototype.toHTML = function () {
				var t = this._cell.row[this._cell.col.id];
				this._editor && (t = this._editor.value);
				var e = void 0 !== (this._config.$editable.editor = this)._cell.row.height || this._cell.col.htmlEnable ? "dhx_cell-editor dhx_cell-editor__textarea_constant-height" : "dhx_cell-editor dhx_cell-editor__textarea";
				return n.el("textarea", {
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_ref: "textarea",
					_key: "cell_editor",
					dhx_id: "cell_editor",
					value: t,
					class: e,
					style: {
						width: this._width
					}
				})
			}, l.prototype._initHandlers = function () {
				var o = this;
				this._handlers = {
					onBlur: function () {
						o.endEdit()
					},
					onChange: function () {
						o.endEdit()
					},
					onInput: function (t) {
						if (void 0 === o._cell.row.height && !o._cell.col.htmlEnable) {
							var e = o._getCurrentHeight(o._editor.value, {
								width: o._cell.col.$width - 2,
								maxHeight: o._config.rowHeight
							});
							o._cell.row[o._cell.col.id] = o._editor.value;
							var i = a.getCalculatedRowHeight(a.getMaxRowHeight(o._cell.row, o._config.columns)),
								n = a.getCalculatedRowHeight(a.getMaxRowHeight(((n = {})[o._cell.col.id] = o._cell.row[o._cell.col.id], n), o._config.columns));
							if (o._minHeight = i === n ? o._config.rowHeight : i, e >= o._minHeight && e !== o._prevHeight) {
								if (!o._config.events.fire(s.GridEvents.beforeRowResize, [o._cell.row, t, e])) return;
								o._config.events.fire(s.GridEvents.afterRowResize, [o._cell.row, t, e])
							}
							o._prevHeight = e
						}
					},
					didInsert: function (t) {
						o._editor = t.el, o._editor.focus(), o._editor.setSelectionRange(0, o._editor.value.length), o._editor.addEventListener("change", o._handlers.onChange), o._editor.addEventListener("blur", o._handlers.onBlur), o._editor.addEventListener("input", o._handlers.onInput)
					}
				}
			}, l.prototype._getCurrentHeight = function (t, e) {
				e = r({
					width: 100,
					maxHeight: 40,
					lineHeight: 20
				}, e);
				var i = document.createElement("textarea");
				i.className = "dhx_cell-editor dhx_cell-editor__textarea", i.value = t, i.style.width = e.width + "px", i.style.lineHeight = e.lineHeight + "px", i.style.maxHeight = e.maxHeight + "px", i.style.boxSizing = "border-box", document.body.appendChild(i);
				var n = this._getElementHeight(i),
					o = i.value.split("\n").length,
					t = Math.round(n / e.lineHeight),
					n = n < e.maxHeight ? e.maxHeight : n;
				return document.body.removeChild(i), 1 === o && o === t ? e.maxHeight : n
			}, l.prototype._getElementHeight = function (t) {
				return t.scrollHeight
			}, l);

		function l(t, e, i) {
			this._config = i, this._cell = {
				row: t,
				col: e
			}, this._width = this._cell.col.$width;
			e = 0;
			this._config.firstColId === this._cell.col.id && this._cell.row.hasOwnProperty("$level") && (e = a.getTreeCellWidthOffset(this._cell.row)), this._width -= e - 4, this._initHandlers()
		}
		e.TextAreaEditor = i
	}, function (t, e, i) {
		"use strict";
		var f = this && this.__assign || function () {
			return (f = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		},
			p = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var _ = i(0),
			v = i(2),
			g = i(42),
			m = i(81),
			y = i(16);
		e.getFixedColsHeader = function (t, e) {
			if ("number" == typeof t.leftSplit) {
				for (var i = 0, n = 0; n < t.leftSplit; n++) t.columns[n].hidden && i++;
				if (i !== t.leftSplit) {
					for (var o = t.columns.slice(0, t.leftSplit - i), r = 0, s = 0, a = o; s < a.length; s++) r += a[s].$width;
					o = 0 <= t.leftSplit && m.getFixedRows(f(f({}, t), {
						currentColumns: o,
						$positions: f(f({}, t.$positions), {
							xStart: 0,
							xEnd: t.leftSplit
						}),
						scroll: {
							top: 0,
							left: 0
						},
						columns: o
					}), f(f({}, e), {
						name: "header",
						position: "top",
						shifts: {
							x: 0,
							y: 0
						}
					})), e = f(f({}, e), {
						name: "header",
						position: "top"
					});
					return o && _.el(".dhx_" + e.name + "-fixed-cols", f({
						style: {
							position: "absolute",
							top: 0,
							left: 0,
							maxWidth: r,
							overflow: "hidden"
						}
					}, {
						role: "rowgroup",
						"aria-rowcount": o.length
					}), o.body)
				}
			}
		}, e.getFixedCols = function (t, e) {
			if ("number" == typeof t.leftSplit) {
				for (var i = 0, n = 0; n < t.leftSplit; n++) t.columns[n].hidden && i++;
				if (i !== t.leftSplit) {
					var o = t.$totalWidth <= e.wrapper.width ? 0 : v.getScrollbarWidth(),
						r = t.$totalHeight + t.headerHeight + t.footerHeight,
						s = r > e.gridBodyHeight ? r - o : r < e.gridBodyHeight - o ? r : e.gridBodyHeight,
						a = t.columns.slice(0, t.leftSplit - i);
					t.fixedColumnsWidth = y.getTotalWidth(a);
					var l = g.getCells(f(f({}, t), {
						columns: a,
						$positions: f(f({}, t.$positions), {
							xStart: 0,
							xEnd: t.leftSplit
						})
					})),
						c = e.sticky,
						u = f(f({}, e), {
							name: "footer",
							position: "bottom"
						}),
						d = 0 <= t.leftSplit && m.getRows(f(f({}, t), {
							currentColumns: a,
							$positions: f(f({}, t.$positions), {
								xStart: 0,
								xEnd: t.leftSplit
							})
						}), f(f({}, e), {
							name: "footer",
							position: "bottom"
						})),
						h = 0;
					d && d.forEach(function (t) {
						return h += t.attrs.style.height
					});
					r = function (t) {
						return {
							role: "rowgroup",
							"aria-rowcount": t
						}
					}, a = c ? d && _.el(".dhx_" + u.name + "-fixed-cols", f({
						style: {
							position: "absolute",
							top: s < e.gridBodyHeight ? s - h : null,
							left: 0,
							bottom: s >= e.gridBodyHeight ? 0 + (c ? o : 0) + "px" : null
						}
					}, r(d.length)), d) : null, u = t.$positions, d = g.getSpans(t, !0);
					return [_.el(".dhx_grid-fixed-cols-wrap", f({
						style: {
							height: s >= e.gridBodyHeight ? (c ? e.gridBodyHeight : e.gridBodyHeight + t.headerHeight) - o : s,
							paddingTop: t.headerHeight,
							overflow: "hidden",
							width: t.fixedColumnsWidth
						}
					}, {
						role: "presentation",
						"aria-label": "Fixed column"
					}), [_.el(".dhx_grid-fixed-cols", f(f({
						style: {
							top: -t.scroll.top + t.headerHeight - 1 + "px",
							paddingTop: e.shifts.y,
							height: t.$totalHeight,
							position: "absolute"
						},
						_flags: _.KEYED_LIST
					}, g.getHandlers(u.yStart, u.xStart, t)), r(t.data.length)), p([d && _.el(".dhx_span-spans", {
						role: "presentation"
					}, [d])], l)), _.el(".dhx_frozen-cols-border", {
						role: "presentation"
					})]), t.$footer ? a : null]
				}
			}
		}
	}, function (t, e, i) {
		"use strict";
		var o = this && this.__spreadArrays || function () {
			for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
			for (var n = Array(t), o = 0, e = 0; e < i; e++)
				for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
			return n
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(24),
			c = i(6),
			u = i(9),
			n = i(22),
			d = i(2),
			r = i(1);

		function h(e, i, n, o, r) {
			function t() {
				//var t = (d.isIE() || d.isSafari() ? r.target : r.path ? r.path[0] : r.explicitOriginalTarget).value;
				var t = d.isIE() || d.isSafari() ? r.target.value : r.path ? r.path[0].value : r.target.value;
				o.value[i] = t, e.fire(u.GridEvents.filterChange, [t, i, n])
			}
			"selectFilter" !== n ? (s && clearTimeout(s), s = setTimeout(t, 250)) : t()
		}

		function f(t, n, e, i) {
			if (t && n && e) {
				var o = t.id,
					t = i ? i(o, n.data) : n.data.reduce(function (t, e) {
						return void 0 === e[o] || "" === e[o] || isNaN(e[o]) || t.push(parseFloat(e[o])), t
					}, []),
					i = t;
				return "tree" === n.type && (i = n.data.reduce(function (t, e) {
					var i;
					return 0 === e.$level && (void 0 === e[o] || "" === e[o] || isNaN(e[o]) ? (i = 0, n.datacollection.eachChild(e.id, function (t) {
						n.datacollection.haveItems(t.id) || (i += parseFloat(t[o]))
					}), t.push(i)) : t.push(parseFloat(e[o]) || 0)), t
				}, [])), e(t, i)
			}
		}
		e.getContent = function () {
			var i = this;
			return {
				inputFilter: {
					element: {},
					toHtml: function (t, e) {
						var i = r.uid(),
							n = t.id.toString();
						return this.element[n] = a.el("div.dhx_grid-filter__label.dxi.dxi-magnify", {
							"aria-label": "Type to search",
							_ref: t.id + "_filter"
						}, [a.el("label", {
							style: {
								display: "none"
							},
							"aria-label": "Type to search",
							for: i
						}, "Type to search"), a.el("input", {
							type: "text",
							class: "dhx_input dhx_grid-filter",
							oninput: [h, e.events, t.id, "inputFilter", this],
							_key: t.id,
							id: i,
							value: this.value[t.id] || ""
						})]), this.element[n]
					},
					match: function (t, e) {
						for (var i = "", n = 0; n < e.length; n++) {
							var o = e.charCodeAt(n);
							i += 32 < o && o < 48 || 63 === o || 90 < o && o < 95 || 124 === o ? "\\" + e[n] : e[n]
						}
						return new RegExp("" + i, "i").test(t)
					},
					value: {}
				},
				selectFilter: {
					element: {},
					toHtml: function (e, t) {
						var i = this,
							n = e.id.toString();
						return this.element[n] = a.el("label.dhx_grid-filter__label.dxi.dxi-menu-down", {
							_ref: e.id + "_filter"
						}, [a.el("select.dxi.dxi-menu-down", {
							type: "text",
							class: "dhx_input dhx_grid-filter dhx_grid-filter--select",
							onchange: [h, t.events, e.id, "selectFilter", this],
							_key: e.id
						}, o([a.el("option", {
							value: ""
						}, "")], e.$uniqueData.map(function (t) {
							return "" !== (t = null != t ? t : "") && a.el("option", {
								value: t,
								selected: i.value[e.id] === t.toString()
							}, t)
						})))]), this.element[n]
					},
					match: function (t, e) {
						return !e || t && t.toString() == e
					},
					value: {}
				},
				comboFilter: {
					element: {},
					toHtml: function (i, n) {
						var t, o, r = i.id.toString();
						return !this.element[r] && n.events ? (t = i.header.filter(function (t) {
							return void 0 !== t.filterConfig
						})[0], (o = t && t.filterConfig ? new l.Combobox(null, Object.assign({}, t.filterConfig)) : new l.Combobox(null, {})).data.parse(i.$uniqueData.map(function (t) {
							return {
								value: t
							}
						})), n.events.on(c.DataEvents.load, function () {
							o.data.parse(i.$uniqueData.map(function (t) {
								return {
									value: t
								}
							}))
						}), (this.element[r] = o).events.on("change", function (t) {
							var e;
							t && (e = void 0, (Array.isArray(t) ? t.find(function (t) {
								return o.data.getItem(t)
							}) : o.data.getItem(t)) ? (e = o.config.multiselection ? o.list.selection.getItem().map(function (t) {
								if (t && o.data.getItem(t.id)) return t.value
							}) : o.list.selection.getItem().value, n.events.fire(u.GridEvents.filterChange, [e, r, "comboFilter"])) : n.events.fire(u.GridEvents.filterChange, ["", r, "comboFilter"]))
						}), n.events.on(c.DataEvents.change, function (t, e) {
							"add" !== e && "update" !== e && "remove" !== e || (o.data.parse(i.$uniqueData.map(function (t) {
								return {
									value: t
								}
							})), o.events.fire(l.ComboboxEvents.change, [o.list.selection.getItem()]))
						}), n.events.on(c.DataEvents.removeAll, function () {
							o.data.parse(i.$uniqueData.map(function (t) {
								return {
									value: t
								}
							})), o.events.fire(l.ComboboxEvents.change, [o.list.selection.getItem()])
						}), o.popup.events.on("afterHide", function () {
							o.list.selection.getItem() && (!o.config.multiselection || o.list.selection.getItem().length) || (o.clear(), n.events.fire(u.GridEvents.filterChange, ["", r, "comboFilter"]))
						})) : o = this.element[i.id] || new l.Combobox(null, {}), a.inject(o.getRootView())
					},
					match: function (t, i, e, n) {
						if (void 0 === i && (i = ""), void 0 === n && (n = !1), Array.isArray(i)) {
							for (var o = void 0, r = 0; r < i.length; r++)
								if ("break" === function (e) {
									if (o = n ? !!t.split(", ").find(function (t) {
										return t === i[e]
									}) : i[e] === t) return "break"
								}(r)) break;
							return !i || !i.length || o
						}
						return "" === i || t === i
					},
					destroy: function () {
						if (i.content && i.content.comboFilter.element) {
							var t, e = i.content.comboFilter.element;
							for (t in e) e[t].destructor(), delete e[t]
						}
					},
					value: {}
				},
				sum: {
					calculate: function (t, e) {
						return e.reduce(function (t, e) {
							return t + (parseFloat(e) || 0)
						}, 0)
					},
					toHtml: function (t, e) {
						e = f(t, e, this.calculate);
						return t.format || "percent" === t.type ? n.toFormat(e, t.type, t.format) : e ? e.toFixed(3) : null
					}
				},
				avg: {
					calculate: function (t, e) {
						return t.length ? e.reduce(function (t, e) {
							return t + e
						}, 0) / t.length : null
					},
					toHtml: function (t, e) {
						e = f(t, e, this.calculate);
						return t.format || "percent" === t.type ? n.toFormat(e, t.type, t.format) : e ? e.toFixed(3) : null
					}
				},
				min: {
					calculate: function (t) {
						return t.length ? Math.min.apply(Math, t) : null
					},
					toHtml: function (t, e) {
						e = f(t, e, this.calculate);
						return t.format || "percent" === t.type ? n.toFormat(e, t.type, t.format) : e ? e.toFixed(3) : null
					}
				},
				max: {
					calculate: function (t) {
						return t.length ? Math.max.apply(Math, t) : null
					},
					toHtml: function (t, e) {
						e = f(t, e, this.calculate);
						return t.format || "percent" === t.type ? n.toFormat(e, t.type, t.format) : e ? e.toFixed(3) : null
					}
				},
				count: {
					calculate: function (t, e) {
						return e.reduce(function (t, e) {
							return t + e
						}, 0)
					},
					validate: function (i, t) {
						return t.reduce(function (t, e) {
							return void 0 !== e[i] && "" !== e[i] && (isNaN(e) ? t.push(1) : t.push(e)), t
						}, [])
					},
					toHtml: function (t, e) {
						return f(t, e, this.calculate, this.validate)
					}
				}
			}
		}
	}, function (t, e, i) {
		"use strict";
		var h = this && this.__spreadArrays || function () {
			for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
			for (var n = Array(t), o = 0, e = 0; e < i; e++)
				for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
			return n
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var f = i(1),
			p = i(9),
			_ = i(2);
		e.startResize = function (a, l, t, e) {
			t.targetTouches && t.preventDefault();
			var c = (t.targetTouches ? t.targetTouches[0] : t).clientX,
				u = a.config.columns.filter(function (t) {
					return !t.hidden
				}),
				d = 0;

			function i(t) {
				var e, i, n = f.findIndex(u, function (t) {
					return t.id === l
				}),
					o = (t.targetTouches ? t.targetTouches[0] : t).clientX,
					r = o - a.getRootNode().getBoundingClientRect().left,
					s = a.config.$totalHeight > a.config.height ? _.getScrollbarWidth() : 0;
				a.config.leftSplit === n + 1 && r >= a.config.width - s - 2 || (d = d || u[n].$width, e = u[n].minWidth || 40, r = u[n].maxWidth, s = o - c, o = h(u), s = d + s, r && r <= s || s <= e ? (s <= e && (i = e), r <= s && (i = r)) : i = s, o[n].$width = i, o[n].$fixed = !0, a.events.fire(p.GridEvents.resize, [u[n], t]), a.paint())
			}
			a.config.$resizing = l;
			var n = function () {
				t.targetTouches ? (document.removeEventListener("touchmove", i), document.removeEventListener("touchend", n)) : (document.removeEventListener("mousemove", i), document.removeEventListener("mouseup", n)), e()
			};
			t.targetTouches ? (document.addEventListener("touchmove", i), document.addEventListener("touchend", n)) : (document.addEventListener("mousemove", i), document.addEventListener("mouseup", n)), a.paint()
		}
	}, function (t, e, i) {
		"use strict";
		var r = this && this.__assign || function () {
			return (r = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s = i(9),
			u = i(2),
			a = i(16);

		function l(t, e, i, n, o, r, s) {
			var a, l, c;
			void 0 === o && (o = !1), void 0 === r && (r = !1), void 0 === s && (s = !1), e.config.$editable || !e.config.selection || u.locateNodeByClassName(t, "dhx_grid-header-cell") || (t && t.preventDefault(), a = e.selection.getCell(), t = e.config.columns.filter(function (t) {
				return !t.hidden
			}), a && ("vertical" === i ? o ? (l = 1 === n ? e.data.getItem(e.data.getId(e.data.getLength() - 1)) : e.data.getItem(e.data.getId(0)), e.selection.setCell(l.id, a.column.id, r, s), e.scrollTo(l.id.toString(), a.column.id.toString())) : 0 <= (c = e.data.getIndex(a.row.id.toString())) + n && c + n < e.data.getLength() && (l = e.data.getItem(e.data.getId(c + n)), e.selection.setCell(l.id, a.column.id, r, s), e.scrollTo(l.id.toString(), a.column.id.toString())) : o ? (l = 1 === n ? t[t.length - 1] : t[0], e.selection.setCell(a.row.id, l.id, r, s), e.scrollTo(a.row.id.toString(), l.id.toString())) : 0 <= (c = t.indexOf(a.column)) + n && c + n < t.length && (e.selection.setCell(a.row.id, t[c + n].id, r, s), e.scrollTo(a.row.id.toString(), t[c + n].id.toString()))))
		}
		e.selectionMove = l, e.getKeysHandlers = function (o) {
			var t, n = "cell" === o.config.selection || "complex" === o.config.selection || !0 === o.config.selection,
				e = {};
			if (o.getRootView()) {
				var i = null === (t = null === (t = null === (t = o.getRootView()) || void 0 === t ? void 0 : t.refs) || void 0 === t ? void 0 : t.grid_body) || void 0 === t ? void 0 : t.el;
				if (!i) return;
				e = {
					pageUp: function (t) {
						t.preventDefault(), i.scrollTop -= i.clientHeight
					},
					pageDown: function (t) {
						t.preventDefault(), i.scrollTop += i.clientHeight
					},
					home: function (t) {
						t.preventDefault(), i.scrollTop = 0
					},
					end: function (t) {
						t.preventDefault(), i.scrollTop += i.scrollHeight
					}
				}
			}
			return r({
				enter: function (t) {
					var e, i = u.locateNodeByClassName(t, "dhx_grid-header-cell");
					i && (e = i.getAttribute("dhx_id"), i = t.target.getAttribute("dhx_resized"), !e || (t = o.getColumn(e)) && a.isSortable(o.config, t) && !i && o.events.fire(s.GridEvents.afterSort, [e])), n ? (e = o.selection.getCell()) && (!1 !== e.column.editable && o.config.editable || e.column.editable) && (o.config.$editable ? o.editEnd() : "boolean" !== e.column.type ? o.editCell(e.row.id, e.column.id, e.column.editorType) : o.events.fire(s.GridEvents.afterEditEnd, [!e.row[e.column.id], e.row, e.column])) : o.config.$editable && o.editEnd()
				},
				space: function (t) {
					var e, i = o.selection.getCell();
					n && (null !== (e = null == i ? void 0 : i.column.editable) && void 0 !== e ? e : o.config.editable) && !o.config.$editable && i && "boolean" === i.column.type && (t.preventDefault(), o.events.fire(s.GridEvents.afterEditEnd, [!i.row[i.column.id], i.row, i.column]))
				},
				escape: function () {
					o.config.$editable && o.editEnd(!0)
				},
				tab: function (t) {
					var e, i, n;
					o.config.selection && !u.locateNodeByClassName(t, "dhx_grid-header-cell") && (o.config.$editable && o.editEnd(), n = o.selection.getCell(), e = o.config.columns.filter(function (t) {
						return !t.hidden
					}), n && (0 <= (i = e.indexOf(n.column) + 1) && i < e.length ? (t && t.preventDefault(), o.selection.setCell(n.row.id, e[i].id), o.scrollTo(n.row.id.toString(), e[i].id.toString())) : 0 <= i && ((n = o.data.getIndex(n.row.id.toString()) + 1) < o.data.getLength() && (t && t.preventDefault(), o.selection.setCell(o.data.getId(n), e[0].id), o.scrollTo(o.data.getId(n).toString(), e[0].id.toString())))))
				},
				"shift+tab": function (t) {
					var e, i, n;
					o.config.selection && !u.locateNodeByClassName(t, "dhx_grid-header-cell") && (o.config.$editable && o.editEnd(), n = o.selection.getCell(), e = o.config.columns.filter(function (t) {
						return !t.hidden
					}), n && (0 <= (i = e.indexOf(n.column) - 1) && i < e.length ? (t && t.preventDefault(), o.selection.setCell(n.row.id, e[i].id), o.scrollTo(n.row.id.toString(), e[i].id.toString())) : i < o.data.getLength() && (0 <= (n = o.data.getIndex(n.row.id.toString()) - 1) && (t && t.preventDefault(), o.selection.setCell(o.data.getId(n), e[e.length - 1].id), o.scrollTo(o.data.getId(n).toString(), e[e.length - 1].id.toString())))))
				},
				arrowUp: function (t) {
					l(t, o, "vertical", -1)
				},
				"ctrl+arrowUp": function (t) {
					l(t, o, "vertical", -1, !0)
				},
				"shift+arrowUp": function (t) {
					o.config.multiselection && l(t, o, "vertical", -1, !1, !1, !0)
				},
				"ctrl+shift+arrowUp": function (t) {
					o.config.multiselection && l(t, o, "vertical", -1, !0, !1, !0)
				},
				arrowDown: function (t) {
					l(t, o, "vertical", 1)
				},
				"ctrl+arrowDown": function (t) {
					l(t, o, "vertical", 1, !0)
				},
				"shift+arrowDown": function (t) {
					o.config.multiselection && l(t, o, "vertical", 1, !1, !1, !0)
				},
				"ctrl+shift+arrowDown": function (t) {
					o.config.multiselection && l(t, o, "vertical", 1, !0, !1, !0)
				},
				arrowRight: function (t) {
					l(t, o, "horizontal", 1)
				},
				"ctrl+arrowRight": function (t) {
					l(t, o, "horizontal", 1, !0)
				},
				"shift+arrowRight": function (t) {
					o.config.multiselection && l(t, o, "horizontal", 1, !1, !1, !0)
				},
				"ctrl+shift+arrowRight": function (t) {
					o.config.multiselection && l(t, o, "horizontal", 1, !0, !1, !0)
				},
				arrowLeft: function (t) {
					l(t, o, "horizontal", -1)
				},
				"ctrl+arrowLeft": function (t) {
					l(t, o, "horizontal", -1, !0)
				},
				"shift+arrowLeft": function (t) {
					o.config.multiselection && l(t, o, "horizontal", -1, !1, !1, !0)
				},
				"ctrl+shift+arrowLeft": function (t) {
					o.config.multiselection && l(t, o, "horizontal", -1, !0, !1, !0)
				}
			}, e)
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(67),
			l = i(9),
			c = i(6),
			u = i(2),
			d = i(22),
			h = i(68),
			f = i(0),
			p = i(17),
			o = (s = a.Grid, o(_, s), _.prototype._createView = function () {
				var i = this;
				return f.create({
					render: function (t, e) {
						return h.proRender(t, e, i._htmlEvents, i.selection, i._uid)
					}
				}, this)
			}, _.prototype._setEventHandlers = function () {
				var r = this;
				s.prototype._setEventHandlers.call(this), this.events.on(l.GridEvents.headerCellMouseDown, function (t, e) {
					var i = u.locateNodeByClassName(e, "dhx_header-row"),
						i = i && i.getAttribute("aria-rowindex");
					null !== (i = t.header[Number(i) - 1]) && void 0 !== i && i.content || (e.targetTouches ? r._touch.timer = setTimeout(function () {
						r._dragStartColumn(e, t)
					}, r._touch.duration) : r._dragStartColumn(e, t))
				}), this._events.on(l.GridSystemEvents.headerCellTouchMove, function (t, e) {
					r._touch.start && e.preventDefault(), r._clearTouchTimer()
				}), this._events.on(l.GridSystemEvents.headerCellTouchEnd, function () {
					r._touch.start = !1, r._clearTouchTimer()
				}), this.events.on(l.GridEvents.resize, function () {
					r._parseColumns(), r._checkFilters()
				}), this.events.on(l.GridEvents.afterResizeEnd, function () {
					r.config.autoHeight && (r.config.data = r.data.map(function (t) {
						var e = d.getMaxRowHeight(t, r.config.columns);
						return t.$height = d.getCalculatedRowHeight(e, {
							rowHeight: r.config.rowHeight,
							padding: 8
						}), t
					}))
				}), this.events.on(l.GridEvents.afterRowResize, function (t, e, i) {
					var n = r.data.getItem(t.id),
						o = n.id,
						t = n.height,
						n = n.$height;
					t && t !== n && r.data.update(o, {
						height: i
					}), r.data.update(o, {
						$height: i
					}, !0), r.config.data = r.data.map(function (t) {
						return t
					}), r.paint()
				}), this.events.on(l.GridEvents.scroll, function () {
					r._lazyLoad()
				})
			}, _.prototype._prepareData = function (t) {
				var e, i = this;
				return this._adjustColumns(), (Array.isArray(t) || c.isTreeCollection(t) ? t : 0 !== (e = t.getInitialData() || []).length ? e : t.getRawData(0, t.getLength())).map(function (t) {
					var e;
					return i.config.autoHeight && void 0 === t.height ? (e = d.getMaxRowHeight(t, i.config.columns), t.$height = d.getCalculatedRowHeight(e, {
						rowHeight: i.config.rowHeight,
						padding: 8
					}) || i.config.rowHeight) : t.$height = t.height || i.config.rowHeight, t
				})
			}, _.prototype._prepareDataFromTo = function (t, e, i) {
				var n = this;
				return t.mapRange(e, i, function (t) {
					var e = d.getMaxRowHeight(t, n.config.columns);
					return t.$height = d.getCalculatedRowHeight(e, {
						rowHeight: n.config.rowHeight,
						padding: 8
					}) || n.config.rowHeight, t
				})
			}, _.prototype._lazyLoad = function () {
				var t, e, i, n, o = this,
					r = this.data.dataProxy;
				r && r.config && (t = this.data.getRawData(0, -1, null, 2), e = h.getRenderConfig(this, t, {
					width: this.config.width,
					height: this.config.height
				}), i = this.data.getIndex(null === (t = e.currentRows[0]) || void 0 === t ? void 0 : t.id.toString()), n = this.data.getIndex(null === (e = e.currentRows[e.currentRows.length - 1]) || void 0 === e ? void 0 : e.id.toString()), !this.data.isDataLoaded(i, n) && this.data.events.fire(c.DataEvents.beforeLazyLoad, []) && (r.updateUrl(null, {
					from: i,
					limit: r.config.limit
				}), this.data.load(r).then(function () {
					o.config.autoHeight && o._prepareDataFromTo(o.data, i, n)
				})))
			}, _.prototype._getColumnGhost = function (t) {
				var e = this._container || u.toNode(this._uid),
					i = e.querySelector(".dhx_header-row"),
					n = i.querySelector('.dhx_grid-header-cell[dhx_id="' + t.id + '"]'),
					n = Array.from(i.childNodes).indexOf(n) + 1,
					t = e.querySelectorAll('.dhx_grid-header-cell[dhx_id="' + t.id + '"]:not(.dhx_span-cell)'),
					n = e.querySelectorAll(".dhx_grid_data .dhx_grid-cell:nth-child(" + n + ")"),
					o = document.createElement("div");
				return t.forEach(function (t) {
					return o.appendChild(t.cloneNode(!0))
				}), n.forEach(function (t) {
					return o.appendChild(t.cloneNode(!0))
				}), o
			}, _.prototype._dragStartColumn = function (t, e) {
				function i(t) {
					return t.classList.contains("dhx_grid-custom-content-cell")
				}
				var n = t.target;
				i(n.parentElement) || i(n.parentElement.parentElement) || !(e.draggable || "column" === this.config.dragItem && !1 !== e.draggable || "both" === this.config.dragItem && !1 !== e.draggable) || u.locateNodeByClassName(t, "dhx_resizer_grip_wrap") || (t.targetTouches && (this._touch.start = !0), c.dragManager.onMouseDown(t, [e.id], [this._getColumnGhost(e)]))
			}, _);

		function _(t, e) {
			var i = s.call(this, t, r({
				autoHeight: !1
			}, e)) || this;
			return i.scrollView = new p.ScrollView(function () {
				return i.getRootView()
			}, {
				scrollHandler: function (t) {
					return i.events.fire(l.GridEvents.scroll, [{
						y: t.target.scrollTop,
						x: t.target.scrollLeft
					}])
				}
			}), i
		}
		e.ProGrid = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = (o.prototype.setItem = function (t, e) {
			this._store[t] = e
		}, o.prototype.getItem = function (t) {
			return this._store[t] || null
		}, o);

		function o() {
			this._store = {}
		}
		var r = window.dhxHelpers = window.dhxHelpers || {};
		r.collectionStore = r.collectionStore || new n, e.collectionStore = r.collectionStore
	}, function (t, l, c) {
		"use strict";
		(function (t) {
			var n, e = this && this.__extends || (n = function (t, e) {
				return (n = Object.setPrototypeOf || {
					__proto__: []
				}
					instanceof Array && function (t, e) {
						t.__proto__ = e
					} || function (t, e) {
						for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
					})(t, e)
			}, function (t, e) {
				function i() {
					this.constructor = t
				}
				n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
			});
			Object.defineProperty(l, "__esModule", {
				value: !0
			});
			var o, i = c(26),
				r = c(1),
				s = c(41),
				e = (o = i.DataProxy, e(a, o), a.prototype.load = function () {
					var e = this;
					return new t(function (t) {
						e._timeout ? (clearTimeout(e._timeout), e._timeout = setTimeout(function () {
							s.ajax.get(e.url, {
								responseType: "text"
							}).then(t), e._cooling = !0
						}, e.config.delay), e._cooling && (t(null), e._cooling = !1)) : (s.ajax.get(e.url, {
							responseType: "text"
						}).then(t), e._cooling = !0, e._timeout = setTimeout(function () { }))
					})
				}, a);

			function a(t, e) {
				var i = o.call(this, t) || this;
				return i.config = r.extend({
					from: 0,
					limit: 50,
					delay: 50,
					prepare: 0
				}, e), i.updateUrl(t, {
					from: i.config.from,
					limit: i.config.limit
				}), i
			}
			l.LazyDataProxy = e
		}).call(this, c(15))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(3),
			n = i(29),
			r = i(20),
			i = (s.prototype.getId = function () {
				return this._selected
			}, s.prototype.getItem = function () {
				return this._selected ? this._data.getItem(this._selected) : null
			}, s.prototype.remove = function (t) {
				return !(t = t || this._selected) || !!this.events.fire(n.SelectionEvents.beforeUnSelect, [t]) && (this._data.update(t, {
					$selected: !1
				}, !0), this._selected = null, this.events.fire(n.SelectionEvents.afterUnSelect, [t]), !0)
			}, s.prototype.add = function (t) {
				this._selected !== t && !this.config.disabled && this._data.exists(t) && (this.remove(), this._addSingle(t))
			}, s.prototype.enable = function () {
				this.config.disabled = !1
			}, s.prototype.disable = function () {
				this.remove(), this.config.disabled = !0
			}, s.prototype._addSingle = function (t) {
				this.events.fire(n.SelectionEvents.beforeSelect, [t]) && (this._selected = t, this._data.update(t, {
					$selected: !0
				}, !0), this.events.fire(n.SelectionEvents.afterSelect, [t]))
			}, s);

		function s(t, e, i) {
			var n = this;
			this.events = i || new o.EventSystem(this), this._data = e, this.config = t, this._data.events.on(r.DataEvents.removeAll, function () {
				n._selected = null
			}), this._data.events.on(r.DataEvents.change, function () {
				var t;
				!n._selected || (t = n._data.getNearId(n._selected)) !== n._selected && (n._selected = null, t && n.add(t))
			})
		}
		e.Selection = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(1),
			c = i(0),
			u = i(3),
			d = i(2),
			h = i(17),
			f = i(4),
			p = i(6),
			_ = i(11),
			v = i(10),
			g = i(25),
			m = i(19),
			y = i(48),
			b = i(35),
			w = i(62),
			x = i(196),
			E = i(197),
			C = i(198),
			k = i(199),
			o = (a = f.View, o(S, a), S.prototype.destructor = function () {
				this.toolbar.destructor(), this._readStack.stop(), this.uploader.unlinkDropArea(), this.uploader.abort()
			}, S.prototype.getRootView = function () {
				return this._layout.getRootView()
			}, S.prototype._initUI = function (t) {
				var e = this,
					i = this.config.toolbar ? x.layoutConfig : x.layoutConfigWithoutTopbar;
				i.on = this._getDragEvents();
				t = this._layout = new _.Layout(t, i), i = this.toolbar = new g.Toolbar(null, {
					css: "vault-toolbar"
				});
				this.toolbar.data.parse([{
					id: "add",
					tooltip: y.default.add,
					type: "button",
					icon: "dxi-plus"
				}, {
					id: "upload",
					tooltip: y.default.upload,
					type: "button",
					icon: "dxi icon-upload"
				}, {
					id: "spacer",
					type: "spacer"
				}, {
					id: "remove-all",
					tooltip: y.default.clearAll,
					type: "button",
					icon: "dxi-delete-forever"
				}]), this._hideUploadAndDeleteButtons(), this._vaultView = f.toViewLike(c.create({
					render: function () {
						return e._draw()
					}
				})), this.config.toolbar && t.getCell("topbar").attach(i), t.getCell("vault").attach(this._vaultView)
			}, S.prototype._initHandlers = function () {
				var i = this;
				this._handlers = {
					onclick: {
						".action-add": function () {
							return i.uploader.selectFile()
						},
						".action-remove-file": function (t) {
							var e = d.locate(t);
							e && (i.data.update(e, {
								$toRemove: !0
							}), setTimeout(function () {
								i.data.update(e, {
									$toRemove: !1
								}, !0), i.data.remove(e)
							}, 200))
						}
					},
					onmouseover: {
						".action-download": function (t) {
							v.tooltip(y.default.download, {
								node: t.target,
								position: v.Position.bottom
							})
						},
						".action-remove-file": function (t) {
							v.tooltip(y.default.clear, {
								node: t.target,
								position: v.Position.bottom
							})
						},
						".title-content, .dhx-file-name": function (t) {
							var e = d.locate(t),
								e = i.data.getItem(e);
							v.tooltip(e.name, {
								node: t.target,
								position: v.Position.bottom,
								css: "tooltip-light"
							})
						}
					}
				}
			}, S.prototype._getDragEvents = function () {
				var o = this,
					r = {
						left: null,
						top: null,
						width: null,
						height: null
					};
				return {
					dragleave: function (t) {
						o._canDrop && (t.pageX > r.left + r.width - 1 || t.pageX < r.left || t.pageY > r.top + r.height - 1 || t.pageY < r.top) && (o._canDrop = !1, o.config.toolbar && o._layout.getCell("topbar").show(), o._layout.config.css = "vault-layout", o._layout.paint())
					},
					dragenter: function (t) {
						if (t.preventDefault(), !o.uploader.isActive && !o._canDrop) {
							for (var e = 0, i = t.dataTransfer.types; e < i.length; e++) {
								var n = i[e];
								if ("Files" !== n && "application/x-moz-file" !== n) return void (o._canDrop = !1)
							}
							o._canDrop = !0;
							t = o.getRootView().node.el.getBoundingClientRect();
							r.left = t.left + window.pageXOffset, r.top = t.top + window.pageYOffset, r.width = t.width, r.height = t.height, o._canDrop = !0, o.config.toolbar && o._layout.getCell("topbar").hide(), o._layout.config.css = "vault-layout dhx-dragin", o._layout.paint()
						}
					},
					dragover: function (t) {
						t.preventDefault()
					},
					drop: function (t) {
						t.preventDefault(), o._canDrop && (t = t.dataTransfer, o.uploader.parseFiles(t), o._canDrop = !1, o.config.toolbar && o._layout.getCell("topbar").show(), o._layout.config.css = "vault-layout", o._layout.paint())
					}
				}
			}, S.prototype._hideUploadAndDeleteButtons = function () {
				this.toolbar.hide(["upload", "remove-all"])
			}, S.prototype._showUploadAndDeleteButtons = function () {
				this.uploader.config.autosend ? this.toolbar.show("remove-all") : this.toolbar.show(["upload", "remove-all"])
			}, S.prototype._initEvents = function () {
				var e = this;
				this.data.events.on(p.DataEvents.change, function () {
					e.data.getLength() ? e._showUploadAndDeleteButtons() : e._hideUploadAndDeleteButtons(), e._vaultView.paint()
				}), this.events.on(b.UploaderEvents.uploadBegin, function () {
					e.config.toolbar && e._layout.getCell("topbar").attach(e._progressBar)
				}), this.events.on(b.UploaderEvents.uploadComplete, function () {
					e.config.mode === b.VaultMode.grid && e.uploader.config.autosend && e._readStack.read(), e.config.toolbar && e._layout.getCell("topbar").attach(e.toolbar)
				}), this.toolbar.events.on(m.NavigationBarEvents.click, function (t) {
					switch (t) {
						case "add":
							e.uploader.selectFile();
							break;
						case "remove-all":
							e.data.removeAll();
							break;
						case "upload":
							e.uploader.send()
					}
				}), this.events.on(b.ProgressBarEvents.cancel, function () {
					e.uploader.abort(), e._vaultView.paint()
				})
			}, S.prototype._draw = function () {
				var t = !this.data.getLength(),
					e = this.config.mode === b.VaultMode.grid ? this._drawGrid() : this._drawList();
				return c.el("div", r(r({
					class: "vault dhx_widget" + (this._canDrop ? " drop-here" : "")
				}, this._handlers), {
					dhx_widget_id: this._uid
				}), [this._canDrop || t ? this._drawDropableArea() : this.config.customScroll ? this._scrollView.render(e) : e])
			}, S.prototype._getFileActions = function (t) {
				var e, i = [],
					n = [],
					o = [c.el(".dhx-default-actions", i), c.el(".dhx-hover-actions", n)];
				if (t.status === b.FileStatus.inprogress) return o;
				t.status !== b.FileStatus.failed && t.link && (e = (r = (this.config.downloadURL || "") + t.link).split("/").pop().split("?")[0], r = c.el("a", {
					download: e,
					class: "download-link",
					href: r
				}, [c.el(".icon-btn.dxi.dxi-download.action-download")]), n.push(r));
				var r = c.el(".icon-btn.dxi.dxi-delete-forever.action-remove-file");
				return n.push(r), t.status === b.FileStatus.failed && (r = c.el(".dxi.dxi-alert-circle.warning-status"), i.push(r)), t.status === b.FileStatus.uploaded && (t = c.el(".dxi.dxi-checkbox-marked-circle.uploaded-status"), i.push(t)), o
			}, S.prototype._drawList = function () {
				var r = this;
				return c.el(".dhx-files-block.dhx-webkit-scroll", this.data.map(function (t) {
					var e = t.status === b.FileStatus.failed && t.request,
						i = t.status === b.FileStatus.inprogress,
						n = t.status === b.FileStatus.queue,
						o = t.status !== b.FileStatus.uploaded;
					return c.el("div", {
						class: "dhx-file-item" + (t.$toRemove ? " to-remove" : "") + (n ? " in-queue" : ""),
						dhx_id: t.id,
						_key: t.id
					}, [c.el(".dhx-file-icon", [c.el("div", {
						class: "dhx-file-type " + E.getFileClassName(t) + (o ? " not-loaded" : "")
					})]), c.el(".dhx-file-title", [c.el(".dhx-title-content", t.name), c.el(".dhx-file-info", [e && c.el(".warn-message", t.request.statusText || y.default.error), i ? c.el(".progress-value", (100 * t.progress).toFixed(1) + "%") : c.el(".dhx-size" + (e ? ".dhx-size-error" : ""), E.getBasis(t.size))])]), i && c.el(".dhx-download-progress", {
						style: {
							width: (100 * t.progress).toFixed(1) + "%"
						}
					}), !i && c.el(".dhx-file-action", r._getFileActions(t))])
				}))
			}, S.prototype._drawDropableArea = function () {
				return c.el(".dhx-dropable-area.drop-files-here", [c.el(".dhx-big-icon-block", [c.el(".dxi.icon-upload")]), !this._canDrop && c.el(".drop-area-bold-text", y.default.dragAndDrop), !this._canDrop && c.el(".drop-area-bold-text", y.default.filesOrFoldersHere), !this._canDrop && c.el(".drop-area-light-text", y.default.or), !this._canDrop && c.el("button.dhx_btn.dhx_btn--flat.dhx_btn--small.action-add", y.default.browse)])
			}, S.prototype._drawGrid = function () {
				var l = this;
				return c.el("div", {
					class: "dhx-files-grid dhx-webkit-scroll"
				}, [c.el(".dhx-grid-content", this.data.map(function (a) {
					var t = a.status === b.FileStatus.inprogress,
						e = a.status === b.FileStatus.queue,
						i = a.status === b.FileStatus.failed;
					return c.el("div", {
						class: "dhx-file-grid-item" + (t ? " in-progress" : "") + (a.$toRemove ? " to-remove" : "") + (e ? " in-queue" : "") + (i ? " failed" : ""),
						dhx_id: a.id,
						_key: a.id
					}, [c.el(".dhx-preview-wrapper", s([a.preview ? c.el(".dhx-server-file-preview", [c.el("img", {
						src: a.preview
					})]) : a.image ? c.el("canvas", {
						width: 98 * l.config.scaleFactor,
						height: 98 * l.config.scaleFactor,
						_hooks: {
							didInsert: function (t) {
								var e = E.calculateCover(a.image),
									i = e.dx,
									n = e.dy,
									o = e.sx,
									r = e.sy,
									s = e.sHeight,
									e = e.sWidth;
								t.el.getContext("2d").drawImage(a.image, o, r, e, s, i, n, 98 * l.config.scaleFactor, 98 * l.config.scaleFactor)
							}
						}
					}) : c.el("div", {
						class: "dhx-file-preview dhx-file-type " + E.getFileClassName(a)
					}), t && l._drawCircle(a.progress)], l._getFileActions(a), [c.el(".dhx-file-info", [i && c.el(".warn-message", a.request.statusText || y.default.error), !t && c.el(".dhx-size" + (i ? ".dhx-size-error" : ""), E.getBasis(a.size))])])), c.el(".dhx-file-name", E.truncateWord(a.name))])
				}))])
			}, S.prototype._drawCircle = function (t) {
				return c.el(".progress-layout", [c.el(".progress-amount", (100 * t).toFixed(1) + "%"), c.sv("svg", {
					xmlns: "http://www.w3.org/2000/svg",
					class: "progress-circle",
					viewBox: "0 0 60 60"
				}, [c.sv("circle", {
					cx: 30,
					cy: 30,
					r: 28,
					"stroke-width": 4,
					class: "progress-bar-background"
				}), c.sv("circle.active-circle", {
					cx: 30,
					cy: 30,
					r: 28,
					"stroke-width": 4,
					"stroke-dasharray": "175.9 175.9",
					"stroke-dashoffset": 175.9 * (1 - t),
					class: "progress-bar-active"
				})])])
			}, S);

		function S(t, e) {
			void 0 === e && (e = {});
			var n = a.call(this, null, l.extend({
				mode: b.VaultMode.list,
				toolbar: !0,
				updateFromResponse: !0,
				scaleFactor: 4,
				customScroll: !0,
				uploader: {},
				progressBar: {}
			}, e)) || this;
			return n.config.toolbar || (n.config.uploader.autosend = !0), e.data ? (n.data = e.data, n.events = e.data.events, n.events.context = n) : (n.events = new u.EventSystem(n), n.data = new p.DataCollection({}, n.events)), n.data.config.init = function (t) {
				return t.status = t.status || b.FileStatus.uploaded, t.file ? (t.size = t.file.size, t.name = t.file.name) : (t.size = t.size || 0, t.name = t.name || ""), n.config.mode === b.VaultMode.grid && t.file && E.isImage(t) && n._readStack.add(t, n.uploader.config.autosend), t
			}, n._readStack = new k.ReadStackPreview(n.data), n.uploader = new w.Uploader(n.config.uploader, n.data, n.events), n._scrollView = new h.ScrollView(function () {
				return n._vaultView.getRootView()
			}), n._progressBar = new C.ProgressBar(n.events, n.config.progressBar), n.events.on(b.UploaderEvents.uploadProgress, function (t, e, i) {
				return n._progressBar.setState(t, {
					current: e,
					total: i
				})
			}), n._initHandlers(), n._initUI(t), n._initEvents(), n
		}
		e.Vault = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			u = this && this.__assign || function () {
				return (u = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(1),
			d = i(0),
			a = i(3),
			h = i(2),
			l = i(13),
			s = i(4),
			c = i(6),
			f = i(36);
		var p, o = (p = s.View, o(_, p), _.prototype.paint = function () {
			p.prototype.paint.call(this), this._isContextMenu && !this._vpopups && this._init(), this._vpopups && this._vpopups.redraw()
		}, _.prototype.disable = function (t) {
			var e = this;
			void 0 !== t ? this._setProp(t, "disabled", !0) : this.data.forEach(function (t) {
				t = t.id;
				return e._setProp(t, "disabled", !0)
			})
		}, _.prototype.enable = function (t) {
			var e = this;
			void 0 !== t ? this._setProp(t, "disabled", !1) : this.data.forEach(function (t) {
				t = t.id;
				return e._setProp(t, "disabled", !1)
			})
		}, _.prototype.isDisabled = function (t) {
			t = this.data.getItem(t);
			if (t) return t.disabled || !1
		}, _.prototype.show = function (t) {
			var e = this;
			void 0 !== t ? this._setProp(t, "hidden", !1) : this.data.forEach(function (t) {
				t = t.id;
				return e._setProp(t, "hidden", !1)
			})
		}, _.prototype.hide = function (t) {
			var e = this;
			void 0 !== t ? this._setProp(t, "hidden", !0) : this.data.forEach(function (t) {
				t = t.id;
				return e._setProp(t, "hidden", !0)
			})
		}, _.prototype.destructor = function () {
			this.events.clear(), this._keyManager && this._keyManager.destructor(), this._vpopups && this._vpopups.node && this._vpopups.unmount(), this.unmount()
		}, _.prototype.select = function (t, e) {
			var i = this;
			if (void 0 === e && (e = !0), !t) throw new Error("Function argument cannot be empty, for more info check documentation https://docs.dhtmlx.com");
			e && this.unselect(), this.data.update(t, {
				active: !0
			}), this.data.eachParent(t, function (t) {
				i.data.update(t.id, {
					active: !0
				})
			})
		}, _.prototype.unselect = function (t) {
			var e = this;
			t ? (this.data.update(t, {
				active: !1
			}), this.data.eachChild(t, function (t) {
				e.data.update(t.id, {
					active: !1
				})
			})) : this.data.forEach(function (t) {
				e.data.update(t.id, {
					active: !1
				})
			})
		}, _.prototype.isSelected = function (t) {
			if (t && this.data.getItem(t)) return !!this.data.getItem(t).active
		}, _.prototype.getSelected = function () {
			var e = [];
			return this.data.forEach(function (t) {
				t.active && e.push(t.id)
			}), e
		}, _.prototype._customHandlers = function () {
			return {}
		}, _.prototype._close = function (t) {
			var e = this;
			this._popupActive && this.events.fire(f.NavigationBarEvents.beforeHide, [this._activeMenu, t]) && (this._activeParents && this._activeParents.forEach(function (t) {
				return e.data.exists(t) && e.data.update(t, {
					$activeParent: !1
				})
			}), "click" === this.config.navigationType && (this._isActive = !1), clearTimeout(this._currentTimeout), this._popupActive = !1, this._activeMenu = null, this._vpopups.node && this._vpopups.unmount(), this._vpopups = null, this.events.fire(f.NavigationBarEvents.afterHide, [t]), this.paint())
		}, _.prototype._init = function () {
			var t = this;
			this._vpopups = d.create({
				render: function () {
					return d.el("div", {
						dhx_widget_id: t._uid,
						class: (t._isContextMenu ? " dhx_context-menu" : "") + " " + (t.config.css ? t.config.css.split(" ").map(function (t) {
							return t + "--context-menu"
						}).join(" ") : ""),
						onmousemove: t._handlers.onmousemove,
						onmouseleave: t._handlers.onmouseleave,
						onclick: t._handlers.onclick,
						onmousedown: t._handlers.onmousedown
					}, t._drawPopups())
				}
			}), this._vpopups.mount(document.body)
		}, _.prototype._initHandlers = function () {
			var a = this;
			this._isActive = "click" !== this.config.navigationType, this._handlers = u({
				onmousemove: function (t) {
					var e, i;
					!a._isActive || (i = h.locateNode(t)) && (e = i.getAttribute("dhx_id"), a._activeMenu !== e && (a.data.haveItems(e) && (a._vpopups || a._init(), i = h.getRealPosition(i), a.data.update(e, {
						$position: i
					}, !1)), a._activeItemChange(e, t)))
				},
				onmouseleave: function (t) {
					var e;
					"click" !== a.config.navigationType && (a._popupActive && ((e = h.locateNode(t, "dhx_id", "relatedTarget")) ? (e = e.getAttribute("dhx_id"), a.data.getItem(e) || a._close(t)) : a._close(t)), a._activeItemChange(null, t))
				},
				onclick: function (t) {
					var e = h.locateNode(t);
					if (e) {
						var i = e.getAttribute("dhx_id");
						if (!a.isDisabled(i)) {
							var n = a.data.getItem(i);
							if (!n.multiClick)
								if (a.data.haveItems(i)) a._vpopups || a._init(), i !== a._currentRoot && (a._isActive || (a._isActive = !0), a._setRoot(i), e = h.getRealPosition(e), a.data.update(i, {
									$position: e
								}, !1), a._activeItemChange(i, t), a.events.fire(f.NavigationBarEvents.click, [i, t]));
								else switch (n.type) {
									case "input":
									case "title":
										break;
									case "menuItem":
									case "selectButton":
										a._onMenuItemClick(i, t);
										break;
									case "imageButton":
									case "button":
									case "customButton":
									case "customHTML":
									case "navItem":
										n.twoState && a.data.update(n.id, {
											active: !n.active
										}), a.events.fire(f.NavigationBarEvents.click, [i, t]), a._close(t);
										break;
									default:
										a._close(t)
								}
						}
					}
				},
				onmousedown: function (t) {
					var e, i, n, o, r, s = h.locateNode(t);
					s && (e = s.getAttribute("dhx_id"), a.data.getItem(e).multiClick && (i = 365, r = function () {
						clearTimeout(n), document.removeEventListener("mouseup", r)
					}, (o = function () {
						a.events.fire(f.NavigationBarEvents.click, [e, t]), 50 < i && (i -= 55), n = setTimeout(o, i)
					})(), document.addEventListener("mouseup", r)))
				}
			}, this._customHandlers())
		}, _.prototype._initEvents = function () {
			var n = this,
				t = null;
			this.data.events.on(f.DataEvents.change, function () {
				n.paint(), t && clearTimeout(t), t = setTimeout(function () {
					var i = {};
					n.data.eachChild(n.data.getRoot(), function (t) {
						var e;
						t.group && (t.twoState = !0, (e = i)[(t = t).group] ? (t.active && (e[t.group].active = t.id), e[t.group].elements.push(t.id)) : e[t.group] = {
							active: t.active ? t.id : null,
							elements: [t.id]
						})
					}, !0), n._groups = i, n._resetHotkeys(), t = null, n.paint()
				}, 100)
			}), this.events.on(f.NavigationBarEvents.click, function (t) {
				var e = n.data.getItem(t),
					t = n.data.getItem(e.parent);
				t && "selectButton" === t.type && n.data.update(e.parent, {
					value: e.value,
					icon: e.icon
				}), e.group && ((t = n._groups[e.group]).active && n.data.update(t.active, {
					active: !1
				}), t.active = e.id, n.data.update(e.id, {
					active: !0
				}))
			}), this.events.on(f.NavigationBarEvents.inputChange, function (t, e) {
				n.data.update(t, {
					value: e
				})
			}), this._customInitEvents()
		}, _.prototype._getMode = function (t, e, i) {
			return void 0 === i && (i = !1), t.parent === e ? "bottom" : "right"
		}, _.prototype._drawMenuItems = function (t, e) {
			var i = this;
			return void 0 === e && (e = !0), this.data.map(function (t) {
				return i._factory(t, e)
			}, t, !1)
		}, _.prototype._setRoot = function (t) { }, _.prototype._getParents = function (t, e) {
			var i = [],
				n = !1,
				o = this.data.getItem(t),
				o = o && o.disabled;
			return this.data.eachParent(t, function (t) {
				t.id === e ? (i.push(t.id), n = !0) : n || i.push(t.id)
			}, !o), this._isContextMenu && this._activePosition && i.push(e), i
		}, _.prototype._listenOuterClick = function () {
			this._documentHaveListener || (document.addEventListener("mousedown", this._documentClick, !0), this._documentHaveListener = !0)
		}, _.prototype._customInitEvents = function () { }, _.prototype._drawPopups = function () {
			var a = this,
				t = this._activeMenu;
			if (!this._isContextMenu && !t) return null;
			var e = this.getRootNode(),
				e = e && e.offsetParent && e.offsetParent.offsetParent,
				l = null;
			e && e.classList.contains("dhx_popup--window") && e.classList.contains("dhx_popup--window_active") && (l = 2147483647);
			var c = this._currentRoot;
			if (this._isContextMenu && !this._activePosition) return null;
			t = this._getParents(t, c);
			return (this._activeParents = t).forEach(function (t) {
				return a.data.exists(t) && a.data.update(t, {
					$activeParent: !0
				}, !1)
			}), t.map(function (r) {
				if (!a.data.haveItems(r)) return null;
				var s = a.data.getItem(r) || a._rootItem;
				return a._popupActive = !0, d.el("ul", u({
					class: "dhx_widget dhx_menu" + (a.config.menuCss ? " " + a.config.menuCss : ""),
					_key: r,
					_hooks: {
						didInsert: function (t) {
							var e = t.el.getBoundingClientRect(),
								i = e.width,
								n = e.height,
								o = a._isContextMenu && a._activePosition && r === c ? a._activePosition : s.$position,
								e = a._getMode(s, c, o === a._activePosition),
								n = h.calculatePosition(o, {
									mode: e,
									auto: !0,
									width: i,
									height: n
								});
							s.$style = u(u({}, n), {
								zIndex: a._activePosition && a._activePosition.zIndex || l
							}), t.patch({
								style: s.$style
							})
						},
						didRecycle: function (t, e) {
							var i, n;
							a._isContextMenu && a._activePosition && r === c && (i = (n = e.el.getBoundingClientRect()).width, n = n.height, n = h.calculatePosition(a._activePosition, {
								mode: a._getMode(s, c, !0),
								width: i,
								height: n
							}), s.$style = u(u({}, n), {
								zIndex: a._activePosition.zIndex || l
							}), e.patch({
								style: s.$style
							}))
						}
					},
					tabindex: 0,
					style: s.$style || {
						position: "absolute"
					}
				}, {
					role: "menu",
					"aria-labeledby": s.id,
					"aria-live": "polite"
				}), a._drawMenuItems(r))
			}).reverse()
		}, _.prototype._onMenuItemClick = function (t, e) {
			var i = this.data.getItem(t);
			i.disabled || (i.twoState && this.data.update(i.id, {
				active: !i.active
			}), this.events.fire(f.NavigationBarEvents.click, [t, e]), this._close(e))
		}, _.prototype._activeItemChange = function (t, e) {
			var i, n = this;
			this._activeParents && (i = this._getParents(t, this._currentRoot), this._activeParents.forEach(function (t) {
				n.data.exists(t) && !i.includes(t) && n.data.update(t, {
					$activeParent: !1
				}, !1)
			})), t && !this._documentHaveListener && this._listenOuterClick(), t && this.data.haveItems(t) ? (this._activeMenu === t && this._popupActive || this.events.fire(f.NavigationBarEvents.openMenu, [t]), clearTimeout(this._currentTimeout), this.paint()) : (clearTimeout(this._currentTimeout), this._currentTimeout = setTimeout(function () {
				return n.paint()
			}, 400)), this._activeMenu = t
		}, _.prototype._resetHotkeys = function () {
			var e = this;
			this._keyManager.removeHotKey(null, this), this.data.map(function (t) {
				t.hotkey && e._keyManager.addHotKey(t.hotkey, function () {
					return e._onMenuItemClick(t.id, null)
				})
			})
		}, _.prototype._setProp = function (t, e, i) {
			var n = this;
			Array.isArray(t) ? t.forEach(function (t) {
				return n.data.update(t, ((t = {})[e] = i, t))
			}) : this.data.update(t, ((t = {})[e] = i, t))
		}, _);

		function _(t, e) {
			var s = p.call(this, t, e) || this;
			return s._isContextMenu = !1, s._documentHaveListener = !1, s.config = r.extend({
				rootId: "string" == typeof t && t || s._uid
			}, e), s._rootItem = {}, !Array.isArray(s.config.data) && s.config.data && s.config.data.events ? (s.data = s.config.data, s.events = s.data.events, s.events.context = s) : (s.events = new a.EventSystem(s), s.data = new c.TreeCollection({
				rootId: s.config.rootId
			}, s.events)), s._documentClick = function (t) {
				var e, i, n, o, r;
				s._documentHaveListener && (e = h.locateNode(t), i = s.data.getRoot(), n = e && e.getAttribute("dhx_id"), o = s.data.getParent(n), r = "ontouchstart" in window || navigator.msMaxTouchPoints, document.removeEventListener("mousedown", s._documentClick), s._documentHaveListener = !1, ((!r || e) && s._isContextMenu || i !== o && o && s.data.getItem(n)) && o && s.data.getItem(n) || s._close(t))
			}, s._currentRoot = s.data.getRoot(), s._factory = s._getFactory(), s._initHandlers(), s._keyManager = new l.KeyManager(function (t, e) {
				return e === s._uid
			}), s._initEvents(), Array.isArray(s.config.data) && s.data.parse(s.config.data), s
		}
		e.Navbar = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var d = i(185),
			h = i(186),
			f = i(187),
			p = i(188),
			_ = i(189),
			v = i(190),
			g = i(191),
			m = i(192),
			y = i(193),
			b = i(194),
			w = i(30);
		e.createFactory = function (t) {
			for (var n = t.defaultType, e = t.allowedTypes, o = t.widgetName, t = t.widget, r = new Set, i = 0, s = e; i < s.length; i++) {
				var a = s[i];
				r.add(a)
			}
			var l = t.config,
				c = t.events,
				u = t.data;
			return function (t, e) {
				if (t.hidden) return null;
				if (!(t.type && "button" !== t.type && "navItem" !== t.type && "menuItem" !== t.type || t.value || t.icon || t.html)) return null;
				t.type = t.type || n, r && !r.has(t.type) && (t.type = n), "imageButton" === t.type && "ribbon" !== o && (t.active = !1), e && "spacer" !== t.type && "separator" !== t.type && "customHTML" !== t.type && (t.type = "menuItem"), u.haveItems(t.id) && function (t, e, i) {
					switch (t) {
						case "sidebar":
						case "context-menu":
							e.$openIcon = "right";
							break;
						case "toolbar":
							e.parent === i.getRoot() ? e.$openIcon = "right" : e.$openIcon = "bottom";
							break;
						case "menu":
							e.parent !== this.data.getRoot() && (e.$openIcon = "right");
							break;
						case "ribbon":
							var n = i.getItem(e.parent);
							n && "block" !== e.type && ("block" === n.type ? e.$openIcon = "bottom" : e.$openIcon = "right")
					}
				}(o, t, u), "toolbar" === o && t.items && t.items.forEach(function (t) {
					t.type || (t.type = "menuItem")
				});
				var i = "customHTML" !== t.type && function (t, e, i, n) {
					switch (t.type) {
						case "navItem":
						case "selectButton":
							return h.navItem(t, i, n.collapsed);
						case "button":
							return d.button(t, i);
						case "title":
							return y.title(t, i);
						case "separator":
							return g.separator(t, i);
						case "spacer":
							return m.spacer(t, i);
						case "input":
							return _.input(t, e, i);
						case "imageButton":
							return p.imageButton(t, i);
						case "menuItem":
							return v.menuItem(t, i, n.asMenuItem);
						case "customHTMLButton":
							return f.customHTMLButton(t, i, n.asMenuItem);
						case "datePicker":
							return b.datePicker(t, e, i);
						case "block":
						default:
							throw new Error("unknown item type " + t.type)
					}
				}(t, c, o, {
					asMenuItem: e,
					collapsed: "sidebar" !== o || l.collapsed
				});
				return w.navbarComponentMixin(o, t, e, i)
			}
		}
	}, function (t, e, i) {
		"use strict";
		var s = this && this.__assign || function () {
			return (s = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a = i(0),
			l = i(30);
		e.button = function (t, e) {
			var i, n, o = t.icon && !t.value,
				r = o ? " dhx_navbar-count--absolute" : " dhx_navbar-count--button-inline";
			return a.el("button.dhx_button", s({
				class: l.getNavbarButtonCSS(t, e),
				dhx_id: t.id,
				disabled: t.disabled,
				type: "button"
			}, (n = (i = t).active || i.$activeParent, e = {
				"aria-disabled": i.disabled ? "true" : "false",
				"aria-label": i.value || i.tooltip || i.id || " " + (i.count || "")
			}, i.items && (e.id = i.id, e["aria-haspopup"] = "menu", n && (e["aria-expanded"] = "true")), e)), [t.icon ? l.getIcon(t.icon, "button") : null, t.html ? a.el("div.dhx_button__text", {
				".innerHTML": t.html
			}) : t.value && a.el("span.dhx_button__text", t.value), 0 < t.count && l.getCount(t, r, o), t.value && t.$openIcon ? a.el("span.dhx_button__icon.dhx_button__icon--menu.dxi.dxi-menu-right", {
				"aria-hidden": "true"
			}) : null, t.loading && a.el("span.dhx_button__loading", {
				"aria-hidden": "true"
			}, [a.el("span.dhx_button__loading-icon.dxi.dxi-loading")])])
		}
	}, function (t, e, i) {
		"use strict";
		var s = this && this.__assign || function () {
			return (s = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a = i(0),
			l = i(30);
		e.navItem = function (t, e, i) {
			var n, o, r, e = " dhx_" + e + "-button";
			return a.el("button", s({
				class: "dhx_button" + e + (t.active || t.$activeParent ? e + "--active" : "") + (t.disabled ? e + "--disabled" : "") + (t.$openIcon ? e + "--select" : "") + (t.circle ? e + "--circle" : "") + (t.size ? " " + e + "--" + t.size : "") + (!t.value && t.icon ? e + "--icon" : "") + (t.css ? " " + t.css : ""),
				dhx_id: t.id,
				disabled: t.disabled,
				type: "button"
			}, (o = {
				"aria-disabled": (n = t).disabled ? "true" : "false",
				"aria-label": n.value || " "
			}, r = n.active || n.$activeParent, "selectButton" === n.type || n.items ? (o.id = n.id, o["aria-haspopup"] = "menu", r && (o["aria-expanded"] = "true")) : ((n.twoState || r) && (o["aria-pressed"] = r ? "true" : "false"), !n.value && n.icon && n.tooltip && (o["aria-label"] = n.tooltip + " " + (n.count || ""))), o)), [t.icon && a.el("span", {
				class: t.icon + e + "__icon",
				"aria-hidden": "true"
			}), t.html && a.el("div", {
				class: e.trim() + "__html",
				".innerHTML": t.html
			}), !t.html && t.value && a.el("span", {
				class: e.trim() + "__text"
			}, t.value), 0 < t.count && l.getCount(t, e + "__count", i), t.$openIcon && a.el("span.dxi.dxi-menu-right", {
				class: e + "__caret",
				"aria-hidden": "true"
			})])
		}
	}, function (t, e, i) {
		"use strict";
		var o = this && this.__assign || function () {
			return (o = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r = i(0);
		e.customHTMLButton = function (t, e, i) {
			var n, i = i ? " dhx_button dhx_menu-button" : " dhx_button dhx_nav-menu-button";
			return r.el("button", o({
				class: "dhx_custom-button" + i + (t.$activeParent ? i + "--active" : ""),
				dhx_id: t.id,
				type: "button",
				".innerHTML": t.html
			}, (i = {
				"aria-disabled": (n = t).disabled ? "true" : "false"
			}, (n.twoState || n.active || n.$activeParent) && (i["aria-pressed"] = n.active || n.$activeParent ? "true" : "false"), i)), t.html ? "" : t.value)
		}
	}, function (t, e, i) {
		"use strict";
		var r = this && this.__assign || function () {
			return (r = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s = i(0),
			a = i(30);
		e.imageButton = function (t, e) {
			var i, n, o = "dhx_" + e + "-button-image",
				e = "ribbon" === e;
			return s.el("button.dhx_button", r({
				class: o + (t.size ? " " + o + "--" + t.size : "") + (!t.value && t.src ? " " + o + "--icon" : "") + (e && t.$openIcon ? " " + o + "--select" : "") + (t.active ? " " + o + "--active" : ""),
				dhx_id: t.id,
				type: "button"
			}, (n = {
				"aria-disabled": (i = t).disabled ? "true" : "false"
			}, (i.twoState || i.active) && (n["aria-pressed"] = i.active ? "true" : "false"), !i.value && i.src && i.tooltip && (n["aria-label"] = i.tooltip + " " + (i.count || "")), n)), [e && t.value && t.$openIcon && s.el("span.dxi.dxi-menu-right", {
				class: o + "__caret",
				"aria-hidden": "true"
			}), t.html ? s.el("div", {
				class: o + "__text",
				".innerHTML": t.html
			}) : t.value && s.el("span", {
				class: o + "__text"
			}, t.value), t.src && s.el("span", {
				class: o + "__image",
				style: {
					backgroundImage: "url(" + t.src + ")"
				},
				role: "presentation"
			}), 0 < t.count && a.getCount(t, o + "__count", !0)])
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0),
			o = i(36);

		function r(t, e) {
			t.fire(o.NavigationBarEvents.inputBlur, [e])
		}

		function s(t, e) {
			t.fire(o.NavigationBarEvents.inputFocus, [e])
		}

		function a(t, e, i) {
			t.fire(o.NavigationBarEvents.inputChange, [e, i.target.value])
		}
		e.input = function (e, i, t) {
			return n.el(".dhx_form-group.dhx_form-group--no-message-holder.dhx_form-group--label_sr.dhx_" + t + "__input", {
				style: {
					width: e.width || "200px"
				},
				role: "presentation"
			}, [e.label && n.el("label.dhx_label", {
				for: e.id
			}, e.label), n.el(".dhx_input__wrapper", {
				role: "presentation"
			}, [n.el("input.dhx_input", {
				placeholder: e.placeholder,
				class: e.icon ? "dhx_input--icon-padding" : "",
				value: e.value,
				disabled: e.disabled,
				onblur: [r, i, e.id],
				onfocus: [s, i, e.id],
				oninput: [a, i, e.id],
				dhx_id: e.id,
				_hooks: {
					didInsert: function (t) {
						i && i.fire(o.NavigationBarEvents.inputCreated, [e.id, t.el])
					}
				},
				_key: e.id,
				"aria-label": e.label || e.helpMessage || "type " + (e.placeholder ? "text like " + e.placeholder : "text")
			}), e.icon ? n.el(".dhx_input__icon", {
				class: e.icon,
				"aria-hidden": "true"
			}) : null])])
		}
	}, function (t, e, i) {
		"use strict";
		var r = this && this.__assign || function () {
			return (r = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s = i(0),
			a = i(30);
		e.menuItem = function (t, e, i) {
			var n, o = i ? " dhx_menu-button" : " dhx_nav-menu-button";
			return s.el("button", r({
				class: "dhx_button" + o + (t.disabled ? o + "--disabled" : "") + (t.active || t.$activeParent ? o + "--active" : ""),
				disabled: t.disabled,
				dhx_id: t.id,
				type: "button"
			}, (o = {
				role: "menuitem",
				"aria-disabled": (n = t).disabled ? "true" : "false"
			}, n.items && (o["aria-haspopup"] = "true"), o)), i ? [t.icon || t.value || t.html ? s.el("span.dhx_menu-button__block.dhx_menu-button__block--left", [t.icon && s.el("span.dhx_menu-button__icon", {
				class: t.icon
			}), t.html ? s.el("div.dhx_menu-button__text", {
				".innerHTML": t.html
			}) : t.value && s.el("span.dhx_menu-button__text", t.value)]) : null, 0 < t.count || t.hotkey || t.items ? s.el("span.dhx_menu-button__block.dhx_menu-button__block--right", [0 < t.count && a.getCount(t, " dhx_menu-button__count", !1), t.hotkey && s.el("span.dhx_menu-button__hotkey", t.hotkey), t.items && s.el("span.dhx_menu-button__caret.dxi.dxi-menu-right")]) : null] : [t.icon && s.el("span.dhx_menu-button__icon", {
				class: t.icon
			}), t.html ? s.el("div.dhx_menu-button__text", {
				".innerHTML": t.html
			}) : t.value && s.el("span.dhx_nav-menu-button__text", t.value)])
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.separator = function (t, e) {
			return null
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.spacer = function (t, e) {
			return null
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0);
		e.title = function (t, e) {
			return n.el("span", {
				class: "dhx_navbar-title dhx_navbar-title--" + e,
				dhx_id: t.id,
				".innerHTML": t.html,
				"aria-label": t.value || ""
			}, t.html ? null : t.value)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n, o, r, s = i(0),
			a = i(34),
			l = i(12),
			c = i(36);

		function u(t, e) {
			t.fire(c.NavigationBarEvents.inputBlur, [e])
		}

		function d(t, e, i) {
			s.awaitRedraw().then(function () {
				return r.show(i.target)
			}), t.fire(c.NavigationBarEvents.inputFocus, [e])
		}
		e.datePicker = function (e, i, t) {
			return n || (r = new l.Popup, o = new a.Calendar(null, {
				dateFormat: e.dateFormat || "%d/%m/%y",
				value: e.value,
				css: "dhx_widget--bordered"
			}), r.attach(o), o.events.on("change", function () {
				r.hide(), i.fire(c.NavigationBarEvents.inputChange, [e.id, o.getValue()])
			}), n = !0), s.el(".dhx_form-group.dhx_form-group--no-message-holder.dhx_form-group--label_sr.dhx_" + t + "__input", {
				style: {
					width: e.width || "200px"
				},
				role: "presentation"
			}, [e.label && s.el("label.dhx_label", {
				for: e.id
			}, e.label), s.el(".dhx_input__wrapper", {
				role: "presentation"
			}, [s.el("input.dhx_input", {
				placeholder: e.placeholder,
				class: e.icon ? "dhx_input--icon-padding" : "",
				value: o.getValue(),
				disabled: e.disabled,
				onblur: [u, i, e.id],
				onfocus: [d, i, e.id],
				dhx_id: e.id,
				readOnly: !0,
				_hooks: {
					didInsert: function (t) {
						i && i.fire(c.NavigationBarEvents.inputCreated, [e.id, t.el])
					}
				},
				_key: e.id,
				"aria-label": e.label || e.helpMessage || "type " + (e.placeholder ? "text like " + e.placeholder : "text")
			}), e.icon ? s.el(".dhx_input__icon", {
				class: e.icon,
				"aria-hidden": "true"
			}) : null])])
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(82),
			l = i(17),
			c = i(0),
			u = i(2),
			d = i(10),
			o = (s = a.Toolbar, o(h, s), h.prototype._draw = function (t) {
				var i = this,
					e = this.data.getLength() ? this.data.reduce(function (t, e) {
						switch (e.type) {
							case "title":
								return t || 20;
							case "button":
								return "small" === e.size && (!t || t <= 28) ? 28 : t || 32;
							default:
								return 32
						}
					}, 0) + 24 : null,
					t = [c.el("ul.dhx_navbar.dhx_navbar--horizontal", r(r({
						dhx_widget_id: this._uid,
						tabindex: 0
					}, {
						role: "toolbar",
						"aria-label": t || ""
					}), {
						onclick: this._handlers.onclick,
						onmousedown: this._handlers.onmousedown,
						oninput: this._handlers.input,
						onmouseover: this._handlers.tooltip,
						_hooks: {
							didInsert: function (t) {
								t.el.addEventListener("keyup", function (t) {
									var e;
									9 !== t.which || (e = u.locateNode(document.activeElement)) && (t = e.getAttribute("dhx_id"), (t = i.data.getItem(t)).tooltip && d.tooltip(t.tooltip, {
										node: e,
										position: d.Position.bottom,
										force: !0
									}))
								}, !0)
							}
						}
					}), this.data.map(function (t) {
						return i._factory(t)
					}, this.data.getRoot(), !1))];
				return c.el("nav.dhx_widget.dhx_toolbar", {
					style: {
						height: e
					},
					class: this.config.css || ""
				}, this.scrollView && this.scrollView.config.enable ? [].concat(this.scrollView.render(t)) : t)
			}, h);

		function h(t, e) {
			var i = s.call(this, t, e) || this;
			return i.scrollView = new l.ScrollView(function () {
				return i.getRootView()
			}), i
		}
		e.ProToolbar = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.layoutConfig = {
			css: "vault-layout",
			rows: [{
				id: "topbar",
				css: "vault-topbar"
			}, {
				id: "vault",
				css: "vault-file-grid"
			}]
		}, e.layoutConfigWithoutTopbar = {
			css: "vault-layout",
			rows: [{
				id: "vault",
				css: "vault-file-grid"
			}]
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(48),
			o = ["byte", "kilobyte", "megabyte", "gigabyte"];
		e.getBasis = function (t, e) {
			return void 0 === t && (t = 0), void 0 === e && (e = 0), t < 1024 ? t + " " + n.default[o[e]] : this.getBasis(Math.round(t / 1024), e + 1)
		};
		var r;

		function s(t) {
			return {
				extension: t.name.split(".").pop() || "none",
				mime: t.file ? t.file.type : ""
			}
		}

		function a(t, e) {
			switch (t) {
				case "jpg":
				case "jpeg":
				case "gif":
				case "png":
				case "bmp":
				case "tiff":
				case "pcx":
				case "svg":
				case "ico":
					return r.image;
				case "avi":
				case "mpg":
				case "mpeg":
				case "rm":
				case "move":
				case "mov":
				case "mkv":
				case "flv":
				case "f4v":
				case "mp4":
				case "3gp":
				case "wmv":
				case "webm":
				case "vob":
					return r.video;
				case "rar":
				case "zip":
				case "tar":
				case "tgz":
				case "arj":
				case "gzip":
				case "bzip2":
				case "7z":
				case "ace":
				case "apk":
				case "deb":
				case "zipx":
				case "cab":
				case "tar-gz":
				case "rpm":
				case "xar":
					return r.archive;
				case "xlr":
				case "xls":
				case "xlsm":
				case "xlsx":
				case "ods":
				case "csv":
				case "tsv":
					return r.table;
				case "doc":
				case "docx":
				case "docm":
				case "dot":
				case "dotx":
				case "odt":
				case "wpd":
				case "wps":
				case "pages":
					return r.document;
				case "wav":
				case "aiff":
				case "au":
				case "mp3":
				case "aac":
				case "wma":
				case "ogg":
				case "flac":
				case "ape":
				case "wv":
				case "m4a":
				case "mid":
				case "midi":
					return r.audio;
				case "pot":
				case "potm":
				case "potx":
				case "pps":
				case "ppsm":
				case "ppsx":
				case "ppt":
				case "pptx":
				case "pptm":
				case "odp":
					return r.presentation;
				case "html":
				case "htm":
				case "eml":
					return r.web;
				case "exe":
					return r.application;
				case "dmg":
					return r.apple;
				case "pdf":
				case "ps":
				case "eps":
					return r.pdf;
				case "psd":
					return r.psd;
				case "txt":
				case "djvu":
				case "nfo":
				case "xml":
					return r.text;
				default:
					switch (e.split("/")[0]) {
						case "image":
							return r.image;
						case "audio":
							return r.audio;
						case "video":
							return r.video;
						default:
							return r.other
					}
			}
		}
		e.truncateWord = function (t, e) {
			if (void 0 === e && (e = 13), t.length <= e) return t;
			var i, n = t.lastIndexOf(".");
			return (-1 === n ? (i = t.substr(t.length - 4), t.substr(0, e - 7)) : (n = n - 3, i = t.substr(n), t.substr(0, e - (t.length - n)))) + "..." + i
		}, e.calculateCover = function (t) {
			var e, i, n, o = t.width,
				r = t.height,
				o = 1 < (t = o / r) ? (e = i = r, n = (o - i) / 2, 0) : t < 1 ? (n = 0, (r - (e = i = o)) / 2) : (i = e = o, n = 0);
			return {
				sx: n,
				sy: o,
				sWidth: i,
				sHeight: e,
				dx: 0,
				dy: 0
			}
		}, (i = r = e.FileType || (e.FileType = {})).image = "image", i.video = "video", i.archive = "archive", i.table = "table", i.document = "document", i.presentation = "presentation", i.application = "application", i.web = "web", i.apple = "apple", i.pdf = "pdf", i.psd = "psd", i.audio = "audio", i.other = "other", i.text = "text", e.getFileType = a, e.getFileClassName = function (t) {
			var e = s(t),
				t = e.mime;
			return a(e = e.extension, t) + " extension-" + e
		}, e.isImage = function (t) {
			var e = s(t),
				t = e.mime;
			return a(e.extension, t) === r.image
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(0),
			a = i(4),
			l = i(48),
			c = i(35),
			o = (r = a.View, o(u, r), u.prototype.setState = function (t, e) {
				this._progress = t, this.config.template ? this._progressText = this.config.template(t, e) : this._progressText = this._progress.toFixed(1) + "%", this.paint()
			}, u.prototype._draw = function () {
				return s.el(".progress-bar", {
					_key: this._uid
				}, [s.el(".progress-indicator", {
					style: {
						width: this._progress + "%"
					}
				}), s.el(".progress-text", {
					".innerHTML": this._progressText
				}), s.el("button", {
					class: "dhx_btn dhx_btn--flat dhx_btn_small action-abort-all",
					onclick: this._abortUpload
				}, l.default.cancel)])
			}, u);

		function u(t, e) {
			var i = r.call(this, null, e) || this;
			i.events = t, i._progress = 0;
			return i.mount(null, s.create({
				render: function () {
					return i._draw()
				}
			})), i._abortUpload = function () {
				i.events.fire(c.ProgressBarEvents.cancel)
			}, i
		}
		e.ProgressBar = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = (o.prototype.add = function (t, e) {
			void 0 === e && (e = !1), this._readerStack.push(t), e || this.read()
		}, o.prototype.read = function () {
			var i, t, n = this;
			this._readerStack.length && !this._isActive && (i = this._readerStack.shift(), this._isActive = !0, (t = new FileReader).readAsDataURL(i.file), t.onload = function (t) {
				var e = new Image;
				e.src = t.target.result, e.onload = function () {
					n._data.exists(i.id) && n._data.update(i.id, {
						image: e
					}), n._isActive = !1, n.read()
				}
			}, t.onerror = function () {
				n._isActive = !1, n.read()
			})
		}, o.prototype.stop = function () {
			this._readerStack = []
		}, o);

		function o(t) {
			this._readerStack = [], this._isActive = !1, this._data = t
		}
		e.ReadStackPreview = n
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(3),
			c = i(2),
			u = i(4),
			d = i(1),
			h = i(50),
			f = i(83),
			p = i(51),
			_ = i(84),
			v = i(59),
			g = i(10),
			m = i(201),
			y = i(202),
			b = i(13),
			w = i(18),
			x = i(1),
			o = (s = u.View, o(E, s), E.prototype.destructor = function () {
				this.events && this.events.clear(), this.config = this.events = this._selected = this._handlers = this._pickerState = this._inputTimeout = null, this.unmount()
			}, E.prototype.clear = function () {
				this._selected = "", this.events.fire(_.ColorpickerEvents.change, [this._selected]), this.paint()
			}, E.prototype.setValue = function (t) {
				!this._focusColor(t) && w.focusManager.getFocusId() !== this._uid || (this.paint(), this.events.fire(_.ColorpickerEvents.change, [this._selected]), this.events.fire(_.ColorpickerEvents.colorChange, [this._selected]))
			}, E.prototype.setFocus = function (t) {
				this._focusColor(t) && this.paint()
			}, E.prototype.getValue = function () {
				return this._selected || ""
			}, E.prototype.getCustomColors = function () {
				return this.config.customColors
			}, E.prototype.setCustomColors = function (t) {
				this.config.customColors = t.map(function (t) {
					return t.toUpperCase()
				}), this.paint()
			}, E.prototype.setCurrentMode = function (t) {
				"palette" !== t && "picker" !== t || (this.config.mode = t, this.events.fire(_.ColorpickerEvents.modeChange, [t]), this.events.fire(_.ColorpickerEvents.viewChange, [t]), this.paint())
			}, E.prototype.getCurrentMode = function () {
				return this.config.mode
			}, E.prototype.getView = function () {
				return this.getCurrentMode()
			}, E.prototype.setView = function (t) {
				this.setCurrentMode(t)
			}, E.prototype.focusValue = function (t) {
				this.setFocus(t)
			}, E.prototype._setHandlers = function () {
				var i = this;
				this._handlers = {
					click: {
						".dhx_palette__cell": this._onColorClick
					},
					mousedown: function (t) {
						i._pickerMove(t)
					},
					touchstart: function (t) {
						i._pickerMove(t)
					},
					buttonsClick: function (t) {
						i.setCurrentMode("palette"), "cancel" !== t ? "apply" !== t || i.config.customColors.includes(i._pickerState.background) || (i.setValue(i._pickerState.background), i.events.fire(_.ColorpickerEvents.apply, []), i.events.fire(_.ColorpickerEvents.selectClick, [])) : i.events.fire(_.ColorpickerEvents.cancelClick, [])
					},
					customColorClick: function () {
						i.setView("picker")
					},
					oninput: function (e) {
						i._inputTimeout && clearTimeout(i._inputTimeout), i._inputTimeout = setTimeout(function () {
							var t = e.target.value; - 1 === t.indexOf("#") && (t = "#" + t), i._pickerState.customHex = t, h.isHex(t) && (i._pickerState.hsv = h.HexToHSV(t), i.paint())
						}, 100)
					},
					contextmenu: {
						".dhx_palette__cell": function (t, e) {
							t.preventDefault();
							e = i.config.customColors.indexOf(e.data.color); - 1 !== e && i._removeCustomColor(e), i.paint()
						}
					},
					mouseover: {
						".dhx_palette__cell": function (t) {
							t.target && v.tooltip(p.default.rightClickToDelete, {
								node: t.target,
								position: g.Position.bottom
							})
						},
						".dhx_colorpicker-custom-colors__picker": function (t) {
							t.target && v.tooltip(p.default.addNewColor, {
								node: t.target,
								position: g.Position.bottom
							})
						}
					}
				}, this.events.on(_.ColorpickerEvents.change, function () {
					i.paint()
				}), this.events.on(_.ColorpickerEvents.colorChange, function () {
					i.paint()
				})
			}, E.prototype._pickerMove = function (t) {
				var e = c.locate(t);
				this._pickerState.customHex = "", "picker_palette" === e ? this._setPaletteGrip(t) : this._setRangeGrip(t);
				var i = "picker_palette" === e ? this._setPaletteGrip : this._setRangeGrip,
					n = t.targetTouches ? "touchmove" : "mousemove",
					t = t.targetTouches ? "touchend" : "mouseup";
				document.addEventListener(n, i), document.addEventListener(t, function () {
					document.removeEventListener(n, i)
				}), this.paint()
			}, E.prototype._focusColor = function (t) {
				if (void 0 === t || t.length < 4) return !1;
				var i = t.toUpperCase();
				if (!h.isHex(i)) return !1;
				var e = this.config.palette.reduce(function (e, t) {
					return e || (t.forEach(function (t) {
						t.toUpperCase() === i && (e = !0)
					}), e)
				}, !1),
					t = f.grayShades.includes(i);
				return e || t || ((t = this.getCustomColors()).includes(i.toUpperCase()) || t.push(i.toUpperCase())), this._selected = i || null, this._pickerState.hsv = h.HexToHSV(i), !0
			}, E.prototype._removeCustomColor = function (t) {
				this.config.customColors.splice(t, 1)
			}, E.prototype._getCells = function (t, n) {
				var o = this;
				return void 0 === n && (n = ""), t.reduce(function (t, e) {
					var i = (o._selected || "").toUpperCase() === e.toUpperCase() ? "dhx_palette__cell--selected" : "";
					return t.push(a.el(".dhx_palette__cell", {
						class: i + " " + n,
						_data: {
							color: e
						},
						style: "background:" + e,
						tabindex: 0
					})), t
				}, [])
			}, E.prototype._getGrayShades = function () {
				return a.el(".dhx_palette__row", this._getCells(f.grayShades))
			}, E.prototype._getPalette = function () {
				var i = this;
				return this.config.palette.reduce(function (t, e) {
					return t.push(a.el(".dhx_palette__col", i._getCells(e))), t
				}, [])
			}, E.prototype._getContent = function () {
				var t = !this.config.pickerOnly && "palette" === this.config.mode ? r([this.config.grayShades && this._getGrayShades()], this._getPalette(), [!this.config.paletteOnly && a.el(".dhx_colorpicker-custom-colors", {
					onmouseover: this._handlers.mouseover
				}, [a.el(".dhx_colorpicker-custom-colors__header", [p.default.customColors]), a.el(".dhx_palette--custom.dhx_palette__row", r(this._getCells(this.config.customColors, "dhx_custom-color__cell"), [a.el(".dhx_colorpicker-custom-colors__picker", {
					class: "dxi dxi-plus",
					onclick: this._handlers.customColorClick,
					onmouseover: this._handlers.mouseover,
					tabindex: 0
				})]))])]) : [m.getPicker(this, this._pickerState, this._handlers)];
				return a.el(".dhx_widget.dhx_colorpicker", {
					class: this.config.css,
					style: {
						width: this.config.width
					},
					dhx_widget_id: this._uid
				}, [a.el(".dhx_palette", {
					onclick: this._handlers.click,
					oncontextmenu: this._handlers.contextmenu
				}, t)])
			}, E.prototype._initHotKey = function () {
				var t, e = this,
					i = {
						enter: function (t) {
							c.locateNodeByClassName(t, "dhx_palette__cell") && (e._selected = x.rgbToHex(t.target.style.background), e.events.fire(_.ColorpickerEvents.change, [e._selected]), e.events.fire(_.ColorpickerEvents.colorChange, [e._selected])), c.locateNodeByClassName(t, "dhx_colorpicker-custom-colors__picker") && e.setCurrentMode("picker")
						}
					};
				for (t in i) this._keyManager.addHotKey(t, i[t])
			}, E);

		function E(t, e) {
			var n = s.call(this, t, e) || this;
			n._setPaletteGrip = function (t) {
				var e = n.getRootView().refs.picker_palette.el.getBoundingClientRect(),
					i = (t.targetTouches ? t.targetTouches[0] : t).clientX,
					t = (t.targetTouches ? t.targetTouches[0] : t).clientY - e.top,
					i = i - e.left,
					t = y.calculatePaletteGrip(e, t, i),
					i = t.s,
					t = t.v;
				n._pickerState.hsv.s = i, n._pickerState.hsv.v = t, n.paint()
			}, n._setRangeGrip = function (t) {
				var e = n.getRootView().refs.hue_range.el.getBoundingClientRect(),
					t = (t.targetTouches ? t.targetTouches[0] : t).clientX - e.left,
					e = y.calculateRangeGrip(e, t),
					t = e.h,
					e = e.rangeLeft;
				n._pickerState.hsv.h = t, n._pickerState.rangeLeft = e, n.paint()
			}, n._onColorClick = function (t, e) {
				n._selected = e.data.color.toUpperCase(), n.events.fire(_.ColorpickerEvents.change, [n._selected]), n.events.fire(_.ColorpickerEvents.colorChange, [n._selected])
			}, n._container = t, n.config = d.extend({
				css: "",
				grayShades: !0,
				pickerOnly: !1,
				paletteOnly: !1,
				customColors: [],
				palette: f.palette,
				width: "238px",
				mode: "palette"
			}, n.config), n.config.palette || (n.config.palette = f.palette), n.config.customColors && (n.config.customColors = n.config.customColors.map(function (t) {
				return t.toUpperCase()
			})), n._pickerState = {
				hsv: {
					h: 0,
					s: 1,
					v: 1
				},
				customHex: ""
			}, n.events = new l.EventSystem(n), n._setHandlers(), n._keyManager = new b.KeyManager(function (t, e) {
				return e === n._uid
			}), n._initHotKey();
			t = a.create({
				render: function () {
					return n._getContent()
				}
			});
			return n.mount(n._container, t), n
		}
		e.Colorpicker = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var l = i(50),
			c = i(0),
			u = i(51);
		e.getPicker = function (t, e, i) {
			var n = l.HSVtoRGB(e.hsv);
			e.background = l.RGBToHex(n);
			var o = l.RGBToHex(l.HSVtoRGB({
				h: e.hsv.h,
				s: 1,
				v: 1
			})),
				r = t.getRootView(),
				s = (a = r.refs ? r.refs.picker_palette.el.getBoundingClientRect() : {
					height: 200,
					width: 218,
					x: 0,
					y: 0
				}).height - 2,
				n = a.width - 2,
				r = s - e.hsv.v * s - 4,
				s = e.hsv.s * n - 4,
				a = (n = a.width - 6) - (360 - e.hsv.h) / 360 * n,
				n = (l.isHex(e.customHex) ? e.customHex : e.background).replace("#", "");
			return c.el(".dhx_colorpicker-picker", {}, [c.el(".dhx_colorpicker-picker__palette", {
				style: {
					height: 132,
					background: o
				},
				onmousedown: i.mousedown,
				ontouchstart: i.touchstart,
				dhx_id: "picker_palette",
				_ref: "picker_palette"
			}, [c.el(".dhx_palette_grip", {
				style: {
					top: r,
					left: s
				},
				tabindex: 0
			})]), c.el(".dhx_colorpicker-hue-range", {
				style: {
					height: 16
				},
				onmousedown: i.mousedown,
				ontouchstart: i.touchstart,
				dhx_id: "hue_range",
				_key: "hue_range",
				_ref: "hue_range"
			}, [c.el(".dhx_colorpicker-hue-range__grip", {
				style: {
					left: a
				},
				tabindex: 0
			})]), c.el(".dhx_colorpicker-value", [c.el(".dhx_colorpicker-value__color", {
				style: {
					background: e.background
				}
			}), c.el(".dhx_colorpicker-value__input__wrapper", [c.el("input", {
				class: "dhx_colorpicker-value__input",
				value: n,
				oninput: i.oninput,
				maxlength: "7",
				_key: "hex_input",
				"aria-label": "type color in HEX format"
			})])]), c.el(".dhx_colorpicker-picker__buttons", [!t.config.pickerOnly && c.el("button", {
				class: "dhx_button dhx_button--size_medium dhx_button--view_link dhx_button--color_primary",
				onclick: [i.buttonsClick, "cancel"]
			}, u.default.cancel), c.el("button", {
				class: "dhx_button dhx_button--size_medium dhx_button--view_flat dhx_button--color_primary",
				onclick: [i.buttonsClick, "apply"]
			}, u.default.select)])])
		}, e.calculatePaletteGrip = function (t, e, i) {
			var t = (n = t.refs.picker_palette.el.getBoundingClientRect()).height,
				n = n.width;
			e = e < 0 ? 0 : t < e ? t : e, i = i < 0 ? 0 : n < i ? n : i, n = Math.round(i / (n / 100)), t = 100 - Math.round(e / (t / 100)), this._pickerState.hsv.s = n / 100, this._pickerState.hsv.v = t / 100
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.calculatePaletteGrip = function (t, e, i) {
			var n = t.height,
				t = t.width;
			return e = e < 0 ? 0 : n < e ? n : e, i = i < 0 ? 0 : t < i ? t : i, {
				s: Math.round(i / (t / 100)) / 100,
				v: (100 - Math.round(e / (n / 100))) / 100
			}
		}, e.calculateRangeGrip = function (t, e) {
			return t = t.width, e = e < 0 ? 0 : t < e ? t : e, {
				h: Math.round(e / t * 360),
				rangeLeft: e
			}
		}
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(204)), n(e(31))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			a = this && this.__assign || function () {
				return (a = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(0),
			l = i(3),
			c = i(4),
			u = i(6),
			h = i(205),
			d = i(207),
			f = i(31),
			p = i(209),
			_ = i(213),
			v = i(222),
			g = i(7),
			m = i(223),
			o = (r = c.View, o(y, r), y.prototype.getSeries = function (t) {
				return this._series[t]
			}, y.prototype.eachSeries = function (t) {
				var e, i = [];
				for (e in this._series) i.push(t.call(this, this._series[e]));
				return i
			}, y.prototype.destructor = function () {
				this._tooltip.destructor(), this.events.clear(), this.unmount()
			}, y.prototype.setConfig = function (e) {
				var o, r = this;
				if (this.config = e, this._layers.clear(), this._series = {}, this._scales = {}, e.scales)
					for (var t in e.scales) {
						var i = a({}, e.scales[t]);
						void 0 !== e.scales[t].min && (o = e.scales[t].min), i.type = i.type || this._detectScaleType(i, t), e.scales.radial && "radial" !== t && (i.hidden = !0), this._setScale(i, t)
					}
				var n, s = new v.default;
				this._layers.add(s), e.series.forEach(function (t) {
					void 0 !== t.baseLine && t.baseLine < o && (t.baseLine = void 0);
					var i = a({}, t);
					i.type = i.type || e.type;
					t = _.default[i.type];
					(i.barWidth || r.config.barWidth) && (i.barWidth = i.barWidth || r.config.barWidth);
					var n = new t(r.data, i, r.events),
						t = g.getScales(e.scales);
					(1 < t.length && "radial" !== t[0] || "radial" === t[0] ? t : ["bottom", "left"]).forEach(function (t) {
						var e = r._scales[t];
						e && (n.addScale(t, e), i.stacked ? e.add(s) : e.add(n))
					}), r._series[n.id] = n, (i.stacked ? s : r._layers).add(n)
				}), e.legend && ((n = a({}, e.legend)).series && (n.$seriesInfo = n.series.map(function (t) {
					return r._series[t]
				})), n = new d.Legend(this.data, n, this.events), this._layers.add(n)), this.paint()
			}, y.prototype._setScale = function (t, e) {
				t = new p.default[t.type](this.data, t, e);
				"radial" !== t.config.type && this._layers.add(t.scaleGrid()), this._layers.add(t), this._scales[e] = t
			}, y.prototype._detectScaleType = function (t, e) {
				return "radial" === e ? e : t.text ? "text" : "numeric"
			}, y.prototype._initEvents = function () {
				var i = this;
				this.events.on(f.ChartEvents.toggleSeries, function (t, e) {
					e ? (e = i._series[Object.keys(i._series)[0]]) && (e.toggle(t), i.paint()) : i._series[t] && (i._series[t].toggle(), i.paint())
				}, this), this.events.on(u.DataEvents.change, function () {
					return i.paint()
				})
			}, y);

		function y(t, i) {
			void 0 === i && (i = {});
			var d = r.call(this, null, i) || this;
			d._width = 0, d._height = 0, d._left = 0, d._top = 0;
			var e = {};
			i.maxPoints && (e.approximate = {
				value: i.series.map(function (t) {
					return t.value
				}),
				maxNum: i.maxPoints
			}), Array.isArray(i.data) ? (d.events = new l.EventSystem(d), d.data = new u.DataCollection(e, d.events), d.data.parse(i.data)) : i.data && i.data.events ? (d.data = i.data, d.events = d.data.events, d.events.context = d) : (d.events = new l.EventSystem(d), d.data = new u.DataCollection(e, d.events)), d._globalHTMLHandlers = {
				onmousemove: function (t) {
					var e = d._layers.getSizes(),
						i = e.left,
						n = e.top,
						o = e.bottom,
						r = e.right,
						s = t.pageX,
						e = t.pageY,
						t = d.getRootView().node.el.getBoundingClientRect();
					d._left = t.left + window.pageXOffset, d._top = t.top + window.pageYOffset;
					s = s - i - d._left, e = e - n - d._top;
					0 <= s && s <= d._width - r - i && 0 <= e && e <= d._height - o - n ? d.events.fire(f.ChartEvents.chartMouseMove, [s, e, d._left + i, d._top + n]) : d.events.fire(f.ChartEvents.chartMouseLeave)
				},
				onmouseleave: function () {
					return d.events.fire(f.ChartEvents.chartMouseLeave)
				}
			}, d._layers = new h.ComposeLayer, d.setConfig(i), d._initEvents(), d._tooltip = new m.Tooltip(d);
			e = s.create({
				render: function () {
					if (!d.data.getLength()) return s.el("div");

					function t(t) {
						function o(t) {
							return t._isXDirection ? "X" : "Y"
						}

						function r(t) {
							return t.title || t.text || t.value || ""
						}

						function s(t) {
							return t.steps && t.steps.length ? t.steps[0] : t.min || 0
						}

						function a(t) {
							return t.steps && t.steps.length ? t.steps[t.steps.length - 1] : t.max || t.maxPoints || "max"
						}

						function l(t, e) {
							return e[t]
						}
						var c = d._scales,
							i = t.series,
							n = t.type,
							u = ((n = n || (i && i.length && i[0] && i[0].type || "")) || "") + " chart.";
						return c && Object.keys(c).forEach(function (t) {
							var e, i = c[t],
								n = i._axis || i.config;
							"radial" === t ? (e = i._data._order, u += " X scale " + r(i.config) + ": " + t + " axis from " + l(i.config.value, e[0]) + " to " + l(i.config.value, e[e.length - 1]) + ".", u += " " + o(i) + " scale: axis from " + s(n) + " to " + a(n) + ".") : u += " " + o(i) + " scale " + r(i.config) + ": " + t + " axis from " + s(n) + " to " + a(n) + "."
						}), i && i.length && (u += " Series:", i.forEach(function (t, e) {
							u += " " + ("pie" === n || "pie3D" === n || "donut" === n ? t.text : t.value), u += e === i.length - 1 ? "." : ","
						})), u
					}
					var e = [s.resizer(function (t, e) {
						d._width = t, d._height = e || 400;
						e = d.getRootView();
						e && e.node && e.node.el && (e = e.node.el.getBoundingClientRect(), d._left = e.left + window.pageXOffset, d._top = e.top + window.pageYOffset), d.events.fire(f.ChartEvents.resize, [d._width, d._height]), d.paint(), !document.querySelector(".dhx_widget.dhx_chart") && d._tooltip && d._tooltip.destructor()
					})];
					return d._width && d._height && e.push(d._layers.toVDOM(d._width, d._height, d.events)), s.el(".dhx_widget.dhx_chart", a(a({
						class: i.css || "",
						onmousemove: d._globalHTMLHandlers.onmousemove,
						onmouseleave: d._globalHTMLHandlers.onmouseleave
					}, {
						"aria-label": t(i)
					}), {
						tabIndex: 0
					}), e)
				},
				hooks: {
					didMount: function (t) {
						t && t.node && t.node.parent && t.node.parent.el && (d._width = t.node.parent.el.offsetWidth, d._height = t.node.parent.el.offsetHeight || 400, d.paint())
					}
				}
			});
			return d.mount(t, e), d
		}
		e.Chart = o
	}, function (t, e, i) {
		"use strict";
		var c = this && this.__spreadArrays || function () {
			for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
			for (var n = Array(t), o = 0, e = 0; e < i; e++)
				for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
			return n
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var u = i(0),
			d = i(206),
			i = (n.prototype.add = function (t) {
				this._data.push(t)
			}, n.prototype.clear = function () {
				this._data.forEach(function (t) {
					return t.destructor && t.destructor()
				}), this._data = []
			}, n.prototype.getSizes = function () {
				return this._sizes
			}, n.prototype.toVDOM = function (e, i) {
				var n = {
					left: 20,
					right: 20,
					top: 10,
					bottom: 10
				},
					t = this._data.filter(function (t) {
						return !t.dataReady || t.dataReady().length
					});
				this._data.forEach(function (t) {
					return !t.scaleReady || t.scaleReady(n)
				});
				var o = 0,
					r = 0;
				t.forEach(function (t) {
					t.seriesShift && (o += t.seriesShift(), r++)
				});
				var s = o / r,
					o = r ? (s - o) / 2 : 0;
				t.forEach(function (t) {
					t.seriesShift && (t.seriesShift(o), o += s)
				}), this._sizes = n;
				var a = e - n.left - n.right,
					l = i - n.top - n.bottom,
					l = u.sv("g", {
						transform: "translate(" + n.left + ", " + n.top + ")"
					}, c([u.sv("rect.dhx_chart-graph_area", {
						width: 0 < a ? a : 0,
						height: 0 < l ? l : 0,
						fill: "transparent"
					})], t.map(function (t) {
						return t.paint(e - (n.left + n.right), i - (n.top + n.bottom))
					}))),
					t = u.sv("defs", [d.dropShadow(), d.shadow()]);
				return u.sv("svg", {
					width: e,
					height: i,
					role: "graphics-document"
				}, [t, l])
			}, n);

		function n() {
			this._data = [], this._sizes = {
				left: 20,
				right: 20,
				top: 10,
				bottom: 10
			}
		}
		e.ComposeLayer = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(0);
		e.shadow = function () {
			return n.sv("filter", {
				id: "shadow"
			}, [n.sv("feDiffuseLighting", {
				in: "SourceGraphic",
				result: "light",
				"lighting-color": "white"
			}, [n.sv("feDistantLight", {
				azimuth: 90,
				elevation: 25
			})]), n.sv("feComposite", {
				in: "SourceGraphic",
				in2: "light",
				operator: "arithmetic",
				k1: "1",
				k2: "0",
				k3: "0",
				k4: "0"
			})])
		}, e.dropShadow = function () {
			return n.sv("filter", {
				id: "dropshadow",
				x: "-100%",
				y: "-100%",
				width: "300%",
				height: "300%"
			}, [n.sv("feGaussianBlur", {
				in: "SourceAlpha",
				stdDeviation: 2
			}), n.sv("feOffset", {
				dx: 0,
				dy: 0,
				result: "offsetblur"
			}), n.sv("feOffset", {
				dx: 0,
				dy: 0,
				result: "offsetblur"
			}), n.sv("feFlood", {
				"flood-color": "rgba(85,85,85,0.5)"
			}), n.sv("feComposite", {
				in2: "offsetblur",
				operator: "in"
			}), n.sv("feMerge", [n.sv("feMergeNode"), n.sv("feMergeNode", {
				in: "SourceGraphic"
			})])])
		}
	}, function (t, e, i) {
		"use strict";
		var _ = this && this.__assign || function () {
			return (_ = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(31),
			v = i(0),
			g = i(7),
			m = i(208);
		n.prototype.scaleReady = function (t) {
			"middle" === this.config.valign ? "right" === this.config.halign ? t.right += this.config.size || 200 : "left" === this.config.halign && (t.left += this.config.size || 200) : "top" === this.config.valign ? t.top += this.config.size || 40 : "bottom" === this.config.valign && (t.bottom += this.config.size || 40)
		}, n.prototype.paint = function (n, t) {
			var e, i, o = this,
				r = this._getData(),
				s = this.config,
				a = g.getFontStyle("legend-text"),
				l = s.margin,
				c = s.itemPadding,
				u = [],
				d = "middle" === s.valign,
				h = 0,
				f = 0,
				p = 0;
			switch (r.forEach(function (t, e) {
				var i;
				d || "top" !== s.valign && "bottom" !== s.valign || (i = g.getTextWidth(t.text, a), n < h + i + 15 && 0 !== e && (h = 0, f += c + 2)), u.push(v.sv("g", _(_({
					transform: "translate(" + h + "," + f + ")",
					onclick: [o._handlers.onclick, t.id, o.config.values],
					onkeyup: [o._handlers.onkeyup, t.id, o.config.values],
					class: "legend-item " + (t.active ? "" : "not-active")
				}, {
					role: "button",
					"aria-label": (e = t).active ? "Hide chart " + e.text : "Show chart " + e.text
				}), {
					tabindex: 0
				}), [v.sv("text", {
					x: 10,
					y: 0,
					class: "start-text legend-text"
				}, [g.verticalCenteredText(t.text)]), m.legendShape(s.form, t)])), d ? f += c + 2 : (i = g.getTextWidth(t.text, a), p = (h += i + c + 15) < p ? p : h)
			}), s.valign) {
				case "top":
					i = -l - f - 5;
					break;
				case "middle":
					i = (t - f) / 2 + c / 2;
					break;
				case "bottom":
					i = t + l
			}
			switch (s.halign) {
				case "left":
					e = d ? -l : 5;
					break;
				case "center":
					e = (n - p) / 2;
					break;
				case "right":
					e = d ? n + l + 5 : n - p + c + 5
			}
			return v.sv("g", {
				transform: "translate(" + e + ", " + i + ")",
				"aria-label": "Legend",
				tabindex: 0
			}, u)
		}, n.prototype._getData = function () {
			var i = [];
			if (this.config.values) {
				var n = g.locator(this.config.values.text),
					o = g.locator(this.config.values.color);
				this._data.map(function (t, e) {
					i.push({
						id: t.id,
						text: n(t).toString(),
						alpha: 1,
						fill: o(t).toString() || g.getDefaultColor(e),
						active: !t.$hidden
					})
				})
			} else
				for (var t = 0, e = this.config.$seriesInfo; t < e.length; t++) {
					var r = e[t].config,
						s = r.fill && r.color;
					i.push({
						id: r.id,
						text: r.name || r.value,
						alpha: r.alpha,
						fill: r.fill || r.color,
						color: s && r.color,
						active: r.active
					})
				}
			return i
		}, i = n;

		function n(t, e, i) {
			var n = this;
			this._data = t, this._events = i;
			this.config = _(_({}, {
				form: "rect",
				itemPadding: 20,
				halign: "right",
				valign: "middle"
			}), e), this.config.margin = e.margin || function (t, e) {
				switch (e) {
					case "middle":
						switch (t) {
							case "right":
								return 60;
							case "left":
								return 120;
							case "center":
								throw new Error("cant place legend on center, middle")
						}
					case "top":
						return 20;
					case "bottom":
						return 60
				}
			}(this.config.halign, this.config.valign), this._handlers = {
				onclick: function (t, e) {
					return n._events.fire(o.ChartEvents.toggleSeries, [t, e])
				},
				onkeyup: function (t, e, i) {
					i.preventDefault(), "Enter" !== i.key && " " !== i.key || n._events.fire(o.ChartEvents.toggleSeries, [t, e])
				}
			}
		}
		e.Legend = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(1),
			o = i(0),
			r = {
				circle: function (t, e, i) {
					return o.sv("circle", {
						id: n.uid(),
						r: 5,
						fill: e,
						class: "figure " + ("none" !== t ? "with-stroke" : ""),
						"stroke-width": 2,
						"fill-opacity": i,
						stroke: t,
						transform: "translate(0, -1)"
					})
				},
				rect: function (t, e, i) {
					return o.sv("rect", {
						id: n.uid(),
						fill: e,
						"fill-opacity": i,
						width: 10,
						"stroke-width": 2,
						height: 10,
						class: "figure " + ("none" !== t ? "with-stroke" : ""),
						stroke: t,
						transform: "translate(-5, -5)"
					})
				}
			};
		e.legendShape = function (t, e) {
			return "string" == typeof t && (t = r[t]), t(e.color || "none", e.fill, e.alpha || 1)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(210),
			o = i(52),
			i = i(212),
			o = {
				radial: n.RadialScale,
				text: i.TextScale,
				numeric: o.Scale
			};
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, a = i(32),
			i = i(52),
			o = (r = i.Scale, o(s, r), s.prototype.paint = function (t, e) {
				var i = this;
				if (this.config.hidden) return null;
				var n = this.config.zebra,
					o = this.config.value,
					r = this.config.showAxis ? this._axis.steps : null,
					s = this._axis.steps.map(function (t) {
						return i.point(t)
					}),
					n = {
						scales: this._data.map(function (t) {
							return t[o]
						}),
						axis: s,
						realAxis: r,
						zebra: n,
						attribute: o
					};
				return a.radarScale(n, t, e)
			}, s.prototype.point = function (t) {
				return (t - this._axis.min) / (this._axis.max - this._axis.min)
			}, s);

		function s(t, e) {
			return r.call(this, t, e, "radial") || this
		}
		e.RadialScale = o
	}, function (t, e, i) {
		"use strict";
		var s = this && this.__assign || function () {
			return (s = Object.assign || function (t) {
				for (var e, i = 1, n = arguments.length; i < n; i++)
					for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
				return t
			}).apply(this, arguments)
		},
			n = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(7),
			r = [1, 2, 3, 5, 10],
			i = (a.prototype.getScale = function () {
				var t = this.config.log ? this._logSteps() : this._calculateSteps(this._getStep());
				return {
					min: t[0],
					max: t[t.length - 1],
					steps: t
				}
			}, a.prototype._getStep = function () {
				var t = this.config.maxTicks,
					e = this.config.max - this.config.min,
					i = Math.floor(o.log10(e / t)),
					i = Math.pow(10, i),
					t = e / i / t;
				return r[n(r, [t]).sort(function (t, e) {
					return t - e
				}).indexOf(t)] * i
			}, a.prototype._calculateSteps = function (t) {
				for (var e = Math.floor(this.config.min / t), i = Math.ceil(this.config.max / t), n = [], o = e; o <= i; o++) {
					var r = t * o;
					Math.floor(r) !== r && (r = parseFloat(r.toFixed(8))), n.push(r)
				}
				return n
			}, a.prototype._logSteps = function () {
				var t = [];
				if (this.config.min < 0) {
					for (var e = Math.ceil(o.log10(-this.config.min)); 0 < e; e--) t.push(-Math.pow(10, e));
					t.push(0)
				}
				if (0 < this.config.max)
					for (var i = Math.ceil(o.log10(this.config.max)), e = 0; e <= i; e++) t.push(Math.pow(10, e));
				return t
			}, a.prototype._addPadding = function () {
				this.config.min -= this.config.padding
			}, a);

		function a(t, e) {
			void 0 === e && (e = {}), this._data = t;
			for (var i = [], n = 0, o = this._data; n < o.length; n++) {
				var r = o[n];
				"number" == typeof r && i.push(r)
			}
			t = {
				min: Math.min.apply(Math, i),
				max: Math.max.apply(Math, i),
				maxTicks: this._data.length < 20 ? this._data.length : 20
			};
			this.config = s(s({}, t), e), this.config.padding && this._addPadding()
		}
		e.AxisCreator = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(7),
			l = i(52),
			i = i(85),
			c = {
				left: i.left,
				right: i.right,
				bottom: i.bottom,
				top: i.top
			},
			d = {
				left: i.leftGrid,
				right: i.rightGrid,
				bottom: i.bottomGrid,
				top: i.topGrid
			},
			o = (s = l.Scale, o(u, s), u.prototype.scaleReady = function (t) {
				var e = this._data.getLength() - 1,
					i = this._data.map(this.locator);
				this._axis = {
					max: e,
					steps: i
				}, t[this._position] += this.config.size
			}, u.prototype.point = function (t) {
				var e = this._axis.steps.indexOf(t);
				if (this._padding) {
					var i = this._axis.max + 1,
						t = .5 / i,
						i = e / i;
					return this._isXDirection ? t + i : 1 - t - i
				}
				return this._isXDirection ? e / this._axis.max : 1 - e / this._axis.max
			}, u.prototype.paint = function (i, n) {
				var o = this;
				if (this.config.hidden) return null;
				var t = this._axis.steps.map(function (t, e) {
					return [o._isXDirection ? o._getAxisPoint(e) * i : o.point(t) * n, t]
				});
				return c[this._position](t, this.config, i, n)
			}, u.prototype.scaleGrid = function () {
				var s = this,
					a = this._position,
					l = this.config.grid,
					c = this.config.dashed,
					u = this.config.hidden;
				return {
					paint: function (t, e) {
						var i, n, o = s._axis.steps.indexOf(s.config.targetLine),
							r = (i = t, n = e, s._axis.steps.map(function (t, e) {
								return [s._isXDirection ? s._getAxisPoint(e) * i : s._getAxisPoint(e) * n, t]
							})),
							o = {
								targetLine: o,
								dashed: c,
								grid: l,
								hidden: u
							};
						return d[a](r, t, e, o)
					}
				}
			}, u.prototype._setDefaults = function (t) {
				this.locator = a.locator(t.text), this.config = r(r({}, {
					scalePadding: 30,
					textPadding: 12,
					grid: !0,
					targetLine: null,
					showText: !0
				}), t)
			}, u.prototype._getAxisPoint = function (t) {
				var e = this._axis.max;
				if (this._padding) {
					var i = e + 1,
						n = .5 / i,
						i = t / i;
					return this._isXDirection ? n + i : 1 - n - i
				}
				return this._isXDirection ? t / e : 1 - t / e
			}, u);

		function u() {
			return null !== s && s.apply(this, arguments) || this
		}
		e.TextScale = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(86),
			o = i(88),
			r = i(214),
			s = i(215),
			a = i(56),
			l = i(216),
			c = i(217),
			u = i(218),
			d = i(219),
			h = i(220),
			i = i(221),
			r = {
				line: a.default,
				spline: h.default,
				area: n.default,
				splineArea: i.default,
				scatter: d.default,
				pie: l.default,
				pie3D: c.default,
				donut: s.default,
				radar: u.default,
				bar: o.default,
				xbar: r.default
			};
		e.default = r
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, i = i(88),
			o = (r = i.default, o(s, r), s.prototype.addScale = function (t, e) {
				t = "top" === t || "bottom" === t ? "left" : "top";
				r.prototype.addScale.call(this, t, e)
			}, s.prototype.paint = function (t, e, i) {
				return r.prototype.paint.call(this, e, t, i)
			}, s.prototype.getTooltipType = function (t, e, i) {
				return void 0 !== this.config.baseLine && this._baseLinePosition > e ? "left" : "right"
			}, s.prototype.getClosest = function (t, e) {
				for (var i = [1 / 0, null, null, null], n = 0, o = this._points; n < o.length; n++) {
					var r = o[n],
						s = this._getClosestDist(t, e, r[1], r[0]);
					i[0] > s && (i[0] = s, i[1] = r[1], i[2] = r[0], i[3] = r[2])
				}
				return i
			}, s.prototype._getText = function (t) {
				return t[4].toString()
			}, s.prototype._getClosestDist = function (t, e, i, n) {
				return this.config.stacked && i < t ? 1 / 0 : Math.abs(e - n)
			}, s.prototype._path = function (t, e) {
				return t[0] += this._shift, "\nM " + e + " " + (t[0] - this.config.barWidth / 2) + "\nH " + t[1] + "\nv " + this.config.barWidth + "\nH " + e
			}, s.prototype._base = function (t) {
				var e = this.config.baseLine;
				return this._baseLinePosition = void 0 !== e ? this.yScale.point(e) * t : 0
			}, s.prototype._text = function (t, e, i) {
				e = (e + t[1]) / 2, t = t[0];
				return {
					x: e,
					y: t,
					class: "bar-text",
					transform: i && !isNaN(i) ? "rotate(" + i + " " + e + " " + t + ")" : ""
				}
			}, s);

		function s() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			B = this && this.__assign || function () {
				return (B = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, G = i(0),
			q = i(32),
			Y = i(7),
			i = i(55),
			o = (r = i.default, o(s, r), s.prototype.paint = function (V, D) {
				function T(t, e, i) {
					return void 0 === i && (i = 0), {
						role: "graphics-symbol",
						"aria-roledescription": "piece of donut",
						"aria-label": (e || "") + ", " + t + " (" + (i = i, Math.round(100 * (100 * i + Number.EPSILON)) / 100) + "%)"
					}
				}
				var H = this,
					t = this.config,
					F = t.stroke,
					e = t.strokeWidth,
					j = t.useLines,
					L = t.subType,
					R = !e || e < 1 ? 4 : 15 < e ? 15 : e,
					A = V < D ? V / 2 : D / 2,
					$ = -.25,
					N = [],
					z = [],
					W = [];
				return 1 < this._points.length && F && N.push(G.sv("circle", {
					cx: 0,
					cy: 0,
					r: A - .5,
					fill: F
				})), this._points.forEach(function (t, e) {
					var i = [],
						n = t[0],
						o = t[1],
						r = t[2],
						s = t[3],
						a = t[4],
						l = 0 === n || 1 === n ? -1e-6 : 0,
						c = q.getCoordinates($, A, A, 1 < H._points.length && F ? R / 2 : null),
						u = c[0],
						d = c[1],
						h = $ + n / 2;
					$ += n + l;
					var f = q.getCoordinates($, A, A, 1 < H._points.length && F ? -R / 2 : null),
						p = f[0],
						_ = f[1],
						v = .5 < n ? 1 : 0,
						g = q.getCoordinates(h, A, A),
						m = -.25 < h && h < .25,
						y = .5 < h || h < 0,
						b = h < .25 ? 5 : -5,
						c = [5, 30],
						l = c[0],
						w = c[1],
						f = [q.getCoordinates(h, A + l, A + l), q.getCoordinates(h, A + w, A + w)],
						c = f[0],
						x = f[1];
					switch (H.config.subType) {
						case "basic":
							var E = m ? "donut-value-title start-text" : "donut-value-title end-text",
								C = y ? -20 : 10,
								k = q.getCoordinates(h, A + 10, A + 10),
								S = m ? "donut-value start-text" : "donut-value end-text",
								I = {
									text1: {
										x: (j ? x : k)[0],
										y: (j ? x : k)[1] + C,
										width: 0,
										height: 0,
										class: ""
									},
									text2: {
										x: (j ? x : k)[0],
										y: (j ? x : k)[1] + C + 16,
										width: 0,
										height: 0,
										class: ""
									},
									changeSector: !1,
									line: b,
									right: m,
									dy: C
								},
								P = G.sv("text", B(B({
									x: (j ? x : k)[0],
									y: (j ? x : k)[1] + C,
									class: E
								}, T(o, s, n)), {
									tabindex: 0
								}), [Y.verticalCenteredText(s.toString())]),
								E = G.sv("text", {
									x: (j ? x : k)[0],
									y: (j ? x : k)[1] + C + 16,
									class: S,
									"aria-hidden": "true"
								}, [Y.verticalCenteredText(o.toString())]),
								S = Y.getSizesSVGText(s.toString(), {
									font: "normal 14px Roboto",
									lineHeight: 14
								});
							I.text1.width = S[0], I.text1.height = S[1], S = Y.getSizesSVGText(o.toString(), {
								font: "normal 12px Roboto",
								lineHeight: 12
							}), I.text2.width = S[0], I.text2.height = S[1];
							var O = j ? A + w : A + 10;
							0 !== z.length && (m ? (z.forEach(function (t) {
								Y.checkPositions(I.text1, t.text1, O, O, I), Y.checkPositions(I.text1, t.text2, O, O, I)
							}), I.text1.class && (I.text2.class = I.text1.class)) : (z.forEach(function (t) {
								Y.checkPositions(I.text2, t.text2, O, O, I), Y.checkPositions(I.text2, t.text1, O, O, I)
							}), I.text2.class && (I.text1.class = I.text2.class)), P.attrs.x = I.text1.x, P.attrs.y = I.text1.y, E.attrs.x = I.text2.x, E.attrs.y = I.text2.y, (I.text1.class || I.text2.class) && (P.attrs.class = I.text1.class, E.attrs.class = I.text2.class), b = I.line, j ? (x[0] = I.text1.x, x[1] = I.text1.y - C) : (k[0] = I.text1.x, k[1] = I.text1.y - C)), z.push(I);
							k = G.sv("text", B({
								x: 7 * g[0] / 9,
								y: 7 * g[1] / 9,
								class: "pie-inner-value"
							}, T(o, s, n)), [Y.verticalCenteredText(Math.round(100 * n) + "%")]);
							i.push(G.sv("g", {
								id: r + "-text",
								class: "chart donut"
							}, [P, E])), i.push(k);
							break;
						case "valueOnly":
							var M = G.sv("text", B(B({
								x: 7 * g[0] / 9,
								y: 7 * g[1] / 9,
								class: "pie-inner-value"
							}, T(o, s, n)), {
								tabindex: 0
							}), [Y.verticalCenteredText("" + o)]);
							i.push(M);
							break;
						case "percentOnly":
							M = G.sv("text", B(B({
								x: 7 * g[0] / 9,
								y: 7 * g[1] / 9,
								class: "pie-inner-value"
							}, T(o, s, n)), {
								tabindex: 0
							}), [Y.verticalCenteredText(Math.round(100 * n) + "%")]);
							i.push(M)
					}
					j && "basic" === L && (C = y ? 3 : 0, i.push(G.sv("path", {
						d: "M" + c[0] + " " + c[1] + " L" + x[0] + "\n\t\t\t\t\t\t\t" + (x[1] + C) + " h " + b,
						id: r + "-connector",
						class: "pie-value-connector chart donut"
					})));
					l = 0, f = 0, c = R / (2 * Math.sin(Math.PI / H._points.length));
					1 < H._points.length && F && (l = (c = q.getCoordinates(h, c, c))[0], f = c[1]);
					_ = "M " + u + " " + d + " A " + A + " " + A + " 0 " + v + " 1 " + p + " " + _ + "\n\t\t\t\tL " + (0 + l) + " " + (0 + f), l = q.getCoordinates(h, 4, 4), f = l[0], l = l[1], l = G.sv("path", {
						d: _,
						_key: r,
						fill: a,
						class: "chart donut",
						onclick: [H._handlers.onclick, t[1], t[2]],
						onmouseout: [q.pieLikeHandlers.onmouseout],
						onmouseover: [q.pieLikeHandlers.onmouseover, f, l],
						role: "presentation"
					});
					i.unshift(l), N.push(G.sv("g", {
						id: r
					}, i)), 1 === H._points.length ? W.push([V / 2, D / 2]) : W.push([.8 * g[0] + V / 2, .8 * g[1] + D / 2])
				}), this._center = [V / 2, D / 2], this._tooltipData = W, (N = N.concat([])).push(G.sv("circle", {
					cx: 0,
					cy: 0,
					r: 5 * A / 9,
					fill: "#FFFFFF",
					role: "presentation"
				})), G.sv("g", B(B({
					transform: "translate(" + V / 2 + ", " + D / 2 + ")"
				}, {
					"aria-label": "chart " + (this.config.text || "")
				}), {
					tabindex: 0
				}), N)
			}, s);

		function s() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			X = this && this.__assign || function () {
				return (X = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, K = i(1),
			J = i(0),
			Z = i(32),
			Q = i(7),
			i = i(55),
			o = (r = i.default, o(s, r), s.prototype.paint = function (D, T) {
				function H(t, e, i) {
					return {
						role: "graphics-symbol",
						"aria-roledescription": "piece of pie",
						"aria-label": (e || "") + ", " + t + " (" + (i = i, Math.round(100 * (100 * i + Number.EPSILON)) / 100) + "%)"
					}
				}
				var F = this,
					t = this.config,
					j = t.stroke,
					e = t.strokeWidth,
					L = t.gradient,
					R = t.useLines,
					A = t.showText,
					$ = t.showTextTemplate,
					N = t.subType,
					z = !e || e < 1 ? 4 : 15 < e ? 15 : e,
					W = D < T ? D / 2 : T / 2,
					B = -.25,
					G = [],
					q = [],
					e = [],
					Y = [],
					U = [];
				return 1 < this._points.length && j && e.push(J.sv("circle", {
					cx: 0,
					cy: 0,
					r: W - .5,
					fill: j
				})), this._points.forEach(function (t, e) {
					var i = [],
						n = t[0],
						o = t[1],
						r = t[2],
						s = t[3],
						a = t[4],
						l = 0 === n || 1 === n ? -1e-6 : 0,
						c = a;
					L && (_ = L(a), v = "gradient" + K.uid(), g = Q.getRadialGradient(_.options, _.stops, v), c = "url(#" + v + ")", Y.push(g));
					var u = Z.getCoordinates(B, W, W, 1 < F._points.length && j ? z / 2 : null),
						d = u[0],
						a = u[1],
						h = B + n / 2,
						f = h < .25 ? 5 : -5,
						p = Z.getCoordinates(h, W, W);
					B += n + l;
					var _ = Z.getCoordinates(B, W, W, 1 < F._points.length && j ? -z / 2 : null),
						v = _[0],
						g = _[1],
						u = .5 < n ? 1 : 0,
						l = [5, 30],
						_ = l[0],
						m = l[1],
						l = [Z.getCoordinates(h, W + _, W + _), Z.getCoordinates(h, W + m, W + m)],
						_ = l[0],
						y = l[1],
						b = -.25 < h && h < .25,
						w = .5 < h || h < 0,
						x = -.25 < h && h < .25 ? "pie-value start-text" : "pie-value end-text";
					switch ((A || $) && !1 !== A && (V = J.sv("text", {
						x: .7 * p[0],
						y: .7 * p[1],
						class: "pie-inner-value",
						"aria-hidden": "true"
					}, [$ ? Q.verticalCenteredText($(o.toString())) : Q.verticalCenteredText(o.toString())]), i.push(V)), F.config.subType) {
						case "basic":
							var E = w ? -20 : 10,
								C = Z.getCoordinates(h, W + 10, W + 10),
								k = b ? "donut-value start-text" : "donut-value end-text",
								S = {
									text1: {
										x: (R ? y : C)[0],
										y: (R ? y : C)[1] + E,
										width: 0,
										height: 0,
										class: ""
									},
									text2: {
										x: (R ? y : C)[0],
										y: (R ? y : C)[1] + E + 16,
										width: 0,
										height: 0,
										class: ""
									},
									changeSector: !1,
									line: f,
									right: b,
									dy: E
								},
								I = J.sv("text", X({
									x: (R ? y : C)[0],
									y: (R ? y : C)[1] + E,
									class: x
								}, H(o, s, n)), [Q.verticalCenteredText(s.toString())]),
								P = J.sv("text", {
									x: (R ? y : C)[0],
									y: (R ? y : C)[1] + E + 16,
									class: k,
									"aria-hidden": "true"
								}, [Q.verticalCenteredText(o.toString())]),
								k = Q.getSizesSVGText(s.toString(), {
									font: "normal 14px Roboto",
									lineHeight: 14
								});
							S.text1.width = k[0], S.text1.height = k[1], k = Q.getSizesSVGText(o.toString(), {
								font: "normal 12px Roboto",
								lineHeight: 12
							}), S.text2.width = k[0], S.text2.height = k[1];
							var O = R ? W + m : W + 10;
							0 !== G.length && (b ? (G.forEach(function (t) {
								Q.checkPositions(S.text1, t.text1, O, O, S), Q.checkPositions(S.text1, t.text2, O, O, S)
							}), S.text1.class && (S.text2.class = S.text1.class)) : (G.forEach(function (t) {
								Q.checkPositions(S.text2, t.text2, O, O, S), Q.checkPositions(S.text2, t.text1, O, O, S)
							}), S.text2.class && (S.text1.class = S.text2.class)), I.attrs.x = S.text1.x, I.attrs.y = S.text1.y, P.attrs.x = S.text2.x, P.attrs.y = S.text2.y, (S.text1.class || S.text2.class) && (I.attrs.class = S.text1.class, P.attrs.class = S.text2.class), f = S.line, R ? (y[0] = S.text1.x, y[1] = S.text1.y - E) : (C[0] = S.text1.x, C[1] = S.text1.y - E)), G.push(S);
							C = J.sv("text", {
								x: .5 * p[0],
								y: .5 * p[1],
								class: "pie-inner-value",
								"aria-hidden": "true"
							}, [Q.verticalCenteredText(Math.round(100 * n) + "%")]);
							i.push(J.sv("g", {
								id: r + "-text",
								class: "chart donut"
							}, [I, P])), i.push(C);
							break;
						case "valueOnly":
							var M = J.sv("text", {
								x: .5 * p[0],
								y: .5 * p[1],
								class: "pie-inner-value",
								"aria-hidden": "true"
							}, [Q.verticalCenteredText("" + o)]);
							i.push(M);
							break;
						case "percentOnly":
							M = J.sv("text", {
								x: .5 * p[0],
								y: .5 * p[1],
								class: "pie-inner-value",
								"aria-hidden": "true"
							}, [Q.verticalCenteredText(Math.round(100 * n) + "%")]);
							i.push(M)
					}
					R && "basic" === N && (E = w ? 3 : 0, i.push(J.sv("path", {
						d: "M" + _[0] + " " + _[1] + " L" + y[0] + "\n\t\t\t\t\t\t\t" + (y[1] + E) + " h " + f,
						id: r + "-connector",
						class: "pie-value-connector chart pie"
					})));
					var l = 0,
						V = 0,
						_ = z / (2 * Math.sin(Math.PI / F._points.length));
					1 < F._points.length && j && (l = (_ = Z.getCoordinates(h, _, _))[0], V = _[1]);
					g = "M " + d + " " + a + " A " + W + " " + W + " 0 " + u + " 1 " + v + " " + g + "\n\t\t\t\tL " + (0 + l) + " " + (0 + V), l = Z.getCoordinates(h, 4, 4), V = l[0], l = l[1], l = J.sv("path", {
						d: g,
						class: "chart pie",
						_key: r,
						fill: c,
						onclick: [F._handlers.onclick, t[1], t[2]],
						onmouseover: [Z.pieLikeHandlers.onmouseover, V, l],
						onmouseout: [Z.pieLikeHandlers.onmouseout],
						role: "presentation"
					});
					i.unshift(l), U.push(J.sv("g", {
						id: r
					}, i)), 1 === F._points.length ? q.push([D / 2, T / 2]) : q.push([.7 * p[0] + D / 2, .7 * p[1] + T / 2])
				}), this._center = [D / 2, T / 2], this._tooltipData = q, e.push(J.sv("defs", Y)), e = (e = e.concat(U)).concat([]), J.sv("g", X(X({
					transform: "translate(" + D / 2 + ", " + T / 2 + ")"
				}, {
					"aria-label": "chart " + (this.config.text || "")
				}), {
					tabindex: 0
				}), e)
			}, s);

		function s() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			U = this && this.__assign || function () {
				return (U = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, X = i(0),
			K = i(32),
			J = i(7),
			i = i(55),
			o = (r = i.default, o(s, r), s.prototype.paint = function (D, T) {
				function H(t, e, i) {
					return {
						role: "graphics-symbol",
						"aria-roledescription": "piece of pie",
						"aria-label": (e || "") + ", " + t + " (" + (i = i, Math.round(100 * (100 * i + Number.EPSILON)) / 100) + "%)"
					}
				}
				var F = this,
					t = this.config,
					j = t.subType,
					L = t.useLines,
					R = t.showText,
					A = t.showTextTemplate,
					$ = D < T ? D / 2 : T / 2,
					N = .5 * $,
					z = $ / 5,
					W = [],
					B = -.25,
					G = [],
					q = [],
					Y = [];
				return this._points.forEach(function (t, e) {
					var i = t[0],
						n = t[1],
						o = t[2],
						r = t[3],
						s = t[4],
						a = 0 === i || 1 === i ? -1e-6 : 0,
						l = K.getCoordinates(B, $, N),
						c = l[0],
						u = l[1],
						d = B + i / 2,
						h = d < .25 ? 5 : -5,
						f = K.getCoordinates(d, $, N),
						p = 0;
					0 < d && d < .5 && (p = z * Math.sin(2 * Math.PI * d));
					var _ = K.getCoordinates(d, $ + 5 + p, 5 + N + p),
						v = K.getCoordinates(d, $ + 30 + p, 30 + N + p),
						g = B + i + a,
						m = K.getCoordinates(g, $, N),
						l = m[0],
						a = m[1],
						m = .5 < i ? 1 : 0,
						y = -.25 < d && d < .25,
						b = .5 < d || d < 0,
						w = y ? "pie-value start-text" : "pie-value end-text";
					switch (F.config.subType) {
						case "basic":
							var x = b ? -20 : 10,
								E = y ? "donut-value start-text" : "donut-value end-text",
								C = {
									text1: {
										x: (L ? v : _)[0],
										y: (L ? v : _)[1] + x,
										width: 0,
										height: 0,
										class: ""
									},
									text2: {
										x: (L ? v : _)[0],
										y: (L ? v : _)[1] + x + 16,
										width: 0,
										height: 0,
										class: ""
									},
									changeSector: !1,
									line: h,
									right: y,
									dy: x
								},
								k = X.sv("text", U({
									x: (L ? v : _)[0],
									y: (L ? v : _)[1] + x,
									dx: L ? 0 < h / 2 + h ? 10 : -10 : null,
									class: w
								}, H(n, r, i)), [J.verticalCenteredText(r.toString())]),
								S = X.sv("text", {
									x: (L ? v : _)[0],
									y: (L ? v : _)[1] + x + 16,
									dx: L ? 0 < h / 2 + h ? 10 : -10 : null,
									class: E,
									"aria-hidden": "true"
								}, [J.verticalCenteredText(n.toString())]),
								I = X.sv("text", {
									x: .5 * f[0],
									y: .5 * f[1],
									class: "pie-inner-value",
									"aria-hidden": "true"
								}, [J.verticalCenteredText(Math.round(100 * i) + "%")]),
								E = J.getSizesSVGText(r.toString(), {
									font: "normal 14px Roboto",
									lineHeight: 14
								});
							C.text1.width = E[0], C.text1.height = E[1], E = J.getSizesSVGText(n.toString(), {
								font: "normal 12px Roboto",
								lineHeight: 12
							}), C.text2.width = E[0], C.text2.height = E[1];
							var P = L ? $ + 30 + p : $ + 5 + p,
								O = L ? 30 + N + p : 5 + N + p;
							0 !== Y.length && (y ? (Y.forEach(function (t) {
								J.checkPositions(C.text1, t.text1, P, O, C), J.checkPositions(C.text1, t.text2, P, O, C)
							}), C.text1.class && (C.text2.class = C.text1.class)) : (Y.forEach(function (t) {
								J.checkPositions(C.text2, t.text2, P, O, C), J.checkPositions(C.text2, t.text1, P, O, C)
							}), C.text2.class && (C.text1.class = C.text2.class)), k.attrs.x = C.text1.x, k.attrs.y = C.text1.y, S.attrs.x = C.text2.x, S.attrs.y = C.text2.y, (C.text1.class || C.text2.class) && (k.attrs.class = C.text1.class, S.attrs.class = C.text2.class), h = C.line, L ? (v[0] = C.text1.x, v[1] = C.text1.y - x) : (_[0] = C.text1.x, _[1] = C.text1.y - x)), Y.push(C), q.push(k, S, I);
							break;
						case "valueOnly":
							var M = X.sv("text", {
								x: .5 * f[0],
								y: .5 * f[1],
								class: "pie-inner-value",
								"aria-hidden": "true"
							}, [J.verticalCenteredText(n.toString())]);
							q.push(M);
							break;
						case "percentOnly":
							M = X.sv("text", {
								x: .5 * f[0],
								y: .5 * f[1],
								class: "pie-inner-value",
								"aria-hidden": "true"
							}, [J.verticalCenteredText(Math.round(100 * i) + "%")]);
							q.push(M)
					}
					L && "basic" === j && q.push(X.sv("path", {
						d: "M" + _[0] + " " + _[1] + " L" + v[0] + " " + v[1] + " h " + h,
						class: "pie-value-connector"
					})), (R || A) && !1 !== R && (V = X.sv("text", {
						x: .7 * f[0],
						y: .7 * f[1],
						class: "pie-inner-value",
						"aria-hidden": "true"
					}, [A ? J.verticalCenteredText(A(n)) : J.verticalCenteredText(n.toString())]), q.push(V));
					var V = "";
					B <= 0 && .5 <= g ? V = "M " + $ + " 0 v " + z + " A " + $ + " " + N + " 0 1 1 " + -$ + " " + z + " v " + -z : B <= 0 && g < .5 ? V = "M " + $ + " 0 v " + z + " A " + $ + " " + N + " 0 0 1 " + l + " " + (a + z) + " v " + -z : 0 < B && B <= .5 && .5 <= g ? V = "M " + c + " " + u + " v " + z + " A " + $ + " " + N + " 0 0 1 " + -$ + " " + z + " v " + -z : 0 < B && g < .5 && (V = "M " + c + " " + u + " v " + z + " A " + $ + " " + N + " 0 0 1 " + l + " " + (a + z) + " v " + -z), V && (V = X.sv("path", {
						_key: o + "__shadow__",
						d: V,
						fill: s,
						onclick: [F._handlers.onclick, t[1], t[2]],
						class: "chart pie3d addition",
						stroke: "none",
						filter: "url(#shadow)",
						role: "presentation"
					}), G.push(V));
					a = "M " + c + " " + u + " A " + $ + " " + N + " 0 " + m + " 1 " + l + " " + a + " L 0 0";
					G.push(X.sv("path", {
						d: a,
						_key: o,
						fill: s,
						stroke: "none",
						onclick: [F._handlers.onclick, t[1], t[2]],
						class: "chart pie3d",
						role: "presentation"
					})), 1 === F._points.length ? W.push([D / 2, T / 2]) : W.push([.7 * f[0] + D / 2, .7 * f[1] + T / 2]), B = g
				}), this._center = [D / 2, T / 2], this._tooltipData = W, G = G.concat(q), X.sv("g", U(U({
					transform: "translate(" + D / 2 + ", " + T / 2 + ")"
				}, {
					"aria-label": "chart " + (this.config.text || "")
				}), {
					tabindex: 0
				}), G)
			}, s);

		function s() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			c = i(32),
			l = i(7),
			i = i(54),
			o = (s = i.default, o(u, s), u.prototype.addScale = function (t, e) {
				this._scale = e
			}, u.prototype.scaleReady = function (t) {
				for (var e in t) t[e] += this.config.paddings;
				return t
			}, u.prototype.dataReady = function (n) {
				var o = this;
				if (!this.config.active) return this._points = [];
				var r = l.locator(this._scale.config.value);
				return this._points = this._data.map(function (t, e) {
					var i = o._locator(t),
						i = [i, i, t.id, i, i];
					return n && (i[1] += n[e][1]), r && i.push(r(t)), i
				}), this._points
			}, u.prototype.getTooltipText = function (t) {
				if (this.config.tooltip) {
					t = this._defaultLocator(this._data.getItem(t));
					return this.config.tooltipTemplate ? this.config.tooltipTemplate(t) : t
				}
			}, u.prototype.paint = function (t, e) {
				var o = this;
				s.prototype.paint.call(this, t, e);
				if (this.config.active) {
					var i = this.config,
						t = [],
						e = this._points.map(function (t, e) {
							return (e ? "L" : "M") + t[0] + " " + t[1]
						}).join(" ") + "Z";
					return t.push(a.sv("path", {
						d: e,
						stroke: i.color,
						"stroke-width": i.strokeWidth,
						fill: i.fill,
						"fill-opacity": i.alpha,
						class: "chart radar"
					})), i.pointType && (e = this._points.map(function (t) {
						return o._drawPointType(t[0], t[1], l.calcPointRef(t[2], o.id))
					}).map(function (t) {
						var e, i, n;
						return t && t.attrs && (t.attrs = r(r({}, t.attrs), (e = t.key, i = o._points, e && (n = i.find(function (t) {
							return e.includes(t[2])
						})), {
							role: "graphics-symbol",
							"aria-roledescription": "point",
							"aria-label": n ? "point x=" + n[5] + " y=" + n[4] : "",
							tabindex: 0
						}))), t
					}), t.push(a.sv("g", e))), a.sv("g", r(r({
						id: "seria" + i.id
					}, {
						"aria-label": "chart " + (this.config.value || "")
					}), {
						tabindex: 0
					}), t)
				}
			}, u.prototype._calckFinalPoints = function (n, o) {
				var r = this,
					s = n < o ? n / 2 : o / 2,
					a = 1 / this._data.getLength(),
					l = -.25 - a;
				this._points.forEach(function (t, e) {
					l += a;
					var i = r._scale.point(t[0]),
						i = c.getCoordinates(l, i * s, i * s);
					t[0] = i[0] + n / 2, t[1] = i[1] + o / 2
				})
			}, u.prototype._defaultLocator = function (t) {
				return this._locator(t)
			}, u.prototype._setDefaults = function (t) {
				this._locator = l.locator(t.value), t.scales = t.scales || ["radial"], this.config = r(r({}, {
					strokeWidth: 2,
					active: !0,
					tooltip: !0,
					paddings: 5,
					color: "none",
					fill: "none",
					pointType: "circle"
				}), t), this.config.pointType && (t = this.config.pointColor || this.config.color, this._drawPointType = this._getPointType(this.config.pointType, t))
			}, u);

		function u() {
			return null !== s && s.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(56),
			l = i(7),
			o = (s = a.default, o(c, s), c.prototype.addScale = function (t, e) {
				"bottom" === t || "top" === t ? (this.xScale = e, this._xLocator = l.locator(this.config.value)) : (this.yScale = e, this._yLocator = l.locator(this.config.valueY))
			}, c.prototype._setDefaults = function (t) {
				this.config = r(r({}, {
					active: !0,
					tooltip: !0,
					pointType: "rect"
				}), t);
				var e = this.config.pointType,
					t = this.config.pointColor || this.config.color;
				e && (this._drawPointType = this._getPointType(e, t))
			}, c);

		function c() {
			return null !== s && s.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, a = i(0),
			l = i(89),
			i = i(56),
			o = (r = i.default, o(s, r), s.prototype._getForm = function (t, e, i, n, o) {
				var r = e.color,
					s = e.css,
					t = l.default(t);
				return a.sv("path", {
					id: "seria" + e.id,
					d: t,
					class: s,
					stroke: r,
					"stroke-width": 2,
					fill: "none"
				})
			}, s);

		function s() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, h = i(0),
			f = i(89),
			i = i(86),
			o = (r = i.default, o(s, r), s.prototype._form = function (t, e, i, n) {
				var o = this.config,
					r = o.fill,
					s = o.alpha,
					a = o.strokeWidth,
					l = o.color,
					c = o.id,
					u = this.config.css,
					d = "",
					o = this._points[0],
					d = n ? f.default([].concat(n).reverse()) + " " + f.default(this._points, !0) + " Z" : "M" + o[0] + " " + e + " V " + o[1] + " " + f.default(this._points) + " V" + e + " H " + o[0];
				a && (o = f.default(this._points), l = h.sv("path", {
					d: o,
					"stroke-width": a,
					stroke: l,
					fill: "none",
					"stroke-linecap": "butt",
					class: u
				}), i.push(l));
				s = h.sv("path", {
					id: "seria" + c,
					d: d,
					class: u,
					fill: r,
					"fill-opacity": s,
					stroke: "none"
				});
				return i.push(s), i
			}, s);

		function s() {
			return null !== r && r.apply(this, arguments) || this
		}
		e.default = o
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var l = i(0),
			i = (n.prototype.add = function (t) {
				this._series.push(t)
			}, n.prototype.dataReady = function (e) {
				return this._toPaint = this._series.filter(function (t) {
					t = t.dataReady(e);
					return !!t.length && (e = t, !0)
				}), e || []
			}, n.prototype.getPoints = function () {
				return this._toPaint.length ? this._toPaint[0].getPoints().concat(this._toPaint[this._toPaint.length - 1].getPoints()) : []
			}, n.prototype.paint = function (n, o, r) {
				var s = [],
					a = [];
				return this._toPaint.forEach(function (t) {
					var e, i;
					t.paintformAndMarkers ? (i = (e = t.paintformAndMarkers(n, o, r))[0], e = e[1], s.push(i), a.push(e)) : (i = t.paint(n, o, r), s.push(i)), r = t.getPoints()
				}), l.sv("g", s.concat(a))
			}, n);

		function n() {
			this._series = []
		}
		e.default = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var v = i(10),
			s = i(7),
			g = i(87),
			n = i(31),
			i = (o.prototype.destructor = function () {
				this._tooltip && (document.body.removeChild(this._tooltip), this._tooltip = null)
			}, o.prototype._showLineTooltip = function (t) {
				for (var e = "", i = this._chart.getRootView(), n = 0, o = 0, r = t; o < r.length; o++) {
					var s = r[o];
					n += s.top, this._prevLine = i && i.refs && i.refs["line" + Math.round(s.left)] && i.refs["line" + Math.round(s.left)].el, this._prevLine.classList.add("grid-line__active");
					var a, l = this._chart.getSeries(s.seriaId),
						c = l.getPoints();
					c.length && l.config.tooltip && (a = l.config.pointColor || (l.config.color && "none" !== l.config.color ? l.config.color : l.config.fill), a = g.getShadeHTMLHelper("empty" !== l.config.pointType && l.config.pointType ? l.config.pointType : "simpleRect", a), e += '<div class="line-point" _ref="dhx_tooltip_' + l.config.id + '_box">\n\t\t\t\t<svg class="dhx_tooltip_svg" role="graphics-document" style="width: 8px; height: 8px;">' + a(4, 4, c[0][2]) + '</svg>\n\t\t\t\t<span class="dhx_line-point-text" _ref="dhx_tooltip_' + l.config.id + '_text">' + s.text + "</span>\n\t\t\t</div>")
				}
				this._tooltip || this._createTooltip();
				var u = document.body.offsetHeight,
					d = document.body.offsetWidth;
				this._tooltip.innerHTML = e, this._tooltip.classList.add("dhx_chart_tooltip__visible");
				var h = n / t.length,
					f = this._prevLine.getBoundingClientRect(),
					p = this._tooltip.offsetHeight,
					_ = this._tooltip.offsetWidth,
					t = h - p / 2 + f.top + window.scrollY,
					h = f.left + 10;
				u < t + p && (t -= t + p - u), d < h + _ && (h = f.left - _ - 10);
				_ = v.getZIndex(this._chart.getRootNode());
				_ && (this._tooltip.style.zIndex = _.toString()), this._tooltip.style.left = h + "px", this._tooltip.style.top = t + "px"
			}, o.prototype._showTooltip = function (t, e) {
				this._tooltip || this._createTooltip();
				var i = document.body.offsetHeight,
					n = document.body.offsetWidth;
				this._tooltip.innerHTML = t, this._tooltip.classList.add("dhx_chart_tooltip__visible");
				var o = this._tooltip.offsetHeight,
					r = this._tooltip.offsetWidth,
					s = e.pageY + 10,
					t = e.pageX + 10;
				i < s + o && (s = e.pageY - o), n < t + r && (t = e.pageX - r - 10);
				r = v.getZIndex(this._chart.getRootNode());
				r && (this._tooltip.style.zIndex = r.toString()), this._tooltip.style.left = t + "px", this._tooltip.style.top = s + "px"
			}, o.prototype._showTooltipOnClosest = function (t) {
				this._tooltip || this._createTooltip();
				var e, i, n, o, r = this._chart.getSeries(t[4]);
				!r || (o = r.getTooltipText(t[3])) && (e = this._chart._layers.getSizes(), i = this._chart.getRootNode().getBoundingClientRect(), this._tooltip.innerHTML = o, this._tooltip.classList.add("dhx_chart_tooltip__visible"), n = document.body.offsetHeight, r = this._tooltip.offsetHeight, n < (o = t[2] + i.top + r / 2 + e.top - ("radar" === this._chart.config.type ? 10 : 15)) + r && (o = t[2] + i.top - r / 2 + e.top - ("radar" === this._chart.config.type ? 10 : 15)), n < o + r && this._tooltip.classList.remove("dhx_chart_tooltip__visible"), (r = v.getZIndex(this._chart.getRootNode())) && (this._tooltip.style.zIndex = r.toString()), this._tooltip.style.left = i.left + t[1] + 10 + "px", this._tooltip.style.top = o + "px")
			}, o.prototype._createTooltip = function () {
				this._tooltip = document.createElement("div"), this._tooltip.setAttribute("dhx_widget_id", this._chart._uid), this._tooltip.classList.add("dhx_chart_tooltip"), this._tooltip.classList.add("dhx_chart_tooltip_line"), this._tooltip.classList.add("dhx_chart_tooltip__hidden"), document.body.appendChild(this._tooltip)
			}, o.prototype._initEvents = function () {
				var a = this;
				this._chart.events.on(n.ChartEvents.chartMouseMove, function (n, o) {
					var r, s;
					a._prevLine && a._prevLine.classList.remove("grid-line__active"), a._mouseOverBar || (r = [], s = [1 / 0, null, null, null, null], a._chart.eachSeries(function (t) {
						var e, i = t.config.type;
						"line" === i || "spline" === i || "splineArea" === i || "area" === i ? "number" == typeof (e = {
							value: (e = t.getClosestVertical(n))[4],
							text: t.getTooltipText(e[3]),
							seriaId: t.config.id,
							left: e[1],
							top: e[2]
						}).left && "number" == typeof e.top && r.push(e) : "pie" !== i && "pie3D" !== i && "donut" !== i && "radar" !== i || (i = t.getClosest(n, o), s[0] > i[0] && (s[0] = i[0], s[1] = i[1], s[2] = i[2], s[3] = i[3], s[4] = t.id))
					}), r.length ? (r.sort(function (t, e) {
						return e.value - t.value
					}), a._showLineTooltip(r)) : a._showTooltipOnClosest(s))
				}), this._chart.events.on(n.ChartEvents.seriaMouseMove, function (t, e, i) {
					a._mouseOverBar = !0;
					var n = a._chart.getRootView(),
						o = a._chart.getSeries(e),
						r = o.getTooltipText(t);
					(n && n.refs && n.refs[s.calcPointRef(t, e)].el).setAttribute("fill-opacity", .6 < o.config.alpha ? o.config.alpha - .4 : 1), r ? a._showTooltip(r, i) : a._tooltip && a._tooltip.classList.remove("dhx_chart_tooltip__visible")
				}), this._chart.events.on(n.ChartEvents.seriaMouseLeave, function (t, e) {
					a._mouseOverBar = !1;
					var i = a._chart.getRootView(),
						n = a._chart.getSeries(e);
					"area" !== n.config.type && (i && i.refs && i.refs[s.calcPointRef(t, e)].el).setAttribute("fill-opacity", n.config.alpha), a._tooltip && a._tooltip.classList.remove("dhx_chart_tooltip__visible")
				}), this._chart.events.on(n.ChartEvents.chartMouseLeave, function () {
					a._tooltip && a._tooltip.classList.remove("dhx_chart_tooltip__visible"), a._prevLine && a._prevLine.classList.remove("grid-line__active")
				})
			}, o);

		function o(t) {
			this._chart = t, this._initEvents()
		}
		e.Tooltip = i
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var n = i(225);
		e.getEditor = function (t, e) {
			return new n.InputEditor(t, e)
		}
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var o = i(0),
			n = i(92),
			i = (r.prototype.endEdit = function () {
				var t;
				this._input && (t = this._input.value, this._dataView.events.fire(n.DataViewEvents.beforeEditEnd, [t, this._item.id]) ? (this._input.removeEventListener("blur", this._handlers.onBlur), this._input.removeEventListener("change", this._handlers.onChange), this._handlers = {}, this._mode = !1, this._dataView.events.fire(n.DataViewEvents.afterEditEnd, [t, this._item.id])) : this._input.focus())
			}, r.prototype.toHTML = function (t) {
				this._mode = !0;
				var e = this._config,
					i = e.itemsInRow,
					n = e.gap,
					e = function (t) {
						return parseFloat(t)
					};
				return o.el(".dhx_input__wrapper", {
					style: {
						width: "calc(" + 100 / i + "% - " + e(n) + " * " + (i - 1) / i + "px)",
						maxWidth: "calc(" + 100 / i + "% - " + e(n) + " * " + (i - 1) / i + "px)",
						marginRight: t ? "" : n
					},
					role: "presentation"
				}, [o.el("div.dhx_input__container", {
					style: {
						height: "100%"
					},
					role: "presentation"
				}, [o.el("input.dhx_input", {
					class: (this._item.css ? " " + this._item.css : "") + (t ? " dhx_dataview-item--last-item-in-row" : ""),
					style: {
						padding: "8px, 12px",
						width: "100%",
						height: "100%"
					},
					_hooks: {
						didInsert: this._handlers.didInsert
					},
					_key: this._item.id,
					dhx_id: this._item.id
				})])])
			}, r.prototype._initHandlers = function () {
				var e = this;
				this._handlers = {
					onBlur: function () {
						e.endEdit()
					},
					onChange: function () {
						e.endEdit()
					},
					didInsert: function (t) {
						t = t.el;
						(e._input = t).focus(), t.value = e._item.value, t.setSelectionRange(0, t.value.length), t.addEventListener("change", e._handlers.onChange), t.addEventListener("blur", e._handlers.onBlur)
					}
				}
			}, r);

		function r(t, e) {
			var i = this;
			this._dataView = e, this._config = e.config, this._item = t, this._dataView.events.on(n.DataViewEvents.focusChange, function (t, e) {
				i._mode && e !== i._item.id && i.endEdit()
			}), this._initHandlers()
		}
		e.InputEditor = i
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			l = this && this.__assign || function () {
				return (l = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(17),
			a = i(91),
			c = i(0),
			o = (r = a.DataView, o(u, r), u.prototype.destructor = function () {
				r.prototype.destructor.call(this), this.scrollView = null
			}, u.prototype.showItem = function (t) {
				var e, i, n, o = this.getRootView();
				o && o.node && o.node.el && void 0 !== t && ((e = this.getRootNode().getElementsByClassName("scroll-view")[0]) && (i = this.config.virtual, n = this.data.getIndex(t), o = Math.floor(n / this.config.itemsInRow), t = Math.floor(o / e.children.length) || 0, t = e.children[o - e.children.length * t], (i || t) && (t = t.children[n % this.config.itemsInRow], n = parseInt(this.config.gap.toString().replace("px", ""), null), t.offsetTop >= e.clientHeight + e.scrollTop - t.clientHeight ? e.scrollTop = t.offsetTop - e.clientHeight + t.clientHeight + n : t.offsetTop < e.scrollTop - n && (e.scrollTop = t.offsetTop - n))))
			}, u.prototype._renderList = function () {
				var n = this,
					t = this.data.getRawData(0, -1),
					e = this.config,
					o = e.itemsInRow,
					i = e.css,
					r = e.gap,
					s = 0,
					a = t.reduce(function (t, e, i) {
						return 0 === s && t.push([]), t[t.length - 1].push(n._renderItem(e, i)), s = (s + 1) % o, t
					}, []),
					e = a.map(function (t, e) {
						return c.el(".dhx_dataview-row", {
							style: {
								margin: r
							},
							"aria-label": "Row " + (e + 1)
						}, t)
					}),
					t = this.scrollView && this.scrollView.config.enable,
					i = (i || "") + " dhx_widget dhx_dataview" + (this.config.multiselection && this.selection.getItem() ? " dhx_no-select--pointer" : "") + (t ? " dhx_dataview--scroll-view" : "");
				return c.el("", l(l(l({}, this._handlers), {
					dhx_widget_id: this._uid,
					class: i,
					style: {
						height: this.config.height
					}
				}), this.getDataViewAriaAttrs(this.config, this.data.getLength(), a.length, o)), t ? [this.scrollView.render(e)] : e)
			}, u);

		function u(t, e) {
			void 0 === e && (e = {});
			var i = r.call(this, t, e) || this;
			return i.scrollView = new s.ScrollView(function () {
				return i.getRootView()
			}), i.paint(), i
		}
		e.ProDataView = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(1),
			c = i(0),
			u = i(3),
			d = i(4),
			h = i(8),
			f = i(5);
		(i = a = e.RadioButtonEvents || (e.RadioButtonEvents = {})).change = "change", i.focus = "focus", i.blur = "blur", i.keydown = "keydown";
		var p, o = (p = d.View, o(_, p), _.prototype.destructor = function () {
			this.events && this.events.clear(), this.config = this.events = this._handlers = this._uid = this._props = this._propsItem = null, this.unmount()
		}, _.prototype.setProperties = function (t) {
			if (t && !l.isEmptyObj(t)) {
				for (var e in t) this._props.includes(e) && (this.config[e] = t[e]);
				this.paint()
			}
		}, _.prototype.getProperties = function () {
			var t, e = {};
			for (t in this.config) this._props.includes(t) && (e[t] = this.config[t]);
			return e
		}, _.prototype.getValue = function () {
			if (this.config.checked) return this.config.value
		}, _.prototype.setValue = function (t) {
			this.config.checked = t, this.paint()
		}, _.prototype.focus = function () {
			var t = this;
			c.awaitRedraw().then(function () {
				t.getRootView().refs.input.el.focus()
			})
		}, _.prototype.blur = function () {
			var t = this;
			c.awaitRedraw().then(function () {
				t.getRootView().refs.input.el.blur()
			})
		}, _.prototype.disable = function () {
			this.config.$disabled = !0, this.paint()
		}, _.prototype.enable = function () {
			this.config.$disabled = !1, this.paint()
		}, _.prototype.isDisabled = function () {
			return this.config.$disabled
		}, _.prototype.clear = function () {
			this.config.checked = !1, this.paint()
		}, _.prototype.validate = function () {
			var t = !1;
			return this.config.checked && (t = !0), this.config.$validationStatus = t ? f.ValidationStatus.success : f.ValidationStatus.error, this.paint(), t
		}, _.prototype.clearValidate = function () {
			this.config.$validationStatus = f.ValidationStatus.pre, this.paint()
		}, _.prototype._draw = function () {
			var t = this.config,
				e = t.id,
				i = t.value,
				n = t.checked,
				o = t.$disabled,
				r = t.$name,
				s = t.$required,
				t = t.text;
			return c.el("label.dhx_radiobutton.dhx_form-group", {
				class: h.getFormItemCss(this.config, !!s)
			}, [c.el("input.dhx_radiobutton__input", {
				type: "radio",
				id: e,
				value: i || "",
				name: r || "",
				disabled: o,
				checked: n,
				onchange: this._handlers.onchange,
				onfocus: this._handlers.onfocus,
				onblur: this._handlers.onblur,
				onkeydown: this._handlers.onkeydown,
				required: s,
				_ref: "input"
			}), c.el("span.dhx_radiobutton__visual-input"), c.el("span.dhx_text", [t])])
		}, _);

		function _(t, e) {
			void 0 === e && (e = {});
			var i = p.call(this, t, r({
				text: "",
				width: "content",
				height: "content",
				padding: 0
			}, e)) || this;
			i._propsItem = ["text"], i._props = s(h.baseProps, i._propsItem), i._handlers = {
				onchange: function (t) {
					i.config.checked = t.target.checked, i.events.fire(a.change, [t.target.checked])
				},
				onfocus: function () {
					return i.events.fire(a.focus, [i.getValue() || "", i.config.id])
				},
				onblur: function () {
					return i.events.fire(a.blur, [i.getValue() || "", i.config.id])
				},
				onkeydown: function (t) {
					i.events.fire(a.keydown, [t, i.config.id])
				}
			}, i.events = new u.EventSystem;
			return i.mount(t, c.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.RadioButton = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			C = this && this.__rest || function (t, e) {
				var i = {};
				for (o in t) Object.prototype.hasOwnProperty.call(t, o) && e.indexOf(o) < 0 && (i[o] = t[o]);
				if (null != t && "function" == typeof Object.getOwnPropertySymbols)
					for (var n = 0, o = Object.getOwnPropertySymbols(t); n < o.length; n++) e.indexOf(o[n]) < 0 && Object.prototype.propertyIsEnumerable.call(t, o[n]) && (i[o[n]] = t[o[n]]);
				return i
			},
			k = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(94),
			S = i(5),
			a = i(11),
			I = i(1),
			P = i(96),
			O = i(95),
			M = i(57),
			V = i(97),
			D = i(37),
			T = i(98),
			H = i(99),
			F = i(104),
			j = i(103),
			L = i(100),
			R = i(101),
			A = i(106),
			$ = i(107),
			N = i(229),
			z = i(108),
			W = i(230),
			o = (r = s.Form, o(l, r), l.prototype._addLayoutItem = function (t) {
				var i = this,
					e = t.id = t.id || I.uid(),
					n = t.name = t.name || e.toString();
				t.type = t.type && t.type.toLowerCase();
				var o = t.width,
					r = t.height,
					s = t.css,
					e = t.padding,
					a = C(t, ["css", "padding"]),
					l = s ? s + " dhx_form-element" : "dhx_form-element",
					s = !("spacer" === t.type || void 0 === t.type);
				switch (s && !o && (o = "content"), s && !r && (r = "content"), a.type) {
					case "button":
						a.full && (l += " dhx_button--full-gravity");
						var c = this._attachments[n] = new P.Button(null, a);
						c.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), c.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), c.events.on(S.ItemEvent.click, function (t) {
							t.preventDefault(), i.events.fire(S.FormEvents.click, [n, t]), i.events.fire(S.FormEvents.buttonClick, [n, t]), c.config.submit && i.validate() && c.config.url && i.send(c.config.url)
						}), c.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), c.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), c.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), c.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), c.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), c.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), c.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "datepicker":
						var u = this._attachments[n] = new O.DatePicker(null, a);
						this._state[n] = u.getValue("Date" === a.valueFormat), u.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), u.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = u.getValue("Date" === a.valueFormat), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), u.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), u.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), u.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), u.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), u.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), u.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), u.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), u.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), u.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), u.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "checkbox":
						var d = this._attachments[n] = new M.Checkbox(null, a);
						this._state[n] = d.getValue(), d.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), d.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = d.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), d.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), d.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), d.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), d.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), d.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), d.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), d.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), d.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), d.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), d.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "checkboxgroup":
						var h = this._attachments[n] = new V.CheckboxGroup(null, a);
						this._state[n] = h.getValue(), h.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), h.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = h.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), h.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), h.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), h.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), h.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), h.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), h.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), h.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), h.events.on(S.ItemEvent.focus, function (t, e) {
							i.events.fire(S.FormEvents.focus, [n, t, e])
						}), h.events.on(S.ItemEvent.blur, function (t, e) {
							i.events.fire(S.FormEvents.blur, [n, t, e])
						}), h.events.on(S.ItemEvent.keydown, function (t, e) {
							i.events.fire(S.FormEvents.keydown, [t, n, e])
						});
						break;
					case "combo":
						var f = this._attachments[n] = new W.ProCombo(null, a);
						this._state[n] = f.getValue(), f.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), f.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = f.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), f.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), f.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), f.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), f.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), f.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), f.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), f.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), f.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), f.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), f.events.on(S.ItemEvent.keydown, function (t, e) {
							i.events.fire(S.FormEvents.keydown, [t, n, e])
						});
						break;
					case "input":
						var p = this._attachments[n] = new D.Input(null, a);
						this._state[n] = p.getValue(), p.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), p.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = p.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), p.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), p.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), p.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), p.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), p.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), p.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), p.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), p.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), p.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), p.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "radiogroup":
						var _ = this._attachments[n] = new T.RadioGroup(null, a);
						this._state[n] = _.getValue(), _.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), _.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = _.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), _.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), _.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), _.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), _.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), _.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), _.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), _.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), _.events.on(S.ItemEvent.focus, function (t, e) {
							i.events.fire(S.FormEvents.focus, [n, t, e])
						}), _.events.on(S.ItemEvent.blur, function (t, e) {
							i.events.fire(S.FormEvents.blur, [n, t, e])
						}), _.events.on(S.ItemEvent.keydown, function (t, e) {
							i.events.fire(S.FormEvents.keydown, [t, n, e])
						});
						break;
					case "select":
						var v = this._attachments[n] = new H.Select(null, a);
						this._state[n] = v.getValue(), v.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), v.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = v.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), v.events.on(S.ItemEvent.changeOptions, function (t) {
							i.layout.getCell(n).config.options = k(t)
						}), v.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), v.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), v.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), v.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), v.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), v.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), v.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), v.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), v.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), v.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "simplevault":
						a.$vaultHeight = r;
						var g = this._attachments[n] = new F.SimpleVault(null, a);
						this._state[n] = g.getValue(), g.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), g.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = g.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), g.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), g.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), g.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), g.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), g.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), g.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), g.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						});
						break;
					case "slider":
						var m = this._attachments[n] = new j.SliderForm(null, a);
						this._state[n] = m.getValue(), m.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), m.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), m.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), m.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), m.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), m.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), m.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), m.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "textarea":
						var y = this._attachments[n] = new L.Textarea(null, a);
						this._state[n] = y.getValue(), y.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), y.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = y.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), y.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), y.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), y.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), y.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), y.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), y.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), y.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), y.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), y.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), y.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "text":
						var b = this._attachments[n] = new R.Text(null, a);
						this._state[n] = b.getValue(), b.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), b.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = b.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), b.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), b.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), b.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), b.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), b.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), b.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), b.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						});
						break;
					case "timepicker":
						var w = this._attachments[n] = new A.TimePicker(null, a);
						this._state[n] = a.value && w.getValue("timeObject" === a.valueFormat) || "", w.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), w.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = w.getValue("timeObject" === a.valueFormat), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), w.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), w.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), w.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), w.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), w.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), w.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), w.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), w.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), w.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), w.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "colorpicker":
						var x = this._attachments[n] = new $.ColorPicker(null, a);
						this._state[n] = x.getValue(), x.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), x.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i._state[n] = x.getValue(), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), x.events.on(S.ItemEvent.change, function (t) {
							i._state[n] = t, i.events.fire(S.FormEvents.change, [n, t])
						}), x.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), x.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), x.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), x.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						}), x.events.on(S.ItemEvent.beforeValidate, function (t) {
							return i.events.fire(S.FormEvents.beforeValidate, [n, t])
						}), x.events.on(S.ItemEvent.afterValidate, function (t, e) {
							i.events.fire(S.FormEvents.afterValidate, [n, t, e])
						}), x.events.on(S.ItemEvent.focus, function (t) {
							i.events.fire(S.FormEvents.focus, [n, t])
						}), x.events.on(S.ItemEvent.blur, function (t) {
							i.events.fire(S.FormEvents.blur, [n, t])
						}), x.events.on(S.ItemEvent.keydown, function (t) {
							i.events.fire(S.FormEvents.keydown, [t, n])
						});
						break;
					case "container":
						var E = this._attachments[n] = new N.Container(null, a);
						E.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), E.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), E.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), E.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), E.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), E.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						});
						break;
					case "spacer":
					default:
						E = this._attachments[n] = new z.Spacer(null, a);
						E.events.on(S.ItemEvent.beforeChangeProperties, function (t) {
							return i.events.fire(S.FormEvents.beforeChangeProperties, [n, t])
						}), E.events.on(S.ItemEvent.afterChangeProperties, function (t) {
							i._changeProps(n, t), i.events.fire(S.FormEvents.afterChangeProperties, [n, t]), i.layout.paint()
						}), E.events.on(S.ItemEvent.beforeHide, function (t, e) {
							if (!e) return i.events.fire(S.FormEvents.beforeHide, [n, t])
						}), E.events.on(S.ItemEvent.beforeShow, function (t) {
							return i.events.fire(S.FormEvents.beforeShow, [n, t])
						}), E.events.on(S.ItemEvent.afterHide, function (t, e) {
							i.layout.getCell(n).hide(), e || i.events.fire(S.FormEvents.afterHide, [n, t])
						}), E.events.on(S.ItemEvent.afterShow, function (t) {
							i.layout.getCell(n).show(), i.events.fire(S.FormEvents.afterShow, [n, t])
						})
				}
				e = {
					id: n,
					width: o,
					height: r,
					padding: e,
					css: l
				};
				return "gravity" in t && (e.gravity = t.gravity), e
			}, l.prototype._initUI = function (t) {
				var e = this._attachments = {},
					i = {
						padding: "8px"
					};
				this.config.css += " dhx_form", this._createLayoutConfig(this.config, i);
				var n, o = this.layout = new a.ProLayout(t, i);
				for (n in e) o.getCell(n).attach(e[n])
			}, l);

		function l(t, e) {
			return r.call(this, t, e) || this
		}
		e.ProForm = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s, a = i(0),
			l = i(4),
			c = i(5),
			u = i(3),
			d = i(1),
			h = i(11),
			f = i(8),
			o = (s = l.View, o(p, s), p.prototype.attach = function (t) {
				this.container.attach(t)
			}, p.prototype.attachHTML = function (t) {
				this.container.attachHTML(t)
			}, p.prototype.show = function () {
				this.config.hidden && this.events.fire(c.ItemEvent.beforeShow, [void 0]) && (this.config.hidden = !1, this.events.fire(c.ItemEvent.afterShow, [void 0]))
			}, p.prototype.hide = function (t) {
				this.config.hidden && !t || !this.events.fire(c.ItemEvent.beforeHide, [void 0, t]) || (this.config.hidden = !0, this.events.fire(c.ItemEvent.afterHide, [void 0, t]))
			}, p.prototype.isVisible = function () {
				return !this.config.hidden
			}, p.prototype.disable = function () {
				this.config.disabled = !0, this.paint()
			}, p.prototype.enable = function () {
				this.config.disabled = !1, this.paint()
			}, p.prototype.isDisabled = function () {
				return this.config.disabled
			}, p.prototype.setProperties = function (t) {
				if (t && !d.isEmptyObj(t) && this.events.fire(c.ItemEvent.beforeChangeProperties, [this.getProperties()])) {
					for (var e in t) f.baseProps.includes(e) && (this.config[e] = t[e]);
					this.events.fire(c.ItemEvent.afterChangeProperties, [this.getProperties()]), this.paint()
				}
			}, p.prototype.getProperties = function () {
				var t, e = {};
				for (t in this.config) f.baseProps.includes(t) && (e[t] = this.config[t]);
				return e
			}, p.prototype._getRootView = function () {
				return this.container.getRootView()
			}, p.prototype._draw = function () {
				var t = this.config,
					e = t.name,
					i = t.id,
					t = t.disabled ? " dhx_form-group--disabled" : "";
				return a.el(".dhx_form-group.dhx_form-group--container", {
					class: t,
					dhx_id: e || i
				}, [a.inject(this._getRootView())])
			}, p);

		function p(t, e) {
			var i = s.call(this, t, r({
				disabled: !1,
				hidden: !1,
				width: "content",
				height: "content",
				padding: 0
			}, e)) || this;
			if (i.events = new u.EventSystem, d.isEmptyObj(e)) throw new Error("Check the configuration is correct");
			i.container && i.container.destructor();
			var n, o = {};
			for (n in e) "id" !== n && "type" !== n && "name" !== n && "width" !== n && "height" !== n && (i.config[n] = e[n], "validation" !== n && (o[n] = e[n]));
			i.container = new h.Layout(null, {
				rows: [o]
			}), i.config.hidden && a.awaitRedraw().then(function () {
				i.hide(!0)
			}), i.paint();
			return i.mount(t, a.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Container = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(102),
			a = i(1),
			l = i(24),
			c = i(0),
			o = (r = s.Combo, o(u, r), u.prototype._initView = function (t) {
				var e = this;
				if (a.isEmptyObj(t)) throw new Error("Check the configuration is correct");
				this.combobox && this.combobox.destructor(), this.config = {
					type: t.type,
					id: t.id,
					name: t.name,
					disabled: !1,
					hidden: !1,
					value: "",
					readOnly: !1,
					template: void 0,
					filter: void 0,
					multiselection: !1,
					selectAllButton: !1,
					itemsCount: void 0,
					itemHeight: 32,
					virtual: !1,
					listHeight: 224,
					required: !1,
					validation: void 0,
					placeholder: "",
					label: "",
					labelWidth: "",
					labelPosition: "top",
					hiddenLabel: !1,
					helpMessage: "",
					preMessage: "",
					successMessage: "",
					errorMessage: "",
					width: "content",
					height: "content",
					padding: 0
				};
				var i, n = {};
				for (i in t) "id" !== i && "type" !== i && "name" !== i && (this.config[i] = t[i], "validation" !== i && (n[i] = t[i]));
				this.combobox = new l.ProCombobox(null, n), this.config.hidden && c.awaitRedraw().then(function () {
					e.hide(!0)
				}), this.paint()
			}, u);

		function u(t, e) {
			return r.call(this, t, e) || this
		}
		e.ProCombo = o
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(232)), n(e(233))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(2),
			a = i(10),
			l = i(19),
			o = (r = l.Navbar, o(c, r), c.prototype.showAt = function (t, e) {
				void 0 === e && (e = "bottom"), t instanceof MouseEvent ? (this._close(t), this._changeActivePosition({
					left: window.pageXOffset + t.x + 1,
					right: window.pageXOffset + t.x + 1,
					top: window.pageYOffset + t.y,
					bottom: window.pageYOffset + t.y,
					zIndex: a.getZIndex(t.currentTarget)
				}, e)) : t instanceof TouchEvent ? (this._close(t), this._changeActivePosition({
					left: window.pageXOffset + t.touches[0].clientX,
					right: window.pageXOffset + t.touches[0].clientX,
					top: window.pageYOffset + t.touches[0].clientY,
					bottom: window.pageYOffset + t.touches[0].clientY,
					zIndex: a.getZIndex(t.currentTarget)
				}, e)) : (t = s.toNode(t), this._changeActivePosition(s.getRealPosition(t), e))
			}, c.prototype._getFactory = function () {
				return l.createFactory({
					widget: this,
					defaultType: "menuItem",
					allowedTypes: ["menuItem", "spacer", "separator", "customHTML", "customHTMLButton"],
					widgetName: "context-menu"
				})
			}, c.prototype._close = function (t) {
				var e = this;
				this.events.on(l.NavigationBarEvents.afterHide, function () {
					e._activeMenu = null, e._changeActivePosition(null, null)
				}), r.prototype._close.call(this, t)
			}, c.prototype._getMode = function (t, e, i) {
				return i ? this._mode : "right"
			}, c.prototype._changeActivePosition = function (t, e) {
				this._activePosition = t, this._mode = e, this._listenOuterClick(), this.paint()
			}, c);

		function c() {
			var t = null !== r && r.apply(this, arguments) || this;
			return t._isContextMenu = !0, t
		}
		e.ContextMenu = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(0),
			a = i(19),
			o = (r = a.Navbar, o(l, r), l.prototype._getFactory = function () {
				return a.createFactory({
					widget: this,
					defaultType: "menuItem",
					allowedTypes: ["navItem", "menuItem", "spacer", "separator", "customHTML", "customHTMLButton"],
					widgetName: "menu-nav"
				})
			}, l.prototype._draw = function () {
				return s.el("ul.dhx_widget", {
					dhx_widget_id: this._uid,
					onmousemove: this._handlers.onmousemove,
					onmouseleave: this._handlers.onmouseleave,
					onclick: this._handlers.onclick,
					onmousedown: this._handlers.onmousedown,
					class: "dhx_menu-nav " + (this.config.css || "")
				}, this._drawMenuItems(this.data.getRoot(), !1))
			}, l);

		function l(t, e) {
			var i = r.call(this, t, e) || this;
			return i.mount(t, s.create({
				render: function () {
					return i._draw()
				}
			})), i
		}
		e.Menu = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(110),
			a = i(17),
			l = i(0),
			c = i(2),
			u = i(10),
			o = (r = s.Ribbon, o(d, r), d.prototype._draw = function () {
				var i = this;
				this._heightCalculate();
				var t = [l.el("ul.dhx_ribbon-content.dhx_ribbon-content--full-width", {
					style: {
						height: Math.max.apply(Math, this._widgetHeight)
					}
				}, this.data.map(function (t) {
					return "block" === t.type ? i._drawBlock(t, !0) : i._factory(t)
				}, this.data.getRoot(), !1))],
					e = Math.max.apply(Math, this._widgetHeight);
				return l.el("ul.dhx_ribbon.dhx_widget", {
					dhx_widget_id: this._uid,
					class: this.config.css || "",
					tabindex: 0,
					onclick: this._handlers.onclick,
					onmousedown: this._handlers.onmousedown,
					oninput: this._listeners.input,
					onmouseover: this._listeners.tooltip,
					_hooks: {
						didInsert: function (t) {
							t.el.addEventListener("keyup", function (t) {
								var e;
								9 !== t.which || (e = c.locateNode(document.activeElement)) && (t = e.getAttribute("dhx_id"), (t = i.data.getItem(t)).tooltip && u.tooltip(t.tooltip, {
									node: e,
									position: u.Position.bottom,
									force: !0
								}))
							}, !0)
						}
					}
				}, [l.el("li", {
					class: "dhx_ribbon-block dhx_ribbon-block--root",
					style: {
						height: this._haveTitle ? e + 24 : e
					}
				}, this.scrollView && this.scrollView.config.enable ? [].concat(this.scrollView.render(t)) : t)])
			}, d);

		function d(t, e) {
			var i = r.call(this, t, e) || this;
			return i.scrollView = new a.ScrollView(function () {
				return i.getRootView()
			}), i
		}
		e.ProRibbon = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(0),
			a = i(2),
			l = i(17),
			c = i(112),
			u = i(10),
			o = (r = c.Sidebar, o(d, r), d.prototype._draw = function () {
				var i = this,
					t = this.config,
					e = t.width,
					t = t.minWidth,
					t = this.config.collapsed ? t : e,
					e = [s.el("ul.dhx_navbar.dhx_navbar--vertical", {
						dhx_widget_id: this._uid,
						tabindex: 0,
						onclick: this._handlers.onclick,
						onmousedown: this._handlers.onmousedown,
						oninput: this._handlers.input,
						onmouseover: this._handlers.tooltip,
						_hooks: {
							didInsert: function (t) {
								t.el.addEventListener("keyup", function (t) {
									var e;
									9 !== t.which || (e = a.locateNode(document.activeElement)) && (t = e.getAttribute("dhx_id"), ((t = i.data.getItem(t)).tooltip || i.config.collapsed && t.value) && u.tooltip(t.tooltip || t.value, {
										node: e,
										position: u.Position.right,
										force: !0
									}))
								}, !0)
							}
						}
					}, this.data.map(function (t) {
						return i._factory(t)
					}, this.data.getRoot(), !1))];
				return s.el("nav.dhx_widget.dhx_sidebar", {
					class: (this.config.css || "") + (this.config.collapsed ? " dhx_sidebar--minimized" : ""),
					style: {
						width: t + "px"
					}
				}, this.scrollView && this.scrollView.config.enable ? [].concat(this.scrollView.render(e)) : e)
			}, d);

		function d(t, e) {
			var i = r.call(this, t, e) || this;
			return i.scrollView = new l.ScrollView(function () {
				return i.getRootView()
			}), i
		}
		e.ProSidebar = o
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(237)), n(e(114))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			},
			s = this && this.__spreadArrays || function () {
				for (var t = 0, e = 0, i = arguments.length; e < i; e++) t += arguments[e].length;
				for (var n = Array(t), o = 0, e = 0; e < i; e++)
					for (var r = arguments[e], s = 0, a = r.length; s < a; s++, o++) n[o] = r[s];
				return n
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var a, l = i(1),
			c = i(0),
			u = i(3),
			d = i(2),
			h = i(13),
			f = i(18),
			p = i(11),
			_ = i(114),
			o = (a = p.Layout, o(v, a), v.prototype.toVDOM = function () {
				var t = this;
				this._getTabContainer();
				var e, i = null;
				return this.config.noContent || (i = this.getCell(this.config.activeView)) && (e = this._disabled.includes(this.config.activeView) ? " dhx_tabbar-content--disabled" : "", i.config.css ? -1 !== i.config.css.indexOf("dhx_tabbar-content--disabled") ? i.config.css = i.config.css.replace("dhx_tabbar-content--disabled", "") : i.config.css = i.config.css + e : i.config.css = e), c.awaitRedraw().then(function () {
					t._tabsContainer || t.paint()
				}), c.el("div", {
					class: "dhx_widget dhx_tabbar" + (this.config.mode ? " dhx_tabbar--" + this.config.mode : "") + (this.config.css ? " " + this.config.css : "")
				}, this._tabsContainer ? s(this._drawTabs(), [i ? i.toVDOM() : null]) : [])
			}, v.prototype.destructor = function () {
				this.events && this.events.clear(), this._keyManager && this._keyManager.destructor(), a.prototype.destructor.call(this), this.unmount(), this._tabsContainer = this._beforeScrollSize = this._afterScrollSize = this._keyManager = null, this.config = this.events = this._cells = this._handlers = null
			}, v.prototype.getWidget = function () {
				var e = this;
				return this._cells.filter(function (t) {
					return e.getActive() === t.id
				})[0].getWidget()
			}, v.prototype.setActive = function (t) {
				var e;
				this._cells.map(function (t) {
					return t.id
				}).includes(t) && !this._disabled.includes(t) && (e = this.config.activeView, this.config.activeView = t, this.getCell(t).show(), this._focusTab(t), this.events.fire(_.TabbarEvents.change, [t, e]))
			}, v.prototype.getActive = function () {
				return this.config ? this.config.activeView : null
			}, v.prototype.addTab = function (t, e) {
				this.addCell(t, e), 1 !== this._cells.length || t.disabled || this.setActive(this._cells[0].id)
			}, v.prototype.removeTab = function (t) {
				var e = this;
				if (this.events.fire(_.TabbarEvents.beforeClose, [t])) {
					if (t === this.config.activeView) {
						var i = this._getEnableTabs().length,
							n = l.findIndex(this._getEnableTabs(), function (t) {
								return t.id === e.config.activeView
							});
						if (n < 0) return;
						n === i - 1 && --n, a.prototype.removeCell.call(this, t), 1 === i ? this.config.activeView = null : this.setActive(this._getEnableTabs()[n].id)
					} else a.prototype.removeCell.call(this, t);
					this.events.fire(_.TabbarEvents.afterClose, [t]), this.events.fire(_.TabbarEvents.close, [t])
				}
			}, v.prototype.disableTab = function (t) {
				return !(!this._cells.map(function (t) {
					return t.id
				}).includes(t) || this._disabled.includes(t)) && (this._disabled.push(t), this.paint(), !0)
			}, v.prototype.enableTab = function (e) {
				var t;
				this._disabled.includes(e) && (t = this._disabled.filter(function (t) {
					return t !== e
				}), this._disabled = s(t), this.paint())
			}, v.prototype.isDisabled = function (t) {
				return this._disabled.includes(t || this.config.activeView)
			}, v.prototype.removeCell = function (t) {
				this.removeTab(t)
			}, v.prototype._initHandlers = function () {
				var u = this;
				a.prototype._initHandlers.call(this), this._handlers = r(r({}, this._handlers), {
					onTabClick: function (i) {
						c.awaitRedraw().then(function () {
							var t, e = d.locate(i, "dhx_tabid");
							e && !u._disabled.includes(e) && (t = u.config.activeView, i.target.classList.contains("dhx_tabbar-tab__close") ? u.removeTab(e) : t !== (u.config.activeView = e) && u.events.fire(_.TabbarEvents.change, [u.config.activeView, t]), u.paint())
						})
					},
					onScrollClick: function (t) {
						var n = d.locate(t, "mode");
						if (!n) switch (t.key) {
							case "ArrowRight":
								n = "right";
								break;
							case "ArrowLeft":
								n = "left";
								break;
							case "ArrowUp":
								n = "up";
								break;
							case "ArrowDown":
								n = "down"
						}
						var o, r, s, a, l, c, t = {
							behavior: "smooth"
						};
						u._isHorizontalMode() ? (o = u._normalizeSize({
							width: u._getSizes(u._cells[0].config).width
						}).width, r = u._normalizeSize({
							width: u._getSizes(u._cells[u._cells.length - 1].config).width
						}).width, u._tabsContainer && (s = u._tabsContainer.scrollWidth, u._cells.reduce(function (t, e, i) {
							if (t >= u._tabsContainer.scrollLeft && 0 !== i && "left" === n) o = Math.abs(u._normalizeSize({
								width: u._getSizes(u._cells[i - 1].config).width
							}).width - (t - u._tabsContainer.scrollLeft));
							else {
								if (!(t > s + u._tabsContainer.scrollLeft && "right" === n)) return t + u._normalizeSize({
									width: u._getSizes(e.config).width
								}).width;
								r = Math.abs(s + u._tabsContainer.scrollLeft - t)
							}
						}, 0)), t.left = "left" === n ? u._tabsContainer.scrollLeft - o : u._tabsContainer.scrollLeft + r) : (a = u._normalizeSize({
							height: u._getSizes(u._cells[0].config).height
						}).height, l = u._normalizeSize({
							height: u._getSizes(u._cells[u._cells.length - 1].config).height
						}).height, u._tabsContainer && (c = u._tabsContainer.clientHeight, u._cells.reduce(function (t, e, i) {
							if (t >= u._tabsContainer.scrollTop && 0 !== i && "up" === n) a = Math.abs(u._normalizeSize({
								height: u._getSizes(u._cells[i - 1].config).height
							}).height - (t - u._tabsContainer.scrollTop));
							else {
								if (!(t > c + u._tabsContainer.scrollTop && "down" === n)) return t + u._normalizeSize({
									height: u._getSizes(e.config).height
								}).height;
								l = Math.abs(c + u._tabsContainer.scrollTop - t)
							}
						}, 0)), t.top = "up" === n ? u._tabsContainer.scrollTop - a : u._tabsContainer.scrollTop + l), d.isIE() ? (u._tabsContainer.scrollLeft = t.left, u._tabsContainer.scrollTop = t.top) : u._tabsContainer.scrollTo(t)
					},
					onHeaderScroll: l.debounce(function () {
						u.paint()
					}, 10)
				})
			}, v.prototype._isHorizontalMode = function () {
				return "bottom" === this.config.mode || "top" === this.config.mode
			}, v.prototype._focusTab = function (t, e) {
				var i = this;
				c.awaitRedraw().then(function () {
					i.getRootView().refs[t].el.focus(), e && i._handlers.onScrollClick(e)
				})
			}, v.prototype._getEnableTabs = function () {
				var e = this;
				return this._cells.filter(function (t) {
					return !e._disabled.includes(t.config.id)
				})
			}, v.prototype._getIndicatorPosition = function () {
				var n = this,
					o = l.findIndex(this._cells, function (t) {
						return t.id === n.config.activeView
					}); - 1 === o && (o = 0);
				var t = this.getCell(this.config.activeView);
				if (this._isHorizontalMode()) {
					var e = this._normalizeSize({
						width: this._getSizes(t.config).width
					}),
						i = e.width,
						e = e.unit,
						r = this._tabsContainer.clientWidth;
					return {
						left: 0,
						transform: "translateX(" + this._cells.reduce(function (t, e, i) {
							e = n._normalizeSize({
								width: n._getSizes(e.config).width
							});
							return "%" === e.unit && (e.width = r / 100 * e.width), i < o ? t + e.width : t
						}, 0) + "px)",
						transition: "all 0.1s ease",
						width: i + e,
						height: "2px"
					}
				}
				var i = this._normalizeSize({
					height: this._getSizes(t.config).height
				}),
					t = i.height,
					e = i.unit,
					s = this._tabsContainer.clientHeight;
				return {
					top: 0,
					transform: "translateY(" + this._cells.reduce(function (t, e, i) {
						e = n._normalizeSize({
							height: n._getSizes(e.config).height
						});
						return "%" === e.unit && (e.height = s / 100 * e.height), i < o ? t + e.height : t
					}, 0) + "px)",
					transition: "all 0.1s ease",
					height: t + e,
					width: "2px"
				}
			}, v.prototype._drawTabs = function () {
				var o = this;
				if (!this._cells.length) return [];
				this._beforeScrollSize = 0, this._afterScrollSize = 0;
				var t = this._isHorizontalMode(),
					e = t ? (i = this._tabsContainer.clientWidth, e = Math.round(this._cells.reduce(function (t, e) {
						return o._normalizeSize({
							width: o._getSizes(e.config).width
						}).width + t
					}, 0)), this._tabsContainer && i <= e ? (this._beforeScrollSize = this._tabsContainer.scrollLeft, this._afterScrollSize = e - (i + this._beforeScrollSize)) : i <= e && (this._afterScrollSize = e - i), {
						height: this.config.tabHeight || "45px",
						top: "top" === this.config.mode ? 0 : ""
					}) : (i = this._tabsContainer.clientHeight, e = Math.round(this._cells.reduce(function (t, e) {
						return o._normalizeSize({
							height: o._getSizes(e.config).height
						}).height + t
					}, 0)), this._tabsContainer && i <= e ? (this._beforeScrollSize = this._tabsContainer.scrollTop, this._afterScrollSize = e - (i + this._beforeScrollSize)) : this._afterScrollSize = e - i, {
						width: this.config.tabWidth || "200px",
						left: "left" === this.config.mode ? 0 : ""
					}),
					i = this._getIndicatorPosition();
				return [c.el(".dhx_tabbar-header__wrapper", {
					onscroll: this._handlers.onHeaderScroll,
					class: this.config.tabAlign && this._beforeScrollSize <= 0 && this._afterScrollSize <= 0 ? "dhx_tabbar-header__wrapper-" + this.config.tabAlign : ""
				}, [c.el("ul." + this.config.mode, {
					tabs_id: this._uid,
					class: "dhx_tabbar-header ",
					onclick: this._handlers.onTabClick
				}, s(this._cells.map(function (t) {
					var e = o.config,
						i = e.closable,
						n = e.closeButtons,
						e = e.activeView;
					return c.el("li", {
						class: "dhx_tabbar-tab" + (t.config.tabCss ? " " + t.config.tabCss : ""),
						dhx_tabid: t.id,
						role: "presentation",
						style: o._getSizes(t.config)
					}, [c.el("button.dhx_button.dhx_tabbar-tab-button" + (e === t.id ? ".dhx_tabbar-tab-button--active" : "") + (o._disabled.includes(t.config.id) ? ".dhx_tabbar-tab-button--disabled" : ""), {
						tabindex: "0",
						"aria-controls": t.id,
						id: "tab-content-" + t.id,
						"aria-selected": "" + (e === t.id),
						_ref: t.id.toString(),
						type: "button"
					}, [c.el("span.dhx_button__text", t.config.tab)]), Array.isArray(i) && i.includes(t.config.id) && !o._disabled.includes(t.config.id) || i && "boolean" == typeof i && !o._disabled.includes(t.config.id) || n && "boolean" == typeof n && !o._disabled.includes(t.config.id) ? c.el("div.dhx_tabbar-tab__close.dxi--small.dxi.dxi-close", {
						tabindex: 0,
						role: "button",
						"aria-pressed": "false"
					}) : null])
				}), [c.el(".dhx_tabbar-header-active", {
					style: i
				})]))]), 0 < this._beforeScrollSize && c.el(".dhx_tabbar_scroll", {
					class: "dxi dxi-chevron-" + (t ? "left" : "up") + " arrow-" + (t ? "left" : "up"),
					_key: "startArrow",
					onclick: this._handlers.onScrollClick,
					mode: t ? "left" : "up",
					style: e
				}), 0 < this._afterScrollSize && c.el(".dhx_tabbar_scroll", {
					class: "dxi dxi-chevron-" + (t ? "right" : "down") + " arrow-" + (t ? "right" : "down"),
					_key: "endArrow",
					onclick: this._handlers.onScrollClick,
					mode: t ? "right" : "down",
					style: e
				})]
			}, v.prototype._getSizes = function (t) {
				"number" == typeof t.tabWidth && (t.tabWidth = t.tabWidth + "px"), "number" == typeof t.tabHeight && (t.tabHeight = t.tabHeight + "px"), "number" == typeof this.config.tabWidth && (this.config.tabWidth = this.config.tabWidth + "px"), "number" == typeof this.config.tabHeight && (this.config.tabHeight = this.config.tabHeight + "px");
				var e = this.config.tabWidth || (this._isHorizontalMode() ? l.getStringWidth(t.tab.toUpperCase(), {
					font: "500 14.4px Arial"
				}) + 50 + "px" : "200px"),
					i = this.config.tabHeight || "45px";
				return this._isHorizontalMode() ? void 0 !== t.tabWidth && (e = t.tabWidth) : void 0 !== t.tabHeight && (i = t.tabHeight), (this.config.tabAutoWidth && !1 !== t.tabAutoWidth || t.tabAutoWidth) && void 0 === this.config.tabWidth && void 0 === t.tabWidth && (e = this._getTabAutoWidth()), (this.config.tabAutoHeight && !1 !== t.tabAutoHeight || t.tabAutoHeight) && void 0 === this.config.tabHeight && void 0 === t.tabHeight && (i = this._getTabAutoHeight()), {
					width: e,
					height: i
				}
			}, v.prototype._normalizeSize = function (t) {
				var e = {};
				if (1 <= Object.keys(t).length)
					for (var i in t) "number" == typeof t[i] ? e.unit = "px" : (t[i].includes("%") ? (e[i] = t[i].slice(0, -1), e.unit = "%") : t[i].includes("px") && (e[i] = t[i].slice(0, -2), e.unit = "px"), e[i] = parseFloat(e[i]));
				return e
			}, v.prototype._getTabAutoWidth = function () {
				var e = this,
					t = this._tabsContainer.clientWidth,
					i = 0,
					n = 0;
				return this._cells.forEach(function (t) {
					t.config.tabAutoWidth || e.config.tabAutoWidth && !1 !== t.config.tabAutoWidth ? t.config.tabWidth ? i += e._normalizeSize({
						width: t.config.tabWidth
					}).width : n++ : i += e._normalizeSize({
						width: e._getSizes(t.config).width
					}).width
				}), (t - i) / n + "px"
			}, v.prototype._getTabAutoHeight = function () {
				var e = this,
					t = this._tabsContainer.clientHeight,
					i = 0,
					n = 0;
				return this._cells.forEach(function (t) {
					t.config.tabAutoHeight || e.config.tabAutoHeight && !1 !== t.config.tabAutoHeight ? t.config.tabHeight ? i += e._normalizeSize({
						height: t.config.tabHeight
					}).height : n++ : i += e._normalizeSize({
						height: e._getSizes(t.config).height
					}).height
				}), (t - i) / n + "px"
			}, v.prototype._getTabContainer = function () {
				var t, e = this;
				this._tabsContainer ? this.getRootNode() ? (t = this.getRootNode() && this.getRootNode().getElementsByClassName("dhx_tabbar-header__wrapper")[0]) && this._tabsContainer !== t && (this._tabsContainer = t, this.paint()) : c.awaitRedraw().then(function () {
					return e.paint()
				}) : (this._tabsContainer = this.getRootNode(), this.paint())
			}, v.prototype._initHotkeys = function () {
				function t(t) {
					t.preventDefault();
					var e = o._getEnableTabs(),
						i = l.findIndex(e, function (t) {
							return t.id === o.config.activeView
						}),
						n = o.config.activeView; - 1 !== i && (i === e.length - 1 ? o.config.activeView = e[0].id : o.config.activeView = e[i + 1].id, o.events.fire(_.TabbarEvents.change, [o.config.activeView, n]), o._focusTab(o.config.activeView, t), o.paint())
				}

				function e(t) {
					t.preventDefault();
					var e = o._getEnableTabs(),
						i = l.findIndex(e, function (t) {
							return t.id === o.config.activeView
						}),
						n = o.config.activeView; - 1 !== i && (o.config.activeView = (0 === i ? e[e.length - 1] : e[i - 1]).id, o.events.fire(_.TabbarEvents.change, [o.config.activeView, n]), o._focusTab(o.config.activeView, t), o.paint())
				}
				var i, o = this,
					n = "right" === this.config.mode || "left" === this.config.mode,
					r = {
						arrowRight: t,
						arrowUp: n ? e : t,
						arrowLeft: e,
						arrowDown: n ? t : e
					};
				for (i in r) this._keyManager.addHotKey(i, r[i])
			}, v);

		function v(t, e) {
			var i, n = a.call(this, t, l.extend({
				mode: "top"
			}, e)) || this;
			return n._keyManager = new h.KeyManager(function () {
				return d.locate(document.activeElement, "tabs_id") === n._uid
			}), n._initHotkeys(), n.config.disabled && (e = n.config.disabled, i = n._cells.map(function (t) {
				return t.id
			}), Array.isArray(e) ? e.forEach(function (t) {
				i.includes(t) && !n._disabled.includes(t) && n._disabled.push(t)
			}) : i.includes(e) && !n._disabled.includes(e) && n._disabled.push(e), n.paint()), n.events = new u.EventSystem(n), f.focusManager.setFocusId(n._uid), n
		}
		e.Tabbar = o
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(239)), n(e(115)), n(e(116))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			u = this && this.__assign || function () {
				return (u = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var s = i(1),
			d = i(0),
			r = i(3),
			a = i(2),
			l = i(29),
			c = i(4),
			h = i(6),
			f = i(115),
			p = i(13),
			_ = i(18),
			v = i(116);

		function g(t, e) {
			return t ? e ? "openFolder" : "folder" : "file"
		}
		var m, o = (m = c.View, o(y, m), y.prototype.focusItem = function (t) {
			var e = this;
			this._focusId = t, this.data.eachParent(t, function (t) {
				t.opened || e.expand(t.id)
			}), this.events.fire(v.TreeEvents.focusChange, [this.data.getIndex(t), t]), this.paint()
		}, y.prototype.destructor = function () {
			this.events && this.events.clear(), this._keyManager && this._keyManager.destructor(), this.config = this.events = this.selection = null, this._editor = this._handlers = this._root = this._focusId = this._right = this._keyManager = this._touch = this._isDraget = null, this.unmount()
		}, y.prototype.editItem = function (t, e) {
			var i;
			this._editor.isEditable() || (i = this.data.getItem(t), this.events.fire(v.TreeEvents.beforeEditStart, [i.value, t]) && (this.data.update(t, {
				$edit: !0,
				$editConfig: e
			}, !0), this.events.fire(v.TreeEvents.afterEditStart, [i.value, t])))
		}, y.prototype.getState = function () {
			var e = {};
			return this.data.eachChild(this._root, function (t) {
				e[t.id] = {
					open: t.opened,
					selected: t.$mark
				}
			}, !0), e
		}, y.prototype.setState = function (e) {
			this.data.eachChild(this._root, function (t) {
				t.id in e && (t.opened = e[t.id].open, t.$mark = e[t.id].selected)
			}, !0), this.paint()
		}, y.prototype.toggle = function (t) {
			var e = this.data.getItem(t);
			e.$autoload ? this.events.fire(v.TreeEvents.beforeExpand, [t]) && (this.data.loadItems(t), this.data.update(t, {
				$autoload: !1,
				opened: !0
			}), this.events.fire(v.TreeEvents.afterExpand, [t])) : e.opened ? this.collapse(t) : this.expand(t)
		}, y.prototype.getChecked = function () {
			var e = [];
			return this.data.eachChild(this._root, function (t) {
				t.$mark === v.SelectStatus.selected && e.push(t.id)
			}), e
		}, y.prototype.checkItem = function (t) {
			t && this.data.getItem(t) && (this._updateItemCheck(t, v.SelectStatus.selected), this.paint())
		}, y.prototype.collapse = function (t) {
			this.data.haveItems(t) && this.events.fire(v.TreeEvents.beforeCollapse, [t]) && (this.data.update(t, {
				opened: !1
			}), this.events.fire(v.TreeEvents.afterCollapse, [t]))
		}, y.prototype.collapseAll = function () {
			var e = this;
			this.data.eachChild(this._root, function (t) {
				t = t.id;
				return e.collapse(t)
			}, !0)
		}, y.prototype.expand = function (t) {
			this.data.haveItems(t) && this.events.fire(v.TreeEvents.beforeExpand, [t]) && (this.data.update(t, {
				opened: !0
			}), this.events.fire(v.TreeEvents.afterExpand, [t]))
		}, y.prototype.expandAll = function () {
			var e = this;
			this.data.eachChild(this._root, function (t) {
				t = t.id;
				return e.expand(t)
			}, !0)
		}, y.prototype.uncheckItem = function (t) {
			t && this.data.getItem(t) && (this._updateItemCheck(t, v.SelectStatus.unselected), this.paint())
		}, y.prototype.close = function (t) {
			this.collapse(t)
		}, y.prototype.closeAll = function () {
			this.collapseAll()
		}, y.prototype.open = function (t) {
			this.expand(t)
		}, y.prototype.openAll = function () {
			this.expandAll()
		}, y.prototype.unCheckItem = function (t) {
			this.uncheckItem(t)
		}, y.prototype._draw = function () {
			this._getRightPos();
			var t = this._drawItems(this.data.getRoot());
			return d.el("ul", u({
				class: "dhx_widget dhx_tree" + (this._isSelectionActive ? "" : " dhx_tree--no-selection ") + (this.config.css ? " " + this.config.css : ""),
				dhx_widget_id: this._uid,
				tabindex: 0,
				dhx_root_id: this.config.rootId
			}, this._handlers), t)
		}, y.prototype._initEvents = function () {
			var r = this;
			this.data.events.on(h.DataEvents.change, function (t, e, i) {
				"remove" === e && r._updateParents(i.parent, !0), "add" === e && r._updateParents(t), r.paint()
			}), this.data.events.on(h.DataEvents.beforeRemove, function (t) {
				var e, i, n = t.id,
					o = t.parent;
				n == r._focusId && (e = r.data.findAll(function (t) {
					return t.parent === o
				}), i = o, 1 < e.length && (t = e.findIndex(function (t) {
					return t.id === n
				}), i = e[t === e.length - 1 ? e.length - 2 : t + 1].id), r.selection.remove(n), r.selection.add(i), r.focusItem(i))
			}), this._editor.events.on(f.EditorEvents.begin, function (t) {
				t === r._uid && (r.config.$editable = !0)
			}), this._editor.events.on(f.EditorEvents.end, function (t, e, i) {
				return !!r.events.fire(v.TreeEvents.beforeEditEnd, [i, e]) && (r._uid === t && r.data.update(e, {
					$edit: !1,
					value: i
				}), r.config.$editable = !1, void r.events.fire(v.TreeEvents.afterEditEnd, [i, e]))
			}), this.events.on(h.DragEvents.beforeDrag, function (t, e, i) {
				var n = t.start,
					t = r.data.getItem(n),
					n = g(r.config.isFolder ? r.config.isFolder(t) : r.data.haveItems(t.id), t.opened),
					n = (t.icon || r.config.icon)[n] || r.config.icon[n];
				i.innerHTML = '<div class="dhx_tree-list-item__icon ' + n + '"></div><span class="dhx_tree-list-item__text">' + (t.text || t.value) + "</span>"
			}), this.events.on(h.DragEvents.canDrop, function (t) {
				var e = t.target,
					t = t.dropPosition,
					t = "complex" === r.config.dropBehaviour ? "top" === t ? "dhx_tree-drop--top" : "bottom" === t ? "dhx_tree-drop--bottom" : "dhx_tree-drop--in-folder" : "child" === r.config.dropBehaviour ? "dhx_tree-drop--in-folder" : "dhx_tree-drop--bottom";
				r.data.exists(e) && r.data.update(e, {
					$drophere: t
				}, !0), r.paint()
			}), this.events.on(h.DragEvents.cancelDrop, function (t) {
				t = t.target;
				r.data.exists(t) && r.data.update(t, {
					$drophere: null
				}, !0)
			}), this.events.on(h.DragEvents.afterDrop, function (t) {
				var e = t.target,
					t = t.dropPosition;
				("child" === r.config.dropBehaviour || "complex" === r.config.dropBehaviour && "in" === t) && r.expand(e)
			}), this.events.on(h.DragEvents.dragStart, function () {
				r._isSelectionActive = !1, r.paint()
			}), this.events.on(h.DragEvents.afterDrag, function (t) {
				r._isSelectionActive = !0, r._isDraget = !0, r.data.exists(t.start) && r.selection.add(t.start), r.paint()
			}), this.events.on(v.TreeEvents.itemClick, function (t) {
				r._focusId = t, r.paint()
			}), this.events.on(l.SelectionEvents.afterSelect, function () {
				return r.paint()
			}), this.events.on(l.SelectionEvents.afterUnSelect, function () {
				return r.paint()
			})
		}, y.prototype._initHandlers = function () {
			var n = this;
			this._handlers = {
				onmouseleave: function (t) {
					h.dragManager.cancelCanDrop(t)
				},
				onclick: function (t) {
					if (n._isDraget) n._isDraget = !1;
					else {
						var e = a.locate(t);
						if (e)
							if (t.target.classList.contains("dhx_tree-toggle-button")) n.toggle(e);
							else {
								if (t.target.classList.contains("dhx_tree-checkbox")) {
									if (!n.events.fire(v.TreeEvents.beforeCheck, [n.data.getIndex(e), e])) return;
									var i = n.data.getItem(e);
									return i.$mark === v.SelectStatus.unselected ? n.checkItem(e) : n.uncheckItem(e), void n.events.fire(v.TreeEvents.afterCheck, [n.data.getIndex(e), e, !!i.$mark])
								}
								n.events.fire(v.TreeEvents.itemClick, [e, t]), n.data.exists(e) && n.selection.add(e)
							}
					}
				},
				ondblclick: function (t) {
					n._dblClick(t)
				},
				ondragstart: function (t) {
					t.preventDefault()
				},
				onmousedown: function (t) {
					n._dragStart(t)
				},
				ontouchstart: function (t) {
					n._touch.timer = setTimeout(function () {
						n._dragStart(t)
					}, n._touch.duration), n._touch.timeStamp ? (n._touch.dblDuration >= n._touch.timeStamp - +t.timeStamp.toFixed() && n._dblClick(t), n._touch.timeStamp = null) : n._touch.timeStamp = +t.timeStamp.toFixed(), setTimeout(function () {
						n._touch.timeStamp = null
					}, n._touch.dblDuration)
				},
				ontouchmove: function (t) {
					n._touch.start && t.preventDefault(), n._clearTouchTimer()
				},
				ontouchend: function () {
					n._touch.start = !1, n._clearTouchTimer()
				},
				oncontextmenu: function (t) {
					var e = a.locate(t);
					e && (n.events.fire(v.TreeEvents.itemRightClick, [e, t]), n.events.fire(v.TreeEvents.itemContextMenu, [e, t]))
				}
			};
			var t = this.config.eventHandlers;
			if (t)
				for (var e = 0, i = Object.entries(t); e < i.length; e++) {
					var o = i[e],
						r = o[0],
						o = o[1];
					this._handlers[r] = a.eventHandler(function (t) {
						var e = u({}, n.data.getItem(a.locate(t)));
						return Object.keys(e).forEach(function (t) {
							t.startsWith("$") && delete e[t]
						}), e
					}, o, this._handlers[r])
				}
		}, y.prototype._dblClick = function (t) {
			var e = a.locate(t);
			e && (this.events.fire(v.TreeEvents.itemDblClick, [e, t]), this.config.editable && this.editItem(e))
		}, y.prototype._clearTouchTimer = function () {
			this._touch.timer && (clearTimeout(this._touch.timer), this._touch.timer = null)
		}, y.prototype._dragStart = function (t) {
			this.config.dragMode && "target" !== this.config.dragMode && (this._editor.isEditable() || (this._touch.start = !0, h.dragManager.onMouseDown(t)))
		}, y.prototype._getRightPos = function (t, e) {
			if (void 0 === e && (e = 0), !t)
				for (var i = this.data.getRoot(), n = 0, o = this.data.findAll(function (t) {
					return t.parent === i
				}); n < o.length; n++) {
					var r = o[n];
					return this._getRightPos(r.id, e)
				}
			t = this.data.getItem(t);
			if (t && (this._right = e, this.data.haveItems(t.id) && t.opened && t.items))
				for (var s = 0, a = t.items; s < a.length; s++) {
					var l = a[s];
					this._getRightPos(l.id, e + 1)
				}
		}, y.prototype._drawItems = function (t, l) {
			var c = this;
			return void 0 === l && (l = 0), this.data.getRawData(0, -1, null, 0, t).map(function (t) {
				if (t) {
					var e, i, n, o = !!c.config.isFolder && c.config.isFolder(t);
					(t.$autoload || c.data.haveItems(t.id)) && (o = !0, e = d.el("div", {
						class: "dxi dxi-menu-right dhx_tree-toggle-button"
					}, ""), t.opened && (e = d.el("div", {
						class: "dxi dxi-menu-down dhx_tree-toggle-button dhx_tree-toggle-button--open"
					}, ""), i = c._drawItems(t.id, l + 1))), t.checkbox && (s = t.$mark === v.SelectStatus.indeterminate ? "dxi-minus-box" : t.$mark === v.SelectStatus.selected ? "dxi-checkbox-marked" : "dxi-checkbox-blank-outline", n = d.el("div", {
						class: "dhx_tree-checkbox dxi " + s
					}));
					var r = g(o, t.opened),
						s = (t.icon || c.config.icon)[r] || c.config.icon[r];
					r = t.$edit ? c._editor.edit(c._uid, u({
						item: t
					}, t.$editConfig)) : d.el("span", {
						class: "dhx_tree-list-item__text"
					}, t.text || t.value), c.config.template && (r = "string" == typeof (a = c.config.template(t, o)) ? d.el("div.dhx_tree-template__wrapper", {
						".innerHTML": a
					}) : r);
					var a = c.config.itemHeight || null;
					return o ? d.el("li", {
						class: "dhx_tree-list-item dhx_tree-list-item--parent" + (t.css ? " " + t.css : ""),
						dhx_id: t.id,
						_key: t.id
					}, [d.el("div.dhx_tree-folder", {
						class: (t.id == c._focusId ? " dhx_tree-folder--focused" : "") + (t.$selected ? " dhx_tree-folder--selected" : "") + (t.$drophere ? " " + t.$drophere : ""),
						style: {
							left: -20 * l + "px",
							right: 0,
							"margin-left": 20 * l + "px",
							minHeight: a
						}
					}, [e, d.el("div.dhx_tree-list-item__content", [n, d.el("div", {
						class: "dhx_tree-list-item__icon " + s
					}), r])]), i && d.el("ul.dhx_tree-list", i)]) : d.el("li", {
						class: "dhx_tree-list__item dhx_tree-list-item" + (t.id == c._focusId ? " dhx_tree-list-item--focused" : "") + (t.$selected ? " dhx_tree-list-item--selected" : "") + (t.$drophere ? " " + t.$drophere : "") + (o ? "dhx_tree-list-item--folder" : "") + (t.css ? " " + t.css : ""),
						style: {
							left: -20 * l + "px",
							right: 0,
							"margin-left": 20 * l + "px",
							minHeight: a
						},
						dhx_id: t.id,
						level: l
					}, [e, d.el("div", {
						class: "dhx_tree-list-item__content"
					}, [n, d.el("div", {
						class: "dhx_tree-list-item__icon " + s
					}), r])])
				}
			})
		}, y.prototype._updateItemCheck = function (t, e) {
			this.data.update(t, {
				$mark: e
			}, !0), this.data.eachChild(t, function (t) {
				return t.$mark = e
			}), this._updateParents(t)
		}, y.prototype._updateParents = function (t, e) {
			var n = this;
			void 0 === e && (e = !1), t !== this._root && this.data.eachParent(t, function (t) {
				var e = 0,
					i = 0;
				n.data.eachChild(t.id, function (t) {
					if (t.checkbox) switch (t.$mark) {
						case v.SelectStatus.unselected:
							i++;
							break;
						case v.SelectStatus.selected:
							e++
					}
				}, !0), t.$mark = 0 !== e && 0 !== i ? v.SelectStatus.indeterminate : 0 === e && 0 !== i ? v.SelectStatus.unselected : v.SelectStatus.selected
			}, e)
		}, y.prototype._initHotkeys = function () {
			function i() {
				var t = o._focusId;
				return t || (t = o.data.getRoot(), o.data.getItems(t)[0].id)
			}
			var t, o = this,
				r = function (e, t) {
					void 0 === t && (t = !1);
					var i = o.data.getItem(e);
					if (o.data.haveItems(e) && i.opened && !t) return o.data.getItems(e)[0].id;
					var n = o.data.getParent(e),
						i = o.data.getItems(n),
						t = s.findIndex(i, function (t) {
							return t.id === e
						});
					return t + 1 < i.length ? i[t + 1].id : n === o.data.getRoot() ? null : r(n, !0)
				},
				e = {
					arrowLeft: function (t) {
						t.preventDefault();
						var e = i(),
							t = o.data.getParent(e);
						o.data.getRoot() !== t ? !o.data.getItem(e).opened ? o.focusItem(t) : e !== o.data.getRoot() && o.collapse(e) : o.collapse(e)
					},
					arrowRight: function (t) {
						t.preventDefault();
						t = i();
						if (o.data.getItem(t).$autoload) return o.events.fire(v.TreeEvents.beforeExpand, [t]) ? (o.data.loadItems(t), o.data.update(t, {
							$autoload: !1,
							opened: !0
						}), void o.events.fire(v.TreeEvents.afterExpand, [t])) : void 0;
						o.data.haveItems(t) && o.expand(t)
					},
					arrowUp: function (t) {
						t.preventDefault();
						t = function (t) {
							var e = o.data.getIndex(t),
								t = o.data.getParent(t);
							if (0 < e) {
								var i = o.data.getItems(t)[e - 1];
								if (!o.data.haveItems(i.id) || !i.opened) return i.id;
								for (; o.data.haveItems(i.id) && i.opened;) var n = o.data.getItems(i.id),
									i = n[n.length - 1];
								return i.id
							}
							return t === o.data.getRoot() ? null : t
						}(i());
						t && o.focusItem(t)
					},
					arrowDown: function (t) {
						t.preventDefault();
						t = i(), t = r(t);
						t && o.focusItem(t)
					},
					enter: function () {
						var t = i();
						t && o.selection.add(t)
					}
				};
			for (t in e) this._keyManager.addHotKey(t, e[t])
		}, y);

		function y(t, e) {
			void 0 === e && (e = {});
			var i = m.call(this, t, e) || this;
			i._touch = {
				duration: 350,
				dblDuration: 300,
				timer: null,
				start: !1,
				timeStamp: null
			}, i.config = s.extend({
				dropBehaviour: "child",
				icon: {
					file: "dxi dxi-file-outline",
					folder: "dxi dxi-folder",
					openFolder: "dxi dxi-folder-open"
				},
				keyNavigation: !0,
				editable: !1,
				selection: !0,
				rootId: "string" == typeof t && t || i._uid
			}, e), i.config.editable = i.config.editable || i.config.editing;
			e = function (t) {
				return t.$mark = v.SelectStatus.unselected, t.checkbox = i.config.checkbox, t.$autoload = Boolean(t.items && "string" == typeof i.config.autoload), t.$editor = !1, t
			};
			Array.isArray(i.config.data) ? (i.events = new r.EventSystem(i), i.data = new h.TreeCollection({
				autoload: i.config.autoload,
				init: e,
				rootId: i.config.rootId || t
			}, i.events), i.data.parse(i.config.data)) : i.config.data && i.config.data.events ? (i.data = i.config.data, i.data.config.init = e, i.events = i.data.events, i.events.context = i) : (i.events = new r.EventSystem(i), i.data = new h.TreeCollection({
				autoload: i.config.autoload,
				init: e,
				rootId: i.config.rootId || t
			}, i.events)), i._isSelectionActive = !0, i.selection = new h.Selection({
				disabled: !i.config.selection
			}, i.data, i.events), i.config.keyNavigation && (i._keyManager = new p.KeyManager(function (t, e) {
				return e === i._uid
			}), i._initHotkeys()), i._editor = new f.Editor, i._initEvents(), i._initHandlers(), i.config.dragMode && h.dragManager.setItem(i._uid, i), i._root = i.data.getRoot();
			return i.mount(t, d.create({
				render: function () {
					return i._draw()
				}
			})), _.focusManager.setFocusId(i._uid), i
		}
		e.Tree = o
	}, function (t, e, i) {
		"use strict";
		(function (t) {
			Object.defineProperty(e, "__esModule", {
				value: !0
			}), e.detectDrag = function (r) {
				return new t(function (e) {
					var i = function () {
						document.removeEventListener("mousemove", o), document.removeEventListener("mouseup", i), e(!1)
					},
						n = setTimeout(function () {
							i()
						}, 1e3),
						o = function (t) {
							(4 < Math.abs(t.pageX - r.pageX) || 4 < Math.abs(t.pageY - r.pageY)) && (document.removeEventListener("mousemove", o), document.removeEventListener("mouseup", i), clearTimeout(n), e({
								x: r.pageX,
								y: r.pageY
							}))
						};
					document.addEventListener("mousemove", o), document.addEventListener("mouseup", i)
				})
			}
		}).call(this, i(15))
	}, function (t, e, i) {
		"use strict";
		Object.defineProperty(e, "__esModule", {
			value: !0
		}), e.default = {
			popups: {},
			lastActive: null,
			freeCount: 0,
			add: function (t, e) {
				this.lastActive && this.popups[this.lastActive].classList.remove("dhx_popup--window_active"), this.lastActive = t, e.classList.add("dhx_popup--window_active"), this.popups[t] = e
			},
			setActive: function (t) {
				var e;
				t === this.lastActive || (e = this.popups[t]) && (this.lastActive && this.popups[this.lastActive].classList.remove("dhx_popup--window_active"), this.lastActive = t, e.classList.add("dhx_popup--window_active"))
			},
			openFreeWindow: function (t) {
				0 === this.freeCount && t.classList.add("dhx_window--no-scroll"), this.freeCount++
			},
			closeFreeWindow: function (t) {
				this.freeCount--, 0 === this.freeCount && t.classList.remove("dhx_window--no-scroll")
			}
		}
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(118),
			a = i(25),
			l = i(11),
			c = i(19),
			f = i(119),
			o = (r = s.Window, o(u, r), u.prototype._startResize = function (r) {
				var t, e, s = this,
					a = 100 | this.config.minWidth,
					l = 100 | this.config.minHeight,
					c = this._popup.offsetLeft,
					u = this._popup.offsetTop,
					d = this._popup.offsetWidth,
					h = this._popup.offsetHeight,
					i = this.getRootView().refs;
				switch (!0) {
					case r.bottom && r.left:
						e = "dhx_window-body-pointer--bottom_left", t = i.bottomLeft;
						break;
					case r.bottom && r.right:
						e = "dhx_window-body-pointer--bottom_right", t = i.bottomLeft;
						break;
					case r.top && r.left:
						e = "dhx_window-body-pointer--top_left", t = i.bottomLeft;
						break;
					case r.top && r.right:
						e = "dhx_window-body-pointer--top-right", t = i.right;
						break;
					case r.top:
						e = "dhx_window-body-pointer--top", t = i.bottomLeft;
						break;
					case r.bottom:
						e = "dhx_window-body-pointer--bottom", t = i.bottomLeft;
						break;
					case r.left:
						e = "dhx_window-body-pointer--left", t = i.bottomLeft;
						break;
					case r.right:
						e = "dhx_window-body-pointer--right", t = i.right
				}
				t.el.classList.add("dhx_window-resizer--active"), this.config.node.classList.add("dhx_window--stop_selection"), this.config.node.classList.add(e);

				function n(t) {
					var e = (o = s._getContainerParams()).containerInnerWidth,
						i = o.containerInnerHeight,
						n = o.containerXOffset,
						o = o.containerYOffset,
						t = {
							width: s._notInNode() ? t.pageX - c : t.pageX - s.config.node.offsetLeft - c,
							height: s._notInNode() ? t.pageY - u : t.pageY - s.config.node.offsetTop - u,
							left: s._notInNode() ? t.pageX : t.pageX - s.config.node.offsetLeft,
							top: s._notInNode() ? t.pageY : t.pageY - s.config.node.offsetTop
						};
					r.right && (t.width < a ? t.width = a : t.width > n + e - c && (t.width = n + e - c), s._popup.style.width = t.width + "px"), r.bottom && (t.height < l ? t.height = l : t.height > o + i - u && (t.height = o + i - u), s._popup.style.height = t.height + "px"), r.left && (c + d - t.left < a && (t.left = c + d - a), t.width = c + d - t.left, s.config.left = t.left, s._popup.style.left = t.left + "px", s._popup.style.width = t.width + "px"), r.top && (t.top < o ? t.top = o : u + h - t.top < l && (t.top = u + h - l), t.height = u + h - t.top, s.config.top = t.top, s._popup.style.top = t.top + "px", s._popup.style.height = t.height + "px"), s.config.width = s._popup.offsetWidth, s.config.height = s._popup.offsetHeight, s.events.fire(f.WindowEvents.resize, [t, {
						left: c,
						top: u,
						height: h,
						width: d
					}, r]), s.scrollView && s.scrollView.update()
				}
				var o = function () {
					document.removeEventListener("mouseup", o), document.removeEventListener("mousemove", n), s.config.node.classList.remove("dhx_window--stop_selection"), s.config.node.classList.remove(e), t.el.classList.remove("dhx_window-resizer--active")
				};
				document.addEventListener("mouseup", o), document.addEventListener("mousemove", n)
			}, u.prototype._initUI = function () {
				var i = this,
					t = [],
					e = (this.config.header || this.config.title || this.config.closable || this.config.movable) && !1 !== this.config.header;
				e && t.push({
					id: "header",
					height: "content",
					css: "dhx_window-header " + (this.config.movable ? "dhx_window-header--movable" : ""),
					on: {
						mousedown: this.config.movable && this._handlers.move,
						dblclick: this._handlers.headerDblClick
					}
				}), t.push({
					id: "content",
					css: e ? "dhx_window-content" : "dhx_window-content-without-header"
				}), this.config.footer && t.push({
					id: "footer",
					height: "content",
					css: "dhx_window-footer"
				}), this.config.resizable && t.push({
					id: "resizers",
					height: "content",
					css: "resizers"
				});
				var n, o = this._layout = new l.ProLayout(this._popup, {
					css: "dhx_window" + (this.config.modal ? " dhx_window--modal" : ""),
					rows: t,
					on: {
						click: this._handlers.setActive
					},
					id: this._uid
				}),
					t = this._layout.getCell("content");
				t && t.scrollView && (this.scrollView = t.scrollView), e && (n = this.header = new a.Toolbar, this.config.title && (this.header.data.add({
					type: "title",
					value: this.config.title,
					id: "title",
					css: "title_max"
				}), this._popup.setAttribute("aria-label", this.config.title)), this.config.closable && (this.header.data.add({
					type: "spacer"
				}), this.header.data.add({
					id: "close",
					type: "button",
					view: "link",
					size: "medium",
					color: "secondary",
					circle: !0,
					icon: "dxi dxi-close"
				}), n.events.on(c.NavigationBarEvents.click, function (t, e) {
					"close" === t && i._hide(e)
				})), o.getCell("header").attach(n)), this.config.footer && (n = this.footer = new a.Toolbar, o.getCell("footer").attach(n)), this.config.resizable && o.getCell("resizers").attach(function () {
					return i._drawResizers()
				})
			}, u);

		function u(t) {
			return r.call(this, t) || this
		}
		e.ProWindow = o
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		}),
			r = this && this.__assign || function () {
				return (r = Object.assign || function (t) {
					for (var e, i = 1, n = arguments.length; i < n; i++)
						for (var o in e = arguments[i]) Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
					return t
				}).apply(this, arguments)
			};
		Object.defineProperty(e, "__esModule", {
			value: !0
		});

		function s(t, e) {
			return (t || "") + " (" + e.length + ")"
		}

		function a(t, e) {
			return p.getTreeCell(e.groupTitleTemplate(t.$groupName, t.items), t, {
				id: "$groupName",
				$width: "100%",
				$cellCss: {}
			}, e)
		}
		var l, p = i(27),
			_ = i(1),
			c = i(6),
			u = i(121),
			d = i(122),
			o = (l = p.ProGrid, o(h, l), h.prototype.scrollTo = function (e, i) {
				var t = this.selection.getCell(),
					n = this.config.columns.filter(function (t) {
						return !t.hidden
					}),
					o = _.findIndex(n, function (t) {
						return t.id == i
					}),
					r = t ? t.column : this.config.columns[0],
					s = _.findIndex(n, function (t) {
						return t.id == r.id
					}),
					a = this.config.leftSplit ? p.getTotalWidth(n.slice(0, this.config.leftSplit)) : 0,
					l = p.getTotalWidth(n.slice(0, o)) - (o - s < 0 ? a : 0),
					c = this.data.mapVisible(function (t) {
						return t
					}),
					u = _.findIndex(c, function (t) {
						return t.id == e
					}),
					d = p.getTotalHeight(c.slice(0, u)),
					h = this.getScrollState(),
					f = this.config.width + h.x,
					t = this.config.height + h.y - this.config.headerRowHeight * this.config.$headerLevel,
					s = d - h.y - c[u].$height,
					a = l - h.x - n[o].$width,
					t = d + 2 * c[u].$height + 18 - t,
					f = l + 2 * n[o].$width + 18 - f,
					t = 0 < s && t < 0 ? 0 : s < 0 ? s : t,
					f = 0 < a && f < 0 ? 0 : a < 0 ? a : f;
				this.scroll(f + h.x, t + h.y)
			}, h.prototype.expand = function (t) {
				this.data.haveItems(t) && this.events.fire(d.TreeGridEvents.beforeExpand, [t]) && (this.data.update(t, {
					$opened: !0
				}), this.events.fire(d.TreeGridEvents.afterExpand, [t]))
			}, h.prototype.collapse = function (t) {
				this.data.haveItems(t) && this.events.fire(d.TreeGridEvents.beforeCollapse, [t]) && (this.data.update(t, {
					$opened: !1
				}), this.events.fire(d.TreeGridEvents.afterCollapse, [t]))
			}, h.prototype.expandAll = function () {
				var e = this;
				this.data.eachChild(this.data.getRoot(), function (t) {
					t = t.id;
					return e.expand(t)
				})
			}, h.prototype.collapseAll = function () {
				var e = this;
				this.data.forEach(function (t) {
					e.data.haveItems(t.id) && e.collapse(t.id)
				})
			}, h.prototype.groupBy = function (t) {
				var e = this;
				this.ungroup(), this.config.groupTitleTemplate = this.config.groupTitleTemplate || s, this._pregroupData = this._serialize();
				t = this._groupBy(this._serialize(), t);
				this.data.parse(t), t.forEach(function (t) {
					e.addRowCss(t.id, "dhx_tree-cell_group-title")
				})
			}, h.prototype.ungroup = function () {
				this._pregroupData && (this.data.parse(this._pregroupData), this._pregroupData = null)
			}, h.prototype.showRow = function (t) {
				var e, i = this;
				!_.isDefined(t) || (e = this.data.getItem(t)) && e.hidden && this.events.fire(p.GridEvents.beforeRowShow, [e]) && (this.data.update(t, {
					hidden: !1,
					wasHidden: !1
				}), this.data.restoreOrder(), this.data.eachChild(t, function (t) {
					t.wasHidden || i.data.update(t.id, {
						hidden: !1
					})
				}), this.data.filter(function (t) {
					return !t.hidden
				}), this.events.fire(p.GridEvents.afterRowShow, [e]))
			}, h.prototype.hideRow = function (t) {
				var e, i = this;
				!_.isDefined(t) || (e = this.data.getItem(t)) && !e.hidden && this.events.fire(p.GridEvents.beforeRowShow, [e]) && (this.data.update(t, {
					hidden: !0,
					wasHidden: !0
				}), this.data.eachChild(t, function (t) {
					return i.data.update(t.id, {
						hidden: !0
					})
				}), this.data.filter(function (t) {
					return !t.hidden
				}), this.events.fire(p.GridEvents.afterRowShow, [e]))
			}, h.prototype.getCellRect = function (e, i) {
				var t = this.config.columns.filter(function (t) {
					return !t.hidden
				}),
					n = this.data.mapVisible(function (t) {
						return t
					}),
					o = _.findIndex(t, function (t) {
						return t.id == i
					}),
					r = _.findIndex(n, function (t) {
						return t.id == e
					});
				return {
					x: p.getTotalWidth(t.slice(0, o)),
					y: p.getTotalHeight(n.slice(0, r)),
					height: n[r] ? n[r].$height : 0,
					width: t[o] ? t[o].$width : 0
				}
			}, h.prototype._adjustColumnsWidth = function (t, e, i) {
				var n;
				void 0 === i && (i = this.config.adjust);
				var o = {};
				if ("header" === i || !0 === i) {
					var r = e.filter(function (t) {
						return t.header
					});
					if (r = p.getMaxColsWidth(this._prepareColumnData(r, "header"), r, {
						font: "bold 14.4px Arial"
					}, "header"))
						for (var s = 0, a = Object.entries(r); s < a.length; s++) var l = a[s],
							c = l[0],
							u = l[1],
							o = Object.assign(o, ((l = {})[c] = +u + (p.isSortable(this.config, this.getColumn(c)) ? 36 : 16), l))
				}
				if (("data" === i || !0 === i) && (r = p.getMaxColsWidth(t, e, {
					font: "normal 14.4px Arial"
				}, "data")))
					for (var d = 0, h = Object.entries(r); d < h.length; d++) {
						var f = h[d],
							c = f[0],
							u = f[1],
							f = (null === (n = e[0]) || void 0 === n ? void 0 : n.id) === c ? 24 * this.data.getMaxLevel() + 16 : 16;
						(o[c] && o[c] < +u + f || !o[c]) && (o = Object.assign(o, ((n = {})[c] = +u + f, n)))
					}
				return o
			}, h.prototype._createCollection = function (t) {
				this.data = new u.TreeGridCollection({
					prep: t,
					rootId: this.config.rootParent
				}, this.events)
			}, h.prototype._getRowIndex = function (e) {
				return _.findIndex(this._serialize(), function (t) {
					return t.id == e
				})
			}, h.prototype._setEventHandlers = function () {
				var n = this;
				l.prototype._setEventHandlers.call(this), this.events.on(p.GridEvents.expand, function (t) {
					n.data.getItem(t).$opened ? n.collapse(t) : n.expand(t)
				}), this.events.detach(p.GridEvents.filterChange, this), this.events.on(p.GridEvents.filterChange, function (t, e, i) {
					t = null != t ? t : "", n._activeFilters || (n._activeFilters = {}), "" !== t ? n._activeFilters[e] = {
						by: e,
						match: t,
						compare: n.content[i].match
					} : delete n._activeFilters[e], n.data.filter(), n.data.filter(n._activeFilters, {
						add: !0
					})
				})
			}, h.prototype._groupBy = function (t, n) {
				var e = {};
				return t.reduce(function (t, e) {
					var i = "function" != typeof n ? e[n] && e[n].toString() : n(e);
					return t[i] || (t[i] = []), t[i].push(e), t
				}, e), Object.entries(e).map(function (t) {
					var e = t[0],
						t = t[1];
					return t.forEach(function (t) {
						t.parent = "$group::" + e
					}), {
						id: "$group::" + e,
						$groupName: e,
						$customRender: a,
						items: t
					}
				})
			}, h.prototype._serialize = function (t) {
				var i = this;
				void 0 === t && (t = c.DataDriver.json);
				var n = [];
				this.data.eachChild(this.data.getRoot(), function (t) {
					var e;
					t && (e = r(r({}, t), {
						$level: t.$level || i.data.getLevel(t.id),
						$items: i.data.haveItems(t.id)
					}), i.data.haveItems(t.id) && void 0 === t.$opened && (t.$opened = e.$opened = !0), n.push(e))
				});
				t = c.toDataDriver(t);
				if (t) return t.serialize(n)
			}, h);

		function h(t, e) {
			var i = this;
			return e.keyNavigation = !1, e.multiselection = !1, (i = l.call(this, t, e) || this).config.dropBehaviour || (i.config.dropBehaviour = "complex"), i.config.type = "tree", i
		}
		e.TreeGrid = o
	}, function (t, i, e) {
		"use strict";

		function n(t) {
			for (var e in t) i.hasOwnProperty(e) || (i[e] = t[e])
		}
		Object.defineProperty(i, "__esModule", {
			value: !0
		}), n(e(245)), n(e(123))
	}, function (t, e, i) {
		"use strict";
		var n, o = this && this.__extends || (n = function (t, e) {
			return (n = Object.setPrototypeOf || {
				__proto__: []
			}
				instanceof Array && function (t, e) {
					t.__proto__ = e
				} || function (t, e) {
					for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i])
				})(t, e)
		}, function (t, e) {
			function i() {
				this.constructor = t
			}
			n(t, e), t.prototype = null === e ? Object.create(e) : (i.prototype = e.prototype, new i)
		});
		Object.defineProperty(e, "__esModule", {
			value: !0
		});
		var r, s = i(1),
			a = i(0),
			l = i(3),
			c = i(6),
			u = i(4),
			d = i(123),
			h = i(25),
			o = (r = u.View, o(f, r), f.prototype.paint = function () {
				this._updateSizeUI()
			}, f.prototype.destructor = function () {
				this._toolbar.destructor(), this.events.clear(), this.unmount()
			}, f.prototype.setPage = function (t, e) {
				void 0 === e && (e = !1);
				var i = Math.max(0, Math.min(t, this.getPagesCount() - 1)),
					t = this._page;
				i !== t || e ? (this._page = i, e = this._page * this._size, this.data.setRange(e, e + this._size), this._toolbar.data.update("count", {
					value: (i + 1).toString()
				}), this.paint(), this.events.fire(d.PaginationEvents.change, [i, t]), this.data.events.fire(c.DataEvents.change, [void 0, "setPage", [i * this._size, t * this._size]])) : this.paint()
			}, f.prototype.getPage = function () {
				return this._page
			}, f.prototype.setPageSize = function (t) {
				this._size = t, this.setPage(this._page, !0)
			}, f.prototype.getPageSize = function () {
				return this._size
			}, f.prototype.getPagesCount = function () {
				return Math.ceil(this.data.getLength() / this._size)
			}, f.prototype._showItem = function (t) {
				this.setPage(Math.floor(t / this._size))
			}, f.prototype._showTreeItem = function (t) {
				var e = this.data.getItem(t);
				this.data.getItem(e.parent) ? this._showTreeItem(e.parent) : this.setPage(Math.floor(this.data.getIndex(t) / this._size))
			}, f.prototype._updateSizeUI = function () {
				this._toolbar.data.update("size", {
					value: "/ " + this.getPagesCount()
				})
			}, f.prototype._initUI = function () {
				this._toolbar = new h.Toolbar(null, {
					data: [{
						id: "first",
						type: "button",
						icon: "dxi dxi-chevron-double-left",
						tooltip: "First",
						view: "link",
						circle: !0
					}, {
						id: "previous",
						type: "button",
						icon: "dxi dxi-chevron-left",
						tooltip: "Previous",
						view: "link",
						circle: !0
					}, {
						id: "count",
						type: "input",
						width: this.config.inputWidth,
						css: "dhx_pagination__count-panel"
					}, {
						id: "size",
						type: "title",
						css: "dhx_pagination__count-panel"
					}, {
						id: "next",
						type: "button",
						icon: "dxi dxi-chevron-right",
						tooltip: "Next",
						view: "link",
						circle: !0
					}, {
						id: "last",
						type: "button",
						icon: "dxi dxi-chevron-double-right",
						tooltip: "Last",
						view: "link",
						circle: !0
					}]
				}), this._updateSizeUI()
			}, f.prototype._initEvents = function () {
				var i = this;
				this.data.events.on("focusChange", function (t, e) {
					i.data instanceof c.TreeCollection ? i._showItem(t) : i._showTreeItem(e)
				}), this.data.events.on(c.DataEvents.change, function () {
					i.setPage(i._page)
				}), this._toolbar.events.on("click", function (t) {
					var e;
					switch (t) {
						case "previous":
							e = i._page - 1;
							break;
						case "next":
							e = i._page + 1;
							break;
						case "first":
							e = 0;
							break;
						case "last":
							e = 1 / 0
					}
					void 0 !== e && i.setPage(e)
				}), this._toolbar.events.on("inputFocus", function (t) {
					var e = document.querySelector(".dhx_pagination[dhx_widget_id=" + i._uid + "] .dhx_input");
					e && e.select()
				}), this._toolbar.events.on("inputBlur", function (t) {
					var e = document.querySelector(".dhx_pagination[dhx_widget_id=" + i._uid + "] .dhx_input");
					e && !e.value && (e.value = (i._page + 1).toString())
				}), this._toolbar.events.on(c.DataEvents.change, function (t) {
					"count" === t && (t = parseInt(i._toolbar.data.getItem(t).value), isNaN(t) || i.setPage(t - 1))
				})
			}, f.prototype._render = function () {
				return a.el(".dhx_widget.dhx_pagination", {
					class: this.config.css,
					dhx_widget_id: this._uid,
					_key: this._uid
				}, [a.inject(this._toolbar.getRootView())])
			}, f);

		function f(t, e) {
			var i = r.call(this, t, s.extend({
				page: 0,
				pageSize: 10,
				inputWidth: 40
			}, e)) || this;
			i.data = i.config.data, i._page = -1, i._size = i.config.pageSize, i.events = new l.EventSystem(i), i._initUI(), i._initEvents();
			e = a.create({
				render: function () {
					return i._render()
				}
			});
			return i.mount(t, e), i.setPage(i.config.page), i
		}
		e.Pagination = o
	}], o.c = n, o.d = function (t, e, i) {
		o.o(t, e) || Object.defineProperty(t, e, {
			enumerable: !0,
			get: i
		})
	}, o.r = function (t) {
		"undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(t, Symbol.toStringTag, {
			value: "Module"
		}), Object.defineProperty(t, "__esModule", {
			value: !0
		})
	}, o.t = function (e, t) {
		if (1 & t && (e = o(e)), 8 & t) return e;
		if (4 & t && "object" == typeof e && e && e.__esModule) return e;
		var i = Object.create(null);
		if (o.r(i), Object.defineProperty(i, "default", {
			enumerable: !0,
			value: e
		}), 2 & t && "string" != typeof e)
			for (var n in e) o.d(i, n, function (t) {
				return e[t]
			}.bind(null, n));
		return i
	}, o.n = function (t) {
		var e = t && t.__esModule ? function () {
			return t.default
		} : function () {
			return t
		};
		return o.d(e, "a", e), e
	}, o.o = function (t, e) {
		return Object.prototype.hasOwnProperty.call(t, e)
	}, o.p = "/codebase/", o(o.s = 124);

	function o(t) {
		if (n[t]) return n[t].exports;
		var e = n[t] = {
			i: t,
			l: !1,
			exports: {}
		};
		return i[t].call(e.exports, e, e.exports, o), e.l = !0, e.exports
	}
	var i, n
}), window.dhx_legacy) {
	if (window.dhx)
		for (var key in dhx) dhx_legacy[key] = dhx[key];
	window.dhx = dhx_legacy, delete window.dhx_legacy
}